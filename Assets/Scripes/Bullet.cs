using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {


    void OnCollisionEnter(Collision col)
    {
        GameObject hit = col.gameObject;
        PlayerState health = hit.GetComponent<PlayerState>();

        if (health != null)
        {
            health.TakeDamage(10);
        }
        Destroy(this.gameObject);
    }

}

