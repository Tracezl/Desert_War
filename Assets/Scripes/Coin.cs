using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
    float v = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward * v * 120 * Time.deltaTime);
        //transform.Translate(Vector3.forward * v * 3 * Time.deltaTime);
    }
}
