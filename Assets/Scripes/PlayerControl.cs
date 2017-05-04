using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
public class PlayerControl : NetworkBehaviour
{

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Camera camera;
    [SyncVar]
    public bool canMove = true;
    [SyncVar(hook = "change")]
    float v;
    public GameObject UI;//自身的血条


    public float speed = 5;
    public float angularSpeed = 30;
    //public NetworkPlayer ownerPlayer;
    private PlayerAnimation playerAnimation;
    // Update is called once per frame
    private Rigidbody rigidbody;
    void Awake()
    {
        playerAnimation = this.GetComponent<PlayerAnimation>();
        rigidbody = this.GetComponent<Rigidbody>();

    }
    void Update()
    {

        if (!isLocalPlayer)
        {
            LoseControl();
            return;
        }
        UI.SetActive(false);

        float h = Input.GetAxis("Horizontal");
        if (this.transform.position.y < 1.14)
            v = Input.GetAxis("Vertical");
        //transform.Rotate(Vector3.up * h * 120 * Time.deltaTime);
        //transform.Translate(Vector3.forward * v * 3 * Time.deltaTime);
        if (!canMove)
            v = 0;
        if(v>-1.1&&v<1.1)
            rigidbody.velocity = transform.forward * v * speed * Time.deltaTime;
            rigidbody.angularVelocity = transform.up * h * angularSpeed * Time.deltaTime;
          CmdPlayState(v);
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            this.transform.position += new Vector3(0, 0.5f, 0);
            CmdPlayState(3);
        }
        if (Input.GetMouseButtonDown(1))
        {
            CmdFire();
        }
        //float cv;
        //if (Input.GetMouseButton(0))
        //{//鼠标按着左键移动 
        //    cv = Input.GetAxis("Mouse Y") * Time.deltaTime * 100;//鼠标纵向增量（纵向移动
        //}
        //else
        //{
        //   cv = 0;
        //}

        float cv;
        float ch;
        if (Input.GetMouseButton(0))
        {//鼠标按着左键移动 
            cv = Input.GetAxis("Mouse Y") * Time.deltaTime * 100;//鼠标纵向增量（纵向移动
            ch = Input.GetAxis("Mouse X") * Time.deltaTime * 100;
        }
        else
        {
            cv = 0;
            ch = 0;
        }

        this.transform.Rotate(new Vector3(0, ch, 0));

        camera.transform.Rotate(new Vector3(-cv,0, 0));

    }

 
    public override void OnStartLocalPlayer()
    {
        //这个方法只会在本地角色那里调用  当角色被创建的时候
        //GetComponent<MeshRenderer>().material.color = Color.blue;
        //camera = Camera.main;
       // Debug.Log("QQ");
    }


    [Command]// called in client, run in server
    void CmdFire()//这个方法需要在server里面调用
    {
        // 子弹的生成 需要server完成，然后把子弹同步到各个client
        if (GetComponent<PlayerState>().bullet > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0)) + camera.transform.forward * 2, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody>().velocity = (camera.transform.forward).normalized * 100;
            Destroy(bullet, 2);
            NetworkServer.Spawn(bullet);
            CmdPlayState(2);
            GetComponent<PlayerState>().bullet -= 1;
        }

        //Vector3 point = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2,0));
        //RaycastHit hitinfo;
        //bool isCollider = Physics.Raycast(point, camera.transform.forward, out hitinfo);
        //if (isCollider)
        //{
        //    bullet.transform.LookAt(hitinfo.point);
        //}
        //else
        //{
        //    point += camera.transform.forward * 1000;
        //    bullet.transform.LookAt(point);
        //}
        
    }
    public void LoseControl()//禁用一些东西
    {
       
        camera.gameObject.SetActive(false);
        playerAnimation.enabled = false;
    }
    //动画控制
    [Command]
    void CmdPlayState(float k)
    {
        v = k;

    }
    void change(float v)
    {
        if (v > 0 && v < 1.1f)
            GetComponent<Animation>().CrossFade("Run", 0.2f);
        else if (v < 0 && v > -1.1f)
            GetComponent<Animation>().CrossFade("Backward", 0.2f);
        else if (v == 0)
            GetComponent<Animation>().CrossFade("Standby", 0.2f);
        else if (v == 2)
            GetComponent<Animation>().CrossFade("Fire", 0f);
        else if (v == 3)
            GetComponent<Animation>().CrossFade("Jump", 0.2f);
    }

}
