using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 泛型单例管理类
/// </summary>
public class ManagerBase<T> : MonoBehaviour where T : ManagerBase<T>
{
    static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject(typeof(T).Name).AddComponent<T>();
                DontDestroyOnLoad(_instance);
            }
            return _instance;
        }
    }
    //用于其它管理类的单例初始化
    protected virtual void Awake()
    {
        _instance = this as T;
    }
}
