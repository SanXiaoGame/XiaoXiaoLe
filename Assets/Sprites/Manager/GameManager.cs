using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏基础功能管理类
/// </summary>
public class GameManager : ManagerBase<GameManager>
{
    //一次被消除的块数量
    internal int RemoveBlockNumber;
    //普通块所有预制体
    internal Object[] playingObjectPrefabs;
    //初始块种类数量 默认4
    internal int normalBlockNumber = 4;
    //块下降的时间 默认0.45
    internal float objectFallingDuration = 0.45f;

    protected override void Awake()
    {
        base.Awake();
        playingObjectPrefabs = ResourcesManager.Instance.FindBlockAll(BlockObjectType.NormalType);
    }

    private void Start()
    {
        Invoke("AssignNeighbours", .5f);
    }

    /// <summary>
    /// 指派(赋值)相连块
    /// </summary>
    internal void AssignNeighbours()
    {
        for (int i = 0; i < ColumnManager.Instance.gameColumns.Length; i++)
        {
            ColumnManager.Instance.gameColumns[i].AssignNeighbours();
        }


    }

    /// <summary>
    /// 计分
    /// </summary>
    internal void AddScore()
    {

    }
}
