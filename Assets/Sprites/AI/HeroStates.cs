using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStates : MonoBehaviour
{
    //英雄状态机
    internal Animator myanim;
    //英雄原始数据
    internal HeroData mydata;
    //英雄触发器
    internal CircleCollider2D mycld;

    #region 基本属性和流属性
    //英雄HP上限
    internal int maxHP;
    //英雄状态专用暂存HP
    float tempHPA = 0;
    float tempHPB = 0;
    //英雄动态HP
    internal int currentHP;
    //英雄基础AD
    int ADbase;
    //英雄常规下AD
    internal int AD;
    //英雄削弱后AD
    int ADweak;
    //英雄动态AD
    internal int currentAD;
    //英雄基础AP
    int APbase;
    //英雄常规下AP
    internal int AP;
    //英雄削弱后AP
    int APweak;
    //英雄动态AP
    internal int currentAP;
    //英雄基础物防
    int DEFbase;
    //英雄常规下物防
    internal int DEF;
    //英雄削弱后物防
    int DEFweak;
    //英雄动态物防
    internal int currentDEF;
    //英雄基础魔防
    int RESbase;
    //英雄常规下魔防
    internal int RES;
    //英雄削弱后魔防
    int RESweak;
    //英雄动态魔防
    internal int currentRES;
    #endregion

    #region 状态动画开关
    //是否正在被眩晕
    bool isDizing = false;
    //是否正在被沉默
    bool isSilenceing = false;
    //是否正在燃烧状态
    bool isBurning = false;
    //是否正在流血状态
    bool isBleeding = false;
    //是否正在振奋状态
    bool isInspire = false;
    //是否正在冥想状态
    bool isMuse = false;
    //是否正在硬化状态
    bool isHarden = false;
    //是否正在魔法盾状态
    bool isMagicShield = false;
    //是否正在无敌状态
    bool isGod = false;
    //是否正在虚弱状态
    bool isWeaknees = false;
    //是否正在恐惧状态
    bool isFear = false;
    #endregion

    #region 状态开关
    //燃烧开关
    internal bool burn = false;
    //流血开关
    internal bool bleed = false;
    //振奋开关
    internal bool inspire = false;
    //冥想开关
    internal bool muse = false;
    //硬化开关
    internal bool harden = false;
    //魔法盾开关
    internal bool magicShield = false;
    //无敌开关
    internal bool god = false;
    //虚弱开关
    internal bool weakness = false;
    //恐惧开关
    internal bool fear = false;
    //英雄的职业
    string myClass;
    #endregion

    #region 生成的特效对象
    GameObject dizG;
    GameObject silenceG;
    GameObject burnG;
    GameObject bleedG;
    GameObject inspireG;
    GameObject museG;
    GameObject hardenG;
    GameObject magicShieldG;
    GameObject godG;
    GameObject weaknessG;
    GameObject fearG;
    #endregion

    private void Awake()
    {
        //获取英雄的职业类型
        myClass = SQLiteManager.Instance.characterDataSource[int.Parse(gameObject.name)].character_Class;
        //获取英雄的原始数据字典
        mydata = SQLiteManager.Instance.team[myClass];
        //获取英雄状态机
        myanim = transform.GetComponent<Animator>();
        //获取触发器
        mycld = transform.GetComponent<CircleCollider2D>();
        //以下均为初始化各项数值（从字典中读取）
        maxHP = mydata.totalHP;
        currentHP = maxHP;
        ADbase = mydata.totalAD;
        AD = ADbase;
        ADweak = (int)(AD - AD * 0.2f);
        currentAD = AD;
        APbase = mydata.totalAP;
        AP = APbase;
        APweak = (int)(AP - AP * 0.2f);
        currentAP = AP;
        DEFbase = mydata.totalDEF;
        DEF = DEFbase;
        DEFweak = (int)(DEF - DEF * 0.2f);
        currentDEF = DEF;
        RESbase = mydata.totalRES;
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

        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(false);
        }
        //重开重力
        //transform.GetComponent<Rigidbody2D>().gravityScale = 1;
        //重开触发器
        mycld.enabled = true;
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
        //一级判断：英雄是否还活着，活着才能进行以下所有判断
        if (transform.GetComponent<HeroController>().isAlive == true)
        {
            //二级判断：英雄HP是否已经没了，决定是否关闭存活开关和跳出判断，只要不被复活，再也无法继续后续判断
            if (currentHP <= 0 && transform.GetComponent<HeroController>().alive == true)
            {
                mycld.enabled = false;
                myanim.SetTrigger("Dead");
                transform.position -= new Vector3(0.3f, 0, 0);
                //播放死亡音效
                AudioManager.Instance.PlayEffectMusic(SoundEffect.Die);
                transform.GetComponent<HeroController>().isAlive = false;
                transform.GetComponent<HeroController>().alive = false;
                //死亡列表增加
                UIBattle.deadCharaList.Add(gameObject);
                StatesClear();
                return;
            }
            //二级判断：如果已经被复活了，进入这里时会重新激活是否正在存活（alive）
            if(transform.GetComponent<HeroController>().alive == false)
            {
                myanim.SetTrigger("Reset");
                transform.GetComponent<HeroController>().alive = true;
            }
            //二级判断：玩家是否处于无敌状态，如果否，才能进行接下来的各种BUFF状态监测
            if (god == false)
            {
                //三级判断：如果状态为正在无敌，修正为没有在无敌，并且回收无敌特效预制体
                if (isGod == true)
                {
                    ObjectPoolManager.Instance.RecycleMyGameObject(godG);
                    isGod = false;
                }
                if (transform.GetComponent<HeroController>().isDiz == true) //眩晕状态：无法行动
                {
                    if (isDizing == false)
                    {
                        myanim.SetBool("isDiz", true);
                        isDizing = true;
                        dizG = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_diz));
                        dizG.transform.position = gameObject.transform.position + new Vector3(0, 1, 0);
                        dizG.transform.parent = transform;
                        dizG.name = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_diz).name;
                    }
                }
                else
                {
                    if (isDizing == true)
                    {
                        myanim.SetBool("isDiz", false);
                        isDizing = false;
                        ObjectPoolManager.Instance.RecycleMyGameObject(dizG);
                    }
                }
                if (transform.GetComponent<HeroController>().isSilence == true) //沉默状态：无法释放技能
                {
                    if (isSilenceing == false)
                    {
                        isSilenceing = true;
                        silenceG = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_silence));
                        silenceG.transform.position = gameObject.transform.position;
                        silenceG.transform.parent = transform;
                        silenceG.name = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_silence).name;
                    }
                }
                else
                {
                    if (isSilenceing == true)
                    {
                        ObjectPoolManager.Instance.RecycleMyGameObject(silenceG);
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
                        weaknessG = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_weakness));
                        weaknessG.transform.position = gameObject.transform.position;
                        weaknessG.transform.parent = transform;
                        weaknessG.name = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_weakness).name;
                    }
                }
                else
                {
                    if (isWeaknees == true)
                    {
                        AD = ADbase;
                        DEF = DEFbase;
                        ObjectPoolManager.Instance.RecycleMyGameObject(weaknessG);
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
                        fearG = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_fear));
                        fearG.transform.position = gameObject.transform.position;
                        fearG.transform.parent = transform;
                        fearG.name = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_fear).name;
                    }

                }
                else
                {
                    if (isFear == true)
                    {
                        AP = APbase;
                        RES = RESbase;
                        ObjectPoolManager.Instance.RecycleMyGameObject(fearG);
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
                        burnG = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_fire));
                        burnG.transform.position = gameObject.transform.position;
                        burnG.transform.parent = transform;
                        burnG.name = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_fire).name;
                    }
                }
                else
                {
                    if (isBurning == true)
                    {
                        ObjectPoolManager.Instance.RecycleMyGameObject(burnG);
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
                        bleedG = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_bleed));
                        bleedG.transform.position = gameObject.transform.position;
                        bleedG.transform.parent = transform;
                        bleedG.name = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_bleed).name;
                    }
                }
                else
                {
                    if (isBleeding == true)
                    {
                        ObjectPoolManager.Instance.RecycleMyGameObject(bleedG);
                        isBleeding = false;
                    }
                }
                if (inspire == true) //振奋状态：AD增长20%
                {
                    if (isInspire == false)
                    {
                        isInspire = true;
                        currentAD = (int)(AD + AD * 0.2f);
                        inspireG = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_inspire));
                        inspireG.transform.position = gameObject.transform.position + new Vector3(0, 1, 0);
                        inspireG.transform.parent = transform;
                        inspireG.name = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_inspire).name;
                    }
                }
                else
                {
                    if (isInspire == true)
                    {
                        currentAD = AD;
                        ObjectPoolManager.Instance.RecycleMyGameObject(inspireG);
                        isInspire = false;
                    }
                }
                if (muse == true) //冥想状态：AP增长20%
                {
                    if (isMuse == false)
                    {
                        isMuse = true;
                        currentAP = (int)(AP + AP * 0.2f);
                        museG = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_muse));
                        museG.transform.position = gameObject.transform.position + new Vector3(0, 1, 0);
                        museG.transform.parent = transform;
                        museG.name = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_muse).name;
                    }
                }
                else
                {
                    if (isMuse == true)
                    {
                        currentAP = AP;
                        ObjectPoolManager.Instance.RecycleMyGameObject(museG);
                        isMuse = false;
                    }
                }
                if (harden == true) //硬化状态：DEF增长5点
                {
                    if (isHarden == false)
                    {
                        currentDEF = DEF + 5;
                        isHarden = true;
                        hardenG = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_harden));
                        hardenG.transform.position = gameObject.transform.position + new Vector3(0, 1, 0);
                        hardenG.transform.parent = transform;
                        hardenG.name = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_harden).name;
                    }
                }
                else
                {
                    if (isHarden == true)
                    {
                        currentDEF = DEF;
                        ObjectPoolManager.Instance.RecycleMyGameObject(hardenG);
                        isHarden = false;
                    }
                }
                if (magicShield == true) //魔法盾状态：RES增长5点
                {
                    if (isMagicShield == false)
                    {
                        currentRES = RES + 5;
                        isMagicShield = true;
                        magicShieldG = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_magicShied));
                        magicShieldG.transform.position = gameObject.transform.position + new Vector3(0, 1, 0);
                        magicShieldG.transform.parent = transform;
                        magicShieldG.name = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_magicShied).name;
                    }
                }
                else
                {
                    if (isMagicShield == true)
                    {
                        currentRES = RES;
                        ObjectPoolManager.Instance.RecycleMyGameObject(magicShieldG);
                        isMagicShield = false;
                    }
                }
            }
            //二级判断：如果无敌开关为开，则持续清空所有其他状态
            else if (god == true)
            {
                StatesClear();
                //三级判断：刚开启时，生成无敌特效，并且让英雄处于无敌中
                if (isGod == false)
                {
                    godG = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Knight02_Aegis));
                    godG.transform.position = gameObject.transform.position + new Vector3(0, 1, 0);
                    godG.transform.parent = transform;
                    godG.name = ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Knight02_Aegis).name;
                    isGod = true;
                }
            }
        }
    }

    //清空英雄身上无敌以外所有状态
    void StatesClear()
    {
        transform.GetComponent<HeroController>().isDiz = false;
        transform.GetComponent<HeroController>().isSilence = false;
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
        switch(stateID)
        {
            case 3201://眩晕
                transform.GetComponent<HeroController>().isDiz = true;
                vp_Timer.In(keepTime, new vp_Timer.Callback(delegate () { transform.GetComponent<HeroController>().isDiz = false; }));
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
                transform.GetComponent<HeroController>().isSilence = true;
                vp_Timer.In(keepTime, new vp_Timer.Callback(delegate () { transform.GetComponent<HeroController>().isSilence = false; }));
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
