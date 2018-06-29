using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//商城界面最上的按钮的管理
public class ShopClassifyBtn : MonoBehaviour
{
    GameObject target;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("ShopCreateItem").gameObject;
        this.transform.Find("Weapon").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("Amror").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("Consumable").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("Material").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        //显示属于武器的装备
        this.transform.Find("Weapon").GetComponent<Toggle>().onValueChanged.AddListener((isOn)=> {
            
            SetActiveItem(isOn,"Weapon");


        });
        //显示属于防具的装备
        this.transform.Find("Amror").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {

            SetActiveItem(isOn, "Equipment");


        });
        //显示属于消耗品的装备
        this.transform.Find("Consumable").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {

            SetActiveItem(isOn, "Consumable");


        });
        //显示属于材料的装备
        this.transform.Find("Material").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {

            SetActiveItem(isOn, "Material");


        });
    }
    /// <summary>
    /// 显示装备
    /// </summary>
    /// <param name="isOn"></param>
    /// <param name="name">装备的类型</param>
    void SetActiveItem(bool isOn,string name) {

        foreach (var item in target.GetComponent<ShopCreateItems>().itemDict)
        {
            Debug.Log(item.Value.GetComponent<ShopItem>().Type);
            if (item.Value.GetComponent<ShopItem>().Type == name)
            {
                item.Value.SetActive(isOn);
            }
        }
    }

}
	
