using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 块的点击功能类
/// </summary>
public class BlockObjectTouch : UISceneWidget
{
    //块身上的基类
    BlockObject blockObject;
    //定义UI点击事件的基类
    UISceneWidget blockClick;

    private void Awake()
    {
        blockObject = GetComponent<BlockObject>();
    }

    private void Start()
    {
        //绑定块的拖拽事件
        blockClick = GetComponent<UISceneWidget>();
        if (blockClick != null)
        {
            blockClick.Select += BlockSelect;
            blockClick.BeginDrag += BlockOnBeginDrag;
            blockClick.Drag += BlockOnDrag;
            blockClick.EndDrag += BlockOnEndDrag;
        }
    }

    //块选中的方法,未实现
    private void BlockSelect(BaseEventData eventData)
    {
        
    }

    //块开始拖拽,未实现
    private void BlockOnBeginDrag(PointerEventData eventData)
    {
        
    }

    //块拖拽中,未实现
    private void BlockOnDrag(PointerEventData eventData)
    {

    }

    //块拖拽结束,未实现
    private void BlockOnEndDrag(PointerEventData eventData)
    {

    }
}
