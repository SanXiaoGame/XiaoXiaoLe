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
    BlockObject _blockObject;
    #region 消块的临时数量存储
    int Berserker = 0;
    int Caster = 0;
    int Hunter = 0;
    int Knight = 0;
    int Saber = 0;
    #endregion

    private void Start()
    {
        _blockObject = GetComponent<BlockObject>();
        //绑定块的点击事件
        blockClick = UISceneWidget.Get(gameObject);
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
        //清零临时记录
        Berserker = 0;
        Caster = 0;
        Hunter = 0;
        Knight = 0;
        Saber = 0;

        if (!GameManager.Instance.isBusy)
        {
            if (eventData.pointerEnter.tag == ConstData.SkillBlock)
            {
                //技能块
                _blockObject.brust = true;
                for (int i = 0; i < _blockObject.adjacentItems.Length; i++)
                {
                    if (_blockObject.adjacentItems[i] != null && _blockObject.adjacentItems[i].gameObject.activeSelf)
                    {
                        _blockObject.adjacentItems[i].brust = true;
                        if (i == 0 || i == 1)
                        {
                            if (_blockObject.adjacentItems[i].adjacentItems[2] != null)
                            {
                                _blockObject.adjacentItems[i].adjacentItems[2].brust = true;
                            }
                            if (_blockObject.adjacentItems[i].adjacentItems[3] != null)
                            {
                                _blockObject.adjacentItems[i].adjacentItems[3].brust = true;
                            }
                        }
                        //记录消除的块数量
                        switch (_blockObject.adjacentItems[i].name)
                        {
                            case ConstData.Berserker:
                                Berserker++;
                                break;
                            case ConstData.Caster:
                                Caster++;
                                break;
                            case ConstData.Hunter:
                                Hunter++;
                                break;
                            case ConstData.Knight:
                                Knight++;
                                break;
                            case ConstData.Saber:
                                Saber++;
                                break;
                        }
                    }
                }
                //计分
                GameManager.Instance.AddScore(ConstData.BlastSkill);
            }
            else if(eventData.pointerEnter.tag == ConstData.SpecialBlock)
            {
                //高级技能块
                for (int i = 0; i < ColumnManager.Instance.numberOfColumns; i++)
                {
                    for (int j = 0; j < ColumnManager.Instance.numberOfRows; j++)
                    {
                        ColumnManager.Instance.gameColumns[i].BlockObjectsScriptList[j].brust = true;
                        //记录消除的块数量
                        switch (ColumnManager.Instance.gameColumns[i].BlockObjectsScriptList[j].name)
                        {
                            case ConstData.Berserker:
                                Berserker++;
                                break;
                            case ConstData.Caster:
                                Caster++;
                                break;
                            case ConstData.Hunter:
                                Hunter++;
                                break;
                            case ConstData.Knight:
                                Knight++;
                                break;
                            case ConstData.Saber:
                                Saber++;
                                break;
                        }
                    }
                }

                #region 全屏块的最终技能
                if (Berserker >= 3)
                {
                    Berserker = 3;
                    if (Berserker >= 4)
                    {
                        Berserker = 4;
                        if (Berserker >= 5)
                        {
                            Berserker = 5;
                        }
                    }
                }
                if (Caster >= 3)
                {
                    Caster = 3;
                    if (Caster >= 4)
                    {
                        Caster = 4;
                        if (Caster >= 5)
                        {
                            Caster = 5;
                        }
                    }
                }
                if (Hunter >= 3)
                {
                    Hunter = 3;
                    if (Hunter >= 4)
                    {
                        Hunter = 4;
                        if (Hunter >= 5)
                        {
                            Hunter = 5;
                        }
                    }
                }
                if (Knight >= 3)
                {
                    Knight = 3;
                    if (Knight >= 4)
                    {
                        Knight = 4;
                        if (Knight >= 5)
                        {
                            Knight = 5;
                        }
                    }
                }
                if (Saber >= 3)
                {
                    Saber = 3;
                    if (Saber >= 4)
                    {
                        Saber = 4;
                        if (Saber >= 5)
                        {
                            Saber = 5;
                        }
                    }
                }
                #endregion

            }
            //计分
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
