using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 关卡的管理类
/// </summary>
public class LevelManager : ManagerBase<LevelManager>
{
    //一关总列数
    internal int numberOfColumns;
    //一关总行数 默认6
    internal int numberOfRows = 6;

    protected override void Awake()
    {
        base.Awake();
        numberOfColumns = transform.childCount;
    }
}
