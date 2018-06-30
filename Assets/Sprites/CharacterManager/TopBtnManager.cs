using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//各个界面按钮的切换管理
public class TopBtnManager : MonoBehaviour {
    [HideInInspector]
    public int ID;//战斗位置选择的角色的ID
    [HideInInspector]
    public GameObject characterPoprety;//角色的信息面板
    [HideInInspector]
    public GameObject bag;//角色的背包
    GameObject playerShow;
    GameObject playerShow0;
    GameObject player1;
    GameObject selectbtn;
    GameObject skillHole;
    GameObject equiptHole;
    GameObject playerCreate;
    private void Awake()
    {
        characterPoprety = GameObject.FindGameObjectWithTag("characterproprety");//查找角色信息面板
        characterPoprety.SetActive(false);
        playerShow = GameObject.FindGameObjectWithTag("PlayerShow");//查找显示的战斗的角色的位置   
        playerShow0 = GameObject.FindGameObjectWithTag("PlayerShow0");//查找个人角色的职业头像的显示位置
        playerShow0.SetActive(false);
        player1 = playerShow0.transform.Find("Player0").gameObject;
        selectbtn = GameObject.FindGameObjectWithTag("SelectBtn");//战斗角色的筛选管理
        skillHole = GameObject.FindGameObjectWithTag("SkillHole");//角色技能的显示位置查找
        skillHole.SetActive(false);
        bag = GameObject.FindGameObjectWithTag("Bag");//角色的背包
        bag.SetActive(false);
        equiptHole = GameObject.FindGameObjectWithTag("EquiptHole");//角色的装备孔显示位置
        equiptHole.SetActive(false);
        playerCreate = GameObject.FindGameObjectWithTag("PlayerCreate");//角色创建的显示位置
        this.transform.Find("Person").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("Team").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("Bag").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        //激活本身的所需的界面并关闭其他界面的展示
        this.transform.Find("Person").GetComponent<Toggle>().onValueChanged.AddListener((isOn)=> {
            playerShow0.SetActive(true);
            playerShow.SetActive(false);          
            skillHole.SetActive(true);
            if (skillHole.GetComponent<SkillManager>() == null)
            {
                skillHole.AddComponent<SkillManager>();
            }
            if (ID != 0)
            {
                string path = string.Format("Texture/Icon/Cube_{0}", SQLiteManager.Instance.playerDataSource[ID].player_Class);
                player1.GetComponent<Image>().sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
                skillHole.GetComponent<SkillManager>().RefreshSkill(ID);
            }
            playerCreate.SetActive(false);           
            selectbtn.SetActive(false);
            equiptHole.SetActive(false);
            bag.SetActive(false);
        });
        //激活本身的所需的界面并关闭其他界面的展示
        this.transform.Find("Team").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            if (playerCreate.transform.Find("PlayerCreate").GetComponent<CreateCharacter>() == null)
            {
                playerCreate.transform.Find("PlayerCreate").gameObject.AddComponent<CreateCharacter>();
            }
            selectbtn.SetActive(true);
            playerShow0.SetActive(false);
            skillHole.SetActive(false);
            playerCreate.SetActive(true);
            playerShow.SetActive(true);          
            equiptHole.SetActive(false);
            bag.SetActive(false);
        });
        //激活本身的所需的界面并关闭其他界面的展示
        this.transform.Find("Bag").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            playerShow0.SetActive(true);
            selectbtn.SetActive(false);
            if (ID != 0)
            {
                string path = string.Format("Texture/Icon/Cube_{0}", SQLiteManager.Instance.playerDataSource[ID].player_Class);
                player1.GetComponent<Image>().sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
            }         
            skillHole.SetActive(false);
            playerCreate.SetActive(false);
            playerShow.SetActive(false);           
            equiptHole.SetActive(true);
            bag.SetActive(true);
            if (bag.GetComponent<CreateEquipts>() == null)
            {
                bag.AddComponent<CreateEquipts>();
            }
        });
    }
    
}
