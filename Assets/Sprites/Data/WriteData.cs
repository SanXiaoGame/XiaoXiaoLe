using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 写数据功能类
/// </summary>
public class WriteData
{
    //数据库操作对象
    DBOperation dbOperation;

    /// <summary>
    /// 初始化读读数据功能
    /// </summary>
    /// <param 数据库的路径="path"></param>
    public WriteData(string path)
    {
        //初始化数据库对象
        string tempPath;

#if UNITY_EDITOR||UNITY_STANDALONE_WIN
        tempPath = "data source=" + path;
#elif UNITY_ANDROID
        tempPath = "uri=file:" + path;
        Debug.Log("写:" + tempPath);
#endif
        dbOperation = new DBOperation(tempPath);
    }

    /// <summary>
    /// 插入一条新数据到数据库
    /// </summary>
    /// <param 要插入数据各个字段的值得集合="values"></param>
    public void InsertDataToSQL(string tbName, string[] values)
    {
        dbOperation.InsertDataToTable(tbName, values);
    }

    /// <summary>
    /// 更新指定存档数据
    /// </summary>
    /// <param 表名="tbName"></param>
    /// <param 更新的字段="field"></param>
    /// <param 更新字段的值="fieldValue"></param>
    /// <param 条件字段="key"></param>
    /// <param 条件字段的值="keyValue"></param>
    public void UpdataDataFromSQL(string tbName, string field, int fieldValue, string key, int keyValue)
    {
        dbOperation.UpdataDataFormTable(tbName, field, fieldValue, key, keyValue);
    }

    /// <summary>
    /// 删除存档中一行指定的数据
    /// </summary>
    /// <param 表名="tbName"></param>
    /// <param 对应字段="key"></param>
    /// <param 对应ID号="keyValue"></param>
    public void DeleteInTableData(string tbName, string key, int keyValue)
    {
        dbOperation.DeleteTableData(tbName, key, keyValue);
    }
}
