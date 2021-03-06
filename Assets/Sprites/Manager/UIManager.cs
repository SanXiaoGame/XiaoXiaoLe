﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// UI界面管理
/// </summary>
public class UIManager : ManagerBase<UIManager>
{
    //游戏暂停开关
    bool isGamePause = false;
    //UI堆栈
    Stack<IUIBase> UIStack = new Stack<IUIBase>();
    //保存所有进栈的UI界面
    Dictionary<string, IUIBase> CurrentUI = new Dictionary<string, IUIBase>();
    //保存所有进栈的UI界面
    //internal List<GameObject> UIPrefabList = new List<GameObject>();
    //所有UI的父级画布
    Transform uiParent;
    //控制退出按钮的点击次数
    internal bool isClickEscape = false;

    /// <summary>
    /// 检测退出按钮的点击
    /// </summary>
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape) && !isClickEscape)
    //    {
    //        isClickEscape = true;
    //        //显示退出界面
    //        PushUIStack(ConstData.UIExitGame);
    //    }
    //}

    /// <summary>
    /// 游戏暂停
    /// </summary>
    internal void GamePause()
    {
        isGamePause = !isGamePause;
        Time.timeScale = isGamePause == true ? 0 : 1;
    }

    /// <summary>
    /// 实例化界面,并返回当前界面
    /// </summary>
    /// <param UI的名字="uiname"></param>
    /// <returns></returns>
    IUIBase GetCurrentUI(string uiname)
    {
        //遍历是否有对应元素
        foreach (string name in CurrentUI.Keys)
        {
            if (uiname == name)
            {
                return CurrentUI[uiname];
            }
        }
        //给新加的UI找画布
        if (uiParent == null)
        {
            uiParent = transform.Find(ConstData.CanvasName);
        }
        //生成资源中取出的预制体
        GameObject obj = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindUIPrefab(uiname));
        //UI界面名字一致性
        obj.name = uiname;
        //统一父级
        obj.transform.parent = uiParent;
        //局部坐标
        obj.transform.localPosition = Vector3.zero;
        //UI的缩放
        obj.transform.localScale = Vector3.one;
        //添加对应的UI脚本
        if (obj.GetComponent(Type.GetType(uiname)) == null)
        {
            obj.AddComponent(Type.GetType(uiname));
        }
        //取得界面脚本的基础接口
        IUIBase iuibase = obj.GetComponent<IUIBase>();
        //新生成的UI加入CurrentUI的字典
        CurrentUI.Add(uiname, iuibase);
        //添加场景中的所有UI预制体
        //UIPrefabList.Add(obj);
        //返回一个新生成的UI界面
        return iuibase;
    }

    /// <summary>
    /// 界面入栈
    /// </summary>
    /// <param name="name"></param>
    public void PushUIStack(string uiname)
    {
        if (UIStack.Count > 0)
        {
            //返回栈顶的界面，且不移除
            IUIBase old_Pop = UIStack.Peek();
            //保留之前的界面
            old_Pop.OnPausing();
        }
        //创建界面
        IUIBase new_Pop = GetCurrentUI(uiname);
        //界面进栈顶部
        UIStack.Push(new_Pop);
        //进入当前界面
        new_Pop.OnEntering();
    }

    /// <summary>
    /// 界面出栈
    /// </summary>
    public void PopUIStack()
    {
        //没有界面元素
        if (UIStack.Count == 0)
        {
            return;
        }
        //出栈,并移除界面
        IUIBase old_pop = UIStack.Pop();
        old_pop.OnExiting();
        //有界面元素
        if (UIStack.Count > 0)
        {
            //推出旧的界面,重新显示栈顶界面
            IUIBase newPop = UIStack.Peek();
            newPop.OnResuming();
        }
    }

    /// <summary>
    /// 清理UI数据
    /// </summary>
    internal void ClearUIData()
    {
        //清除画布
        if (uiParent != null)
        {
            uiParent = null;
        }

        if (UIStack.Count > 0) { UIStack.Clear(); }
        if (CurrentUI.Count > 0) { CurrentUI.Clear(); }
    }
}
