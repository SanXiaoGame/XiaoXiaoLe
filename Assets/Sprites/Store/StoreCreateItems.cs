using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//商店创建装备
public class StoreCreateItems : MonoBehaviour {
    GameObject target;
    Transform[] itemPos = new Transform[36];//各个装备存放的位置
    public Dictionary<int, GameObject> itemDict = new Dictionary<int, GameObject>();
    int[] ItemArray = {2001,2002, 2003,2004,2016,2017,2018,2019,2028,2029,2030,2031,2040,
        2041,2042,2043,2052,2053,2054,2055,2101,2102,2103,2104,2106,2107,2108,2109,2111,2112,
        2113,2114,2117,2118,2119,2121,2123,2124,2201,2202,2203,2204,2205,2301,2302,2303,2304,2305 };//商店拥有的装备ID
    private void Awake()
    {
        for (int i = 0; i < 36; i++)
        {
            string path = string.Format("bag{0}",i);
            itemPos[i] = this.transform.Find(path).transform;
        }
        target = Resources.Load(ConstData.ItemPrefab) as GameObject;

        for (int i = 0; i < ItemArray.Length; i++)
        {
            CreateItem(ItemArray[i]);//根据装备的ID创出装备物品
        }
        
        
    }
    int a = 0;
    int b = 0;
    int c = 0;
    int d = 0;
    /// <summary>
    /// 创建装备
    /// </summary>
    /// <param name="id">装备ID</param>
    void CreateItem(int id) {
        
        GameObject item = Instantiate(target);
        string path = string.Format("Texture/Item/{0}",id);
        item.GetComponent<Image>().sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        item.GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>(); 
        item.AddComponent<StoreItem>();
        item.GetComponent<StoreItem>().ID = id;//将装备的ID存放于各个装备的内部 便于后面根据ID显示装备信息
       

        if (id < 2126)
        {
           //查找属于武器的装备
            if (SQLiteManager.Instance.equipmentDataSource[id].equipmentType == "Weapon")
            {
                //将装备标识所属类型便于后面查找
                item.GetComponent<StoreItem>().Type = "Weapon";
                if (a < 36)
                {
                    //将装备放置于所属装备类型的空间下
                    item.transform.parent = itemPos[a];
                    item.transform.localPosition = Vector3.zero;
                    item.transform.localScale = itemPos[a].localScale;
                    a++;
                }          
            }
            else
            {
                
                item.GetComponent<StoreItem>().Type = "Amror";
                if (b < 36)
                {
                    item.transform.parent = itemPos[b];
                    item.transform.localPosition = Vector3.zero;
                    item.transform.localScale = itemPos[b].localScale;
                    b++;
                }
                  
                   
                
            }
           
        }
        else
        {
            //查找属于消耗品的装备
            if (SQLiteManager.Instance.itemDataSource[id].item_Type == "Consumable")
            {
                item.GetComponent<StoreItem>().Type = "Consumable";
                if (c < 36)
                {
                    
                    item.transform.parent = itemPos[c];
                    item.transform.localPosition = Vector3.zero;
                    item.transform.localScale = itemPos[c].transform.localScale;
                    c++;
                }
            }
            else
            {
                item.GetComponent<StoreItem>().Type = "Material";
                if (d < 36)
                {                
                    item.transform.parent = itemPos[d];
                    item.transform.localPosition = Vector3.zero;
                    item.transform.localScale = itemPos[d].transform.localScale;
                    d++;
                }                  
            }          
        }
        itemDict.Add(id, item);
        item.SetActive(false);
        
    }
}
