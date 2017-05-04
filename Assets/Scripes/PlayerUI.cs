using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
public delegate void OnValueChange(int val);
public class PlayerUI : MonoBehaviour
{

    // Use this for initialization
    [Header("UI")]
    public Text[] gold;//金币
    public Text[] sliver;//银币 
    public Text[] copper;//铜币
    public Text integral;//积分
    public Text[] bullet;//子弹
    public Text[] food;//食物
    public Text Message;
    public GameObject shop;
    public GameObject vote;
    public GameObject Quit;
    // public PlayerState playerState;
    //商城面板

    //声明委托，购买食物的时候调用
    public OnValueChange onBuyFood;
    //声明委托，购买食物的时候调用
    public OnValueChange onBuyBullet;
    //声明委托，购买食物的时候调用
    public OnValueChange onBuyProp;
    //
    public OnValueChange onUseProp;

    public void moneyChange(Money money)
    {
        for (int i = 0; i < gold.Length; i++)
        {
            gold[i].text = money.goldCoin + "";
            sliver[i].text = money.sliverCoin + "";
            copper[i].text = money.copperCoin + "";
        }
        integral.text = money.GetIntegral() + "";
    }
    public void foodChange(float foodNum)
    {
        for (int i = 0; i < food.Length; i++)
        {
            food[i].text = foodNum + "";
        }
    }
    public void bulletChange(int bulletNum)
    {
        for (int i = 0; i < bullet.Length; i++)
        {
            bullet[i].text = bulletNum + "";
        }

    }


    public void OnBuyFood(int food)
    {
        if (onBuyFood != null)
            onBuyFood(food);
    }
    public void OnBuyBullet(int bullet)
    {
        if (onBuyBullet != null)
            onBuyBullet(bullet);
    }
    public void OnBuyProp(int prop)
    {
        if (onBuyProp != null)
            onBuyProp(prop);
    }
    public void OnUseProp(int prop)
    {
        if (onUseProp != null)
            onUseProp(prop);
    }


    public void OnChangeDay(bool isDay)
    {
        shop.SetActive(!isDay);
        vote.SetActive(!isDay);
        if (!isCreate)
        {
            StartCoroutine(CreateMakeCoin());
            isCreate = true;
        }
        if(!isDay)
        ButtonIS(!isDay);
    }

    public void PopMessage(string message)
    {
        Message.text = message;
    }
    /*****************************************技能********************************************/



