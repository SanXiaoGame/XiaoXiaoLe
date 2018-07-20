using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_GuidenStart : MonoBehaviour,IUIBase
{
    //确定按钮
    GameObject confirm;
    //取消按钮
    GameObject cancel;
    //添加事件基类
    UISceneWidget confirmButtonClick;
    UISceneWidget cancelButtonClick;
    public void OnEntering()
    {
        gameObject.SetActive(true);

    }

    public void OnExiting()
    {
        gameObject.SetActive(false);

    }

    public void OnPausing()
    {
        gameObject.SetActive(false);

    }

    public void OnResuming()
    {
        gameObject.SetActive(true);

    }
    // Use this for initialization
    void Start ()
    {
        confirm = transform.Find("BG/Confirm").gameObject;
        cancel = transform.Find("BG/Cancel").gameObject;
        confirmButtonClick = UISceneWidget.Get(confirm);
        cancelButtonClick = UISceneWidget.Get(cancel);
        //绑定事件
        if (confirmButtonClick != null && cancelButtonClick != null)
        {
            confirmButtonClick.PointerClick += ConfirmButtonFunc;
            cancelButtonClick.PointerClick += CancelButtonFunc;
        }
    }

    public void ConfirmButtonFunc(PointerEventData eventData)
    {
        Debug.Log("开始引导");
        UIManager.Instance.PushUIStack("UI_GuidenStory");        //进入引导界面
    }
    /// <summary>
    /// 取消引导
    /// </summary>
    /// <param name="eventData"></param>
    public void CancelButtonFunc(PointerEventData eventData)
    {
        Debug.Log("取消引导");
        UIManager.Instance.PopUIStack();        //退出当前界面
    }

}
