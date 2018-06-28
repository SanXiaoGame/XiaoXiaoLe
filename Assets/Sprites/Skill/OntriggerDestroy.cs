using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OntriggerDestroy : MonoBehaviour {

    //碰到敌人就销毁特效
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            Destroy(this.gameObject, 0.1f);
        }
    }
}
