using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//商店界面最上面的按钮管理
public class StoreTopBtn : MonoBehaviour {
    [HideInInspector]
   public  GameObject target;
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("StoreItemMessage").gameObject;//获取装备的信息面板
        target.SetActive(false);//隐藏装备信息面板
        GameObject createItem = GameObject.FindGameObjectWithTag("StoreCreateItem").gameObject;
        
        this.transform.Find("StoreBtn").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("MoneyBtn").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("BackBtn").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();


        this.transform.Find("StoreBtn").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {

            if (createItem.GetComponent<StoreCreateItems>() == null)
            {
                createItem.AddComponent<StoreCreateItems>();
            }



        });
        this.transform.Find("MoneyBtn").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            

        });
        this.transform.Find("BackBtn").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => { });

    }
    
}
