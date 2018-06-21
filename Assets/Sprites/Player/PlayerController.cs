using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero
{
    public GameObject enemy;
    public Rigidbody2D myRigidbody;
    public Animator animator;

    public PlayerData playerData;
    public StateData stateData;
    public SkillData skillData;

    public int starHP;
    public int currentHP;
    public int currentAD;
    public int currentAP;
    public int currentDEF;
    public int currentRES;
    public int currentStateID;

    //public int heroID;
}

public class PlayerController : MonoBehaviour
{
    GameObject enemy;
    //EnemyController enemyTarget;
    Rigidbody2D myRigidbody;
    Animator heroAnimator;
    public PlayerData playerData;
  
    ////测试用临时变量
    //int startHP;
    Hero hero;
    StateData stateData;
    SkillData skillData;
    int ProfessionID;
    bool isFindEnemy = false;
   public static bool isAttackEnemy = false;
    bool isWin = false;
    private float distance = 0.9f;         //英雄与敌人开始攻击的临界值

    Transform weaponLeft;
    Transform weaponRight;
    Transform weapon;

    Ray2D findRay;           //探敌射线
    RaycastHit2D findHit;    //探敌射线碰撞信息
    int findMask;          //探敌碰撞层

    Queue<GameObject> enemyQueue;
    void JudgeHeroProfession()
    {
        string heroName = transform.name;
        if (heroName.Contains("Saber"))
        {
            ProfessionID = 1002;
        }
        else if (heroName.Contains("Knight"))
        {
            ProfessionID = 1003;
        }
        else if (heroName.Contains("Caster"))
        {
            ProfessionID = 1004;
        }
        else if (heroName.Contains("Berserker"))
        {
            ProfessionID = 1005;
        }
        else if (heroName.Contains("Hunter"))
        {
            ProfessionID = 1006;
        }
       
        Debug.Log("英雄名:" + heroName+ "ProfessionID:" + ProfessionID);
    }
    private void Awake()
    {
      
        JudgeHeroProfession();

        findRay = new Ray2D();
        findMask = LayerMask.GetMask("EnemyLayer");

        enemyQueue = new Queue<GameObject>();
    }
    private void Start()
    {
       
        hero = SQLiteManager.Instance.team[ProfessionID];

        heroAnimator = this.transform.GetComponent<Animator>();
        //enemy = GameObject.FindGameObjectWithTag("Enemy");
        //hero.enemy = GameObject.FindGameObjectWithTag("Enemy");
        hero.animator = this.transform.GetComponent<Animator>();

        myRigidbody = new Rigidbody2D();
        myRigidbody = transform.GetComponent<Rigidbody2D>();
        hero.myRigidbody = myRigidbody;
        weaponLeft = transform.Find("Bones/Torso/L-arm/L-fist/Weapon");
        weaponRight = transform.Find("Bones/Torso/R-arm/R-fist/Weapon2");
        weapon = weaponLeft.transform.GetChild(0);
        Debug.Log("武器的名字为:" + weapon.name);
        //Debug.Log("该英雄的ID:" + hero.playerData.player_Id);
        //Debug.Log("该英雄de名字:" + hero.playerData.player_Name);
        //Debug.Log("该英雄de当前状态ID:" + hero.playerData.stateID);


        //监听hero状态
        //SkillsManager.Listion += OnStateListion;
        
        InvokeRepeating("HeroGo", 0f, 1f);
        InvokeRepeating("FindEnemyLive", 0f, 0.1f); //每0.1秒查询是否有敌人

    }

