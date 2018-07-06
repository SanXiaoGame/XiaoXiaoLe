using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    GameObject enemy;
    Rigidbody2D myRigidbody;
    Animator heroAnimator;
    public PlayerData playerData;
  
    HeroData hero;
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
    Ray2D attRay;           //探敌是否进入攻击圈射线
    RaycastHit2D findHit;    //探敌射线碰撞信息
    RaycastHit2D attHit;    //探敌射线碰撞信息
    int findMask;          //探敌碰撞层

    Queue<GameObject> enemyQueue;
    void JudgeHeroProfession()
    {
        string heroName = transform.name;
        if (heroName.Contains("Saber"))
        {
            ProfessionID = 1301;
        }
        else if (heroName.Contains("Knight"))
        {
            ProfessionID = 1302;
        }
        else if (heroName.Contains("Caster"))
        {
            ProfessionID = 1304;
        }
        else if (heroName.Contains("Berserker"))
        {
            ProfessionID = 1303;
        }
        else if (heroName.Contains("Hunter"))
        {
            ProfessionID = 1305;
        }
       
        Debug.Log("英雄名:" + heroName+ "ProfessionID:" + ProfessionID);
    }
    private void Awake()
    {
      
        JudgeHeroProfession();

        findRay = new Ray2D();
        findMask = LayerMask.GetMask("EnemyLayer");
        attRay = new Ray2D();

        enemyQueue = new Queue<GameObject>();
    }

    private void Start()
    {
        StartCoroutine("Wait");
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.3f);
        hero = SQLiteManager.Instance.team[ProfessionID];

        hero.transform = this.transform;
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

        //监听hero状态
        SkillsManager.Listion += OnStateListion;

        //InvokeRepeating("HeroGo", 0f, 1f);
        InvokeRepeating("FindEnemyLive", 0f, 0.1f); //每0.1秒查询是否有敌人
        //isFindEnemy = true;
        InvokeRepeating("AttackRange", 0f, 0.1f); //每0.1秒查询在英雄的攻击范围内是否有敌人
    }

  

    #region  OnStateListion() 英雄的状态监听及触发
    /// <summary>
    /// 通过设置状态监听,来设置玩家英雄动作
    /// </summary>
    //IEnumerator   OnStateListion()
    private void OnStateListion()
    {
        //Debug.Log("此时的状态为:" + hero.ActionName);
        switch (hero.ActionName)
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
                myRigidbody.velocity = Vector2.zero;
                heroAnimator.SetBool("isRun", false);        //所有英雄移动
                heroAnimator.SetBool("SaberOneSkill", true);
                Debug.Log("此时的状态是SaberOneSkill");
                SkillsManager.Instance.FireSkill(hero, 1);

                break;
            case "SaberTwoSkill":
                myRigidbody.velocity = Vector2.zero;
                heroAnimator.SetBool("isRun", false);        //所有英雄移动
                heroAnimator.SetBool("SaberOneSkill", false);
                heroAnimator.SetTrigger("SaberTwoSkill");
                SkillsManager.Instance.FireSkill(hero, 2);

                break;
            case "SaberThreeSkill":
                myRigidbody.velocity = Vector2.zero;
                heroAnimator.SetBool("isRun", false);        //所有英雄移动
                heroAnimator.SetBool("SaberOneSkill", false);
                heroAnimator.SetTrigger("SaberThreeSkill");
                SkillsManager.Instance.FireSkill(hero, 3);

                break;
            #endregion

            #region case "KnightOneSkill": 骑士三种技能监听
            case "KnightOneSkill":
                myRigidbody.velocity = Vector2.zero;
                heroAnimator.SetBool("isRun", false);        //所有英雄移动
                heroAnimator.SetTrigger("KnightSkill");
                SkillsManager.Instance.FireSkill(hero, 1);
                break;
            case "KnightTwoSkill":
                myRigidbody.velocity = Vector2.zero;
                heroAnimator.SetBool("isRun", false);        //所有英雄移动
                heroAnimator.SetTrigger("KnightSkill");
                SkillsManager.Instance.FireSkill(hero, 2);
                break;
            case "KnightThreeSkill":
                myRigidbody.velocity = Vector2.zero;
                heroAnimator.SetBool("isRun", false);        //所有英雄移动
                heroAnimator.SetTrigger("KnightSkill");
                SkillsManager.Instance.FireSkill(hero, 3);
                break;
            #endregion

            #region case "CasterOneSkill": 法师三种技能监听
            case "CasterOneSkill":
                myRigidbody.velocity = Vector2.zero;
                heroAnimator.SetBool("isRun", false);        //所有英雄移动
                heroAnimator.SetTrigger("CasterSkill");
                SkillsManager.Instance.FireSkill(hero, 1);
                break;
            case "CasterTwoSkill":
                myRigidbody.velocity = Vector2.zero;
                heroAnimator.SetBool("isRun", false);        //所有英雄移动
                heroAnimator.SetTrigger("CasterSkill");
                SkillsManager.Instance.FireSkill(hero, 2);
                break;
            case "CasterThreeSkill":
                myRigidbody.velocity = Vector2.zero;
                heroAnimator.SetBool("isRun", false);        //所有英雄移动
                heroAnimator.SetTrigger("CasterSkill");
                SkillsManager.Instance.FireSkill(hero, 3);
                break;
            #endregion

            #region case "BerserkerOneSkill": 狂战三种技能监听
            case "BerserkerOneSkill":
                myRigidbody.velocity = Vector2.zero;
                heroAnimator.SetBool("isRun", false);        //所有英雄移动
                heroAnimator.SetTrigger("BerserkerOneSkill");
                SkillsManager.Instance.FireSkill(hero, 1);

                break;
            case "BerserkerTwoSkill":
                //狂战英雄二技能攻击,没有动画播放    
                myRigidbody.velocity = Vector2.zero;
                heroAnimator.SetBool("isRun", false);        //所有英雄移动
                BerserkerTwoSkillEffectStart();
                SkillsManager.Instance.FireSkill(hero, 2);

                break;
            case "BerserkerThreeSkill":
                myRigidbody.velocity = Vector2.zero;
                heroAnimator.SetBool("isRun", false);        //所有英雄移动
                heroAnimator.SetTrigger("BerserkerThreeSkill");
                SkillsManager.Instance.FireSkill(hero, 3);

                break;
            #endregion

            #region case "HunterOneSkill": 猎人三种技能监听
            case "HunterOneSkill":
                myRigidbody.velocity = Vector2.zero;
                heroAnimator.SetBool("isRun", false);        //所有英雄移动
                heroAnimator.SetTrigger("HunterSkill");
                SkillsManager.Instance.FireSkill(hero, 1);
                break;
            case "HunterTwoSkill":
                myRigidbody.velocity = Vector2.zero;
                heroAnimator.SetBool("isRun", false);        //所有英雄移动
                heroAnimator.SetTrigger("HunterSkill");
                SkillsManager.Instance.FireSkill(hero, 2);
                break;
            case "HunterThreeSkill":
                myRigidbody.velocity = Vector2.zero;
                heroAnimator.SetBool("isRun", false);        //所有英雄移动
                heroAnimator.SetTrigger("HunterSkill");
                SkillsManager.Instance.FireSkill(hero, 3);
                break;
            #endregion
            //case StateName.Idle:
            case "Idle":
                //heroAnimator.SetTrigger("Idle");    //暂时用不上
                //weapon.GetComponent<PolygonCollider2D>().enabled = false;        //武器,剑士的剑关闭触发器
                myRigidbody.velocity = Vector2.zero;
                break;
            case "Await":
                myRigidbody.velocity = Vector2.zero;
                heroAnimator.SetBool("isWait", true);           //非战斗等待

                break;
            case "Move":
                //weapon.GetComponent<PolygonCollider2D>().enabled = true;        //武器,剑士的剑激活触发器
                heroAnimator.SetBool("isRun", true);        //所有英雄移动
                myRigidbody.velocity = Vector2.right * ConstData.movingSpeed * this.transform.localScale.x;
                break;
            case "Diz":
                myRigidbody.velocity = Vector2.zero;
                heroAnimator.SetBool("isDiz", true);         //眩晕
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

    #region void FindEnemyLive()探敌方法,检测是否有敌人
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
        //ContactFilter2D contactFilter2D;
        Vector2 myPos = new Vector2(transform.position.x + 0.2f, transform.position.y + 0.15f);
        RaycastHit2D hit = Physics2D.Raycast(myPos, Vector2.right, 20f, findMask);
        Debug.DrawRay(myPos, findRay.direction, Color.green);

        if (hit)
        {
            //Debug.Log("有敌人发现");
            if (hit.transform.tag == "Enemy")
            {
                isFindEnemy = true;
                enemy = hit.transform.gameObject;
                //Debug.Log("有敌人发现");
                //Debug.DrawRay(myPos, findRay.direction, Color.green);
            }
            else
            {
                isFindEnemy = false;
            }
        }
        else
        {
            isFindEnemy = false;
        }
    }
    #endregion

    #region void AttackRange()英雄的攻击范围内是否有怪并执行相应的动作
    int tempCount;  //普通攻击即时
    /// <summary>
    /// 英雄的攻击范围内是否有怪并执行相应的动作
    /// </summary>
    void AttackRange()
    {
        tempCount++;
        attRay.origin = transform.position;
        attRay.direction = Vector2.right;
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D atthit = Physics2D.Raycast(myPos, Vector2.right, 1f * transform.lossyScale.x, findMask);     //长度应该是武器原本长度乘上缩放比例,实际要小一些
        if (atthit)
        {
            if (atthit.transform.tag == "Enemy")     //攻击范围内有敌人
            {
                isAttackEnemy = true;
                Debug.DrawRay(myPos, attRay.direction, Color.red, 1f * transform.lossyScale.x);
                //Debug.Log("当前状态:" + hero.ActionName);
                if (hero.ActionName == "Move" || hero.ActionName == "Idle"&& tempCount>=10)
                {
                    tempCount = 0;
                    //攻击范围内发现有敌人,如果此时英雄处于移动或者发呆状态应立即转为普通攻击状态,其他状态(如技能)不切换
                    SkillsManager.Instance.ChangeHerosCommonAttack(ProfessionID);
                    OnStateListion();       //切换完成立即轮询执行
                    //Debug.Log("有敌人发现,已经进入攻击范围,英雄普通攻击");
                }
            }
        }
        else
        {
            //攻击范围内没有敌人,如果检测到还有敌人的话,则跑动
            if (isFindEnemy)
            {
                //Debug.Log("有敌人发现,还没进入攻击范围,英雄跑动");
                if (hero.ActionName == "Idle" || hero.ActionName == "CommonAttack")
                {
                    SkillsManager.Instance.ChangeHerosRun(ProfessionID);
                    //Debug.Log("执行跑动切换动作");
                    OnStateListion();       //切换完成立即轮询执行
                }
            }
            else
            {
                //所有敌人都消灭了
                if (hero.ActionName != "Win")
                {
                    SkillsManager.Instance.ChangeHerosWin(ProfessionID);
                    OnStateListion();       //切换完成立即轮询执行
                }
                //Debug.Log("没有敌人发现,英雄胜利");
            }
        }
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
        SkillsManager.Instance.ChangeHeroState(hero.playerData.player_Id, ActionEnum.Idle.ToString());

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

    #region 动画触发事件
    #region 普通攻击特效
    void AttackBegin()
    {
        weapon.GetComponent<PolygonCollider2D>().enabled = true;
    }

    void AttackAfter()
    {
        weapon.GetComponent<PolygonCollider2D>().enabled = false;
    }
    void AttackFinished()
    {
        OnStateListion();       //攻击完成切换下一轮询执行
    }
    //法师的远程普通攻击或骑士的物理攻击
    IEnumerator RemoteAttack()
    {
        if (transform.name.Contains("Knight"))
        {
            GameObject weapon = hero.transform.Find("Bones/Torso/L-arm/L-fist/Weapon").gameObject;
            GameObject weapon1 = weapon.transform.GetChild(0).gameObject;
            PolygonCollider2D collider2D = weapon1.GetComponent<PolygonCollider2D>();
            collider2D.enabled = true;
            yield return new WaitForSeconds(0.5f);
            collider2D.enabled = false;
        }
        else
        {
            GameObject weaponEffect = ResourcesManager.Instance.FindPrefab(EffectPrefabs.Effect_magicAttack);
            GameObject weapon = hero.transform.Find("Bones/Torso/L-arm/L-fist/Weapon").gameObject;
            Vector3 pos = new Vector3(weapon.transform.position.x + 0.5f, weapon.transform.position.y);
            weaponEffect.transform.position = pos;
            WeaponEffect = Instantiate(weaponEffect) as GameObject;
            Rigidbody2D tempRigibody2D = WeaponEffect.GetComponent<Rigidbody2D>();
            tempRigibody2D.velocity = Vector3.right * 5f;
        }

    }
    #endregion

    /// <summary>
    /// 技能完成后调用,回归到Idle状态
    /// </summary>
    void SkillFinishedChange()
    {
        SkillsManager.Instance.ChangeHeroState(hero.playerData.player_Id, ActionEnum.Idle.ToString());
    }
    #region 剑士动画事件
    /// <summary>
    /// 剑士一技能突刺的特效预制体加载
    /// </summary>
    void SaberOneSkillEffectStart()
    {
        //生成技能特效
        GameObject Skill_Saber01_Sprint = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Saber01_Sprint);
        GameObject weapon = hero.transform.Find("Bones/Torso/L-arm/L-fist/Weapon").gameObject;
        WeaponEffect = Instantiate(Skill_Saber01_Sprint, weapon.transform) as GameObject;


        //GameObject skillEffect = hero.transform.Find("Bones/Torso/L-arm/L-fist/Weapon/Skill_Saber01_Sprint").gameObject;
        //skillEffect.SetActive(true);
        InvokeRepeating("SaberWeaponRay", 0f, 0.05f);
    }
    void SaberTwoSkillEffectStart()
    {
        //生成技能特效
        GameObject Skill_Saber02_ExplodingSword = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Saber02_ExplodingSword);
        GameObject weapon = hero.transform.Find("Bones/Torso/L-arm/L-fist/Weapon").gameObject;
        Vector3 pos = new Vector3(hero.transform.position.x + 1f, hero.transform.position.y - 0.2f);
        Skill_Saber02_ExplodingSword.transform.position = pos;
        WeaponEffect = Instantiate(Skill_Saber02_ExplodingSword) as GameObject;
        Rigidbody2D tempRigibody2D = WeaponEffect.AddComponent<Rigidbody2D>();
        tempRigibody2D.gravityScale = 0;
        tempRigibody2D.velocity = Vector3.right * 5f;
        //weapon.GetComponent<PolygonCollider2D>().enabled = true;
        //InvokeRepeating("SaberWeaponRay", 0f, 0.05f);

    }
    void SaberThreeSkill_1()
    {
        //生成技能特效
        GameObject Skill_Saber03_SixSonicSlash01 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Saber03_SixSonicSlash01);
        //Skill_Saber03_SixSonicSlash01.transform.position = Vector3.zero;
        //Vector3 pos = new Vector3(hero.transform.position.x, hero.transform.position.y + 1.112f);
        Vector3 pos = new Vector3(0,1.112f,0);
        Skill_Saber03_SixSonicSlash01.transform.position = pos+this.transform.position;
        WeaponEffect = Instantiate(Skill_Saber03_SixSonicSlash01) as GameObject;
        Destroy(WeaponEffect, 0.5f);
    }
    void SaberThreeSkill_2()
    {
        //生成技能特效
        GameObject Skill_Saber03_SixSonicSlash02 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Saber03_SixSonicSlash02);
        //Skill_Saber03_SixSonicSlash02.transform.position = Vector3.zero;
        //Vector3 pos = new Vector3(hero.transform.position.x + 0.74f, hero.transform.position.y - 0.139f);
        Vector3 pos = new Vector3(2.31f, -0.1f, 0);
        Skill_Saber03_SixSonicSlash02.transform.position = pos + this.transform.position;
        WeaponEffect = Instantiate(Skill_Saber03_SixSonicSlash02) as GameObject;

    }
    void SaberThreeSkill_3()
    {
        //生成技能特效
        GameObject Skill_Saber03_SixSonicSlash03 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Saber03_SixSonicSlash03);
        //Skill_Saber03_SixSonicSlash03.transform.position = Vector3.zero;
        //Vector3 pos = new Vector3(hero.transform.position.x - 0.566f, hero.transform.position.y - 0.068f);
        Vector3 pos = new Vector3(-0.566f, -0.068f, 0);
        Skill_Saber03_SixSonicSlash03.transform.position = pos + this.transform.position;
        WeaponEffect = Instantiate(Skill_Saber03_SixSonicSlash03) as GameObject;
        Destroy(WeaponEffect, 0.5f);

    }
    void SaberThreeSkill_4()
    {
        //生成技能特效
        GameObject Skill_Saber03_SixSonicSlash04 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Saber03_SixSonicSlash04);
        //Skill_Saber03_SixSonicSlash04.transform.position = Vector3.zero;
        //Vector3 pos = new Vector3(hero.transform.position.x + 2.084f, hero.transform.position.y - 0.139f);
        Vector3 pos = new Vector3(2.084f, -0.139f, 0);
        Skill_Saber03_SixSonicSlash04.transform.position = pos + this.transform.position;
        WeaponEffect = Instantiate(Skill_Saber03_SixSonicSlash04) as GameObject;

    }
    void SaberThreeSkill_5()
    {
        //生成技能特效
        GameObject Skill_Saber03_SixSonicSlash05 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Saber03_SixSonicSlash05);
        //Skill_Saber03_SixSonicSlash05.transform.position = Vector3.zero;
        //Vector3 pos = new Vector3(hero.transform.position.x + 0.989f, hero.transform.position.y + 0.377f);
        Vector3 pos = new Vector3(0.989f, 0.377f, 0);
        Skill_Saber03_SixSonicSlash05.transform.position = pos + this.transform.position;
        WeaponEffect = Instantiate(Skill_Saber03_SixSonicSlash05) as GameObject;

    }
    #endregion

    #region 战士动画事件

    void BerserkerOneSkillEffectStart()
    {
        GameObject Skill_Berserker01_LeapAttack = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Berserker01_LeapAttack);
        GameObject weapon = hero.transform.Find("Bones/Torso/L-arm/L-fist/Weapon").gameObject;
        Vector3 pos = new Vector3(hero.transform.position.x + 1f, hero.transform.position.y - 0.2f);
        Skill_Berserker01_LeapAttack.transform.position = pos;
        GameObject WeaponEffect = Instantiate(Skill_Berserker01_LeapAttack) as GameObject;

        Destroy(WeaponEffect, 0.5f);
    }

    void BerserkerTwoSkillEffectStart()
    {
        GameObject Skill_Berserker02_Blood_01 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Berserker02_Blood_01);
        Vector3 pos = new Vector3( 0,1f);
        Skill_Berserker02_Blood_01.transform.position = pos;
        GameObject  WeaponEffect = Instantiate(Skill_Berserker02_Blood_01,this.transform) as GameObject;
        SkillFinishedChange();

        Destroy(WeaponEffect, 10f);

    }
    IEnumerator BerserkerThreeSkillEffectStart()
    {
        GameObject Skill_Berserker03_Fissure01 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Berserker03_Fissure01);
        GameObject Skill_Berserker03_Fissure02 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Berserker03_Fissure02);
        GameObject Skill_Berserker03_Fissure03 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Berserker03_Fissure03);
        GameObject weapon = hero.transform.Find("Bones/Torso/L-arm/L-fist/Weapon").gameObject;
        Vector3 pos1, pos2, pos3;
        if (enemy!=null)
        {
             pos1 = new Vector3(enemy.transform.position.x + 0f, enemy.transform.position.y);
             pos2 = new Vector3(enemy.transform.position.x + 1f, enemy.transform.position.y);
             pos3 = new Vector3(enemy.transform.position.x + 2f, enemy.transform.position.y);
        }
        else
        {
             pos1 = new Vector3(weapon.transform.position.x + 4f, enemy.transform.position.y);
             pos2 = new Vector3(weapon.transform.position.x + 5f, enemy.transform.position.y);
             pos3 = new Vector3(weapon.transform.position.x + 6f, enemy.transform.position.y);
        }


        GameObject WeaponEffect1 = Instantiate(Skill_Berserker03_Fissure01,pos1,Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(0.5f);
        GameObject WeaponEffect3 = Instantiate(Skill_Berserker03_Fissure03, pos2, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(0.5f);
        GameObject WeaponEffect2 = Instantiate(Skill_Berserker03_Fissure02, pos3, Quaternion.identity) as GameObject;
        //估计三秒后动画播放玩,切换状态
        //vp_Timer.In(3f, new vp_Timer.Callback(delegate () { SkillFinishedChange(); }));
        yield return new WaitForSeconds(4f);
        SkillFinishedChange();
    }

    #endregion

    #region 猎人动画事件 
    void   HunterAttackEffectStart()
    {
        //生成技能特效
        GameObject Effect_arrow = ResourcesManager.Instance.FindPrefab(EffectPrefabs.Effect_arrow);
        GameObject weapon = hero.transform.Find("Bones/Torso/R-arm/R-fist/Weapon2").gameObject;
        Vector3 pos = new Vector3(weapon.transform.position.x , weapon.transform.position.y + 0.2f);
        Effect_arrow.transform.position = pos;
        WeaponEffect = Instantiate(Effect_arrow) as GameObject;
        Rigidbody2D tempRigibody2D = WeaponEffect.GetComponent<Rigidbody2D>();
        tempRigibody2D.velocity = Vector3.right * 5f;
    }
    IEnumerator HunterOneSkillEffectStart()
    {
        Debug.Log("猎人的技能攻击开始事件");
        GameObject weaponEffect;
        GameObject weapon = hero.transform.Find("Bones/Torso/R-arm/R-fist/Weapon2").gameObject;
        switch (hero.ActionName)
        {
            case "HunterOneSkill":
                weaponEffect = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Hunter01_ExplosionArrow);
                Vector3 pos = new Vector3(weapon.transform.position.x, weapon.transform.position.y - 0.2f);
                weaponEffect.transform.position = pos;
                GameObject WeaponEffect = Instantiate(weaponEffect) as GameObject;
                Rigidbody2D tempRigibody2D = WeaponEffect.GetComponent<Rigidbody2D>();
                tempRigibody2D.velocity = Vector3.right * 5f;
                yield return new WaitForSeconds(1f);
                SkillFinishedChange();
                break;
            case "HunterTwoSkill":
                weaponEffect = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Hunter02_VrilleArrow);
                Vector3 pos1 = new Vector3(weapon.transform.position.x, weapon.transform.position.y - 0.2f);
                weaponEffect.transform.position = pos1;
                GameObject Skill_Hunter02_VrilleArrow = Instantiate(weaponEffect) as GameObject;
                GameObject Effect_arrowWind = ResourcesManager.Instance.FindPrefab(EffectPrefabs.Effect_arrowWind);
                Vector3 pos2 = new Vector3(weapon.transform.position.x - 1f, weapon.transform.position.y - 0.2f);
                Effect_arrowWind.transform.position = pos2;
                GameObject effect = Instantiate(Effect_arrowWind) as GameObject;

                Rigidbody2D tempRigibody2D1 = Skill_Hunter02_VrilleArrow.GetComponent<Rigidbody2D>();
                tempRigibody2D1.velocity = Vector3.right * 5f;
                Rigidbody2D tempRigibody2D2 = effect.GetComponent<Rigidbody2D>();
                tempRigibody2D2.velocity = Vector3.right * 5f;
                yield return new WaitForSeconds(1f);
                SkillFinishedChange();

                Destroy(effect, 3f);
                Destroy(Skill_Hunter02_VrilleArrow, 3f);

                break;
            case "HunterThreeSkill":
                weaponEffect = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Hunter03_ArrowRainShoot);
                Vector3 pos3 = new Vector3(weapon.transform.position.x, weapon.transform.position.y - 0.2f);
                weaponEffect.transform.position = pos3;
               GameObject Skill_Hunter03_ArrowRainShoot = Instantiate(weaponEffect) as GameObject;
                Rigidbody2D tempRigibody2D3 = Skill_Hunter03_ArrowRainShoot.GetComponent<Rigidbody2D>();
                tempRigibody2D3.velocity = new Vector3(3f, 1f, 0) * 3f;


                yield return new WaitForSeconds(0.5f);
                Destroy(Skill_Hunter03_ArrowRainShoot);
                GameObject weaponEffect2 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Hunter03_ArrowRainDown);
                Vector3 pos4, pos5, pos6;
                if (enemy != null)
                {
                    pos4 = new Vector3(enemy.transform.position.x + 0f, enemy.transform.position.y);
                    pos5 = new Vector3(enemy.transform.position.x + 1f, enemy.transform.position.y);
                    pos6 = new Vector3(enemy.transform.position.x + 2f, enemy.transform.position.y);
                }
                else
                {
                    pos4 = new Vector3(weapon.transform.position.x + 4f, enemy.transform.position.y);
                    pos5 = new Vector3(weapon.transform.position.x + 5f, enemy.transform.position.y);
                    pos6 = new Vector3(weapon.transform.position.x + 6f, enemy.transform.position.y);
                }

                GameObject WeaponEffect1 = Instantiate(weaponEffect2, pos4, Quaternion.identity) as GameObject;
                GameObject WeaponEffect2 = Instantiate(weaponEffect2, pos5, Quaternion.identity) as GameObject;
                GameObject WeaponEffect3 = Instantiate(weaponEffect2, pos6, Quaternion.identity) as GameObject;

                Destroy(WeaponEffect1, 2f);
                Destroy(WeaponEffect2, 2f);
                Destroy(WeaponEffect3, 2f);
                yield return new WaitForSeconds(1f);
                SkillFinishedChange();
                break;
            default:
                break;

        }
    }
    #endregion

    #region 法师动画事件
    IEnumerator  CasterSkillEffectStart()
    {
        GameObject weaponEffect;
        GameObject weaponEffect2;
        if (hero.ActionName == "CasterOneSkill")
        {
            Debug.Log("法师的1技能攻击开始");
            weaponEffect = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Caster01_IceCubeOne);
            weaponEffect2 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Caster01_IceCubeTwo);
            GameObject weapon = hero.transform.Find("Bones/Torso/L-arm/L-fist/Weapon").gameObject;
              Vector3 pos1, pos2, pos3;
            if (enemy != null)
            {
                pos1 = new Vector3(enemy.transform.position.x + 0.5f, enemy.transform.position.y);
                pos2 = new Vector3(enemy.transform.position.x + 1f, enemy.transform.position.y);
                pos3 = new Vector3(enemy.transform.position.x + 1.5f, enemy.transform.position.y);
            }
            else
            {
                pos1 = new Vector3(weapon.transform.position.x + 4f, weapon.transform.position.y);
                pos2 = new Vector3(weapon.transform.position.x + 5f, weapon.transform.position.y);
                pos3 = new Vector3(weapon.transform.position.x + 6f, weapon.transform.position.y);
            }
            GameObject WeaponEffect1 = Instantiate(weaponEffect,pos1,Quaternion.identity) as GameObject;
            GameObject WeaponEffect2 = Instantiate(weaponEffect2, pos2, Quaternion.identity) as GameObject;
            GameObject WeaponEffect3 = Instantiate(weaponEffect, pos3, Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(2f);
            SkillFinishedChange();
        }           
        else if (hero.ActionName == "CasterTwoSkill")
        {
            Debug.Log("法师的2技能攻击开始");
            weaponEffect = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Caster02_FallingStone);
            GameObject weapon = hero.transform.Find("Bones/Torso/L-arm/L-fist/Weapon").gameObject;
            Vector3 pos1, pos2, pos3;
            if (enemy != null)
            {
                pos1 = new Vector3(enemy.transform.position.x + 0f, enemy.transform.position.y);
                pos2 = new Vector3(enemy.transform.position.x + 1f, enemy.transform.position.y);
                pos3 = new Vector3(enemy.transform.position.x + 2f, enemy.transform.position.y);
            }
            else
            {
                pos1 = new Vector3(weapon.transform.position.x + 4f, weapon.transform.position.y);
                pos2 = new Vector3(weapon.transform.position.x + 5f, weapon.transform.position.y);
                pos3 = new Vector3(weapon.transform.position.x + 6f, weapon.transform.position.y);
            }
            WeaponEffect = Instantiate(weaponEffect, pos1, Quaternion.identity) as GameObject;
            WeaponEffect = Instantiate(weaponEffect, pos2, Quaternion.identity) as GameObject;
            WeaponEffect = Instantiate(weaponEffect, pos3, Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(3f);
            SkillFinishedChange();
        }
        else if (hero.ActionName == "CasterThreeSkill")
        {
            Debug.Log("法师的3技能攻击开始");
            Debug.Log("法师的2技能攻击开始");
            weaponEffect = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Caster03_BlackMagic);
            GameObject weapon = hero.transform.Find("Bones/Torso/L-arm/L-fist/Weapon").gameObject;
            Vector3 pos1, pos2, pos3;
            if (enemy != null)
            {
                pos1 = new Vector3(enemy.transform.position.x + 0f, enemy.transform.position.y);
                pos2 = new Vector3(enemy.transform.position.x + 1f, enemy.transform.position.y);
                pos3 = new Vector3(enemy.transform.position.x + 2f, enemy.transform.position.y);
            }
            else
            {
                pos1 = new Vector3(weapon.transform.position.x + 4f, weapon.transform.position.y);
                pos2 = new Vector3(weapon.transform.position.x + 5f, weapon.transform.position.y);
                pos3 = new Vector3(weapon.transform.position.x + 6f, weapon.transform.position.y);
            }
            WeaponEffect = Instantiate(weaponEffect,pos1,Quaternion.identity) as GameObject;
            WeaponEffect = Instantiate(weaponEffect, pos2, Quaternion.identity) as GameObject;
            WeaponEffect = Instantiate(weaponEffect, pos3, Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(4f);
            SkillFinishedChange();
        }

    }
    #endregion

    #region 骑士动画事件
    IEnumerator KnightSkillEffectStart()
    {
        GameObject weaponEffect;
        //GameObject weaponEffect2;
        if (hero.ActionName == "KnightOneSkill")
        {
            Debug.Log("骑士的1技能攻击开始");
            GameObject Skill_Knight01_Belief = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Knight01_Belief);
            GameObject weapon = hero.transform.Find("Bones/Torso/L-arm/L-fist/Weapon").gameObject;
            Skill_Knight01_Belief.transform.position = new Vector3(0f, 0f);
            GameObject WeaponEffect = Instantiate(Skill_Knight01_Belief, this.transform) as GameObject;
            yield return new WaitForSeconds(2f);
            SkillFinishedChange();
        }
        else if (hero.ActionName == "KnightTwoSkill")
        {
            Debug.Log("骑士的2技能攻击开始");
            GameObject Skill_Knight02_Aegis = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Knight02_Aegis);
            GameObject weapon = hero.transform.Find("Bones/Torso/L-arm/L-fist/Weapon").gameObject;
            Skill_Knight02_Aegis.transform.position= new Vector3(0.5f, 0f);
            GameObject WeaponEffect = Instantiate(Skill_Knight02_Aegis, this.transform) as GameObject;
            Destroy(WeaponEffect, 10f);
            yield return new WaitForSeconds(2f);
            SkillFinishedChange();
        }
        else if (hero.ActionName == "KnightThreeSkill")
        {
            Debug.Log("骑士的3技能攻击开始");
            weaponEffect = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Knight03_Gungnir);
            GameObject weapon = hero.transform.Find("Bones/Torso/L-arm/L-fist/Weapon").gameObject;
            Vector3 pos1 = new Vector3(weapon.transform.position.x + 1f, weapon.transform.position.y);
            WeaponEffect = Instantiate(weaponEffect, pos1, Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(4f);
            SkillFinishedChange();
        }

    }
    #endregion
    void SaberWeaponRay()
    {
        //attRay.origin = transform.position;
        //attRay.direction = Vector2.right;
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y + 0.1f);
        RaycastHit2D atthit2 = Physics2D.Raycast(myPos, Vector2.right, 1f * transform.lossyScale.x, findMask);     //长度应该是武器原本长度乘上缩放比例,实际要小一些
        Debug.DrawRay(myPos, Vector2.right, Color.yellow, 1f * transform.lossyScale.x);
        if (atthit2)
        {
            if (atthit2.transform.tag == "Enemy")     //已经接近敌人了
            {
                Debug.Log("突刺到了敌人面前");
                if (WeaponEffect != null)
                {
                    //延时0.3秒关闭特效
                    vp_Timer.In(0.3f, new vp_Timer.Callback(delegate () { SaberOneSkillEffectEnd(); }));
                }
                //SaberOneSkillEffectEnd();
                SkillFinishedChange();
            }
        }
    }
    GameObject WeaponEffect;
    /// <summary>
    /// 剑士一技能突刺,特效的销毁
    /// </summary>
    void SaberOneSkillEffectEnd()
    {
        //生成技能特效
        //WeaponEffect = hero.transform.Find("Bones/Torso/L-arm/L-fist/Weapon/Skill_Saber01_Sprint(Clone)").gameObject;
        Destroy(WeaponEffect);
        //skillEffect.SetActive(false);
        CancelInvoke("SaberWeaponRay");
    }
    #endregion

    #region 碰撞器触发事件
    //进入攻击范围
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            //isAttackEnemy = true;
            //weapon.GetComponent<PolygonCollider2D>().enabled = false;
            //myRigidbody.velocity = Vector2.zero;
        }
        else if (collision.tag == "WinFlag")
        {
            isWin = true;
            Debug.Log("触碰到胜利开关");
        }
    }
}
#endregion
