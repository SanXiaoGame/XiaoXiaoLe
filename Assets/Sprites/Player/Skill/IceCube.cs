using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCube : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Plane")
        {
            vp_Timer.In(0.2f, new vp_Timer.Callback(delegate ()
            {
                gameObject.SetActive(false);
                transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
                transform.GetComponent<Rigidbody2D>().gravityScale = 0;
            }));
        }
    }
}
