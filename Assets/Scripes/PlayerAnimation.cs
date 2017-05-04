using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerAnimation : NetworkBehaviour
{

    // Use this for initialization
    
    float v;
    void Start()
    { }
    

    // Update is called once per frame
    void Update()
    {

        v = Input.GetAxis("Vertical");
        
        
        //GetComponent<NetworkView>().RPC("CmdPlayState", RPCMode.All, "Standby");
        //CmdPlayState("Standby");
        //networkAnimator.SetTrigger("Standby");
        //if (Input.GetKeyDown(KeyCode.Space))
        // GetComponent<NetworkView>().RPC("CmdPlayState", RPCMode.All, "Jump");
        //{
          //  CmdPlayState("Jump");
            //networkAnimator.SetTrigger("Jump");
        //}
    }
     
    
}