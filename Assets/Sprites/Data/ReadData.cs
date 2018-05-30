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
    public void GetData()
    {
        //清空数据
        SQLiteManager.Instance.dataSource.Clear();
        //执行查询操作
        SqliteDataReader reader = dbOperation.GetAllDataFromSQLTable("表名");

        while (reader.Read())
        {
            /*//获取读到内容中的字段,来保存对应的值
            int ID = reader.GetInt32(reader.GetOrdinal("id"));
            string ItemName = reader.GetString(reader.GetOrdinal("Item_Name"));
            int ItemHP = reader.GetInt32(reader.GetOrdinal("Item_HP"));
            int ItemMP = reader.GetInt32(reader.GetOrdinal("Item_MP"));
            int ItemAtt = reader.GetInt32(reader.GetOrdinal("Item_Att"));
            int ItemDef = reader.GetInt32(reader.GetOrdinal("Item_Def"));
            int ItemGZ = reader.GetInt32(reader.GetOrdinal("Item_GZ"));
            int ItemType = reader.GetInt32(reader.GetOrdinal("Item_Type"));
            //创建模型
            ItemData item = new ItemData();
            item.ItemID = ID;
            item.ItemName = ItemName;
            item.ItemHP = ItemHP;
            item.ItemMP = ItemMP;
            item.ItemAtt = ItemAtt;
            item.ItemDef = ItemDef;
            item.ItemGZ = ItemGZ;
            item.ItemType = ItemType;

            //加入到数据库
            SQLiteManager.Instance.dataSource.Add(item.ItemID, item);*/
        }
    }
    /// <summary>
    /// 获取指定数据
    /// </summary>
    /// <param 条件="codition"></param>
    public void GetData(string codition)
    {
        
    }
}
