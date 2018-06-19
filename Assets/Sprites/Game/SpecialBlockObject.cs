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
    #region 消块的临时数量存储
    int Berserker = 0;
    int Caster = 0;
    int Hunter = 0;
    int Knight = 0;
    int Saber = 0;
    #endregion

    private void Start()
    {
        myPlayingObject = GetComponent<BlockObject>();
        //绑定块的点击事件
        blockClick = GetComponent<UISceneWidget>();
        if (blockClick != null)
        {
            blockClick.PointerDown += BlockOnPointerDown;
            blockClick.PointerUp += BlockOnPointerUp;
        }
    }

    /// <summary>
    /// 点击特殊块
    /// </summary>
    /// <param name="eventData"></param>
    private void BlockOnPointerDown(PointerEventData eventData)
    {
        if (!GameManager.Instance.isBusy)
        {
            if (myPlayingObject.tag == ConstData.SkillBlock)
            {
                //技能块
                myPlayingObject.brust = true;
                for (int i = 0; i < myPlayingObject.adjacentItems.Length; i++)
                {
                    if (myPlayingObject.adjacentItems[i] != null && myPlayingObject.adjacentItems[i].gameObject.activeSelf)
                    {
                        myPlayingObject.adjacentItems[i].brust = true;
                    }
                }
                GameManager.Instance.AddScore(ConstData.BlastSkill);
            }
            else if(myPlayingObject.tag == ConstData.SpecialBlock)
            {
                //高级技能块
                for (int i = 0; i < ColumnManager.Instance.numberOfColumns; i++)
                {
                    for (int j = 0; j < ColumnManager.Instance.numberOfRows; j++)
                    {
                        ColumnManager.Instance.gameColumns[i].BlockObjectsScriptList[j].brust = true;
                    }
                }
            }
            GameManager.Instance.AddScore(ConstData.SpecialSkill);
            transform.DOScale(Vector3.zero, 0.34f).OnComplete(delegate ()
            {
                GameManager.Instance.RemoveBlock();
                GameManager.Instance.AddMissingBlock();
            });
        }
    }

    /// <summary>
    /// 鼠标弹起
    /// </summary>
    /// <param name="eventData"></param>
    private void BlockOnPointerUp(PointerEventData eventData)
    {
        GameManager.Instance.isBusy = false;
    }
}
