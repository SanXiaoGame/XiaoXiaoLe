using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BagClassifyBtn : MonoBehaviour {
   GameObject target;
    private void Awake()
    {
        //GameObject weapon = this.transform.Find("Weapon/weapon").gameObject;
        //weapon.SetActive(false);
        //GameObject amror = this.transform.Find("Amror/amror").gameObject;
        //amror.SetActive(false);
        //GameObject consumable = this.transform.Find("Consumable/consumable").gameObject;
        //consumable.SetActive(false);
        //GameObject material = this.transform.Find("Material/material").gameObject;
        //material.SetActive(false);
        target = GameObject.FindGameObjectWithTag("BagCreateItem").gameObject;
        this.transform.Find("Weapon").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("Amror").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("Consumable").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("Material").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        //显示装备类型是武器的装备
        this.transform.Find("Weapon").GetComponent<Toggle>().onValueChanged.AddListener((isOn)=> {
            //weapon.SetActive(isOn);
            SetActiveItem(isOn,"Weapon");


        });
        //显示装备类型是防具的装备
        this.transform.Find("Amror").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            //amror.SetActive(isOn);
            SetActiveItem(isOn, "Equipment");


        });
        //显示装备类型是消耗品的装备
        this.transform.Find("Consumable").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            //consumable.SetActive(isOn);
            SetActiveItem(isOn, "Consumable");



        });
        //显示装备类型是材料的装备
        this.transform.Find("Material").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            //material.SetActive(isOn);
            SetActiveItem(isOn, "Material");


        });
    }
    /// <summary>
    /// 显示装备
    /// </summary>
    /// <param name="isOn"></param>
    /// <param name="name">装备的类型</param>
    void SetActiveItem(bool isOn,string name) {
        
        foreach (var item in target.GetComponent<BagCreateItems>().itemDict)
        {
            
            if (item.Value.GetComponent<BagItems>().Type == name)
            {
                item.Value.SetActive(isOn);
            }
        }

    }
}
