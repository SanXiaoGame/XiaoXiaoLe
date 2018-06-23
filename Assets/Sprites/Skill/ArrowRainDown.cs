using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRainDown : MonoBehaviour
{
    GameObject fireEffect;
    GameObject o1;

    private void Awake()
    {
        fireEffect = Resources.Load("Prefabs/EffectPrefabs/Effect_arrowRain") as GameObject;
    }

    private void Update()
    {
        transform.position -= Vector3.up * Time.deltaTime * 8f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Plane")
        {
            o1 = Instantiate(fireEffect);
            o1.transform.position = gameObject.transform.position;
            Destroy(gameObject);
            Destroy(o1, 1f);
        }
    }
}
