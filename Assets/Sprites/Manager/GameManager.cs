using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏基础功能管理类
/// </summary>
public class GameManager : ManagerBase<GameManager>
{
    //普通块所有预制体
    internal object[] playingObjectPrefabs;
    //初始块种类数量 默认5
    internal int normalBlockNumber = 5;
    //块下降的时间
    internal float objectFallingDuration = 0.35f;
    //是否在造作中
    internal bool isBusy = false;
    //是否有可以消的块
    internal bool doesHaveBrustItem = false;
    //总分，结算用
    internal int totalScore = 0;
    //游戏暂停开关
    bool isGamePause = false;

    protected override void Awake()
    {
        base.Awake();
        playingObjectPrefabs = ResourcesManager.Instance.FindBlockAll(BlockObjectType.NormalType);
        Input.multiTouchEnabled = false;
    }

    private void Start()
    {
        Invoke("AssignNeighbours", 0.5f);
    }

    /// <summary>
    /// 添加缺失块
    /// </summary>
    internal void AddMissingBlock()
    {
        float delay = 0;
        for (int i = 0; i < ColumnManager.Instance.gameColumns.Length; i++)
        {
            if (ColumnManager.Instance.gameColumns[i].GetNumberOfItemsToAdd() > 0)
            {
                ColumnManager.Instance.gameColumns[i].Invoke("AddMissingBlock", delay);
                delay += 0.05f;
            }
        }
        //指派邻居
        Invoke("AssignNeighbours", delay + 0.1f);
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
        //检查全部块状态
        Invoke("CheckBoardState", Instance.objectFallingDuration);
    }

    /// <summary>
    /// 检查全部块状态
    /// </summary>
    internal void CheckBoardState()
    {
        //print("检查全部块状态");
        doesHaveBrustItem = false;
        for (int i = 0; i < ColumnManager.Instance.gameColumns.Length; i++)
        {
            for (int j = 0; j < ColumnManager.Instance.gameColumns[i].BlockObjectsScriptList.Count; j++)
            {
                if (ColumnManager.Instance.gameColumns[i].BlockObjectsScriptList[j] != null)
                {
                    ColumnManager.Instance.gameColumns[i].BlockObjectsScriptList[j].CheckIfCanBrust();
                }
            }
        }

        if (doesHaveBrustItem)
        {
            //播放消的声音

            RemoveBlock();
            AddMissingBlock();
        }
    }

    /// <summary>
    /// 去除块
    /// </summary>
    internal void RemoveBlock()
    {
        for (int i = 0; i < ColumnManager.Instance.gameColumns.Length; i++)
        {
            ColumnManager.Instance.gameColumns[i].DeleteBrustedBlock();
        }
    }

    /// <summary>
    /// 计分算战果
    /// </summary>
    internal void AddScore(int number)
    {
        totalScore += number;
    }

    /// <summary>
    /// 游戏暂停
    /// </summary>
    internal void GamePause()
    {
        isGamePause = !isGamePause;
        Time.timeScale = isGamePause == true ? 0 : 1;
    }

    /// <summary>
    /// 退出游戏关闭所有协程
    /// </summary>
    private void OnApplicationQuit()
    {
        StopAllCoroutines();
    }
}
