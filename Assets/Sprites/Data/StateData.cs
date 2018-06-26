using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 角色状态数据类
/// </summary>
public class StateData
{
    public int StateID;
    public string state_Name;
    public string state_Type;
    public float state_Value;
    public float state_KeepTime;
    public string state_Description;
}

public enum HeroState
{
    idle = 3251,
    await = 3252,
    move = 3253,
    diz = 3254,
    win = 3255,
    dead = 3256,
    commonAttack = 3257,
    saberOneSkill=3258,
    saberTwoSkill=3259,
    saberThreeSkill=3260,
    knightOneSkill=3261,
    knightTwoSkill = 3262,
    knightThreeSkill = 3263,
    casterOneSkill=3264,
    casterTwoSkill=3265,
    casterThreeSkill=3266,
    berserkerOneSkill=3267,
    berserkerTwoSkill=3268,
    berserkerThreeSkill=3269,
    hunterOneSkill=3270,
    hunterTwoSkill=3271,
    hunterThreeSkill=3272,
}

/// <summary>
/// 状态名称
/// </summary>
public enum StateName
{
    Idle,
    Await,
    Run,
    Diz,
    Win,
    CommonAttack,
    FirstAttack,
    SecondAttack,
    ThirdAttack,
    GetHit,
    Recover,
    Dead,
    Reset
}