using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("a");
        if (collision.tag == "Effect")
        {
            Debug.Log(collision.name);
        }
    }
}
