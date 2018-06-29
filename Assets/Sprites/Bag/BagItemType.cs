using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//筛选不同角色职业的武器、防具、消耗品、材料
public class BagItemType : MonoBehaviour {
    GameObject tagret;
    private void Awake()
    {
        tagret = GameObject.FindGameObjectWithTag("BagCreateItem").gameObject;

        GameObject caster = this.transform.Find("Caster/caster").gameObject;
        caster.SetActive(false);
        GameObject berserker = this.transform.Find("Berserker/berserker").gameObject;
        berserker.SetActive(false);
        GameObject saber = this.transform.Find("Saber/saber").gameObject;
        saber.SetActive(false);
        GameObject knight = this.transform.Find("Knight/knight").gameObject;
        knight.SetActive(false);
        GameObject hunter = this.transform.Find("Hunter/hunter").gameObject;
        hunter.SetActive(false);
        this.transform.Find("Saber").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("Caster").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("Berserker").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("Knight").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("Hunter").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("All").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();

        //显示角色职业是剑士的装备
        this.transform.Find("Saber").GetComponent<Toggle>().onValueChanged.AddListener((isOn)=>{
            saber.SetActive(isOn);
            FiltrateItem(isOn,"Saber");

        });
        //显示角色职业是猎人的装备
        this.transform.Find("Hunter").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            hunter.SetActive(isOn);
            FiltrateItem(isOn,"Hunter");
        });
        //骑士
        this.transform.Find("Knight").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            knight.SetActive(isOn);
            FiltrateItem(isOn,"Knight");

        });
        //显示角色职业是狂战士的装备
        this.transform.Find("Berserker").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            berserker.SetActive(isOn);
            FiltrateItem(isOn,"Berserker");
        });
        //显示角色职业是魔法师的装备
        this.transform.Find("Caster").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            caster.SetActive(isOn);
            FiltrateItem(isOn,"Caster");
        });
        //显示所有角色职业的装备
        this.transform.Find("All").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            foreach (var item in tagret.GetComponent<BagCreateItems>().itemDict)
            {
                item.Value.SetActive(isOn);
            }
        });
    }
    /// <summary>
    /// 根据不同角色职业显示出武器、防具、消耗品、材料
    /// </summary>
    /// <param name="type">角色的职业</param>
    void FiltrateItem(bool isOn,string type) {
        if (tagret.GetComponent<BagCreateItems>())
        {
            foreach (var item in tagret.GetComponent<BagCreateItems>().itemDict)
            {
                if (item.Value.GetComponent<BagItems>().Class == type)
                {
                    item.Value.SetActive(isOn);
                }
            }
        }
        
    }
}
