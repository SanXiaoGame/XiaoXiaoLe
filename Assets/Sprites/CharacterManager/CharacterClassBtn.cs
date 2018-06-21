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
        this.transform.Find("Enchanter0").GetComponent<Toggle>().onValueChanged.AddListener((isOn)=> {
            enchcanter.SetActive(isOn);
            for (int i = 0; i < SQLiteManager.Instance.characterLists.Count; i++)
            {
            if (SQLiteManager.Instance.characterLists[i].GetComponent<Character>().Type == "魔法师")
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
                if (Lists[i].GetComponent<Item>().type == "魔法师")
                {
                    Lists[i].SetActive(true);
                }
                else
                {
                    Lists[i].SetActive(false);
                }
            }
        });
        this.transform.Find("Hunter0").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            hunter.SetActive(isOn);
            for (int i = 0; i < SQLiteManager.Instance.characterLists.Count; i++)
            {
                if (SQLiteManager.Instance.characterLists[i].GetComponent<Character>().Type == "猎人")
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
                if (Lists[i].GetComponent<Item>().type == "猎人")
                {
                    Lists[i].SetActive(true);
                }
                else
                {
                    Lists[i].SetActive(false);
                }
            }
        });
        this.transform.Find("Berserker0").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            berserker.SetActive(isOn);
            for (int i = 0; i < SQLiteManager.Instance.characterLists.Count; i++)
            {
                if (SQLiteManager.Instance.characterLists[i].GetComponent<Character>().Type == "狂战士")
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
                if (Lists[i].GetComponent<Item>().type == "狂战士")
                {
                    Lists[i].SetActive(true);
                }
                else
                {
                    Lists[i].SetActive(false);
                }
            }
        });
        this.transform.Find("Knight0").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            knight.SetActive(isOn);
            for (int i = 0; i < SQLiteManager.Instance.characterLists.Count; i++)
            {
                if (SQLiteManager.Instance.characterLists[i].GetComponent<Character>().Type == "骑士")
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
                if (Lists[i].GetComponent<Item>().type == "骑士")
                {
                    Lists[i].SetActive(true);
                }
                else
                {
                    Lists[i].SetActive(false);
                }
            }
        });
        this.transform.Find("Swordman0").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            swordman.SetActive(isOn);
            for (int i = 0; i < SQLiteManager.Instance.characterLists.Count; i++)
            {
                if (SQLiteManager.Instance.characterLists[i].GetComponent<Character>().Type == "剑士")
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
                if (Lists[i].GetComponent<Item>().type == "剑士")
                {
                    Lists[i].SetActive(true);
                }
                else
                {
                    Lists[i].SetActive(false);
                }
            }
        });
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
