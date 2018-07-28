using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMagicBoom : MonoBehaviour
{
    GameObject boomEffect;

    private void Awake()
    {
        boomEffect = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_blackMagic);
    }

    private void OnEnable()
    {
        vp_Timer.In(3.0f, new vp_Timer.Callback(delegate () { MagicBurst(); }));
    }

    void MagicBurst()
    {
        GameObject blk = ObjectPoolManager.Instance.InstantiateMyGameObject(boomEffect);
        blk.transform.position = transform.position;
        ObjectPoolManager.Instance.RecycleMyGameObject(gameObject);
        vp_Timer.In(2.0f, new vp_Timer.Callback(delegate () { ObjectPoolManager.Instance.RecycleMyGameObject(blk); }));
    }
}