    /***************************************投票系统******************************************/
    [Space]
    [Header("Vote")]
    public GameObject MakeCoin;
    public Transform father;
    GameObject[] MakeCoinUI = new GameObject[12];
    MakeCoin[] makeCoin = new MakeCoin[12];
    bool isCreate = false;
    //声明委托，投票的时候调用
    public OnValueChange onVote;
    void Start()
    {
        //StartCoroutine(CreateMakeCoin());
    }
    public void OnClick(int GO)
    {
        // onVote(GO.transform.parent.parent.GetComponent<MakeCoinUI>().ID);
        Debug.Log(GO);
    }
    private IEnumerator CreateMakeCoin()
    {

       yield return new WaitForSeconds(0);
        makeCoin = FindObjectsOfType<MakeCoin>();
        int i = 0;
        foreach (var item in makeCoin)
        {
            
            MakeCoinUI[i] = Instantiate(MakeCoin);
            MakeCoinUI[i].transform.SetParent(father);
            MakeCoinUI[i].GetComponent<MakeCoinUI>().ID = item.ID;
            MakeCoinUI[i].GetComponent<MakeCoinUI>().Owner = item.owner;
            MakeCoinUI[i].GetComponent<MakeCoinUI>().Style = item.type + "";
            GameObject go = MakeCoinUI[i].GetComponent<MakeCoinUI>().vote;
            //go.name = i + "";
            //GameObject btnObj = GameObject.Find(go.name);
            //Button btn = go.GetComponent<Button>();
            switch(item.ID)
            {
                case 1:
                    go.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        onVote(1);
                        this.OnClick(1);
                        // Debug.Log("CHUHSI" + go.GetComponent<MakeCoinUI>().ID);
                        ButtonIS(false);
                    }) ; break;
                case 2:
                    go.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        onVote(2);
                        this.OnClick(2);
                        // Debug.Log("CHUHSI" + go.GetComponent<MakeCoinUI>().ID);
                        ButtonIS(false);
                    }); break;
                case 3:
                    go.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        onVote(3);
                        this.OnClick(3);
                        // Debug.Log("CHUHSI" + go.GetComponent<MakeCoinUI>().ID);
                        ButtonIS(false);
                    }); break;
                case 4:
                    go.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        onVote(4);
                        this.OnClick(4);
                        // Debug.Log("CHUHSI" + go.GetComponent<MakeCoinUI>().ID);
                        ButtonIS(false);
                    }); break;
                case 5:
                    go.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        onVote(5);
                        this.OnClick(5);
                        // Debug.Log("CHUHSI" + go.GetComponent<MakeCoinUI>().ID);
                        ButtonIS(false);
                    }); break;
                case 6:
                    go.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        onVote(6);
                        this.OnClick(6);
                        // Debug.Log("CHUHSI" + go.GetComponent<MakeCoinUI>().ID);
                        ButtonIS(false);
                    }); break;
                case 7:
                    go.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        onVote(7);
                        this.OnClick(7);
                        // Debug.Log("CHUHSI" + go.GetComponent<MakeCoinUI>().ID);
                        ButtonIS(false);
                    }); break;
                case 8:
                    go.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        onVote(8);
                        this.OnClick(8);
                        // Debug.Log("CHUHSI" + go.GetComponent<MakeCoinUI>().ID);
                        ButtonIS(false);
                    }); break;
                case 9:
                    go.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        onVote(9);
                        this.OnClick(9);
                        // Debug.Log("CHUHSI" + go.GetComponent<MakeCoinUI>().ID);
                        ButtonIS(false);
                    }); break;
                case 10:
                    go.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        onVote(10);
                        this.OnClick(10);
                        // Debug.Log("CHUHSI" + go.GetComponent<MakeCoinUI>().ID);
                        ButtonIS(false);
                    }); break;
                case 11:
                    go.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        onVote(11);
                        this.OnClick(11);
                        // Debug.Log("CHUHSI" + go.GetComponent<MakeCoinUI>().ID);
                        ButtonIS(false);
                    }); break;
                case 12:
                    go.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        onVote(12);
                        this.OnClick(12);
                        // Debug.Log("CHUHSI" + go.GetComponent<MakeCoinUI>().ID);
                        ButtonIS(false);
                    }); break;


            }
            
            i++;
        }
        //foreach (var item in gameManager.makeCoin)
        //{
        //    MakeCoinUI[i] = Instantiate(MakeCoin);
        //    MakeCoinUI[i].transform.SetParent(father);
        //    MakeCoinUI[i].GetComponent<MakeCoinUI>().ID = item.GetComponent<MakeCoin>().ID;
        //    MakeCoinUI[i].GetComponent<MakeCoinUI>().Owner = item.GetComponent<MakeCoin>().owner;
        //    MakeCoinUI[i].GetComponent<MakeCoinUI>().Style = item.GetComponent<MakeCoin>().type + "";
        //    MakeCoinUI[i].GetComponent<MakeCoinUI>().vote.onClick.AddListener(delegate () {
        //    this.onVote(item.GetComponent<MakeCoin>().ID);
        //        ButtonIS(false);
        //    });
        //    i++;
        //}
        yield return null;

    }
    public void MakeCoinReSet()
    {
        int i = 0;
        foreach (var item in makeCoin)
        {
            MakeCoinUI[i].GetComponent<MakeCoinUI>().ID = item.ID;
            MakeCoinUI[i].GetComponent<MakeCoinUI>().Owner = item.owner;
            MakeCoinUI[i].GetComponent<MakeCoinUI>().Style = item.type + "";
            i++;
        }
    }
    public void ButtonIS(bool open)
    {
        int i = 0;
        if (open)
        {
            makeCoin = FindObjectsOfType<MakeCoin>();
            foreach (var item in makeCoin)
            {
                if (item.isDestroy)
                    MakeCoinUI[i].GetComponent<MakeCoinUI>().button.SetActive(false);
                else
                    MakeCoinUI[i].GetComponent<MakeCoinUI>().button.SetActive(true);
                i++;
            }
        }
        else
            foreach (var item in MakeCoinUI)
            {
                MakeCoinUI[i].GetComponent<MakeCoinUI>().button.SetActive(false);
                i++;
            }
    }

}
