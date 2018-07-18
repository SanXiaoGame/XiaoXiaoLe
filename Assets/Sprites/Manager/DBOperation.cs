using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System;

/// <summary>
/// 该类用来处理数据库操作命令
/// 数据库核心类:该类负责数据库连接，操作数据库，更新数据库，关闭数据库等所有和数据库相关的操作
/// </summary>
public class DBOperation
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
        //Debug.Log(query);
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
        string sqlQuery = "INSERT INTO " + tbName + " VALUES(" + values[0] + "," + "'" + values[1] + "'" + "," + "'" + values[2] + "'" +","+ "'" + values[3] + "'";
        for (int i = 4; i < values.Length; i++)
        {
            sqlQuery = StringSplicingTool.StringSplicing(StringSplicingTool.StringSplicing(sqlQuery, ","), values[i]);
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
