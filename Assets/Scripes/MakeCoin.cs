using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MakeCoin : NetworkBehaviour
{
    public GameObject[] child;
    public Material[] material;
    public Text UI;
    public GameObject show;
    bool hasOwner=false;//制币机是否被拥有
    [SyncVar]
    public string owner;//制币机拥有者
    [SyncVar]
    public int ID;//制币机ID
    [SyncVar(hook = "SetMaterial")]
    public Money.Coin type;//制币机的种类
    [SyncVar(hook = "UIUpdate")]//(
    public int num=2;//制币机上的钱币数
    int numInit=2;//每次初始化的钱币数
    [SyncVar(hook = "SetState")]
    public bool isDestroy = false;
    bool isOpen = true;
    bool isSafe=true;//是否是安全时间
    float safeTime = 40;
    
    // Use this for initialization
    
	void Start () {
        
    }
	
	// Update is called once per frame
    [Server]
	void Update ()
    {
        
    }
    void LateUpdate()
    {
        UI.text = ID+"";
    }
    public void SetMaterial(Money.Coin type)
    {

        if(type==Money.Coin.金币)
        {
            foreach (var item in child)
            {
                item.GetComponent<Renderer>().material = material[0];
            }
            show.GetComponent<Renderer>().material = material[0];
        }
        if (type == Money.Coin.银币)
        {
            foreach (var item in child)
            {
                item.GetComponent<Renderer>().material = material[1];
            }
            show.GetComponent<Renderer>().material = material[1];
        }
        if (type == Money.Coin.铜币)
        {
            foreach (var item in child)
            {
                item.GetComponent<Renderer>().material = material[2];
            }
            show.GetComponent<Renderer>().material = material[2];
        }
    }
    [Server]
    void OnCollisionEnter(Collision col) //Collision Trigger
    {
        if (!isOpen)
            return;

        if (col.collider.tag == "Player")
        {
            string player = col.collider.GetComponent<PlayerState>().player + "";
            if (!hasOwner)
            {
                owner = player;
                hasOwner = true;
            }

            else
            {
                if (isSafe && owner != player)
                    return;
                else
                {
                    col.collider.GetComponent<PlayerState>().CmdChangeMoney(type, num,0);
                    //RpcChangeMoney(col.gameObject);
                    num = 0;
                   // RpcUIUpdate(num);
                }
            }

        } 
    }

    [ClientRpc]
    public void RpcChangeMoney(GameObject col)
    {
      col.GetComponent<PlayerState>().CmdChangeMoney(type, num,0);
    }

    //private IEnumerator GameLoop()
    //{
    //    yield return new WaitForSeconds(2.0f);
    //    yield return StartCoroutine(TimeIsCome());
    //}
    /// <summary>
    /// 安全时间处理
    /// </summary>
    /// <returns></returns>
    [Server]
    private IEnumerator TimeIsCome()
    {

        yield return new WaitForSeconds(safeTime);
      
            num = 1;
            isSafe = false;
       
        yield return null;
    }
    public void Rest(bool IsOpen)//白天黑夜切换
    {
        if (IsOpen)
        {
            num = numInit;
            isSafe = true;
            isOpen = true;
            if (!isDestroy)
                StartCoroutine(TimeIsCome());
        }
        else
        {
            num = 0;
            isOpen = false;
        }
    }
    public void SetState(bool isDestroy)
    {
        if(isDestroy)
        {
            gameObject.SetActive(false);
        }
    }

    public void UIUpdate(int u)
    {
        if (u == 2)
        {
            child[0].SetActive(true);
            child[1].SetActive(true);
            child[0].transform.localPosition = new Vector3(-0.001f,0f, 0.0021f);
            child[1].transform.localPosition = new Vector3(0.001f, 0f, 0.0021f);
 

        }
        else if (u == 1)
        {
            child[0].SetActive(false);
            child[1].transform.localPosition = new Vector3(0, 0f, 0.0021f);
        }
        else
        {
            child[0].SetActive(false);
            child[1].SetActive(false);
        }

    } 
}
