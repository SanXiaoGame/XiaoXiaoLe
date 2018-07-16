using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRainDown : MonoBehaviour
{
    //特效预制体
    GameObject fireEffect;
    BoxCollider2D cld;
    List<GameObject> enemyList;

    //使用者
    GameObject user;
    //最终伤害
    int totalDamage;
    //找到旗手
    GameObject flagM;

    private void Awake()
    {
        fireEffect = ResourcesManager.Instance.FindPrefab(EffectPrefabs.Effect_arrowRain);
        cld = transform.GetComponent<BoxCollider2D>();
        enemyList = new List<GameObject>();
        user = transform.Find("/" + SQLiteManager.Instance.team[ConstData.Hunter].playerData.PrefabsID).gameObject;
        flagM = transform.Find("/1001").gameObject;
    }

    private void OnEnable()
    {
        if (cld.enabled == false)
        {
            cld.enabled = true;
        }
        enemyList.Clear();
        totalDamage = 0;
    }

    private void Update()
    {
        transform.position -= Vector3.up * Time.deltaTime * 8f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Plane")
        {
            AudioManager.Instance.PlayEffectMusic(SoundEffect.Fire);
            GameObject o1 = ObjectPoolManager.Instance.InstantiateMyGameObject(fireEffect);
            o1.transform.position = gameObject.transform.position;
            ObjectPoolManager.Instance.RecycleMyGameObject(gameObject);
            vp_Timer.In(1.0f, new vp_Timer.Callback(delegate () { ObjectPoolManager.Instance.RecycleMyGameObject(o1); }));
        }
        if (collision.tag == "Enemy")
        {
            if (enemyList.Contains(collision.gameObject) == false)
            {
                enemyList.Add(collision.gameObject);
                //生成击打特效
                GameObject hit1 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(EffectPrefabs.Effect_hit));
                hit1.transform.position = collision.transform.position;
                //回收击打特效
                vp_Timer.In(1f, new vp_Timer.Callback(delegate () { ObjectPoolManager.Instance.RecycleMyGameObject(hit1); }));
                //计算伤害
                if (collision.GetComponent<EnemyStates>().god == false)
                {
                    totalDamage = (int)((user.GetComponent<HeroStates>().currentAD * 2f) * 0.25f);
                    collision.GetComponent<EnemyStates>().currentHP -= totalDamage;
                }
                //敌人获得燃烧状态
                collision.GetComponent<EnemyStates>().GetState(3202, 4.0f);
                //清空所有锁定目标
                vp_Timer.In(0.2f, new vp_Timer.Callback(delegate () { flagM.GetComponent<FlagManController>().ClearAllTarget(); }));
            }
        }
    }
}
