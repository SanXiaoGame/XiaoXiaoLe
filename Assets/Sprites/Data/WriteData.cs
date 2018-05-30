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
#endif
        dbOperation = new DBOperation(tempPath);
    }

    /// <summary>
    /// 插入一条数据到数据库
    /// </summary>
    /// <param 要插入数据各个字段的值得集合="values"></param>
    public void InsertDataToSQL(string[] values)
    {
        dbOperation.InsertDataToTable("表名", values);
    }

    /// <summary>
    /// 更新数据中的某条数据
    /// </summary>
    /// <param name="cols"></param>
    /// <param name="colsValue"></param>
    /// <param name="key"></param>
    /// <param name="keyValue"></param>
    public void UpdataDataFromSQL(string cols, int colsValue, string key, string keyValue)
    {
        dbOperation.UpdataDataFormTable("表名", cols, colsValue, key, keyValue);
    }
}
