using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeadPortrait : MonoBehaviour {
    public int ID { get; set; }
    private void Awake()
    {      
        GameObject characterProprety = GameObject.FindGameObjectWithTag("TopBtn").GetComponent<TopBtnManager>().characterPoprety;
        characterProprety.SetActive(false);
        
        SelectBtn selectBtn = GameObject.FindGameObjectWithTag("SelectBtn").GetComponent<SelectBtn>();
        this.GetComponent<Button>().onClick.AddListener(()=> {
            characterProprety.SetActive(true);
            characterProprety.AddComponent<CharacterProprety>();
            selectBtn.characterName = this.name;
            selectBtn.DelctID = ID;
            GameObject.FindGameObjectWithTag("TopBtn").GetComponent<TopBtnManager>().ID = ID;
            
            characterProprety.GetComponent<CharacterProprety>().RefreshCharacterProprety(ID);
        });
    }
 
}
