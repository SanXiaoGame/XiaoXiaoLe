using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System;

/// <summary>
/// 该类用来处理数据库操作命令
/// 数据库核心类:该类负责数据库连接，操作数据库，更新数据库，关闭数据库等所有和数据库相关的操作
/// </summary>
public class DBOperation:MonoBehaviour
{
    //数据库连接对象
    SqliteConnection dbconnect;
    //数据库命令对象
    SqliteCommand dbcommand;
    //接收数据库内容
    SqliteDataReader dbReader;

    public DBOperation(string path)
    {
        //建立连接
        OpenDB(path);
    }
    private void Awake()
    {
        for (int i = 1301; i < 1306; i++)
        {
            ReadCharacterData(i);
            ReadSkillData(SQLiteManager.Instance.characterDataSource[i].character_SkillOneID);
            ReadSkillData(SQLiteManager.Instance.characterDataSource[i].character_SkillTwoID);
            ReadSkillData(SQLiteManager.Instance.characterDataSource[i].character_SkillThreeID);
        }
        for (int i = 2001; i < 2064; i++)
        {
            ReadEquipmentData(i);
        }
        for (int i = 2101; i < 2126; i++)
        {
            ReadEquipmentData(i);
        }

    }
    public void ReadCharacterData(int id)
    {
        string path = "Data Source =" + Application.streamingAssetsPath + "/SQLite/NereisQuest.sqlite";
        OpenDB(path);
        string query = string.Format("SELECT ID,player_Name,player_Class,player_HP,player_AD,player_AP,player_DEF,player_RES,player_SkillOneID,player_SkillTwoID,player_SkillThreeID,player_Level,player_EXP FROM Player WHERE ID = {0}", id);
        SqliteCommand cmd = dbconnect.CreateCommand();
        cmd.CommandText = query;
        SqliteDataReader read = cmd.ExecuteReader();
        CharacterListData characterListData = new CharacterListData();
        characterListData.character_Id = int.Parse(read[0].ToString());
        characterListData.character_Name = read[1].ToString();
        characterListData.character_Class = read[2].ToString();
        characterListData.character_HP = int.Parse(read[3].ToString());
        characterListData.character_AD = int.Parse(read[4].ToString());
        characterListData.character_AP = int.Parse(read[5].ToString());
        characterListData.character_DEF = int.Parse(read[6].ToString());
        characterListData.character_RES = int.Parse(read[7].ToString());
        characterListData.character_SkillOneID = int.Parse(read[8].ToString());
        characterListData.character_SkillTwoID = int.Parse(read[9].ToString());
        characterListData.character_SkillThreeID = int.Parse(read[10].ToString());
        characterListData.character_EXP = int.Parse(read[11].ToString());
        SQLiteManager.Instance.characterDataSource.Add(id, characterListData);
        cmd.Dispose();
        cmd = null;
    }
    public void ReadSkillData(int id)
    {
        string path = "Data Source =" + Application.streamingAssetsPath + "/SQLite/NereisQuest.sqlite";
        OpenDB(path);
        string query = string.Format("SELECT *FROM Skill WHERE ID = {0}", id);
        SqliteCommand cmd = dbconnect.CreateCommand();
        cmd.CommandText = query;
        SqliteDataReader read = cmd.ExecuteReader();
        SkillData skillData = new SkillData();
        skillData.skill_ID = int.Parse(read[0].ToString());
        skillData.skill_Name = read[1].ToString();
        skillData.skill_Type = read[2].ToString();
        skillData.skill_DamageLevel = int.Parse(read[3].ToString());
        skillData.skill_Description = read[4].ToString();
        SQLiteManager.Instance.skillDataSource.Add(id, skillData);
        cmd.Dispose();
        cmd = null;
    }
    public void ReadItemData(int id)
    {
        string path = "Data Source =" + Application.streamingAssetsPath + "/SQLite/NereisQuest.sqlite";
        OpenDB(path);
        string query = string.Format("SELECT *FROM Item WHERE ID = {0}", id);
        SqliteCommand cmd = dbconnect.CreateCommand();
        cmd.CommandText = query;
        SqliteDataReader read = cmd.ExecuteReader();
        ItemData itemData = new ItemData();
        itemData.item_Id = int.Parse(read[0].ToString());
        itemData.item_Name = read[1].ToString();
        itemData.item_Type = read[2].ToString();
        itemData.item_Price = int.Parse(read[3].ToString());
        SQLiteManager.Instance.itemDataSource.Add(id, itemData);
        cmd.Dispose();
        cmd = null;
    }
    public void ReadEquipmentData(int id)
    {
        string path = "Data Source =" + Application.streamingAssetsPath + "/SQLite/NereisQuest.sqlite";
        OpenDB(path);
        string query = string.Format("SELECT *FROM Equipment WHERE ID = {0}", id);
        SqliteCommand cmd = dbconnect.CreateCommand();
        cmd.CommandText = query;
        SqliteDataReader read = cmd.ExecuteReader();
        EquipmentData equipmentData = new EquipmentData();
        equipmentData.equipment_Id = int.Parse(read[0].ToString());
        equipmentData.equipmentNmae = read[1].ToString();
        equipmentData.equipmentType = read[2].ToString();
        equipmentData.equipmentClass = read[3].ToString();
        equipmentData.equipment_HP = int.Parse(read[4].ToString());
        equipmentData.equipment_AD = int.Parse(read[5].ToString());
        equipmentData.equipment_AP = int.Parse(read[6].ToString());
        equipmentData.equipment_DEF = int.Parse(read[7].ToString());
        equipmentData.equipment_RES = int.Parse(read[8].ToString());
        equipmentData.equipmentPrice = ulong.Parse(read[9].ToString());
        SQLiteManager.Instance.equipmentDataSource.Add(id, equipmentData);
        cmd.Dispose();
        cmd = null;
    }
    /// <summary>
    /// 建立数据库连接
    /// </summary>
    /// <param 数据库路径="path"></param>
    private void OpenDB(string dbpath)
    {
        try
        {
            dbconnect = new SqliteConnection(dbpath);
            dbconnect.Open();
            Debug.Log("连接数据库成功");
        }
        catch (Exception exp)
        {
            Debug.Log("连接失败" + exp);
        }
    }

