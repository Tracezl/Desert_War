using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MakeCoinUI : MonoBehaviour {

    public Text IDText;
    public Text ownerText;
    public Text styleText;
    public GameObject vote;
    public GameObject button;
    private int id;
    private string owner;
    private string style;
    public int ID
    {
        set { id = value;
            IDText.text = value+"";
        }
        get
        {
            return id;
        }
    }
    public string Owner
    {
        set
        {
            owner = value;
            ownerText.text = "所有者"+ value ;
        }
    }
    public string Style
    {
        set
        {
            style = value;
            styleText.text = "币种：" + value;
        }
    }

	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
