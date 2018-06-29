using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCreateItems : MonoBehaviour {
    GameObject target;
    Transform[] itemPos = new Transform[36];
    public Dictionary<int, GameObject> itemDict = new Dictionary<int, GameObject>();
    int[] itemArray = {2014,2015,2026,2027,2038,2039,2050,2051,2062,2063,2105,2110,2115,2120,2125,2206,2207,2208,2209,2210,2211};//商城拥有的装备ID
    private void Awake()
    {
        for (int i = 0; i < 36; i++)
        {
            string path = string.Format("bag{0}", i);
            itemPos[i] = this.transform.Find(path).transform;
        }
        target = Resources.Load(ConstData.ItemPrefab) as GameObject;
        for (int i = 0; i < itemArray.Length; i++)
        {
            CreateItem(itemArray[i]);
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
    void CreateItem(int id)
    {

        GameObject item = Instantiate(target);
        string path = string.Format("Texture/Item/{0}", id);
        item.GetComponent<Image>().sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        item.GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        item.AddComponent<ShopItem>();
        item.GetComponent<ShopItem>().ID = id;//将装备的ID存放于各个装备的内部 便于后面根据ID显示装备信息


        if (id < 2126)
        {
            //查找属于武器的装备
            if (SQLiteManager.Instance.equipmentDataSource[id].equipmentType == "Weapon")
            {
                //将装备标识所属类型便于后面查找
                item.GetComponent<ShopItem>().Type = "Weapon";
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

                item.GetComponent<ShopItem>().Type = "Equipment";
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
                item.GetComponent<ShopItem>().Type = "Consumable";
                if (c < 36)
                {

                    item.transform.parent = itemPos[c];
                    item.transform.localPosition = Vector3.zero;
                    item.transform.localScale = itemPos[c].localScale;
                    c++;
                }
            }
            else
            {
                item.GetComponent<ShopItem>().Type = "Material";
                if (d < 36)
                {
                    item.transform.parent = itemPos[d];
                    item.transform.localPosition = Vector3.zero;
                    item.transform.localScale = itemPos[d].localScale;
                    d++;
                }
            }
        }
        itemDict.Add(id, item);
        item.SetActive(false);

    }
}
