using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Character : MonoBehaviour {
    
    public Text Name { get; set; }
    
    public Text Lv { get; set; }
    
    public int ID { get; set; }

    public Image HeadPortrait { get; set; }
    public string Type { get; set; }
    private void Awake()
    {
        Name = transform.Find("Name").GetComponent<Text>();
        Lv = transform.Find("LV/lv").GetComponent<Text>();
        HeadPortrait = transform.Find("HeadPortrait/Profession").GetComponent<Image>();
        GameObject selectBtn = GameObject.FindGameObjectWithTag("SelectBtn");
        this.GetComponent<Button>().onClick.AddListener(()=> {
            Debug.Log(selectBtn);
            if (selectBtn != null)
            {
               
                selectBtn.GetComponent<SelectBtn>().ID = ID;
               
            }
        });
    }
}
