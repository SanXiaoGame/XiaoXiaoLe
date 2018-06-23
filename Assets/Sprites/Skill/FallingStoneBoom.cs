using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStoneBoom : MonoBehaviour
{
    CircleCollider2D ballCollider;
    GameObject ballBoom;
    GameObject o1;

    private void Awake()
    {
        ballCollider = transform.GetComponent<CircleCollider2D>();
        ballBoom = Resources.Load("Prefabs/EffectPrefabs/Effect_fallingStoneBoom") as GameObject;
    }

    private void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * 2.5f;
        transform.position -= Vector3.up * Time.deltaTime * 2.5f;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Plane")
        {
            o1 = Instantiate(ballBoom);
            //o1 = ObjectPoolManager.Instance.InstantiateBlockObject(ballBoom);
            o1.transform.position = gameObject.transform.position;
            Destroy(gameObject, 0.1f);
            Destroy(o1, 1.5f);
            //Invoke("DestroyGameObject", 0.1f);
            //Invoke("DestroyO1", 1.5f);
        }
    }

    //void DestroyO1()
    //{
    //    ObjectPoolManager.Instance.RecycleBlockObject(o1);
    //}
}
