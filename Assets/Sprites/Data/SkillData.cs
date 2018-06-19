using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 技能数据
/// </summary>
public struct SkillData
{
    public int skill_ID;
    public string skill_Name;
    public string skill_Type;
    public int skill_DamageLevel;
    public string skill_Description;
    public int skill_AddStateID1;
    public int skill_AddStateID2;
}
//剑士技能状态列表
public enum SwordsmanSkillID
{
    oneSkill = 3001,
    twoSkill = 3002,
    threeSkill = 3003,

}

//骑士技能列表
public enum KnightSkillID
{
    oneSkill = 3104,
    twoSkill = 3105,
    threeSkill = 3106,

}

//法师技能列表
public enum MasterSkillID
{
    oneSkill = 3101,
    twoSkill = 3102,
    threeSkill = 3103,

}

//猎人技能列表
public enum HunterSkillID
{
    oneSkill = 3007,
    twoSkill = 3008,
    threeSkill = 3009,

}

//狂战士技能列表
public enum BerserkerSkillID
{
    oneSkill = 3004,
    twoSkill = 3005,
    threeSkill = 3006,

}