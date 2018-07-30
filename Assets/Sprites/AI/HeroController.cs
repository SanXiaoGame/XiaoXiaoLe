using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroController : MonoBehaviour
{
    //存活开关
    internal bool isAlive = true;
    //是否正在存活
    internal bool alive = true;
    //是否被眩晕
    internal bool isDiz = false;
    //是否被沉默
    internal bool isSilence = false;
    //是否在奔跑
    internal bool isRun = false;
    //是否可以关卡前进
    internal bool moveSwitch = false;
    //是否可以进行战斗位移
    internal bool moveSwitch_Battle = true;
    //是否正在释放技能
    internal bool skillIsOperation = false;
    //攻击间隔计时
    internal int attackRate = 50;
    //我的职业
    internal string myClass;
    //最终伤害
    internal int totalDamage;
    //常用手位置
    GameObject mainFist;
    //次手位置
    GameObject minorFist;

    //英雄锁定的敌人
    internal GameObject targetEnemy;
    //英雄的状态机
    Animator animHero;
    //英雄的触发器
    CircleCollider2D triggerHero;

    #region 需要提前获取的技能预制体和法球弓箭
    GameObject saber01;
    GameObject saber02;
    GameObject saber03_1;
    GameObject saber03_2;
    GameObject saber03_3;
    GameObject saber03_4;
    GameObject saber03_5;

    GameObject knight01;
    GameObject knight03;

    GameObject berserker01;
    GameObject berserker03_hit;
    GameObject berserker03_1;
    GameObject berserker03_2;
    GameObject berserker03_3;

    GameObject caster01_1;
    GameObject caster01_2;
    GameObject caster02;
    GameObject caster03;

    GameObject hunter01;
    GameObject hunter02;
    GameObject hunter03_1;
    GameObject hunter03_2;

    //法球和弓箭
    GameObject magicBall;
    GameObject arrow;
    #endregion


    private void Awake()
    {
        //获取英雄状态机
        animHero = GetComponent<Animator>();
        //获取英雄触发器
        triggerHero = GetComponent<CircleCollider2D>();
        //获取英雄职业
        myClass = SQLiteManager.Instance.characterDataSource[int.Parse(gameObject.name)].character_Class;
        //获取英雄两手位置
        mainFist = transform.Find(ConstData.MainFist).gameObject;
        minorFist = transform.Find(ConstData.MinorFist).gameObject;
        //获取所有技能特效预制体
        saber01 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Saber01_Sprint);
        saber02 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Saber02_ExplodingSword);
        saber03_1 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Saber03_SixSonicSlash01);
        saber03_2 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Saber03_SixSonicSlash02);
        saber03_3 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Saber03_SixSonicSlash03);
        saber03_4 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Saber03_SixSonicSlash04);
        saber03_5 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Saber03_SixSonicSlash05);
        knight01 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Knight01_Belief);
        knight03 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Knight03_Gungnir);
        berserker01 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Berserker01_LeapAttack);
        berserker03_hit = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_fissureHit);
        berserker03_1 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Berserker03_Fissure01);
        berserker03_2 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Berserker03_Fissure02);
        berserker03_3 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Berserker03_Fissure03);
        caster01_1 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Caster01_IceCubeOne);
        caster01_2 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Caster01_IceCubeTwo);
        caster02 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Caster02_FallingStone);
        caster03 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Caster03_BlackMagic);
        hunter01 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Hunter01_ExplosionArrow);
        hunter02 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Hunter02_VrilleArrow);
        hunter03_1 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Hunter03_ArrowRainShoot);
        hunter03_2 = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Hunter03_ArrowRainDown);
        magicBall = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_magicAttack);
        arrow = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_arrow);
    }

    private void OnEnable()
    {
        isAlive = true;
        alive = true;
        isDiz = false;
        isSilence = false;
        isRun = false;
        moveSwitch_Battle = true;
        skillIsOperation = false;
        targetEnemy = null;
        totalDamage = 0;
    }

    private void Update()
    {
        if (isAlive == true) //一级判断：玩家是否还活着
        {
            switch (myClass)
            {
                case ConstData.Saber:
                case ConstData.Knight:
                case ConstData.Berserker:
                    #region 近战单位AI
                    //二级判断：是否有锁定的敌人，如果有，他是否还活着；如果已经死了，就解除锁定
                    if (targetEnemy != null)
                    {
                        if (targetEnemy.GetComponent<EnemyControllers>().isAlive == false)
                        {
                            //解除该敌人的锁定
                            targetEnemy = null;
                            //允许战斗位移（因为敌人死了，需要进行战斗位移）
                            moveSwitch_Battle = true;
                        }
                    }
                    else if (targetEnemy == null && FlagManController.battleSwitch == true && isDiz == false && skillIsOperation == false)
                    {
                        //允许战斗位移
                        moveSwitch_Battle = true;
                    }
                    //二级判断：是否处于战斗状态中and以及英雄是否被眩晕and是否在释放技能
                    if (FlagManController.battleSwitch == true && isDiz == false && skillIsOperation == false)
                    {
                        moveSwitch = false;
                        if (moveSwitch_Battle == true) //三级判断：是否可以进行战斗位移（敌人离开了攻击范围）
                        {
                            if (isRun == false) //四级判断：是否在播放奔跑动画
                            {
                                animHero.SetBool("isRun", true);
                                isRun = true;
                            }
                            transform.position += Vector3.right * Time.deltaTime * 1f;
                        }
                        else
                        {
                            if (isRun == true)
                            {
                                animHero.SetBool("isRun", false);
                                isRun = false;
                            }
                        }
                        #region AI普通攻击
                        if (targetEnemy != null) //三级判断：是否锁定了敌人（攻击范围有敌人），如果有就开始进行自动攻击
                        {
                            switch (myClass) //四级判断：根据自己职业调用自动攻击的种类（近战）
                            {
                                case ConstData.Saber:
                                    if (attackRate >= 50)
                                    {
                                        animHero.SetTrigger("Attack");
                                        attackRate = 0;
                                    }
                                    break;
                                case ConstData.Knight:
                                    if (attackRate >= 50)
                                    {
                                        animHero.SetTrigger("AttackPoke");
                                        attackRate = 0;
                                    }
                                    break;
                                case ConstData.Berserker:
                                    if (attackRate >= 50)
                                    {
                                        animHero.SetTrigger("Attack");
                                        attackRate = 0;
                                    }
                                    break;
                            }
                            attackRate++;
                        }
                        #endregion
                    }
                    else if (FlagManController.battleSwitch == false && skillIsOperation == false) //二级判断：是否处于战斗中and是否处于释放技能中
                    {
                        if (moveSwitch == true) //三级判断：是否可以关卡移动
                        {
                            if (isRun == false) //四级判断：是否在播放奔跑动画
                            {
                                animHero.SetBool("isRun", true);
                                isRun = true;
                            }
                            transform.position += Vector3.right * Time.deltaTime * 1f;
                        }
                        else
                        {
                            if (isRun == true)
                            {
                                animHero.SetBool("isRun", false);
                                isRun = false;
                            }
                        }
                        if (targetEnemy != null)
                        {
                            targetEnemy = null;
                        }
                    }
                    break;
                    #endregion
                case ConstData.Caster:
                case ConstData.Hunter:
                    #region 远程单位AI
                    //二级判断：是否处于战斗状态中and以及英雄是否被眩晕and是否在释放技能
                    if (FlagManController.battleSwitch == true && isDiz == false && skillIsOperation == false)
                    {
                        moveSwitch = false;
                        if (isRun == true)
                        {
                            animHero.SetBool("isRun", false);
                            isRun = false;
                        }
                        #region AI普通攻击
                        //进行普通攻击（远程）
                        switch (myClass)
                        {
                            case ConstData.Caster:
                                if (attackRate >= 85)
                                {
                                    animHero.SetTrigger("AttackPoke");
                                    attackRate = 0;
                                }
                                break;
                            case ConstData.Hunter:
                                if (attackRate >= 70)
                                {
                                    animHero.SetTrigger("AttackBow");
                                    attackRate = 0;
                                }
                                break;
                        }
                        attackRate++;
                        #endregion
                    }
                    //二级判断：是否处于战斗中and是否处于释放技能中
                    else if (FlagManController.battleSwitch == false && skillIsOperation == false) 
                    {
                        #region 战斗结束开始移动判断
                        if (moveSwitch == true) //三级判断：是否可以关卡移动
                        {
                            if (isRun == false) //四级判断：是否在播放奔跑动画
                            {
                                animHero.SetBool("isRun", true);
                                isRun = true;
                            }
                            transform.position += Vector3.right * Time.deltaTime * 1f;
                        }
                        else
                        {
                            if (isRun == true)
                            {
                                animHero.SetBool("isRun", false);
                                isRun = false;
                            }
                        }
                        if (targetEnemy != null)
                        {
                            targetEnemy = null;
                        }
                        #endregion
                    }
                    break;
                    #endregion
            }
        }
    }

    /// <summary>
    /// 监测碰到的点
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ConstData.WinFlagTag)
        {
            switch (myClass)
            {
                case ConstData.Saber:
                    vp_Timer.In(0.2f, new vp_Timer.Callback
                        (delegate () { moveSwitch = false; }));
                    break;
                case ConstData.Knight:
                    vp_Timer.In(1.0f, new vp_Timer.Callback
                        (delegate () { moveSwitch = false; }));
                    break;
                case ConstData.Berserker:
                    vp_Timer.In(1.8f, new vp_Timer.Callback
                        (delegate () { moveSwitch = false; }));
                    break;
                case ConstData.Caster:
                    vp_Timer.In(2.6f, new vp_Timer.Callback
                        (delegate () { moveSwitch = false; }));
                    break;
                case ConstData.Hunter:
                    vp_Timer.In(3.4f, new vp_Timer.Callback
                        (delegate () { moveSwitch = false; }));
                    break;
            }
        }
    }

    /// <summary>
    /// 监测是否有敌人进入攻击范围，是就锁定
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        //判断：侦测到的是否是敌人and我当前没有锁定任何敌人
        if (collision.tag == "Enemy" && targetEnemy == null) 
        {
            //锁定敌人
            targetEnemy = collision.gameObject;
            //不允许战斗位移（因为敌人在攻击范围内，不需要进行战斗位移）
            moveSwitch_Battle = false;
        }
        //判断：碰到的是不是旗手and我是否活着
        if (collision.tag == "FlagMan" && isAlive == true && FlagManController.battleSwitch == false)
        {
            //关卡移动开启
            moveSwitch = true;
            //判断：我是否正在被眩晕，如果是则取消眩晕
            if (isDiz == true)
            {
                isDiz = false;
            }
            skillIsOperation = false;
        }
    }
    /// <summary>
    /// 监测锁定敌人是否离开攻击范围
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            //判断：侦测到离开的是否是我当前锁定的敌人
            if (collision == targetEnemy)
            {
                //解除该敌人的锁定
                targetEnemy = null;
                //允许战斗位移（因为敌人离开了攻击范围，需要进行战斗位移）
                moveSwitch_Battle = true;
            }
        }
    }

    //普通攻击的调用
    internal void Attack()
    {
        if (isDiz == false && isAlive == true && skillIsOperation == false && FlagManController.battleSwitch == true)
        {
            switch (myClass)
            {
                case ConstData.Saber:
                case ConstData.Knight:
                case ConstData.Berserker:
                    if (targetEnemy != null)
                    {
                        AudioManager.Instance.PlayEffectMusic(SoundEffect.Hit);
                        //生成击打特效
                        GameObject hit1 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_hit));
                        hit1.transform.position = targetEnemy.transform.position;
                        //回收击打特效
                        vp_Timer.In(1f, new vp_Timer.Callback(delegate ()
                        {
                            if (SceneManager.GetActiveScene().name != "LoadingScene")
                            {
                                ObjectPoolManager.Instance.RecycleMyGameObject(hit1);
                            }
                        }));
                        //计算伤害
                        if (targetEnemy.GetComponent<EnemyStates>().god == false)
                        {
                            totalDamage = (int)
                                (
                                (
                                transform.GetComponent<HeroStates>().currentAD * 1f -
                                (transform.GetComponent<HeroStates>().currentAD * 1f) *
                                (targetEnemy.GetComponent<EnemyStates>().currentDEF * 0.01f)
                                ) * 0.1f
                                );
                            targetEnemy.GetComponent<EnemyStates>().currentHP -= totalDamage;
                        }
                    }
                    break;
                case ConstData.Caster:
                    //生成法球
                    GameObject magic = ObjectPoolManager.Instance.InstantiateMyGameObject(magicBall);
                    magic.transform.position = mainFist.transform.position + new Vector3(0.3f, 0.05f, 0);
                    break;
                case ConstData.Hunter:
                    //生成弓箭
                    GameObject arw = ObjectPoolManager.Instance.InstantiateMyGameObject(arrow);
                    arw.transform.position = mainFist.transform.position + new Vector3(0.1f, 0.05f, 0);
                    arw.transform.rotation = arrow.transform.rotation;
                    break;
            }
        }
    }

    /// <summary>
    /// 所有职业的一阶技能调用方法
    /// </summary>
    /// <param 要释放技能的职业="heroClass"></param>
    internal void Skill_A(string heroClass)
    {
        if (myClass == heroClass && isDiz == false && isSilence == false && isAlive == true && skillIsOperation == false && 
            FlagManController.battleSwitch == true)
        {
            switch (heroClass)
            {
                case ConstData.Saber:
                    Skill_A_Saber();
                    break;
                case ConstData.Knight:
                    Skill_A_Knight();
                    break;
                case ConstData.Berserker:
                    Skill_A_Berserker();
                    break;
                case ConstData.Caster:
                    Skill_A_Caster();
                    break;
                case ConstData.Hunter:
                    Skill_A_Hunter();
                    break;
            }
        }
    }

    /// <summary>
    /// 所有职业的二阶技能调用方法
    /// </summary>
    /// <param 要释放技能的职业="heroClass"></param>
    internal void Skill_B(string heroClass)
    {
        if (myClass == heroClass && isDiz == false && isSilence == false && isAlive == true && skillIsOperation == false &&
            FlagManController.battleSwitch == true)
        {
            switch (heroClass)
            {
                case ConstData.Saber:
                    Skill_B_Saber();
                    Debug.Log("SaberB");
                    break;
                case ConstData.Knight:
                    Skill_B_Knight();
                    Debug.Log("KnightB");
                    break;
                case ConstData.Berserker:
                    Skill_B_Berserker();
                    Debug.Log("BerserkerB");
                    break;
                case ConstData.Caster:
                    Skill_B_Caster();
                    Debug.Log("CasterB");
                    break;
                case ConstData.Hunter:
                    Skill_B_Hunter();
                    Debug.Log("HunterB");
                    break;
            }
        }
    }

    /// <summary>
    /// 所有职业的三阶技能调用方法
    /// </summary>
    /// <param 要释放技能的职业="heroClass"></param>
    internal void Skill_C(string heroClass)
    {
        if (myClass == heroClass && isDiz == false && isSilence == false && isAlive == true && skillIsOperation == false &&
            FlagManController.battleSwitch == true)
        {
            switch (heroClass)
            {
                case ConstData.Saber:
                    Skill_C_Saber();
                    Debug.Log("SaberC");
                    break;
                case ConstData.Knight:
                    Skill_C_Knight();
                    Debug.Log("KnightC");
                    break;
                case ConstData.Berserker:
                    Skill_C_Berserker();
                    Debug.Log("BerserkerC");
                    break;
                case ConstData.Caster:
                    Skill_C_Caster();
                    Debug.Log("CasterC");
                    break;
                case ConstData.Hunter:
                    Skill_C_Hunter();
                    Debug.Log("HunterC");
                    break;
            }
        }
    }


    /////////////////////////////////////////////////////////
    ////////        技能部分：剑士英雄的技能         ////////
    /////////////////////////////////////////////////////////

        
    /// <summary>
    /// 剑士一技能：突刺
    /// </summary>
    void Skill_A_Saber()
    {
        skillIsOperation = true;
        if(isRun == true)
        {
            animHero.SetBool("isRun", false);
            isRun = false;
        }
        transform.position -= Vector3.right * 0.5f;
        animHero.SetBool("SaberOneSkill", true);
        GameObject temp01 = ObjectPoolManager.Instance.InstantiateMyGameObject(saber01);
        InvokeRepeating("Skill_A_Saber_Sprint", 0f, 0.02f);
        temp01.GetComponent<SprintDash>().isOver = false;
        temp01.transform.position = mainFist.transform.position;
        AudioManager.Instance.PlayEffectMusic(SoundEffect.Sprint);
    }
    void Skill_A_Saber_Sprint()
    {
        transform.position += Vector3.right * Time.deltaTime * 3f;
    }
    //被冲刺击退
    internal void Dashed()
    {
        if (myClass != ConstData.Berserker)
        {
            transform.position -= Vector3.right * Time.deltaTime * 3f;
        }
    }
    /// <summary>
    /// 剑士二技能：剑气
    /// </summary>
    void Skill_B_Saber()
    {
        skillIsOperation = true;
        if (isRun == true)
        {
            animHero.SetBool("isRun", false);
            isRun = false;
        }
        animHero.SetTrigger("SaberTwoSkill");
    }
    internal void Skill_B_Saber_ExplosionSlash()
    {
        AudioManager.Instance.PlayEffectMusic(SoundEffect.ExplodingSword);
        GameObject temp02 = ObjectPoolManager.Instance.InstantiateMyGameObject(saber02);
        temp02.transform.position = transform.position + new Vector3(0.8f, -0.126f, 0);
        vp_Timer.In(0.8f, new vp_Timer.Callback(delegate () { skillIsOperation = false; }));
    }
    /// <summary>
    /// 剑士三技能：六光连斩
    /// </summary>
    void Skill_C_Saber()
    {
        skillIsOperation = true;
        if (isRun == true)
        {
            animHero.SetBool("isRun", false);
            isRun = false;
        }
        animHero.SetTrigger("SaberThreeSkill");
    }
    internal void InstantiateSlash(int num)
    {
        //根据技能动画传过来的参数不同生成不同的技能特效
        switch (num)
        {
            case 1:
                AudioManager.Instance.PlayEffectMusic(SoundEffect.SixSonicSlash2);
                GameObject slash1 = ObjectPoolManager.Instance.InstantiateMyGameObject(saber03_1);
                slash1.transform.position = transform.position + new Vector3(0, 1.1f, 0);
                break;
            case 2:
                AudioManager.Instance.PlayEffectMusic(SoundEffect.SixSonicSlash1);
                GameObject slash2 = ObjectPoolManager.Instance.InstantiateMyGameObject(saber03_2);
                slash2.transform.position = transform.position + new Vector3(2.257f, -0.126f, 0);
                break;
            case 3:
                AudioManager.Instance.PlayEffectMusic(SoundEffect.SixSonicSlash1);
                GameObject slash3 = ObjectPoolManager.Instance.InstantiateMyGameObject(saber03_3);
                slash3.transform.position = transform.position + new Vector3(-0.595f, -0.368f, 0);
                break;
            case 4:
                AudioManager.Instance.PlayEffectMusic(SoundEffect.SixSonicSlash2);
                GameObject slash4 = ObjectPoolManager.Instance.InstantiateMyGameObject(saber03_4);
                slash4.transform.position = transform.position + new Vector3(0.857f, -0.126f, 0);
                break;
            case 5:
                AudioManager.Instance.PlayEffectMusic(SoundEffect.SixSonicSlash1);
                GameObject slash5 = ObjectPoolManager.Instance.InstantiateMyGameObject(saber03_5);
                slash5.transform.position = transform.position + new Vector3(0.757f, 0.5f, 0);
                //vp_Timer.In(0.15f, new vp_Timer.Callback(delegate () { skillIsOperation = false; }));
                break;
            case 6:
                skillIsOperation = false;
                break;
        }
    }

    
    /////////////////////////////////////////////////////////
    ////////        技能部分：骑士英雄的技能         ////////
    /////////////////////////////////////////////////////////


    /// <summary>
    /// 骑士一阶技能：信仰
    /// </summary>
    void Skill_A_Knight()
    {
        skillIsOperation = true;
        animHero.SetTrigger("KnightSkill");
        if (isRun == true)
        {
            animHero.SetBool("isRun", false);
            isRun = false;
        }
        vp_Timer.In(0.5f, new vp_Timer.Callback(delegate () { Skill_A_Knight_Belief(); }));
    }
    internal void Skill_A_Knight_Belief()
    {
        AudioManager.Instance.PlayEffectMusic(SoundEffect.Belief);
        GameObject st = ObjectPoolManager.Instance.InstantiateMyGameObject(knight01);
        if (SceneManager.GetActiveScene().name != "LoadingScene")
        {
            st.transform.position = transform.position;
        }
        
        vp_Timer.In(1f, new vp_Timer.Callback(delegate ()
        {
            if (SceneManager.GetActiveScene().name != "LoadingScene")
            {
                ObjectPoolManager.Instance.RecycleMyGameObject(st);
            }
        }));
        transform.GetComponent<HeroStates>().GetState(3210, 5.0f);
        vp_Timer.In(0.2f, new vp_Timer.Callback(delegate () { skillIsOperation = false; }));
    }
    /// <summary>
    /// 骑士二阶技能：圣盾
    /// </summary>
    void Skill_B_Knight()
    {
        skillIsOperation = true;
        animHero.SetTrigger("KnightSkill");
        if (isRun == true)
        {
            animHero.SetBool("isRun", false);
            isRun = false;
        }
        vp_Timer.In(0.5f, new vp_Timer.Callback(delegate () { Skill_B_Knight_Aegis(); }));
    }
    internal void Skill_B_Knight_Aegis()
    {
        AudioManager.Instance.PlayEffectMusic(SoundEffect.Aegis);
        transform.GetComponent<HeroStates>().GetState(3211, 3.0f);
        vp_Timer.In(0.5f, new vp_Timer.Callback(delegate () { skillIsOperation = false; }));
    }
    /// <summary>
    /// 骑士三阶技能：圣矛投掷
    /// </summary>
    void Skill_C_Knight()
    {
        skillIsOperation = true;
        if (isRun == true)
        {
            animHero.SetBool("isRun", false);
            isRun = false;
        }
        vp_Timer.In(0.5f, new vp_Timer.Callback(delegate () { Skill_C_Knight_Gungnir(); }));
    }
    internal void Skill_C_Knight_Gungnir()
    {
        AudioManager.Instance.PlayEffectMusic(SoundEffect.Belief);
        GameObject temp03 = ObjectPoolManager.Instance.InstantiateMyGameObject(knight03);
        temp03.transform.position = transform.position - new Vector3(0.4f, -0.2f, 0);
        vp_Timer.In(0.8f, new vp_Timer.Callback(delegate () { AudioManager.Instance.PlayEffectMusic(SoundEffect.Gungnir_Throw); }));
        vp_Timer.In(0.5f, new vp_Timer.Callback(delegate () { skillIsOperation = false; }));
    }


    /////////////////////////////////////////////////////////
    ////////        技能部分：狂战英雄的技能         ////////
    /////////////////////////////////////////////////////////


    /// <summary>
    /// 狂战士一阶技能：跳劈
    /// </summary>
    void Skill_A_Berserker()
    {
        skillIsOperation = true;
        if (isRun == true)
        {
            animHero.SetBool("isRun", false);
            isRun = false;
        }
        animHero.SetTrigger("BerserkerOneSkill");
    }
    internal void Skill_A_Berserker_LeapAttack()
    {
        AudioManager.Instance.PlayEffectMusic(SoundEffect.LeapAttack);
        GameObject temp04 = ObjectPoolManager.Instance.InstantiateMyGameObject(berserker01);
        temp04.transform.position = transform.position + new Vector3(1f, -0.126f, 0);
        vp_Timer.In(0.3f, new vp_Timer.Callback(delegate () { skillIsOperation = false; }));
    }
    /// <summary>
    /// 狂战士二阶技能：嗜血
    /// </summary>
    void Skill_B_Berserker()
    {
        skillIsOperation = true;
        if (isRun == true)
        {
            animHero.SetBool("isRun", false);
            isRun = false;
        }
        Skill_B_Berserker_Bleed();
    }
    internal void Skill_B_Berserker_Bleed()
    {
        AudioManager.Instance.PlayEffectMusic(SoundEffect.Blood);
        GameObject st = ObjectPoolManager.Instance.InstantiateMyGameObject
            (ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Berserker02_Blood_01));
        st.transform.position = transform.position + new Vector3(0, 1, 0);
        st.transform.parent = transform;
        vp_Timer.In(5.0f, new vp_Timer.Callback(delegate ()
        {
            if (SceneManager.GetActiveScene().name != "LoadingScene")
            {
                ObjectPoolManager.Instance.RecycleMyGameObject(st);
            }
        }));
        transform.GetComponent<HeroStates>().GetState(3207, 5.0f);
        transform.GetComponent<HeroStates>().GetState(3209, 5.0f);
        vp_Timer.In(0.8f, new vp_Timer.Callback(delegate () { skillIsOperation = false; }));
    }
    /// <summary>
    /// 狂战士三阶技能：地裂
    /// </summary>
    void Skill_C_Berserker()
    {
        skillIsOperation = true;
        if (isRun == true)
        {
            animHero.SetBool("isRun", false);
            isRun = false;
        }
        animHero.SetTrigger("BerserkerThreeSkill");
    }
    internal void Skill_C_Berserker_Fissure(int num)
    {
        switch (num)
        {
            case 1:
                AudioManager.Instance.PlayEffectMusic(SoundEffect.BoatAnchorHit);
                GameObject efct = ObjectPoolManager.Instance.InstantiateMyGameObject(berserker03_hit);
                efct.transform.position = transform.position + new Vector3(1f, -0.15f, 0);
                vp_Timer.In(0.3f, new vp_Timer.Callback(delegate ()
                {
                    if (SceneManager.GetActiveScene().name != "LoadingScene")
                    {
                        ObjectPoolManager.Instance.RecycleMyGameObject(efct);
                    }
                }));
                vp_Timer.In(0.5f, new vp_Timer.Callback(delegate () { skillIsOperation = false; }));
                break;
            case 2:
                GameObject sto1 = ObjectPoolManager.Instance.InstantiateMyGameObject(berserker03_1);
                sto1.transform.position = transform.position + new Vector3(0.5f, -0.126f, 0);
                sto1.transform.position -= new Vector3(0, 0.9f, 0);
                break;
            case 3:
                GameObject sto2 = ObjectPoolManager.Instance.InstantiateMyGameObject(berserker03_2);
                sto2.transform.position = transform.position + new Vector3(2.4f, -0.126f, 0);
                sto2.transform.position -= new Vector3(0, 0.9f, 0);
                break;
            case 4:
                GameObject sto3 = ObjectPoolManager.Instance.InstantiateMyGameObject(berserker03_3);
                sto3.transform.position = transform.position + new Vector3(1.2f, -0.126f, 0);
                sto3.transform.position -= new Vector3(0, 0.9f, 0);
                break;
        }
    }



    /////////////////////////////////////////////////////////
    ////////        技能部分：法师英雄的技能         ////////
    /////////////////////////////////////////////////////////


    /// <summary>
    /// 法师一阶技能：冰冻术
    /// </summary>
    void Skill_A_Caster()
    {
        skillIsOperation = true;
        animHero.SetTrigger("CasterSkill");
        if (isRun == true)
        {
            animHero.SetBool("isRun", false);
            isRun = false;
        }
        vp_Timer.In(0.5f, new vp_Timer.Callback(delegate ()
        {
            if (SceneManager.GetActiveScene().name != "LoadingScene")
            {
                Skill_A_Caster_IceCube();
            }
        }));
    }
    internal void Skill_A_Caster_IceCube()
    {
        AudioManager.Instance.PlayEffectMusic(SoundEffect.Freeze_Awake);
        GameObject icecube1 = ObjectPoolManager.Instance.InstantiateMyGameObject(caster01_1);
        icecube1.transform.position = transform.position + new Vector3(1.2f, 0.05f, 0);
        icecube1.transform.name = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Caster01_IceCubeOne).name;
        GameObject icecube2 = ObjectPoolManager.Instance.InstantiateMyGameObject(caster01_2);
        icecube2.transform.position = transform.position + new Vector3(2.2f, -0.02f, 0);
        icecube2.transform.name = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Caster01_IceCubeTwo).name;
        GameObject icecube3 = ObjectPoolManager.Instance.InstantiateMyGameObject(caster01_1);
        icecube3.transform.position = transform.position + new Vector3(3.2f, 0.05f, 0);
        icecube3.transform.name = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Caster01_IceCubeOne).name;
        vp_Timer.In(0.3f, new vp_Timer.Callback(delegate () { skillIsOperation = false; }));
    }
    /// <summary>
    /// 法师二阶技能：陨石术
    /// </summary>
    void Skill_B_Caster()
    {
        skillIsOperation = true;
        animHero.SetTrigger("CasterSkill");
        if (isRun == true)
        {
            animHero.SetBool("isRun", false);
            isRun = false;
        }
        AudioManager.Instance.PlayEffectMusic(SoundEffect.FallingStone_Spellcaster);
        vp_Timer.In(0.6f, new vp_Timer.Callback(delegate () { stoneSummon(); }));
        vp_Timer.In(0.9f, new vp_Timer.Callback(delegate () { stoneSummon(); }));
        vp_Timer.In(1f, new vp_Timer.Callback(delegate () { stoneSummon(); }));
        vp_Timer.In(1.2f, new vp_Timer.Callback(delegate () { stoneSummon(); }));
        vp_Timer.In(1.8f, new vp_Timer.Callback(delegate () { stoneSummon(); }));
        vp_Timer.In(2.3f, new vp_Timer.Callback(delegate () { stoneSummon(); }));
        vp_Timer.In(0.8f, new vp_Timer.Callback(delegate () { skillIsOperation = false; }));
    }
    internal void stoneSummon()
    {

        GameObject sto = ObjectPoolManager.Instance.InstantiateMyGameObject(caster02);
        sto.transform.position = transform.Find("/1001").transform.position + new Vector3(Random.Range(-4f,1.0f),4.132f,0);
    }
    /// <summary>
    /// 法师三阶技能：黑魔法
    /// </summary>
    void Skill_C_Caster()
    {
        skillIsOperation = true;
        animHero.SetTrigger("CasterSkill");
        if (isRun == true)
        {
            animHero.SetBool("isRun", false);
            isRun = false;
        }
        vp_Timer.In(0.5f, new vp_Timer.Callback(delegate () { blkMagic(); }));
        vp_Timer.In(0.8f, new vp_Timer.Callback(delegate () { skillIsOperation = false; }));
    }
    internal void blkMagic()
    {
        AudioManager.Instance.PlayEffectMusic(SoundEffect.BlackMagic_Shoot);
        GameObject blk = ObjectPoolManager.Instance.InstantiateMyGameObject(caster03);
        blk.transform.position = transform.position + new Vector3(2.2f, -0.126f, 0);

    }



    /////////////////////////////////////////////////////////
    ////////        技能部分：猎人英雄的技能         ////////
    /////////////////////////////////////////////////////////



    /// <summary>
    /// 猎人一阶技能：爆破箭
    /// </summary>
    void Skill_A_Hunter()
    {
        skillIsOperation = true;
        animHero.SetTrigger("HunterSkill");
        if (isRun == true)
        {
            animHero.SetBool("isRun", false);
            isRun = false;
        }
        vp_Timer.In(1.0f, new vp_Timer.Callback(delegate () 
        {
            ExplosionArrowShoot();
            vp_Timer.In(1.0f, new vp_Timer.Callback(delegate () { skillIsOperation = false; }));
        }));
    }
    internal void ExplosionArrowShoot()
    {
        AudioManager.Instance.PlayEffectMusic(SoundEffect.ExplodeArrow_Shoot);
        GameObject expArw = ObjectPoolManager.Instance.InstantiateMyGameObject(hunter01);
        if (SceneManager.GetActiveScene().name != "LoadingScene")
        {
            expArw.transform.position = transform.position + new Vector3(0.2f, 0.2f, 0);
            expArw.transform.rotation = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Hunter01_ExplosionArrow).transform.rotation;
        }
    }
    /// <summary>
    /// 猎人二阶技能：螺旋箭
    /// </summary>
    void Skill_B_Hunter()
    {
        skillIsOperation = true;
        animHero.SetTrigger("HunterSkill");
        if (isRun == true)
        {
            animHero.SetBool("isRun", false);
            isRun = false;
        }
        vp_Timer.In(1.0f, new vp_Timer.Callback(delegate () 
        {
            VrilleArrowShoot();
            vp_Timer.In(1.0f, new vp_Timer.Callback(delegate () { skillIsOperation = false; }));
        }));
    }
    internal void VrilleArrowShoot()
    {
        AudioManager.Instance.PlayEffectMusic(SoundEffect.VrilleArrow);
        GameObject VllArw = ObjectPoolManager.Instance.InstantiateMyGameObject(hunter02);
        if (SceneManager.GetActiveScene().name != "LoadingScene")
        {
            VllArw.transform.position = transform.position;
        }
        GameObject shootEffect = ObjectPoolManager.Instance.InstantiateMyGameObject
            (ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_arrowWind));
        shootEffect.transform.position = mainFist.transform.position;
        vp_Timer.In(0.8f, new vp_Timer.Callback(delegate ()
        {
            if (SceneManager.GetActiveScene().name != "LoadingScene")
            {
                ObjectPoolManager.Instance.RecycleMyGameObject(shootEffect);
            }
        }));
    }
    /// <summary>
    /// 猎人三阶技能：火箭雨
    /// </summary>
    void Skill_C_Hunter()
    {
        skillIsOperation = true;
        animHero.SetTrigger("HunterSkill");
        if (isRun == true)
        {
            animHero.SetBool("isRun", false);
            isRun = false;
        }
        vp_Timer.In(1.0f, new vp_Timer.Callback(delegate ()
        {
            ArrowRainShoot();
            vp_Timer.In(1.0f, new vp_Timer.Callback(delegate () { skillIsOperation = false; }));
        }));
        
    }
    internal void ArrowRainShoot()
    {
        AudioManager.Instance.PlayEffectMusic(SoundEffect.FireArrowRain_Shoot);
        //第一根
        GameObject upArw = ObjectPoolManager.Instance.InstantiateMyGameObject(hunter03_1);
        upArw.transform.position = transform.position;
        upArw.transform.rotation = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Hunter03_ArrowRainShoot).transform.rotation;
        vp_Timer.In(1f, new vp_Timer.Callback(delegate ()
        {
            if (SceneManager.GetActiveScene().name != "LoadingScene")
            {
                ObjectPoolManager.Instance.RecycleMyGameObject(upArw);
            }
        }));
        //第二根
        vp_Timer.In(0.1f, new vp_Timer.Callback(delegate ()
        {
            GameObject upArw2 = ObjectPoolManager.Instance.InstantiateMyGameObject(hunter03_1);
            upArw2.transform.position = transform.position;
            upArw2.transform.rotation = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Hunter03_ArrowRainShoot).transform.rotation;
            vp_Timer.In(1f, new vp_Timer.Callback(delegate ()
            {
                if (SceneManager.GetActiveScene().name != "LoadingScene")
                {
                    ObjectPoolManager.Instance.RecycleMyGameObject(upArw2);
                }
            }));
        }));
        //第三根
        vp_Timer.In(0.2f, new vp_Timer.Callback(delegate ()
        {
            GameObject upArw3 = ObjectPoolManager.Instance.InstantiateMyGameObject(hunter03_1);
            upArw3.transform.position = transform.position;
            upArw3.transform.rotation = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Hunter03_ArrowRainShoot).transform.rotation;
            vp_Timer.In(1f, new vp_Timer.Callback(delegate ()
            {
                if (SceneManager.GetActiveScene().name != "LoadingScene")
                {
                    ObjectPoolManager.Instance.RecycleMyGameObject(upArw3);
                }
            }));
        }));
        //第四根
        vp_Timer.In(0.3f, new vp_Timer.Callback(delegate ()
        {
            GameObject upArw4 = ObjectPoolManager.Instance.InstantiateMyGameObject(hunter03_1);
            upArw4.transform.position = transform.position;
            upArw4.transform.rotation = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Hunter03_ArrowRainShoot).transform.rotation;
            vp_Timer.In(1f, new vp_Timer.Callback(delegate ()
            {
                if (SceneManager.GetActiveScene().name != "LoadingScene")
                {
                    ObjectPoolManager.Instance.RecycleMyGameObject(upArw4);
                }
            }));
        }));
        //第五根
        vp_Timer.In(0.4f, new vp_Timer.Callback(delegate ()
        {
            GameObject upArw5 = ObjectPoolManager.Instance.InstantiateMyGameObject(hunter03_1);
            upArw5.transform.position = transform.position;
            upArw5.transform.rotation = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Hunter03_ArrowRainShoot).transform.rotation;
            vp_Timer.In(1f, new vp_Timer.Callback(delegate () { ObjectPoolManager.Instance.RecycleMyGameObject(upArw5); }));
        }));
        //开始落下
        vp_Timer.In(1.0f, new vp_Timer.Callback(delegate ()
        {
            ArrowRainDown();
        }));
    }
    internal void ArrowRainDown()
    {
        AudioManager.Instance.PlayEffectMusic(SoundEffect.FireArrowRain_Hit);
        //第一根
        GameObject downArw = ObjectPoolManager.Instance.InstantiateMyGameObject(hunter03_2);
        downArw.transform.position = transform.Find("/1001").transform.position + new Vector3(Random.Range(1.658f,5f), 4.132f, 0);
        downArw.transform.rotation = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Hunter03_ArrowRainDown).transform.rotation;
        //第二根
        vp_Timer.In(0.2f, new vp_Timer.Callback(delegate ()
        {
            GameObject downArw2 = ObjectPoolManager.Instance.InstantiateMyGameObject(hunter03_2);
            downArw2.transform.position = transform.Find("/1001").transform.position + new Vector3(Random.Range(1.658f, 5f), 4.132f, 0);
            downArw2.transform.rotation = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Hunter03_ArrowRainDown).transform.rotation;
        }));
        //第三根
        vp_Timer.In(0.4f, new vp_Timer.Callback(delegate ()
        {
            GameObject downArw3 = ObjectPoolManager.Instance.InstantiateMyGameObject(hunter03_2);
            downArw3.transform.position = transform.Find("/1001").transform.position + new Vector3(Random.Range(1.658f, 5f), 4.132f, 0);
            downArw3.transform.rotation = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Hunter03_ArrowRainDown).transform.rotation;
        }));
        //第四根
        vp_Timer.In(0.8f, new vp_Timer.Callback(delegate ()
        {
            GameObject downArw4 = ObjectPoolManager.Instance.InstantiateMyGameObject(hunter03_2);
            downArw4.transform.position = transform.Find("/1001").transform.position + new Vector3(Random.Range(1.658f, 5f), 4.132f, 0);
            downArw4.transform.rotation = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Hunter03_ArrowRainDown).transform.rotation;
        }));
        //第五根
        vp_Timer.In(1.0f, new vp_Timer.Callback(delegate ()
        {
            GameObject downArw5 = ObjectPoolManager.Instance.InstantiateMyGameObject(hunter03_2);
            downArw5.transform.position = transform.Find("/1001").transform.position + new Vector3(Random.Range(1.658f, 5f), 4.132f, 0);
            downArw5.transform.rotation = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Hunter03_ArrowRainDown).transform.rotation;
        }));
        //第六根
        vp_Timer.In(1.2f, new vp_Timer.Callback(delegate ()
        {
            GameObject downArw6 = ObjectPoolManager.Instance.InstantiateMyGameObject(hunter03_2);
            downArw6.transform.position = transform.Find("/1001").transform.position + new Vector3(Random.Range(1.658f, 5f), 4.132f, 0);
            downArw6.transform.rotation = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Hunter03_ArrowRainDown).transform.rotation;
        }));
        //第七根
        vp_Timer.In(1.4f, new vp_Timer.Callback(delegate ()
        {
            GameObject downArw7 = ObjectPoolManager.Instance.InstantiateMyGameObject(hunter03_2);
            downArw7.transform.position = transform.Find("/1001").transform.position + new Vector3(Random.Range(1.658f, 5f), 4.132f, 0);
            downArw7.transform.rotation = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Hunter03_ArrowRainDown).transform.rotation;
        }));
        //第八根
        vp_Timer.In(1.6f, new vp_Timer.Callback(delegate ()
        {
            GameObject downArw8 = ObjectPoolManager.Instance.InstantiateMyGameObject(hunter03_2);
            downArw8.transform.position = transform.Find("/1001").transform.position + new Vector3(Random.Range(1.658f, 5f), 4.132f, 0);
            downArw8.transform.rotation = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Hunter03_ArrowRainDown).transform.rotation;
        }));
        //第九根
        vp_Timer.In(1.8f, new vp_Timer.Callback(delegate ()
        {
            GameObject downArw9 = ObjectPoolManager.Instance.InstantiateMyGameObject(hunter03_2);
            downArw9.transform.position = transform.Find("/1001").transform.position + new Vector3(Random.Range(1.658f, 5f), 4.132f, 0);
            downArw9.transform.rotation = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Hunter03_ArrowRainDown).transform.rotation;
        }));
    }

    internal void RecWeapon()
    {
        if (myClass != ConstData.Hunter && myClass != ConstData.FlagMan)
        {
            ObjectPoolManager.Instance.RecycleMyGameObject(minorFist.transform.GetChild(0).gameObject);
            ObjectPoolManager.Instance.RecycleMyGameObject(mainFist.transform.GetChild(0).gameObject);
        }
        else if (myClass == ConstData.Hunter && myClass != ConstData.FlagMan)
        {
            ObjectPoolManager.Instance.RecycleMyGameObject(minorFist.transform.GetChild(0).gameObject);
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
}
