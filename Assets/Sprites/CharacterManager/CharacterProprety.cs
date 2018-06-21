using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterProprety : MonoBehaviour {

    Text _name;
    Text _profession;
    Text _exp;
    public Text AD { get; set; }
    public Text AP { get; set; }
    public Text HP { get; set; }
    public Text RES { get; set; }
    public Text DEF { get; set; }
  
    private void Awake()
    {
        _name = this.transform.Find("Name").GetComponent<Text>();
        _profession = this.transform.Find("Profession").GetComponent<Text>();
        _exp = this.transform.Find("EXP").GetComponent<Text>();
        AD = this.transform.Find("AD").GetComponent<Text>();
        AP = this.transform.Find("AP").GetComponent<Text>();
        HP = this.transform.Find("HP").GetComponent<Text>();
        RES = this.transform.Find("RES").GetComponent<Text>();
        DEF = this.transform.Find("DEF").GetComponent<Text>();
    }
    public void RefreshCharacterProprety(int id)
    {
       
        CharacterListData characterListData = SQLiteManager.Instance.characterDataSource[id];
        _name.text = characterListData.character_Name;
        _profession.text = characterListData.character_Class;
        _exp.text = characterListData.character_EXP.ToString();
        AD.text = characterListData.character_AD.ToString();
        AP.text = characterListData.character_AP.ToString();
        HP.text = characterListData.character_HP.ToString();
        RES.text = characterListData.character_RES.ToString();
        DEF.text = characterListData.character_DEF.ToString();
       
    }
}
