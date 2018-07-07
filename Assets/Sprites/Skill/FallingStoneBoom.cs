using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStoneBoom : MonoBehaviour
{
    //触发器
    CircleCollider2D ballCollider;
    //爆炸特效
    GameObject ballBoom;
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
        ballCollider = transform.GetComponent<CircleCollider2D>();
        user = transform.Find("/" + SQLiteManager.Instance.team[ConstData.Caster].playerData.PrefabsID).gameObject;
        flagM = transform.Find("/1001").gameObject;
        enemyList = new List<GameObject>();
        ballBoom = ResourcesManager.Instance.FindPrefab(EffectPrefabs.Effect_fallingStoneBoom);
    }

    private void OnEnable()
    {
        totalDamage = 0;
        if (ballCollider.enabled == false)
        {
            ballCollider.enabled = true;
        }
        enemyList.Clear();
    }

    private void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * 2.5f;
        transform.position -= Vector3.up * Time.deltaTime * 2.5f;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Plane")
        {
            AudioManager.Instance.PlayEffectMusic(SoundEffect.FallingStone_Hit);
            GameObject o1 = ObjectPoolManager.Instance.InstantiateMyGameObject(ballBoom);
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
                    totalDamage = (int)
                        (
                        (
                        user.GetComponent<HeroStates>().currentAP * 1.5f -
                        (user.GetComponent<HeroStates>().currentAP * 1.5f) *
                        (collision.GetComponent<EnemyStates>().currentRES * 0.01f)
                        ) * 0.5f
                        );
                    collision.GetComponent<EnemyStates>().currentHP -= totalDamage;
                    //敌人虚弱
                    collision.transform.GetComponent<EnemyStates>().GetState(3212, 3.0f);
                }
                //清空所有锁定目标
                vp_Timer.In(0.3f, new vp_Timer.Callback(delegate () { flagM.GetComponent<FlagManController>().ClearAllTarget(); }));
            }
        }
    }
}
