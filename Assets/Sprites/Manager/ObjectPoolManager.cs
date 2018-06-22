﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : ManagerBase<ObjectPoolManager>
{
    //所有块对象池的队列
    Dictionary<string, ObjectPoolBase> objectPoolDictionary = new Dictionary<string, ObjectPoolBase>();

    protected override void Awake()
    {
        base.Awake();
    }
    /// <summary>
    /// 在池库中找需要的游戏物体，没有就生成新的池添加进字典
    /// </summary>
    /// <param 预制体="obj"></param>
    /// <returns></returns>
    internal GameObject InstantiateMyGameObject(GameObject obj)
    {
        if (!objectPoolDictionary.ContainsKey(obj.name))
        {
            ObjectPoolBase newPool = new ObjectPoolBase(obj);
            objectPoolDictionary.Add(newPool.Name, newPool);
        }
        return objectPoolDictionary[obj.name].InstantiateGameObject();
    }

    /// <summary>
    /// 回收块到对应的池中
    /// </summary>
    /// <param 游戏物体="block"></param>
    internal void RecycleMyGameObject(GameObject obj)
    {
        foreach (ObjectPoolBase pool in objectPoolDictionary.Values)
        {
            if (obj.name == pool.Name || obj.name == StringSplicingTool.StringSplicing(pool.Name, "(Clone)"))
            {
                pool.RecycleGameObject(obj);
                obj.transform.parent = transform;
                break;
            }
        }
    }
}
