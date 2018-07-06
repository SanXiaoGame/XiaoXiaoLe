using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fissure : MonoBehaviour
{
    SpriteRenderer red;
    Color r;
    GameObject boom;
    bool boomSwitch = false;
    //使用者
    GameObject user;

    private void Awake()
    {
        boom = ResourcesManager.Instance.FindPrefab(EffectPrefabs.Effect_fissureBoom);
        red = transform.GetChild(0).GetComponent<SpriteRenderer>();
        user = transform.Find("/" + SQLiteManager.Instance.team[ConstData.Berserker].playerData.PrefabsID).gameObject;
        r = red.color;
        r.a = 0;
        red.color = r;
    }

    private void OnEnable()
    {
        r.a = 0;
        red.color = r;
        boomSwitch = false;
        InvokeRepeating("FissureUp", 0f, 0.02f);
    }

    private void Update()
    {
        if (r.a <= 1)
        {
            r.a += 0.01f;
            red.color = r;
        }
        else
        {
            if (boomSwitch == false)
            {
                vp_Timer.In(0.2f, new vp_Timer.Callback(delegate () { ObjectPoolManager.Instance.RecycleMyGameObject(gameObject); }));
                FissureBoom();
            }
        }
    }
    
    void FissureBoom()
    {
        if (boomSwitch == false)
        {
            GameObject efctBoom = ObjectPoolManager.Instance.InstantiateMyGameObject(boom);
            efctBoom.transform.position = transform.position;
            vp_Timer.In(1.0f, new vp_Timer.Callback(delegate () { ObjectPoolManager.Instance.RecycleMyGameObject(efctBoom); }));
            boomSwitch = true;
        }
    }

    internal void FissureUp()
    {
        transform.position += Vector3.up * Time.deltaTime * 3f;
        if (transform.position.y >= (user.transform.position + new Vector3(1f, -0.2f, 0)).y)
        {
            CancelInvoke("FissureUp");
        }
    }

}
