using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    BoxCollider2D wall;

    Vector3 savePoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && savePoint == null)
        {
            savePoint = collision.transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            //collision.transform.position = new Vector3(savePoint.x - 0.02f, savePoint.y, savePoint.z);
            collision.GetComponent<EnemyControllers>().CancelInvoke("Dashed");
            collision.transform.position -= Vector3.right * Time.deltaTime * 1f;
        }

        if (collision.tag == "Bow")
        {
            ObjectPoolManager.Instance.RecycleMyGameObject(collision.gameObject);
        }

        if (collision.tag == "Effect")
        {
            vp_Timer.In(0.2f, new vp_Timer.Callback(delegate () { ObjectPoolManager.Instance.RecycleMyGameObject(collision.gameObject); }));
        }
    }
}
