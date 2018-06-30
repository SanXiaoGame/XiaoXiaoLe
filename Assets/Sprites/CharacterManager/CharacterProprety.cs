using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//角色信息面板
public class CharacterProprety : MonoBehaviour {

    Text _name;//角色的名字
    Text _profession;//角色的职业
    Text _exp;//角色经验
    [HideInInspector]
    public Text AD { get; set; }//角色的AD
    [HideInInspector]
    public Text AP { get; set; }//角色的AP
    [HideInInspector]
    public Text HP { get; set; }//角色的HP
    [HideInInspector]
    public Text RES { get; set; }//角色的RES
    [HideInInspector]
    public Text DEF { get; set; }//角色的DEF
  
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
    /// <summary>
    /// 角色信息的显示（刷新信息）
    /// </summary>
    /// <param name="id">角色ID</param>
    public void RefreshCharacterProprety(int id)
    {
       
        Hero hero = SQLiteManager.Instance.team[id];
        PlayerData playerData = SQLiteManager.Instance.playerDataSource[id];
        _name.text = playerData.player_Name;
        _profession.text = playerData.player_Class;
        _exp.text = playerData.EXP.ToString();
        AD.text = hero.currentAD.ToString();
        AP.text = hero.currentAP.ToString();
        HP.text = hero.currentHP.ToString();
        RES.text = hero.currentRES.ToString();
        DEF.text = hero.currentDEF.ToString();
       
    }
}
