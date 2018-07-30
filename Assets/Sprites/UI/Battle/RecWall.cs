using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecWall : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ConstData.Effect)
        {
            Destroy(collision.gameObject);
        }
        if (collision.tag == ConstData.Enemy)
        {
            Destroy(collision.gameObject);
        }
    }
}
