using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Character : MonoBehaviour {
    
    internal Text Name { get; set; }//角色名字
    
    internal Text Lv { get; set; }//角色等级
    
    internal int ID { get; set; }//角色ID
    
    internal Image HeadPortrait { get; set; }//角色职业头像
    
    internal string Type { get; set; }//角色的职业
    private void Awake()
    {
        Name = transform.Find("Name").GetComponent<Text>();
        Lv = transform.Find("LV/lv").GetComponent<Text>();
        HeadPortrait = transform.Find("HeadPortrait/Profession").GetComponent<Image>();
        GameObject selectBtn = GameObject.FindGameObjectWithTag("SelectBtn");
        //选择准备战斗的角色
        this.GetComponent<Button>().onClick.AddListener(()=> {
            Debug.Log(selectBtn);
            if (selectBtn != null)
            {
               
                selectBtn.GetComponent<CharacterSelectBtn>().ID = ID;
               
            }
        });
    }
}
