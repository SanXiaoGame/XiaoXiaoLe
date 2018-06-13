﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 列的功能类
/// </summary>
public class ColumnScript : MonoBehaviour
{
    //列的索引
    internal int columnIndex = 0;
    //块的脚本列表
    internal List<BlockObject> BlockObjectsScriptList;
    //实例化初始块用的临时变量
    GameObject objectPrefab;
    //需要添加块数
    int numberOfItemsToAdd;
    //存放临时随机数
    int randomNumber;

    private void Start()
    {
        BlockObjectsScriptList = new List<BlockObject>();
        PopulateInitialColumn();
    }

    /// <summary>
    /// 填充实例化初始块
    /// </summary>
    void PopulateInitialColumn()
    {
        //记录当前列数
        ColumnManager.Instance.ColumnNumber++;
        for (int i = 0; i < ColumnManager.Instance.numberOfColumns; i++)
        {
            int index = Random.Range(0, GameManager.Instance.normalBlockNumber);

            while (objectPrefab != null && index == randomNumber)
            {
                index = Random.Range(0, GameManager.Instance.normalBlockNumber);
            }
            //新的随机数
            randomNumber = index;
            if (ColumnManager.Instance.ColumnNumber != 0 && ColumnManager.Instance.ColumnNumber % 2 == 0 && ColumnManager.Instance.PreviousColumnBlock(i).name == (GameManager.Instance.playingObjectPrefabs[index]as GameObject).name)
            {
                if (index != 0 && index != 4)
                {
                    while (index == randomNumber)
                    {
                        index = Random.Range(0, GameManager.Instance.normalBlockNumber);
                    }
                }
                else
                {
                    index = index == 0 ? 4 : 0;
                }
                //新的随机数
                randomNumber = index;
            }

            objectPrefab = GameManager.Instance.playingObjectPrefabs[index] as GameObject;

            /*GameObject block = Instantiate(objectPrefab, Vector3.zero, Quaternion.identity);
            block.name = objectPrefab.name;*/

            GameObject block = ObjectPoolManager.Instance.InstantiateBlockObject(objectPrefab);

            block.transform.parent = transform;
            block.transform.localPosition = new Vector3(0, -i * 200, 0);
            block.GetComponent<RectTransform>().localScale = Vector3.one;
            block.GetComponent<BlockObject>().myColumnScript = this;
            block.GetComponent<BlockObject>().ColumnNumber = i;
            BlockObjectsScriptList.Add(block.GetComponent<BlockObject>());
        }
    }

    /// <summary>
    /// 指派(赋值)相连块
    /// </summary>
    internal void AssignNeighbours()
    {
        //print(BlockObjectsScriptList.Count-1);
        for (int i = 0; i < BlockObjectsScriptList.Count; i++)
        {
            if (BlockObjectsScriptList[i] == null)
            {
                continue;
            }
            //检测是最左边,统一等于null
            //不是就当前列号减一，找左边对应位置的块脚本BlockObject,存在数组0位
            BlockObjectsScriptList[i].adjacentItems[0] = columnIndex == 0 ? null : ColumnManager.Instance.gameColumns[columnIndex - 1].BlockObjectsScriptList[i];
            if (columnIndex == 0)
            {
                BlockObjectsScriptList[i].adjacentItems[4] = null;
                BlockObjectsScriptList[i].adjacentItems[6] = null;
            }
            else if (i > 0 && i < BlockObjectsScriptList.Count - 1)
            {
                BlockObjectsScriptList[i].adjacentItems[4] = ColumnManager.Instance.gameColumns[columnIndex - 1].BlockObjectsScriptList[i - 1];
                BlockObjectsScriptList[i].adjacentItems[6] = ColumnManager.Instance.gameColumns[columnIndex - 1].BlockObjectsScriptList[i + 1];
            }

            //检测是最右边,统一等于null
            //不是就当前列号加一，找右边对应位置的块脚本BlockObject,存在数组1位
            BlockObjectsScriptList[i].adjacentItems[1] = columnIndex == ColumnManager.Instance.gameColumns.Length - 1 ? null : ColumnManager.Instance.gameColumns[columnIndex + 1].BlockObjectsScriptList[i];
            if (columnIndex == ColumnManager.Instance.gameColumns.Length - 1)
            {
                BlockObjectsScriptList[i].adjacentItems[5] = null;
                BlockObjectsScriptList[i].adjacentItems[7] = null;
            }
            else if (i > 0 && i < BlockObjectsScriptList.Count - 1)
            {
                BlockObjectsScriptList[i].adjacentItems[5] = ColumnManager.Instance.gameColumns[columnIndex + 1].BlockObjectsScriptList[i - 1];
                BlockObjectsScriptList[i].adjacentItems[7] = ColumnManager.Instance.gameColumns[columnIndex + 1].BlockObjectsScriptList[i + 1];
            }

            //检测是最上边,统一等于null
            //不是就当前行表减一，找上边对应位置的块脚本BlockObject,存在数组2位
            BlockObjectsScriptList[i].adjacentItems[2] = i == 0 ? null : BlockObjectsScriptList[i - 1];

            //检测是最下边,统一等于null
            //不是就当前行表减一，找下边对应位置的块脚本BlockObject,存在数组3位
            BlockObjectsScriptList[i].adjacentItems[3] = i == ColumnManager.Instance.numberOfRows - 1 ? null : BlockObjectsScriptList[i + 1];
        }
    }

