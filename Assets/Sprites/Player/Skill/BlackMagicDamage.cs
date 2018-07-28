using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMagicDamage : MonoBehaviour
{
    //击中目标列表
    List<GameObject> enemyList;
    //触发器
    BoxCollider2D cld;
    //使用者
    GameObject user;
    //最终伤害
    int totalDamage;
    //找到旗手
    GameObject flagM;

    private void Awake()
    {
        cld = transform.GetComponent<BoxCollider2D>();
        enemyList = new List<GameObject>();
        user = transform.Find("/" + SQLiteManager.Instance.team[ConstData.Caster].playerData.PrefabsID).gameObject;
        flagM = transform.Find("/1001").gameObject;
    }

    private void OnEnable()
    {
        vp_Timer.In(0.2f, new vp_Timer.Callback(delegate () { AudioManager.Instance.PlayEffectMusic(SoundEffect.BlackMaigBoom); }));
        if (cld.enabled == false)
        {
            cld.enabled = true;
        }
        enemyList.Clear();
        totalDamage = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (enemyList.Contains(collision.gameObject) == false)
            {
                enemyList.Add(collision.gameObject);
                //计算伤害
                if (collision.GetComponent<EnemyStates>().god == false)
                {
                    totalDamage = (int)
                        (
                        (
                        user.GetComponent<HeroStates>().currentAP * 2f -
                        (user.GetComponent<HeroStates>().currentAP * 2f) *
                        (collision.GetComponent<EnemyStates>().currentRES * 0.01f)
                        ) * 0.5f
                        );
                    collision.GetComponent<EnemyStates>().currentHP -= totalDamage;
                }
                //清空所有锁定目标
                vp_Timer.In(0.2f, new vp_Timer.Callback(delegate () { flagM.GetComponent<FlagManController>().ClearAllTarget(); }));
            }
        }
    }

    void hitEffect()
    {
        //生成击打特效
        GameObject hit1 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_hit));
        hit1.transform.position = transform.position;
        //回收击打特效
        vp_Timer.In(0.1f, new vp_Timer.Callback(delegate () { ObjectPoolManager.Instance.RecycleMyGameObject(hit1); }));
    }
}
