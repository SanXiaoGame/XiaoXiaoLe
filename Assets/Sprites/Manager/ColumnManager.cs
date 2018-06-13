using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 每列块的管理类
/// </summary>
public class ColumnManager : ManagerBase<ColumnManager>
{
    //每列块的父类
    internal ColumnScript[] gameColumns;
    //总列数 默认6
    internal int numberOfColumns = 6;
    //总行数 默认6
    internal int numberOfRows = 6;
    //记录当前的列（最开始左边的列数等于-1）
    internal int ColumnNumber = -1;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        gameColumns = new ColumnScript[numberOfColumns];
        for (int i = 0; i < gameColumns.Length; i++)
        {
            //实例化出对应的列空物体，添加ColumnScript
            GameObject blockTemp = new GameObject();
            gameColumns[i] = blockTemp.AddComponent<ColumnScript>();
            //blockTemp.transform.parent = columnParent;
            blockTemp.transform.parent = transform;
            blockTemp.transform.localScale = Vector3.one;
            blockTemp.name = "Column" + i;
        }

        //列的排序
        for (int i = 0; i < gameColumns.Length; i++)
        {
            gameColumns[i].columnIndex = i;
            gameColumns[i].transform.localPosition = new Vector3(-500 + i * 200f, 500f, 0f);
        }
    }

    /// <summary>
    /// 返回前一列相连块（左边）
    /// </summary>
    internal GameObject PreviousColumnBlock(int i)
    {
        int num = ColumnNumber;
        return transform.GetChild(num - 1).GetChild(i).gameObject;
    }
}
