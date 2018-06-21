using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Item : MonoBehaviour {
    Transform[] bagLists = new Transform[10];
    Transform[] equiptHole = new Transform[2];
    public int ID { get; set; }
    public string type;
    private void Awake()
    {
        
        for (int i = 0; i < 10; i++)
        {
            string path = string.Format("item{0}", i);

            bagLists[i] = GameObject.FindGameObjectWithTag("Bag").transform.Find(path).transform;
            
        }
        GameObject[] equiptHoles = GameObject.FindGameObjectsWithTag("equiptHole");
        TopBtnManager topBtn = GameObject.FindGameObjectWithTag("TopBtn").GetComponent<TopBtnManager>();
        for (int i = 0; i < equiptHoles.Length; i++)
        {
            equiptHole[i] = equiptHoles[i].transform;
        }
        bool isCome = false;
        this.GetComponent<Button>().onClick.AddListener(()=> {
            
           
            if (topBtn.ID != 0 && SQLiteManager.Instance.characterDataSource[topBtn.ID].character_Class == type)
            {
                
                if (!isCome)
                {
                    for (int i = 0; i < equiptHole.Length; i++)
                    {
                        if (equiptHole[i].childCount == 0)
                        {
                            this.transform.parent = equiptHole[i].transform;
                            this.transform.localPosition = Vector3.zero;
                            isCome = true;
                           
                                    EquipmentData equipmentData = SQLiteManager.Instance.equipmentDataSource[ID];
                                    CharacterListData characterListData = SQLiteManager.Instance.characterDataSource[topBtn.ID];
                                    characterListData.character_AD += equipmentData.equipment_AD;                          
                                    characterListData.character_AP += equipmentData.equipment_AP;                          
                                    characterListData.character_DEF += equipmentData.equipment_DEF;
                            Debug.Log(SQLiteManager.Instance.characterDataSource[topBtn.ID].character_DEF);
                                    characterListData.character_HP += equipmentData.equipment_HP;
                                    characterListData.character_RES += equipmentData.equipment_RES;
                                 
                            topBtn.characterPoprety.GetComponent<CharacterProprety>().RefreshCharacterProprety(topBtn.ID);
                            break;


                        }
                    }
                }
                else
                {
                    for (int i = 0; i < bagLists.Length; i++)
                    {
                        if (bagLists[i].childCount == 0)
                        {
                            this.transform.parent = bagLists[i].transform;
                            this.transform.localPosition = Vector3.zero;
                            EquipmentData equipmentData = SQLiteManager.Instance.equipmentDataSource[ID];
                            CharacterListData characterListData = SQLiteManager.Instance.characterDataSource[topBtn.ID];
                            characterListData.character_AD -= equipmentData.equipment_AD;
                            characterListData.character_AP -= equipmentData.equipment_AP;
                            characterListData.character_DEF -= equipmentData.equipment_DEF;
                            characterListData.character_HP -= equipmentData.equipment_HP;
                            characterListData.character_RES -= equipmentData.equipment_RES;
                            topBtn.characterPoprety.GetComponent<CharacterProprety>().RefreshCharacterProprety(topBtn.ID);
                           
                            isCome = false;
                            break;
                        }
                    }
                }
              

            }
            
           

        });
    }
}
