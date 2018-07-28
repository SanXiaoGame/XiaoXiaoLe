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
    //游戏预制体的队列
    Queue<GameObject> gameObjectPrefab = new Queue<GameObject>();

    /// <summary>
    /// 用于实例化，并查找是否有库存
    /// </summary>
    internal GameObject InstantiateGameObject()
    {
        GameObject obj;
        if (gameObjectPrefab.Count > 0)
        {
            obj = gameObjectPrefab.Dequeue();
        }
        else
        {
            obj = Object.Instantiate(objectPrefab, Vector3.zero, Quaternion.identity);
        }
        obj.SetActive(true);
        return obj;
    }
    /// <summary>
    /// 回收快进入队列
    /// </summary>
    /// <param 游戏物体="block"></param>
    internal void RecycleGameObject(GameObject obj)
    {
        obj.SetActive(false);
        gameObjectPrefab.Enqueue(obj);
    }

    /// <summary>
    /// 清空队列
    /// </summary>
    internal void EmptyQueue()
    {
        foreach (GameObject obj in gameObjectPrefab)
        {
            Object.Destroy(obj);
        }
        gameObjectPrefab.Clear();
    }
}
