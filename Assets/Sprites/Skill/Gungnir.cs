using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gungnir : MonoBehaviour
{
    GameObject ShootEffect;
    GameObject o1;

    private void Awake()
    {
        ShootEffect = Resources.Load("Prefabs/EffectPrefabs/Effect_gungnirShoot") as GameObject;
    }

    int tim = 0;
    bool shootSwitch = false;
    private void Update()
    {
        if (tim > 55)
        {
            transform.position += Vector3.right * Time.deltaTime * 12f;
        }
        if (tim > 55 && shootSwitch == false)
        {
            o1 = Instantiate(ShootEffect);
            o1.transform.position = gameObject.transform.position;
            Destroy(o1, 0.7f);
            Destroy(gameObject, 3f);
            shootSwitch = true;
        }
        tim++;
    }
}
