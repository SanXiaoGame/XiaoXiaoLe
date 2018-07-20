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
    //记录吃了交换药后选中的两个块
    internal List<BlockObject> exchangeBlock = new List<BlockObject>();
    //列的父物体
    RectTransform columnParent;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        //找列的父物体
        columnParent = transform.Find(ConstData.ColumnParentObj).GetComponent<RectTransform>();
        gameColumns = new ColumnScript[numberOfColumns];
        for (int i = 0; i < gameColumns.Length; i++)
        {
            //实例化出对应的列空物体，添加ColumnScript
            RectTransform blockColumnTemp = new GameObject(StringSplicingTool.StringSplicing("Column", i.ToString())).AddComponent<RectTransform>();
            gameColumns[i] = blockColumnTemp.gameObject.AddComponent<ColumnScript>();
            //列的排序
            gameColumns[i].columnIndex = i;

            //设置父物体物体
            blockColumnTemp.parent = columnParent;
            blockColumnTemp.localScale = Vector3.one;
            blockColumnTemp.sizeDelta = new Vector2(columnParent.sizeDelta.x / numberOfRows, columnParent.sizeDelta.y / numberOfColumns);
            //列的位置
            blockColumnTemp.anchorMax = new Vector2(0, 1);
            blockColumnTemp.anchorMin = new Vector2(0, 1);
            blockColumnTemp.anchoredPosition3D = new Vector3(columnParent.sizeDelta.x / numberOfRows * i + blockColumnTemp.sizeDelta.x * 0.5f, -blockColumnTemp.sizeDelta.y * 0.5f, 0f);
        }
    }

    /// <summary>
    /// 特殊药水的方法
    /// </summary>
    internal void MedicinalWaterProp()
    {
        for (int i = 0; i < numberOfColumns; i++)
        {
            for (int j = 0; j < numberOfRows; j++)
            {
                gameColumns[i].BlockObjectsScriptList[j].brust = true;
            }
        }
        //用于补充药水消的块
        MedicinalWaterAddMissingBlock();
    }

    /// <summary>
    /// 用于补充药水消的块
    /// </summary>
    void MedicinalWaterAddMissingBlock()
    {
        vp_Timer.In(0.35f, new vp_Timer.Callback(delegate() 
        {
            GameManager.Instance.RemoveBlock();
            GameManager.Instance.AddMissingBlock();
        }));
    }

    /// <summary>
    /// 返回前一列相连块（左边）
    /// </summary>
    internal GameObject PreviousColumnBlock(int i)
    {
        int num = ColumnNumber;
        return columnParent.GetChild(num - 1).GetChild(i).gameObject;
    }
}