    /// <summary>
    /// 获取要添加的块的数目
    /// </summary>
    /// <returns></returns>
    internal int GetNumberOfItemsToAdd()
    {
        return ColumnManager.Instance.numberOfRows - BlockObjectsScriptList.Count;
    }

    /// <summary>
    /// 生成特殊块
    /// </summary>
    /// <param 块之前的位置="index"></param>
    /// <param 特殊块的预制体="specialBlock"></param>
    internal void InstantiateSpecialBlock(int index, GameObject specialBlock)
    {

    }

    /// <summary>
    /// 删除能消的块
    /// </summary>
    internal void DeleteBrustedBlock()
    {
        for (int i = 0; i < ColumnManager.Instance.numberOfRows; i++)
        {
            if (BlockObjectsScriptList[i] != null)
            {
                if (BlockObjectsScriptList[i].brust)
                {
                    BlockObjectsScriptList[i].DestroyBlock();

                    //特殊块的预制体
                    GameObject specialBlock = BlockObjectsScriptList[i].specialObjectToForm;
                    if (specialBlock)
                    {
                        InstantiateSpecialBlock(i, specialBlock);
                    }
                    else
                    {
                        BlockObjectsScriptList[i] = null;
                    }
                }
            }
        }
        //清除对应块的元素
        for (int i = 0; i < BlockObjectsScriptList.Count; i++)
        {
            if (BlockObjectsScriptList[i] == null)
            {
                BlockObjectsScriptList.RemoveAt(i);
                i--;
            }
        }
    }

    /// <summary>
    /// 添加消除的块
    /// </summary>
    internal void AddMissingBlock()
    {
        //需要添加块数 = 总行（默认6） - 剩余子物体（BlockObjectsScript脚本）
        numberOfItemsToAdd = LevelManager.Instance.numberOfRows - BlockObjectsScriptList.Count;
        if (numberOfItemsToAdd == 0)
        {
            return;
        }
        //添加块实例
        for (int i = 0; i < numberOfItemsToAdd; i++)
        {
            int index = Random.Range(0, GameManager.Instance.normalBlockNumber);
            objectPrefab= GameManager.Instance.playingObjectPrefabs[index] as GameObject;

            /*GameObject block = Instantiate(objectPrefab, Vector3.zero, Quaternion.identity);
            block.name = objectPrefab.name;*/

            GameObject block = ObjectPoolManager.Instance.InstantiateBlockObject(objectPrefab);

            block.transform.parent = transform;
            block.GetComponent<RectTransform>().localScale = Vector3.one;
            block.transform.localPosition = new Vector3(0, (i + 1) * 200, 0);
            //赋值新的ColumnScript类
            block.GetComponent<BlockObject>().myColumnScript = this;
            //Insert，在指定的位置插入一个元素
            BlockObjectsScriptList.Insert(0, block.GetComponent<BlockObject>());
        }
        //重新赋值块所在的列编号
        for (int i = 0; i < BlockObjectsScriptList.Count; i++)
        {
            if (BlockObjectsScriptList[i] != null)
            {
                BlockObjectsScriptList[i].ColumnNumber = i;
            }
        }
        //旧的块下降
        for (int i = numberOfItemsToAdd; i < BlockObjectsScriptList.Count; i++)
        {
            BlockObjectsScriptList[i].transform.DOLocalMoveY(-i * 200, 0.1f).SetDelay(0.1f).SetEase(Ease.Linear);
        }

        //新的块下降
        for (int i = 0; i < numberOfItemsToAdd; i++)
        {
            //下降的动画
            BlockObjectsScriptList[i].transform.DOLocalMoveY(-i * 200, 0.1f).SetDelay(0.1f).SetEase(Ease.Linear).OnComplete(delegate ()
            {
                Invoke("DelayDOTween", 0.5f);
            });
        }

        //播放下降音效(未实现)
        AudioManager.Instance.PlayEffectMusic(SoundEffect.Attack);
    }

    /// <summary>
    /// 延迟检测是否还有块在消
    /// </summary>
    void DelayDOTween()
    {
        if (!GameManager.Instance.doesHaveBrustItem)
        {
            GameManager.Instance.isBusy = false;
        }
    }
}
