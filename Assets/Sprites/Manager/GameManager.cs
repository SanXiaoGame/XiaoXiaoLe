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
    //交换块开关
    internal bool props_CubeChangeSwitch = false;
    //破坏块开关
    internal bool props_CubeBreakSwitch = false;
    //技能块生成开关
    internal bool props_SkillCubeSwitch = false;
    //关卡固定奖励金币
    internal Dictionary<string, int> fixedGoldCoin;
    //关卡掉落物品列表
    internal Dictionary<string, List<int>> stageDropItemList;

    protected override void Awake()
    {
        base.Awake();
        playingObjectPrefabs = ResourcesManager.Instance.FindBlockAll(BlockObjectType.NormalType);
        Input.multiTouchEnabled = false;
        fixedGoldCoin = new Dictionary<string, int>();
        stageDropItemList = new Dictionary<string, List<int>>();
        fixedGoldCoin.Clear();
        fixedGoldCoin.Add(ConstData.Stage01, 6700);
        fixedGoldCoin.Add(ConstData.Stage02, 8648);
        fixedGoldCoin.Add(ConstData.Stage03, 16358);
        fixedGoldCoin.Add(ConstData.Stage04, 23646);
        fixedGoldCoin.Add(ConstData.Stage05, 29531);
        fixedGoldCoin.Add(ConstData.Stage06, 36486);
        fixedGoldCoin.Add(ConstData.Stage07, 45318);
        fixedGoldCoin.Add(ConstData.Stage08, 56169);
        fixedGoldCoin.Add(ConstData.Stage09, 64248);
        fixedGoldCoin.Add(ConstData.Stage10, 79586);
        stageDropItemList.Clear();
        stageDropItemList.Add(ConstData.Stage01, new List<int>() { 2003,2018,2053,2054,2043 });
        stageDropItemList.Add(ConstData.Stage02, new List<int>() { 2030,2019,2055,2004,2031,2005,2020,2044,2056,2006,2112,2103 });
        stageDropItemList.Add(ConstData.Stage03, new List<int>() { 2006,2007,2021,2022,2032,2033,2034,2045,2046,2057,2058,2107,2117,2122 });
        stageDropItemList.Add(ConstData.Stage04, new List<int>() { 2008,2022,2023,2034,2035,2047,2059,2123 });
        stageDropItemList.Add(ConstData.Stage05, new List<int>() { 2008,2009,2023,2024,2036,2047,2048,2059,2060,2104 });
        stageDropItemList.Add(ConstData.Stage06, new List<int>() { 2010,2011,2025,2036,2037,2048,2049,2060,2061,2113,2114,2118 });
        stageDropItemList.Add(ConstData.Stage07, new List<int>() { 2011,2012,2013,2026,2037,2038,2050,2061,2062,2108,2119,2105 });
        stageDropItemList.Add(ConstData.Stage08, new List<int>() { 2013,2014,2027,2039,2050,2051,2062,2063,2109,2115 });
        stageDropItemList.Add(ConstData.Stage09, new List<int>() { 2014,2027,2039,2051,2063,2015,2124,2110 });
        stageDropItemList.Add(ConstData.Stage10, new List<int>() { 2120,2125 });
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
            if (ColumnManager.Instance.gameColumns != null)
            {
                for (int i = 0; i < ColumnManager.Instance.gameColumns.Length; i++)
                {
                    ColumnManager.Instance.gameColumns[i].AssignNeighbours();
                }
                //检查全部块状态
                CheckBoardState();
            }
        }));
    }

    /// <summary>
    /// 检查全部块状态
    /// </summary>
    internal void CheckBoardState()
    {
        vp_Timer.In(objectFallingDuration, new vp_Timer.Callback(delegate() 
        {
            doesHaveBrustItem = false;
            if (ColumnManager.Instance.gameColumns!=null)
            {
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
