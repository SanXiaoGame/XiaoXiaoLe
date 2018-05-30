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
    /// 插入一条数据到表中
    /// </summary>
    /// <param 表名="tbName"></param>
    /// <param 值集合="values"></param>
    public void InsertDataToTable(string tbName, string[] values)
    {
        string sqlQuery = ConstData.InsertLine01 + tbName + ConstData.InsertLine02 + "'" + values[0] + "'";
        for (int i = 1; i < values.Length; i++)
        {
            sqlQuery += "," + "'" + values[i] + "'";
        }
        sqlQuery += ")";
        ExcuteSQLQuery(sqlQuery);
    }

    /// <summary>
    /// 更新一条数据
    /// </summary>
    /// <param 表名="tbName"></param>
    /// <param 更新的字段集合="cols"></param>
    /// <param 更新字段的值="colsValue"></param>
    /// <param 条件字段="key"></param>
    /// <param 条件字段的值="keyValue"></param>
    public void UpdataDataFormTable(string tbName, string cols, int colsValue, string key, string keyValue)
    {
        string query = ConstData.UPDATE + tbName + ConstData.SET + cols + " = " + colsValue + ConstData.WHERE + key + " = " + "'" + keyValue + "'";
        Debug.Log(query);
        ExcuteSQLQuery(query);
    }
    //暂不使用
    /*public void UpdataDataFormTable(string tbName,string[] cols, string[] colsValue, string key, string keyValue)
    {
        string query = "UPDATE " + tbName + " SET " + cols[0] + "=" + "'" + colsValue[0] + "'";
        for (int i = 1; i < cols.Length; i++)
        {
            query += "," + cols[i] + "=" + "'" + colsValue[i] + "'";
        }
        query += "WHERE " + key + "=" + "'" + keyValue + "'";
        Debug.Log(query);
        ExcuteSQLQuery(query);
    }*/
}
