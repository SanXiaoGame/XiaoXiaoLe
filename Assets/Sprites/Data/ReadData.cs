using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 读数据功能类
/// </summary>
public class ReadData
{
    //数据库操作对象
    DBOperation dbOperation;

    public ReadData(string path)
    {
        //初始化数据库对象
        string tempPath;
#if UNITY_ANDROID
        tempPath = "URI=file:" + path;
#else
        tempPath = "Data Source=" + path;
#endif
        dbOperation = new DBOperation(tempPath);
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    public void GetData(string tbName)
    {
        //清空数据
        SQLiteManager.Instance.dataSource.Clear();
        //执行查询操作
        SqliteDataReader reader = dbOperation.GetAllDataFromSQLTable(tbName);
        //读取所有表
        IsCharacterList(tbName, reader);
        IsEnemy(tbName, reader);
        IsItem(tbName, reader);
        IsLevel(tbName, reader);
        IsPlayer(tbName, reader);
        IsSkill(tbName, reader);
        IsState(tbName, reader);
    }

    /// <summary>
    /// 是否是人物表
    /// </summary>
    /// <param 表名="tbName"></param>
    /// <param 数据阅读器="reader"></param>
    private void IsCharacterList(string tbName, SqliteDataReader reader)
    {
        while (reader.Read() && tbName == ConstData.CharacterList)
        {
            //获取读到内容中的字段,来保存对应的值
            int character_Id = reader.GetInt32(reader.GetOrdinal("ID"));
            string character_Name = reader.GetString(reader.GetOrdinal("character_Name"));
            string character_Class = reader.GetString(reader.GetOrdinal("character_Class"));
            string character_Description = reader.GetString(reader.GetOrdinal("character_Description"));
            int character_HP = reader.GetInt32(reader.GetOrdinal("character_HP"));
            int character_AD = reader.GetInt32(reader.GetOrdinal("character_AD"));
            int character_AP = reader.GetInt32(reader.GetOrdinal("character_AP"));
            int character_DEF = reader.GetInt32(reader.GetOrdinal("character_DEF"));
            int character_RES = reader.GetInt32(reader.GetOrdinal("character_RES"));
            int character_EXP = reader.GetInt32(reader.GetOrdinal("character_EXP"));
            int character_SkillOneID = reader.GetInt32(reader.GetOrdinal("character_SkillOneID"));
            int character_SkillTwoID = reader.GetInt32(reader.GetOrdinal("character_SkillTwoID"));
            int character_SkillThreeID = reader.GetInt32(reader.GetOrdinal("character_SkillThreeID"));
            int character_EXHP = reader.GetInt32(reader.GetOrdinal("character_EXHP"));
            int character_EXAD = reader.GetInt32(reader.GetOrdinal("character_EXAD"));
            int character_EXAP = reader.GetInt32(reader.GetOrdinal("character_EXAP"));
            int character_EXDEF = reader.GetInt32(reader.GetOrdinal("character_EXDEF"));
            int character_EXRES = reader.GetInt32(reader.GetOrdinal("character_EXRES"));
            int character_Level = reader.GetInt32(reader.GetOrdinal("character_Level"));
            int character_Weapon = reader.GetInt32(reader.GetOrdinal("character_Weapon"));
            int character_Equipment = reader.GetInt32(reader.GetOrdinal("character_Equipment"));
            //创建模型
            CharacterListData characterListData = new CharacterListData();
            characterListData.character_Id = character_Id;
            characterListData.character_Name = character_Name;
            characterListData.character_Class = character_Class;
            characterListData.character_Description = character_Description;
            characterListData.character_HP = character_HP;
            characterListData.character_AD = character_AD;
            characterListData.character_AP = character_AP;
            characterListData.character_DEF = character_DEF;
            characterListData.character_RES = character_RES;
            characterListData.character_EXP = character_EXP;
            characterListData.character_SkillOneID = character_SkillOneID;
            characterListData.character_SkillTwoID = character_SkillTwoID;
            characterListData.character_SkillThreeID = character_SkillThreeID;
            characterListData.character_EXHP = character_EXHP;
            characterListData.character_EXAD = character_EXAD;
            characterListData.character_EXAP = character_EXAP;
            characterListData.character_EXDEF = character_EXDEF;
            characterListData.character_EXRES = character_EXRES;
            characterListData.character_Level = character_Level;
            characterListData.character_Weapon = character_Weapon;
            characterListData.character_Equipment = character_Equipment;
            //加入到数据库
            SQLiteManager.Instance.dataSource.Add(characterListData.character_Id, characterListData);
        }
    }
    /// <summary>
    /// 是否是敌人表
    /// </summary>
    /// <param 表名="tbName"></param>
    /// <param 数据阅读器="reader"></param>
    private void IsEnemy(string tbName, SqliteDataReader reader)
    {
        while (reader.Read() && tbName == ConstData.Enemy)
        {
            //获取读到内容中的字段,来保存对应的值
            int enemy_Id = reader.GetInt32(reader.GetOrdinal("ID"));
            string enemy_Name = reader.GetString(reader.GetOrdinal("enemy_Name"));
            string enemy_Type = reader.GetString(reader.GetOrdinal("enemy_Type"));
            int HP = reader.GetInt32(reader.GetOrdinal("enemy_HP"));
            int AD = reader.GetInt32(reader.GetOrdinal("enemy_AD"));
            int AP = reader.GetInt32(reader.GetOrdinal("enemy_AP"));
            int DEF = reader.GetInt32(reader.GetOrdinal("enemy_DEF"));
            int RES = reader.GetInt32(reader.GetOrdinal("enemy_RES"));
            int EXP = reader.GetInt32(reader.GetOrdinal("enemy_EXP"));
            int Skill_ID = reader.GetInt32(reader.GetOrdinal("enemy_SkillID"));
            //创建模型
            EnemyData enemyData = new EnemyData
            {
                enemy_Id = enemy_Id,
                enemy_Name = enemy_Name,
                enemy_Type = enemy_Type,
                HP = HP,
                AD = AD,
                AP = AP,
                DEF = DEF,
                RES = RES,
                EXP = EXP,
                Skill_ID = Skill_ID
            };
            //加入到数据库
            SQLiteManager.Instance.dataSource.Add(enemyData.enemy_Id, enemyData);
        }
    }
    /// <summary>
    /// 是否是物品表
    /// </summary>
    /// <param 表名="tbName"></param>
    /// <param 数据阅读器="reader"></param>
    private void IsItem(string tbName, SqliteDataReader reader)
    {
        while (reader.Read() && tbName == ConstData.Item)
        {
            //获取读到内容中的字段,来保存对应的值
            int item_Id = reader.GetInt32(reader.GetOrdinal("ID"));
            string item_Name = reader.GetString(reader.GetOrdinal("item_Name"));
            string item_Type = reader.GetString(reader.GetOrdinal("item_Type"));
            string item_Description = reader.GetString(reader.GetOrdinal("item_Description"));
            int item_Price = reader.GetInt32(reader.GetOrdinal("item_Price"));
            int item_Diamond = reader.GetInt32(reader.GetOrdinal("item_Diamond"));
            int Stockpile = reader.GetInt32(reader.GetOrdinal("item_Stockpile"));
            //创建模型
            ItemData itemData = new ItemData
            {
                item_Id = item_Id,
                item_Name = item_Name,
                item_Type = item_Type,
                item_Description = item_Description,
                item_Price = item_Price,
                item_Diamond = item_Diamond,
                Stockpile = Stockpile,
            };
            //加入到数据库
            SQLiteManager.Instance.dataSource.Add(itemData.item_Id, itemData);
        }
    }
    /// <summary>
    /// 是否是等级经验表
    /// </summary>
    /// <param 表名="tbName"></param>
    /// <param 数据阅读器="reader"></param>
    private void IsLevel(string tbName, SqliteDataReader reader)
    {
        while (reader.Read() && tbName == ConstData.Level)
        {
            //获取读到内容中的字段,来保存对应的值
            int level = reader.GetInt32(reader.GetOrdinal("Level"));
            int level_MaxEXP = reader.GetInt32(reader.GetOrdinal("level_MaxEXP"));
            int level_HP = reader.GetInt32(reader.GetOrdinal("level_HP"));
            int level_AD = reader.GetInt32(reader.GetOrdinal("level_AD"));
            int level_AP = reader.GetInt32(reader.GetOrdinal("level_AP"));
            int level_DEF = reader.GetInt32(reader.GetOrdinal("level_DEF"));
            int level_RES = reader.GetInt32(reader.GetOrdinal("level_RES"));
            //创建模型
            LVData lVData = new LVData
            {
                level = level,
                level_MaxEXP = level_MaxEXP,
                level_HP = level_HP,
                level_AD = level_AD,
                level_AP = level_AP,
                level_DEF = level_DEF,
                level_RES = level_RES,
            };
            //加入到数据库
            SQLiteManager.Instance.dataSource.Add(lVData.level, lVData);
        }
    }
    /// <summary>
    /// 是否是角色存档表
    /// </summary>
    /// <param 表名="tbName"></param>
    /// <param 数据阅读器="reader"></param>
    private void IsPlayer(string tbName, SqliteDataReader reader)
    {
        while (reader.Read() && tbName == ConstData.Player)
        {
            //获取读到内容中的字段,来保存对应的值
            int player_Id = reader.GetInt32(reader.GetOrdinal("ID"));
            string player_Name = reader.GetString(reader.GetOrdinal("player_Name"));
            string player_Class = reader.GetString(reader.GetOrdinal("player_Class"));
            string player_Description = reader.GetString(reader.GetOrdinal("player_Description"));
            int player_HP = reader.GetInt32(reader.GetOrdinal("player_HP"));
            int player_AD = reader.GetInt32(reader.GetOrdinal("player_AD"));
            int player_AP = reader.GetInt32(reader.GetOrdinal("player_AP"));
            int player_DEF = reader.GetInt32(reader.GetOrdinal("player_DEF"));
            int player_RES = reader.GetInt32(reader.GetOrdinal("player_RES"));
            int player_EXP = reader.GetInt32(reader.GetOrdinal("player_EXP"));
            int player_SkillOneID = reader.GetInt32(reader.GetOrdinal("player_SkillOneID"));
            int player_SkillTwoID = reader.GetInt32(reader.GetOrdinal("player_SkillTwoID"));
            int player_SkillThreeID = reader.GetInt32(reader.GetOrdinal("player_SkillThreeID"));
            int player_EXHP = reader.GetInt32(reader.GetOrdinal("player_EXHP"));
            int player_EXAD = reader.GetInt32(reader.GetOrdinal("player_EXAD"));
            int player_EXAP = reader.GetInt32(reader.GetOrdinal("player_EXAP"));
            int player_EXDEF = reader.GetInt32(reader.GetOrdinal("player_EXDEF"));
            int player_EXRES = reader.GetInt32(reader.GetOrdinal("player_EXRES"));
            int player_Level = reader.GetInt32(reader.GetOrdinal("player_Level"));
            int player_Weapon = reader.GetInt32(reader.GetOrdinal("player_Weapon"));
            int player_Equipment = reader.GetInt32(reader.GetOrdinal("player_Equipment"));
            //创建模型
            PlayerData playerData = new PlayerData();
            playerData.player_Id = player_Id;
            playerData.player_Name = player_Name;
            playerData.player_Class = player_Class;
            playerData.player_Description = player_Description;
            playerData.HP = player_HP;
            playerData.AD = player_AD;
            playerData.AP = player_AP;
            playerData.DEF = player_DEF;
            playerData.RES = player_RES;
            playerData.EXP = player_EXP;
            playerData.skillOneID = player_SkillOneID;
            playerData.skillTwoID = player_SkillTwoID;
            playerData.skillThreeID = player_SkillThreeID;
            playerData.EXHP = player_EXHP;
            playerData.EXAD = player_EXAD;
            playerData.EXAP = player_EXAP;
            playerData.EXDEF = player_EXDEF;
            playerData.EXRES = player_EXRES;
            playerData.Level = player_Level;
            playerData.Weapon = player_Weapon;
            playerData.Equipment = player_Equipment;
            //加入到数据库
            SQLiteManager.Instance.dataSource.Add(playerData.player_Id, playerData);
        }
    }
    /// <summary>
    /// 是否是技能表
    /// </summary>
    /// <param 表名="tbName"></param>
    /// <param 数据阅读器="reader"></param>
    private void IsSkill(string tbName, SqliteDataReader reader)
    {
        while (reader.Read() && tbName == ConstData.Skill)
        {
            //获取读到内容中的字段,来保存对应的值
            int skill_ID = reader.GetInt32(reader.GetOrdinal("ID"));
            string skill_Name = reader.GetString(reader.GetOrdinal("skill_Name"));
            string skill_Type = reader.GetString(reader.GetOrdinal("skill_Type"));
            int skill_DamageLevel = reader.GetInt32(reader.GetOrdinal("skill_DamageLevel"));
            string skill_Description = reader.GetString(reader.GetOrdinal("skill_Description"));
            int skill_AddStateID = reader.GetInt32(reader.GetOrdinal("skill_AddStateID"));
            //创建模型
            SkillData skillData = new SkillData
            {
                skill_ID = skill_ID,
                skill_Name = skill_Name,
                skill_Type = skill_Type,
                skill_DamageLevel = skill_DamageLevel,
                skill_Description = skill_Description,
                skill_AddStateID = skill_AddStateID,
            };
            //加入到数据库
            SQLiteManager.Instance.dataSource.Add(skillData.skill_ID, skillData);
        }
    }
    /// <summary>
    /// 是否是状态表
    /// </summary>
    /// <param 表名="tbName"></param>
    /// <param 数据阅读器="reader"></param>
    private void IsState(string tbName, SqliteDataReader reader)
    {
        while (reader.Read() && tbName == ConstData.State)
        {
            //获取读到内容中的字段,来保存对应的值
            int state_ID = reader.GetInt32(reader.GetOrdinal("ID"));
            string state_Name = reader.GetString(reader.GetOrdinal("state_Name"));
            string state_Type = reader.GetString(reader.GetOrdinal("state_Type"));
            float state_Value = reader.GetFloat(reader.GetOrdinal("state_Value"));
            float state_KeepTime = reader.GetFloat(reader.GetOrdinal("state_KeepTime"));
            string state_Description = reader.GetString(reader.GetOrdinal("state_Description"));
            //创建模型
            StateData stateData = new StateData
            {
                StateID = state_ID,
                state_Name = state_Name,
                state_Type = state_Type,
                state_Value = state_Value,
                state_KeepTime = state_KeepTime,
                state_Description = state_Description
            };
            //加入到数据库
            SQLiteManager.Instance.dataSource.Add(stateData.StateID, stateData);
        }
    }

    /// <summary>
    /// 读取指定数据（保留）
    /// </summary>
    /// <param 条件="codition"></param>
    //public void GetData(string codition)
    //{

    //}
}
