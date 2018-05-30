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
    /// <param name="go"></param>
    /// <returns></returns>
    public static UISceneWidget Get(GameObject go)
    {
        UISceneWidget widget = go.GetComponent<UISceneWidget>();
        if (!widget) widget = go.AddComponent<UISceneWidget>();
        return widget;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (BeginDrag != null) BeginDrag(eventData);
    }
    public override void OnCancel(BaseEventData eventData)
    {
        if (Cancel != null) Cancel(eventData);
    }
    public override void OnDeselect(BaseEventData eventData)
    {
        if (Deselect != null) Deselect(eventData);
    }
    public override void OnDrag(PointerEventData eventData)
    {
        if (Drag != null) Drag(eventData);
    }
    public override void OnDrop(PointerEventData eventData)
    {
        if (Drop != null) Drop(eventData);
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (EndDrag != null) EndDrag(eventData);
    }
    public override void OnInitializePotentialDrag(PointerEventData eventData)
    {
        if (InitializePotentialDrag != null) InitializePotentialDrag(eventData);
    }
    public override void OnMove(AxisEventData eventData)
    {
        if (Move != null) Move(eventData);
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (PointerClick != null) PointerClick(eventData);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (PointerDown != null) PointerDown(eventData);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (PointerEnter != null) PointerEnter(eventData);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (PointerExit != null) PointerExit(eventData);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (PointerUp != null) PointerUp(eventData);
    }
    public override void OnScroll(PointerEventData eventData)
    {
        if (Scroll != null) Scroll(eventData);
    }
    public override void OnSelect(BaseEventData eventData)
    {
        if (Select != null) Select(eventData);
    }
    public override void OnSubmit(BaseEventData eventData)
    {
        if (Submit != null) Submit(eventData);
    }
    public override void OnUpdateSelected(BaseEventData eventData)
    {
        if (UpdateSelected != null) UpdateSelected(eventData);
    }
}
