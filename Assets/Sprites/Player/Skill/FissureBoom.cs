using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FissureBoom : MonoBehaviour
{
    //触发器
    BoxCollider2D cld;
    //使用者
    GameObject user;
    //最终伤害
    int totalDamage;
    //找到旗手
    GameObject flagM;
    //被击中的目标列表
    List<GameObject> enemyList;


    private void Awake()
    {
        cld = transform.GetComponent<BoxCollider2D>();
        user = transform.Find("/" + SQLiteManager.Instance.team[ConstData.Berserker].playerData.PrefabsID).gameObject;
        flagM = transform.Find("/1001").gameObject;
        enemyList = new List<GameObject>();
    }

    private void OnEnable()
    {
        totalDamage = 0;
        if (cld.enabled == false)
        {
            cld.enabled = true;
        }
        enemyList.Clear();
        //0.5秒后自动回收该特效
        vp_Timer.In(0.5f, new vp_Timer.Callback(delegate () { ObjectPoolManager.Instance.RecycleMyGameObject(gameObject); }));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
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
                    totalDamage = (int)
                        (
                        (
                        user.GetComponent<HeroStates>().currentAD * 2f -
                        (user.GetComponent<HeroStates>().currentAD * 2f) *
                        (collision.GetComponent<EnemyStates>().currentDEF * 0.01f)
                        ) * 0.5f
                        );
                    collision.GetComponent<EnemyStates>().currentHP -= totalDamage;
                }
                //清空所有锁定目标
                vp_Timer.In(0.3f, new vp_Timer.Callback(delegate () { flagM.GetComponent<FlagManController>().ClearAllTarget(); }));
            }
        }
    }

}
