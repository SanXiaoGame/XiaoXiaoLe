using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIMainCityManagerController : MonoBehaviour, IUIBase
{
    //预制体外：游戏画面区域
    GameObject GameArea;
    //选中的道具
    GameObject itemG;
    int itemID;
    //显示信息
    Text nameAndClass;
    Text property;
    Text GoldCoin;
    //装备格预制体
    GameObject itemGrid;
    //装备图标预制体
    GameObject itemBar;
    //带道具装备格对象表（武器包）
    List<GameObject> itemList_WP;
    //空装备格对象表（武器包）
    List<GameObject> null_itemList_WP;
    //带道具装备格对象表（防具包）
    List<GameObject> itemList_EQ;
    //空装备格对象表（防具包）
    List<GameObject> null_itemList_EQ;
    //带道具装备格对象表（消耗品包）
    List<GameObject> itemList_CO;
    //空装备格对象表（消耗品包）
    List<GameObject> null_itemList_CO;
    //带道具装备格对象表（材料包）
    List<GameObject> itemList_MT;
    //空装备格对象表（材料包）
    List<GameObject> null_itemList_MT;
    //筛选列表
    List<GameObject> filterList;

    //需要操作的界面
    GameObject charaButton;
    GameObject storeButton;
    GameObject superMarketButton;
    GameObject drunkeryButton;
    GameObject settingButton;
    GameObject BattleButton;
    GameObject ItemListBG_WP;
    GameObject ItemListBG_EQ;
    GameObject ItemListBG_CO;
    GameObject ItemListBG_MT;
    GameObject WeaponBag;
    GameObject EquipmentBag;
    GameObject ConsumableBag;
    GameObject MaterialBag;
    GameObject saberStone;
    GameObject knightStone;
    GameObject berserkerStone;
    GameObject casterStone;
    GameObject hunterStone;

    private void Awake()
    {
        CurrencyManager.Instance.LoadCurrencyDataFromSQLite();
    }

    //进入界面
    public void OnEntering()
    {
        #region 游戏画面区
        GameArea = ObjectPoolManager.Instance.InstantiateMyGameObject
            (ResourcesManager.Instance.FindUIPrefab(ConstData.UIMainCityPrefab_GameArea));
        GameArea.transform.position = ResourcesManager.Instance.FindUIPrefab(ConstData.UIMainCityPrefab_GameArea).transform.position;
        #endregion

        #region UI组件和对象获取
        nameAndClass = transform.Find(ConstData.GameArea_MessageFrame_NameAndClass).gameObject.GetComponent<Text>();
        property = transform.Find(ConstData.GameArea_MessageFrame_Property).gameObject.GetComponent<Text>();
        itemGrid = ResourcesManager.Instance.FindUIPrefab(ConstData.Grid);
        itemBar = ResourcesManager.Instance.FindUIPrefab(ConstData.GridEx);
        GoldCoin = transform.Find(ConstData.GameArea_GoldCoin).GetComponent<Text>();
        #endregion

        //初始化设置
        string[] tempText01 = new string[]
            {
                "HP:",
                "000",
                "\nAD:000 AP:000 DEF:00 RES:00",
            };
        nameAndClass.text = "[无]";
        property.text = StringSplicingTool.StringSplicing(tempText01);
        itemList_WP = new List<GameObject>();
        null_itemList_WP = new List<GameObject>();
        itemList_EQ = new List<GameObject>();
        null_itemList_EQ = new List<GameObject>();
        itemList_CO = new List<GameObject>();
        null_itemList_CO = new List<GameObject>();
        itemList_MT = new List<GameObject>();
        null_itemList_MT = new List<GameObject>();
        filterList = new List<GameObject>();
        itemList_WP.Clear();
        null_itemList_WP.Clear();
        itemList_EQ.Clear();
        null_itemList_EQ.Clear();
        itemList_CO.Clear();
        null_itemList_CO.Clear();
        itemList_MT.Clear();
        null_itemList_MT.Clear();
        filterList.Clear();

        #region 系统操作区
        charaButton = transform.Find(ConstData.SystemArea_CharacterButton).gameObject;
        if (charaButton.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget charaButtonClick = UISceneWidget.Get(charaButton);
            charaButtonClick.PointerClick += CharacterEnter;
        }
        storeButton = transform.Find(ConstData.SystemArea_StoreButton).gameObject;
        if (storeButton.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget storeButtonClick = UISceneWidget.Get(storeButton);
            storeButtonClick.PointerClick += StoreEnter;
        }
        superMarketButton = transform.Find(ConstData.SystemArea_SuperMarketButton).gameObject;
        if(superMarketButton.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget superMarketButtonClick = UISceneWidget.Get(superMarketButton);
            superMarketButtonClick.PointerClick += SuperMarketEnter;
        }
        drunkeryButton = transform.Find(ConstData.SystemArea_DrunkeryButton).gameObject;
        if (drunkeryButton.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget drunkeryButtonClick = UISceneWidget.Get(drunkeryButton);
            drunkeryButtonClick.PointerClick += DrunkeryEnter;
        }
        settingButton = transform.Find(ConstData.SystemArea_SettingButton).gameObject;
        if (settingButton.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget settingButtonClick = UISceneWidget.Get(settingButton);
            settingButtonClick.PointerClick += SettingEnter;
        }
        BattleButton = transform.Find(ConstData.SystemArea_BattleButton).gameObject;
        if (BattleButton.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget BattleButtonClick = UISceneWidget.Get(BattleButton);
            BattleButtonClick.PointerClick += BattleEnter;
        }
        #endregion

        #region 控制区
        ItemListBG_WP = transform.Find(ConstData.ControllerArea_ItemListBG_WP).gameObject;
        ItemListBG_EQ = transform.Find(ConstData.ControllerArea_ItemListBG_EQ).gameObject;
        ItemListBG_CO = transform.Find(ConstData.ControllerArea_ItemListBG_CO).gameObject;
        ItemListBG_MT = transform.Find(ConstData.ControllerArea_ItemListBG_MT).gameObject;
        AllListCreate();
        #endregion

        #region 控制区附属区域
        WeaponBag = transform.Find(ConstData.ControllerExArea_WeaponBag).gameObject;
        if (WeaponBag.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget WeaponBagClick = UISceneWidget.Get(WeaponBag);
            WeaponBagClick.PointerClick += WeaponBagChange;
        }
        EquipmentBag = transform.Find(ConstData.ControllerExArea_EquipmentBag).gameObject;
        if (EquipmentBag.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget EquipmentBagClick = UISceneWidget.Get(EquipmentBag);
            EquipmentBagClick.PointerClick += EquipmentBagChange;
        }
        ConsumableBag = transform.Find(ConstData.ControllerExArea_ConsumableBag).gameObject;
        if (ConsumableBag.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget ConsumableBagClick = UISceneWidget.Get(ConsumableBag);
            ConsumableBagClick.PointerClick += ConsumableBagChange;
        }
        MaterialBag = transform.Find(ConstData.ControllerExArea_MaterialBag).gameObject;
        if (MaterialBag.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget MaterialBagClick = UISceneWidget.Get(MaterialBag);
            MaterialBagClick.PointerClick += MaterialBagChange;
        }
        #endregion

        #region 筛选区
        saberStone = transform.Find(ConstData.Filter_StoneSaberTag).gameObject;
        if (saberStone.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget saberStoneClick = UISceneWidget.Get(saberStone);
            saberStoneClick.PointerClick += SaberFilter;
        }
        knightStone = transform.Find(ConstData.Filter_StoneKnightTag).gameObject;
        if (knightStone.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget knightStoneClick = UISceneWidget.Get(knightStone);
            knightStoneClick.PointerClick += KnightFilter;
        }
        berserkerStone = transform.Find(ConstData.Filter_StoneBerserkerTag).gameObject;
        if (berserkerStone.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget berserkerStoneClick = UISceneWidget.Get(berserkerStone);
            berserkerStoneClick.PointerClick += BerserkerFilter;
        }
        casterStone = transform.Find(ConstData.Filter_StoneCasterTag).gameObject;
        if (casterStone.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget casterStoneClick = UISceneWidget.Get(casterStone);
            casterStoneClick.PointerClick += CasterFilter;
        }
        hunterStone = transform.Find(ConstData.Filter_StoneHunterTag).gameObject;
        if (hunterStone.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget hunterStoneClick = UISceneWidget.Get(hunterStone);
            hunterStoneClick.PointerClick += HunterFilter;
        }
        filterList.Add(saberStone);
        filterList.Add(knightStone);
        filterList.Add(berserkerStone);
        filterList.Add(casterStone);
        filterList.Add(hunterStone);
        #endregion

        //金币显示
        GoldCoin.text = CurrencyManager.Instance.GoldCoinDisplay();

    }
    //离开界面
    public void OnExiting()
    {
        gameObject.SetActive(false);
    }
    //界面暂停
    public void OnPausing()
    {
        gameObject.SetActive(false);
    }
    //界面唤醒
    public void OnResuming()
    {
        gameObject.SetActive(true);
        AllListCreate();
        //筛选重置
        for (int i = 0; i < filterList.Count; i++)
        {
            filterList[i].GetComponent<Toggle>().isOn = false;
        }
    }



    ////////////////////////////////////////////////////////
    //////////            UI绑定方法            ////////////
    ////////////////////////////////////////////////////////

    
    //系统操作区
    void CharacterEnter(PointerEventData eventData)
    {
        UIManager.Instance.PushUIStack(ConstData.UICharacterManagePrefab);
    }
    void StoreEnter(PointerEventData eventData)
    {

    }
    void SuperMarketEnter(PointerEventData eventData)
    {

    }
    void DrunkeryEnter(PointerEventData eventData)
    {

    }
    void SettingEnter(PointerEventData eventData)
    {

    }
    void BattleEnter(PointerEventData eventData)
    {

    }

    //控制区
    void ItemSelect(PointerEventData eventData)
    {
        itemG = eventData.pointerEnter.gameObject;
        itemID = Convert.ToInt32(itemG.name);
        switch (itemG.tag)
        {
            case ConstData.EquipmentType:
                #region 职业中文名获取
                string tempClass;
                if (itemG.GetComponent<BagItem>().mydata_equipt.equipmentClass == ConstData.Saber)
                {
                    tempClass = "剑士";
                }
                else if (itemG.GetComponent<BagItem>().mydata_equipt.equipmentClass == ConstData.Knight)
                {
                    tempClass = "骑士";
                }
                else if (itemG.GetComponent<BagItem>().mydata_equipt.equipmentClass == ConstData.Berserker)
                {
                    tempClass = "狂战士";
                }
                else if (itemG.GetComponent<BagItem>().mydata_equipt.equipmentClass == ConstData.Caster)
                {
                    tempClass = "魔法师";
                }
                else if (itemG.GetComponent<BagItem>().mydata_equipt.equipmentClass == ConstData.Hunter)
                {
                    tempClass = "猎人";
                }
                else
                {
                    tempClass = "无";
                }
                #endregion
                string[] oneText = new string[]
                    {
                        "[",
                        itemG.GetComponent<BagItem>().mydata_equipt.equipmentNmae,
                        "]  装备职业:",
                        tempClass,
                    };
                nameAndClass.text = StringSplicingTool.StringSplicing(oneText);
                string[] twoText = new string[]
                    {
                        "HP: ",
                        (itemG.GetComponent<BagItem>().mydata_equipt.equipment_HP).ToString(),
                        "\nAD:",
                        (itemG.GetComponent<BagItem>().mydata_equipt.equipment_AD).ToString(),
                        "  AP:",
                        (itemG.GetComponent<BagItem>().mydata_equipt.equipment_AP).ToString(),
                        "  DEF:",
                        (itemG.GetComponent<BagItem>().mydata_equipt.equipment_DEF).ToString(),
                        "  RES:",
                        (itemG.GetComponent<BagItem>().mydata_equipt.equipment_RES).ToString(),
                    };
                property.text = StringSplicingTool.StringSplicing(twoText);
                break;
            case ConstData.ItemType:
                #region 类型中文名获取
                string tempType;
                if (itemG.GetComponent<BagItem>().mydata_item.item_Type == ConstData.ListType_Consumable)
                {
                    tempType = "消耗品";
                }
                else if (itemG.GetComponent<BagItem>().mydata_item.item_Type == ConstData.ListType_Material)
                {
                    tempType = "材料";
                }
                else
                {
                    tempType = "装备";
                }
                #endregion
                string[] tempText = new string[]
                    {
                        "[",
                        itemG.GetComponent<BagItem>().mydata_item.item_Name,
                        "]  类型:",
                        tempType,
                    };
                nameAndClass.text = StringSplicingTool.StringSplicing(tempText);
                property.text = itemG.GetComponent<BagItem>().mydata_item.item_Description;
                break;
        }
    }

    //控制区附属
    void WeaponBagChange(PointerEventData eventData)
    {
        ItemListBG_WP.SetActive(true);
        ItemListBG_EQ.SetActive(false);
        ItemListBG_CO.SetActive(false);
        ItemListBG_MT.SetActive(false);
    }
    void EquipmentBagChange(PointerEventData eventData)
    {
        ItemListBG_WP.SetActive(false);
        ItemListBG_EQ.SetActive(true);
        ItemListBG_CO.SetActive(false);
        ItemListBG_MT.SetActive(false);
    }
    void ConsumableBagChange(PointerEventData eventData)
    {
        ItemListBG_WP.SetActive(false);
        ItemListBG_EQ.SetActive(false);
        ItemListBG_CO.SetActive(true);
        ItemListBG_MT.SetActive(false);
    }
    void MaterialBagChange(PointerEventData eventData)
    {
        ItemListBG_WP.SetActive(false);
        ItemListBG_EQ.SetActive(false);
        ItemListBG_CO.SetActive(false);
        ItemListBG_MT.SetActive(true);
    }

    //筛选
    void SaberFilter(PointerEventData eventData)
    {
        if (transform.Find(ConstData.Filter_StoneSaberTag).GetComponent<Toggle>().isOn == false)
        {
            AllListCreate();
        }
        else
        {
            LimitListCreate(ConstData.Saber);
        }
    }
    void KnightFilter(PointerEventData eventData)
    {
        if (transform.Find(ConstData.Filter_StoneKnightTag).GetComponent<Toggle>().isOn == false)
        {
            AllListCreate();
        }
        else
        {
            LimitListCreate(ConstData.Knight);
        }
    }
    void BerserkerFilter(PointerEventData eventData)
    {
        if (transform.Find(ConstData.Filter_StoneBerserkerTag).GetComponent<Toggle>().isOn == false)
        {
            AllListCreate();
        }
        else
        {
            LimitListCreate(ConstData.Berserker);
        }
    }
    void CasterFilter(PointerEventData eventData)
    {
        if (transform.Find(ConstData.Filter_StoneCasterTag).GetComponent<Toggle>().isOn == false)
        {
            AllListCreate();
        }
        else
        {
            LimitListCreate(ConstData.Caster);
        }
    }
    void HunterFilter(PointerEventData eventData)
    {
        if (transform.Find(ConstData.Filter_StoneHunterTag).GetComponent<Toggle>().isOn == false)
        {
            AllListCreate();
        }
        else
        {
            LimitListCreate(ConstData.Hunter);
        }
    }



    ////////////////////////////////////////////////////////////
    ////////////            非绑定方法           ///////////////
    ////////////////////////////////////////////////////////////



    /// <summary>
    /// 根据清空限制来清空所有道具区域和道具列表
    /// </summary>
    void ItemClear(string limitClear)
    {
        switch (limitClear)
        {
            case ConstData.All:
                ListClearTool(ConstData.WeaponBag);
                ListClearTool(ConstData.EquipmentBag);
                ListClearTool(ConstData.ConsumableBag);
                ListClearTool(ConstData.MaterialBag);
                break;
            case ConstData.Equipment:
                ListClearTool(ConstData.WeaponBag);
                ListClearTool(ConstData.EquipmentBag);
                break;
        }
    }
    /// <summary>
    /// 根据背包类型来清空区域和列表
    /// </summary>
    /// <param 背包名="bagName"></param>
    void ListClearTool(string bagName)
    {
        switch (bagName)
        {
            case ConstData.WeaponBag:
                for (int i = 0; i < itemList_WP.Count; i++)
                {
                    ObjectPoolManager.Instance.RecycleMyGameObject(itemList_WP[i]);
                }
                itemList_WP.Clear();
                for (int j = 0; j < null_itemList_WP.Count; j++)
                {
                    ObjectPoolManager.Instance.RecycleMyGameObject(null_itemList_WP[j]);
                }
                null_itemList_WP.Clear();
                break;
            case ConstData.EquipmentBag:
                for (int i = 0; i < itemList_EQ.Count; i++)
                {
                    ObjectPoolManager.Instance.RecycleMyGameObject(itemList_EQ[i]);
                }
                itemList_EQ.Clear();
                for (int j = 0; j < null_itemList_EQ.Count; j++)
                {
                    ObjectPoolManager.Instance.RecycleMyGameObject(null_itemList_EQ[j]);
                }
                null_itemList_EQ.Clear();
                break;
            case ConstData.ConsumableBag:
                for (int i = 0; i < itemList_CO.Count; i++)
                {
                    ObjectPoolManager.Instance.RecycleMyGameObject(itemList_CO[i]);
                }
                itemList_CO.Clear();
                for (int j = 0; j < null_itemList_CO.Count; j++)
                {
                    ObjectPoolManager.Instance.RecycleMyGameObject(null_itemList_CO[j]);
                }
                null_itemList_CO.Clear();
                break;
            case ConstData.MaterialBag:
                for (int i = 0; i < itemList_MT.Count; i++)
                {
                    ObjectPoolManager.Instance.RecycleMyGameObject(itemList_MT[i]);
                }
                itemList_MT.Clear();
                for (int j = 0; j < null_itemList_MT.Count; j++)
                {
                    ObjectPoolManager.Instance.RecycleMyGameObject(null_itemList_MT[j]);
                }
                null_itemList_MT.Clear();
                break;
        }
    }
    /// <summary>
    /// 生成所有背包
    /// </summary>
    void AllListCreate()
    {
        ItemClear(ConstData.All);
        WeaponListCreate(ConstData.All);
        EquipmentListCreate(ConstData.All);
        ConsumableListCreate();
        MaterialListCreate();
    }
    /// <summary>
    /// 仅生成武器和防具的背包
    /// </summary>
    void LimitListCreate(string filterMode)
    {
        ItemClear(ConstData.Equipment);
        WeaponListCreate(filterMode);
        EquipmentListCreate(filterMode);
    }
    /// <summary>
    /// 生成武器背包
    /// </summary>
    /// <param 筛选模式="filterMode"></param>
    void WeaponListCreate(string filterMode)
    {
        switch(filterMode)
        {
            case ConstData.All:
                for (int i = 0; i < SQLiteManager.Instance.bagDataSource.Count; i++)
                {
                    if (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon != 0)
                    {
                        //生成武器
                        GameObject wp = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                        //指定父物体
                        wp.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_WP).transform;
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
                            UISceneWidget EquipmentClick = UISceneWidget.Get(wp);
                            EquipmentClick.PointerClick += ItemSelect;
                        }
                        //获得数据
                        wp.transform.GetChild(0).GetComponent<BagItem>().GetData();
                        wp.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                        itemList_WP.Add(wp);
                    }
                }
                if (itemList_WP.Count < ConstData.GridCount)
                {
                    for (int i = 0; i < (ConstData.GridCount - itemList_WP.Count); i++)
                    {
                        GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                        //指定父物体
                        nullGrid.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_WP).transform;
                        //图的大小设置
                        nullGrid.transform.localScale = new Vector3(1, 1, 1);
                        null_itemList_WP.Add(nullGrid);
                    }
                }
                break;
            case ConstData.Saber:
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
                            wp.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_WP).transform;
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
                                UISceneWidget EquipmentClick = UISceneWidget.Get(wp);
                                EquipmentClick.PointerClick += ItemSelect;
                            }
                            //获得数据
                            wp.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            wp.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                            itemList_WP.Add(wp);
                        }
                    }
                }
                if (itemList_WP.Count < ConstData.GridCount)
                {
                    for (int i = 0; i < (ConstData.GridCount - itemList_WP.Count); i++)
                    {
                        GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                        //指定父物体
                        nullGrid.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_WP).transform;
                        //图的大小设置
                        nullGrid.transform.localScale = new Vector3(1, 1, 1);
                        null_itemList_WP.Add(nullGrid);
                    }
                }
                break;
            case ConstData.Knight:
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
                            wp.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_WP).transform;
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
                                UISceneWidget EquipmentClick = UISceneWidget.Get(wp);
                                EquipmentClick.PointerClick += ItemSelect;
                            }
                            //获得数据
                            wp.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            wp.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                            itemList_WP.Add(wp);
                        }
                    }
                }
                if (itemList_WP.Count < ConstData.GridCount)
                {
                    for (int i = 0; i < (ConstData.GridCount - itemList_WP.Count); i++)
                    {
                        GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                        //指定父物体
                        nullGrid.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_WP).transform;
                        //图的大小设置
                        nullGrid.transform.localScale = new Vector3(1, 1, 1);
                        null_itemList_WP.Add(nullGrid);
                    }
                }
                break;
            case ConstData.Berserker:
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
                            wp.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_WP).transform;
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
                                UISceneWidget EquipmentClick = UISceneWidget.Get(wp);
                                EquipmentClick.PointerClick += ItemSelect;
                            }
                            //获得数据
                            wp.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            wp.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                            itemList_WP.Add(wp);
                        }
                    }
                }
                if (itemList_WP.Count < ConstData.GridCount)
                {
                    for (int i = 0; i < (ConstData.GridCount - itemList_WP.Count); i++)
                    {
                        GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                        //指定父物体
                        nullGrid.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_WP).transform;
                        //图的大小设置
                        nullGrid.transform.localScale = new Vector3(1, 1, 1);
                        null_itemList_WP.Add(nullGrid);
                    }
                }
                break;
            case ConstData.Caster:
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
                            wp.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_WP).transform;
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
                                UISceneWidget EquipmentClick = UISceneWidget.Get(wp);
                                EquipmentClick.PointerClick += ItemSelect;
                            }
                            //获得数据
                            wp.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            wp.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                            itemList_WP.Add(wp);
                        }
                    }
                }
                if (itemList_WP.Count < ConstData.GridCount)
                {
                    for (int i = 0; i < (ConstData.GridCount - itemList_WP.Count); i++)
                    {
                        GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                        //指定父物体
                        nullGrid.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_WP).transform;
                        //图的大小设置
                        nullGrid.transform.localScale = new Vector3(1, 1, 1);
                        null_itemList_WP.Add(nullGrid);
                    }
                }
                break;
            case ConstData.Hunter:
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
                            wp.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_WP).transform;
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
                                UISceneWidget EquipmentClick = UISceneWidget.Get(wp);
                                EquipmentClick.PointerClick += ItemSelect;
                            }
                            //获得数据
                            wp.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            wp.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                            itemList_WP.Add(wp);
                        }
                    }
                }
                if (itemList_WP.Count < ConstData.GridCount)
                {
                    for (int i = 0; i < (ConstData.GridCount - itemList_WP.Count); i++)
                    {
                        GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                        //指定父物体
                        nullGrid.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_WP).transform;
                        //图的大小设置
                        nullGrid.transform.localScale = new Vector3(1, 1, 1);
                        null_itemList_WP.Add(nullGrid);
                    }
                }
                break;
        }
    }
    /// <summary>
    /// 生成防具背包
    /// </summary>
    /// <param 筛选模式="filterMode"></param>
    void EquipmentListCreate(string filterMode)
    {
        switch (filterMode)
        {
            case ConstData.All:
                for (int i = 0; i < SQLiteManager.Instance.bagDataSource.Count; i++)
                {
                    if (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment != 0)
                    {
                        //生成防具
                        GameObject eq = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                        //指定父物体
                        eq.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_EQ).transform;
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
                            UISceneWidget EquipmentClick = UISceneWidget.Get(eq);
                            EquipmentClick.PointerClick += ItemSelect;
                        }
                        //获得数据
                        eq.transform.GetChild(0).GetComponent<BagItem>().GetData();
                        eq.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                        itemList_EQ.Add(eq);
                    }
                }
                if (itemList_EQ.Count < ConstData.GridCount)
                {
                    for (int i = 0; i < (ConstData.GridCount - itemList_EQ.Count); i++)
                    {
                        GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                        //指定父物体
                        nullGrid.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_EQ).transform;
                        //图的大小设置
                        nullGrid.transform.localScale = new Vector3(1, 1, 1);
                        null_itemList_EQ.Add(nullGrid);
                    }
                }
                break;
            case ConstData.Saber:
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
                            eq.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_EQ).transform;
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
                                UISceneWidget EquipmentClick = UISceneWidget.Get(eq);
                                EquipmentClick.PointerClick += ItemSelect;
                            }
                            //获得数据
                            eq.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            eq.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                            itemList_EQ.Add(eq);
                        }
                    }
                }
                if (itemList_EQ.Count < ConstData.GridCount)
                {
                    for (int i = 0; i < (ConstData.GridCount - itemList_EQ.Count); i++)
                    {
                        GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                        //指定父物体
                        nullGrid.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_EQ).transform;
                        //图的大小设置
                        nullGrid.transform.localScale = new Vector3(1, 1, 1);
                        null_itemList_EQ.Add(nullGrid);
                    }
                }
                break;
            case ConstData.Knight:
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
                            eq.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_EQ).transform;
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
                                UISceneWidget EquipmentClick = UISceneWidget.Get(eq);
                                EquipmentClick.PointerClick += ItemSelect;
                            }
                            //获得数据
                            eq.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            eq.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                            itemList_EQ.Add(eq);
                        }
                    }
                }
                if (itemList_EQ.Count < ConstData.GridCount)
                {
                    for (int i = 0; i < (ConstData.GridCount - itemList_EQ.Count); i++)
                    {
                        GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                        //指定父物体
                        nullGrid.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_EQ).transform;
                        //图的大小设置
                        nullGrid.transform.localScale = new Vector3(1, 1, 1);
                        null_itemList_EQ.Add(nullGrid);
                    }
                }
                break;
            case ConstData.Berserker:
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
                            eq.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_EQ).transform;
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
                                UISceneWidget EquipmentClick = UISceneWidget.Get(eq);
                                EquipmentClick.PointerClick += ItemSelect;
                            }
                            //获得数据
                            eq.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            eq.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                            itemList_EQ.Add(eq);
                        }
                    }
                }
                if (itemList_EQ.Count < ConstData.GridCount)
                {
                    for (int i = 0; i < (ConstData.GridCount - itemList_EQ.Count); i++)
                    {
                        GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                        //指定父物体
                        nullGrid.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_EQ).transform;
                        //图的大小设置
                        nullGrid.transform.localScale = new Vector3(1, 1, 1);
                        null_itemList_EQ.Add(nullGrid);
                    }
                }
                break;
            case ConstData.Caster:
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
                            eq.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_EQ).transform;
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
                                UISceneWidget EquipmentClick = UISceneWidget.Get(eq);
                                EquipmentClick.PointerClick += ItemSelect;
                            }
                            //获得数据
                            eq.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            eq.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                            itemList_EQ.Add(eq);
                        }
                    }
                }
                if (itemList_EQ.Count < ConstData.GridCount)
                {
                    for (int i = 0; i < (ConstData.GridCount - itemList_EQ.Count); i++)
                    {
                        GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                        //指定父物体
                        nullGrid.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_EQ).transform;
                        //图的大小设置
                        nullGrid.transform.localScale = new Vector3(1, 1, 1);
                        null_itemList_EQ.Add(nullGrid);
                    }
                }
                break;
            case ConstData.Hunter:
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
                            eq.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_EQ).transform;
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
                                UISceneWidget EquipmentClick = UISceneWidget.Get(eq);
                                EquipmentClick.PointerClick += ItemSelect;
                            }
                            //获得数据
                            eq.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            eq.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                            itemList_EQ.Add(eq);
                        }
                    }
                }
                if (itemList_EQ.Count < ConstData.GridCount)
                {
                    for (int i = 0; i < (ConstData.GridCount - itemList_EQ.Count); i++)
                    {
                        GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                        //指定父物体
                        nullGrid.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_EQ).transform;
                        //图的大小设置
                        nullGrid.transform.localScale = new Vector3(1, 1, 1);
                        null_itemList_EQ.Add(nullGrid);
                    }
                }
                break;
        }
    }
    /// <summary>
    /// 生成消耗品背包
    /// </summary>
    void ConsumableListCreate()
    {
        for (int i = 0; i < SQLiteManager.Instance.bagDataSource.Count; i++)
        {
            if (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Consumable != 0)
            {
                //生成消耗品
                GameObject co = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                //指定父物体
                co.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_CO).transform;
                //消耗品的大小、图片和标签设置
                co.transform.localScale = new Vector3(1, 1, 1);
                co.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                    ((SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Consumable).ToString());
                co.transform.GetChild(0).tag = ConstData.ItemType;
                co.transform.GetChild(0).name = (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Consumable).ToString();
                //添加数据脚本
                if (co.transform.GetChild(0).GetComponent<BagItem>() == null)
                {
                    co.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                    UISceneWidget ItemClick = UISceneWidget.Get(co);
                    ItemClick.PointerClick += ItemSelect;
                }
                //获得数据
                co.transform.GetChild(0).GetComponent<BagItem>().GetData();
                co.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                itemList_CO.Add(co);
            }
        }
        if (itemList_CO.Count < ConstData.GridCount)
        {
            for (int i = 0; i < (ConstData.GridCount - itemList_CO.Count); i++)
            {
                GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                //指定父物体
                nullGrid.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_CO).transform;
                //图的大小设置
                nullGrid.transform.localScale = new Vector3(1, 1, 1);
                null_itemList_CO.Add(nullGrid);
            }
        }
    }
    /// <summary>
    /// 生成材料背包
    /// </summary>
    void MaterialListCreate()
    {
        for (int i = 0; i < SQLiteManager.Instance.bagDataSource.Count; i++)
        {
            if (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Material != 0)
            {
                //生成消耗品
                GameObject mt = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                //指定父物体
                mt.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_MT).transform;
                //消耗品的大小、图片和标签设置
                mt.transform.localScale = new Vector3(1, 1, 1);
                mt.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                    ((SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Material).ToString());
                mt.transform.GetChild(0).tag = ConstData.ItemType;
                mt.transform.GetChild(0).name = (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Material).ToString();
                //添加数据脚本
                if (mt.transform.GetChild(0).GetComponent<BagItem>() == null)
                {
                    mt.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                    UISceneWidget ItemClick = UISceneWidget.Get(mt);
                    ItemClick.PointerClick += ItemSelect;
                }
                //获得数据
                mt.transform.GetChild(0).GetComponent<BagItem>().GetData();
                mt.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                itemList_MT.Add(mt);
            }
        }
        if (itemList_MT.Count < ConstData.GridCount)
        {
            for (int i = 0; i < (ConstData.GridCount - itemList_MT.Count); i++)
            {
                GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                //指定父物体
                nullGrid.transform.parent = transform.Find(ConstData.ControllerArea_ItemListBG_MT).transform;
                //图的大小设置
                nullGrid.transform.localScale = new Vector3(1, 1, 1);
                null_itemList_MT.Add(nullGrid);
            }
        }
    }

}
