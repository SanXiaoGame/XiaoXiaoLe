using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gungnir : MonoBehaviour
{
    GameObject ShootEffect;
    BoxCollider2D cld;
    List<GameObject> enemyList;
    
    bool shootSwitch = false;
    //使用者
    GameObject user;
    //最终伤害
    int totalDamage;
    //找到旗手
    GameObject flagM;

    private void Awake()
    {
        ShootEffect = ResourcesManager.Instance.FindPrefab(EffectPrefabs.Effect_gungnirShoot);
        cld = transform.GetComponent<BoxCollider2D>();
        enemyList = new List<GameObject>();
        user = transform.Find("/" + SQLiteManager.Instance.team[ConstData.Knight].playerData.PrefabsID).gameObject;
        flagM = transform.Find("/1001").gameObject;
    }

    private void OnEnable()
    {
        shootSwitch = false;
        if (cld.enabled == false)
        {
            cld.enabled = true;
        }
        enemyList.Clear();
        totalDamage = 0;
    }
    
    private void Update()
    {
        if (shootSwitch == false)
        {
            vp_Timer.In(1.1f, new vp_Timer.Callback(delegate () { InvokeRepeating("ShootGo", 0f, 0.02f); }));
            vp_Timer.In(1.1f, new vp_Timer.Callback(delegate ()
             {
                 GameObject o1 = ObjectPoolManager.Instance.InstantiateMyGameObject(ShootEffect);
                 o1.transform.position = transform.position;
                 vp_Timer.In(0.7f, new vp_Timer.Callback(delegate () { ObjectPoolManager.Instance.RecycleMyGameObject(o1); }));
             }));
            vp_Timer.In(1.5f, new vp_Timer.Callback(delegate () 
            {
                ObjectPoolManager.Instance.RecycleMyGameObject(gameObject);
                CancelInvoke("ShootGo");
            }));
            shootSwitch = true;
        }
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
                GameObject hit1 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(EffectPrefabs.Effect_hit));
                hit1.transform.position = collision.transform.position;
                //回收击打特效
                vp_Timer.In(1f, new vp_Timer.Callback(delegate () { ObjectPoolManager.Instance.RecycleMyGameObject(hit1); }));
                //敌人略微后退
                collision.GetComponent<EnemyControllers>().InvokeRepeating("Dashed", 0f, 0.02f);
                vp_Timer.In(0.6f, new vp_Timer.Callback
                    (delegate () { collision.GetComponent<EnemyControllers>().CancelInvoke("Dashed"); }));
                //计算伤害
                if (collision.GetComponent<EnemyStates>().god == false)
                {
                    totalDamage = (int)
                        (
                        user.GetComponent<HeroStates>().currentAP * 2f -
                        (user.GetComponent<HeroStates>().currentAP * 2f) *
                        (collision.GetComponent<EnemyStates>().currentRES * 0.01f)
                        );
                    collision.GetComponent<EnemyStates>().currentHP -= totalDamage;
                }
                //清空所有锁定目标
                vp_Timer.In(0.2f, new vp_Timer.Callback(delegate () { flagM.GetComponent<FlagManController>().ClearAllTarget(); }));
            }
        }
    }

    void ShootGo()
    {
        transform.position += Vector3.right * Time.deltaTime * 12f;
    }
}
