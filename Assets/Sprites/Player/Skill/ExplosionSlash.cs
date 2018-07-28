using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//剑士二阶技能附属脚本：剑气
//该脚本需挂载到剑气技能预制体下
public class ExplosionSlash : MonoBehaviour
{
    //一次开关
    internal bool isOver = false;
    //触发器
    BoxCollider2D cld;
    //最终伤害
    int totalDamage;
    //使用者
    GameObject user;
    //碰到的目标
    GameObject hitTarget;

    private void Awake()
    {
        //获取触发器
        cld = transform.GetComponent<BoxCollider2D>();
        //获取使用者
        user = transform.Find("/" + SQLiteManager.Instance.team[ConstData.Saber].playerData.PrefabsID).gameObject;
    }

    private void OnEnable()
    {
        //每次重用时重置
        isOver = false;
        if (cld.enabled == false)
        {
            cld.enabled = true;
        }
        user = transform.Find("/" + SQLiteManager.Instance.team[ConstData.Saber].playerData.PrefabsID).gameObject;
        totalDamage = 0;
        hitTarget = null;
    }

    private void Update()
    {
        //剑气持续移动
        transform.position += Vector3.right * Time.deltaTime * 4f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //因为只能对碰到的第一个敌人造成伤害但是又不回收特效，因此需要增加判断isOver
        if(collision.tag == "Enemy" && isOver == false)
        {
            if (collision.GetComponent<EnemyControllers>().isAlive == true)
            {
                //碰到敌人时关闭触发器
                if (cld.enabled == true)
                {
                    cld.enabled = false;
                }
                AudioManager.Instance.PlayEffectMusic(SoundEffect.FireHit);
                //获得目标
                hitTarget = collision.gameObject;
                //生成击打特效
                GameObject hit1 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_hit));
                hit1.transform.position = hitTarget.transform.position;
                //回收击打特效
                vp_Timer.In(1f, new vp_Timer.Callback(delegate () { ObjectPoolManager.Instance.RecycleMyGameObject(hit1); }));
                //计算伤害
                if (hitTarget.GetComponent<EnemyStates>().god == false)
                {
                    totalDamage = (int)(user.GetComponent<HeroStates>().currentAD * 1.5f -
                            (user.GetComponent<HeroStates>().currentAD * 1.5f) * (hitTarget.GetComponent<EnemyStates>().currentDEF * 0.01f));
                    hitTarget.GetComponent<EnemyStates>().currentHP -= totalDamage;
                }
                //碰到敌人后延时1s销毁特效
                vp_Timer.In(1.0f, new vp_Timer.Callback(delegate () { ObjectPoolManager.Instance.RecycleMyGameObject(gameObject); }));
                //一次开关
                isOver = true;
            }
        }
    }
}
