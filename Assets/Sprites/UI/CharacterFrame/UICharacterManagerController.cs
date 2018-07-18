﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UICharacterManagerController : MonoBehaviour, IUIBase
{
    //预制体外：游戏画面区域
    GameObject GameArea;
    //踏台
    GameObject step01;
    GameObject step02;
    GameObject step03;
    GameObject step04;
    GameObject step05;
    //选中英雄的数据
    PlayerData selecteHero;
    //Message显示信息的物体和UI组件
    Text heroName;
    Text LV;
    Slider EXP;
    Text HP;
    Text AD;
    Text AP;
    Text DEF;
    Text RES;
    Text GoldCoin;
    Text IntroductionText;
    GameObject introductionFrame;
    GameObject confirmFrame;
    GameObject confirmFrame_EQ;
    Text deleteCharacterMessage;
    Text selecteEquipmentMessage;
    //技能图标
    Image skillImage_A;
    Image skillImage_B;
    Image skillImage_C;
    //当前选中的角色和编号
    GameObject selectPlayer;
    int selectBarNum;
    //选中要解雇的角色编号
    int deleteNum;
    //选中要替换的装备
    GameObject equipmentG;
    int equipmentID;

    //角色条预制体
    GameObject characterBar;
    //装备格预制体
    GameObject itemGrid;
    //装备图标预制体
    GameObject itemBar;
    //角色条总数列表
    List<GameObject> playerList;
    //踏台列表
    List<GameObject> stepList;
    //带道具装备格对象表
    List<GameObject> itemList;
    //空装备格对象表
    List<GameObject> null_itemList;
    //筛选列表
    List<GameObject> filterList;
    //临时队伍字典
    Dictionary<string, PlayerData> tempTeamDic;

    //角色页面、装备页面和队伍页面开关
    bool isTeam = false;
    bool isEquipt = false;

    //需要操作的界面
    GameObject G_GoldCoinFrame;
    GameObject G_ItemListBG;
    GameObject G_CharacterListBG;
    GameObject G_SkillMode;
    GameObject G_TeamMode;
    GameObject G_EquipmentMode;
    GameObject G_FilterFrame;
    GameObject G_IntroductionFrame;
    GameObject G_ConfirmFrame;

    private void Awake()
    {
        
    }

    //进入界面
    public void OnEntering()
    {
        gameObject.SetActive(true);
        #region 召唤画面区域UI预制体并且获得游戏画面区域控制权
        GameArea = ObjectPoolManager.Instance.InstantiateMyGameObject
            (ResourcesManager.Instance.FindUIPrefab(ConstData.UICharacterManage_GameArea));
        GameArea.transform.position = ResourcesManager.Instance.FindUIPrefab(ConstData.UICharacterManage_GameArea).transform.position;
        stepList = new List<GameObject>();
        stepList.Clear();
        step01 = GameArea.transform.GetChild(1).gameObject;
        step02 = GameArea.transform.GetChild(2).gameObject;
        step03 = GameArea.transform.GetChild(3).gameObject;
        step04 = GameArea.transform.GetChild(4).gameObject;
        step05 = GameArea.transform.GetChild(5).gameObject;
        stepList.Add(step01);
        stepList.Add(step02);
        stepList.Add(step03);
        stepList.Add(step04);
        stepList.Add(step05);
        #endregion

        //初始化设置//
        isTeam = false;
        isEquipt = false;
        gameObject.SetActive(true);
        selecteHero = null;
        selectPlayer = null;
        selectBarNum = 0;
        deleteNum = 0;
        playerList = new List<GameObject>();
        playerList.Clear();
        itemList = new List<GameObject>();
        itemList.Clear();
        null_itemList = new List<GameObject>();
        null_itemList.Clear();
        filterList = new List<GameObject>();
        filterList.Clear();
        tempTeamDic = new Dictionary<string, PlayerData>();
        tempTeamDic.Clear();
        tempTeamDic.Add(ConstData.FlagMan, SQLiteManager.Instance.playerDataSource[1300]);
        tempTeamDic.Add(ConstData.Saber, null);
        tempTeamDic.Add(ConstData.Knight, null);
        tempTeamDic.Add(ConstData.Berserker, null);
        tempTeamDic.Add(ConstData.Caster, null);
        tempTeamDic.Add(ConstData.Hunter, null);
        characterBar = ResourcesManager.Instance.FindUIPrefab(ConstData.CharacterBar);
        itemGrid = ResourcesManager.Instance.FindUIPrefab(ConstData.Grid);
        itemBar = ResourcesManager.Instance.FindUIPrefab(ConstData.GridEx);

        #region UI组件获取（动态显示选中物品和英雄对象信息）
        heroName = transform.Find(ConstData.GameArea_MessageFrame_Name).GetComponent<Text>();
        LV = transform.Find(ConstData.GameArea_MessageFrame_LV).GetComponent<Text>();
        EXP = transform.Find(ConstData.GameArea_MessageFrame_EXPSlider).GetComponent<Slider>();
        HP = transform.Find(ConstData.GameArea_MessageFrame_HP).GetComponent<Text>();
        AD = transform.Find(ConstData.GameArea_MessageFrame_AD).GetComponent<Text>();
        AP = transform.Find(ConstData.GameArea_MessageFrame_AP).GetComponent<Text>();
        DEF = transform.Find(ConstData.GameArea_MessageFrame_DEF).GetComponent<Text>();
        RES = transform.Find(ConstData.GameArea_MessageFrame_RES).GetComponent<Text>();
        GoldCoin = transform.Find(ConstData.GameArea_GoldCoin).GetComponent<Text>();
        IntroductionText = transform.Find(ConstData.Introduction_Content).GetComponent<Text>();
        deleteCharacterMessage = transform.Find(ConstData.ConfirmFrame_ContentText).GetComponent<Text>();
        selecteEquipmentMessage = transform.Find(ConstData.ConfirmFrame_EQ_ContentText).GetComponent<Text>();
        skillImage_A = transform.Find(ConstData.ControllerExArea_SkillMode).GetChild(0).GetChild(0).GetComponent<Image>();
        skillImage_B = transform.Find(ConstData.ControllerExArea_SkillMode).GetChild(1).GetChild(0).GetComponent<Image>();
        skillImage_C = transform.Find(ConstData.ControllerExArea_SkillMode).GetChild(2).GetChild(0).GetComponent<Image>();
        #endregion

        #region 系统区点击（角色页面、编队页面、装备页面、主城页面）
        GameObject characterIcon = transform.Find(ConstData.SystemArea_CharacterIcon).gameObject;
        GameObject teamIcon = transform.Find(ConstData.SystemArea_TeamIcon).gameObject;
        GameObject equipmentIcon = transform.Find(ConstData.SystemArea_EquipmentIcon).gameObject;
        GameObject mainCityIcon = transform.Find(ConstData.SystemArea_MainCityIcon).gameObject;
        if (characterIcon.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget characterButtonClick = UISceneWidget.Get(characterIcon);
            characterButtonClick.PointerClick += CharacterSwitch;
        }
        if (teamIcon.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget teamButtonClick = UISceneWidget.Get(teamIcon);
            teamButtonClick.PointerClick += TeamSwitch;
        }
        if (equipmentIcon.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget equipmentButtonClick = UISceneWidget.Get(equipmentIcon);
            equipmentButtonClick.PointerClick += EquipmentSwitch;
        }
        if (mainCityIcon.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget mainCityButtonClick = UISceneWidget.Get(mainCityIcon);
            mainCityButtonClick.PointerClick += MainCitySwitch;
        }
        characterIcon.GetComponent<Toggle>().isOn = true;
        teamIcon.GetComponent<Toggle>().isOn = false;
        equipmentIcon.GetComponent<Toggle>().isOn = false;
        mainCityIcon.GetComponent<Toggle>().isOn = false;
        #endregion

        #region 筛选区点击（根据职业筛选出对应信息）
        GameObject saberStone = transform.Find(ConstData.Filter_StoneSaberTag).gameObject;
        GameObject knightStone = transform.Find(ConstData.Filter_StoneKnightTag).gameObject;
        GameObject berserkerStone = transform.Find(ConstData.Filter_StoneBerserkerTag).gameObject;
        GameObject casterStone = transform.Find(ConstData.Filter_StoneCasterTag).gameObject;
        GameObject hunterStone = transform.Find(ConstData.Filter_StoneHunterTag).gameObject;
        if (saberStone.GetComponent<UISceneWidget>() == null && knightStone.GetComponent<UISceneWidget>() == null && berserkerStone.
            GetComponent<UISceneWidget>() == null && casterStone.GetComponent<UISceneWidget>() == null && hunterStone.GetComponent
            <UISceneWidget>() == null)
        {
            UISceneWidget saberStoneClick = UISceneWidget.Get(saberStone);
            UISceneWidget knightStoneClick = UISceneWidget.Get(knightStone);
            UISceneWidget berserkerStoneClick = UISceneWidget.Get(berserkerStone);
            UISceneWidget casterStoneClick = UISceneWidget.Get(casterStone);
            UISceneWidget hunterStoneClick = UISceneWidget.Get(hunterStone);
            saberStoneClick.PointerClick += SaberFilter;
            knightStoneClick.PointerClick += KnightFilter;
            berserkerStoneClick.PointerClick += BerserkerFilter;
            casterStoneClick.PointerClick += CasterFilter;
            hunterStoneClick.PointerClick += HunterFilter;
        }
        filterList.Add(saberStone);
        filterList.Add(knightStone);
        filterList.Add(berserkerStone);
        filterList.Add(casterStone);
        filterList.Add(hunterStone);
        saberStone.GetComponent<Toggle>().isOn = false;
        knightStone.GetComponent<Toggle>().isOn = false;
        berserkerStone.GetComponent<Toggle>().isOn = false;
        casterStone.GetComponent<Toggle>().isOn = false;
        hunterStone.GetComponent<Toggle>().isOn = false;
        #endregion

        #region 操作区域附属（角色页面的技能栏、编队页面的编队按钮、装备页面的装备槽、主城页面的背包种类筛选）
        GameObject skill01 = transform.Find(ConstData.ControllerExArea_SkillMode).transform.GetChild(0).gameObject;
        GameObject skill02 = transform.Find(ConstData.ControllerExArea_SkillMode).transform.GetChild(1).gameObject;
        GameObject skill03 = transform.Find(ConstData.ControllerExArea_SkillMode).transform.GetChild(2).gameObject;
        if (skill01.GetComponent<UISceneWidget>() == null && skill02.GetComponent<UISceneWidget>() == null &&
            skill03.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget skill01Click = UISceneWidget.Get(skill01);
            UISceneWidget skill02Click = UISceneWidget.Get(skill02);
            UISceneWidget skill03Click = UISceneWidget.Get(skill03);
            skill01Click.PointerClick += SkillFrame_A;
            skill02Click.PointerClick += SkillFrame_B;
            skill03Click.PointerClick += SkillFrame_C;
        }
        GameObject teamEditUP = transform.Find(ConstData.ControllerExArea_TeamModeUP).gameObject;
        GameObject teamEditDOWN = transform.Find(ConstData.ControllerExArea_TeamModeDOWN).gameObject;
        GameObject teamEditOK = transform.Find(ConstData.ControllerExArea_TeamModeCONFIRM).gameObject;
        if (teamEditUP.GetComponent<UISceneWidget>() == null && teamEditDOWN.GetComponent<UISceneWidget>() == null &&
            teamEditOK.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget teamEditUPClick = UISceneWidget.Get(teamEditUP);
            UISceneWidget teamEditDOWNClick = UISceneWidget.Get(teamEditDOWN);
            UISceneWidget teamEditOKClick = UISceneWidget.Get(teamEditOK);
            teamEditUPClick.PointerClick += AddTeamMember;
            teamEditDOWNClick.PointerClick += DeleteTeamMember;
            teamEditOKClick.PointerClick += TeamMemberConfirm;
        }
        GameObject weaponHole = transform.Find(ConstData.ControllerExArea_EquipmentMode).transform.GetChild(0).gameObject;
        GameObject equipmentHole = transform.Find(ConstData.ControllerExArea_EquipmentMode).transform.GetChild(1).gameObject;
        if (weaponHole.GetComponent<UISceneWidget>() == null && equipmentHole.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget weaponHoleClick = UISceneWidget.Get(weaponHole);
            UISceneWidget equipmentHoleClick = UISceneWidget.Get(equipmentHole);
            weaponHoleClick.PointerClick += WeaponHoleMessage;
            equipmentHoleClick.PointerClick += EquipmentHoleMessage;
        }
        GameObject skillClose = transform.Find(ConstData.Introduction_CloseButton).gameObject;
        if (skillClose.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget skillCloseClick = UISceneWidget.Get(skillClose);
            skillCloseClick.PointerClick += CloseWindow_Introduction;
        }
        #endregion

        #region 操作区
        CharacterBarCreate(ConstData.All);
        ItemBarCreate(ConstData.All);
        #endregion

        #region 窗口显示和对象获取
        //金币显示
        GoldCoin.text = CurrencyManager.Instance.GoldCoinDisplay();
        //简介窗口
        introductionFrame = transform.Find(ConstData.Introduction).gameObject;
        //确认窗口
        confirmFrame = transform.Find(ConstData.ConfirmFrame).gameObject;
        GameObject confirm_OK = transform.Find(ConstData.ConfirmFrame_ConfirmButton).gameObject;
        GameObject cancel_NO = transform.Find(ConstData.ConfirmFrame_CancelButton).gameObject;
        if (confirm_OK.GetComponent<UISceneWidget>() == null && cancel_NO.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget confirmClick = UISceneWidget.Get(confirm_OK);
            UISceneWidget cancelClick = UISceneWidget.Get(cancel_NO);
            confirmClick.PointerClick += ConfirmOK;
            cancelClick.PointerClick += CancelNO;
        }
        //装备替换窗口
        confirmFrame_EQ = transform.Find(ConstData.ConfirmFrame_EQ).gameObject;
        GameObject confirm_OK_EQ = transform.Find(ConstData.ConfirmFrame_EQ_ConfirmButton).gameObject;
        GameObject cancel_NO_EQ = transform.Find(ConstData.ConfirmFrame_EQ_CancelButton).gameObject;
        if (confirm_OK_EQ.GetComponent<UISceneWidget>() == null && cancel_NO_EQ.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget confirm2Click = UISceneWidget.Get(confirm_OK_EQ);
            UISceneWidget cancel2Click = UISceneWidget.Get(cancel_NO_EQ);
            confirm2Click.PointerClick += ConfirmOK_EQ;
            cancel2Click.PointerClick += CancelNO_EQ;
        }
        //对象获取
        G_GoldCoinFrame = transform.Find(ConstData.GameArea_GoldCoin).transform.parent.gameObject;
        G_ItemListBG = transform.Find(ConstData.ControllerArea_ItemListBG).gameObject;
        G_CharacterListBG = transform.Find(ConstData.ControllerArea_CharacterListBG).gameObject;
        G_SkillMode = transform.Find(ConstData.ControllerExArea_SkillMode).gameObject;
        G_TeamMode = transform.Find(ConstData.ControllerExArea_TeamMode).gameObject;
        G_EquipmentMode = transform.Find(ConstData.ControllerExArea_EquipmentMode).gameObject;
        G_FilterFrame = transform.Find(ConstData.Filter).gameObject;
        G_IntroductionFrame = transform.Find(ConstData.Introduction).gameObject;
        G_ConfirmFrame = transform.Find(ConstData.ConfirmFrame).gameObject;
        #endregion
        //装备区拖拽获取
        itemList_Drag = transform.Find(ConstData.ControllerArea_ItemListBG).gameObject;
        //信息初始化
        HeroDataDisplay();
    }
    //退出界面
    public void OnExiting()
    {
        //清空画面区域
        GameAreaClear("Team", "");
        CharacterBarClear();
        ObjectPoolManager.Instance.RecycleMyGameObject(GameArea);
        //清空装备区域
        EquipmentClear();
        gameObject.SetActive(false);
        ItemBarClear();
        //控制区域激活和关闭
        G_ItemListBG.gameObject.SetActive(false);
        G_CharacterListBG.gameObject.SetActive(true);
        //控制区域附属区激活和关闭
        G_SkillMode.gameObject.SetActive(true);
        G_TeamMode.gameObject.SetActive(false);
        G_EquipmentMode.gameObject.SetActive(false);
        //信息框隐藏
        G_IntroductionFrame.gameObject.SetActive(false);
        //确认框隐藏
        G_ConfirmFrame.gameObject.SetActive(false);
    }
    //暂停界面（无方法）
    public void OnPausing()
    {
        gameObject.SetActive(false);
    }
    //唤醒界面（无方法）
    public void OnResuming()
    {
        gameObject.SetActive(true);
    }

    //系统操作区切换显示的功能界面
    void CharacterSwitch(PointerEventData eventData)
    {
        //数值重置
        isTeam = false;
        isEquipt = false;
        if (selecteHero == null)
        {
            selectBarNum = 0;
            selectPlayer = null;
            heroName.text = "无";
            LV.text = "00";
            EXP.minValue = 0;
            EXP.maxValue = 1;
            EXP.value = 0;
            HP.text = "000";
            AD.text = "000";
            AP.text = "000";
            DEF.text = "000";
            RES.text = "000";
        }
        //画面区域激活和关闭
        GameAreaClear("Team","");
        if (selecteHero != null)
        {
            GameObject chara = ObjectPoolManager.Instance.InstantiateMyGameObject(selectPlayer);
            chara.transform.position = step01.transform.GetChild(0).transform.position;
            chara.transform.parent = step01.transform;
            chara.name = (selecteHero.player_Id).ToString();
            chara.GetComponent<Animator>().SetBool("isWait", true);
        }
        //控制区域激活和关闭
        G_ItemListBG.gameObject.SetActive(false);
        G_CharacterListBG.gameObject.SetActive(true);
        //控制区域附属区激活和关闭
        G_SkillMode.gameObject.SetActive(true);
        G_TeamMode.gameObject.SetActive(false);
        G_EquipmentMode.gameObject.SetActive(false);
        //信息框隐藏
        G_IntroductionFrame.gameObject.SetActive(false);
        //确认框隐藏
        G_ConfirmFrame.gameObject.SetActive(false);
    }
    void TeamSwitch(PointerEventData eventData)
    {
        //改变开关
        isTeam = true;
        isEquipt = false;
        //清除角色
        GameAreaClear("Single", "Default");
        if (SQLiteManager.Instance.team[ConstData.FlagMan] != null && SQLiteManager.Instance.team[ConstData.Saber] != null &&
            SQLiteManager.Instance.team[ConstData.Knight] != null && SQLiteManager.Instance.team[ConstData.Berserker] != null &&
            SQLiteManager.Instance.team[ConstData.Caster] != null && SQLiteManager.Instance.team[ConstData.Hunter] != null)
        {
            tempTeamDic[ConstData.Saber] = SQLiteManager.Instance.team[ConstData.Saber].playerData;
            tempTeamDic[ConstData.Knight] = SQLiteManager.Instance.team[ConstData.Knight].playerData;
            tempTeamDic[ConstData.Berserker] = SQLiteManager.Instance.team[ConstData.Berserker].playerData;
            tempTeamDic[ConstData.Caster] = SQLiteManager.Instance.team[ConstData.Caster].playerData;
            tempTeamDic[ConstData.Hunter] = SQLiteManager.Instance.team[ConstData.Hunter].playerData;
        }
        //画面区域生成所有英雄角色
        ShowTheTeamMumber("All");
        //操作区域初始化
        transform.Find(ConstData.ControllerArea_ItemListBG).gameObject.SetActive(false);
        if (G_CharacterListBG.gameObject.activeSelf == false)
        {
            G_CharacterListBG.gameObject.SetActive(true);
        }
        //操作区附属激活与关闭
        transform.Find(ConstData.ControllerExArea_SkillMode).gameObject.SetActive(false);
        transform.Find(ConstData.ControllerExArea_TeamMode).gameObject.SetActive(true);
        transform.Find(ConstData.ControllerExArea_EquipmentMode).gameObject.SetActive(false);
        //多余窗口关闭
        transform.Find(ConstData.Introduction).gameObject.SetActive(false);
        transform.Find(ConstData.ConfirmFrame).gameObject.SetActive(false);
    }
    void EquipmentSwitch(PointerEventData eventData)
    {
        //开关设置
        isTeam = false;
        isEquipt = true;
        //画面区域激活和关闭
        GameAreaClear("Team", "");
        if (selecteHero != null)
        {
            GameObject chara = ObjectPoolManager.Instance.InstantiateMyGameObject(selectPlayer);
            chara.transform.position = step01.transform.GetChild(0).transform.position;
            chara.transform.parent = step01.transform;
            chara.name = (selecteHero.player_Id).ToString();
            chara.GetComponent<Animator>().SetBool("isWait", true);
        }
        //控制区域激活和关闭
        transform.Find(ConstData.ControllerArea_ItemListBG).gameObject.SetActive(true);
        transform.Find(ConstData.ControllerArea_CharacterListBG).gameObject.SetActive(false);
        //控制区域附属区激活和关闭
        transform.Find(ConstData.ControllerExArea_SkillMode).gameObject.SetActive(false);
        transform.Find(ConstData.ControllerExArea_TeamMode).gameObject.SetActive(false);
        transform.Find(ConstData.ControllerExArea_EquipmentMode).gameObject.SetActive(true);
        //生成装备
        UpdataTheEquipment();
        //信息框隐藏
        transform.Find(ConstData.Introduction).gameObject.SetActive(false);
        //确认框隐藏
        transform.Find(ConstData.ConfirmFrame).gameObject.SetActive(false);
    }
    void MainCitySwitch(PointerEventData eventData)
    {
        #region team字典算最终数值(total)
        if (SQLiteManager.Instance.team[ConstData.FlagMan] != null && SQLiteManager.Instance.team[ConstData.Saber] != null &&
            SQLiteManager.Instance.team[ConstData.Knight] != null && SQLiteManager.Instance.team[ConstData.Berserker] != null &&
            SQLiteManager.Instance.team[ConstData.Caster] != null && SQLiteManager.Instance.team[ConstData.Hunter] != null)
        {
            //旗手
            SQLiteManager.Instance.team[ConstData.FlagMan].totalHP = SQLiteManager.Instance.team[ConstData.FlagMan].playerData.HP;
            SQLiteManager.Instance.team[ConstData.FlagMan].totalAD = 0;
            SQLiteManager.Instance.team[ConstData.FlagMan].totalAP = 0;
            SQLiteManager.Instance.team[ConstData.FlagMan].totalDEF = SQLiteManager.Instance.team[ConstData.FlagMan].playerData.DEF;
            SQLiteManager.Instance.team[ConstData.FlagMan].totalRES = SQLiteManager.Instance.team[ConstData.FlagMan].playerData.RES;
            //剑士
            SQLiteManager.Instance.team[ConstData.Saber].totalHP = SQLiteManager.Instance.team[ConstData.Saber].playerData.HP +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Saber].playerData.Weapon].equipment_HP +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Saber].playerData.Equipment].equipment_HP;
            SQLiteManager.Instance.team[ConstData.Saber].totalAD = SQLiteManager.Instance.team[ConstData.Saber].playerData.AD +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Saber].playerData.Weapon].equipment_AD +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Saber].playerData.Equipment].equipment_AD;
            SQLiteManager.Instance.team[ConstData.Saber].totalAP = 0;
            SQLiteManager.Instance.team[ConstData.Saber].totalDEF = SQLiteManager.Instance.team[ConstData.Saber].playerData.DEF +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Saber].playerData.Weapon].equipment_DEF +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Saber].playerData.Equipment].equipment_DEF;
            SQLiteManager.Instance.team[ConstData.Saber].totalRES = SQLiteManager.Instance.team[ConstData.Saber].playerData.RES +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Saber].playerData.Weapon].equipment_RES +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Saber].playerData.Equipment].equipment_RES;
            //骑士
            SQLiteManager.Instance.team[ConstData.Knight].totalHP = SQLiteManager.Instance.team[ConstData.Knight].playerData.HP +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Knight].playerData.Weapon].equipment_HP +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Knight].playerData.Equipment].equipment_HP;
            SQLiteManager.Instance.team[ConstData.Knight].totalAD = SQLiteManager.Instance.team[ConstData.Knight].playerData.AD +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Knight].playerData.Weapon].equipment_AD +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Knight].playerData.Equipment].equipment_AD;
            SQLiteManager.Instance.team[ConstData.Knight].totalAP = SQLiteManager.Instance.team[ConstData.Knight].playerData.AP +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Knight].playerData.Weapon].equipment_AP +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Knight].playerData.Equipment].equipment_AP;
            SQLiteManager.Instance.team[ConstData.Knight].totalDEF = SQLiteManager.Instance.team[ConstData.Knight].playerData.DEF +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Knight].playerData.Weapon].equipment_DEF +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Knight].playerData.Equipment].equipment_DEF;
            SQLiteManager.Instance.team[ConstData.Knight].totalRES = SQLiteManager.Instance.team[ConstData.Knight].playerData.RES +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Knight].playerData.Weapon].equipment_RES +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Knight].playerData.Equipment].equipment_RES;
            //狂战士
            SQLiteManager.Instance.team[ConstData.Berserker].totalHP = SQLiteManager.Instance.team[ConstData.Berserker].playerData.HP +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Berserker].playerData.Weapon].equipment_HP +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Berserker].playerData.Equipment].equipment_HP;
            SQLiteManager.Instance.team[ConstData.Berserker].totalAD = SQLiteManager.Instance.team[ConstData.Berserker].playerData.AD +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Berserker].playerData.Weapon].equipment_AD +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Berserker].playerData.Equipment].equipment_AD;
            SQLiteManager.Instance.team[ConstData.Berserker].totalAP = 0;
            SQLiteManager.Instance.team[ConstData.Berserker].totalDEF = SQLiteManager.Instance.team[ConstData.Berserker].playerData.DEF +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Berserker].playerData.Weapon].equipment_DEF +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Berserker].playerData.Equipment].equipment_DEF;
            SQLiteManager.Instance.team[ConstData.Berserker].totalRES = SQLiteManager.Instance.team[ConstData.Berserker].playerData.RES +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Berserker].playerData.Weapon].equipment_RES +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Berserker].playerData.Equipment].equipment_RES;
            //魔法师
            SQLiteManager.Instance.team[ConstData.Caster].totalHP = SQLiteManager.Instance.team[ConstData.Caster].playerData.HP +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Caster].playerData.Weapon].equipment_HP +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Caster].playerData.Equipment].equipment_HP;
            SQLiteManager.Instance.team[ConstData.Caster].totalAD = 0;
            SQLiteManager.Instance.team[ConstData.Caster].totalAP = SQLiteManager.Instance.team[ConstData.Caster].playerData.AP +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Caster].playerData.Weapon].equipment_AP +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Caster].playerData.Equipment].equipment_AP;
            SQLiteManager.Instance.team[ConstData.Caster].totalDEF = 0;
            SQLiteManager.Instance.team[ConstData.Caster].totalRES = SQLiteManager.Instance.team[ConstData.Caster].playerData.RES +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Caster].playerData.Weapon].equipment_RES +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Caster].playerData.Equipment].equipment_RES;
            //猎人
            SQLiteManager.Instance.team[ConstData.Hunter].totalHP = SQLiteManager.Instance.team[ConstData.Hunter].playerData.HP +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Hunter].playerData.Weapon].equipment_HP +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Hunter].playerData.Equipment].equipment_HP;
            SQLiteManager.Instance.team[ConstData.Hunter].totalAD = SQLiteManager.Instance.team[ConstData.Hunter].playerData.AD +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Hunter].playerData.Weapon].equipment_AD +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Hunter].playerData.Equipment].equipment_AD;
            SQLiteManager.Instance.team[ConstData.Hunter].totalAP = 0;
            SQLiteManager.Instance.team[ConstData.Hunter].totalDEF = SQLiteManager.Instance.team[ConstData.Hunter].playerData.DEF +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Hunter].playerData.Weapon].equipment_DEF +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Hunter].playerData.Equipment].equipment_DEF;
            SQLiteManager.Instance.team[ConstData.Hunter].totalRES = SQLiteManager.Instance.team[ConstData.Hunter].playerData.RES +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Hunter].playerData.Weapon].equipment_RES +
                SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.team[ConstData.Hunter].playerData.Equipment].equipment_RES;
        }
        #endregion

        //各种数据库变更
        //player表角色删除
        //player表装备变更
        //bag背包变更

        //切换回主城页面
        UIManager.Instance.PopUIStack();
    }
    //点击技能栏查看技能详情和关闭详情窗口
    void CloseWindow_Introduction(PointerEventData eventData)
    {
        introductionFrame.gameObject.SetActive(false);
        IntroductionText.text = "";
    }
    void SkillFrame_A(PointerEventData eventData)
    {
        if (selecteHero != null)
        {
            if (selecteHero.player_Class != ConstData.FlagMan)
            {
                introductionFrame.gameObject.SetActive(true);
                SkillData _skill01 = SQLiteManager.Instance.skillDataSource
                    [SQLiteManager.Instance.characterDataSource[int.Parse(selectPlayer.name)].character_SkillOneID];
                string[] tempText = new string[]
                    {
                "技能名：",
                _skill01.skill_Name,
                "\n技能类型：",
                _skill01.skill_Type,
                "\n技能效果：",
                _skill01.skill_Description
                    };
                IntroductionText.text = StringSplicingTool.StringSplicing(tempText);
            }
        }
    }
    void SkillFrame_B(PointerEventData eventData)
    {
        if(selecteHero != null)
        {
            if (selecteHero.player_Class != ConstData.FlagMan)
            {
                introductionFrame.gameObject.SetActive(true);
                SkillData _skill02 = SQLiteManager.Instance.skillDataSource
                    [SQLiteManager.Instance.characterDataSource[int.Parse(selectPlayer.name)].character_SkillTwoID];
                string[] tempText = new string[]
                    {
                "技能名：",
                _skill02.skill_Name,
                "\n技能类型：",
                _skill02.skill_Type,
                "\n技能效果：",
                _skill02.skill_Description
                    };
                IntroductionText.text = StringSplicingTool.StringSplicing(tempText);
            }
        }
    }
    void SkillFrame_C(PointerEventData eventData)
    {
        if (selecteHero != null)
        {
            if (selecteHero.player_Class != ConstData.FlagMan && selecteHero != null)
            {
                introductionFrame.gameObject.SetActive(true);
                SkillData _skill03 = SQLiteManager.Instance.skillDataSource
                    [SQLiteManager.Instance.characterDataSource[int.Parse(selectPlayer.name)].character_SkillThreeID];
                string[] tempText = new string[]
                    {
                "技能名：",
                _skill03.skill_Name,
                "\n技能类型：",
                _skill03.skill_Type,
                "\n技能效果：",
                _skill03.skill_Description
                    };
                IntroductionText.text = StringSplicingTool.StringSplicing(tempText);
            }
        }
    }
    //确认取消窗口
    void ConfirmOK(PointerEventData eventData)
    {
        Debug.Log("你删除了一个角色");
        confirmFrame.gameObject.SetActive(false);
        deleteNum = 0;
    }
    void CancelNO(PointerEventData eventData)
    {
        confirmFrame.gameObject.SetActive(false);
        deleteNum = 0;
    }
    //队伍增加和删减成员以及队伍编成确定
    void AddTeamMember(PointerEventData eventData)
    {
        if (selecteHero != null && selecteHero.player_Class != ConstData.FlagMan)
        {
            if (tempTeamDic[selecteHero.player_Class] == null)
            {
                //临时队伍添加
                tempTeamDic[selecteHero.player_Class] = selecteHero;
                //更新显示位置
                ShowTheTeamMumber(selecteHero.player_Class);
            }
        }
    }
    void DeleteTeamMember(PointerEventData eventData)
    {
        if (selecteHero != null && selecteHero.player_Class != ConstData.FlagMan)
        {
            if (tempTeamDic[selecteHero.player_Class] != null && 
                tempTeamDic[selecteHero.player_Class].player_Id == selecteHero.player_Id)
            {
                //临时队伍移除
                tempTeamDic[selecteHero.player_Class] = null;
                //条去色
                foreach (GameObject bar in playerList)
                {
                    if (bar.name == (selecteHero.player_Id).ToString())
                    {
                        bar.GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite("LineList");
                    }
                }
                //更新显示位置
                ShowTheTeamMumber(selecteHero.player_Class);
            }
        }
    }
    void TeamMemberConfirm(PointerEventData eventData)
    {
        if (tempTeamDic[ConstData.FlagMan] != null && tempTeamDic[ConstData.Saber] != null && tempTeamDic[ConstData.Knight] != null &&
            tempTeamDic[ConstData.Berserker] != null && tempTeamDic[ConstData.Caster] != null && tempTeamDic[ConstData.Hunter] != null)
        {
            HeroData flg = new HeroData();
            flg.playerData = tempTeamDic[ConstData.FlagMan];
            SQLiteManager.Instance.team[ConstData.FlagMan] = flg;
            HeroData sbr = new HeroData();
            sbr.playerData = tempTeamDic[ConstData.Saber];
            SQLiteManager.Instance.team[ConstData.Saber] = sbr;
            HeroData knt = new HeroData();
            knt.playerData = tempTeamDic[ConstData.Knight];
            SQLiteManager.Instance.team[ConstData.Knight] = knt;
            HeroData bsk = new HeroData();
            bsk.playerData = tempTeamDic[ConstData.Berserker];
            SQLiteManager.Instance.team[ConstData.Berserker] = bsk;
            HeroData cst = new HeroData();
            cst.playerData = tempTeamDic[ConstData.Caster];
            SQLiteManager.Instance.team[ConstData.Caster] = cst;
            HeroData hut = new HeroData();
            hut.playerData = tempTeamDic[ConstData.Hunter];
            SQLiteManager.Instance.team[ConstData.Hunter] = hut;
        }
    }
    //点击装备槽里的武器和防具查看详情
    void WeaponHoleMessage(PointerEventData eventData)
    {
        if (selecteHero != null)
        {
            if (selecteHero.player_Class != ConstData.FlagMan)
            {
                introductionFrame.gameObject.SetActive(true);
                EquipmentData eqdata = SQLiteManager.Instance.equipmentDataSource
                    [SQLiteManager.Instance.playerDataSource[selecteHero.player_Id].Weapon];
                string[] tempText = new string[]
                    {
                "\n武器名：",
                eqdata.equipmentNmae,
                "\n\n武器职业：",
                eqdata.equipmentClass,
                "\n\nHP: ",
                (eqdata.equipment_HP).ToString(),
                "   AD: ",
                (eqdata.equipment_AD).ToString(),
                "   AP: ",
                (eqdata.equipment_AP).ToString(),
                "   DEF: ",
                (eqdata.equipment_DEF).ToString(),
                "   RES: ",
                (eqdata.equipment_RES).ToString(),
                    };
                IntroductionText.text = StringSplicingTool.StringSplicing(tempText);
            }
        }
    }
    void EquipmentHoleMessage(PointerEventData eventData)
    {
        if (selecteHero != null)
        {
            if (selecteHero.player_Class != ConstData.FlagMan)
            {
                introductionFrame.gameObject.SetActive(true);
                EquipmentData eqdata = SQLiteManager.Instance.equipmentDataSource
                    [SQLiteManager.Instance.playerDataSource[selecteHero.player_Id].Equipment];
                string[] tempText = new string[]
                    {
                "\n防具名：",
                eqdata.equipmentNmae,
                "\n\n防具职业：",
                eqdata.equipmentClass,
                "\n\nHP: ",
                (eqdata.equipment_HP).ToString(),
                "   AD: ",
                (eqdata.equipment_AD).ToString(),
                "   AP: ",
                (eqdata.equipment_AP).ToString(),
                "   DEF: ",
                (eqdata.equipment_DEF).ToString(),
                "   RES: ",
                (eqdata.equipment_RES).ToString(),
                    };
                IntroductionText.text = StringSplicingTool.StringSplicing(tempText);
            }
        }
    }
    //装备选中窗口
    void WeaponSelectFrame(PointerEventData eventData)
    {
        if (selecteHero != null)
        {
            equipmentG = eventData.pointerEnter;
            equipmentID = eventData.pointerEnter.transform.GetComponent<BagItem>().mydata_equipt.equipment_Id;
            //Debug.Log(equipmentID);
            //Debug.Log(equipmentG.GetComponent<BagItem>().myGrid);
            confirmFrame_EQ.gameObject.SetActive(true);
            switch (eventData.pointerEnter.transform.GetComponent<BagItem>().mydata_equipt.equipmentType)
            {
                case ConstData.ListType_Weapon:
                    string[] textArray = new string[]
                {
                "当前武器：",
                SQLiteManager.Instance.equipmentDataSource[selecteHero.Weapon].equipmentNmae,
                "\nHP: ",
                (SQLiteManager.Instance.equipmentDataSource[selecteHero.Weapon].equipment_HP).ToString(),
                "  AD: ",
                (SQLiteManager.Instance.equipmentDataSource[selecteHero.Weapon].equipment_AD).ToString(),
                "  AP: ",
                (SQLiteManager.Instance.equipmentDataSource[selecteHero.Weapon].equipment_AP).ToString(),
                "  DEF: ",
                (SQLiteManager.Instance.equipmentDataSource[selecteHero.Weapon].equipment_DEF).ToString(),
                "  RES: ",
                (SQLiteManager.Instance.equipmentDataSource[selecteHero.Weapon].equipment_RES).ToString(),
                "\n<color=#eed925>↓ 替换到↓ </color>",
                "\n选中武器：",
                SQLiteManager.Instance.equipmentDataSource[equipmentID].equipmentNmae,
                "\nHP: ",
                (SQLiteManager.Instance.equipmentDataSource[equipmentID].equipment_HP).ToString(),
                "  AD: ",
                (SQLiteManager.Instance.equipmentDataSource[equipmentID].equipment_AD).ToString(),
                "  AP: ",
                (SQLiteManager.Instance.equipmentDataSource[equipmentID].equipment_AP).ToString(),
                "  DEF: ",
                (SQLiteManager.Instance.equipmentDataSource[equipmentID].equipment_DEF).ToString(),
                "  RES: ",
                (SQLiteManager.Instance.equipmentDataSource[equipmentID].equipment_RES).ToString(),
                };
                    selecteEquipmentMessage.text = StringSplicingTool.StringSplicing(textArray);
                    if (eventData.pointerEnter.transform.GetComponent<BagItem>().mydata_equipt.equipmentClass !=
                        SQLiteManager.Instance.equipmentDataSource[selecteHero.Weapon].equipmentClass)
                    {
                        transform.Find(ConstData.ConfirmFrame_EQ_ConfirmButton).gameObject.SetActive(false);
                        transform.Find(ConstData.ConfirmFrame_EQ_CancelButton).transform.GetComponent<RectTransform>().anchoredPosition =
                            new Vector2(0, -445);
                        selecteEquipmentMessage.text += "\n\n<color=#ff0000>*武器职业不符，不能替换！</color>";
                    }
                    break;
                case ConstData.ListType_Equipment:
                    string[] textArray2 = new string[]
                {
                "当前防具：",
                SQLiteManager.Instance.equipmentDataSource[selecteHero.Equipment].equipmentNmae,
                "\nHP: ",
                (SQLiteManager.Instance.equipmentDataSource[selecteHero.Equipment].equipment_HP).ToString(),
                "  AD: ",
                (SQLiteManager.Instance.equipmentDataSource[selecteHero.Equipment].equipment_AD).ToString(),
                "  AP: ",
                (SQLiteManager.Instance.equipmentDataSource[selecteHero.Equipment].equipment_AP).ToString(),
                "  DEF: ",
                (SQLiteManager.Instance.equipmentDataSource[selecteHero.Equipment].equipment_DEF).ToString(),
                "  RES: ",
                (SQLiteManager.Instance.equipmentDataSource[selecteHero.Equipment].equipment_RES).ToString(),
                "\n<color=#eed925>↓ 替换到↓ </color>",
                "\n选中防具：",
                SQLiteManager.Instance.equipmentDataSource[equipmentID].equipmentNmae,
                "\nHP: ",
                (SQLiteManager.Instance.equipmentDataSource[equipmentID].equipment_HP).ToString(),
                "  AD: ",
                (SQLiteManager.Instance.equipmentDataSource[equipmentID].equipment_AD).ToString(),
                "  AP: ",
                (SQLiteManager.Instance.equipmentDataSource[equipmentID].equipment_AP).ToString(),
                "  DEF: ",
                (SQLiteManager.Instance.equipmentDataSource[equipmentID].equipment_DEF).ToString(),
                "  RES: ",
                (SQLiteManager.Instance.equipmentDataSource[equipmentID].equipment_RES).ToString(),
                };
                    selecteEquipmentMessage.text = StringSplicingTool.StringSplicing(textArray2);
                    if (eventData.pointerEnter.transform.GetComponent<BagItem>().mydata_equipt.equipmentClass !=
                        SQLiteManager.Instance.equipmentDataSource[selecteHero.Equipment].equipmentClass)
                    {
                        transform.Find(ConstData.ConfirmFrame_EQ_ConfirmButton).gameObject.SetActive(false);
                        transform.Find(ConstData.ConfirmFrame_EQ_CancelButton).transform.GetComponent<RectTransform>().anchoredPosition =
                            new Vector2(0, -445);
                        selecteEquipmentMessage.text += "\n\n<color=#ff0000>*防具职业不符，不能替换！</color>";
                    }
                    break;
            }
        }
        else
        {
            equipmentG = eventData.pointerEnter;
            equipmentID = eventData.pointerEnter.transform.GetComponent<BagItem>().mydata_equipt.equipment_Id;
            confirmFrame_EQ.gameObject.SetActive(true);
            switch (eventData.pointerEnter.transform.GetComponent<BagItem>().mydata_equipt.equipmentType)
            {
                case ConstData.ListType_Weapon:
                    string[] textArray3 = new string[]
                {
                "选中武器：",
                SQLiteManager.Instance.equipmentDataSource[equipmentID].equipmentNmae,
                "\n装备职业：",
                SQLiteManager.Instance.equipmentDataSource[equipmentID].equipmentClass,
                "\nHP: ",
                (SQLiteManager.Instance.equipmentDataSource[equipmentID].equipment_HP).ToString(),
                "  AD: ",
                (SQLiteManager.Instance.equipmentDataSource[equipmentID].equipment_AD).ToString(),
                "  AP: ",
                (SQLiteManager.Instance.equipmentDataSource[equipmentID].equipment_AP).ToString(),
                "  DEF: ",
                (SQLiteManager.Instance.equipmentDataSource[equipmentID].equipment_DEF).ToString(),
                "  RES: ",
                (SQLiteManager.Instance.equipmentDataSource[equipmentID].equipment_RES).ToString(),
                };
                    selecteEquipmentMessage.text = StringSplicingTool.StringSplicing(textArray3);
                    transform.Find(ConstData.ConfirmFrame_EQ_ConfirmButton).gameObject.SetActive(false);
                    transform.Find(ConstData.ConfirmFrame_EQ_CancelButton).transform.GetComponent<RectTransform>().anchoredPosition =
                        new Vector2(0, -445);
                    break;
                case ConstData.ListType_Equipment:
                    string[] textArray4 = new string[]
                {
                "选中防具：",
                SQLiteManager.Instance.equipmentDataSource[equipmentID].equipmentNmae,
                "\n装备职业：",
                SQLiteManager.Instance.equipmentDataSource[equipmentID].equipmentClass,
                "\nHP: ",
                (SQLiteManager.Instance.equipmentDataSource[equipmentID].equipment_HP).ToString(),
                "  AD: ",
                (SQLiteManager.Instance.equipmentDataSource[equipmentID].equipment_AD).ToString(),
                "  AP: ",
                (SQLiteManager.Instance.equipmentDataSource[equipmentID].equipment_AP).ToString(),
                "  DEF: ",
                (SQLiteManager.Instance.equipmentDataSource[equipmentID].equipment_DEF).ToString(),
                "  RES: ",
                (SQLiteManager.Instance.equipmentDataSource[equipmentID].equipment_RES).ToString(),
                };
                    selecteEquipmentMessage.text = StringSplicingTool.StringSplicing(textArray4);
                    transform.Find(ConstData.ConfirmFrame_EQ_ConfirmButton).gameObject.SetActive(false);
                    transform.Find(ConstData.ConfirmFrame_EQ_CancelButton).transform.GetComponent<RectTransform>().anchoredPosition =
                        new Vector2(0, -445);
                    break;
            }
        }
    }
    void ConfirmOK_EQ(PointerEventData eventData)
    {
        switch (SQLiteManager.Instance.equipmentDataSource[equipmentID].equipmentType)
        {
            case ConstData.ListType_Weapon:
                //旧武器卸下
                int outWeapon = selecteHero.Weapon;
                //新武器换上
                selecteHero.Weapon = equipmentID;
                //新武器字典变更
                SQLiteManager.Instance.playerDataSource[selecteHero.player_Id].Weapon = equipmentID;
                if (SQLiteManager.Instance.team[selecteHero.player_Class] != null)
                {
                    if (SQLiteManager.Instance.team[selecteHero.player_Class].playerData.player_Id == selecteHero.player_Id)
                    {
                        SQLiteManager.Instance.team[selecteHero.player_Class].playerData.Weapon = equipmentID;
                    }
                }
                //背包字典变更
                SQLiteManager.Instance.bagDataSource[equipmentG.GetComponent<BagItem>().myGrid].Bag_Weapon = outWeapon;
                //装备格刷新
                ItemBarCreate(ConstData.All);
                foreach (GameObject filt in filterList)
                {
                    if (filt.GetComponent<Toggle>().isOn == true)
                    {
                        switch (filt.name)
                        {
                            case ConstData.StoneSaberTag:
                                ItemBarCreate(ConstData.Saber);
                                break;
                            case ConstData.StoneKnightTag:
                                ItemBarCreate(ConstData.Knight);
                                break;
                            case ConstData.StoneBerserkerTag:
                                ItemBarCreate(ConstData.Berserker);
                                break;
                            case ConstData.StoneCasterTag:
                                ItemBarCreate(ConstData.Caster);
                                break;
                            case ConstData.StoneHunterTag:
                                ItemBarCreate(ConstData.Hunter);
                                break;
                        }
                        return;
                    }
                }
                //显示武器刷新
                UpdataTheEquipment();
                break;
            case ConstData.ListType_Equipment:
                //旧防具卸下
                int outEquipt = selecteHero.Equipment;
                //新防具换上
                selecteHero.Equipment = equipmentID;
                //新防具字典变更
                SQLiteManager.Instance.playerDataSource[selecteHero.player_Id].Equipment = equipmentID;
                if (SQLiteManager.Instance.team[selecteHero.player_Class] != null)
                {
                    if (SQLiteManager.Instance.team[selecteHero.player_Class].playerData.player_Id == selecteHero.player_Id)
                    {
                        SQLiteManager.Instance.team[selecteHero.player_Class].playerData.Equipment = equipmentID;
                    }
                }
                //背包字典变更
                SQLiteManager.Instance.bagDataSource[equipmentG.GetComponent<BagItem>().myGrid].Bag_Equipment = outEquipt;
                //装备格刷新
                ItemBarCreate(ConstData.All);
                foreach (GameObject filt in filterList)
                {
                    if (filt.GetComponent<Toggle>().isOn == true)
                    {
                        switch (filt.name)
                        {
                            case ConstData.StoneSaberTag:
                                ItemBarCreate(ConstData.Saber);
                                break;
                            case ConstData.StoneKnightTag:
                                ItemBarCreate(ConstData.Knight);
                                break;
                            case ConstData.StoneBerserkerTag:
                                ItemBarCreate(ConstData.Berserker);
                                break;
                            case ConstData.StoneCasterTag:
                                ItemBarCreate(ConstData.Caster);
                                break;
                            case ConstData.StoneHunterTag:
                                ItemBarCreate(ConstData.Hunter);
                                break;
                        }
                        return;
                    }
                }
                //显示武器刷新
                UpdataTheEquipment();
                break;
        }
        //整个窗口隐藏
        confirmFrame_EQ.gameObject.SetActive(false);
    }
    void CancelNO_EQ(PointerEventData eventData)
    {
        //确认按钮隐藏取消
        transform.Find(ConstData.ConfirmFrame_EQ_ConfirmButton).gameObject.SetActive(true);
        //按钮归位
        transform.Find(ConstData.ConfirmFrame_EQ_CancelButton).transform.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(225, -445);
        //整个窗口隐藏
        confirmFrame_EQ.gameObject.SetActive(false);
    }
    //角色条选中
    void CharacterSelect(PointerEventData eventData)
    {
        selectBarNum = Convert.ToInt32(eventData.pointerEnter.transform.parent.name);
        selectPlayer = ResourcesManager.Instance.FindPlayerPrefab((SQLiteManager.Instance.playerDataSource[selectBarNum].PrefabsID).ToString());
        selecteHero = SQLiteManager.Instance.playerDataSource[selectBarNum];
        HeroDataDisplay();
        if (selecteHero.player_Class != ConstData.FlagMan)
        {
            skillImage_A.sprite = ResourcesManager.Instance.FindSprite((selecteHero.skillOneID).ToString());
            skillImage_B.sprite = ResourcesManager.Instance.FindSprite((selecteHero.skillTwoID).ToString());
            skillImage_C.sprite = ResourcesManager.Instance.FindSprite((selecteHero.skillThreeID).ToString());
        }
        else
        {
            skillImage_A.sprite = ResourcesManager.Instance.FindSprite(ConstData.SkillNull);
            skillImage_B.sprite = ResourcesManager.Instance.FindSprite(ConstData.SkillNull);
            skillImage_C.sprite = ResourcesManager.Instance.FindSprite(ConstData.SkillNull);
        }
        //////////游戏画面区域的处理//////////
        if (isTeam == false)
        {
            if (step01.transform.childCount == 1)
            {
                GameObject chara = ObjectPoolManager.Instance.InstantiateMyGameObject(selectPlayer);
                chara.transform.position = step01.transform.GetChild(0).transform.position;
                chara.transform.parent = step01.transform;
                chara.name = (selecteHero.player_Id).ToString();
                chara.GetComponent<Animator>().SetBool("isWait", true);
            }
            else
            {
                step01.transform.GetChild(1).name = (SQLiteManager.Instance.playerDataSource
                            [Convert.ToInt32(step01.transform.GetChild(1).name)].PrefabsID).ToString();
                step01.transform.GetChild(1).GetComponent<Animator>().SetBool("isWait", false);
                ObjectPoolManager.Instance.RecycleMyGameObject(step01.transform.GetChild(1).gameObject);
                GameObject chara = ObjectPoolManager.Instance.InstantiateMyGameObject(selectPlayer);
                chara.transform.position = step01.transform.GetChild(0).transform.position;
                chara.transform.parent = step01.transform;
                chara.name = (selecteHero.player_Id).ToString();
                chara.GetComponent<Animator>().SetBool("isWait", true);
            }
            
        }
        else
        {

        }
    }
    //角色条解雇
    void CharacterDismissal(PointerEventData eventData)
    {
        deleteNum = Convert.ToInt32(eventData.pointerEnter.transform.parent.name);
        confirmFrame.gameObject.SetActive(true);
        string[] textArray = new string[5];
        textArray[0] = "你确定要解雇“";
        textArray[1] = SQLiteManager.Instance.playerDataSource[deleteNum].player_Name;
        textArray[2] = "”吗？ \n";
        textArray[3] = "解雇能获得金币：";
        textArray[4] = (SQLiteManager.Instance.playerDataSource[deleteNum].GoldCoin * 0.1f).ToString();
        deleteCharacterMessage.text = StringSplicingTool.StringSplicing(textArray);
    }
    //筛选区选中
    void SaberFilter(PointerEventData eventData)
    {
        if (transform.Find(ConstData.Filter_StoneSaberTag).GetComponent<Toggle>().isOn == false)
        {
            CharacterBarCreate(ConstData.All);
            ItemBarCreate(ConstData.All);
        }
        else
        {
            CharacterBarCreate(ConstData.Saber);
            ItemBarCreate(ConstData.Saber);
        }
    }
    void KnightFilter(PointerEventData eventData)
    {
        if (transform.Find(ConstData.Filter_StoneKnightTag).GetComponent<Toggle>().isOn == false)
        {
            CharacterBarCreate(ConstData.All);
            ItemBarCreate(ConstData.All);
        }
        else
        {
            CharacterBarCreate(ConstData.Knight);
            ItemBarCreate(ConstData.Knight);
        }
    }
    void BerserkerFilter(PointerEventData eventData)
    {
        if (transform.Find(ConstData.Filter_StoneBerserkerTag).GetComponent<Toggle>().isOn == false)
        {
            CharacterBarCreate(ConstData.All);
            ItemBarCreate(ConstData.All);
        }
        else
        {
            CharacterBarCreate(ConstData.Berserker);
            ItemBarCreate(ConstData.Berserker);
        }
    }
    void CasterFilter(PointerEventData eventData)
    {
        if (transform.Find(ConstData.Filter_StoneCasterTag).GetComponent<Toggle>().isOn == false)
        {
            CharacterBarCreate(ConstData.All);
            ItemBarCreate(ConstData.All);
        }
        else
        {
            CharacterBarCreate(ConstData.Caster);
            ItemBarCreate(ConstData.Caster);
        }
    }
    void HunterFilter(PointerEventData eventData)
    {
        if (transform.Find(ConstData.Filter_StoneHunterTag).GetComponent<Toggle>().isOn == false)
        {
            CharacterBarCreate(ConstData.All);
            ItemBarCreate(ConstData.All);
        }
        else
        {
            CharacterBarCreate(ConstData.Hunter);
            ItemBarCreate(ConstData.Hunter);
        }
    }



    ////////////////////////////////////////////////////////////
    ////////////            非绑定方法           ///////////////
    ////////////////////////////////////////////////////////////

    /// <summary>
    /// 角色条内容区域自适应长度
    /// </summary>
    /// <param 筛选模式="FilterClass"></param>
    void CharacterBarContentAdaptive(string FilterClass)
    {
        int tempheight = 1000;
        transform.Find(ConstData.ControllerArea_ListContent).GetComponent<RectTransform>().sizeDelta = new Vector2(0, tempheight);
        switch (FilterClass)
        {
            case ConstData.All:
                if (SQLiteManager.Instance.playerDataSource.Count > 5)
                {
                    tempheight = 1000 + (SQLiteManager.Instance.playerDataSource.Count - 5) * 130;
                    transform.Find(ConstData.ControllerArea_ListContent).GetComponent<RectTransform>().sizeDelta = new Vector2(0, tempheight);
                }
                break;
            case ConstData.Saber:
                int saberCount = 0;
                for (int i = 0; i < SQLiteManager.Instance.playerDataSource.Count; i++)
                {
                    if (SQLiteManager.Instance.playerDataSource[(1300 + i)].player_Class == ConstData.Saber)
                    {
                        saberCount++;
                    }
                }
                if (saberCount > 5)
                {
                    tempheight = 1150 + (saberCount - 5) * 130;
                    transform.Find(ConstData.ControllerArea_ListContent).GetComponent<RectTransform>().sizeDelta = new Vector2(0, tempheight);
                    saberCount = 0;
                }
                break;
            case ConstData.Knight:
                int knightCount = 0;
                for (int i = 0; i < SQLiteManager.Instance.playerDataSource.Count; i++)
                {
                    if (SQLiteManager.Instance.playerDataSource[(1300 + i)].player_Class == ConstData.Knight)
                    {
                        knightCount++;
                    }
                }
                if (knightCount > 5)
                {
                    tempheight = 1150 + (knightCount - 5) * 130;
                    transform.Find(ConstData.ControllerArea_ListContent).GetComponent<RectTransform>().sizeDelta = new Vector2(0, tempheight);
                    knightCount = 0;
                }
                break;
            case ConstData.Berserker:
                int berserkerCount = 0;
                for (int i = 0; i < SQLiteManager.Instance.playerDataSource.Count; i++)
                {
                    if (SQLiteManager.Instance.playerDataSource[(1300 + i)].player_Class == ConstData.Berserker)
                    {
                        berserkerCount++;
                    }
                }
                if (berserkerCount > 5)
                {
                    tempheight = 1150 + (berserkerCount - 5) * 130;
                    transform.Find(ConstData.ControllerArea_ListContent).GetComponent<RectTransform>().sizeDelta = new Vector2(0, tempheight);
                    berserkerCount = 0;
                }
                break;
            case ConstData.Caster:
                int casterCount = 0;
                for (int i = 0; i < SQLiteManager.Instance.playerDataSource.Count; i++)
                {
                    if (SQLiteManager.Instance.playerDataSource[(1300 + i)].player_Class == ConstData.Caster)
                    {
                        casterCount++;
                    }
                }
                if (casterCount > 5)
                {
                    tempheight = 1150 + (casterCount - 5) * 130;
                    transform.Find(ConstData.ControllerArea_ListContent).GetComponent<RectTransform>().sizeDelta = new Vector2(0, tempheight);
                    casterCount = 0;
                }
                break;
            case ConstData.Hunter:
                int hunterCount = 0;
                for (int i = 0; i < SQLiteManager.Instance.playerDataSource.Count; i++)
                {
                    if (SQLiteManager.Instance.playerDataSource[(1300 + i)].player_Class == ConstData.Hunter)
                    {
                        hunterCount++;
                    }
                }
                if (hunterCount > 5)
                {
                    tempheight = 1150 + (hunterCount - 5) * 130;
                    transform.Find(ConstData.ControllerArea_ListContent).GetComponent<RectTransform>().sizeDelta = new Vector2(0, tempheight);
                    hunterCount = 0;
                }
                break;
        }
    }
    /// <summary>
    /// 生成角色条
    /// </summary>
    /// <param 筛选模式="FilterClass"></param>
    void CharacterBarCreate(string FilterClass)
    {
        //清空所有角色条和列表
        CharacterBarClear();

        switch (FilterClass)
        {
            case ConstData.All:
                #region 全部生成
                //区域自适应
                CharacterBarContentAdaptive(ConstData.All);
                //生成其他角色条
                for (int i = 0; i < SQLiteManager.Instance.playerDataSource.Count; i++)
                {
                    //生成条
                    GameObject _tempHeroBar = ObjectPoolManager.Instance.InstantiateMyGameObject(characterBar);
                    //设置条父物体
                    _tempHeroBar.transform.parent = transform.Find(ConstData.ControllerArea_ListContent).transform;
                    //往下推条位置
                    _tempHeroBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,
                            ((_tempHeroBar.transform.parent.GetComponent<RectTransform>().sizeDelta.y * 0.5f) - 62) - (i * 150));
                    _tempHeroBar.transform.localScale = new Vector3(1, 1, 1);
                    //更换职业LOGO
                    _tempHeroBar.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite =
                        ResourcesManager.Instance.FindSprite(SQLiteManager.Instance.characterDataSource
                        [SQLiteManager.Instance.playerDataSource[(1300 + i)].
                        PrefabsID].character_Class);
                    //显示名字和LV信息
                    string[] _textBar = new string[3];
                    _textBar[0] = SQLiteManager.Instance.playerDataSource[(1300 + i)].player_Name;
                    _textBar[1] = " LV.";
                    _textBar[2] = (SQLiteManager.Instance.playerDataSource[(1300 + i)].Level).ToString();
                    _tempHeroBar.transform.GetChild(2).GetComponent<Text>().text = StringSplicingTool.StringSplicing(_textBar);
                    _tempHeroBar.name = (1300 + i).ToString();
                    if (_tempHeroBar.transform.GetChild(3).GetComponent<UISceneWidget>() == null)
                    {
                        //绑定选择的点击方法
                        GameObject select = _tempHeroBar.transform.GetChild(3).gameObject; ;
                        UISceneWidget selectClick = UISceneWidget.Get(select);
                        selectClick.PointerClick += CharacterSelect;
                        //绑定解雇按钮的点击方法
                        GameObject dismissal = _tempHeroBar.transform.GetChild(4).gameObject;
                        UISceneWidget dismissalClick = UISceneWidget.Get(dismissal);
                        dismissalClick.PointerClick += CharacterDismissal;
                    }
                    //添加Toggle组件以及设置群组
                    if (_tempHeroBar.GetComponent<Toggle>() == null)
                    {
                        _tempHeroBar.AddComponent<Toggle>();
                    }
                    _tempHeroBar.GetComponent<Toggle>().group = _tempHeroBar.transform.parent.GetComponent<ToggleGroup>();
                    _tempHeroBar.GetComponent<Toggle>().graphic = _tempHeroBar.transform.GetChild(0).GetComponent<Image>();
                    //加入列表
                    playerList.Add(_tempHeroBar);
                    //条的颜色是否需要改变判断
                    if (isTeam == true)
                    {
                        for (int j = 0; j < stepList.Count; j++)
                        {
                            if (stepList[j].transform.childCount != 1)
                            {
                                if (_tempHeroBar.name == stepList[j].transform.GetChild(1).name)
                                {
                                    _tempHeroBar.GetComponent<Image>().sprite = _tempHeroBar.transform.GetChild(0).GetComponent<Image>().sprite;
                                }
                            }
                        }
                        if (_tempHeroBar.name == "1300")
                        {
                            _tempHeroBar.GetComponent<Image>().sprite = _tempHeroBar.transform.GetChild(0).GetComponent<Image>().sprite;
                        }
                    }
                }
                #endregion
                break;
            case ConstData.Saber:
                #region 只生成saber职业
                //区域自适应
                CharacterBarContentAdaptive(ConstData.Saber);
                //生成其他角色条
                int filterCount = 0;
                for (int i = 0; i < SQLiteManager.Instance.playerDataSource.Count; i++)
                {
                    if (SQLiteManager.Instance.playerDataSource[(1300 + i)].player_Class == ConstData.Saber ||
                        SQLiteManager.Instance.playerDataSource[(1300 + i)].player_Class == ConstData.FlagMan)
                    {
                        filterCount++;
                        //生成条
                        GameObject _tempHeroBar = ObjectPoolManager.Instance.InstantiateMyGameObject(characterBar);
                        //设置条父物体
                        _tempHeroBar.transform.parent = transform.Find(ConstData.ControllerArea_ListContent).transform;
                        //往下推条位置
                        _tempHeroBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 
                            ((_tempHeroBar.transform.parent.GetComponent<RectTransform>().sizeDelta.y * 0.5f) - 62 ) - ((filterCount - 1) * 150));
                        _tempHeroBar.transform.localScale = new Vector3(1, 1, 1);
                        //更换职业LOGO
                        _tempHeroBar.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite =
                            ResourcesManager.Instance.FindSprite(SQLiteManager.Instance.characterDataSource
                            [SQLiteManager.Instance.playerDataSource[(1300 + i)].
                            PrefabsID].character_Class);
                        //显示名字和LV信息
                        string[] _textBar = new string[3];
                        _textBar[0] = SQLiteManager.Instance.playerDataSource[(1300 + i)].player_Name;
                        _textBar[1] = " LV.";
                        _textBar[2] = (SQLiteManager.Instance.playerDataSource[(1300 + i)].Level).ToString();
                        _tempHeroBar.transform.GetChild(2).GetComponent<Text>().text = StringSplicingTool.StringSplicing(_textBar);
                        _tempHeroBar.name = (1300 + i).ToString();
                        if (_tempHeroBar.transform.GetChild(3).GetComponent<UISceneWidget>() == null)
                        {
                            //绑定选择的点击方法
                            GameObject select = _tempHeroBar.transform.GetChild(3).gameObject; ;
                            UISceneWidget selectClick = UISceneWidget.Get(select);
                            selectClick.PointerClick += CharacterSelect;
                            //绑定解雇按钮的点击方法
                            GameObject dismissal = _tempHeroBar.transform.GetChild(4).gameObject;
                            UISceneWidget dismissalClick = UISceneWidget.Get(dismissal);
                            dismissalClick.PointerClick += CharacterDismissal;
                        }
                        //添加Toggle组件以及设置群组
                        if (_tempHeroBar.GetComponent<Toggle>() == null)
                        {
                            _tempHeroBar.AddComponent<Toggle>();
                        }
                        _tempHeroBar.GetComponent<Toggle>().group = _tempHeroBar.transform.parent.GetComponent<ToggleGroup>();
                        _tempHeroBar.GetComponent<Toggle>().graphic = _tempHeroBar.transform.GetChild(0).GetComponent<Image>();
                        //加入列表
                        playerList.Add(_tempHeroBar);
                        //条的颜色是否需要改变判断
                        if (isTeam == true)
                        {
                            for (int j = 0; j < stepList.Count; j++)
                            {
                                if (stepList[j].transform.childCount != 1)
                                {
                                    if (_tempHeroBar.name == stepList[j].transform.GetChild(1).name)
                                    {
                                        _tempHeroBar.GetComponent<Image>().sprite = _tempHeroBar.transform.GetChild(0).GetComponent<Image>().sprite;
                                    }
                                }
                            }
                            if (_tempHeroBar.name == "1300")
                            {
                                _tempHeroBar.GetComponent<Image>().sprite = _tempHeroBar.transform.GetChild(0).GetComponent<Image>().sprite;
                            }
                        }
                    }
                }
                filterCount = 0;
                #endregion
                break;
            case ConstData.Knight:
                #region 只生成knight职业
                //区域自适应
                CharacterBarContentAdaptive(ConstData.Knight);
                //生成其他角色条
                int filterCount2 = 0;
                for (int i = 0; i < SQLiteManager.Instance.playerDataSource.Count; i++)
                {
                    if (SQLiteManager.Instance.playerDataSource[(1300 + i)].player_Class == ConstData.Knight ||
                        SQLiteManager.Instance.playerDataSource[(1300 + i)].player_Class == ConstData.FlagMan)
                    {
                        filterCount2++;
                        //生成条
                        GameObject _tempHeroBar = ObjectPoolManager.Instance.InstantiateMyGameObject(characterBar);
                        //设置条父物体
                        _tempHeroBar.transform.parent = transform.Find(ConstData.ControllerArea_ListContent).transform;
                        //往下推条位置
                        _tempHeroBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,
                            ((_tempHeroBar.transform.parent.GetComponent<RectTransform>().sizeDelta.y * 0.5f) - 62) - ((filterCount2 - 1) * 150));
                        _tempHeroBar.transform.localScale = new Vector3(1, 1, 1);
                        //更换职业LOGO
                        _tempHeroBar.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite =
                            ResourcesManager.Instance.FindSprite(SQLiteManager.Instance.characterDataSource
                            [SQLiteManager.Instance.playerDataSource[(1300 + i)].
                            PrefabsID].character_Class);
                        //显示名字和LV信息
                        string[] _textBar = new string[3];
                        _textBar[0] = SQLiteManager.Instance.playerDataSource[(1300 + i)].player_Name;
                        _textBar[1] = " LV.";
                        _textBar[2] = (SQLiteManager.Instance.playerDataSource[(1300 + i)].Level).ToString();
                        _tempHeroBar.transform.GetChild(2).GetComponent<Text>().text = StringSplicingTool.StringSplicing(_textBar);
                        _tempHeroBar.name = (1300 + i).ToString();
                        if (_tempHeroBar.transform.GetChild(3).GetComponent<UISceneWidget>() == null)
                        {
                            //绑定选择的点击方法
                            GameObject select = _tempHeroBar.transform.GetChild(3).gameObject; ;
                            UISceneWidget selectClick = UISceneWidget.Get(select);
                            selectClick.PointerClick += CharacterSelect;
                            //绑定解雇按钮的点击方法
                            GameObject dismissal = _tempHeroBar.transform.GetChild(4).gameObject;
                            UISceneWidget dismissalClick = UISceneWidget.Get(dismissal);
                            dismissalClick.PointerClick += CharacterDismissal;
                        }
                        //添加Toggle组件以及设置群组
                        if (_tempHeroBar.GetComponent<Toggle>() == null)
                        {
                            _tempHeroBar.AddComponent<Toggle>();
                        }
                        _tempHeroBar.GetComponent<Toggle>().group = _tempHeroBar.transform.parent.GetComponent<ToggleGroup>();
                        _tempHeroBar.GetComponent<Toggle>().graphic = _tempHeroBar.transform.GetChild(0).GetComponent<Image>();
                        //加入列表
                        playerList.Add(_tempHeroBar);
                        //条的颜色是否需要改变判断
                        if (isTeam == true)
                        {
                            for (int j = 0; j < stepList.Count; j++)
                            {
                                if (stepList[j].transform.childCount != 1)
                                {
                                    if (_tempHeroBar.name == stepList[j].transform.GetChild(1).name)
                                    {
                                        _tempHeroBar.GetComponent<Image>().sprite = _tempHeroBar.transform.GetChild(0).GetComponent<Image>().sprite;
                                    }
                                }
                            }
                            if (_tempHeroBar.name == "1300")
                            {
                                _tempHeroBar.GetComponent<Image>().sprite = _tempHeroBar.transform.GetChild(0).GetComponent<Image>().sprite;
                            }
                        }
                    }
                }
                filterCount2 = 0;
                #endregion
                break;
            case ConstData.Berserker:
                #region 只生成berserker职业
                //区域自适应
                CharacterBarContentAdaptive(ConstData.Berserker);
                //生成其他角色条
                int filterCount3 = 0;
                for (int i = 0; i < SQLiteManager.Instance.playerDataSource.Count; i++)
                {
                    if (SQLiteManager.Instance.playerDataSource[(1300 + i)].player_Class == ConstData.Berserker ||
                        SQLiteManager.Instance.playerDataSource[(1300 + i)].player_Class == ConstData.FlagMan)
                    {
                        filterCount3++;
                        //生成条
                        GameObject _tempHeroBar = ObjectPoolManager.Instance.InstantiateMyGameObject(characterBar);
                        //设置条父物体
                        _tempHeroBar.transform.parent = transform.Find(ConstData.ControllerArea_ListContent).transform;
                        //往下推条位置
                        _tempHeroBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,
                            ((_tempHeroBar.transform.parent.GetComponent<RectTransform>().sizeDelta.y * 0.5f) - 62) - ((filterCount3 - 1) * 150));
                        _tempHeroBar.transform.localScale = new Vector3(1, 1, 1);
                        //更换职业LOGO
                        _tempHeroBar.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite =
                            ResourcesManager.Instance.FindSprite(SQLiteManager.Instance.characterDataSource
                            [SQLiteManager.Instance.playerDataSource[(1300 + i)].
                            PrefabsID].character_Class);
                        //显示名字和LV信息
                        string[] _textBar = new string[3];
                        _textBar[0] = SQLiteManager.Instance.playerDataSource[(1300 + i)].player_Name;
                        _textBar[1] = " LV.";
                        _textBar[2] = (SQLiteManager.Instance.playerDataSource[(1300 + i)].Level).ToString();
                        _tempHeroBar.transform.GetChild(2).GetComponent<Text>().text = StringSplicingTool.StringSplicing(_textBar);
                        _tempHeroBar.name = (1300 + i).ToString();
                        if (_tempHeroBar.transform.GetChild(3).GetComponent<UISceneWidget>() == null)
                        {
                            //绑定选择的点击方法
                            GameObject select = _tempHeroBar.transform.GetChild(3).gameObject; ;
                            UISceneWidget selectClick = UISceneWidget.Get(select);
                            selectClick.PointerClick += CharacterSelect;
                            //绑定解雇按钮的点击方法
                            GameObject dismissal = _tempHeroBar.transform.GetChild(4).gameObject;
                            UISceneWidget dismissalClick = UISceneWidget.Get(dismissal);
                            dismissalClick.PointerClick += CharacterDismissal;
                        }
                        //添加Toggle组件以及设置群组
                        if (_tempHeroBar.GetComponent<Toggle>() == null)
                        {
                            _tempHeroBar.AddComponent<Toggle>();
                        }
                        _tempHeroBar.GetComponent<Toggle>().group = _tempHeroBar.transform.parent.GetComponent<ToggleGroup>();
                        _tempHeroBar.GetComponent<Toggle>().graphic = _tempHeroBar.transform.GetChild(0).GetComponent<Image>();
                        //加入列表
                        playerList.Add(_tempHeroBar);
                        //条的颜色是否需要改变判断
                        if (isTeam == true)
                        {
                            for (int j = 0; j < stepList.Count; j++)
                            {
                                if (stepList[j].transform.childCount != 1)
                                {
                                    if (_tempHeroBar.name == stepList[j].transform.GetChild(1).name)
                                    {
                                        _tempHeroBar.GetComponent<Image>().sprite = _tempHeroBar.transform.GetChild(0).GetComponent<Image>().sprite;
                                    }
                                }
                            }
                            if (_tempHeroBar.name == "1300")
                            {
                                _tempHeroBar.GetComponent<Image>().sprite = _tempHeroBar.transform.GetChild(0).GetComponent<Image>().sprite;
                            }
                        }
                    }
                }
                filterCount3 = 0;
                #endregion
                break;
            case ConstData.Caster:
                #region 只生成caster职业
                //区域自适应
                CharacterBarContentAdaptive(ConstData.Caster);
                //生成其他角色条
                int filterCount4 = 0;
                for (int i = 0; i < SQLiteManager.Instance.playerDataSource.Count; i++)
                {
                    if (SQLiteManager.Instance.playerDataSource[(1300 + i)].player_Class == ConstData.Caster ||
                        SQLiteManager.Instance.playerDataSource[(1300 + i)].player_Class == ConstData.FlagMan)
                    {
                        filterCount4++;
                        //生成条
                        GameObject _tempHeroBar = ObjectPoolManager.Instance.InstantiateMyGameObject(characterBar);
                        //设置条父物体
                        _tempHeroBar.transform.parent = transform.Find(ConstData.ControllerArea_ListContent).transform;
                        //往下推条位置
                        _tempHeroBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,
                            ((_tempHeroBar.transform.parent.GetComponent<RectTransform>().sizeDelta.y * 0.5f) - 62) - ((filterCount4 - 1) * 150));
                        _tempHeroBar.transform.localScale = new Vector3(1, 1, 1);
                        //更换职业LOGO
                        _tempHeroBar.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite =
                            ResourcesManager.Instance.FindSprite(SQLiteManager.Instance.characterDataSource
                            [SQLiteManager.Instance.playerDataSource[(1300 + i)].
                            PrefabsID].character_Class);
                        //显示名字和LV信息
                        string[] _textBar = new string[3];
                        _textBar[0] = SQLiteManager.Instance.playerDataSource[(1300 + i)].player_Name;
                        _textBar[1] = " LV.";
                        _textBar[2] = (SQLiteManager.Instance.playerDataSource[(1300 + i)].Level).ToString();
                        _tempHeroBar.transform.GetChild(2).GetComponent<Text>().text = StringSplicingTool.StringSplicing(_textBar);
                        _tempHeroBar.name = (1300 + i).ToString();
                        if (_tempHeroBar.transform.GetChild(3).GetComponent<UISceneWidget>() == null)
                        {
                            //绑定选择的点击方法
                            GameObject select = _tempHeroBar.transform.GetChild(3).gameObject; ;
                            UISceneWidget selectClick = UISceneWidget.Get(select);
                            selectClick.PointerClick += CharacterSelect;
                            //绑定解雇按钮的点击方法
                            GameObject dismissal = _tempHeroBar.transform.GetChild(4).gameObject;
                            UISceneWidget dismissalClick = UISceneWidget.Get(dismissal);
                            dismissalClick.PointerClick += CharacterDismissal;
                        }
                        //添加Toggle组件以及设置群组
                        if (_tempHeroBar.GetComponent<Toggle>() == null)
                        {
                            _tempHeroBar.AddComponent<Toggle>();
                        }
                        _tempHeroBar.GetComponent<Toggle>().group = _tempHeroBar.transform.parent.GetComponent<ToggleGroup>();
                        _tempHeroBar.GetComponent<Toggle>().graphic = _tempHeroBar.transform.GetChild(0).GetComponent<Image>();
                        //加入列表
                        playerList.Add(_tempHeroBar);
                        //条的颜色是否需要改变判断
                        if (isTeam == true)
                        {
                            for (int j = 0; j < stepList.Count; j++)
                            {
                                if (stepList[j].transform.childCount != 1)
                                {
                                    if (_tempHeroBar.name == stepList[j].transform.GetChild(1).name)
                                    {
                                        _tempHeroBar.GetComponent<Image>().sprite = _tempHeroBar.transform.GetChild(0).GetComponent<Image>().sprite;
                                    }
                                }
                            }
                            if (_tempHeroBar.name == "1300")
                            {
                                _tempHeroBar.GetComponent<Image>().sprite = _tempHeroBar.transform.GetChild(0).GetComponent<Image>().sprite;
                            }
                        }
                    }
                }
                filterCount4 = 0;
                #endregion
                break;
            case ConstData.Hunter:
                #region 只生成hunter职业
                //区域自适应
                CharacterBarContentAdaptive(ConstData.Hunter);
                //生成其他角色条
                int filterCount5 = 0;
                for (int i = 0; i < SQLiteManager.Instance.playerDataSource.Count; i++)
                {
                    if (SQLiteManager.Instance.playerDataSource[(1300 + i)].player_Class == ConstData.Hunter ||
                        SQLiteManager.Instance.playerDataSource[(1300 + i)].player_Class == ConstData.FlagMan)
                    {
                        filterCount5++;
                        //生成条
                        GameObject _tempHeroBar = ObjectPoolManager.Instance.InstantiateMyGameObject(characterBar);
                        //设置条父物体
                        _tempHeroBar.transform.parent = transform.Find(ConstData.ControllerArea_ListContent).transform;
                        //往下推条位置
                        _tempHeroBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,
                            ((_tempHeroBar.transform.parent.GetComponent<RectTransform>().sizeDelta.y * 0.5f) - 62) - ((filterCount5 - 1) * 150));
                        _tempHeroBar.transform.localScale = new Vector3(1, 1, 1);
                        //更换职业LOGO
                        _tempHeroBar.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite =
                            ResourcesManager.Instance.FindSprite(SQLiteManager.Instance.characterDataSource
                            [SQLiteManager.Instance.playerDataSource[(1300 + i)].
                            PrefabsID].character_Class);
                        //显示名字和LV信息
                        string[] _textBar = new string[3];
                        _textBar[0] = SQLiteManager.Instance.playerDataSource[(1300 + i)].player_Name;
                        _textBar[1] = " LV.";
                        _textBar[2] = (SQLiteManager.Instance.playerDataSource[(1300 + i)].Level).ToString();
                        _tempHeroBar.transform.GetChild(2).GetComponent<Text>().text = StringSplicingTool.StringSplicing(_textBar);
                        _tempHeroBar.name = (1300 + i).ToString();
                        if (_tempHeroBar.transform.GetChild(3).GetComponent<UISceneWidget>() == null)
                        {
                            //绑定选择的点击方法
                            GameObject select = _tempHeroBar.transform.GetChild(3).gameObject; ;
                            UISceneWidget selectClick = UISceneWidget.Get(select);
                            selectClick.PointerClick += CharacterSelect;
                            //绑定解雇按钮的点击方法
                            GameObject dismissal = _tempHeroBar.transform.GetChild(4).gameObject;
                            UISceneWidget dismissalClick = UISceneWidget.Get(dismissal);
                            dismissalClick.PointerClick += CharacterDismissal;
                        }
                        //添加Toggle组件以及设置群组
                        if (_tempHeroBar.GetComponent<Toggle>() == null)
                        {
                            _tempHeroBar.AddComponent<Toggle>();
                        }
                        _tempHeroBar.GetComponent<Toggle>().group = _tempHeroBar.transform.parent.GetComponent<ToggleGroup>();
                        _tempHeroBar.GetComponent<Toggle>().graphic = _tempHeroBar.transform.GetChild(0).GetComponent<Image>();
                        //加入列表
                        playerList.Add(_tempHeroBar);
                        //条的颜色是否需要改变判断
                        if (isTeam == true)
                        {
                            for (int j = 0; j < stepList.Count; j++)
                            {
                                if (stepList[j].transform.childCount != 1)
                                {
                                    if (_tempHeroBar.name == stepList[j].transform.GetChild(1).name)
                                    {
                                        _tempHeroBar.GetComponent<Image>().sprite = _tempHeroBar.transform.GetChild(0).GetComponent<Image>().sprite;
                                    }
                                }
                            }
                            if (_tempHeroBar.name == "1300")
                            {
                                _tempHeroBar.GetComponent<Image>().sprite = _tempHeroBar.transform.GetChild(0).GetComponent<Image>().sprite;
                            }
                        }
                    }
                }
                filterCount5 = 0;
                #endregion
                break;
        }
    }
    /// <summary>
    /// 清空所有角色条和列表
    /// </summary>
    void CharacterBarClear()
    {
        //清空现有角色条
        for (int i = 0; i < playerList.Count; i++)
        {
            playerList[i].name = "CharacterBar";
            playerList[i].transform.GetChild(4).gameObject.SetActive(true);
            playerList[i].transform.GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite("LineList");
            ObjectPoolManager.Instance.RecycleMyGameObject(playerList[i]);
        }
        //清空列表
        playerList.Clear();
    }


    /// <summary>
    /// 根据清除模式 清空画面区域角色对象（Single单人，Team全体）
    /// </summary>
    /// <param 清除模式="mode"></param>
    void GameAreaClear(string mode, string heroClass)
    {
        switch (mode)
        {
            case "Single":
                switch (heroClass)
                {
                    case "Default":
                        if (step01.transform.childCount != 1)
                        {
                            step01.transform.GetChild(1).GetComponent<Animator>().SetBool("isWait", false);
                            ObjectPoolManager.Instance.RecycleMyGameObject(step01.transform.GetChild(1).gameObject);
                        }
                        break;
                    case ConstData.Saber:
                        if (step01.transform.childCount != 1)
                        {
                            step01.transform.GetChild(1).name = (SQLiteManager.Instance.playerDataSource
                                    [Convert.ToInt32(step01.transform.GetChild(1).name)].PrefabsID).ToString();
                            step01.transform.GetChild(1).GetComponent<Animator>().SetBool("isWait", false);
                            ObjectPoolManager.Instance.RecycleMyGameObject(step01.transform.GetChild(1).gameObject);
                        }
                        break;
                    case ConstData.Knight:
                        if (step02.transform.childCount != 1)
                        {
                            step02.transform.GetChild(1).name = (SQLiteManager.Instance.playerDataSource
                                    [Convert.ToInt32(step02.transform.GetChild(1).name)].PrefabsID).ToString();
                            step02.transform.GetChild(1).GetComponent<Animator>().SetBool("isWait", false);
                            ObjectPoolManager.Instance.RecycleMyGameObject(step02.transform.GetChild(1).gameObject);
                        }
                        break;
                    case ConstData.Berserker:
                        if (step03.transform.childCount != 1)
                        {
                            step03.transform.GetChild(1).name = (SQLiteManager.Instance.playerDataSource
                                    [Convert.ToInt32(step03.transform.GetChild(1).name)].PrefabsID).ToString();
                            step03.transform.GetChild(1).GetComponent<Animator>().SetBool("isWait", false);
                            ObjectPoolManager.Instance.RecycleMyGameObject(step03.transform.GetChild(1).gameObject);
                        }
                        break;
                    case ConstData.Caster:
                        if (step04.transform.childCount != 1)
                        {
                            step04.transform.GetChild(1).name = (SQLiteManager.Instance.playerDataSource
                                    [Convert.ToInt32(step04.transform.GetChild(1).name)].PrefabsID).ToString();
                            step04.transform.GetChild(1).GetComponent<Animator>().SetBool("isWait", false);
                            ObjectPoolManager.Instance.RecycleMyGameObject(step04.transform.GetChild(1).gameObject);
                        }
                        break;
                    case ConstData.Hunter:
                        if (step05.transform.childCount != 1)
                        {
                            step05.transform.GetChild(1).name = (SQLiteManager.Instance.playerDataSource
                                    [Convert.ToInt32(step05.transform.GetChild(1).name)].PrefabsID).ToString();
                            step05.transform.GetChild(1).GetComponent<Animator>().SetBool("isWait", false);
                            ObjectPoolManager.Instance.RecycleMyGameObject(step05.transform.GetChild(1).gameObject);
                        }
                        break;
                }
                break;
            case "Team":
                for (int i = 0; i < stepList.Count; i++)
                {
                    if (stepList[i].transform.childCount != 1)
                    {
                        stepList[i].transform.GetChild(1).name = (SQLiteManager.Instance.playerDataSource
                            [Convert.ToInt32(stepList[i].transform.GetChild(1).name)].PrefabsID).ToString();
                        stepList[i].transform.GetChild(1).GetComponent<Animator>().SetBool("isWait", false);
                        ObjectPoolManager.Instance.RecycleMyGameObject(stepList[i].transform.GetChild(1).gameObject);
                    }
                }
                break;
        }
    }
    /// <summary>
    /// 展示team队伍角色
    /// </summary>
    void ShowTheTeamMumber(string mode)
    {
        //画面区域角色清空
        switch (mode)
        {
            case ConstData.All:
                GameAreaClear("Team", "");
                break;
            case ConstData.Saber:
                GameAreaClear("Single", ConstData.Saber);
                break;
            case ConstData.Knight:
                GameAreaClear("Single", ConstData.Knight);
                break;
            case ConstData.Berserker:
                GameAreaClear("Single", ConstData.Berserker);
                break;
            case ConstData.Caster:
                GameAreaClear("Single", ConstData.Caster);
                break;
            case ConstData.Hunter:
                GameAreaClear("Single", ConstData.Hunter);
                break;
        }
        //画面区域角色生成
        if (tempTeamDic[ConstData.FlagMan] != null)
        {
            //条变色
            foreach (GameObject bar in playerList)
            {
                if (bar.name == "1300")
                {
                    bar.GetComponent<Image>().sprite = bar.transform.GetChild(0).GetComponent<Image>().sprite;
                }
            }
        }
        switch (mode)
        {
            case ConstData.All:
                if (tempTeamDic[ConstData.Saber] != null)
                {
                    GameObject sbr = ObjectPoolManager.Instance.InstantiateMyGameObject
                    (
                    ResourcesManager.Instance.FindPlayerPrefab
                    ((tempTeamDic[ConstData.Saber].PrefabsID).ToString())
                    );
                    sbr.transform.position = step01.transform.GetChild(0).transform.position;
                    sbr.transform.parent = step01.transform;
                    sbr.name = (tempTeamDic[ConstData.Saber].player_Id).ToString();
                    sbr.transform.GetComponent<Animator>().SetBool("isWait", true);
                    //条变色
                    foreach (GameObject bar in playerList)
                    {
                        if (bar.name == (tempTeamDic[ConstData.Saber].player_Id).ToString())
                        {
                            bar.GetComponent<Image>().sprite = bar.transform.GetChild(0).GetComponent<Image>().sprite;
                        }
                    }
                }
                if (tempTeamDic[ConstData.Knight] != null)
                {
                    GameObject knt = ObjectPoolManager.Instance.InstantiateMyGameObject
                    (
                    ResourcesManager.Instance.FindPlayerPrefab
                    ((tempTeamDic[ConstData.Knight].PrefabsID).ToString())
                    );
                    knt.transform.position = step02.transform.GetChild(0).transform.position;
                    knt.transform.parent = step02.transform;
                    knt.name = (tempTeamDic[ConstData.Knight].player_Id).ToString();
                    knt.transform.GetComponent<Animator>().SetBool("isWait", true);
                    //条变色
                    foreach (GameObject bar in playerList)
                    {
                        if (bar.name == (tempTeamDic[ConstData.Knight].player_Id).ToString())
                        {
                            bar.GetComponent<Image>().sprite = bar.transform.GetChild(0).GetComponent<Image>().sprite;
                        }
                    }
                }
                if (tempTeamDic[ConstData.Berserker] != null)
                {
                    GameObject bsk = ObjectPoolManager.Instance.InstantiateMyGameObject
                    (
                    ResourcesManager.Instance.FindPlayerPrefab
                    ((tempTeamDic[ConstData.Berserker].PrefabsID).ToString())
                    );
                    bsk.transform.position = step03.transform.GetChild(0).transform.position;
                    bsk.transform.parent = step03.transform;
                    bsk.name = (tempTeamDic[ConstData.Berserker].player_Id).ToString();
                    bsk.transform.GetComponent<Animator>().SetBool("isWait", true);
                    //条变色
                    foreach (GameObject bar in playerList)
                    {
                        if (bar.name == (tempTeamDic[ConstData.Berserker].player_Id).ToString())
                        {
                            bar.GetComponent<Image>().sprite = bar.transform.GetChild(0).GetComponent<Image>().sprite;
                        }
                    }
                }
                if (tempTeamDic[ConstData.Caster] != null)
                {
                    GameObject cst = ObjectPoolManager.Instance.InstantiateMyGameObject
                    (
                    ResourcesManager.Instance.FindPlayerPrefab
                    ((tempTeamDic[ConstData.Caster].PrefabsID).ToString())
                    );
                    cst.transform.position = step04.transform.GetChild(0).transform.position;
                    cst.transform.parent = step04.transform;
                    cst.name = (tempTeamDic[ConstData.Caster].player_Id).ToString();
                    cst.transform.GetComponent<Animator>().SetBool("isWait", true);
                    //条变色
                    foreach (GameObject bar in playerList)
                    {
                        if (bar.name == (tempTeamDic[ConstData.Caster].player_Id).ToString())
                        {
                            bar.GetComponent<Image>().sprite = bar.transform.GetChild(0).GetComponent<Image>().sprite;
                        }
                    }
                }
                if (tempTeamDic[ConstData.Hunter] != null)
                {
                    GameObject hut = ObjectPoolManager.Instance.InstantiateMyGameObject
                                (
                                ResourcesManager.Instance.FindPlayerPrefab
                                ((tempTeamDic[ConstData.Hunter].PrefabsID).ToString())
                                );
                    hut.transform.position = step05.transform.GetChild(0).transform.position;
                    hut.transform.parent = step05.transform;
                    hut.name = (tempTeamDic[ConstData.Hunter].player_Id).ToString();
                    hut.transform.GetComponent<Animator>().SetBool("isWait", true);
                    //条变色
                    foreach (GameObject bar in playerList)
                    {
                        if (bar.name == (tempTeamDic[ConstData.Hunter].player_Id).ToString())
                        {
                            bar.GetComponent<Image>().sprite = bar.transform.GetChild(0).GetComponent<Image>().sprite;
                        }
                    }
                }
                break;
            case ConstData.Saber:
                if (tempTeamDic[ConstData.Saber] != null)
                {
                    GameObject sbr = ObjectPoolManager.Instance.InstantiateMyGameObject
                    (
                    ResourcesManager.Instance.FindPlayerPrefab
                    ((tempTeamDic[ConstData.Saber].PrefabsID).ToString())
                    );
                    sbr.transform.position = step01.transform.GetChild(0).transform.position;
                    sbr.transform.parent = step01.transform;
                    sbr.name = (tempTeamDic[ConstData.Saber].player_Id).ToString();
                    sbr.transform.GetComponent<Animator>().SetBool("isWait", true);
                    //条变色
                    foreach (GameObject bar in playerList)
                    {
                        if (bar.name == (tempTeamDic[ConstData.Saber].player_Id).ToString())
                        {
                            bar.GetComponent<Image>().sprite = bar.transform.GetChild(0).GetComponent<Image>().sprite;
                        }
                    }
                }
                break;
            case ConstData.Knight:
                if (tempTeamDic[ConstData.Knight] != null)
                {
                    GameObject knt = ObjectPoolManager.Instance.InstantiateMyGameObject
                    (
                    ResourcesManager.Instance.FindPlayerPrefab
                    ((tempTeamDic[ConstData.Knight].PrefabsID).ToString())
                    );
                    knt.transform.position = step02.transform.GetChild(0).transform.position;
                    knt.transform.parent = step02.transform;
                    knt.name = (tempTeamDic[ConstData.Knight].player_Id).ToString();
                    knt.transform.GetComponent<Animator>().SetBool("isWait", true);
                    //条变色
                    foreach (GameObject bar in playerList)
                    {
                        if (bar.name == (tempTeamDic[ConstData.Knight].player_Id).ToString())
                        {
                            bar.GetComponent<Image>().sprite = bar.transform.GetChild(0).GetComponent<Image>().sprite;
                        }
                    }
                }
                break;
            case ConstData.Berserker:
                if (tempTeamDic[ConstData.Berserker] != null)
                {
                    GameObject bsk = ObjectPoolManager.Instance.InstantiateMyGameObject
                    (
                    ResourcesManager.Instance.FindPlayerPrefab
                    ((tempTeamDic[ConstData.Berserker].PrefabsID).ToString())
                    );
                    bsk.transform.position = step03.transform.GetChild(0).transform.position;
                    bsk.transform.parent = step03.transform;
                    bsk.name = (tempTeamDic[ConstData.Berserker].player_Id).ToString();
                    bsk.transform.GetComponent<Animator>().SetBool("isWait", true);
                    //条变色
                    foreach (GameObject bar in playerList)
                    {
                        if (bar.name == (tempTeamDic[ConstData.Berserker].player_Id).ToString())
                        {
                            bar.GetComponent<Image>().sprite = bar.transform.GetChild(0).GetComponent<Image>().sprite;
                        }
                    }
                }
                break;
            case ConstData.Caster:
                if (tempTeamDic[ConstData.Caster] != null)
                {
                    GameObject cst = ObjectPoolManager.Instance.InstantiateMyGameObject
                    (
                    ResourcesManager.Instance.FindPlayerPrefab
                    ((tempTeamDic[ConstData.Caster].PrefabsID).ToString())
                    );
                    cst.transform.position = step04.transform.GetChild(0).transform.position;
                    cst.transform.parent = step04.transform;
                    cst.name = (tempTeamDic[ConstData.Caster].player_Id).ToString();
                    cst.transform.GetComponent<Animator>().SetBool("isWait", true);
                    //条变色
                    foreach (GameObject bar in playerList)
                    {
                        if (bar.name == (tempTeamDic[ConstData.Caster].player_Id).ToString())
                        {
                            bar.GetComponent<Image>().sprite = bar.transform.GetChild(0).GetComponent<Image>().sprite;
                        }
                    }
                }
                break;
            case ConstData.Hunter:
                if (tempTeamDic[ConstData.Hunter] != null)
                {
                    GameObject hut = ObjectPoolManager.Instance.InstantiateMyGameObject
                                (
                                ResourcesManager.Instance.FindPlayerPrefab
                                ((tempTeamDic[ConstData.Hunter].PrefabsID).ToString())
                                );
                    hut.transform.position = step05.transform.GetChild(0).transform.position;
                    hut.transform.parent = step05.transform;
                    hut.name = (tempTeamDic[ConstData.Hunter].player_Id).ToString();
                    hut.transform.GetComponent<Animator>().SetBool("isWait", true);
                    //条变色
                    foreach (GameObject bar in playerList)
                    {
                        if (bar.name == (tempTeamDic[ConstData.Hunter].player_Id).ToString())
                        {
                            bar.GetComponent<Image>().sprite = bar.transform.GetChild(0).GetComponent<Image>().sprite;
                        }
                    }
                }
                break;
        }
    }
    

    /// <summary>
    /// 装备格内容区域自适应长度
    /// </summary>
    /// <param 筛选模式="FilterClass"></param>
    void ItemBarContentAdaptive()
    {
        int tempheight = 1207;
        transform.Find(ConstData.ControllerArea_ListContent2).GetComponent<RectTransform>().sizeDelta = new Vector2(1200, tempheight);
        if (itemList.Count > ConstData.GridCount)
        {
            tempheight = 1207 + ((int)((itemList.Count - ConstData.GridCount) / 6) + 1) * 200;
            transform.Find(ConstData.ControllerArea_ListContent2).GetComponent<RectTransform>().sizeDelta = new Vector2(1200, tempheight);
        }
    }
    /// <summary>
    /// 生成装备格
    /// </summary>
    /// <param 筛选模式="FilterClass"></param>
    void ItemBarCreate(string FilterClass)
    {
        //清空现有格子
        ItemBarClear();

        switch (FilterClass)
        {
            case ConstData.All:
                //生成武器类
                for (int i = 0; i < SQLiteManager.Instance.bagDataSource.Count; i++)
                {
                    if (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon != 0)
                    {
                        //生成武器
                        GameObject wp = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                        //指定父物体
                        wp.transform.parent = transform.Find(ConstData.ControllerArea_ListContent2).transform;
                        //武器的大小、图片和标签设置
                        wp.transform.localScale = new Vector3(1, 1, 1);
                        wp.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                            ((SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon).ToString());
                        wp.transform.GetChild(0).tag = ConstData.EquipmentType;
                        wp.transform.GetChild(0).name = (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon).ToString();
                        //添加数据脚本
                        if (wp.transform.GetChild(0).GetComponent<BagItem>() == null)
                        {
                            wp.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                            UISceneWidget ChangeEquipmentClick = UISceneWidget.Get(wp);
                            ChangeEquipmentClick.PointerClick += WeaponSelectFrame;
                            ChangeEquipmentClick.Drag += OnDrag;
                        }
                        //获得数据
                        wp.transform.GetChild(0).GetComponent<BagItem>().GetData();
                        wp.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                        itemList.Add(wp);
                    }
                }
                //生成防具类
                for (int i = 0; i < SQLiteManager.Instance.bagDataSource.Count; i++)
                {
                    if (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment != 0)
                    {
                        //生成防具
                        GameObject eq = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                        //指定父物体
                        eq.transform.parent = transform.Find(ConstData.ControllerArea_ListContent2).transform;
                        //防具的大小、图片和标签设置
                        eq.transform.localScale = new Vector3(1, 1, 1);
                        eq.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                            ((SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment).ToString());
                        eq.transform.GetChild(0).tag = ConstData.EquipmentType;
                        eq.transform.GetChild(0).name = (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment).ToString();
                        //添加数据脚本
                        if (eq.transform.GetChild(0).GetComponent<BagItem>() == null)
                        {
                            eq.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                            UISceneWidget ChangeEquipmentClick = UISceneWidget.Get(eq);
                            ChangeEquipmentClick.PointerClick += WeaponSelectFrame;
                            ChangeEquipmentClick.Drag += OnDrag;
                        }
                        //获得数据
                        eq.transform.GetChild(0).GetComponent<BagItem>().GetData();
                        eq.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                        itemList.Add(eq);
                    }
                }
                //生成空格子
                if (itemList.Count < ConstData.GridCount)
                {
                    for (int i = 0; i < (ConstData.GridCount - itemList.Count); i++)
                    {
                        GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                        //指定父物体
                        nullGrid.transform.parent = transform.Find(ConstData.ControllerArea_ListContent2).transform;
                        //图的大小设置
                        nullGrid.transform.localScale = new Vector3(1, 1, 1);
                        null_itemList.Add(nullGrid);
                    }
                }
                ItemBarContentAdaptive();
                break;
            case ConstData.Saber:
                //生成武器类
                for (int i = 0; i < SQLiteManager.Instance.bagDataSource.Count; i++)
                {
                    if (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon != 0)
                    {
                        if (SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon].
                            equipmentClass == ConstData.Saber)
                        {
                            //生成武器
                            GameObject wp = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                            //指定父物体
                            wp.transform.parent = transform.Find(ConstData.ControllerArea_ListContent2).transform;
                            //武器的大小、图片和标签设置
                            wp.transform.localScale = new Vector3(1, 1, 1);
                            wp.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                                ((SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon).ToString());
                            wp.transform.GetChild(0).tag = ConstData.EquipmentType;
                            wp.transform.GetChild(0).name = (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon).ToString();
                            //添加数据脚本
                            if (wp.transform.GetChild(0).GetComponent<BagItem>() == null)
                            {
                                wp.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                                UISceneWidget ChangeEquipmentClick = UISceneWidget.Get(wp);
                                ChangeEquipmentClick.PointerClick += WeaponSelectFrame;
                                ChangeEquipmentClick.Drag += OnDrag;
                            }
                            //获得数据
                            wp.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            wp.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                            itemList.Add(wp);
                        }
                    }
                }
                //生成防具类
                for (int i = 0; i < SQLiteManager.Instance.bagDataSource.Count; i++)
                {
                    if (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment != 0)
                    {
                        if (SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment].
                            equipmentClass == ConstData.Saber)
                        {
                            //生成防具
                            GameObject eq = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                            //指定父物体
                            eq.transform.parent = transform.Find(ConstData.ControllerArea_ListContent2).transform;
                            //防具的大小、图片和标签设置
                            eq.transform.localScale = new Vector3(1, 1, 1);
                            eq.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                                ((SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment).ToString());
                            eq.transform.GetChild(0).tag = ConstData.EquipmentType;
                            eq.transform.GetChild(0).name = (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment).ToString();
                            //添加数据脚本
                            if (eq.transform.GetChild(0).GetComponent<BagItem>() == null)
                            {
                                eq.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                                UISceneWidget ChangeEquipmentClick = UISceneWidget.Get(eq);
                                ChangeEquipmentClick.PointerClick += WeaponSelectFrame;
                                ChangeEquipmentClick.Drag += OnDrag;
                            }
                            //获得数据
                            eq.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            eq.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                            itemList.Add(eq);
                        }
                    }
                }
                //生成空格子
                if (itemList.Count < ConstData.GridCount)
                {
                    for (int i = 0; i < (ConstData.GridCount - itemList.Count); i++)
                    {
                        GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                        //指定父物体
                        nullGrid.transform.parent = transform.Find(ConstData.ControllerArea_ListContent2).transform;
                        //图的大小设置
                        nullGrid.transform.localScale = new Vector3(1, 1, 1);
                        null_itemList.Add(nullGrid);
                    }
                }
                ItemBarContentAdaptive();
                break;
            case ConstData.Knight:
                //生成武器类
                for (int i = 0; i < SQLiteManager.Instance.bagDataSource.Count; i++)
                {
                    if (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon != 0)
                    {
                        if (SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon].
                            equipmentClass == ConstData.Knight)
                        {
                            //生成武器
                            GameObject wp = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                            //指定父物体
                            wp.transform.parent = transform.Find(ConstData.ControllerArea_ListContent2).transform;
                            //武器的大小、图片和标签设置
                            wp.transform.localScale = new Vector3(1, 1, 1);
                            wp.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                                ((SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon).ToString());
                            wp.transform.GetChild(0).tag = ConstData.EquipmentType;
                            wp.transform.GetChild(0).name = (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon).ToString();
                            //添加数据脚本
                            if (wp.transform.GetChild(0).GetComponent<BagItem>() == null)
                            {
                                wp.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                                UISceneWidget ChangeEquipmentClick = UISceneWidget.Get(wp);
                                ChangeEquipmentClick.PointerClick += WeaponSelectFrame;
                                ChangeEquipmentClick.Drag += OnDrag;
                            }
                            //获得数据
                            wp.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            wp.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                            itemList.Add(wp);
                        }
                    }
                }
                //生成防具类
                for (int i = 0; i < SQLiteManager.Instance.bagDataSource.Count; i++)
                {
                    if (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment != 0)
                    {
                        if (SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment].
                            equipmentClass == ConstData.Knight)
                        {
                            //生成防具
                            GameObject eq = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                            //指定父物体
                            eq.transform.parent = transform.Find(ConstData.ControllerArea_ListContent2).transform;
                            //防具的大小、图片和标签设置
                            eq.transform.localScale = new Vector3(1, 1, 1);
                            eq.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                                ((SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment).ToString());
                            eq.transform.GetChild(0).tag = ConstData.EquipmentType;
                            eq.transform.GetChild(0).name = (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment).ToString();
                            //添加数据脚本
                            if (eq.transform.GetChild(0).GetComponent<BagItem>() == null)
                            {
                                eq.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                                UISceneWidget ChangeEquipmentClick = UISceneWidget.Get(eq);
                                ChangeEquipmentClick.PointerClick += WeaponSelectFrame;
                                ChangeEquipmentClick.Drag += OnDrag;
                            }
                            //获得数据
                            eq.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            eq.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                            itemList.Add(eq);
                        }
                    }
                }
                //生成空格子
                if (itemList.Count < ConstData.GridCount)
                {
                    for (int i = 0; i < (ConstData.GridCount - itemList.Count); i++)
                    {
                        GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                        //指定父物体
                        nullGrid.transform.parent = transform.Find(ConstData.ControllerArea_ListContent2).transform;
                        //图的大小设置
                        nullGrid.transform.localScale = new Vector3(1, 1, 1);
                        null_itemList.Add(nullGrid);
                    }
                }
                ItemBarContentAdaptive();
                break;
            case ConstData.Berserker:
                //生成武器类
                for (int i = 0; i < SQLiteManager.Instance.bagDataSource.Count; i++)
                {
                    if (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon != 0)
                    {
                        if (SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon].
                            equipmentClass == ConstData.Berserker)
                        {
                            //生成武器
                            GameObject wp = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                            //指定父物体
                            wp.transform.parent = transform.Find(ConstData.ControllerArea_ListContent2).transform;
                            //武器的大小、图片和标签设置
                            wp.transform.localScale = new Vector3(1, 1, 1);
                            wp.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                                ((SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon).ToString());
                            wp.transform.GetChild(0).tag = ConstData.EquipmentType;
                            wp.transform.GetChild(0).name = (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon).ToString();
                            //添加数据脚本
                            if (wp.transform.GetChild(0).GetComponent<BagItem>() == null)
                            {
                                wp.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                                UISceneWidget ChangeEquipmentClick = UISceneWidget.Get(wp);
                                ChangeEquipmentClick.PointerClick += WeaponSelectFrame;
                                ChangeEquipmentClick.Drag += OnDrag;
                            }
                            //获得数据
                            wp.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            wp.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                            itemList.Add(wp);
                        }
                    }
                }
                //生成防具类
                for (int i = 0; i < SQLiteManager.Instance.bagDataSource.Count; i++)
                {
                    if (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment != 0)
                    {
                        if (SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment].
                            equipmentClass == ConstData.Berserker)
                        {
                            //生成防具
                            GameObject eq = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                            //指定父物体
                            eq.transform.parent = transform.Find(ConstData.ControllerArea_ListContent2).transform;
                            //防具的大小、图片和标签设置
                            eq.transform.localScale = new Vector3(1, 1, 1);
                            eq.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                                ((SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment).ToString());
                            eq.transform.GetChild(0).tag = ConstData.EquipmentType;
                            eq.transform.GetChild(0).name = (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment).ToString();
                            //添加数据脚本
                            if (eq.transform.GetChild(0).GetComponent<BagItem>() == null)
                            {
                                eq.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                                UISceneWidget ChangeEquipmentClick = UISceneWidget.Get(eq);
                                ChangeEquipmentClick.PointerClick += WeaponSelectFrame;
                                ChangeEquipmentClick.Drag += OnDrag;
                            }
                            //获得数据
                            eq.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            eq.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                            itemList.Add(eq);
                        }
                    }
                }
                //生成空格子
                if (itemList.Count < ConstData.GridCount)
                {
                    for (int i = 0; i < (ConstData.GridCount - itemList.Count); i++)
                    {
                        GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                        //指定父物体
                        nullGrid.transform.parent = transform.Find(ConstData.ControllerArea_ListContent2).transform;
                        //图的大小设置
                        nullGrid.transform.localScale = new Vector3(1, 1, 1);
                        null_itemList.Add(nullGrid);
                    }
                }
                ItemBarContentAdaptive();
                break;
            case ConstData.Caster:
                //生成武器类
                for (int i = 0; i < SQLiteManager.Instance.bagDataSource.Count; i++)
                {
                    if (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon != 0)
                    {
                        if (SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon].
                            equipmentClass == ConstData.Caster)
                        {
                            //生成武器
                            GameObject wp = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                            //指定父物体
                            wp.transform.parent = transform.Find(ConstData.ControllerArea_ListContent2).transform;
                            //武器的大小、图片和标签设置
                            wp.transform.localScale = new Vector3(1, 1, 1);
                            wp.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                                ((SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon).ToString());
                            wp.transform.GetChild(0).tag = ConstData.EquipmentType;
                            wp.transform.GetChild(0).name = (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon).ToString();
                            //添加数据脚本
                            if (wp.transform.GetChild(0).GetComponent<BagItem>() == null)
                            {
                                wp.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                                UISceneWidget ChangeEquipmentClick = UISceneWidget.Get(wp);
                                ChangeEquipmentClick.PointerClick += WeaponSelectFrame;
                                ChangeEquipmentClick.Drag += OnDrag;
                            }
                            //获得数据
                            wp.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            wp.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                            itemList.Add(wp);
                        }
                    }
                }
                //生成防具类
                for (int i = 0; i < SQLiteManager.Instance.bagDataSource.Count; i++)
                {
                    if (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment != 0)
                    {
                        if (SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment].
                            equipmentClass == ConstData.Caster)
                        {
                            //生成防具
                            GameObject eq = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                            //指定父物体
                            eq.transform.parent = transform.Find(ConstData.ControllerArea_ListContent2).transform;
                            //防具的大小、图片和标签设置
                            eq.transform.localScale = new Vector3(1, 1, 1);
                            eq.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                                ((SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment).ToString());
                            eq.transform.GetChild(0).tag = ConstData.EquipmentType;
                            eq.transform.GetChild(0).name = (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment).ToString();
                            //添加数据脚本
                            if (eq.transform.GetChild(0).GetComponent<BagItem>() == null)
                            {
                                eq.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                                UISceneWidget ChangeEquipmentClick = UISceneWidget.Get(eq);
                                ChangeEquipmentClick.PointerClick += WeaponSelectFrame;
                                ChangeEquipmentClick.Drag += OnDrag;
                            }
                            //获得数据
                            eq.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            eq.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                            itemList.Add(eq);
                        }
                    }
                }
                //生成空格子
                if (itemList.Count < ConstData.GridCount)
                {
                    for (int i = 0; i < (ConstData.GridCount - itemList.Count); i++)
                    {
                        GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                        //指定父物体
                        nullGrid.transform.parent = transform.Find(ConstData.ControllerArea_ListContent2).transform;
                        //图的大小设置
                        nullGrid.transform.localScale = new Vector3(1, 1, 1);
                        null_itemList.Add(nullGrid);
                    }
                }
                ItemBarContentAdaptive();
                break;
            case ConstData.Hunter:
                //生成武器类
                for (int i = 0; i < SQLiteManager.Instance.bagDataSource.Count; i++)
                {
                    if (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon != 0)
                    {
                        if (SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon].
                            equipmentClass == ConstData.Hunter)
                        {
                            //生成武器
                            GameObject wp = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                            //指定父物体
                            wp.transform.parent = transform.Find(ConstData.ControllerArea_ListContent2).transform;
                            //武器的大小、图片和标签设置
                            wp.transform.localScale = new Vector3(1, 1, 1);
                            wp.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                                ((SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon).ToString());
                            wp.transform.GetChild(0).tag = ConstData.EquipmentType;
                            wp.transform.GetChild(0).name = (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon).ToString();
                            //添加数据脚本
                            if (wp.transform.GetChild(0).GetComponent<BagItem>() == null)
                            {
                                wp.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                                UISceneWidget ChangeEquipmentClick = UISceneWidget.Get(wp);
                                ChangeEquipmentClick.PointerClick += WeaponSelectFrame;
                                ChangeEquipmentClick.Drag += OnDrag;
                            }
                            //获得数据
                            wp.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            wp.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                            itemList.Add(wp);
                        }
                    }
                }
                //生成防具类
                for (int i = 0; i < SQLiteManager.Instance.bagDataSource.Count; i++)
                {
                    if (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment != 0)
                    {
                        if (SQLiteManager.Instance.equipmentDataSource[SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment].
                            equipmentClass == ConstData.Hunter)
                        {
                            //生成防具
                            GameObject eq = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                            //指定父物体
                            eq.transform.parent = transform.Find(ConstData.ControllerArea_ListContent2).transform;
                            //防具的大小、图片和标签设置
                            eq.transform.localScale = new Vector3(1, 1, 1);
                            eq.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                                ((SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment).ToString());
                            eq.transform.GetChild(0).tag = ConstData.EquipmentType;
                            eq.transform.GetChild(0).name = (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment).ToString();
                            //添加数据脚本
                            if (eq.transform.GetChild(0).GetComponent<BagItem>() == null)
                            {
                                eq.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                                UISceneWidget ChangeEquipmentClick = UISceneWidget.Get(eq);
                                ChangeEquipmentClick.PointerClick += WeaponSelectFrame;
                                ChangeEquipmentClick.Drag += OnDrag;
                            }
                            //获得数据
                            eq.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            eq.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                            itemList.Add(eq);
                        }
                    }
                }
                //生成空格子
                if (itemList.Count < ConstData.GridCount)
                {
                    for (int i = 0; i < (ConstData.GridCount - itemList.Count); i++)
                    {
                        GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                        //指定父物体
                        nullGrid.transform.parent = transform.Find(ConstData.ControllerArea_ListContent2).transform;
                        //图的大小设置
                        nullGrid.transform.localScale = new Vector3(1, 1, 1);
                        null_itemList.Add(nullGrid);
                    }
                }
                ItemBarContentAdaptive();
                break;
        }

    }
    /// <summary>
    /// 清空所有装备格和道具图片
    /// </summary>
    void ItemBarClear()
    {
        //回收所有格
        for (int i = 0; i < itemList.Count; i++)
        {
            itemList[i].name = ConstData.GridEx;
            ObjectPoolManager.Instance.RecycleMyGameObject(itemList[i]);
        }
        for (int j = 0; j < null_itemList.Count; j++)
        {
            ObjectPoolManager.Instance.RecycleMyGameObject(null_itemList[j]);
        }
        //清空列表
        itemList.Clear();
        null_itemList.Clear();
    }


    /// <summary>
    /// 清除装备
    /// </summary>
    void EquipmentClear()
    {
        if (transform.Find(ConstData.ControllerExArea_EquipmentMode).GetChild(0).childCount != 1)
        {
            ObjectPoolManager.Instance.RecycleMyGameObject
                (transform.Find(ConstData.ControllerExArea_EquipmentMode).GetChild(0).GetChild(1).gameObject);
        }
        if (transform.Find(ConstData.ControllerExArea_EquipmentMode).GetChild(1).childCount != 1)
        {
            ObjectPoolManager.Instance.RecycleMyGameObject
                (transform.Find(ConstData.ControllerExArea_EquipmentMode).GetChild(1).GetChild(1).gameObject);
        }
    }
    /// <summary>
    /// 更新装备信息
    /// </summary>
    void UpdataTheEquipment()
    {
        //清除现有展示信息
        EquipmentClear();
        if (selecteHero != null)
        {
            //更新武器展示信息
            GameObject tempWP = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindUIPrefab(ConstData.ItemIcon));
            tempWP.transform.parent = transform.Find(ConstData.ControllerExArea_EquipmentMode).GetChild(0).transform;
            tempWP.transform.localScale = new Vector3(1, 1, 1);
            tempWP.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            tempWP.GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite((selecteHero.Weapon).ToString());
            tempWP.GetComponent<Image>().raycastTarget = false;
            //更新防具展示信息
            GameObject tempEQ = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindUIPrefab(ConstData.ItemIcon));
            tempEQ.transform.parent = transform.Find(ConstData.ControllerExArea_EquipmentMode).GetChild(1).transform;
            tempEQ.transform.localScale = new Vector3(1, 1, 1);
            tempEQ.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            tempEQ.GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite((selecteHero.Equipment).ToString());
            tempEQ.GetComponent<Image>().raycastTarget = false;
        }
        HeroDataDisplay();
    }


    /// <summary>
    /// 显示英雄信息
    /// </summary>
    void HeroDataDisplay()
    {
        if (selecteHero != null)
        {
            heroName.text = selecteHero.player_Name;
            LV.text = (selecteHero.Level).ToString();
            EXP.minValue = SQLiteManager.Instance.lVDataSource[selecteHero.Level - 1].level_MaxEXP;
            EXP.maxValue = SQLiteManager.Instance.lVDataSource[selecteHero.Level].level_MaxEXP;
            EXP.value = selecteHero.EXP;
            HP.text = (selecteHero.HP +
                SQLiteManager.Instance.equipmentDataSource[selecteHero.Weapon].equipment_HP +
                SQLiteManager.Instance.equipmentDataSource[selecteHero.Equipment].equipment_HP).ToString();
            AD.text = (selecteHero.AD +
                SQLiteManager.Instance.equipmentDataSource[selecteHero.Weapon].equipment_AD +
                SQLiteManager.Instance.equipmentDataSource[selecteHero.Equipment].equipment_AD).ToString();
            AP.text = (selecteHero.AP +
                SQLiteManager.Instance.equipmentDataSource[selecteHero.Weapon].equipment_AP +
                SQLiteManager.Instance.equipmentDataSource[selecteHero.Equipment].equipment_AP).ToString();
            DEF.text = (selecteHero.DEF +
                SQLiteManager.Instance.equipmentDataSource[selecteHero.Weapon].equipment_DEF +
                SQLiteManager.Instance.equipmentDataSource[selecteHero.Equipment].equipment_DEF).ToString();
            RES.text = (selecteHero.RES +
                SQLiteManager.Instance.equipmentDataSource[selecteHero.Weapon].equipment_RES +
                SQLiteManager.Instance.equipmentDataSource[selecteHero.Equipment].equipment_RES).ToString();
        }
        else
        {
            heroName.text = "无";
            LV.text = "0";
            EXP.minValue = 0;
            EXP.maxValue = 1;
            EXP.value = 0;
            HP.text = "0";
            AD.text = "0";
            AP.text = "0";
            DEF.text = "0";
            RES.text = "0";
        }
    }

    #region 解决拖拽和点击冲突的问题
    GameObject itemList_Drag;
    void OnBeginDrag(PointerEventData data)
    {
        itemList_Drag.GetComponent<ScrollRect>().OnBeginDrag(data);
    }
    void OnDrag(PointerEventData data)
    {
        itemList_Drag.GetComponent<ScrollRect>().OnDrag(data);
    }
    void OnEndDrag(PointerEventData data)
    {
        itemList_Drag.GetComponent<ScrollRect>().OnEndDrag(data);
    }
    #endregion
}
