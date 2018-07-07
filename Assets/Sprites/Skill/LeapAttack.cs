using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapAttack : MonoBehaviour
{
    //触发器
    BoxCollider2D cld;
    //使用者
    GameObject user;
    //最终伤害
    int totalDamage;
    //一次开关
    bool isOver = false;
    //找到旗手
    GameObject flagM;

    private void Awake()
    {
        user = transform.Find("/" + SQLiteManager.Instance.team[ConstData.Berserker].playerData.PrefabsID).gameObject;
        cld = transform.GetComponent<BoxCollider2D>();
        flagM = transform.Find("/1001").gameObject;
    }

    private void OnEnable()
    {
        totalDamage = 0;
        if (cld.enabled == false)
        {
            cld.enabled = true;
        }
        isOver = false;
        //0.5秒后自动回收该特效
        vp_Timer.In(0.5f, new vp_Timer.Callback(delegate () { ObjectPoolManager.Instance.RecycleMyGameObject(gameObject); }));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            if(isOver == false)
            {
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
                        user.GetComponent<HeroStates>().currentAD * 1f -
                        (user.GetComponent<HeroStates>().currentAD * 1f) *
                        (collision.GetComponent<EnemyStates>().currentDEF * 0.01f)
                        );
                    collision.GetComponent<EnemyStates>().currentHP -= totalDamage;
                }
                //敌人获得流血状态
                collision.GetComponent<EnemyStates>().GetState(3203, 3.0f);
                //敌人略微后退
                collision.GetComponent<EnemyControllers>().InvokeRepeating("Dashed", 0f, 0.02f);
                vp_Timer.In(0.2f, new vp_Timer.Callback(delegate () { collision.GetComponent<EnemyControllers>().CancelInvoke("Dashed"); }));
                //清空所有锁定目标
                vp_Timer.In(0.3f, new vp_Timer.Callback(delegate () { flagM.GetComponent<FlagManController>().ClearAllTarget(); }));
                isOver = true;
            }
        }
    }

}
