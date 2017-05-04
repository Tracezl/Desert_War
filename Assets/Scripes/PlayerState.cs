using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerState : NetworkBehaviour
{
    public Text nameUI;
    public Canvas UI;
    private Canvas ui;
    //人物状态
    //食物
    public Slider foodSlider;
    public const int maxFood = 100;
    [SyncVar(hook = "OnChangeFood")]//检测变化之后执行响应代码(hook = "OnChangeMoney")
    private int currentFood =  maxFood;
    public int CurrentFood
    {
        set
        {
            if (value > maxFood)
                currentFood = maxFood;
            else
            currentFood = value;
        }
        get
        {
            return currentFood;
        }
    }
    //金币
    //[SyncVar]
    public Money money=new Money();//最新值都存在客户端的
    //武器
    [SyncVar(hook = "OnChangeBullet")]
    public int bullet = 0;
    //jineng

    SkillItem[] Skill = new SkillItem[4];
    [SyncVar]
    private int skill1 = 0;
    public int Skill1
    {
        set
        {
            skill1 = value;
            Skill[0].Num = value;
        }
        get
        {
            return skill1;
        }
    }
    [SyncVar]
    private int skill2= 0;
    public int Skill2
    {
        set
        {
            skill2 = value;
            Skill[0].Num = value;

        }
        get
        {
            return skill2;
        }
    }
    [SyncVar]
    private int skill3 = 0;
    public int Skill3
    {
        set
        {
            skill3= value;
            Skill[0].Num = value;

        }
        get
        {
            return skill3;
        }
    }
    [SyncVar]
    private int skill4 = 0;
    public int Skill4
    {
        set
        {
            skill4 = value;
            Skill[0].Num = value;
        }
        get
        {
            return skill4;
        }
    }
    [SyncVar]
    public bool isDefend = false;//fanghu
    [SyncVar]
    public bool isInvincible = false;//wudi


    //个人ID
    [SyncVar(hook ="OnNameChange")]
    public string player;
    [SyncVar]
    public Color playerColor = Color.white;
    [SyncVar(hook = "OnChangeDay")]
    public bool isDay = true;


    public int damage = 1;//每秒受到的伤害
    float time = 0;
    PlayerUI playerUI;
    GameObject uI;

    void Awake()
    {
        GameManager.player.Add(this);
        
    }
    void Start()
    {
        
        if (isLocalPlayer)
        {
           // player= GetComponent<NetworkIdentity>().netId;
            ui = Instantiate<Canvas>(UI);
            uI = GameObject.Find("Canvas(Clone)");
            uI.GetComponent<PlayerUI>().onBuyFood +=OnBuyFood;//委托事件绑定
            uI.GetComponent<PlayerUI>().onBuyBullet += OnBuyBullet;
            uI.GetComponent<PlayerUI>().onBuyProp += OnBuyProp;
            uI.GetComponent<PlayerUI>().onVote+= vote;//委托事件绑定
            for(int i=0;i<4;i++)
            {
                Skill[i] = uI.transform.Find("Skill" + i).GetComponent<SkillItem>();
            }
        }
        nameUI.text = player + "";
    }
    
    public override void OnStartClient()//当客户端
    {
        
    }
    void OnTriggerEnter(Collider col)//Collision
    {

    }
    /*********************************************商店******************************************************/
    [ClientRpc]
    public void RpcChangeMoney(Money.Coin type, int num)
    {
        if(!isServer)
        money.SetCoin(type, num);
        if (isLocalPlayer)
        {
            uI.GetComponent<PlayerUI>().moneyChange(money);
        }
    }
    [Command]
    public void CmdChangeMoney(Money.Coin type, int num,int food)
    {
        RpcChangeMoney(type, num);
        
        if (money.SetCoin(type, num))
        {
            if (food <0)
                RpcPopMessage("购买成功");
            CurrentFood += food;
            
        }

        else
            RpcPopMessage("钱币不够");
    }
    [Command]
    public void CmdChangeBullet(Money.Coin type, int num, int food)
    {
        RpcChangeMoney(type, num);
        if (money.SetCoin(type, num))
        {
            bullet += food;
            RpcPopMessage("购买成功");
        }

        else
            RpcPopMessage("钱币不够");
    }
    [Command]
    public void CmdChangeProp( int prop)
    {
        RpcChangeMoney(Money.Coin.金币, Money.PropMoney((Money.Prop)prop));
        if (money.SetCoin(Money.Coin.金币, Money.PropMoney((Money.Prop)prop)))
        {switch(prop)
            {
                case 0:
                    Skill1 += 1;
                    break;
                case 1:
                    Skill2 += 1;
                    break;
                case 2:
                    Skill3 += 1;
                    break;
                case 3:
                    Skill4 += 1;
                    break;
            }
            RpcPopMessage("购买成功");
        }

        else
            RpcPopMessage("钱币不够");
    }

    public void OnBuyBullet(int num)
    {
        CmdChangeBullet(Money.Coin.银币, -2,10);
    }
    public void OnBuyFood(int food)
    {
        CmdChangeMoney(Money.Coin.铜币, Money.FoodMoney((Money.Food)food),Money.FoodAdd((Money.Food)food));
    }
    public void OnBuyProp(int prop)
    {
        CmdChangeProp(prop);
    }
    /***********************************消息弹框****************************************/
    [ClientRpc]
    public void RpcPopMessage(string message)
    {
        StartCoroutine(POPMessage(message));
    }
    private IEnumerator POPMessage(string message)
    {
        if (isLocalPlayer)
            uI.GetComponent<PlayerUI>().PopMessage(message);
        yield return new WaitForSeconds(4);
        if (isLocalPlayer)
            uI.GetComponent<PlayerUI>().PopMessage("");
        yield return null;
    }
    /**********************************投票**************************************/
    [Command]
    public void CmdVote(int id)
    {
        Debug.Log("2222222222222222222222");
        GameObject.Find("GameManager").GetComponent<GameManager>().CmdVote(id);
    }
    public void vote(int id)
    {
        CmdVote(id);
    }
    /*******************************同步变量之后的处理*******************************/
    public void OnChangeDay(bool isDay)
    {
        if (isLocalPlayer)
        {
            uI.GetComponent<PlayerUI>().OnChangeDay(isDay);
            if (!isDay)
                GetComponent<PlayerControl>().canMove = true;
        }
    }
    void OnNameChange(string player)
    {
        nameUI.text = player + "";
    }
    public void TakeDamage(int damage)
    {

        if (isServer == false) return;// 血量的处理只在服务器端执行

        
        if (currentFood <= 0)//当食物为0时
        {
            GetComponent<PlayerControl>().canMove = false;
            RpcRespawn();
        }
        else
            currentFood -= damage;

    }
    void OnChangeFood(int Food)
    {
       foodSlider.value = Food;
        if(isLocalPlayer)
       uI.GetComponent<PlayerUI>().foodChange(Food);
    }
    void OnChangeBullet(int bullet)
    {
        if (isLocalPlayer)
            uI.GetComponent<PlayerUI>().bulletChange(bullet);
    }

    [Server]
    void Update()
    {
        //OnChangeMoney(money);
        if (isDay)//如果是白天的话持续扣血，晚上则不扣
        {
            time += Time.deltaTime;
            if (time > 1)
            {
                TakeDamage(damage);
                time = 0;
            }
        }

    }
    [ClientRpc]//客户端调用
    void RpcRespawn()
    {
        GetComponent<PlayerControl>().canMove = false;
    }
    /********************************小地图****************************/
    [Header("MiniMap")]
    public GameObject show;
    public override void OnStartLocalPlayer()
    {
        //这个方法只会在本地角色那里调用  当角色被创建的时候
        show.GetComponent<MeshRenderer>().material.color = Color.blue;      
    }
    /**************************************jineng*********************************************/
    [ClientRpc]
    public void RpcEquit()
    {
        if (isLocalPlayer)
            uI.GetComponent<PlayerUI>().Quit.SetActive(true);
    }
}
