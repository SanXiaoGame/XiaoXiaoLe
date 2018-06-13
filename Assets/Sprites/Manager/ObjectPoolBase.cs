using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolBase
{
    GameObject objectPrefab;
    internal string Name
    {
        get
        {
            return objectPrefab.name;
        }
    }
    internal ObjectPoolBase(GameObject obj)
    {
        objectPrefab = obj;
    }
    //块预制体的队列
    Queue<GameObject> blockObjectPrefab = new Queue<GameObject>();

    /// <summary>
    /// 用于块的实例化，并查找是否有库存
    /// </summary>
    internal GameObject InstantiateBlock()
    {
        GameObject block;
        if (blockObjectPrefab.Count > 0)
        {
            block = blockObjectPrefab.Dequeue();
        }
        else
        {
            block = Object.Instantiate(objectPrefab, Vector3.zero, Quaternion.identity);
            block.name = objectPrefab.name;
        }
        block.SetActive(true);
        return block;
    }
    /// <summary>
    /// 回收快进入队列
    /// </summary>
    /// <param 块="block"></param>
    internal void RecycleBlock(GameObject block)
    {
        block.SetActive(false);
        blockObjectPrefab.Enqueue(block);
    }
}
