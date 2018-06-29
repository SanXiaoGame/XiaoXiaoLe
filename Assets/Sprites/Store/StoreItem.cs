using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//商店的装备
public class StoreItem : MonoBehaviour {
    [HideInInspector]
    internal int ID;//装备ID
    internal string Type;//装备的类型
    private void Awake()
    {
        GameObject target = GameObject.FindGameObjectWithTag("StoreTopBtn").gameObject.GetComponent<StoreTopBtn>().target;
        GameObject item = this.transform.Find("equipment").gameObject;
        item.SetActive(false);
        //显示装备的信息面板
        this.GetComponent<Toggle>().onValueChanged.AddListener((isOn)=> {
            item.SetActive(isOn);
            target.SetActive(true);
            if (ID < 2126)
            {
                
                target.GetComponent<StoreItemMessage>().RefreshEquiptDate(ID);
            }
            else
            {
                Debug.Log(ID);
                target.GetComponent<StoreItemMessage>().RefreshItemDate(ID);
            }
            

        });
    }
}
