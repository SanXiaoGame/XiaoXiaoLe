using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCubeBoom : MonoBehaviour
{
    Rigidbody2D ic1;
    Rigidbody2D ic2;
    Rigidbody2D ic3;
    Rigidbody2D ic4;
    Rigidbody2D ic5;
    Rigidbody2D ic6;
    Rigidbody2D ic7;
    Rigidbody2D ic8;
    Rigidbody2D ic9;
    Rigidbody2D ic10;
    PolygonCollider2D ci1;
    PolygonCollider2D ci2;
    PolygonCollider2D ci3;
    PolygonCollider2D ci4;
    PolygonCollider2D ci5;
    PolygonCollider2D ci6;
    PolygonCollider2D ci7;
    PolygonCollider2D ci8;
    PolygonCollider2D ci9;
    PolygonCollider2D ci10;

    private void Awake()
    {
        ic1 = transform.GetChild(0).GetComponent<Rigidbody2D>();
        ic2 = transform.GetChild(1).GetComponent<Rigidbody2D>();
        ic3 = transform.GetChild(2).GetComponent<Rigidbody2D>();
        ic4 = transform.GetChild(3).GetComponent<Rigidbody2D>();
        ic5 = transform.GetChild(4).GetComponent<Rigidbody2D>();
        ic6 = transform.GetChild(5).GetComponent<Rigidbody2D>();
        ic7 = transform.GetChild(6).GetComponent<Rigidbody2D>();
        ic8 = transform.GetChild(7).GetComponent<Rigidbody2D>();
        ic9 = transform.GetChild(8).GetComponent<Rigidbody2D>();
        ic10 = transform.GetChild(9).GetComponent<Rigidbody2D>();
        ci1 = transform.GetChild(0).GetComponent<PolygonCollider2D>();
        ci2 = transform.GetChild(1).GetComponent<PolygonCollider2D>();
        ci3 = transform.GetChild(2).GetComponent<PolygonCollider2D>();
        ci4 = transform.GetChild(3).GetComponent<PolygonCollider2D>();
        ci5 = transform.GetChild(4).GetComponent<PolygonCollider2D>();
        ci6 = transform.GetChild(5).GetComponent<PolygonCollider2D>();
        ci7 = transform.GetChild(6).GetComponent<PolygonCollider2D>();
        ci8 = transform.GetChild(7).GetComponent<PolygonCollider2D>();
        ci9 = transform.GetChild(8).GetComponent<PolygonCollider2D>();
        ci10 = transform.GetChild(9).GetComponent<PolygonCollider2D>();
    }

    int tim = 0;
    bool NONONO = false;
    private void Update()
    {
        if (tim > 100 && NONONO == false)
        {
            ci1.enabled = true;
            ci2.enabled = true;
            ci3.enabled = true;
            ci4.enabled = true;
            ci5.enabled = true;
            ci6.enabled = true;
            ci7.enabled = true;
            ci8.enabled = true;
            ci9.enabled = true;
            ci10.enabled = true;
            ic1.gravityScale = 1;
            ic2.gravityScale = 1;
            ic3.gravityScale = 1;
            ic4.gravityScale = 1;
            ic5.gravityScale = 1;
            ic6.gravityScale = 1;
            ic7.gravityScale = 1;
            ic8.gravityScale = 1;
            ic9.gravityScale = 1;
            ic10.gravityScale = 1;
            NONONO = true;
            Destroy(gameObject, 1f);
        }
        tim++;
    }



}
