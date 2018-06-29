using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BagItemMessage : MonoBehaviour {
    
     int ID;
    string itemName;
    Text _name;//装备名字（显示）
    Text _type;//装备类型（显示）
    Text _price;//装备价格（显示）
    Text _ad;//装备的属性AD（显示）
    Text _ap;//装备的属性AP（显示）
    Text _hp;//装备的属性HP（显示）
    Text _res;//装备的属性RES（显示）
    Text _def;//装备的属性DEF（显示）
    Text _description;//
    
    Text _stockplie;//
    
    private void Awake()
    {
        
        _name = this.transform.Find("Name").GetComponent<Text>();
        _type = this.transform.Find("Type").GetComponent<Text>();
        _price = this.transform.Find("Price").GetComponent<Text>();
        _ad = this.transform.Find("AD").GetComponent<Text>();
        _ap = this.transform.Find("AP").GetComponent<Text>();
        _hp = this.transform.Find("HP").GetComponent<Text>();
        _res = this.transform.Find("RES").GetComponent<Text>();
        _def = this.transform.Find("DEF").GetComponent<Text>();
        _description = this.transform.Find("Description").GetComponent<Text>();
       
        _stockplie = this.transform.Find("StockPlie").GetComponent<Text>();
        this.transform.Find("Sale").GetComponent<Button>().onClick.AddListener(() => {
            GameObject.FindGameObjectWithTag("BagCreateItem").GetComponent<BagCreateItems>().itemDict.Remove(ID);
            Debug.Log(itemName);
        Destroy(GameObject.Find(itemName).gameObject);//销毁已经出售的装备
            DelctItem(ID);//从背包里面移除该装备所有信息
        });
    }
    /// <summary>
    /// 加载出武器跟防具的信息
    /// </summary>
    /// <param name="id">装备的ID</param>
    public void RefreshEquiptDate(int id,string name)
    {
        ID = id;
        itemName = name;
        EquipmentData equipmentData = SQLiteManager.Instance.equipmentDataSource[id];
        _name.text = "名字:["+equipmentData.equipmentNmae+"]";
        _type.text = equipmentData.equipmentClass+"装备";
        _price.text = "回收价:"+equipmentData.equipmentPrice.ToString();
        _ad.text = "AD+"+equipmentData.equipment_AD.ToString();
        _ap.text = "AP+"+equipmentData.equipment_AP.ToString();
        _hp.text = "HP+"+equipmentData.equipment_HP.ToString();
        _res.text = "RES+"+equipmentData.equipment_RES.ToString();
        _def.text = "DEF"+equipmentData.equipment_DEF.ToString();
    }
    /// <summary>
    /// 加载出材料跟消耗品的信息
    /// </summary>
    /// <param name="id">装备的ID</param>
    public void RefreshItemDate(int id,string name)
    {
        ID = id;
        itemName = name;
        ItemData itemData = SQLiteManager.Instance.itemDataSource[id];
        _name.text = itemData.item_Name;
        _type.text = itemData.item_Type;
        _price.text = itemData.item_Price.ToString();
        _stockplie.text = itemData.Stockpile.ToString();
        _description.text = itemData.item_Description;
       

    }
    /// <summary>
    /// 移除出售的装备
    /// </summary>
    /// <param name="id">装备ID</param>
    void DelctItem(int id) {
        if (id < 2201)
        {
            //SQLiteManager.Instance.DeleteTableData("Equpment",id,);
        }
        else
        {
            //SQLiteManager.Instance.DeleteTableData();
        }



    }
}
