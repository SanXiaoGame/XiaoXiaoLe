﻿using System;
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
    //交换块开关
    internal bool props_CubeChangeSwitch = false;
    //破坏块开关
    internal bool props_CubeBreakSwitch = false;
    //技能块生成开关
    internal bool props_SkillCubeSwitch = false;

    protected override void Awake()
    {
        base.Awake();
        playingObjectPrefabs = ResourcesManager.Instance.FindBlockAll(BlockObjectType.NormalType);
        Input.multiTouchEnabled = false;
    }

    private void Start()
    {
        //AssignNeighbours(0.5f);
        //AudioManager.Instance.ReplaceBGM(BGM.maincity);
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
                ColumnManager.Instance.gameColumns[i].CallAddMissingBlock(delay);
                delay += 0.05f;
            }
        }
        //指派邻居
        AssignNeighbours(delay + 0.1f);
    }

    /// <summary>
    /// 指派(赋值)相连块
    /// </summary>
    /// <param 延迟调用的时间="delay"></param>
    internal void AssignNeighbours(float delay)
    {
        vp_Timer.In(delay, new vp_Timer.Callback(delegate() 
        {
            for (int i = 0; i < ColumnManager.Instance.gameColumns.Length; i++)
            {
                ColumnManager.Instance.gameColumns[i].AssignNeighbours();
            }
            //检查全部块状态
            CheckBoardState();
        }));
    }

    /// <summary>
    /// 检查全部块状态
    /// </summary>
    internal void CheckBoardState()
    {
        //print("检查全部块状态");
        vp_Timer.In(objectFallingDuration, new vp_Timer.Callback(delegate() 
        {
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
                RemoveBlock();
                AddMissingBlock();
            }
        }));
    }

    /// <summary>
    /// 去除块
    /// </summary>
    internal void RemoveBlock()
    {
        //播放消的声音
        AudioManager.Instance.PlayEffectMusic(SoundEffect.ClearCube);
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
    /// 退出游戏关闭所有协程
    /// </summary>
    private void OnApplicationQuit()
    {
        print("关闭所有协程");
        StopAllCoroutines();
    }
}
