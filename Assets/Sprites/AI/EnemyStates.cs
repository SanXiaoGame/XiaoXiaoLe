using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStates : MonoBehaviour
{
    //敌人初始数据
    EnemyData mydata;
    //敌人状态机
    Animator myanim;
    //敌人的触发器
    BoxCollider2D triggerEnemy;

    #region 基本属性和流属性（敌人）
    //敌人HP上限
    internal int maxHP;
    //敌人状态专用暂存HP
    float tempHPA = 0;
    float tempHPB = 0;
    //敌人动态HP
    internal int currentHP;
    //敌人基础AD
    int ADbase;
    //敌人常规下AD
    internal int AD;
    //敌人削弱后AD
    int ADweak;
    //敌人动态AD
    internal int currentAD;
    //敌人基础AP
    int APbase;
    //敌人常规下AP
    internal int AP;
    //敌人削弱后AP
    int APweak;
    //敌人动态AP
    internal int currentAP;
    //敌人基础物防
    int DEFbase;
    //敌人常规下物防
    internal int DEF;
    //敌人削弱后物防
    int DEFweak;
    //敌人动态物防
    internal int currentDEF;
    //敌人基础魔防
    int RESbase;
    //敌人常规下魔防
    internal int RES;
    //敌人削弱后魔防
    int RESweak;
    //敌人动态魔防
    internal int currentRES;
    #endregion

    #region 状态动画开关（敌人）
    //是否正在被眩晕
    bool isDizing;
    //是否正在被沉默
    bool isSilenceing;
    //是否正在燃烧状态
    bool isBurning;
    //是否正在流血状态
    bool isBleeding;
    //是否正在振奋状态
    bool isInspire;
    //是否正在冥想状态
    bool isMuse;
    //是否正在硬化状态
    bool isHarden;
    //是否正在魔法盾状态
    bool isMagicShield;
    //是否正在无敌状态
    bool isGod;
    //是否正在虚弱状态
    bool isWeaknees;
    //是否正在恐惧状态
    bool isFear;
    #endregion

    #region 状态开关（敌人）
    //燃烧开关
    internal bool burn;
    //流血开关
    internal bool bleed;
    //振奋开关
    internal bool inspire;
    //冥想开关
    internal bool muse;
    //硬化开关
    internal bool harden;
    //魔法盾开关
    internal bool magicShield;
    //无敌开关
    internal bool god;
    //虚弱开关
    internal bool weakness;
    //恐惧开关
    internal bool fear;
    #endregion

    #region 生成的特效对象
    GameObject dizG2;
    GameObject silenceG2;
    GameObject burnG2;
    GameObject bleedG2;
    GameObject inspireG2;
    GameObject museG2;
    GameObject hardenG2;
    GameObject magicShieldG2;
    GameObject godG2;
    GameObject weaknessG2;
    GameObject fearG2;
    #endregion

    private void Awake()
    {
        //获取敌人的数据
        mydata = SQLiteManager.Instance.enemyDataSource[int.Parse(gameObject.name)];
        //获取敌人的状态机
        myanim = transform.GetComponent<Animator>();
        //获取敌人的触发器
        triggerEnemy = transform.GetComponent<BoxCollider2D>();
        //获取数据存入
        maxHP = mydata.HP;
        currentHP = maxHP;
        ADbase = mydata.AD;
        AD = ADbase;
        ADweak = (int)(AD - AD * 0.2f);
        currentAD = AD;
        APbase = mydata.AP;
        AP = APbase;
        APweak = (int)(AP - AP * 0.2f);
        currentAP = AP;
        DEFbase = mydata.DEF;
        DEF = DEFbase;
        DEFweak = (int)(DEF - DEF * 0.2f);
        currentDEF = DEF;
        RESbase = mydata.RES;
        RES = RESbase;
        RESweak = (int)(RES - RES * 0.2f);
        currentRES = RES;
    }

    private void OnEnable()
    {
        //重置属性
        tempHPA = 0;
        tempHPB = 0;
        currentHP = maxHP;
        AD = ADbase;
        currentAD = AD;
        AP = APbase;
        currentAP = AP;
        DEF = DEFbase;
        currentDEF = DEF;
        RES = RESbase;
        currentRES = RES;

        //重开重力
        transform.GetComponent<Rigidbody2D>().gravityScale = 1;
        //重开触发器
        triggerEnemy.enabled = true;
        //是否正在被眩晕
        isDizing = false;
        //是否正在被沉默
        isSilenceing = false;
        //是否正在燃烧状态
        isBurning = false;
        //是否正在流血状态
        isBleeding = false;
        //是否正在振奋状态
        isInspire = false;
        //是否正在冥想状态
        isMuse = false;
        //是否正在硬化状态
        isHarden = false;
        //是否正在魔法盾状态
        isMagicShield = false;
        //是否正在无敌状态
        isGod = false;
        //是否正在虚弱状态
        isWeaknees = false;
        //是否正在恐惧状态
        isFear = false;
        //燃烧开关
        burn = false;
        //流血开关
        bleed = false;
        //振奋开关
        inspire = false;
        //冥想开关
        muse = false;
        //硬化开关
        harden = false;
        //魔法盾开关
        magicShield = false;
        //无敌开关
        god = false;
        //虚弱开关
        weakness = false;
        //恐惧开关
        fear = false;
    }

    private void Update()
    {
        //一级判断：是否还活着
        if(transform.GetComponent<EnemyControllers>().isAlive == true)
        {
            //二级判断：血量是否已经没了，如果是，关闭存活开关，是否存活状态置为否，播放死亡动画
            if (currentHP <= 0 && transform.GetComponent<EnemyControllers>().aliving == true)
            {
                transform.GetComponent<EnemyControllers>().isAlive = false;
                transform.GetComponent<EnemyControllers>().aliving = false;
                myanim.SetTrigger("Dead");
                triggerEnemy.enabled = false;
                StatesClearEnemy();
                MonsterPointManagerStage01.enemyList.Remove(gameObject);
                //传输经验值
                UISettlement.EXPPoolUp(mydata.EXP);
                vp_Timer.In(1.0f, new vp_Timer.Callback(delegate ()
                {
                    if (SceneManager.GetActiveScene().name != "LoadingScene")
                    {
                        ObjectPoolManager.Instance.RecycleMyGameObject(gameObject);
                    }
                }));
                return;
            }
            //二级判断：如果已经被复活了，进入这里时会重新激活是否正在存活（aliving）
            if (transform.GetComponent<EnemyControllers>().aliving == false)
            {
                myanim.SetTrigger("Reset");
                transform.GetComponent<EnemyControllers>().aliving = true;
            }
            //二级判断：玩家是否处于无敌状态，如果否，才能进行接下来的各种BUFF状态监测
            if (god == false)
            {
                //三级判断：如果状态为正在无敌，修正为没有在无敌，并且回收无敌特效预制体
                if (isGod == true)
                {
                    ObjectPoolManager.Instance.RecycleMyGameObject(godG2);
                    isGod = false;
                }
                if (transform.GetComponent<EnemyControllers>().isDiz == true) //眩晕状态：无法行动
                {
                    if (isDizing == false)
                    {
                        myanim.SetBool("isDiz", true);
                        isDizing = true;
                        dizG2 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_diz));
                        dizG2.transform.position = gameObject.transform.position + new Vector3(0, 1, 0);
                        dizG2.transform.parent = transform;
                        dizG2.name = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_diz).name;
                    }
                }
                else
                {
                    if (isDizing == true)
                    {
                        myanim.SetBool("isDiz", false);
                        isDizing = false;
                        ObjectPoolManager.Instance.RecycleMyGameObject(dizG2);
                    }
                }
                if (transform.GetComponent<EnemyControllers>().isSilence == true) //沉默状态：无法释放技能
                {
                    if (isSilenceing == false)
                    {
                        isSilenceing = true;
                        silenceG2 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_silence));
                        silenceG2.transform.position = gameObject.transform.position;
                        silenceG2.transform.parent = transform;
                        silenceG2.name = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_silence).name;
                    }
                }
                else
                {
                    if (isSilenceing == true)
                    {
                        ObjectPoolManager.Instance.RecycleMyGameObject(silenceG2);
                        isSilenceing = false;
                    }
                }
                if (weakness == true) //虚弱状态：AD和DEF基数下降20%
                {
                    if (isWeaknees == false)
                    {
                        AD = ADweak;
                        DEF = DEFweak;
                        isWeaknees = true;
                        weaknessG2 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_weakness));
                        weaknessG2.transform.position = gameObject.transform.position;
                        weaknessG2.transform.parent = transform;
                        weaknessG2.name = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_weakness).name;
                    }
                }
                else
                {
                    if (isWeaknees == true)
                    {
                        AD = ADbase;
                        DEF = DEFbase;
                        ObjectPoolManager.Instance.RecycleMyGameObject(weaknessG2);
                        isWeaknees = false;
                    }
                }
                if (fear == true) //恐惧状态：AP和RES基数下降20%
                {
                    if (isFear == false)
                    {
                        AP = APweak;
                        RES = RESweak;
                        isFear = true;
                        fearG2 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_fear));
                        fearG2.transform.position = gameObject.transform.position;
                        fearG2.transform.parent = transform;
                        fearG2.name = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_fear).name;
                    }

                }
                else
                {
                    if (isFear == true)
                    {
                        AP = APbase;
                        RES = RESbase;
                        ObjectPoolManager.Instance.RecycleMyGameObject(fearG2);
                        isFear = false;
                    }
                }
                if (burn == true) //燃烧状态：每秒损失5%生命值
                {
                    tempHPA += maxHP * 0.01f;
                    if (tempHPA >= 1.0f)
                    {
                        currentHP -= (int)tempHPA;
                        tempHPA -= (int)tempHPA;
                    }
                    if (isBurning == false)
                    {
                        isBurning = true;
                        burnG2 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_fire));
                        burnG2.transform.position = gameObject.transform.position;
                        burnG2.transform.parent = transform;
                        burnG2.name = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_fire).name;
                    }
                }
                else
                {
                    if (isBurning == true)
                    {
                        ObjectPoolManager.Instance.RecycleMyGameObject(burnG2);
                        isBurning = false;
                    }
                }
                if (bleed == true) //流血状态：每秒损失8%生命值
                {
                    tempHPB += maxHP * 0.016f;
                    if (tempHPB >= 1.0f)
                    {
                        currentHP -= (int)tempHPB;
                        tempHPB -= (int)tempHPB;
                    }
                    if (isBleeding == false)
                    {
                        isBleeding = true;
                        bleedG2 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_bleed));
                        bleedG2.transform.position = gameObject.transform.position;
                        bleedG2.transform.parent = transform;
                        bleedG2.name = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_bleed).name;
                    }
                }
                else
                {
                    if (isBleeding == true)
                    {
                        ObjectPoolManager.Instance.RecycleMyGameObject(bleedG2);
                        isBleeding = false;
                    }
                }
                if (inspire == true) //振奋状态：AD增长20%
                {
                    if (isInspire == false)
                    {
                        isInspire = true;
                        currentAD = (int)(AD + AD * 0.2f);
                        inspireG2 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_inspire));
                        inspireG2.transform.position = gameObject.transform.position + new Vector3(0, 1, 0);
                        inspireG2.transform.parent = transform;
                        inspireG2.name = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_inspire).name;
                    }
                }
                else
                {
                    if (isInspire == true)
                    {
                        currentAD = AD;
                        ObjectPoolManager.Instance.RecycleMyGameObject(inspireG2);
                        isInspire = false;
                    }
                }
                if (muse == true) //冥想状态：AP增长20%
                {
                    if (isMuse == false)
                    {
                        isMuse = true;
                        currentAP = (int)(AP + AP * 0.2f);
                        museG2 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_muse));
                        museG2.transform.position = gameObject.transform.position + new Vector3(0, 1, 0);
                        museG2.transform.parent = transform;
                        museG2.name = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_muse).name;
                    }
                }
                else
                {
                    if (isMuse == true)
                    {
                        currentAP = AP;
                        ObjectPoolManager.Instance.RecycleMyGameObject(museG2);
                        isMuse = false;
                    }
                }
                if (harden == true) //硬化状态：DEF增长5点
                {
                    if (isHarden == false)
                    {
                        currentDEF = DEF + 5;
                        isHarden = true;
                        hardenG2 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_harden));
                        hardenG2.transform.position = gameObject.transform.position + new Vector3(0, 1, 0);
                        hardenG2.transform.parent = transform;
                        hardenG2.name = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_harden).name;
                    }
                }
                else
                {
                    if (isHarden == true)
                    {
                        currentDEF = DEF;
                        ObjectPoolManager.Instance.RecycleMyGameObject(hardenG2);
                        isHarden = false;
                    }
                }
                if (magicShield == true) //魔法盾状态：RES增长5点
                {
                    if (isMagicShield == false)
                    {
                        currentRES = RES + 5;
                        isMagicShield = true;
                        magicShieldG2 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_magicShied));
                        magicShieldG2.transform.position = gameObject.transform.position + new Vector3(0, 1, 0);
                        magicShieldG2.transform.parent = transform;
                        magicShieldG2.name = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_magicShied).name;
                    }
                }
                else
                {
                    if (isMagicShield == true)
                    {
                        currentRES = RES;
                        ObjectPoolManager.Instance.RecycleMyGameObject(magicShieldG2);
                        isMagicShield = false;
                    }
                }
            }
            //二级判断：如果无敌开关为开，则持续清空所有其他状态
            else if (god == true)
            {
                StatesClearEnemy();
                //三级判断：刚开启时，生成无敌特效，并且让英雄处于无敌中
                if (isGod == false)
                {
                    godG2 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Knight02_Aegis));
                    godG2.transform.position = gameObject.transform.position + new Vector3(0, 1, 0);
                    godG2.transform.parent = transform;
                    godG2.name = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Knight02_Aegis).name;
                    isGod = true;
                }
            }
        }
        
    }

    //清空敌人身上无敌以外所有状态
    internal void StatesClearEnemy()
    {
        transform.GetComponent<EnemyControllers>().isDiz = false;
        transform.GetComponent<EnemyControllers>().isSilence = false;
        burn = false;
        bleed = false;
        inspire = false;
        muse = false;
        harden = false;
        magicShield = false;
        weakness = false;
        fear = false;
    }

    /// <summary>
    /// 获得状态的调用
    /// </summary>
    /// <param 状态ID="stateID"></param>
    /// <param 持续时间="keepTime"></param>
    internal void GetState(int stateID, float keepTime)
    {
        switch (stateID)
        {
            case 3201://眩晕
                transform.GetComponent<EnemyControllers>().isDiz = true;
                vp_Timer.In(keepTime, new vp_Timer.Callback(delegate ()
                {
                    if (SceneManager.GetActiveScene().name != "LoadingScene")
                    {
                        transform.GetComponent<EnemyControllers>().isDiz = false;
                    }
                }));
                break;
            case 3202://燃烧
                burn = true;
                vp_Timer.In(keepTime, new vp_Timer.Callback(delegate () { burn = false; }));
                break;
            case 3203://流血
                bleed = true;
                vp_Timer.In(keepTime, new vp_Timer.Callback(delegate () { bleed = false; }));
                break;
            case 3204://沉默
                transform.GetComponent<EnemyControllers>().isSilence = true;
                vp_Timer.In(keepTime, new vp_Timer.Callback(delegate ()
                {
                    if (transform.GetComponent<EnemyControllers>() != null)
                    {
                        transform.GetComponent<EnemyControllers>().isSilence = false;
                    }
                }));
                break;
            case 3207://振奋
                inspire = true;
                vp_Timer.In(keepTime, new vp_Timer.Callback(delegate () { inspire = false; }));
                break;
            case 3208://冥想
                muse = true;
                vp_Timer.In(keepTime, new vp_Timer.Callback(delegate () { muse = false; }));
                break;
            case 3209://硬化
                harden = true;
                vp_Timer.In(keepTime, new vp_Timer.Callback(delegate () { harden = false; }));
                break;
            case 3210://魔法盾
                magicShield = true;
                vp_Timer.In(keepTime, new vp_Timer.Callback(delegate () { magicShield = false; }));
                break;
            case 3211://无敌
                god = true;
                vp_Timer.In(keepTime, new vp_Timer.Callback(delegate () { god = false; }));
                break;
            case 3212://虚弱
                weakness = true;
                vp_Timer.In(keepTime, new vp_Timer.Callback(delegate () { weakness = false; }));
                break;
            case 3213://恐惧
                fear = true;
                vp_Timer.In(keepTime, new vp_Timer.Callback(delegate () { fear = false; }));
                break;
        }
    }
}
