using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fissure : MonoBehaviour
{
    SpriteRenderer red;
    Color r;
    GameObject boom;
    GameObject boomEffect;

    private void Awake()
    {
        boom = Resources.Load("Prefabs/EffectPrefabs/Effect_fissureBoom") as GameObject;
        red = transform.GetChild(0).GetComponent<SpriteRenderer>();
        r = red.color;
        r.a = 0;
        red.color = r;
    }
    
    private void Update()
    {
        if (r.a < 1)
        {
            r.a += 0.01f;
            red.color = r;
        }
        else
        {
            Destroy(gameObject, 0.2f);
            FissureBoom();
        }
    }

    bool boomSwitch = false;
    void FissureBoom()
    {
        if (boomSwitch == false)
        {
            boomEffect = Instantiate(boom);
            boomEffect.transform.position = gameObject.transform.position;
            Destroy(boomEffect, 1f);
            boomSwitch = true;
        }
    }

}
