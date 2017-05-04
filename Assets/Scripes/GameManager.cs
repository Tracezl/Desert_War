using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : NetworkBehaviour
{
    static public List<PlayerState> player = new List<PlayerState>();
    //制币机
    public GameObject makeCoinPrefab;
    public GameObject[] makeCoin = new GameObject[12];
    public Transform[] Position;
    // private Color[] color = { new Color(0.98f, 0.85f, 0.098f), new Color(0.9f, 0.91f, 0.98f),new Color(0.8f, 0.5f, 0.16f) };//金色，

    SyncListInt vote=new SyncListInt();
    private int[] voteNum = new int[12];
    private List<Money.Coin> type=new List<Money.Coin>();
    [SyncVar]
    private int voteCount = 0;
    float timer=0.0f;
    int day = 0;
    public override void OnStartServer()//当服务器创建时调用
    {
        //创建制币机
        StartCoroutine(CreatMakeCoin());
    }

    public void Start()
    {
       
    }
    [Server]
    private IEnumerator CreatMakeCoin()
    {        
        yield return new WaitForSeconds(2);
        foreach (var item in player)
        {
            item.isDay = true;
            item.RpcPopMessage("游戏开始!");
        }
        for (int i = 0; i < 4; i++)
        {
           type.Add(Money.Coin.银币);
           type.Add(Money.Coin.金币);
           type.Add(Money.Coin.铜币);
        }
        for (int i = 0; i < 12; i++)
        {
            Quaternion rotation = Quaternion.Euler(-90, 0, 0);
            rotation = Quaternion.Euler(-90, 0, 0);
            GameObject enemy = Instantiate(makeCoinPrefab, Position[i].position, rotation) as GameObject;
            makeCoin[i] = enemy;
            int j = Random.Range(0, type.Count-1);
            enemy.GetComponent<MakeCoin>().type = type[j];
            type.RemoveAt(j);
            enemy.GetComponent<MakeCoin>().ID = i + 1;
            //if(i<5)
            
            NetworkServer.Spawn(enemy);
        }
        for (int i=0;i<10;i++)
        yield return StartCoroutine(GameOneDay());
        yield return StartCoroutine(GameOver());
    }
    //游戏流程
    private IEnumerator GameOneDay()
    {
        OnDayOn();
        yield return new WaitForSeconds(10);
        OnDayOff();
        yield return new WaitForSeconds(30);
        
        yield return null;     

    }
    //游戏结束
    IEnumerator GameOver()
    {
        int flage = 0;
        for (int i = 0; i < player.Count; i++)
        {
            if (player[i].money.GetIntegral() > player[flage].money.GetIntegral())
                flage = i;
        }
        for (int i = 0; i < player.Count; i++)
        {
            if (flage == i)
                player[flage].RpcPopMessage("恭喜你！获得胜利！");
            else
                player[i].RpcPopMessage("恭喜你！获得胜利！");
        }
        //Destroy(GameObject.Find("LobbyManager"));
        StartCoroutine(ReturnToLoby());
        
        yield return null;
    }
    IEnumerator ReturnToLoby()
    {
        yield return new WaitForSeconds(3.0f);
        foreach (var item in player)
        {
            item.RpcEquit();

        }
        //UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        //Application.LoadLevel(0);
        //Prototype.NetworkLobby.LobbyManager.s_Singleton.StopServer();
        // ServerReturnToLobby();
    }
    [Server]
    void Update()
    {
         } 
    [ClientRpc]
    private void Rpcrotation(GameObject T)
    {
        T.transform.rotation= Quaternion.Euler(-90, 0, 0);
    }

    //白天状态
    private void OnDayOn()
    {
        //打开制币机
        foreach (var item in makeCoin)
        {
            item.GetComponent<MakeCoin>().Rest(true);//重置

        }
        //人物状态改变
        foreach (var item in player)
        {
            item.isDay = true;
            item.RpcPopMessage("天亮了");
        }
        
      
    }
    //黑天状态
    private void OnDayOff()
    {
        //关闭制币机
        foreach (var item in makeCoin)
        {
            item.GetComponent<MakeCoin>().Rest(false);//重置

        }
        //人物状态改变
        foreach (var item in player)
        {
            item.isDay = false;
            item.RpcPopMessage("黑夜来临，你有20秒时间进行投票");
        }
        //投票系统初始化
        for (int i = 0; i <voteNum.Length; i++)
        {
            voteNum[i] = 0;
        }
        voteCount = 0;
        vote.Clear();
        StartCoroutine(Result());
    }
    [Command]
    public void CmdVote(int id)
    {
        voteNum[id-1] += 1;
        //vote.Add(id);
        voteCount += 1;
    }
   [Server]
    private IEnumerator Result()
    {
        

        yield return new WaitForSeconds(20);
        foreach(var item in vote)
        {
            voteNum[item] += 1;
        }
        int flage = 0;
        int num = 0;
        for (int i = 0; i < voteNum.Length; i++)
        {
            if (voteNum[i] > voteNum[flage])
                flage = i;
        }
        for (int i = 0; i < voteNum.Length; i++)
        {
            if (voteNum[i] == voteNum[flage])
                num++;
        }
        if (num == 1)
        {
            makeCoin[flage].GetComponent<MakeCoin>().isDestroy = true;
            if ((voteNum[flage] * 1.0f / voteCount) > 0.7)
                foreach (var item in player)
                {
                    if (item.player + "" == makeCoin[flage].GetComponent<MakeCoin>().owner)
                    {
                        item.money.SetCoin(makeCoin[flage].GetComponent<MakeCoin>().type, 8);
                        item.RpcChangeMoney(makeCoin[flage].GetComponent<MakeCoin>().type, 8);
                        item.RpcPopMessage(flage + 1 + "号制币机获得票数超过百分之七十，你获得8个钱币奖励");
                    }
                    else
                        item.RpcPopMessage(flage + 1 + "号制币机获得票数超过百分之七十，该机的所有者获得8个钱币奖励");
                }
            else
                foreach (var item in player)
                {
                    item.RpcPopMessage(flage+1 + "号制币机被摧毁！");
                }
         }
        else
            foreach (var item in player)
            {
                item.RpcPopMessage("有两个或以上的制币机获得最高票，不销毁！");
            }
        yield return null;
    }
}
