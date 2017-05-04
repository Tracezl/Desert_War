using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
public class NewBehaviourScript : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        List<string> btnsName = new List<string>();
        btnsName.Add("1");
        btnsName.Add("2");
        btnsName.Add("3");

        foreach (string btnName in btnsName)
        {
            GameObject btnObj = GameObject.Find(btnName);
            Button btn = btnObj.GetComponent<Button>();
            btn.onClick.AddListener(delegate () {
                this.OnClick(btnObj);
            });
        }
    }

    public void OnClick(GameObject sender)
    {
        //switch (sender.name)
        //{
        //    case "1":
        //        Debug.Log("BtnPlay");
        //        break;
        //    case "2":
        //        Debug.Log("BtnShop");
        //        break;
        //    case "3":
        //        Debug.Log("BtnLeaderboards");
        //        break;
        //    default:
        //        Debug.Log("none");
        //        break;
        //}
        Debug.Log(sender.name);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
