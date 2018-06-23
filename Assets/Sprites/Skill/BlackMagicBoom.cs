using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMagicBoom : MonoBehaviour
{
    GameObject boomEffect;
    GameObject o1;

    private void Awake()
    {
        boomEffect = Resources.Load("Prefabs/EffectPrefabs/Effect_blackMagic") as GameObject;
    }

    int tim = 0;
    bool boomSwitch = false;
    private void Update()
    {
        if (tim > 150 && boomSwitch == false)
        {
            o1 = Instantiate(boomEffect);
            o1.transform.position = gameObject.transform.position;
            Destroy(gameObject);
            Destroy(o1, 2f);
            boomSwitch = true;
        }
        if (tim < 160)
        {
            tim++;
        }
    }
}
