using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
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
        user = transform.Find("/" + SQLiteManager.Instance.team[ConstData.Caster].playerData.PrefabsID).gameObject;
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
    }

    private void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * 4.5f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (isOver == false)
            {
                AudioManager.Instance.PlayEffectMusic(SoundEffect.Hit);
                //生成击打特效
                GameObject hit1 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_hit));
                hit1.transform.position = collision.transform.position;
                //回收击打特效
                vp_Timer.In(1f, new vp_Timer.Callback(delegate () { ObjectPoolManager.Instance.RecycleMyGameObject(hit1); }));
                //回收法球
                ObjectPoolManager.Instance.RecycleMyGameObject(gameObject);
                //计算伤害
                if (collision.GetComponent<EnemyStates>().god == false)
                {
                    totalDamage = (int)
                        (
                        (
                        user.GetComponent<HeroStates>().currentAP * 1f -
                        (user.GetComponent<HeroStates>().currentAP * 1f) *
                        (collision.GetComponent<EnemyStates>().currentRES * 0.01f)
                        ) * 0.1f
                        );
                    collision.GetComponent<EnemyStates>().currentHP -= totalDamage;
                }
                isOver = true;
            }
        }
    }
}
