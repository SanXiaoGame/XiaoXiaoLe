using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//装备
public class Equipments : MonoBehaviour {
    Transform[] bagLists = new Transform[36];//存放背包各个空间的位置数组
    Transform[] equiptHole = new Transform[2];//存放装备孔空间的位置
    [HideInInspector]
    public int ID { get; set; }//装备ID
    [HideInInspector]
    public string type;//装备的角色职业类型
    bool isCome = false;
    TopBtnManager topBtn;
    private void Awake()
    {
        
        for (int i = 0; i < 10; i++)
        {
            string path = string.Format("item{0}", i);

            bagLists[i] = GameObject.FindGameObjectWithTag("Bag").transform.Find(path).transform;//查找并放入背包各个空间的位置
            
        }
        GameObject[] equiptHoles = GameObject.FindGameObjectsWithTag("equiptHole");//两个装备孔
         topBtn = GameObject.FindGameObjectWithTag("TopBtn").GetComponent<TopBtnManager>();
        for (int i = 0; i < equiptHoles.Length; i++)
        {
            equiptHole[i] = equiptHoles[i].transform;
        }
        
        
        this.GetComponent<Button>().onClick.AddListener(()=> {


            EquipmentAddAndRemove();
           

        });
    }
    /// <summary>
    /// 装备的穿脱
    /// </summary>
    void EquipmentAddAndRemove() {

        if (topBtn.ID != 0 && SQLiteManager.Instance.playerDataSource[topBtn.ID].player_Class == type)
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
                        Hero hero = SQLiteManager.Instance.team[topBtn.ID];
                        hero.currentAD += equipmentData.equipment_AD;
                        hero.currentAP += equipmentData.equipment_AP;
                        hero.currentDEF += equipmentData.equipment_DEF;

                        hero.currentHP += equipmentData.equipment_HP;
                        hero.currentRES += equipmentData.equipment_RES;

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

                        Hero hero = SQLiteManager.Instance.team[topBtn.ID];
                        hero.currentAD -= equipmentData.equipment_AD;
                        hero.currentAP -= equipmentData.equipment_AP;
                        hero.currentDEF -= equipmentData.equipment_DEF;

                        hero.currentHP -= equipmentData.equipment_HP;
                        hero.currentRES -= equipmentData.equipment_RES;
                        topBtn.characterPoprety.GetComponent<CharacterProprety>().RefreshCharacterProprety(topBtn.ID);

                        isCome = false;
                        break;
                    }
                }
            }


        }



    }
}
