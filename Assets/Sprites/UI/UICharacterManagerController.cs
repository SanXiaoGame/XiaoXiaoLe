﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UICharacterManagerController : MonoBehaviour, IUIBase
{
    Text heroName;
    Text LV;
    Text EXP;
    Text HP;
    Text AD;
    Text AP;
    Text DEF;
    Text RES;


    //进入界面
    public void OnEntering()
    {
        gameObject.SetActive(true);
        #region 系统区点击
        GameObject characterIcon = transform.Find(ConstData.SystemArea_CharacterIcon).gameObject;
        GameObject teamIcon = transform.Find(ConstData.SystemArea_TeamIcon).gameObject;
        GameObject equipmentIcon = transform.Find(ConstData.SystemArea_EquipmentIcon).gameObject;
        GameObject mainCityIcon = transform.Find(ConstData.SystemArea_MainCityIcon).gameObject;
        UISceneWidget characterButtonClick = UISceneWidget.Get(characterIcon);
        UISceneWidget teamButtonClick = UISceneWidget.Get(teamIcon);
        UISceneWidget equipmentButtonClick = UISceneWidget.Get(equipmentIcon);
        UISceneWidget mainCityButtonClick = UISceneWidget.Get(mainCityIcon);
        #endregion
        #region 筛选区点击
        GameObject saberStone = transform.Find(ConstData.Filter_StoneSaberTag).gameObject;
        GameObject knightStone = transform.Find(ConstData.Filter_StoneKnightTag).gameObject;
        GameObject berserkerStone = transform.Find(ConstData.Filter_StoneBerserkerTag).gameObject;
        GameObject casterStone = transform.Find(ConstData.Filter_StoneCasterTag).gameObject;
        GameObject hunterStone = transform.Find(ConstData.Filter_StoneHunterTag).gameObject;
        UISceneWidget saberStoneClick = UISceneWidget.Get(saberStone);
        UISceneWidget knightStoneClick = UISceneWidget.Get(knightStone);
        UISceneWidget berserkerStoneClick = UISceneWidget.Get(berserkerStone);
        UISceneWidget casterStoneClick = UISceneWidget.Get(casterStone);
        UISceneWidget hunterStoneClick = UISceneWidget.Get(hunterStone);
        #endregion
        #region 操作区域附属
        GameObject skill01 = transform.Find(ConstData.ControllerExArea_SkillMode).transform.GetChild(0).gameObject;
        GameObject skill02 = transform.Find(ConstData.ControllerExArea_SkillMode).transform.GetChild(1).gameObject;
        GameObject skill03 = transform.Find(ConstData.ControllerExArea_SkillMode).transform.GetChild(2).gameObject;
        UISceneWidget skill01Click = UISceneWidget.Get(skill01);
        UISceneWidget skill02Click = UISceneWidget.Get(skill02);
        UISceneWidget skill03Click = UISceneWidget.Get(skill03);
        GameObject teamEditUP = transform.Find(ConstData.ControllerExArea_TeamModeUP).gameObject;
        GameObject teamEditDOWN = transform.Find(ConstData.ControllerExArea_TeamModeDOWN).gameObject;
        GameObject teamEditOK = transform.Find(ConstData.ControllerExArea_TeamModeCONFIRM).gameObject;
        UISceneWidget teamEditUPClick = UISceneWidget.Get(teamEditUP);
        UISceneWidget teamEditDOWNClick = UISceneWidget.Get(teamEditDOWN);
        UISceneWidget teamEditOKClick = UISceneWidget.Get(teamEditOK);
        GameObject weaponHole = transform.Find(ConstData.ControllerExArea_EquipmentMode).transform.GetChild(0).gameObject;
        GameObject equipmentHole = transform.Find(ConstData.ControllerExArea_EquipmentMode).transform.GetChild(1).gameObject;
        UISceneWidget weaponHoleClick = UISceneWidget.Get(weaponHole);
        UISceneWidget equipmentHoleClick = UISceneWidget.Get(equipmentHole);
        #endregion
        #region 操作区域附属
        //GameObject skill01 = transform.Find(ConstData.ControllerExArea_SkillMode).transform.GetChild(0).gameObject;
        //UISceneWidget skill01Click = UISceneWidget.Get(skill01);
        
        #endregion
    }
    //退出界面
    public void OnExiting()
    {
        throw new System.NotImplementedException();
    }
    //暂停界面
    public void OnPausing()
    {
        throw new System.NotImplementedException();
    }
    //唤醒界面
    public void OnResuming()
    {
        throw new System.NotImplementedException();
    }
}