    /// <summary>
    /// 探敌方法,检测是否有敌人
    /// </summary>
    void FindEnemyLive()
    {
        findRay.origin = transform.position;
        findRay.direction = Vector2.right;
        //设置枪线的初始位置
        //探敌射线的碰撞检测
        //Debug.Log("有敌人探测方法调用");
        ContactFilter2D contactFilter2D;
        Vector2 myPos = new Vector2(transform.position.x+0.2f,transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(myPos, Vector2.right, 5f,findMask);
        if (hit)
        {
            //Debug.Log("有敌人发现");
            if (hit.transform.tag== "Enemy")
            {
                isFindEnemy = true;
                enemy = hit.transform.gameObject;
                Debug.Log("有敌人发现");
            }
        }
        else
        {
            isFindEnemy = false;
        }
    }

    int aa = 0;

    //private void Update()
    //{
    //    //OnStateListion();
    //    //FindEnemy();
      
    //}

    //void Listison()
    //{
        
    //    StartCoroutine("OnStateListion");
    //    //InvokeRepeating("FindEnemy", 0f, 1f);

    //}

    #region  OnStateListion() 英雄的状态监听及触发
    /// <summary>
    /// 通过设置状态监听,来设置玩家英雄动作
    /// </summary>
    //IEnumerator   OnStateListion()
    private void  OnStateListion()
    {
        //CancelInvoke("FindEnemy");
        switch (hero.stateData.state_Name)
        {
            #region case "CommonAttack":所有英雄的普通攻击
            case "CommonAttack":
                heroAnimator.SetBool("isRun", false);        //所有英雄移动
                myRigidbody.velocity = Vector2.zero;

                if (transform.name.Contains("Saber") || transform.name.Contains("Berserker"))       //剑士,狂战英雄普通攻击
                {
                    heroAnimator.SetBool("SaberOneSkill", false);   //取消剑士一技能
                    //Debug.Log("取消剑士一技能,释放普通攻击");
                    heroAnimator.SetTrigger("Attack"); 
                    SkillsManager.Instance.FireSkill(hero, 0);
                }
                else if (transform.name.Contains("Knight") || transform.name.Contains("Caster"))    //骑士,法师英雄普通攻击
                {
                    heroAnimator.SetTrigger("AttackPoke");
                    SkillsManager.Instance.FireSkill(hero, 0);
                }
                else if (transform.name.Contains("Hunter"))     //猎人英雄普通攻击
                {
                    heroAnimator.SetTrigger("AttackBow");
                    SkillsManager.Instance.FireSkill(hero, 0);
                }
                break;
            #endregion
           
            #region case "SaberOneSkill": 剑士三种技能监听
            case "SaberOneSkill":
                heroAnimator.SetBool("SaberOneSkill", true);
                SkillsManager.Instance.FireSkill(hero, 1);

                break;
            case "SaberTwoSkill":
                heroAnimator.SetBool("SaberOneSkill", false);
                heroAnimator.SetTrigger("SaberTwoSkill");
                SkillsManager.Instance.FireSkill(hero, 2);

                break;
            case "SaberThreeSkill":
                heroAnimator.SetBool("SaberOneSkill", false);
                heroAnimator.SetTrigger("SaberThreeSkill");
                SkillsManager.Instance.FireSkill(hero, 3);

                break;
            #endregion

            #region case "KnightOneSkill": 骑士三种技能监听
            case "KnightOneSkill":
                heroAnimator.SetTrigger("KnightSkill");
                SkillsManager.Instance.FireSkill(hero, 1);
                break;
            case "KnightTwoSkill":
                heroAnimator.SetTrigger("KnightSkill");
                SkillsManager.Instance.FireSkill(hero, 2);
                break;
            case "KnightThreeSkill":
                heroAnimator.SetTrigger("KnightSkill");
                SkillsManager.Instance.FireSkill(hero, 3);
                break;
            #endregion

            #region case "CasterOneSkill": 法师三种技能监听
            case "CasterOneSkill":
                heroAnimator.SetTrigger("CasterSkill");
                SkillsManager.Instance.FireSkill(hero, 1);
                break;
            case "CasterTwoSkill":
                heroAnimator.SetTrigger("CasterSkill");
                SkillsManager.Instance.FireSkill(hero, 2);
                break;
            case "CasterThreeSkill":
                heroAnimator.SetTrigger("CasterSkill");
                SkillsManager.Instance.FireSkill(hero, 3);
                break;
            #endregion

            #region case "BerserkerOneSkill": 狂战三种技能监听
            case "BerserkerOneSkill":
                heroAnimator.SetTrigger("BerserkerOneSkill");
                SkillsManager.Instance.FireSkill(hero, 1);

                break;
            case "BerserkerTwoSkill":
                //狂战英雄二技能攻击,没有动画播放
                SkillsManager.Instance.FireSkill(hero, 2);

                break;
            case "BerserkerThreeSkill":
                heroAnimator.SetTrigger("BerserkerThreeSkill");
                SkillsManager.Instance.FireSkill(hero, 3);

                break;
            #endregion

            #region case "HunterOneSkill": 猎人三种技能监听
            case "HunterOneSkill":
                SkillsManager.Instance.FireSkill(hero, 1);
                heroAnimator.SetTrigger("HunterSkill");
                break;
            case "HunterTwoSkill":
                SkillsManager.Instance.FireSkill(hero, 2);
                heroAnimator.SetTrigger("HunterSkill");
                break;
            case "HunterThreeSkill":
                SkillsManager.Instance.FireSkill(hero, 3);
                heroAnimator.SetTrigger("HunterSkill");

                break;
            #endregion
            //case StateName.Idle:
            case "Idle":
                //heroAnimator.SetTrigger("Idle");    //暂时用不上
                weapon.GetComponent<PolygonCollider2D>().enabled = false;        //武器,剑士的剑关闭触发器
                myRigidbody.velocity = Vector2.zero;
                break;
            case "Await":
                myRigidbody.velocity = Vector2.zero;
                heroAnimator.SetBool("isWait", true);           //非战斗等待

                break;
            case "Move":
                weapon.GetComponent<PolygonCollider2D>().enabled = true;        //武器,剑士的剑激活触发器
                heroAnimator.SetBool("isRun", true);        //所有英雄移动
                myRigidbody.velocity = Vector2.right * ConstData.movingSpeed;
                break;
            case "Diz":
                myRigidbody.velocity = Vector2.zero;
                heroAnimator.SetBool("isDiz",true);         //眩晕
                break;
            case "Win":
                myRigidbody.velocity = Vector2.zero;
                heroAnimator.SetTrigger("Win");             //胜利
                break;
            case "GetHit":                          
                heroAnimator.SetTrigger("GetHit");          //受伤
                break;
            case "Dead":
                heroAnimator.SetTrigger("Dead");            //死亡
                break;
            case "Reset":
                heroAnimator.SetTrigger("Reset");           //复活
                break;
            case "Recover":
                //heroAnimator.SetTrigger("Recover");       //恢复生命值
                //Recover(tempHP);
                break;

            default:
                Debug.Log("出错了,当前状态:" + hero.stateData.state_Name);
                break;
        }
        //yield return 0;

    }

    #endregion

    #region 英雄的各种活动状态
    /// <summary>
    /// 原地等待
    /// </summary>
    public void Idle()
    {
       myRigidbody.velocity = Vector3.zero;
    }

    /// <summary>
    /// 移动
    /// </summary>
    public void Move()
    {
        //hero.myRigidbody.velocity = Vector3.right * ConstData.movingSpeed;
        //果然,rigidbaody执行了两次,也就是两个物体上的rigidbody都使用了一个指针地址;
        //myRigidbody.position += Vector2.right;
        myRigidbody.velocity = Vector2.right * 1f;
        Debug.Log("Move--->");
        SkillsManager.Instance.ChangeHeroState(hero.playerData.player_Id, HeroState.idle.GetHashCode());

        //播放移动音效
        //AudioManager.Instance.PlayEffectBase(ResourcesManager.Instance.Load<AudioClip>(ResourcesEnum.SoundEffect.HellephantDeath));
    }

    /// <summary>
    /// 普通攻击
    /// </summary>
    public void CommonAttack()
    {
        //SkillsManager.Instance.FireSkill(hero, 0);
    }
    /// <summary>
    /// 释放一技能
    /// </summary>
    public void FirstAttack()
    {

        //SkillsManager.Instance.FireSkill(hero, 1);
    }
    /// <summary>
    /// 释放二技能
    /// </summary>
    public void SecondAttack()
    {
        //SkillsManager.Instance.FireSkill(hero, 2);
    }
    /// <summary>
    /// 释放三技能
    /// </summary>
    public void ThirdAttack()
    {
        //SkillsManager.Instance.FireSkill(hero, 3);
    }



    /// <summary>
    /// 英雄受伤
    /// </summary>
    /// <param name="damage"></param>
    public void Gethit(int damage)
    {
        playerData.EXHP -= damage;
        if (playerData.EXHP > 0)
        {
            //播放受伤音效
            //AudioManager.Instance.PlayEffectBase(ResourcesManager.Instance.Load<AudioClip>(ResourcesEnum.SoundEffect.PlayerHurt));
            //播放普通攻击的动画,转到普通攻击状态
            heroAnimator.SetTrigger("Attack");
        }
        else
        {
            playerData.EXHP = 0;
            Dead();
        }


    }

    /// <summary>
    /// 制造伤害
    /// </summary>
    /// <param name="damage"></param>
    public void Sethit(int damage)
    {

        //Debug.Log("攻击类型:" + playerData.player_Class + " 攻击力:" + damage);
        //Debug.Log("敌人当前血量:" + enemyTarget.enemyData.HP);
        //if (enemyTarget.enemyData.HP <= 0)
        //{
        //    enemyTarget.enemyData.HP = 0;
        //    EnemySkillState[enemyTarget.enemyData.enemy_Class] = State.Dead;
        //    //enemyTarget.OnStateListion();
        //    Debug.Log("敌人已死亡");
        //}
        //else
        //{
        //    enemyTarget.enemyData.HP -= damage;
        //    Debug.Log("敌人血量:" + enemyTarget.enemyData.HP);

        //}
    }

    /// <summary>
    /// 英雄死亡
    /// </summary>
    public  void Dead()
    {
        //播放死亡音效
        //AudioManager.Instance.PlayEffectBase(ResourcesManager.Instance.Load<AudioClip>(ResourcesEnum.SoundEffect.PlayerDeath));
        ////播放死亡动画
        //heroAnimator.SetTrigger("Dead");
        //执行死亡之后的操作

        Destroy(this.gameObject, 1f);
    }

    /// <summary>
    /// 英雄恢复生命值
    /// </summary>
    /// <param name="tempHP"></param>
    public  void Recover(int tempHP)
    {
        playerData.EXHP += tempHP;
    }

    /// <summary>
    /// 英雄复活
    /// </summary>
    public  void Reset()
    {
        //playerData.EXHP = startHP;
        //myRigidbody.velocity = Vector3.zero;
        ////播放初始化动画
        //heroAnimator.SetTrigger("Idle");
    }

    #endregion

    bool isMove = true;
    
    void AttackBegin()
    {
        weapon.GetComponent<PolygonCollider2D>().enabled = true;
    }
    void AttackWhile()
    {
    }
    void AttackAfter()
    {
        weapon.GetComponent<PolygonCollider2D>().enabled = false;

    }
    void HeroGo()
    {
        //轮询英雄状态
        OnStateListion();

        if (!isFindEnemy && !isWin)
        {
            //此时没有敌人
            Debug.Log("没有发现敌人,英雄们现在应该是跑动状态");
            SkillsManager.Instance.ChangeHerosRun(ProfessionID);
        }
        else if (isFindEnemy&&enemy!=null)
        {
            //此时有敌人了,则判断是否在其攻击范围内,没有则继续跑动,否则开启普通攻击
            //if (Vector2.Distance(this.transform.position, enemy.transform.position) >= distance)
            if (!isAttackEnemy)
                {
                if (hero.stateData.state_Name != "Move")
                {
                    //英雄为普通攻击状态
                    SkillsManager.Instance.ChangeHerosRun(ProfessionID);
                }
                Debug.Log("已发现敌人,进入查找范围,正在跑动");
            }
            else
            {   //进入英雄的攻击范围
                if (hero.stateData.state_Name != "CommonAttack")
                {
                    if (enemy != null)
                    {
                        //英雄为普通攻击状态
                        SkillsManager.Instance.ChangeHerosCommonAttack(ProfessionID);
                    }
                }
            }
        }
        //StartCoroutine(OnStateListion());
        if (isWin && !isFindEnemy)
        {
            heroAnimator.SetBool("isRun", false);        //英雄移动置为FALSE
            SkillsManager.Instance.ChangeHerosWin(ProfessionID);
        }
    }
 
    //进入攻击范围
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Enemy")
        {
            isAttackEnemy = true;
            weapon.GetComponent<PolygonCollider2D>().enabled = false;
            myRigidbody.velocity = Vector2.zero;
        }
        else if (collision.tag == "WinFlag")
        {
            isWin = true;
            Debug.Log("触碰到胜利开关");
        }
    }
}
