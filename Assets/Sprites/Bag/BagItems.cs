using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BagItems : MonoBehaviour {
    
    internal string Type { get; set; }
    
    internal int ID { get; set; }
    
    internal string Class { get; set; }
    private void Awake()
    {
        GameObject itemMessage = GameObject.FindGameObjectWithTag("BagTopBtn").GetComponent<BagTopBtn>().itemMessage;//装备的信息面板

        
        GameObject item = this.transform.Find("equipment").gameObject;
        item.SetActive(false);
        this.GetComponent<Toggle>().onValueChanged.AddListener((isOn)=> {
            item.SetActive(isOn);
            itemMessage.SetActive(true);
            
            //根据不同的ID调用不同的方法显示装备的信息
            if (ID < 2201)
            {
                
                itemMessage.GetComponent<BagItemMessage>().RefreshEquiptDate(ID, this.name);
            }
            else
            {
                itemMessage.GetComponent<BagItemMessage>().RefreshItemDate(ID, this.name);
            }
        });
       
    }
  
}
