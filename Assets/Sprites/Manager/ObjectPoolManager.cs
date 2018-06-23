using System.Collections;
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
    /// 在池库中找需要的块，没有就生成新的池添加进字典
    /// </summary>
    /// <param 块预制体="obj"></param>
    /// <returns></returns>
    internal GameObject InstantiateBlockObject(GameObject block)
    {
        if (!objectPoolDictionary.ContainsKey(block.name))
        {
            ObjectPoolBase newPool = new ObjectPoolBase(block);
            objectPoolDictionary.Add(newPool.Name, newPool);
        }
        return objectPoolDictionary[block.name].InstantiateBlock();
    }

    /// <summary>
    /// 回收块到对应的池中
    /// </summary>
    /// <param 块="block"></param>
    internal void RecycleBlockObject(GameObject block)
    {
        foreach (ObjectPoolBase pool in objectPoolDictionary.Values)
        {
            if (block.name == pool.Name)
            {
                pool.RecycleBlock(block);
                block.transform.parent = transform;
                break;
            }
        }
    }
}
