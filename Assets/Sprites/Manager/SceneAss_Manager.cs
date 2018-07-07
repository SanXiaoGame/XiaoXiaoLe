using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有场景管理类
/// </summary>
public class SceneAss_Manager : ManagerBase<SceneAss_Manager>
{
    //启用对应场景的事件
    internal event Action<int> readDataEnd;

    /// <summary>
    /// 启用协程
    /// </summary>
    /// <param 场景名="name"></param>
    internal void ExecutionOfEvent(int name)
    {
        readDataEnd(name);
    }
}
