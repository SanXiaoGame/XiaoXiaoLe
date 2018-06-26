using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Profession
{
    Saber = 1301,
    Knight=1302,
    Caster=1304,
    Berserker=1303,
    Hunter=1305
}

public class Test : MonoBehaviour
{

    /*
     * 在test类脚本中,首先构建出两个玩家模型,带有数据,并加入hero字典
     * 在玩家英雄脚本上获取该字典中对应ID的英雄数据,
     * 通过按键条件,测试切换状态
    */
    PlayerData playerData;
    HeroData hero;
    //StateData stateData;
    SkillData skillData;
    private void Awake()
    {
        //InitData();
        //Init();
    }
    private void Start()
    {
        StartCoroutine("Wait");
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        AddTeam();
        Debug.Log("team字典的元素个数:" + SQLiteManager.Instance.team.Count);

    }
    private void Update()
    {
        TestSkill();
    }
    #region void TestSkill() 按键测试
    /// <summary>
    /// 按键测试功能,不同按钮实现切换不同玩家的不同状态
    /// </summary>
    void TestSkill()
    {
        //根据字典中获取英雄名来设置英雄当前的状态. 
        if (Input.GetKeyDown(KeyCode.I))
        {
            //改变玩家英雄的状态
            //SkillsManager.Instance.ChangeHerosIdle();

            //SkillsManager.Instance.ChangeHeroState(Profession.Knight.GetHashCode(), HeroState.idle.GetHashCode());
            //SkillsManager.Instance.ChangeHeroState(Profession.Caster.GetHashCode(), HeroState.idle.GetHashCode());
            //SkillsManager.Instance.ChangeHeroState(Profession.Berserker.GetHashCode(), HeroState.idle.GetHashCode());
            //SkillsManager.Instance.ChangeHeroState(Profession.Hunter.GetHashCode(), HeroState.idle.GetHashCode());
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //改变玩家英雄的状态
            //SkillsManager.Instance.ChangeHeroState(Profession.Saber.GetHashCode(), HeroState.move.GetHashCode());
            //SkillsManager.Instance.ChangeHeroState(Profession.Knight.GetHashCode(), HeroState.move.GetHashCode());
            //SkillsManager.Instance.ChangeHeroState(Profession.Caster.GetHashCode(), HeroState.move.GetHashCode());
            //SkillsManager.Instance.ChangeHeroState(Profession.Berserker.GetHashCode(), HeroState.move.GetHashCode());
            //SkillsManager.Instance.ChangeHeroState(Profession.Hunter.GetHashCode(), HeroState.move.GetHashCode());

            //SkillsManager.Instance.ChangeHerosRun();

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ////改变玩家英雄的状态
            //SkillsManager.Instance.ChangeHeroState(Profession.Saber.GetHashCode(), HeroState.diz.GetHashCode());
            //SkillsManager.Instance.ChangeHeroState(Profession.Knight.GetHashCode(), HeroState.diz.GetHashCode());
            //SkillsManager.Instance.ChangeHeroState(Profession.Caster.GetHashCode(), HeroState.diz.GetHashCode());
            //SkillsManager.Instance.ChangeHeroState(Profession.Berserker.GetHashCode(), HeroState.diz.GetHashCode());
            //SkillsManager.Instance.ChangeHeroState(Profession.Hunter.GetHashCode(), HeroState.diz.GetHashCode());

            //SkillsManager.Instance.ChangeHerosDiz();

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //改变玩家英雄的状态
            //SkillsManager.Instance.ChangeHeroState(Profession.Saber.GetHashCode(), HeroState.win.GetHashCode());
            //SkillsManager.Instance.ChangeHeroState(Profession.Knight.GetHashCode(), HeroState.win.GetHashCode());
            //SkillsManager.Instance.ChangeHeroState(Profession.Caster.GetHashCode(), HeroState.win.GetHashCode());
            //SkillsManager.Instance.ChangeHeroState(Profession.Berserker.GetHashCode(), HeroState.win.GetHashCode());
            //SkillsManager.Instance.ChangeHeroState(Profession.Hunter.GetHashCode(), HeroState.win.GetHashCode());

            //SkillsManager.Instance.ChangeHerosWin();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //改变玩家英雄的状态
            SkillsManager.Instance.ChangeHeroState(Profession.Saber.GetHashCode(), ActionType.ActionEnum.Dead.ToString());
            SkillsManager.Instance.ChangeHeroState(Profession.Knight.GetHashCode(), ActionType.ActionEnum.Dead.ToString());
            SkillsManager.Instance.ChangeHeroState(Profession.Caster.GetHashCode(), ActionType.ActionEnum.Dead.ToString());
            SkillsManager.Instance.ChangeHeroState(Profession.Berserker.GetHashCode(), ActionType.ActionEnum.Dead.ToString());
            SkillsManager.Instance.ChangeHeroState(Profession.Hunter.GetHashCode(), ActionType.ActionEnum.Dead.ToString());
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            //改变玩家英雄的状态
            //SkillsManager.Instance.ChangeHeroState(Profession.Saber.GetHashCode(), HeroState.commonAttack.GetHashCode());
            //SkillsManager.Instance.ChangeHeroState(Profession.Knight.GetHashCode(), HeroState.commonAttack.GetHashCode());
            //SkillsManager.Instance.ChangeHeroState(Profession.Caster.GetHashCode(), HeroState.commonAttack.GetHashCode());
            //SkillsManager.Instance.ChangeHeroState(Profession.Berserker.GetHashCode(), HeroState.commonAttack.GetHashCode());
            //SkillsManager.Instance.ChangeHeroState(Profession.Hunter.GetHashCode(), HeroState.commonAttack.GetHashCode());

            //SkillsManager.Instance.ChangeHerosCommonAttack();
        }
        //-----------------------------剑士
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //改变玩家英雄的状态
            //SkillsManager.Instance.ChangeHeroState(Profession.Saber.GetHashCode(), HeroState.saberOneSkill.GetHashCode());

            SkillsManager.Instance.ChangeHeroState(Profession.Saber.GetHashCode(), ActionType.ActionEnum.SaberOneSkill.ToString());
            //Debug.Log("剑士一技能名:" + ActionType.ActionEnum.SaberOneSkill);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //改变玩家英雄的状态
            //SkillsManager.Instance.ChangeHeroState(Profession.Saber.GetHashCode(), HeroState.saberTwoSkill.GetHashCode());
            SkillsManager.Instance.ChangeHeroState(Profession.Saber.GetHashCode(), ActionType.ActionEnum.SaberTwoSkill.ToString());
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //改变玩家英雄的状态
            //SkillsManager.Instance.ChangeHeroState(Profession.Saber.GetHashCode(), HeroState.saberThreeSkill.GetHashCode());
            SkillsManager.Instance.ChangeHeroState(Profession.Saber.GetHashCode(), ActionType.ActionEnum.SaberThreeSkill.ToString());
        }
        //-----------------------------骑士
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //改变玩家英雄的状态
            SkillsManager.Instance.ChangeHeroState(Profession.Knight.GetHashCode(), ActionType.ActionEnum.KnightOneSkill.ToString());
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            //改变玩家英雄的状态
            SkillsManager.Instance.ChangeHeroState(Profession.Knight.GetHashCode(), ActionType.ActionEnum.KnightTwoSkill.ToString());
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            //改变玩家英雄的状态
            SkillsManager.Instance.ChangeHeroState(Profession.Knight.GetHashCode(), ActionType.ActionEnum.KnightThreeSkill.ToString());
        }
        //-----------------------------法师
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            //改变玩家英雄的状态
            SkillsManager.Instance.ChangeHeroState(Profession.Caster.GetHashCode(), ActionType.ActionEnum.CasterOneSkill.ToString());
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            //改变玩家英雄的状态
            SkillsManager.Instance.ChangeHeroState(Profession.Caster.GetHashCode(), ActionType.ActionEnum.CasterTwoSkill.ToString());
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            //改变玩家英雄的状态
            SkillsManager.Instance.ChangeHeroState(Profession.Caster.GetHashCode(), ActionType.ActionEnum.CasterThreeSkill.ToString());
        }
        //-----------------------------狂战士
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //改变玩家英雄的状态
            SkillsManager.Instance.ChangeHeroState(Profession.Berserker.GetHashCode(), ActionType.ActionEnum.BerserkerOneSkill.ToString());
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            //改变玩家英雄的状态
            SkillsManager.Instance.ChangeHeroState(Profession.Berserker.GetHashCode(), ActionType.ActionEnum.BerserkerTwoSkill.ToString());
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            //改变玩家英雄的状态
            SkillsManager.Instance.ChangeHeroState(Profession.Berserker.GetHashCode(), ActionType.ActionEnum.BerserkerThreeSkill.ToString());
        }
        //---------------------------------猎人
        if (Input.GetKeyDown(KeyCode.V))
        {
            //改变玩家英雄的状态
            SkillsManager.Instance.ChangeHeroState(Profession.Hunter.GetHashCode(), ActionType.ActionEnum.HunterOneSkill.ToString());
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            //改变玩家英雄的状态
            SkillsManager.Instance.ChangeHeroState(Profession.Hunter.GetHashCode(), ActionType.ActionEnum.HunterTwoSkill.ToString());
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            //改变玩家英雄的状态
            SkillsManager.Instance.ChangeHeroState(Profession.Hunter.GetHashCode(), ActionType.ActionEnum.HunterThreeSkill.ToString());
        }
   
    }
    #endregion

    #region void InitData() 构建状态数据,加入到hero字典中
    /// <summary>
    /// 装载玩家英雄技能字典和状态字典
    /// </summary>
    void InitData()
    {
        #region 构建玩家英雄状态
        StateData stateData = new StateData
        {
            StateID = 3251,
            state_Name = "Idle",
            state_Type = "常规",
            state_Value = 0.1f,
            state_KeepTime = 0.1f,
            state_Description = "这个状态是等待状态"
        };
        SQLiteManager.Instance.stateDataSource.Add(stateData.StateID, stateData);

        stateData = new StateData
        {
            StateID = 3252,
            state_Name = "Await",

            state_Type = "常规",
            state_Value = 0.1f,
            state_KeepTime = 0.1f,
            state_Description = "这个状态是非攻击等待状态"
        };
        SQLiteManager.Instance.stateDataSource.Add(stateData.StateID, stateData);

        stateData = new StateData
        {
            StateID = 3253,
            state_Name = "Move",
            state_Type = "常规",
            state_Value = 0.1f,
            state_KeepTime = 0.1f,
            state_Description = "这个状态是英雄跑的状态"
        };
        SQLiteManager.Instance.stateDataSource.Add(stateData.StateID, stateData);

        stateData = new StateData
        {
            StateID = 3254,
            state_Name = "Diz",
            state_Type = "常规",
            state_Value = 0.1f,
            state_KeepTime = 0.1f,
            state_Description = "这个状态眩晕状态"
        };
        SQLiteManager.Instance.stateDataSource.Add(stateData.StateID, stateData);

        stateData = new StateData
        {
            StateID = 3255,
            state_Name = "Win",
            state_Type = "常规",
            state_Value = 0.1f,
            state_KeepTime = 0.1f,
            state_Description = "这个状态是胜利状态"
        };
        SQLiteManager.Instance.stateDataSource.Add(stateData.StateID, stateData);

        stateData = new StateData
        {
            StateID = 3256,
            state_Name = "Dead",
            state_Type = "常规",
            state_Value = 0.1f,
            state_KeepTime = 0.1f,
            state_Description = "这个状态是死亡状态"
        };
        SQLiteManager.Instance.stateDataSource.Add(stateData.StateID, stateData);

        stateData = new StateData
        {
            StateID = 3257,
            state_Name = "CommonAttack",
            state_Type = "常规",
            state_Value = 0.1f,
            state_KeepTime = 0.1f,
            state_Description = "这个状态是普通攻击状态"
        };
        SQLiteManager.Instance.stateDataSource.Add(stateData.StateID, stateData);

        stateData = new StateData
        {
            StateID = 3258,
            state_Name = "SaberOneSkill",
            state_Type = "常规",
            state_Value = 0.1f,
            state_KeepTime = 0.1f,
            state_Description = "这个状态是剑士一技能状态"
        };
        SQLiteManager.Instance.stateDataSource.Add(stateData.StateID, stateData);

        stateData = new StateData
        {
            StateID = 3259,
            state_Name = "SaberTwoSkill",
            state_Type = "常规",
            state_Value = 0.1f,
            state_KeepTime = 0.1f,
            state_Description = "这个状态是剑士二技能状态"
        };
        SQLiteManager.Instance.stateDataSource.Add(stateData.StateID, stateData);

        stateData = new StateData
        {
            StateID = 3260,
            state_Name = "SaberThreeSkill",
            state_Type = "常规",
            state_Value = 0.1f,
            state_KeepTime = 0.1f,
            state_Description = "这个状态是剑士三技能状态"
        };
        SQLiteManager.Instance.stateDataSource.Add(stateData.StateID, stateData);

        stateData = new StateData
        {
            StateID = 3261,
            state_Name = "KnightOneSkill",
            state_Type = "常规",
            state_Value = 0.1f,
            state_KeepTime = 0.1f,
            state_Description = "这个状态是骑士一技能状态"
        };
        SQLiteManager.Instance.stateDataSource.Add(stateData.StateID, stateData);

        stateData = new StateData
        {
            StateID = 3262,
            state_Name = "KnightTwoSkill",
            state_Type = "常规",
            state_Value = 0.1f,
            state_KeepTime = 0.1f,
            state_Description = "这个状态是骑士二技能状态"
        };
        SQLiteManager.Instance.stateDataSource.Add(stateData.StateID, stateData);

        stateData = new StateData
        {
            StateID = 3263,
            state_Name = "KnightThreeSkill",
            state_Type = "常规",
            state_Value = 0.1f,
            state_KeepTime = 0.1f,
            state_Description = "这个状态是骑士三技能状态"
        };
        SQLiteManager.Instance.stateDataSource.Add(stateData.StateID, stateData);

        stateData = new StateData
        {
            StateID = 3264,
            state_Name = "CasterOneSkill",
            state_Type = "常规",
            state_Value = 0.1f,
            state_KeepTime = 0.1f,
            state_Description = "这个状态是法师一技能状态"
        };
        SQLiteManager.Instance.stateDataSource.Add(stateData.StateID, stateData);

        stateData = new StateData
        {
            StateID = 3265,
            state_Name = "CasterTwoSkill",
            state_Type = "常规",
            state_Value = 0.1f,
            state_KeepTime = 0.1f,
            state_Description = "这个状态是法师二技能状态"
        };
        SQLiteManager.Instance.stateDataSource.Add(stateData.StateID, stateData);

        stateData = new StateData
        {
            StateID = 3266,
            state_Name = "CasterThreeSkill",
            state_Type = "常规",
            state_Value = 0.1f,
            state_KeepTime = 0.1f,
            state_Description = "这个状态是法师三技能状态"
        };
        SQLiteManager.Instance.stateDataSource.Add(stateData.StateID, stateData);

        stateData = new StateData
        {
            StateID = 3267,
            state_Name = "BerserkerOneSkill",
            state_Type = "常规",
            state_Value = 0.1f,
            state_KeepTime = 0.1f,
            state_Description = "这个状态是战士一技能状态"
        };
        SQLiteManager.Instance.stateDataSource.Add(stateData.StateID, stateData);

        stateData = new StateData
        {
            StateID = 3268,
            state_Name = "BerserkerTwoSkill",
            state_Type = "常规",
            state_Value = 0.1f,
            state_KeepTime = 0.1f,
            state_Description = "这个状态是战士二技能状态"
        };
        SQLiteManager.Instance.stateDataSource.Add(stateData.StateID, stateData);


        stateData = new StateData
        {
            StateID = 3269,
            state_Name = "BerserkerThreeSkill",
            state_Type = "常规",
            state_Value = 0.1f,
            state_KeepTime = 0.1f,
            state_Description = "这个状态是战士三技能状态"
        };
        SQLiteManager.Instance.stateDataSource.Add(stateData.StateID, stateData);


        stateData = new StateData
        {
            StateID = 3270,
            state_Name = "HunterOneSkill",
            state_Type = "常规",
            state_Value = 0.1f,
            state_KeepTime = 0.1f,
            state_Description = "这个状态是猎人一技能状态"
        };
        SQLiteManager.Instance.stateDataSource.Add(stateData.StateID, stateData);


        stateData = new StateData
        {
            StateID = 3271,
            state_Name = "HunterTwoSkill",
            state_Type = "常规",
            state_Value = 0.1f,
            state_KeepTime = 0.1f,
            state_Description = "这个状态是猎人二技能状态"
        };
        SQLiteManager.Instance.stateDataSource.Add(stateData.StateID, stateData);

        stateData = new StateData
        {
            StateID = 3272,
            state_Name = "HunterThreeSkill",
            state_Type = "常规",
            state_Value = 0.1f,
            state_KeepTime = 0.1f,
            state_Description = "这个状态是猎人三技能状态"
        };
        SQLiteManager.Instance.stateDataSource.Add(stateData.StateID, stateData);

        Debug.Log("state字典的元素个数:" + SQLiteManager.Instance.stateDataSource.Count);

        //没新建一个state都要用new来创建,此时创建的两个state是不同的(指针地址不同)所以才能创建出不同的状态
        #endregion

        #region 构建玩家英雄技能
        //------------------------------------------------------//

        #region 剑士技能列表
        skillData = new SkillData
        {
            skill_ID = 3001,
            skill_Name = "突刺",
            skill_Type = "物理",
            skill_DamageLevel = 1,
            skill_AddStateID1 = 0,
            skill_AddStateID2 = 3002
        };
        SQLiteManager.Instance.skillDataSource.Add(skillData.skill_ID, skillData);

        skillData = new SkillData
        {
            skill_ID = 3002,
            skill_Name = "剑气",
            skill_Type = "物理",
            skill_DamageLevel = 2,
            skill_AddStateID1 = 0,
            skill_AddStateID2 = 3003
        };
        SQLiteManager.Instance.skillDataSource.Add(skillData.skill_ID, skillData);


        skillData = new SkillData
        {
            skill_ID = 3003,
            skill_Name = "六剑连斩",
            skill_Type = "物理",
            skill_DamageLevel = 3,
            skill_AddStateID1 = 0,
            skill_AddStateID2 = 3001
        };
        SQLiteManager.Instance.skillDataSource.Add(skillData.skill_ID, skillData);
        #endregion

        #region 骑士技能列表
        skillData = new SkillData
        {
            skill_ID = 3104,
            skill_Name = "信仰",
            skill_Type = "魔法",
            skill_DamageLevel = 1,
            skill_AddStateID1 = 0,
            skill_AddStateID2 = 3002
        };
        SQLiteManager.Instance.skillDataSource.Add(skillData.skill_ID, skillData);

        skillData = new SkillData
        {
            skill_ID = 3105,
            skill_Name = "圣盾",
            skill_Type = "魔法",
            skill_DamageLevel = 2,
            skill_AddStateID1 = 0,
            skill_AddStateID2 = 3003
        };
        SQLiteManager.Instance.skillDataSource.Add(skillData.skill_ID, skillData);

        skillData = new SkillData
        {
            skill_ID = 3106,
            skill_Name = "圣矛投掷",
            skill_Type = "魔法",
            skill_DamageLevel = 3,
            skill_AddStateID1 = 0,
            skill_AddStateID2 = 3001
        };
        SQLiteManager.Instance.skillDataSource.Add(skillData.skill_ID, skillData);
        #endregion

        #region 法师技能列表
        skillData = new SkillData
        {
            skill_ID = 3101,
            skill_Name = "冰冻术",
            skill_Type = "魔法",
            skill_DamageLevel = 1,
            skill_AddStateID1 = 0,
            skill_AddStateID2 = 3002
        };
        SQLiteManager.Instance.skillDataSource.Add(skillData.skill_ID, skillData);

        skillData = new SkillData
        {
            skill_ID = 3102,
            skill_Name = "陨石术",
            skill_Type = "魔法",
            skill_DamageLevel = 2,
            skill_AddStateID1 = 0,
            skill_AddStateID2 = 3003
        };
        SQLiteManager.Instance.skillDataSource.Add(skillData.skill_ID, skillData);

        skillData = new SkillData
        {
            skill_ID = 3103,
            skill_Name = "黑魔法",
            skill_Type = "魔法",
            skill_DamageLevel = 3,
            skill_AddStateID1 = 0,
            skill_AddStateID2 = 3001
        };
        SQLiteManager.Instance.skillDataSource.Add(skillData.skill_ID, skillData);
        #endregion

        #region 狂战技能列表
        skillData = new SkillData
        {
            skill_ID = 3004,
            skill_Name = "跳劈",
            skill_Type = "魔法",
            skill_DamageLevel = 1,
            skill_AddStateID1 = 0,
            skill_AddStateID2 = 3002
        };
        SQLiteManager.Instance.skillDataSource.Add(skillData.skill_ID, skillData);

        skillData = new SkillData
        {
            skill_ID = 3005,
            skill_Name = "嗜血",
            skill_Type = "魔法",
            skill_DamageLevel = 2,
            skill_AddStateID1 = 0,
            skill_AddStateID2 = 3003
        };
        SQLiteManager.Instance.skillDataSource.Add(skillData.skill_ID, skillData);

        skillData = new SkillData
        {
            skill_ID = 3006,
            skill_Name = "地裂",
            skill_Type = "魔法",
            skill_DamageLevel = 3,
            skill_AddStateID1 = 0,
            skill_AddStateID2 = 3001
        };
        SQLiteManager.Instance.skillDataSource.Add(skillData.skill_ID, skillData);
        #endregion

        #region 猎人技能列表
        skillData = new SkillData
        {
            skill_ID = 3007,
            skill_Name = "爆破箭",
            skill_Type = "物理",
            skill_DamageLevel = 1,
            skill_AddStateID1 = 0,
            skill_AddStateID2 = 3002
        };
        SQLiteManager.Instance.skillDataSource.Add(skillData.skill_ID, skillData);

        skillData = new SkillData
        {
            skill_ID = 3008,
            skill_Name = "螺旋箭",
            skill_Type = "物理",
            skill_DamageLevel = 2,
            skill_AddStateID1 = 0,
            skill_AddStateID2 = 3003
        };
        SQLiteManager.Instance.skillDataSource.Add(skillData.skill_ID, skillData);

        skillData = new SkillData
        {
            skill_ID = 3009,
            skill_Name = "火箭雨",
            skill_Type = "物理",
            skill_DamageLevel = 3,
            skill_AddStateID1 = 0,
            skill_AddStateID2 = 3001
        };
        SQLiteManager.Instance.skillDataSource.Add(skillData.skill_ID, skillData);
        #endregion

        Debug.Log("skillData字典的元素个数:" + SQLiteManager.Instance.skillDataSource.Count);
        
        #endregion
    }
    #endregion

    #region void Init()英雄的模型 ,所有数据加载及初始化
    /// <summary>
    /// 英雄的所有数据,初始化
    /// </summary>
    void Init()
    {
        /*-----------------将英雄属性数据存到结构体模型中------------------------------*/
       // Debug.Log("开始构建英雄");
        #region *构建一个初始剑士的英雄
       
        #region 构建英雄数据PlayerDate
        playerData = new PlayerData
        {
            player_Id = 1002,
            player_Name = "初始剑士",
            player_Class = "剑士",
            player_Description = "这是一个低级剑士",
            EXHP = 200,
            EXAD = 100,
            EXAP = 0,
            EXDEF = 15,
            EXRES = 5,
            skillOneID = 3001,
            skillTwoID = 3002,
            skillThreeID = 3003,
            stateID = 3251
        };
        SQLiteManager.Instance.playerDataSource.Add(playerData.player_Id, playerData);
        #endregion
        hero = new HeroData();
        //PlayerData playerData= SQLiteManager.Instance.playerDataSource[1301];
        hero.playerData = SQLiteManager.Instance.playerDataSource[playerData.player_Id];
        hero.stateData = SQLiteManager.Instance.stateDataSource[playerData.stateID];
        hero.skillData = SQLiteManager.Instance.skillDataSource[playerData.skillOneID];

        hero.starHP = hero.playerData.EXHP;
        hero.currentAD = hero.playerData.EXAD;
        hero.currentAP = hero.playerData.EXAP;
        hero.currentDEF = hero.playerData.EXDEF;
        hero.currentRES = hero.playerData.EXRES;
        hero.currentStateID = hero.playerData.stateID;

        SQLiteManager.Instance.team.Add(playerData.player_Id, hero);        //将英雄添加到字典team

        #endregion
        #region *构建一个初始骑士的英雄
        hero = new HeroData();
        playerData = new PlayerData
        {
            player_Id = 1003,
            player_Name = "初始骑士",
            player_Class = "骑士",
            player_Description = "这是一个低级骑士",
            EXHP = 200,
            EXAD = 50,
            EXAP = 50,
            EXDEF = 10,
            EXRES = 10,
            skillOneID = 3104,
            skillTwoID = 3105,
            skillThreeID = 3106,
            stateID = 3251
        };
        SQLiteManager.Instance.playerDataSource.Add(playerData.player_Id, playerData);

        hero.playerData = SQLiteManager.Instance.playerDataSource[playerData.player_Id];
        hero.stateData = SQLiteManager.Instance.stateDataSource[playerData.stateID];
        hero.skillData = SQLiteManager.Instance.skillDataSource[playerData.skillOneID];

        hero.starHP = hero.playerData.EXHP;
        hero.currentAD = hero.playerData.EXAD;
        hero.currentAP = hero.playerData.EXAP;
        hero.currentDEF = hero.playerData.EXDEF;
        hero.currentRES = hero.playerData.EXRES;
        hero.currentStateID = hero.playerData.stateID;

        SQLiteManager.Instance.team.Add(playerData.player_Id, hero);        //将英雄添加到字典team
        #endregion
        #region *构建一个初始法师的英雄
        hero = new HeroData();
        #region 构建英雄数据PlayerDate
        playerData = new PlayerData
        {
            player_Id = 1004,
            player_Name = "初始法师",
            player_Class = "法师",
            player_Description = "这是一个低级法师",
            EXHP = 100,
            EXAD = 0,
            EXAP = 150,
            EXDEF = 0,
            EXRES = 10,
            skillOneID = 3101,
            skillTwoID = 3102,
            skillThreeID = 3103,
            stateID = 3251
        };
        SQLiteManager.Instance.playerDataSource.Add(playerData.player_Id, playerData);
        #endregion
        hero.playerData = SQLiteManager.Instance.playerDataSource[playerData.player_Id];
        hero.stateData = SQLiteManager.Instance.stateDataSource[playerData.stateID];
        hero.skillData = SQLiteManager.Instance.skillDataSource[playerData.skillOneID];

        hero.starHP = hero.playerData.EXHP;
        hero.currentAD = hero.playerData.EXAD;
        hero.currentAP = hero.playerData.EXAP;
        hero.currentDEF = hero.playerData.EXDEF;
        hero.currentRES = hero.playerData.EXRES;
        hero.currentStateID = hero.playerData.stateID;

        SQLiteManager.Instance.team.Add(playerData.player_Id, hero);        //将英雄添加到字典team

        #endregion
        #region *构建一个初始狂战的英雄
        hero = new HeroData();
        #region 构建英雄数据PlayerDate
        playerData = new PlayerData
        {
            player_Id = 1005,
            player_Name = "初始狂战",
            player_Class = "狂战",
            player_Description = "这是一个低级狂战",
            EXHP = 200,
            EXAD = 150,
            EXAP = 0,
            EXDEF = 5,
            EXRES = 5,
            skillOneID = 3004,
            skillTwoID = 3005,
            skillThreeID = 3006,
            stateID = 3251
        };
        SQLiteManager.Instance.playerDataSource.Add(playerData.player_Id, playerData);
        #endregion
        hero.playerData = SQLiteManager.Instance.playerDataSource[playerData.player_Id];
        hero.stateData = SQLiteManager.Instance.stateDataSource[playerData.stateID];
        hero.skillData = SQLiteManager.Instance.skillDataSource[playerData.skillOneID];

        hero.starHP = hero.playerData.EXHP;
        hero.currentAD = hero.playerData.EXAD;
        hero.currentAP = hero.playerData.EXAP;
        hero.currentDEF = hero.playerData.EXDEF;
        hero.currentRES = hero.playerData.EXRES;
        hero.currentStateID = hero.playerData.stateID;

        SQLiteManager.Instance.team.Add(playerData.player_Id, hero);        //将英雄添加到字典team

        #endregion
        #region *构建一个初始猎人的英雄
        hero = new HeroData();
        #region 构建英雄数据PlayerDate
        playerData = new PlayerData
        {
            player_Id = 1006,
            player_Name = "初始猎人",
            player_Class = "猎人",
            player_Description = "这是一个低级猎人",
            EXHP = 100,
            EXAD = 100,
            EXAP = 0,
            EXDEF = 5,
            EXRES = 5,
            skillOneID = 3007,
            skillTwoID = 3008,
            skillThreeID = 3009,
            stateID = 3251
        };
        SQLiteManager.Instance.playerDataSource.Add(playerData.player_Id, playerData);
        #endregion
        hero.playerData = SQLiteManager.Instance.playerDataSource[playerData.player_Id];
        hero.stateData = SQLiteManager.Instance.stateDataSource[playerData.stateID];
        hero.skillData = SQLiteManager.Instance.skillDataSource[playerData.skillOneID];

        hero.starHP = hero.playerData.EXHP;
        hero.currentAD = hero.playerData.EXAD;
        hero.currentAP = hero.playerData.EXAP;
        hero.currentDEF = hero.playerData.EXDEF;
        hero.currentRES = hero.playerData.EXRES;
        hero.currentStateID = hero.playerData.stateID;

        SQLiteManager.Instance.team.Add(playerData.player_Id, hero);        //将英雄添加到字典team

        #endregion
    }
    #endregion


    #region public void AddTeam() 组建小队出战
    public void AddTeam()
    {
        //SetHero(1300);
        //SQLiteManager.Instance.team.Add(1300, SetHero(1300));        //将英雄添加到字典team
        SQLiteManager.Instance.team.Add(1301, SetHero(1301));        //将英雄添加到字典team
        SQLiteManager.Instance.team.Add(1302, SetHero(1302));        //将英雄添加到字典team
        SQLiteManager.Instance.team.Add(1303, SetHero(1303));        //将英雄添加到字典team
        SQLiteManager.Instance.team.Add(1304, SetHero(1304));        //将英雄添加到字典team
        SQLiteManager.Instance.team.Add(1305, SetHero(1305));        //将英雄添加到字典team

    }
    #endregion

    #region public HeroData SetHero(int  heroID) 构建英雄全装
    public HeroData SetHero(int  heroID)
    {
        HeroData hero = new HeroData();
        PlayerData playerData = SQLiteManager.Instance.playerDataSource[heroID];
        hero.playerData = playerData;
        //Debug.Log("构建的英雄名:" + hero.playerData.player_Name+"stateID:"+playerData.stateID);

        hero.stateData = SQLiteManager.Instance.stateDataSource[playerData.stateID];
        //Debug.Log("构建的英雄状态名:" + hero.stateData.state_Name);

        hero.skillData = SQLiteManager.Instance.skillDataSource[playerData.skillOneID];
        //Debug.Log("构建的英雄技能名:" + hero.skillData.skill_Name);

        hero.starHP = hero.playerData.EXHP;
        hero.currentAD = hero.playerData.EXAD;
        hero.currentAP = hero.playerData.EXAP;
        hero.currentDEF = hero.playerData.EXDEF;
        hero.currentRES = hero.playerData.EXRES;
        hero.currentStateID = hero.playerData.stateID;
        return hero;
    }
    #endregion
}
