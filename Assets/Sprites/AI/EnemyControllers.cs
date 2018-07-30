using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllers : MonoBehaviour
{
    //存活开关
    internal bool isAlive = true;
    //是否还活着（二级）
    internal bool aliving = true;
    //是否被眩晕
    internal bool isDiz = false;
    //是否被沉默
    internal bool isSilence = false;
    //是否在奔跑
    internal bool isRun = false;
    //是否可以进行战斗位移
    internal bool moveSwitch_Battle = true;
    //是否正在释放技能
    internal bool skillIsOperation = false;
    //攻击间隔计时
    internal int attackRate = 50;
    //最终伤害
    internal int totalDamage;

    //敌人状态机
    Animator myanim;
    //敌人锁定的玩家
    internal GameObject targetPlayer;
    //敌人的触发器
    BoxCollider2D triggerEnemy;

    private void Awake()
    {
        myanim = transform.GetComponent<Animator>();
        triggerEnemy = transform.GetComponent<BoxCollider2D>();
        totalDamage = 0;
    }
    private void OnEnable()
    {
        isAlive = true;
        aliving = true;
        isDiz = false;
        isSilence = false;
        isRun = false;
        moveSwitch_Battle = true;
        skillIsOperation = false;
        targetPlayer = null;
    }

    private void Update()
    {
        //一级判断：存活否
        if (isAlive == true)
        {
            //二级判断：是否有锁定的玩家，如果有，他是否还活着；如果已经死了，就解除锁定
            if (targetPlayer != null)
            {
                if (targetPlayer.tag == "Player")
                {
                    if (targetPlayer.GetComponent<HeroController>().isAlive == false)
                    {
                        //解除该敌人的锁定
                        targetPlayer = null;
                        //允许战斗位移（因为敌人死了，需要进行战斗位移）
                        moveSwitch_Battle = true;
                    }
                }
                else
                {
                    if (targetPlayer.GetComponent<FlagManController>().isAlive == false)
                    {
                        //解除该敌人的锁定
                        targetPlayer = null;
                        //允许战斗位移（因为敌人死了，需要进行战斗位移）
                        moveSwitch_Battle = true;
                    }
                }
            }
            else if (targetPlayer == null && FlagManController.battleSwitch == true && isDiz == false && skillIsOperation == false)
            {
                //允许战斗位移
                moveSwitch_Battle = true;
            }
            //二级判断：是否处于战斗状态中and以及英雄是否被眩晕and是否在释放技能
            if (FlagManController.battleSwitch == true && isDiz == false && skillIsOperation == false)
            {
                if (moveSwitch_Battle == true) //三级判断：是否可以进行战斗位移（敌人离开了攻击范围）
                {
                    if (isRun == false) //四级判断：是否在播放奔跑动画
                    {
                        myanim.SetBool("isRun", true);
                        isRun = true;
                    }
                    transform.position -= Vector3.right * Time.deltaTime * 1f;
                }
                else
                {
                    if (isRun == true)
                    {
                        myanim.SetBool("isRun", false);
                        isRun = false;
                    }
                }
                if (targetPlayer != null) //三级判断：是否锁定了敌人（攻击范围有敌人），如果有就开始进行自动攻击
                {
                    if (attackRate >= 50)
                    {
                        myanim.SetTrigger("Attack");
                        attackRate = 0;
                    }
                    attackRate++;
                }
            }
            else if (FlagManController.battleSwitch == false && skillIsOperation == false) //二级判断：是否处于战斗中and是否处于释放技能中
            {
                if (targetPlayer != null)
                {
                    targetPlayer = null;
                }
                moveSwitch_Battle = false;
                if (isRun == true)
                {
                    myanim.SetBool("isRun", false);
                    isRun = false;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //判断：侦测到的是否是玩家and我当前没有锁定任何玩家
        if (collision.tag == "Player" && targetPlayer == null)
        {
            //锁定敌人
            targetPlayer = collision.gameObject;
            //不允许战斗位移（因为敌人在攻击范围内，不需要进行战斗位移）
            moveSwitch_Battle = false;
        }
        //判断：碰到的是不是旗手and我当前没有锁定任何玩家
        if (collision.tag == "FlagMan" && targetPlayer == null)
        {
            //锁定敌人
            targetPlayer = collision.gameObject;
            //不允许战斗位移（因为敌人在攻击范围内，不需要进行战斗位移）
            moveSwitch_Battle = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //判断：侦测到离开的是否是我当前锁定的敌人
            if (collision == targetPlayer)
            {
                //解除该敌人的锁定
                targetPlayer = null;
                //允许战斗位移（因为敌人离开了攻击范围，需要进行战斗位移）
                moveSwitch_Battle = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Plane")
        {
            transform.GetComponent<Rigidbody2D>().gravityScale = 0;
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        }
    }

    /// <summary>
    /// 普通攻击的调用
    /// </summary>
    internal void EnemyAttack()
    {
        if (isDiz == false && isAlive == true && skillIsOperation == false && FlagManController.battleSwitch == true)
        {
            if (targetPlayer != null)
            {
                AudioManager.Instance.PlayEffectMusic(SoundEffect.Hit);
                //生成击打特效
                GameObject hit1 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_hit));
                hit1.transform.position = targetPlayer.transform.position;
                //回收击打特效
                vp_Timer.In(1f, new vp_Timer.Callback(delegate () { ObjectPoolManager.Instance.RecycleMyGameObject(hit1); }));
                //计算伤害
                if (targetPlayer.tag == ConstData.Player)
                {
                    if (targetPlayer.GetComponent<HeroStates>().god == false)
                    {
                        totalDamage = (int)
                            (
                            (
                            transform.GetComponent<EnemyStates>().currentAD * 1f -
                            (transform.GetComponent<EnemyStates>().currentAD * 1f) *
                            (targetPlayer.GetComponent<HeroStates>().currentDEF * 0.01f)
                            ) * 0.1f
                            );
                        targetPlayer.GetComponent<HeroStates>().currentHP -= totalDamage;
                    }
                }
                else
                {
                    totalDamage = (int)
                            (
                            (
                            transform.GetComponent<EnemyStates>().currentAD * 1f -
                            (transform.GetComponent<EnemyStates>().currentAD * 1f) *
                            (targetPlayer.GetComponent<FlagManController>().currentDEF * 0.01f)
                            ) * 0.1f
                            );
                    targetPlayer.GetComponent<FlagManController>().currentHP -= totalDamage;
                }
            }
        }
    }

    /// <summary>
    /// 注销正在重复调用的方法
    /// </summary>
    /// <param 方法名="invokeName"></param>
    internal void cancel(string invokeName)
    {
        CancelInvoke(invokeName);
    }

    /////////////////////////////////////////////////////////
    ////////        技能部分：所有敌人的技能         ////////
    /////////////////////////////////////////////////////////

    /// <summary>
    /// 敌人冲刺技能
    /// </summary>
    internal void Enemy_Sprint()
    {
        if (isDiz == false && isSilence == false && isAlive == true && skillIsOperation == false && FlagManController.battleSwitch == true)
        {
            skillIsOperation = true;
            if (isRun == true)
            {
                myanim.SetBool("isRun", false);
                isRun = false;
            }
            transform.position += Vector3.right * 0.5f;
            if (transform.GetComponent<SprintDash>() == null)
            {
                gameObject.AddComponent<SprintDash>();
            }
            else
            {
                if (transform.GetComponent<SprintDash>().enabled == false)
                {
                    transform.GetComponent<SprintDash>().enabled = true;
                }
                transform.GetComponent<SprintDash>().isOver = false;
                transform.GetComponent<SprintDash>().targetList.Clear();
            }
            InvokeRepeating("EnemyDash", 0f, 0.02f);
        }
    }
    void EnemyDash()
    {
        transform.position -= Vector3.right * Time.deltaTime * 3f;
    }
    internal void Dashed()
    {
        transform.position += Vector3.right * Time.deltaTime * 3f;
    }
}
