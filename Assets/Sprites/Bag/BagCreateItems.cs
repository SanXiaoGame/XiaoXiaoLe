using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//背包装备创建
public class BagCreateItems : MonoBehaviour {
    Transform[] itemPos = new Transform[36];//存放背包每个空间的位置
   
    string Name;
    int[] items = { 2001, 2016, 2028, 2040, 2052, 2101, 2106, 2111, 2116, 2121 };
    GameObject target;
    public Dictionary<int, GameObject> itemDict = new Dictionary<int, GameObject>();
    private void Awake()
    {
        
        for (int i = itemPos.Length-1; i >= 0; i--)
        {
            string path = string.Format("bag{0}",i);
            itemPos[i] = this.transform.Find(path); ;//查询背包的每个空间的位置并保存
        }
         target = Resources.Load(ConstData.ItemPrefab) as GameObject;//加载出装备
        
        for (int i = 0; i < items.Length; i++)
        {
            CreateItem(items[i]);
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
        item.AddComponent<BagItems>();
        item.GetComponent<BagItems>().ID = id;//将装备的ID存放于各个装备的内部 便于后面根据ID显示装备信息
        item.name = SQLiteManager.Instance.equipmentDataSource[id].equipmentNmae;

        if (id < 2126)
        {
            item.GetComponent<BagItems>().Class = SQLiteManager.Instance.equipmentDataSource[id].equipmentClass;
            //查找属于武器的装备
            if (SQLiteManager.Instance.equipmentDataSource[id].equipmentType == "Weapon")
            {
                //将装备标识所属类型便于后面查找
                item.GetComponent<BagItems>().Type = "Weapon";
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

                item.GetComponent<BagItems>().Type = "Equipment";
                if (b < 36)
                {
                    item.transform.parent = itemPos[b];
                    item.transform.localPosition = Vector3.zero;
                    item.transform.localScale = itemPos[b].localScale;
                    b++;
                }



            }
        item.name = SQLiteManager.Instance.equipmentDataSource[id].equipmentNmae;


        }
        else
        {
            //查找属于消耗品的装备
            if (SQLiteManager.Instance.itemDataSource[id].item_Type == "消耗品")
            {
                item.GetComponent<BagItems>().Type = "Consumable";
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
                item.GetComponent<BagItems>().Type = "Material";
                if (d < 36)
                {
                    item.transform.parent = itemPos[d];
                    item.transform.localPosition = Vector3.zero;
                    item.transform.localScale = itemPos[d].transform.localScale;
                    d++;
                }
            }
            item.name = SQLiteManager.Instance.itemDataSource[id].item_Name;

        }
        itemDict.Add(id, item);
        item.SetActive(false);

    }

}
