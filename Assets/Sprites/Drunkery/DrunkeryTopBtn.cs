using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DrunkeryTopBtn : MonoBehaviour {
    internal GameObject characterMessage;
    GameObject characterShow;
    UISceneWidget blockClick;
    GameObject Btn1;
    GameObject Btn2;
   //二级拥有的稀有角色
    private void Awake()
    {
        characterMessage = GameObject.FindGameObjectWithTag("characterproprety").gameObject;
        characterMessage.SetActive(false);
        characterShow = GameObject.FindGameObjectWithTag("Player").gameObject;
        //Btn1 = this.transform.Find("Btn/1").gameObject;
        //Btn1.SetActive(false);
        //Btn2 = this.transform.Find("BackBtn/2").gameObject;
        //Btn2.SetActive(false);
    }
    private void Start()
    {
        
        blockClick = UISceneWidget.Get(gameObject);

        if (blockClick != null)
        {
            blockClick.PointerDown += Btn;
            blockClick.PointerDown += BackBtn;

        }
    }
    bool isOver = true;
    void Btn(PointerEventData eventData) {
        
        GameObject target = eventData.pointerEnter;
        if (target.name == "Btn")
        {

            if (isOver)
            {
                int id1 = Character1();
                int id2 = Character2();
                int id3 = Character3();

                characterShow.GetComponent<DrunkeryCharacterShow>().ShowCharacter(id1, id2, id3, isRare);
                isOver = false;
            }
            
        }
    }
    void BackBtn(PointerEventData eventData) {
        GameObject target = eventData.pointerEnter;
        if (target.name == "BackBtn")
        {
            Btn2.SetActive(true);
            Btn1.SetActive(false);


        }
    }
    int[] normalCharacter1ID2 = { 1012, 1021, 1041, 1045, 1051, 1053, 1066, 1075 };//二级1%概率拥有的普通角色
    int[] normalCharacter2ID2 = { 1002, 1003, 1004, 1005, 1006, 1007, 1008,
        1009, 1011, 1013, 1014, 1016, 1017, 1020, 1022, 1025, 1026, 1027,
        1028,1030,1031,1032,1033,1034,1035,1036,1037,1038,1040,1044,1049,
        1052,1054,1056,1057,1060,1061,1063,1064,1065, 1608,1069,1071,
        1072,1073,1076};//二级2%概率拥有的普通角色

    int[] rareCharacter1ID2 = { 1018, 1029, 1048, 1055, 1074 };//二级1%概率拥有的稀有角色
    int[] rareCharacter2ID2 = {1010,1015,1019,1023,1024,1031,
        1039,1042,1043,1050,1058,1059,1062,1063,1067,1070,};//二级2%概率拥有的稀有角色
    int[] normalCharacterID1 = { 1002,1003,1004,1005,1006,1007,1008,1009,
        1011,1012,1013,1014,1016,1017,1020,1021,1022,1025,1026,1027,1028,
        1030,1031,1032,1033,1034,1035,1036,1037,1038,1040,1041,1044,1045,
        1049,1051,1052,1053,1054,1056,1057,1060,1061,1063,1064,1065,1066,
        1608,1069,1071,1072,1073,1075,1076};//一级拥有的普通角色
    int[] rareCharacterID1 = { 1010,1015,1018,1019,1023,1024,1029,1031,
        1039,1042,1043,1048,1050,1055,1058,1059,1062,1063,1067,1070,1074 };
    bool isRare = false;
    /// <summary>
    /// 筛选1级角色
    /// </summary>
    /// <returns>袭击返回一个角色的ID</returns>
    int Character1()
    {
        int a = Random.Range(1,101);
        if (a <= 97)
        {
            int b = Random.Range(0,54);
            return normalCharacterID1[b];
        }
        else
        {
            isRare = true;
            int b = Random.Range(0, 21);
            return rareCharacterID1[b];
            
        }
    }
    /// <summary>
    /// 筛选二级普通角色
    /// </summary>
    /// <returns>随机返回一个普通角色ID</returns>
    int Character2() {
        int a = Random.Range(1, 101);
        if (a < 92  )
        {
            int b = Random.Range(0, 46);
            return normalCharacter2ID2[b];
        }
        else
        {
            int b = Random.Range(0, 8);
            return normalCharacter1ID2[b];
        }
    }
    /// <summary>
    /// 筛选二级稀有角色
    /// </summary>
    /// <returns>随机返回一个稀有的角色ID</returns>
    int Character3()
    {
        int a = Random.Range(1, 101);
        if (a < 81)
        {
            int b = Random.Range(0, 16);
            return rareCharacter2ID2[b];
        }
        else
        {
            int b = Random.Range(0, 5);
            return rareCharacter1ID2[b];
        }
    }
}
