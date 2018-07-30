using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SprintDash : MonoBehaviour
{
    //触发器
    BoxCollider2D enemyClD;
    //使用者
    GameObject user;
    //使用者类型
    string userTag;
    //击中的目标
    GameObject hitTarget;
    //最终伤害
    int totalDamage;
    //控制开关
    internal bool isOver = false;
    //已被击中的对象列表
    internal List<GameObject> targetList;
    //找到旗手
    GameObject flagM;

    private void Awake()
    {
        targetList = new List<GameObject>();
        flagM = transform.Find("/1001").gameObject;
    }

    private void OnEnable()
    {
        if (gameObject.tag == "Enemy")
        {
            user = gameObject;
            userTag = "Enemy";
            enemyClD = transform.GetComponent<BoxCollider2D>();
            targetList.Clear();
        }
        else
        {
            user = transform.Find("/" + (SQLiteManager.Instance.team[ConstData.Saber].playerData.PrefabsID).ToString()).gameObject;
            userTag = "Player";
            targetList.Clear();
        }
    }

    private void Update()
    {
        if (userTag == "Player")
        {
            transform.position += Vector3.right * Time.deltaTime * 3f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (userTag)
        {
            case "Player":
                if (collision.tag == "Enemy")
                {
                    if (collision.GetComponent<EnemyControllers>().isAlive == true && targetList.Contains(collision.gameObject) == false)
                    {
                        AudioManager.Instance.PlayEffectMusic(SoundEffect.Hit);
                        targetList.Add(collision.gameObject);
                        //保存击中目标物体
                        hitTarget = collision.gameObject;
                        //取消冲刺位移
                        user.GetComponent<HeroController>().cancel("Skill_A_Saber_Sprint");
                        //关闭技能释放开关
                        vp_Timer.In(0.4f, new vp_Timer.Callback(delegate ()
                        {
                            if (SceneManager.GetActiveScene().name != "LoadingScene")
                            {
                                user.GetComponent<HeroController>().skillIsOperation = false;
                            }
                        }));
                        //关闭英雄技能动画
                        user.GetComponent<Animator>().SetBool("SaberOneSkill", false);
                        //敌人被击中后退的重复调用
                        collision.GetComponent<EnemyControllers>().InvokeRepeating("Dashed", 0f, 0.02f);
                        //取消重复调用
                        vp_Timer.In(0.6f, new vp_Timer.Callback(delegate ()
                        {
                            if (SceneManager.GetActiveScene().name != "LoadingScene")
                            {
                                collision.GetComponent<EnemyControllers>().CancelInvoke("Dashed");
                            }
                        }));
                        vp_Timer.In(0.2f, new vp_Timer.Callback(delegate ()
                        {
                            if (SceneManager.GetActiveScene().name != "LoadingScene")
                            {
                                transform.Find("/1001").GetComponent<FlagManController>().ClearAllTarget();
                            }
                        }));

                        //生成击打特效
                        GameObject hit1 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_hit));
                        hit1.transform.position = hitTarget.transform.position;
                        //回收击打特效
                        vp_Timer.In(1f, new vp_Timer.Callback(delegate () { ObjectPoolManager.Instance.RecycleMyGameObject(hit1); }));
                        //回收特效
                        ObjectPoolManager.Instance.RecycleMyGameObject(gameObject);
                        //敌人获得眩晕状态
                        hitTarget.GetComponent<EnemyStates>().GetState(3201, 2.0f);
                        if (isOver == false)
                        {
                            //计算伤害
                            if (hitTarget.GetComponent<EnemyStates>().god == false)
                            {
                                totalDamage = (int)(user.GetComponent<HeroStates>().currentAD * 1f -
                                        (user.GetComponent<HeroStates>().currentAD * 1f) * (hitTarget.GetComponent<EnemyStates>().currentDEF * 0.01f));
                                hitTarget.GetComponent<EnemyStates>().currentHP -= totalDamage;
                            }
                            isOver = true;
                        }
                        
                    }
                }
                break;
            case "Enemy":
                if (collision.tag == "Player" || collision.tag == "FlagMan")
                {
                    if (targetList.Contains(collision.gameObject) == false)
                    {
                        AudioManager.Instance.PlayEffectMusic(SoundEffect.Hit);
                        targetList.Add(collision.gameObject);
                        //取消冲刺位移
                        transform.GetComponent<EnemyControllers>().CancelInvoke("EnemyDash");
                        //保存击中目标
                        hitTarget = collision.gameObject;
                        //关闭技能释放开关
                        transform.GetComponent<EnemyControllers>().skillIsOperation = false;
                        if (collision.tag != "FlagMan")
                        {
                            //玩家被击中后退的重复调用
                            collision.GetComponent<HeroController>().InvokeRepeating("Dashed", 0f, 0.02f);
                            //取消重复调用
                            vp_Timer.In(0.6f, new vp_Timer.Callback
                                (delegate () { collision.GetComponent<HeroController>().CancelInvoke("Dashed"); }));
                        }
                        //锁定玩家丢失（因为玩家已经被击飞到攻击范围外）
                        //vp_Timer.In(0.2f, new vp_Timer.Callback(delegate () { transform.GetComponent<EnemyControllers>().targetPlayer = null; }));
                        //玩家锁定同样丢失
                        //if (hitTarget.tag != "FlagMan")
                        //{
                        //    vp_Timer.In(0.2f, new vp_Timer.Callback(delegate () { hitTarget.GetComponent<HeroController>().targetEnemy = null; }));
                        //}

                        //敌人战斗位移开启
                        //transform.GetComponent<EnemyControllers>().moveSwitch_Battle = true;
                        //玩家战斗位移开启
                        //if (hitTarget.tag != "FlagMan")
                        //{
                        //    hitTarget.GetComponent<HeroController>().moveSwitch_Battle = true;
                        //}
                        vp_Timer.In(0.3f, new vp_Timer.Callback(delegate () { flagM.GetComponent<FlagManController>().ClearAllTarget(); }));

                        //生成击打特效
                        GameObject hit1 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_hit));
                        hit1.transform.position = hitTarget.transform.position;
                        //回收击打特效
                        vp_Timer.In(1f, new vp_Timer.Callback(delegate () { ObjectPoolManager.Instance.RecycleMyGameObject(hit1); }));
                        if (collision.tag == "Player")
                        {
                            //玩家获得眩晕状态（剑士和旗手除外）
                            if (hitTarget.GetComponent<HeroController>().myClass != ConstData.Saber)
                            {
                                hitTarget.GetComponent<HeroStates>().GetState(3201, 2.0f);
                            }
                            if (isOver == false)
                            {
                                //计算伤害
                                if (hitTarget.GetComponent<HeroStates>().god == false)
                                {
                                    totalDamage = (int)(user.GetComponent<EnemyStates>().currentAD * 1f -
                                            (user.GetComponent<EnemyStates>().currentAD * 1f) *
                                            (hitTarget.GetComponent<HeroStates>().currentDEF * 0.01f));
                                    hitTarget.GetComponent<HeroStates>().currentHP -= totalDamage;
                                }
                                isOver = true;
                            }
                        }
                        else
                        {
                            if (isOver == false)
                            {
                                //计算伤害
                                totalDamage = (int)(user.GetComponent<EnemyStates>().currentAD * 1f -
                                            (user.GetComponent<EnemyStates>().currentAD * 1f) * (hitTarget.GetComponent<FlagManController>().currentDEF * 0.01f));
                                hitTarget.GetComponent<FlagManController>().currentHP -= totalDamage;
                                isOver = true;
                            }
                        }
                    }
                }
                break;
        }
    }
}
