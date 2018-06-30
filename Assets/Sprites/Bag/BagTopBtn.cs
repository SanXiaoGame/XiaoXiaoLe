using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BagTopBtn : MonoBehaviour {
    
    internal GameObject itemMessage;
    private void Awake()
    {
        GameObject createItem = GameObject.FindGameObjectWithTag("BagCreateItem").gameObject;
         itemMessage = GameObject.FindGameObjectWithTag("BagItemMessage").gameObject;//装备的信息面板
        itemMessage.SetActive(false);
        GameObject classBtn = GameObject.FindGameObjectWithTag("BagClassBtn").gameObject;
        this.transform.Find("BagBtn").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("MoneyBtn").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();

        this.transform.Find("BagBtn").GetComponent<Toggle>().onValueChanged.AddListener((isOn)=> {
           
            if (createItem.GetComponent<BagCreateItems>() == null)
            {
                createItem.AddComponent<BagCreateItems>();
            }
            

        });
        this.transform.Find("MoneyBtn").GetComponent<Toggle>().onValueChanged.AddListener((isOn)=> {


        });
    }
}
