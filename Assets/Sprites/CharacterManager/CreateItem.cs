using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreateItem : MonoBehaviour {
    Transform[] bagLists = new Transform[30];
   
    GameObject target;
    int[] equipts = { 2001, 2016, 2028, 2040, 2052, 2101, 2106, 2111, 2116, 2121 };
    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            string path = string.Format("item{0}",i);
            bagLists[i] = this.transform.Find(path).transform;
            
        }
        
        target = Resources.Load("Prefabs/PlayerPrefabs/Equipment") as GameObject;
        for (int i = 0; i < equipts.Length; i++)
        {
            CreateBag(equipts[i]);
        }
    }
    void CreateBag(int ID) {
        GameObject item = Instantiate(target);
        string path = string.Format("Texture/Item0/{0}",ID);
        item.GetComponent<Image>().sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        Item _item = item.AddComponent<Item>();
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
