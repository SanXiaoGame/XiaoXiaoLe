using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;

public class SpecialBlockObject : MonoBehaviour
{
    //定义UI点击事件的基类
    UISceneWidget blockClick;
    BlockObject myPlayingObject;
    private void Start()
    {
        myPlayingObject = GetComponent<BlockObject>();
        //绑定块的点击事件
        blockClick = gameObject.AddComponent<UISceneWidget>();

        transform.DOScale(Vector3.one, 0.35f).OnComplete(delegate() 
        {
            if (blockClick != null)
            {
                blockClick.PointerDown += BlockOnPointerDown;
            }
        });
    }

    private void BlockOnPointerDown(PointerEventData eventData)
    {
        print("点击了特殊块");
        if (myPlayingObject.objectType == BlockObjectType.SkillType)
        {
            //技能块
        }
        else
        {
            //高级技能块
        }
        ObjectPoolManager.Instance.RecycleBlockObject(gameObject);
    }
}
