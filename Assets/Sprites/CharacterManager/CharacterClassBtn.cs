using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterClassBtn : MonoBehaviour {
    List<GameObject> Lists;
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
        Lists = SQLiteManager.Instance.itemLists;
        this.transform.Find("Enchanter0").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("Hunter0").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("Berserker0").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("Knight0").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("Swordman0").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("All").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        //筛选职业是魔法师或者装备的是属于魔法师
        this.transform.Find("Enchanter0").GetComponent<Toggle>().onValueChanged.AddListener((isOn)=> {
            enchcanter.SetActive(isOn);
            for (int i = 0; i < SQLiteManager.Instance.characterLists.Count; i++)
            {
            if (SQLiteManager.Instance.characterLists[i].GetComponent<Character>().Type == "Caster")
                {
                    SQLiteManager.Instance.characterLists[i].SetActive(true);
                }
                else
                {
                    SQLiteManager.Instance.characterLists[i].SetActive(false);
                }
            }
            for (int i = 0; i < Lists.Count; i++)
            {
                if (Lists[i].GetComponent<Equipments>().type == "Caster")
                {
                    Lists[i].SetActive(true);
                }
                else
                {
                    Lists[i].SetActive(false);
                }
            }
        });
        //筛选职业是猎人或者装备的是属于猎人
        this.transform.Find("Hunter0").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            hunter.SetActive(isOn);
            for (int i = 0; i < SQLiteManager.Instance.characterLists.Count; i++)
            {
                if (SQLiteManager.Instance.characterLists[i].GetComponent<Character>().Type == "Hunter")
                {
                    SQLiteManager.Instance.characterLists[i].SetActive(true);
                }
                else
                {
                    SQLiteManager.Instance.characterLists[i].SetActive(false);
                }
            }
            for (int i = 0; i < Lists.Count; i++)
            {
                if (Lists[i].GetComponent<Equipments>().type == "Hunter")
                {
                    Lists[i].SetActive(true);
                }
                else
                {
                    Lists[i].SetActive(false);
                }
            }
        });
        //筛选职业是狂战士或者装备的是属于狂战士
        this.transform.Find("Berserker0").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            berserker.SetActive(isOn);
            for (int i = 0; i < SQLiteManager.Instance.characterLists.Count; i++)
            {
                if (SQLiteManager.Instance.characterLists[i].GetComponent<Character>().Type == "Berserker")
                {
                    SQLiteManager.Instance.characterLists[i].SetActive(true);
                }
                else
                {
                    SQLiteManager.Instance.characterLists[i].SetActive(false);
                }
            }
            for (int i = 0; i < Lists.Count; i++)
            {
                if (Lists[i].GetComponent<Equipments>().type == "Berserker")
                {
                    Lists[i].SetActive(true);
                }
                else
                {
                    Lists[i].SetActive(false);
                }
            }
        });
        //筛选职业是骑士或者装备的是属于骑士
        this.transform.Find("Knight0").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            knight.SetActive(isOn);
            for (int i = 0; i < SQLiteManager.Instance.characterLists.Count; i++)
            {
                if (SQLiteManager.Instance.characterLists[i].GetComponent<Character>().Type == "Knight")
                {
                    SQLiteManager.Instance.characterLists[i].SetActive(true);
                }
                else
                {
                    SQLiteManager.Instance.characterLists[i].SetActive(false);
                }
            }
            for (int i = 0; i < Lists.Count; i++)
            {
                if (Lists[i].GetComponent<Equipments>().type == "Knight")
                {
                    Lists[i].SetActive(true);
                }
                else
                {
                    Lists[i].SetActive(false);
                }
            }
        });
        //筛选职业是剑士或者装备的是属于剑士
        this.transform.Find("Swordman0").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            swordman.SetActive(isOn);
            for (int i = 0; i < SQLiteManager.Instance.characterLists.Count; i++)
            {
                if (SQLiteManager.Instance.characterLists[i].GetComponent<Character>().Type == "Saber")
                {
                    SQLiteManager.Instance.characterLists[i].SetActive(true);
                }
                else
                {
                    SQLiteManager.Instance.characterLists[i].SetActive(false);
                }
            }
            for (int i = 0; i < Lists.Count; i++)
            {
                if (Lists[i].GetComponent<Equipments>().type == "Saber")
                {
                    Lists[i].SetActive(true);
                }
                else
                {
                    Lists[i].SetActive(false);
                }
            }
        });
        //展示所有的职业或者装备
        this.transform.Find("All").GetComponent<Toggle>().onValueChanged.AddListener((isOn) =>
        {
            for (int i = 0; i < SQLiteManager.Instance.characterLists.Count; i++)
            {
               
                    SQLiteManager.Instance.characterLists[i].SetActive(true);
                
            }
            for (int i = 0; i < Lists.Count; i++)
            {
                Lists[i].SetActive(true);
            }

        });
    }
}
