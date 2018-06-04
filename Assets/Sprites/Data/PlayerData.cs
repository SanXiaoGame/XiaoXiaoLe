using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 角色数据类
/// </summary>
public struct PlayerData
{
    public int player_Id;
    public string Name;
    public string player_Class;
    public string player_Text;
    public int HP;
    public int AD;
    public int AP;
    public int DEF;
    public int RES;
    public int EXP;
    public int Skill_ID;

    public int StateID;
    public const float attackSpeed = 0f;
    public const float movingSpeed = 0f;
    //进化后对应数值
    public int EXHP;
    public int EXAD;
    public int EXAP;
    public int EXDEF;
    public int EXRES;
}
