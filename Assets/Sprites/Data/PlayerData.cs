using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 角色数据类
/// </summary>
public class PlayerData
{
    public int player_Id;
    public string player_Name;
    public string player_Class;
    public string player_Description;
    public int HP;
    public int AD;
    public int AP;
    public int DEF;
    public int RES;
    public int Level;
    public int EXP;
    public int skillOneID;
    public int skillTwoID;
    public int skillThreeID;
    public int Weapon;
    public int Equipment;
    public ulong GoldCoin;
    public int Diamond;
    public int PrefabsID;
    //进化后对应数值
    public int EXHP;
    public int EXAD;
    public int EXAP;
    public int EXDEF;
    public int EXRES;
    //角色状态临时存储
    public int stateID;
}
/// <summary>
/// 人物初始数据类
/// </summary>
public struct CharacterListData
{
    public int character_Id;
    public string character_Name;
    public string character_Class;
    public string character_Description;
    public int character_HP;
    public int character_AD;
    public int character_AP;
    public int character_DEF;
    public int character_RES;
    public int character_Level;
    public int character_EXP;
    public int character_SkillOneID;
    public int character_SkillTwoID;
    public int character_SkillThreeID;
    public int character_Weapon;
    public int character_Equipment;
    //进化后对应数值
    public int character_EXHP;
    public int character_EXAD;
    public int character_EXAP;
    public int character_EXDEF;
    public int character_EXRES;
    //角色状态临时存储
    public int stateID;
}



