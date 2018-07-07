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

    int Berserker2 = 0;
    int Caster2 = 0;
    int Hunter2 = 0;
    int Knight2 = 0;
    int Saber2 = 0;
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
        if (GameManager.Instance.props_CubeBreakSwitch || GameManager.Instance.props_CubeChangeSwitch || GameManager.Instance.props_SkillCubeSwitch)
        {
            return;
        }

        //清零临时记录
        Berserker = 0;
        Caster = 0;
        Hunter = 0;
        Knight = 0;
        Saber = 0;
        Berserker2 = 0;
        Caster2 = 0;
        Hunter2 = 0;
        Knight2 = 0;
        Saber2 = 0;

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
                //调用技能
                if (Saber > 0)
                {
                    SkillManager.Instance.A_ClassSkill(ConstData.Saber);
                }
                if (Knight > 0)
                {
                    SkillManager.Instance.A_ClassSkill(ConstData.Knight);
                }
                if (Berserker > 0)
                {
                    SkillManager.Instance.A_ClassSkill(ConstData.Berserker);
                }
                if (Caster > 0)
                {
                    SkillManager.Instance.A_ClassSkill(ConstData.Caster);
                }
                if (Hunter > 0)
                {
                    SkillManager.Instance.A_ClassSkill(ConstData.Hunter);
                }
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
                if (Berserker >= 1)
                {
                    Berserker2 = 1;
                    if (Berserker >= 4)
                    {
                        Berserker2 = 4;
                        if (Berserker >= 6)
                        {
                            Berserker2 = 6;
                        }
                    }
                }
                if (Caster >= 1)
                {
                    Caster2 = 1;
                    if (Caster >= 4)
                    {
                        Caster2 = 4;
                        if (Caster >= 6)
                        {
                            Caster2 = 6;
                        }
                    }
                }
                if (Hunter >= 1)
                {
                    Hunter2 = 1;
                    if (Hunter >= 4)
                    {
                        Hunter2 = 4;
                        if (Hunter >= 6)
                        {
                            Hunter2 = 6;
                        }
                    }
                }
                if (Knight >= 1)
                {
                    Knight2 = 1;
                    if (Knight >= 4)
                    {
                        Knight2 = 4;
                        if (Knight >= 6)
                        {
                            Knight2 = 6;
                        }
                    }
                }
                if (Saber >= 1)
                {
                    Saber2 = 1;
                    if (Saber >= 4)
                    {
                        Saber2 = 4;
                        if (Saber >= 6)
                        {
                            Saber2 = 6;
                        }
                    }
                }
                #endregion
                if (Saber2 == 1)
                {
                    SkillManager.Instance.A_ClassSkill(ConstData.Saber);
                }
                else if (Saber2 == 4)
                {
                    SkillManager.Instance.B_ClassSkill(ConstData.Saber);
                }
                else if (Saber2 == 6)
                {
                    SkillManager.Instance.C_ClassSkill(ConstData.Saber);
                }
                if (Knight2 == 1)
                {
                    SkillManager.Instance.A_ClassSkill(ConstData.Knight);
                }
                else if (Knight2 == 4)
                {
                    SkillManager.Instance.B_ClassSkill(ConstData.Knight);
                }
                else if (Knight2 == 6)
                {
                    SkillManager.Instance.C_ClassSkill(ConstData.Knight);
                }
                if (Berserker2 == 1)
                {
                    SkillManager.Instance.A_ClassSkill(ConstData.Berserker);
                }
                else if (Berserker2 == 4)
                {
                    SkillManager.Instance.B_ClassSkill(ConstData.Berserker);
                }
                else if (Berserker2 == 6)
                {
                    SkillManager.Instance.C_ClassSkill(ConstData.Berserker);
                }
                if (Caster2 == 1)
                {
                    SkillManager.Instance.A_ClassSkill(ConstData.Caster);
                }
                else if (Caster2 == 4)
                {
                    SkillManager.Instance.B_ClassSkill(ConstData.Caster);
                }
                else if (Caster2 == 6)
                {
                    SkillManager.Instance.C_ClassSkill(ConstData.Caster);
                }
                if (Hunter2 == 1)
                {
                    SkillManager.Instance.A_ClassSkill(ConstData.Hunter);
                }
                else if (Hunter2 == 4)
                {
                    SkillManager.Instance.B_ClassSkill(ConstData.Hunter);
                }
                else if (Hunter2 == 6)
                {
                    SkillManager.Instance.C_ClassSkill(ConstData.Hunter);
                }
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