    /// <summary>
    /// 关闭数据库
    /// </summary>
    public void Cloesd()
    {
        if (dbconnect != null)
        {
            dbconnect.Dispose();
            dbconnect = null;
        }
    }

    /// <summary>
    /// 操作数据库
    /// </summary>
    /// <param 数据库语句="query"></param>
    /// <returns></returns>
    public SqliteDataReader ExcuteSQLQuery(string query)
    {
        try
        {
            //创建查询命令
            dbcommand = dbconnect.CreateCommand();
            dbcommand.CommandText = query;
            dbReader = dbcommand.ExecuteReader();
        }
        catch (Exception exp)
        {
            Debug.Log("语句有误" + exp.ToString());
        }
        return dbReader;
    }

    /// <summary>
    /// 获取表中所有数据
    /// </summary>
    /// <param 表名="tableName"></param>
    /// <returns></returns>
    public SqliteDataReader GetAllDataFromSQLTable(string tableName)
    {
        //建立查询语句
        string query = ConstData.SELECT + ConstData.FROM01 + tableName;
        return ExcuteSQLQuery(query);
    }

    /// <summary>
    /// 获取表中一个数据
    /// </summary>
    /// <param 表的字段="field01"></param>
    /// <param 表的限定字段="field02"></param>
    /// <param 值="codition"></param>
    /// <returns></returns>
    public SqliteDataReader GetDataFromSQLTable(string field01, string field02,string codition)
    {
        //建立查询语句
        string query = ConstData.SELECT + field01 + ConstData.FROM02 + "这里是表名" + ConstData.WHERE + field02 + " = " + "'" + codition + "'";
        return ExcuteSQLQuery(query);
    }

    /// <summary>
    /// 插入一条新数据到表中
    /// </summary>
    /// <param 表名="tbName"></param>
    /// <param 值集合="values"></param>
    public void InsertDataToTable(string tbName, string[] values)
    {
        string sqlQuery = "INSERT INTO " + tbName + " VALUES(" + values[0] + "'" + values[1] + "'" + "'" + values[2] + "'" + "'" + values[3] + "'";
        for (int i = 0; i < values.Length - 4; i++)
        {
            sqlQuery = StringSplicingTool.StringSplicing(sqlQuery, values[i]);
        }
        sqlQuery = StringSplicingTool.StringSplicing(sqlQuery, ")");
        ExcuteSQLQuery(sqlQuery);
    }

    /// <summary>
    /// 更新指定存档数据
    /// </summary>
    /// <param 表名="tbName"></param>
    /// <param 更新的字段="field"></param>
    /// <param 更新字段的值="fieldValue"></param>
    /// <param 条件字段="key"></param>
    /// <param 条件字段的值="keyValue"></param>
    public void UpdataDataFormTable(string tbName, string field, int fieldValue, string key, int keyValue)
    {
        string query = ConstData.UPDATE + tbName + ConstData.SET + field + " = " + fieldValue + ConstData.WHERE + key + " = " + keyValue;
        Debug.Log(query);
        ExcuteSQLQuery(query);
    }

    /// <summary>
    /// 删除存档中一行指定的数据
    /// </summary>
    /// <param 表名="tbName"></param>
    /// <param 对应字段="key"></param>
    /// <param 对应ID号="keyValue"></param>
    public void DeleteTableData(string tbName, string key, int keyValue)
    {
        string query = ConstData.DELETE + ConstData.FROM02 + tbName + ConstData.WHERE + key + " = " + keyValue;
        Debug.Log(query);
        ExcuteSQLQuery(query);
    }
}
