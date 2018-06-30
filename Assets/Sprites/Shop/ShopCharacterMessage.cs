using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ShopCharacterMessage : MonoBehaviour {
    Text _name;
    Text _price;
    Text _adAndAP;
    Text _hp;
    Text _defAndRES;
    Image _class;
    Text _rank;
    UISceneWidget blockClick;
    private void Awake()
    {
        _name = this.transform.Find("Name/name").GetComponent<Text>();
        _price = this.transform.Find("Price/price").GetComponent<Text>();
        _adAndAP = this.transform.Find("ADAndAP/adandap").GetComponent<Text>();
        _hp = this.transform.Find("HP/hp").GetComponent<Text>();
        _defAndRES = this.transform.Find("RESAndDEF/resanddef").GetComponent<Text>();
        _class = this.transform.Find("Name/Class/class").GetComponent<Image>();
        _rank = this.transform.Find("Rank").GetComponent<Text>();
        
    }
    private void Start()
    {

        blockClick = UISceneWidget.Get(gameObject);

        if (blockClick != null)
        {
            blockClick.PointerDown += BugBtn;
            

        }
    }
    void BugBtn(PointerEventData eventData) {
        GameObject target = eventData.pointerEnter;
        if (target.name == "Bug")
        {

          

        }
    }
    /// <summary>
    /// 显示商城的招募角色
    /// </summary>
    /// <param name="id">角色id</param>
    /// <param name="type">角色品质</param>
    public void ShowCharacterMesaage(int id,string type) {
        CharacterListData characterListData  = SQLiteManager.Instance.characterDataSource[id];
        _name.text = characterListData.character_Name;
        _hp.text = "HP:"+characterListData.character_HP.ToString();
        _defAndRES.text = "DEF:" + characterListData.character_DEF.ToString() + " " + "RES:" + characterListData.character_RES.ToString();
        _adAndAP.text = "AD:" + characterListData.character_AD.ToString() + " " + "AP:"+characterListData.character_AP.ToString();
        string path = string.Format("Texture/Icon/Class_{0}", characterListData.character_Class);
        _class.sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        _rank.text = "("+type+")";
    }
}
