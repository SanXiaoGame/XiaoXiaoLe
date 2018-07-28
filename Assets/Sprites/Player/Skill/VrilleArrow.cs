using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrilleArrow : MonoBehaviour
{
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
        vp_Timer.In(1.0f, new vp_Timer.Callback(delegate () { ObjectPoolManager.Instance.RecycleMyGameObject(gameObject); }));
    }

    private void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * 5f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (enemyList.Contains(collision.gameObject) == false)
            {
                AudioManager.Instance.PlayEffectMusic(SoundEffect.Gungnir_Hit);
                enemyList.Add(collision.gameObject);
                //生成击打特效
                GameObject hit1 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_hit));
                hit1.transform.position = collision.transform.position;
                //回收击打特效
                vp_Timer.In(1f, new vp_Timer.Callback(delegate () { ObjectPoolManager.Instance.RecycleMyGameObject(hit1); }));
                //敌人略微后退
                collision.GetComponent<EnemyControllers>().InvokeRepeating("Dashed", 0f, 0.02f);
                vp_Timer.In(0.3f, new vp_Timer.Callback
                    (delegate () { collision.GetComponent<EnemyControllers>().CancelInvoke("Dashed"); }));
                //计算伤害
                if (collision.GetComponent<EnemyStates>().god == false)
                {
                    totalDamage = (int)(user.GetComponent<HeroStates>().currentAD * 1.5f);
                    collision.GetComponent<EnemyStates>().currentHP -= totalDamage;
                }
                //清空所有锁定目标
                vp_Timer.In(0.2f, new vp_Timer.Callback(delegate () { flagM.GetComponent<FlagManController>().ClearAllTarget(); }));
            }
        }
    }
}
