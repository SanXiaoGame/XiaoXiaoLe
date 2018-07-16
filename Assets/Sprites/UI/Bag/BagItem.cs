using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BagItem : MonoBehaviour
{
    internal EquipmentData mydata_equipt;
    internal ItemData mydata_item;
    //所属格子
    internal int myGrid;

    internal void GetData()
    {
        if (transform.tag == ConstData.EquipmentType)
        {
            mydata_equipt = SQLiteManager.Instance.equipmentDataSource[Convert.ToInt32(transform.name)];
        }
        else if (transform.tag == ConstData.ItemType)
        {
            mydata_item = SQLiteManager.Instance.itemDataSource[Convert.ToInt32(transform.name)];
        }
    }
}
