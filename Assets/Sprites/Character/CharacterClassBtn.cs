using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterClassBtn : MonoBehaviour {
    GameObject target;
    private void Awake()
    {
        GameObject enchcanter = this.transform.Find("Enchanter0/enchanter").gameObject;
        enchcanter.SetActive(false);
        GameObject hunter = this.transform.Find("Hunter0/hunter").gameObject;
        hunter.SetActive(false);
        GameObject berserker = this.transform.Find("Berserker0/berserker").gameObject;
        berserker.SetActive(false);
        GameObject knight = this.transform.Find("Knight0/knight").gameObject;
        knight.SetActive(false);
        GameObject swordman = this.transform.Find("Swordman0/swordman").gameObject;
        swordman.SetActive(false);
        target = GameObject.FindGameObjectWithTag("TopBtn").GetComponent<TopBtnManager>().bag;
        this.transform.Find("Enchanter0").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("Hunter0").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("Berserker0").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("Knight0").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("Swordman0").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("All").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        //筛选职业是魔法师或者装备的是属于魔法师
        this.transform.Find("Enchanter0").GetComponent<Toggle>().onValueChanged.AddListener((isOn)=> {
            Debug.Log("1");
            enchcanter.SetActive(isOn);
            SetActive("Caster");


        });
        //筛选职业是猎人或者装备的是属于猎人
        this.transform.Find("Hunter0").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            hunter.SetActive(isOn);
            SetActive("Hnuter");
        });
        //筛选职业是狂战士或者装备的是属于狂战士
        this.transform.Find("Berserker0").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            berserker.SetActive(isOn);
            SetActive("Berserker");
        });
        //筛选职业是骑士或者装备的是属于骑士
        this.transform.Find("Knight0").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            knight.SetActive(isOn);
            SetActive("Knight");
        });
        //筛选职业是剑士或者装备的是属于剑士
        this.transform.Find("Swordman0").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            swordman.SetActive(isOn);
            SetActive("Saber");
        });
        //展示所有的职业或者装备
        this.transform.Find("All").GetComponent<Toggle>().onValueChanged.AddListener((isOn) =>
        {
            for (int i = 0; i < SQLiteManager.Instance.characterLits.Count; i++)
            {
               
                    SQLiteManager.Instance.characterLits[i].SetActive(true);
                
            }
            if (target.GetComponent<CreateEquipts>())
            {
                foreach (var item in target.GetComponent<CreateEquipts>().itemLists)
                {
                    item.SetActive(true);
                }
            }
            

        });
    }
    void SetActive(string name) {
        for (int i = 0; i < SQLiteManager.Instance.characterLits.Count; i++)
        {
            if (SQLiteManager.Instance.characterLits[i].GetComponent<Character>().Type == name)
            {
                SQLiteManager.Instance.characterLits[i].SetActive(true);
            }
            else
            {
                SQLiteManager.Instance.characterLits[i].SetActive(false);
            }
        }

        if (target.GetComponent<CreateEquipts>())
        {
            foreach (var item in target.GetComponent<CreateEquipts>().itemLists)
            {
                if (item.GetComponent<Equipments>().type == name)
                {
                    item.SetActive(true);
                }
                else
                {
                    item.SetActive(false);
                }
            }
        }
            
        
        

    }
}
