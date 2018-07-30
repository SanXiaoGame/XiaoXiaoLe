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