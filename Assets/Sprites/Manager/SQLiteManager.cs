using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 数据库管理类
/// </summary>
public class SQLiteManager : ManagerBase<SQLiteManager>
{
    //拥有一个拷贝路径类的实例
    DataStreamsLoading StreamsLoading;

    //读数据功能
    ReadData readData;
    //写数据功能
    WriteData writeData;

    //数据源：用来保存读到的数据模型
    public Dictionary<int, ItemData> dataSource;

    protected override void Awake()
    {
        base.Awake();
        //初始化数据源字典
        if (dataSource == null)
        {
            dataSource = new Dictionary<int, ItemData>();
        }

        //启动流路径(将需要操作的文件从流路径拷贝到沙盒中)
        StreamsLoading = gameObject.AddComponent<DataStreamsLoading>();

        //是否拷贝完成
        StreamsLoading.onCopyFinished += OnLoadFinished;
        //开始拷贝（输入对应的表名）
        StreamsLoading.LoadWitgPath(new string[] { "输入对应的表名","表名" });
    }

    /// <summary>
    /// 当文件从流路径拷贝到沙盒路径完成时执行的回调函数，一旦该函数被成功回调，则意味着拷贝文件已完成，此时可执行存取等数据库操作
    /// </summary>
    string dataBasePath;
    void OnLoadFinished()
    {
        Debug.Log("路径拷贝完成");
        //数据库存放沙盒的路径
        dataBasePath = System.IO.Path.Combine(Application.persistentDataPath, "输入对应的表名");
        //初始化读和写的功能
        readData = new ReadData(dataBasePath);
        writeData = new WriteData(dataBasePath);

        //取出数据存入字典
        readData.GetData("表名01");
    }

    /// <summary>
    /// 插入一条数据到数据库(没完成)
    /// </summary>
    /// <param name="s_id"></param>
    /// <param name="s_name"></param>
    /// <param name="s_ave"></param>
    public void InsetDataToTable(string s_id, string s_name, string s_ave)
    {
        string[] values = new string[] { s_id, s_name, s_ave };
        writeData.InsertDataToSQL(values);
    }

    /// <summary>
    /// 更新数据中的某条数据(没完成)
    /// </summary>
    /// <param name="cols"></param>
    /// <param name="colsValue"></param>
    /// <param name="key"></param>
    /// <param name="keyValue"></param>
    public void UpdataDataFromTable(string cols, int colsValue, string key, string keyValue)
    {
        writeData.UpdataDataFromSQL(cols, colsValue, key, keyValue);
    }
}
