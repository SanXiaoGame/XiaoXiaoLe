using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    /// <summary>
    /// 添加所需的管理类
    /// </summary>
    private void Awake()
    {
        //添加SQLiteManager
        GameObject obj = new GameObject(typeof(SQLiteManager).Name);
        obj.AddComponent<SQLiteManager>();
        DontDestroyOnLoad(obj);
        // 启用加载界面预制体
        UIManager.Instance.PushUIStack(ConstData.LoadingPrefab);
    }
}
