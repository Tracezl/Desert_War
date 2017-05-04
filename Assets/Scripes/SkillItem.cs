using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillItem : MonoBehaviour
{
    public float coldTime = 2;
    public KeyCode keycode;
    private float timer = 0;
    private Image filledImage;
    private bool isStartTimer = false;
    private Text show;
    private int num = 0;
    public int Num
    {
        set
        {
            num = value;
            show.text = value + "";
        }
    }
	// Use this for initialization
	void Start ()
	{
	    filledImage = transform.Find("FilledImage").GetComponent<Image>();
        show = transform.Find("show").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(keycode))
        {
            if (num > 0)
            {
                isStartTimer = true;
                Num = num-1;
            }
        }
        if (isStartTimer)
	    {
	        timer += Time.deltaTime;
	        filledImage.fillAmount = (coldTime - timer)/coldTime;
	        if (timer >= coldTime)
	        {
	            filledImage.fillAmount = 0;
	            timer = 0;
	            isStartTimer = false;
	        }
	    }
	}

    public void OnClick()
    {
        if (num > 0) {
            isStartTimer = true;
            Num = num - 1;
        }
    }
}
