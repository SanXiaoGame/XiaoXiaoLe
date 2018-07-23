using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 退出游戏的UI
/// </summary>
public class UIExitGame : MonoBehaviour, IUIBase
{
    //确定按钮
    GameObject determine;
    //取消按钮
    GameObject cancel;
    //添加事件基类
    UISceneWidget determineButtonClick;
    UISceneWidget cancelButtonClick;

    private void Start()
    {
        UIManager.Instance.GamePause();
        determine = transform.Find("ExitBG/Determine").gameObject;
        cancel = transform.Find("ExitBG/Cancel").gameObject;
        determineButtonClick = UISceneWidget.Get(determine);
        cancelButtonClick = UISceneWidget.Get(cancel);
        //绑定事件
        if (determineButtonClick != null && cancelButtonClick != null)
        {
            determineButtonClick.PointerClick += DetermineButtonFunc;
            cancelButtonClick.PointerClick += CancelButtonFunc;
        }
    }
    /// <summary>
    /// 进入界面
    /// </summary>
    public void OnEntering()
    {
        gameObject.SetActive(true);
    }
    /// <summary>
    /// 界面暂停
    /// </summary>
    public void OnPausing()
    {
        gameObject.SetActive(false);
    }
    /// <summary>
    /// 界面唤醒
    /// </summary>
    public void OnResuming()
    {
        gameObject.SetActive(true);
    }
    /// <summary>
    /// 界面退出
    /// </summary>
    public void OnExiting()
    {
        gameObject.SetActive(false);
        UIManager.Instance.GamePause();
        UIManager.Instance.isClickEscape = false;
    }

    /// <summary>
    /// 点击确定按钮后的方法
    /// </summary>
    void DetermineButtonFunc(PointerEventData eventData)
    {
        //退出游戏
        print("退出游戏");
        Application.Quit();
    }

    /// <summary>
    /// 点击取消按钮后的方法
    /// </summary>
    void CancelButtonFunc(PointerEventData eventData)
    {
        UIManager.Instance.PopUIStack();
    }
}