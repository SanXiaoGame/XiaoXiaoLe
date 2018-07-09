using System;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 界面控件基类
/// </summary>
public class UISceneWidget : EventTrigger
{
    public event Action<PointerEventData> BeginDrag;
    public event Action<BaseEventData> Cancel;
    public event Action<BaseEventData> Deselect;
    public event Action<PointerEventData> Drag;
    public event Action<PointerEventData> Drop;
    public event Action<PointerEventData> EndDrag;
    public event Action<PointerEventData> InitializePotentialDrag;
    public event Action<AxisEventData> Move;
    public event Action<PointerEventData> PointerClick;
    public event Action<PointerEventData> PointerDown;
    public event Action<PointerEventData> PointerEnter;
    public event Action<PointerEventData> PointerExit;
    public event Action<PointerEventData> PointerUp;
    public event Action<PointerEventData> Scroll;
    public event Action<BaseEventData> Select;
    public event Action<BaseEventData> Submit;
    public event Action<BaseEventData> UpdateSelected;

    /// <summary>
    /// 获取指定UGUI游戏物体的事件监听器
    /// </summary>
    /// <param 指定物体="obj"></param>
    /// <returns></returns>
    public static UISceneWidget Get(GameObject obj)
    {
        UISceneWidget widget = obj.GetComponent<UISceneWidget>();
        if (!widget)
        {
            widget = obj.AddComponent<UISceneWidget>();
        }
        return widget;
    }

    /// <summary>
    /// 开始拖动
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (BeginDrag != null) BeginDrag(eventData);
    }
    /// <summary>
    /// 按下取消按钮时调用
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnCancel(BaseEventData eventData)
    {
        if (Cancel != null) Cancel(eventData);
    }
    /// <summary>
    /// 对选定对象的调用变为取消选择
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnDeselect(BaseEventData eventData)
    {
        if (Deselect != null) Deselect(eventData);
    }
    /// <summary>
    /// 当拖动发生时调用拖动对象
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnDrag(PointerEventData eventData)
    {
        if (Drag != null) Drag(eventData);
    }
    /// <summary>
    /// 拖动中时
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnDrop(PointerEventData eventData)
    {
        if (Drop != null) Drop(eventData);
    }
    /// <summary>
    /// 拖动结束
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (EndDrag != null) EndDrag(eventData);
    }
    /// <summary>
    /// 当找到拖动目标时调用
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnInitializePotentialDrag(PointerEventData eventData)
    {
        if (InitializePotentialDrag != null) InitializePotentialDrag(eventData);
    }
    /// <summary>
    /// 当移动事件发生时
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnMove(AxisEventData eventData)
    {
        if (Move != null) Move(eventData);
    }
    /// <summary>
    /// 鼠标点击
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (PointerClick != null) PointerClick(eventData);
    }
    /// <summary>
    /// 鼠标按下
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (PointerDown != null) PointerDown(eventData);
    }
    /// <summary>
    /// 鼠标进入
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (PointerEnter != null) PointerEnter(eventData);
    }
    /// <summary>
    /// 鼠标离开
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (PointerExit != null) PointerExit(eventData);
    }
    /// <summary>
    /// 鼠标弹起
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (PointerUp != null) PointerUp(eventData);
    }
    /// <summary>
    /// 鼠标滚轮滚动时调用
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnScroll(PointerEventData eventData)
    {
        if (Scroll != null) Scroll(eventData);
    }
    /// <summary>
    /// 第一响应
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnSelect(BaseEventData eventData)
    {
        if (Select != null) Select(eventData);
    }
    /// <summary>
    /// 提交时
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnSubmit(BaseEventData eventData)
    {
        if (Submit != null) Submit(eventData);
    }
    /// <summary>
    /// 更新选择
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnUpdateSelected(BaseEventData eventData)
    {
        if (UpdateSelected != null) UpdateSelected(eventData);
    }
}
