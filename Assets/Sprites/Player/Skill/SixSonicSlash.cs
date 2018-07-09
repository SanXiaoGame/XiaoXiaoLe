using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//剑士三阶技能附属脚本：六光连斩
//该脚本需挂载到5个六光连斩技能预制体下
public class SixSonicSlash : MonoBehaviour
{
    //碰过的敌人列表
    List<GameObject> targetList;
    //最终伤害
    int totalDamage;
    //使用者
    GameObject user;
    //触发器
    BoxCollider2D cld;

    private void Awake()
    {
        user = transform.Find("/" + SQLiteManager.Instance.team[ConstData.Saber].playerData.PrefabsID).gameObject;
        targetList = new List<GameObject>();
        cld = transform.GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        //每次使用时重置
        totalDamage = 0;
        user = transform.Find("/" + SQLiteManager.Instance.team[ConstData.Saber].playerData.PrefabsID).gameObject;
        targetList.Clear();
        if (cld.enabled == false)
        {
            cld.enabled = true;
        }
        //0.5秒后自动回收该特效
        vp_Timer.In(0.5f, new vp_Timer.Callback(delegate () { ObjectPoolManager.Instance.RecycleMyGameObject(gameObject); }));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (targetList.Contains(collision.gameObject) == false && collision.GetComponent<EnemyControllers>().isAlive == true)
            {
                targetList.Add(collision.gameObject);
                //生成击打特效
                GameObject hit1 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(EffectPrefabs.Effect_hit));
                hit1.transform.position = collision.transform.position;
                //回收击打特效
                vp_Timer.In(1f, new vp_Timer.Callback(delegate () { ObjectPoolManager.Instance.RecycleMyGameObject(hit1); }));
                //计算伤害
                if (collision.GetComponent<EnemyStates>().god == false)
                {
                    //因为六光连斩一共进行5次攻击，所以伤害要 * 0.2
                    totalDamage = (int)
                        (
                        (
                        user.GetComponent<HeroStates>().currentAD * 2f -
                        (user.GetComponent<HeroStates>().currentAD * 2f) *
                        (collision.GetComponent<EnemyStates>().currentDEF * 0.01f)
                        ) * 0.2f
                        );
                    collision.GetComponent<EnemyStates>().currentHP -= totalDamage;
                }
            }
        }
    }

}
