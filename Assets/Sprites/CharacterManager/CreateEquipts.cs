using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreateEquipts : MonoBehaviour {

    Transform[] bagLists = new Transform[30];//背包的空间位置
   
    GameObject target;
    int[] equipts = { 2001, 2016, 2028, 2040, 2052, 2101, 2106, 2111, 2116, 2121 };
    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            string path = string.Format("item{0}",i);
            bagLists[i] = this.transform.Find(path).transform;//查找背包的空间位置并添加   
        }
        
        target = Resources.Load("Prefabs/PlayerPrefabs/Equipment") as GameObject;//加载装备
        for (int i = 0; i < equipts.Length; i++)
        {
            CreateBag(equipts[i]);//产生装备
        }
    }
    /// <summary>
    /// 创建装备
    /// </summary>
    /// <param name="ID">装备的ID</param>
    void CreateBag(int ID) {
        GameObject item = Instantiate(target);
        string path = string.Format("Texture/Item/{0}",ID);
        item.GetComponent<Image>().sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        Equipments _item = item.AddComponent<Equipments>();
        _item.type = SQLiteManager.Instance.equipmentDataSource[ID].equipmentClass;
        _item.ID = ID;
        for (int i = 0; i < 10; i++)
        {
            if (bagLists[i].childCount == 0)
            {
                item.transform.parent = bagLists[i];
                item.transform.localPosition = Vector3.zero;
                item.transform.localScale = bagLists[i].localScale;
            }
        }
        SQLiteManager.Instance.itemLists.Add(item);


    }
}
