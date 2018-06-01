using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //列未定的X值
    float ColumnX = 2.5f;

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
            blockTemp.transform.parent = transform;
            blockTemp.name = StringSplicingTool.StringSplicing("Column ", i.ToString());
        }

        //列的排序
        for (int i = 0; i < gameColumns.Length; i++)
        {
            gameColumns[i].columnIndex = i;
            gameColumns[i].transform.localPosition = new Vector3(ColumnX - i, 0f, 0f);
        }
    }

    /// <summary>
    /// 指派(赋值)相连块
    /// </summary>
    internal void AssignNeighbours()
    {
        
    }
}
