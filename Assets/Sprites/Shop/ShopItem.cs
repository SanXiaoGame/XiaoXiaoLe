using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopItem : MonoBehaviour {

    [HideInInspector]
    internal int ID;//装备ID
    internal string Type;//装备的类型
   
    private void Awake()
    {
        GameObject target = GameObject.FindGameObjectWithTag("ShopTopBtn").gameObject.GetComponent<ShopTopBtn>().target;
        GameObject item = this.transform.Find("equipment").gameObject;
        item.SetActive(false);
        //显示装备的信息面板
        this.GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            item.SetActive(isOn);
            target.SetActive(true);
            if (ID < 2126)
            {

                target.GetComponent<ShopItemMessage>().RefreshEquiptDate(ID);
            }
            else
            {
               
                target.GetComponent<ShopItemMessage>().RefreshItemDate(ID);
            }


        });
    }
}
