using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    //存放临时随机数
    int randomNumber;

    private void Start()
    {
        BlockObjectsScriptList = new List<BlockObject>();
        //Invoke("PopulateInitialColumn", .2f);
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
                index = Random.Range(0, 6);
            }
            //新的随机数
            randomNumber = index;
            if (ColumnManager.Instance.ColumnNumber % 2 == 0 && ColumnManager.Instance.PreviousColumnBlock(i).name == StringSplicingTool.StringSplicing(GameManager.Instance.playingObjectPrefabs[index].name, "(Clone)"))
            {
                if (index != 0 && index != 5)
                {
                    while (index == randomNumber)
                    {
                        index = Random.Range(0, 6);
                    }
                }
                else
                {
                    index = index == 0 ? 5 : 0;
                }
                //新的随机数
                randomNumber = index;
            }

            objectPrefab = GameManager.Instance.playingObjectPrefabs[index] as GameObject;
            GameObject block = Instantiate(objectPrefab, Vector3.zero, Quaternion.identity);
            block.transform.parent = transform;
            block.transform.localPosition = new Vector3(0, -i, 0);
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
        for (int i = 0; i < BlockObjectsScriptList.Count; i++)
        {
            if (BlockObjectsScriptList[i] == null)
            {
                continue;
            }
            //检测是最左边,统一等于null
            //不是就当前列号减一，找左边对应位置的块脚本BlockObject,存在数组0位
            BlockObjectsScriptList[i].adjacentItems[0] = columnIndex == 0 ? null : ColumnManager.Instance.gameColumns[columnIndex - 1].BlockObjectsScriptList[i];

            //检测是最右边,统一等于null
            //不是就当前列号加一，找右边对应位置的块脚本BlockObject,存在数组1位
            BlockObjectsScriptList[i].adjacentItems[1] = columnIndex == ColumnManager.Instance.gameColumns.Length - 1 ? null : ColumnManager.Instance.gameColumns[columnIndex + 1].BlockObjectsScriptList[i];

            //检测是最上边,统一等于null
            //不是就当前行表减一，找左边对应位置的块脚本BlockObject,存在数组2位
            BlockObjectsScriptList[i].adjacentItems[2] = i == 0 ? null : BlockObjectsScriptList[i - 1];

            //检测是最上边,统一等于null
            //不是就当前行表减一，找左边对应位置的块脚本BlockObject,存在数组3位
            BlockObjectsScriptList[i].adjacentItems[3] = i == 0 ? null : BlockObjectsScriptList[i + 1];
        }
    }

    /// <summary>
    /// 添加消除的块
    /// </summary>
    internal void AddMissingBlock()
    {
        //需要添加块数 = 总行（默认6） - 剩余子物体（BlockObjectsScript脚本）
        int numberOfItemsToAdd = LevelManager.Instance.numberOfRows - BlockObjectsScriptList.Count;
        if (numberOfItemsToAdd == 0)
        {
            return;
        }
        //添加块实例
        for (int i = 0; i < numberOfItemsToAdd; i++)
        {
            int index = Random.Range(0, GameManager.Instance.normalBlockNumber);
            objectPrefab= GameManager.Instance.playingObjectPrefabs[index] as GameObject;
            GameObject block = Instantiate(objectPrefab, Vector3.zero, Quaternion.identity);
            block.transform.parent = transform;
            block.transform.localPosition = new Vector3(0, i + 1, 0);
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

        //动画相关（未实现）

        //播放下降音效(未实现)
        float valume = 1f;//默认音量(测试用)
        AudioManager.Instance.PlayEffectBase("音效名", transform.position, valume);
    }
}
