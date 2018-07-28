using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIStore : MonoBehaviour, IUIBase
{
    //预制体外：游戏画面区域
    GameObject GameArea;
    //选中的道具
    GameObject itemG;
    int itemID;
    GameObject itemHalo;
    GameObject itemHaloPrefabs;
    //显示信息
    Text goodsIntroduction;
    Text confirmMessage;
    Text GoldCoin;
    //装备空格预制体
    GameObject itemGrid;
    //装备格带图标预制体
    GameObject itemBar;
    //是否处于出售页面
    bool isSell;
    //当前筛选模式
    int filterID;

    //带道具装备格对象表（武器）
    List<GameObject> itemList_WP;
    //空装备格对象表（武器）
    List<GameObject> null_itemList_WP;
    //带道具装备格对象表（防具）
    List<GameObject> itemList_EQ;
    //空装备格对象表（防具）
    List<GameObject> null_itemList_EQ;
    //带道具装备格对象表（消耗品）
    List<GameObject> itemList_CO;
    //空装备格对象表（消耗品）
    List<GameObject> null_itemList_CO;
    //带道具装备格对象表（材料）
    List<GameObject> itemList_MT;
    //空装备格对象表（材料）
    List<GameObject> null_itemList_MT;

    //带道具商店格对象表（武器）
    List<GameObject> storeList_WP;
    //空商店格对象表（武器）
    List<GameObject> null_storeList_WP;
    //带道具商店格对象表（防具）
    List<GameObject> storeList_EQ;
    //空商店格对象表（防具）
    List<GameObject> null_storeList_EQ;
    //带道具商店格对象表（消耗品）
    List<GameObject> storeList_CO;
    //空商店格对象表（消耗品）
    List<GameObject> null_storeList_CO;
    //带道具商店格对象表（材料）
    List<GameObject> storeList_MT;
    //空商店格对象表（材料）
    List<GameObject> null_storeList_MT;

    //筛选列表
    List<GameObject> filterList;
    //出售武器列表
    int[] weaponSellList;
    //出售防具列表
    int[] equipmentSellList;
    //出售消耗品列表
    int[] consumableSellList;
    //出售材料列表
    int[] materialSellList;

    //需要操作的界面
    GameObject BuyIcon;
    GameObject SellIcon;
    GameObject mainCityIcon;
    //--------------------------
    GameObject StoreListBG_WP;
    GameObject StoreListBG_EQ;
    GameObject StoreListBG_CO;
    GameObject StoreListBG_MT;
    GameObject ItemListBG_WP;
    GameObject ItemListBG_EQ;
    GameObject ItemListBG_CO;
    GameObject ItemListBG_MT;
    GameObject storeGridParent_WP;
    GameObject storeGridParent_EQ;
    GameObject storeGridParent_CO;
    GameObject storeGridParent_MT;
    GameObject itemGridParent_WP;
    GameObject itemGridParent_EQ;
    GameObject itemGridParent_CO;
    GameObject itemGridParent_MT;
    //--------------------------
    GameObject weaponStore;
    GameObject equipmentStore;
    GameObject consumableStore;
    GameObject materialStore;
    //--------------------------
    GameObject saberStone;
    GameObject knightStone;
    GameObject berserkerStone;
    GameObject casterStone;
    GameObject hunterStone;
    //--------------------------
    GameObject confirmButton;
    GameObject confirmFrame;
    GameObject confirmOK;
    GameObject cancelNO;

    private void Awake()
    {
        #region 获取UI组件和对象
        goodsIntroduction = transform.Find(ConstData.GameArea_GoodsIntro).GetComponent<Text>();
        confirmMessage = transform.Find(ConstData.ConfirmFrame_ContentText).GetComponent<Text>();
        GoldCoin = transform.Find(ConstData.GameArea_GoldCoin).GetComponent<Text>();
        GameArea = ResourcesManager.Instance.FindUIPrefab(ConstData.UIStore_GameArea);
        itemHaloPrefabs = ResourcesManager.Instance.FindUIPrefab(ConstData.pitchOn);
        itemGrid = ResourcesManager.Instance.FindUIPrefab(ConstData.Grid).gameObject;
        itemBar = ResourcesManager.Instance.FindUIPrefab(ConstData.GridEx).gameObject;
        BuyIcon = transform.Find(ConstData.SystemArea_BuyIcon).gameObject;
        SellIcon = transform.Find(ConstData.SystemArea_SellIcon).gameObject;
        mainCityIcon = transform.Find(ConstData.SystemArea_MainCityIcon).gameObject;
        StoreListBG_WP = transform.Find(ConstData.ControllerArea_StoreListBG_WP).gameObject;
        StoreListBG_EQ = transform.Find(ConstData.ControllerArea_StoreListBG_EQ).gameObject;
        StoreListBG_CO = transform.Find(ConstData.ControllerArea_StoreListBG_CO).gameObject;
        StoreListBG_MT = transform.Find(ConstData.ControllerArea_StoreListBG_MT).gameObject;
        ItemListBG_WP = transform.Find(ConstData.ControllerArea_ItemListBG_WP).gameObject;
        ItemListBG_EQ = transform.Find(ConstData.ControllerArea_ItemListBG_EQ).gameObject;
        ItemListBG_CO = transform.Find(ConstData.ControllerArea_ItemListBG_CO).gameObject;
        ItemListBG_MT = transform.Find(ConstData.ControllerArea_ItemListBG_MT).gameObject;
        storeGridParent_WP = transform.Find(ConstData.ControllerArea_StoreListContent_WP).gameObject;
        storeGridParent_EQ = transform.Find(ConstData.ControllerArea_StoreListContent_EQ).gameObject;
        storeGridParent_CO = transform.Find(ConstData.ControllerArea_StoreListContent_CO).gameObject;
        storeGridParent_MT = transform.Find(ConstData.ControllerArea_StoreListContent_MT).gameObject;
        itemGridParent_WP = ItemListBG_WP;
        itemGridParent_EQ = ItemListBG_EQ;
        itemGridParent_CO = ItemListBG_CO;
        itemGridParent_MT = ItemListBG_MT;
        weaponStore = transform.Find(ConstData.ControllerExArea_WeaponStore).GetChild(0).gameObject;
        equipmentStore = transform.Find(ConstData.ControllerExArea_EquipmentStore).GetChild(0).gameObject;
        consumableStore = transform.Find(ConstData.ControllerExArea_ConsumableStore).GetChild(0).gameObject;
        materialStore = transform.Find(ConstData.ControllerExArea_MaterialStore).GetChild(0).gameObject;
        saberStone = transform.Find(ConstData.Filter_StoneSaberTag).gameObject;
        knightStone = transform.Find(ConstData.Filter_StoneKnightTag).gameObject;
        berserkerStone = transform.Find(ConstData.Filter_StoneBerserkerTag).gameObject;
        casterStone = transform.Find(ConstData.Filter_StoneCasterTag).gameObject;
        hunterStone = transform.Find(ConstData.Filter_StoneHunterTag).gameObject;
        confirmButton = transform.Find(ConstData.GameArea_BuyConfirm).gameObject;
        confirmFrame = transform.Find(ConstData.ConfirmFrame).gameObject;
        confirmOK = confirmFrame.transform.GetChild(2).gameObject;
        cancelNO = confirmFrame.transform.GetChild(3).gameObject;
        #endregion
    }

    //进入界面
    public void OnEntering()
    {
        gameObject.SetActive(true);

        #region 初始化数据
        itemG = null;
        itemID = 0;
        isSell = false;
        filterID = 0;
        itemList_WP = new List<GameObject>();
        null_itemList_WP = new List<GameObject>();
        itemList_EQ = new List<GameObject>();
        null_itemList_EQ = new List<GameObject>();
        itemList_CO = new List<GameObject>();
        null_itemList_CO = new List<GameObject>();
        itemList_MT = new List<GameObject>();
        null_itemList_MT = new List<GameObject>();
        storeList_WP = new List<GameObject>();
        null_storeList_WP = new List<GameObject>();
        storeList_EQ = new List<GameObject>();
        null_storeList_EQ = new List<GameObject>();
        storeList_CO = new List<GameObject>();
        null_storeList_CO = new List<GameObject>();
        storeList_MT = new List<GameObject>();
        null_storeList_MT = new List<GameObject>();
        filterList = new List<GameObject>();
        itemList_WP.Clear();
        null_itemList_WP.Clear();
        itemList_EQ.Clear();
        null_itemList_EQ.Clear();
        itemList_CO.Clear();
        null_itemList_CO.Clear();
        itemList_MT.Clear();
        null_itemList_MT.Clear();
        storeList_WP.Clear();
        null_storeList_WP.Clear();
        storeList_EQ.Clear();
        null_storeList_EQ.Clear();
        storeList_CO.Clear();
        null_storeList_CO.Clear();
        storeList_MT.Clear();
        null_storeList_MT.Clear();
        filterList.Clear();

        weaponSellList = new int[]
            {
                2001,2002,2003,2004,2005,2006,2007,2016,2017,2018,2019,2020,2021,2022,2028,2029,2030,2031,2032,2033,2034,2040,2041,2042,2043,2044,2045,2046,2052,2053,2054,2055,2056,2057,2058
            };
        equipmentSellList = new int[]
            {
                2101,2102,2103,2104,2106,2107,2108,2109,2111,2112,2113,2114,2116,2117,2118,2119,2121,2122,2123,2124
            };
        consumableSellList = new int[]
            {
                2201,2202,2203,2204,2205
            };
        materialSellList = new int[]
            {
                2301,2302,2303,2304,2305
            };
        //生成画面区
        GameArea = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindUIPrefab(ConstData.UIStore_GameArea));
        GameArea.transform.position = ResourcesManager.Instance.FindUIPrefab(ConstData.UIStore_GameArea).transform.position;
        //金币显示
        GoldCoin.text = CurrencyManager.Instance.GoldCoinDisplay();
        #endregion

        #region 系统功能区
        if (BuyIcon.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget BuyIconClick = UISceneWidget.Get(BuyIcon);
            BuyIconClick.PointerClick += StoreBuyEnter;
        }
        else
        {
            BuyIcon.GetComponent<UISceneWidget>().PointerClick += StoreBuyEnter;
        }
        BuyIcon.GetComponent<Toggle>().isOn = true;
        if (SellIcon.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget SellIconClick = UISceneWidget.Get(SellIcon);
            SellIconClick.PointerClick += StoreSellEnter;
        }
        else
        {
            SellIcon.GetComponent<UISceneWidget>().PointerClick += StoreSellEnter;
        }
        SellIcon.GetComponent<Toggle>().isOn = false;
        if (mainCityIcon.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget mainCityIconClick = UISceneWidget.Get(mainCityIcon);
            mainCityIconClick.PointerClick += ReturnMainCity;
        }
        else
        {
            mainCityIcon.GetComponent<UISceneWidget>().PointerClick += ReturnMainCity;
        }
        mainCityIcon.GetComponent<Toggle>().isOn = false;
        #endregion

        #region 控制区
        AllListCreate();
        StoreListBG_WP.SetActive(true);
        StoreListBG_EQ.SetActive(false);
        StoreListBG_CO.SetActive(false);
        StoreListBG_MT.SetActive(false);
        ItemListBG_WP.SetActive(false);
        ItemListBG_EQ.SetActive(false);
        ItemListBG_CO.SetActive(false);
        ItemListBG_MT.SetActive(false);
        if (confirmButton.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget confirmButtonClick = UISceneWidget.Get(confirmButton);
            confirmButtonClick.PointerClick += BuyOrSellConfirm;
        }
        else
        {
            confirmButton.GetComponent<UISceneWidget>().PointerClick += BuyOrSellConfirm;
        }
        #endregion

        #region 控制附属区
        if (weaponStore.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget weaponStoreClick = UISceneWidget.Get(weaponStore);
            weaponStoreClick.PointerClick += WeaponStore;
        }
        else
        {
            weaponStore.GetComponent<UISceneWidget>().PointerClick += WeaponStore;
        }
        if (equipmentStore.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget equipmentStoreClick = UISceneWidget.Get(equipmentStore);
            equipmentStoreClick.PointerClick += EquipmentStore;
        }
        else
        {
            equipmentStore.GetComponent<UISceneWidget>().PointerClick += EquipmentStore;
        }
        if (consumableStore.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget consumableStoreClick = UISceneWidget.Get(consumableStore);
            consumableStoreClick.PointerClick += ConsumableStore;
        }
        else
        {
            consumableStore.GetComponent<UISceneWidget>().PointerClick += ConsumableStore;
        }
        if (materialStore.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget materialStoreClick = UISceneWidget.Get(materialStore);
            materialStoreClick.PointerClick += MaterialStore;
        }
        else
        {
            materialStore.GetComponent<UISceneWidget>().PointerClick += MaterialStore;
        }
        weaponStore.GetComponent<Toggle>().isOn = true;
        equipmentStore.GetComponent<Toggle>().isOn = false;
        consumableStore.GetComponent<Toggle>().isOn = false;
        materialStore.GetComponent<Toggle>().isOn = false;
        #endregion

        #region 筛选区
        if (saberStone.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget saberStoneClick = UISceneWidget.Get(saberStone);
            saberStoneClick.PointerClick += FilterSaber;
        }
        else
        {
            saberStone.GetComponent<UISceneWidget>().PointerClick += FilterSaber;
        }
        if (knightStone.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget knightStoneClick = UISceneWidget.Get(knightStone);
            knightStoneClick.PointerClick += FilterKnight;
        }
        else
        {
            knightStone.GetComponent<UISceneWidget>().PointerClick += FilterKnight;
        }
        if (berserkerStone.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget berserkerStoneClick = UISceneWidget.Get(berserkerStone);
            berserkerStoneClick.PointerClick += FilterBerserker;
        }
        else
        {
            berserkerStone.GetComponent<UISceneWidget>().PointerClick += FilterBerserker;
        }
        if (casterStone.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget casterStoneClick = UISceneWidget.Get(casterStone);
            casterStoneClick.PointerClick += FilterCaster;
        }
        else
        {
            casterStone.GetComponent<UISceneWidget>().PointerClick += FilterCaster;
        }
        if (hunterStone.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget hunterStoneClick = UISceneWidget.Get(hunterStone);
            hunterStoneClick.PointerClick += FilterHunter;
        }
        else
        {
            hunterStone.GetComponent<UISceneWidget>().PointerClick += FilterHunter;
        }
        filterList.Add(saberStone);
        filterList.Add(knightStone);
        filterList.Add(berserkerStone);
        filterList.Add(casterStone);
        filterList.Add(hunterStone);
        for (int i = 0; i < filterList.Count; i++)
        {
            filterList[i].GetComponent<Toggle>().isOn = false;
        }
        #endregion
        
    }
    //退出界面
    public void OnExiting()
    {
        if (itemHalo != null)
        {
            ObjectPoolManager.Instance.RecycleMyGameObject(itemHalo);
        }
        //清空所有道具
        ItemClear(ConstData.All);
        //回收GameArea
        ObjectPoolManager.Instance.RecycleMyGameObject(GameArea);
        //筛选区解绑方法
        saberStone.GetComponent<UISceneWidget>().PointerClick -= FilterSaber;
        knightStone.GetComponent<UISceneWidget>().PointerClick -= FilterKnight;
        berserkerStone.GetComponent<UISceneWidget>().PointerClick -= FilterBerserker;
        casterStone.GetComponent<UISceneWidget>().PointerClick -= FilterCaster;
        hunterStone.GetComponent<UISceneWidget>().PointerClick -= FilterHunter;
        //控制区解绑
        confirmButton.GetComponent<UISceneWidget>().PointerClick -= BuyOrSellConfirm;
        //控制附属区解绑
        weaponStore.GetComponent<UISceneWidget>().PointerClick -= WeaponStore;
        equipmentStore.GetComponent<UISceneWidget>().PointerClick -= EquipmentStore;
        consumableStore.GetComponent<UISceneWidget>().PointerClick -= ConsumableStore;
        materialStore.GetComponent<UISceneWidget>().PointerClick -= MaterialStore;
        //系统区解绑
        BuyIcon.GetComponent<UISceneWidget>().PointerClick -= StoreBuyEnter;
        SellIcon.GetComponent<UISceneWidget>().PointerClick -= StoreSellEnter;
        mainCityIcon.GetComponent<UISceneWidget>().PointerClick -= ReturnMainCity;
        //关闭界面
        gameObject.SetActive(false);
    }
    //界面暂停
    public void OnPausing()
    {
        throw new System.NotImplementedException();
    }
    //界面唤醒
    public void OnResuming()
    {
        throw new System.NotImplementedException();
    }



    ////////////////////////////////////////////////////////
    //////////            UI绑定方法            ////////////
    ////////////////////////////////////////////////////////

    
    //系统操作区
    void StoreBuyEnter(PointerEventData eventData)
    {
        if (isSell == true)
        {
            isSell = false;
            goodsIntroduction.text = "欢迎光临，请选择需要购买的物品。";
            if (itemHalo != null)
            {
                ObjectPoolManager.Instance.RecycleMyGameObject(itemHalo);
            }
            itemG = null;
            itemID = 0;
            //控制区
            StoreListBG_WP.SetActive(true);
            StoreListBG_EQ.SetActive(false);
            StoreListBG_CO.SetActive(false);
            StoreListBG_MT.SetActive(false);
            ItemListBG_WP.SetActive(false);
            ItemListBG_EQ.SetActive(false);
            ItemListBG_CO.SetActive(false);
            ItemListBG_MT.SetActive(false);
            //控制区附属
            weaponStore.GetComponent<Toggle>().isOn = true;
            equipmentStore.GetComponent<Toggle>().isOn = false;
            consumableStore.GetComponent<Toggle>().isOn = false;
            materialStore.GetComponent<Toggle>().isOn = false;
            //筛选区
            //for (int i = 0; i < filterList.Count; i++)
            //{
            //    filterList[i].GetComponent<Toggle>().isOn = false;
            //}
            //信息框和确认框隐藏
        }
    }
    void StoreSellEnter(PointerEventData eventData)
    {
        if (isSell == false)
        {
            isSell = true;
            goodsIntroduction.text = "请选择需要出售的物品。";
            if (itemHalo != null)
            {
                ObjectPoolManager.Instance.RecycleMyGameObject(itemHalo);
            }
            itemG = null;
            itemID = 0;
            //控制区
            StoreListBG_WP.SetActive(false);
            StoreListBG_EQ.SetActive(false);
            StoreListBG_CO.SetActive(false);
            StoreListBG_MT.SetActive(false);
            ItemListBG_WP.SetActive(true);
            ItemListBG_EQ.SetActive(false);
            ItemListBG_CO.SetActive(false);
            ItemListBG_MT.SetActive(false);
            //控制区附属
            weaponStore.GetComponent<Toggle>().isOn = true;
            equipmentStore.GetComponent<Toggle>().isOn = false;
            consumableStore.GetComponent<Toggle>().isOn = false;
            materialStore.GetComponent<Toggle>().isOn = false;
            //筛选区
            //for (int i = 0; i < filterList.Count; i++)
            //{
            //    filterList[i].GetComponent<Toggle>().isOn = false;
            //}
            //信息框和确认框隐藏
        }
    }
    void ReturnMainCity(PointerEventData eventData)
    {
        //保存背包等数据库内容
        //切换回主城
        UIManager.Instance.PopUIStack();
    }

    //控制区
    void GoodsAndItemMessage(PointerEventData eventData)
    {
        itemG = eventData.pointerEnter.gameObject;
        itemID = int.Parse(itemG.name);
        switch (isSell)
        {
            case true:
                if (itemG.tag == ConstData.EquipmentType)
                {
                    string price = StringSplicingTool.StringSplicing
                        (((int)(itemG.GetComponent<BagItem>().mydata_equipt.equipmentPrice * 0.1f)).ToString());
                    string classCN = "";
                    switch (itemG.GetComponent<BagItem>().mydata_equipt.equipmentClass)
                    {
                        case ConstData.Saber:
                            classCN = "剑士装备";
                            break;
                        case ConstData.Knight:
                            classCN = "骑士装备";
                            break;
                        case ConstData.Berserker:
                            classCN = "狂战士装备";
                            break;
                        case ConstData.Caster:
                            classCN = "魔法师装备";
                            break;
                        case ConstData.Hunter:
                            classCN = "猎人装备";
                            break;
                    }
                    string[] tempMSG = new string[]
                        {
                            "[",
                            itemG.GetComponent<BagItem>().mydata_equipt.equipmentNmae,
                            "]  ",
                            classCN,
                            "  回收价:",
                            price,
                            "\nHP:",
                            itemG.GetComponent<BagItem>().mydata_equipt.equipment_HP.ToString(),
                            "  AD:",
                            itemG.GetComponent<BagItem>().mydata_equipt.equipment_AD.ToString(),
                            "  AP:",
                            itemG.GetComponent<BagItem>().mydata_equipt.equipment_AP.ToString(),
                            "  DEF:",
                            itemG.GetComponent<BagItem>().mydata_equipt.equipment_DEF.ToString(),
                            "  RES:",
                            itemG.GetComponent<BagItem>().mydata_equipt.equipment_RES.ToString(),
                            "\n[确认出售点击右方按钮]",
                        };
                    goodsIntroduction.text = StringSplicingTool.StringSplicing(tempMSG);
                }
                else
                {
                    string price = StringSplicingTool.StringSplicing
                        (((int)(itemG.GetComponent<BagItem>().mydata_item.item_Price * 0.5f)).ToString());
                    string[] tempMSG = new string[]
                        {
                            "[",
                            itemG.GetComponent<BagItem>().mydata_item.item_Name,
                            "]  ",
                            "  回收价:",
                            price,
                            "\n",
                            itemG.GetComponent<BagItem>().mydata_item.item_Description,
                            "\n[确认出售点击右方按钮]",
                        };
                    goodsIntroduction.text = StringSplicingTool.StringSplicing(tempMSG);
                }
                break;
            case false:
                if (itemG.tag == ConstData.EquipmentType)
                {
                    string price = StringSplicingTool.StringSplicing
                        (((int)(itemG.GetComponent<BagItem>().mydata_equipt.equipmentPrice)).ToString());
                    string classCN = "";
                    switch (itemG.GetComponent<BagItem>().mydata_equipt.equipmentClass)
                    {
                        case ConstData.Saber:
                            classCN = "剑士装备";
                            break;
                        case ConstData.Knight:
                            classCN = "骑士装备";
                            break;
                        case ConstData.Berserker:
                            classCN = "狂战士装备";
                            break;
                        case ConstData.Caster:
                            classCN = "魔法师装备";
                            break;
                        case ConstData.Hunter:
                            classCN = "猎人装备";
                            break;
                    }
                    string[] tempMSG = new string[]
                        {
                            "[",
                            itemG.GetComponent<BagItem>().mydata_equipt.equipmentNmae,
                            "]  ",
                            classCN,
                            "  价格:",
                            price,
                            "\nHP:",
                            itemG.GetComponent<BagItem>().mydata_equipt.equipment_HP.ToString(),
                            "  AD:",
                            itemG.GetComponent<BagItem>().mydata_equipt.equipment_AD.ToString(),
                            "  AP:",
                            itemG.GetComponent<BagItem>().mydata_equipt.equipment_AP.ToString(),
                            "  DEF:",
                            itemG.GetComponent<BagItem>().mydata_equipt.equipment_DEF.ToString(),
                            "  RES:",
                            itemG.GetComponent<BagItem>().mydata_equipt.equipment_RES.ToString(),
                            "\n[确认购买点击右方按钮]",
                        };
                    goodsIntroduction.text = StringSplicingTool.StringSplicing(tempMSG);
                }
                else
                {
                    string price = StringSplicingTool.StringSplicing
                        (((int)(itemG.GetComponent<BagItem>().mydata_item.item_Price)).ToString());
                    string[] tempMSG = new string[]
                        {
                            "[",
                            itemG.GetComponent<BagItem>().mydata_item.item_Name,
                            "]  ",
                            "  价格:",
                            price,
                            "\n",
                            itemG.GetComponent<BagItem>().mydata_item.item_Description,
                            "\n[确认购买点击右方按钮]",
                        };
                    goodsIntroduction.text = StringSplicingTool.StringSplicing(tempMSG);
                }
                break;
        }
        if (itemHalo != null)
        {
            ObjectPoolManager.Instance.RecycleMyGameObject(itemHalo);
        }
        //生成选中光圈
        itemHalo = ObjectPoolManager.Instance.InstantiateMyGameObject(itemHaloPrefabs);
        //改变父物体到选中的目标
        itemHalo.transform.parent = itemG.transform.parent;
        itemHalo.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        itemHalo.transform.localScale = new Vector3(1, 1, 1);
    }
    void BuyOrSellConfirm(PointerEventData eventData)
    {
        if (itemG != null && itemID != 0)
        {
            confirmFrame.SetActive(true);
            switch (isSell)
            {
                case true:
                    if (confirmOK.GetComponent<UISceneWidget>() == null)
                    {
                        UISceneWidget confirmOKClick = UISceneWidget.Get(confirmOK);
                        confirmOKClick.PointerClick += ItemSell;
                    }
                    else
                    {
                        confirmOK.GetComponent<UISceneWidget>().PointerClick += ItemSell;
                    }
                    if (cancelNO.GetComponent<UISceneWidget>() == null)
                    {
                        UISceneWidget cancelNOClick = UISceneWidget.Get(cancelNO);
                        cancelNOClick.PointerClick += Cancel;
                    }
                    else
                    {
                        cancelNO.GetComponent<UISceneWidget>().PointerClick += Cancel;
                    }
                    if (itemG.tag == ConstData.EquipmentType)
                    {
                        string[] tempStr = new string[]
                        {
                        "确定出售",
                        "[",
                        itemG.GetComponent<BagItem>().mydata_equipt.equipmentNmae,
                        "]吗？",
                        };
                        confirmMessage.text = StringSplicingTool.StringSplicing(tempStr);
                    }
                    else
                    {
                        string[] tempStr = new string[]
                        {
                        "确定出售",
                        "[",
                        itemG.GetComponent<BagItem>().mydata_item.item_Name,
                        "]吗？",
                        };
                        confirmMessage.text = StringSplicingTool.StringSplicing(tempStr);
                    }
                    break;
                case false:
                    if (confirmOK.GetComponent<UISceneWidget>() == null)
                    {
                        UISceneWidget confirmOKClick = UISceneWidget.Get(confirmOK);
                        confirmOKClick.PointerClick += GoodsBuy;
                    }
                    else
                    {
                        confirmOK.GetComponent<UISceneWidget>().PointerClick += GoodsBuy;
                    }
                    if (cancelNO.GetComponent<UISceneWidget>() == null)
                    {
                        UISceneWidget cancelNOClick = UISceneWidget.Get(cancelNO);
                        cancelNOClick.PointerClick += Cancel;
                    }
                    else
                    {
                        cancelNO.GetComponent<UISceneWidget>().PointerClick += Cancel;
                    }
                    if (itemG.tag == ConstData.EquipmentType)
                    {
                        string[] tempStr = new string[]
                        {
                        "确定购买",
                        "[",
                        itemG.GetComponent<BagItem>().mydata_equipt.equipmentNmae,
                        "]吗？",
                        };
                        confirmMessage.text = StringSplicingTool.StringSplicing(tempStr);
                    }
                    else
                    {
                        string[] tempStr = new string[]
                        {
                        "确定购买",
                        "[",
                        itemG.GetComponent<BagItem>().mydata_item.item_Name,
                        "]吗？",
                        };
                        confirmMessage.text = StringSplicingTool.StringSplicing(tempStr);
                    }
                    break;
            }
        }
    }
    void GoodsBuy(PointerEventData eventData)
    {
        //买入判定：判断类型
        switch (itemG.tag)
        {
            case ConstData.EquipmentType:
                //判断金币是否足够
                if (CurrencyManager.Instance.goldCoin > itemG.GetComponent<BagItem>().mydata_equipt.equipmentPrice)
                {
                    //判断背包是否有空间
                    if (itemG.GetComponent<BagItem>().mydata_equipt.equipmentType == ConstData.ListType_Weapon && 
                        SQLiteManager.Instance.bagDataSource[36].Bag_Weapon != 0)
                    {
                        confirmOK.gameObject.SetActive(false);
                        cancelNO.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
                        confirmMessage.text = "背包空间不足，购买失败";
                    }
                    else if (itemG.GetComponent<BagItem>().mydata_equipt.equipmentType == ConstData.ListType_Equipment && 
                        SQLiteManager.Instance.bagDataSource[36].Bag_Equipment != 0)
                    {
                        confirmOK.gameObject.SetActive(false);
                        cancelNO.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
                        confirmMessage.text = "背包空间不足，购买失败";
                    }
                    else
                    {
                        //如果是武器就买入武器，是防具就买入防具
                        if (itemG.GetComponent<BagItem>().mydata_equipt.equipmentType == ConstData.ListType_Weapon)
                        {
                            //更改字典和表
                            BuyWeapon(itemID);
                            //扣除金币
                            CurrencyManager.Instance.GoldCoinDecrease(itemG.GetComponent<BagItem>().mydata_equipt.equipmentPrice);
                            //刷新格子
                            switch (filterID)
                            {
                                case 0:
                                    LimitListCreate(ConstData.All);
                                    break;
                                case 1:
                                    LimitListCreate(ConstData.Saber);
                                    break;
                                case 2:
                                    LimitListCreate(ConstData.Knight);
                                    break;
                                case 3:
                                    LimitListCreate(ConstData.Berserker);
                                    break;
                                case 4:
                                    LimitListCreate(ConstData.Caster);
                                    break;
                                case 5:
                                    LimitListCreate(ConstData.Hunter);
                                    break;
                            }
                            //金币刷新
                            GoldCoin.text = CurrencyManager.Instance.GoldCoinDisplay();
                            //关闭窗口和按钮解绑
                            confirmFrame.SetActive(false);
                            confirmOK.GetComponent<UISceneWidget>().PointerClick -= GoodsBuy;
                            cancelNO.GetComponent<UISceneWidget>().PointerClick -= Cancel;
                        }
                        else
                        {
                            //更改字典和表
                            BuyEquipment(itemID);
                            //扣除金币
                            CurrencyManager.Instance.GoldCoinDecrease(itemG.GetComponent<BagItem>().mydata_equipt.equipmentPrice);
                            //刷新格子
                            switch (filterID)
                            {
                                case 0:
                                    LimitListCreate(ConstData.All);
                                    break;
                                case 1:
                                    LimitListCreate(ConstData.Saber);
                                    break;
                                case 2:
                                    LimitListCreate(ConstData.Knight);
                                    break;
                                case 3:
                                    LimitListCreate(ConstData.Berserker);
                                    break;
                                case 4:
                                    LimitListCreate(ConstData.Caster);
                                    break;
                                case 5:
                                    LimitListCreate(ConstData.Hunter);
                                    break;
                            }
                            //金币刷新
                            GoldCoin.text = CurrencyManager.Instance.GoldCoinDisplay();
                            //关闭窗口和按钮解绑
                            confirmFrame.SetActive(false);
                            confirmOK.GetComponent<UISceneWidget>().PointerClick -= GoodsBuy;
                            cancelNO.GetComponent<UISceneWidget>().PointerClick -= Cancel;
                        }
                    }
                }
                else
                {
                    confirmOK.gameObject.SetActive(false);
                    cancelNO.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
                    confirmMessage.text = "金币不足，购买失败";
                }
                    break;
            case ConstData.ItemType:
                //判断金币是否足够
                if (CurrencyManager.Instance.goldCoin > itemG.GetComponent<BagItem>().mydata_item.item_Price)
                {
                    //判断背包是否有空间
                    if (itemG.GetComponent<BagItem>().mydata_item.item_Type == ConstData.ListType_Consumable &&
                        SQLiteManager.Instance.bagDataSource[36].Bag_Consumable != 0)
                    {
                        confirmOK.gameObject.SetActive(false);
                        cancelNO.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
                        confirmMessage.text = "背包空间不足，购买失败";
                    }
                    else if (itemG.GetComponent<BagItem>().mydata_item.item_Type == ConstData.ListType_Material &&
                       SQLiteManager.Instance.bagDataSource[36].Bag_Material != 0)
                    {
                        confirmOK.gameObject.SetActive(false);
                        cancelNO.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
                        confirmMessage.text = "背包空间不足，购买失败";
                    }
                    else
                    {
                        //如果是消耗品就买入消耗品，是材料就买入材料
                        if (itemG.GetComponent<BagItem>().mydata_item.item_Type == ConstData.ListType_Consumable)
                        {
                            //更改字典和表
                            BuyConsumable(itemID);
                            //扣除金币
                            CurrencyManager.Instance.GoldCoinDecrease(itemG.GetComponent<BagItem>().mydata_item.item_Price);
                            //刷新格子
                            AllListCreate();
                            //金币刷新
                            GoldCoin.text = CurrencyManager.Instance.GoldCoinDisplay();
                            //关闭窗口和按钮解绑
                            confirmFrame.SetActive(false);
                            confirmOK.GetComponent<UISceneWidget>().PointerClick -= GoodsBuy;
                            cancelNO.GetComponent<UISceneWidget>().PointerClick -= Cancel;
                        }
                        else
                        {
                            //更改字典和表
                            BuyMaterial(itemID);
                            //扣除金币
                            CurrencyManager.Instance.GoldCoinDecrease(itemG.GetComponent<BagItem>().mydata_item.item_Price);
                            //刷新格子
                            AllListCreate();
                            //金币刷新
                            GoldCoin.text = CurrencyManager.Instance.GoldCoinDisplay();
                            //关闭窗口和按钮解绑
                            confirmFrame.SetActive(false);
                            confirmOK.GetComponent<UISceneWidget>().PointerClick -= GoodsBuy;
                            cancelNO.GetComponent<UISceneWidget>().PointerClick -= Cancel;
                        }
                    }
                }
                else
                {
                    confirmOK.gameObject.SetActive(false);
                    cancelNO.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
                    confirmMessage.text = "金币不足，购买失败";
                }
                    break;
        }
    }
    void ItemSell(PointerEventData eventData)
    {
        //售出判断：判断类型
        switch (itemG.tag)
        {
            case ConstData.EquipmentType:
                if (itemG.GetComponent<BagItem>().mydata_equipt.equipmentType == ConstData.ListType_Weapon)
                {
                    //删除字典和表内容
                    SellWeapon(itemG.GetComponent<BagItem>().myGrid);
                    //获得金币
                    CurrencyManager.Instance.GoldCoinIncrease((int)(itemG.GetComponent<BagItem>().mydata_equipt.equipmentPrice * 0.1f));
                    //回收光圈
                    if (itemHalo != null)
                    {
                        ObjectPoolManager.Instance.RecycleMyGameObject(itemHalo);
                    }
                    //重新生成格子
                    switch (filterID)
                    {
                        case 0:
                            LimitListCreate(ConstData.All);
                            break;
                        case 1:
                            LimitListCreate(ConstData.Saber);
                            break;
                        case 2:
                            LimitListCreate(ConstData.Knight);
                            break;
                        case 3:
                            LimitListCreate(ConstData.Berserker);
                            break;
                        case 4:
                            LimitListCreate(ConstData.Caster);
                            break;
                        case 5:
                            LimitListCreate(ConstData.Hunter);
                            break;
                    }
                    //金币刷新
                    GoldCoin.text = CurrencyManager.Instance.GoldCoinDisplay();
                }
                else
                {
                    //删除字典和表内容
                    SellEquipment(itemG.GetComponent<BagItem>().myGrid);
                    //获得金币
                    CurrencyManager.Instance.GoldCoinIncrease((int)(itemG.GetComponent<BagItem>().mydata_equipt.equipmentPrice * 0.1f));
                    //回收光圈
                    if (itemHalo != null)
                    {
                        ObjectPoolManager.Instance.RecycleMyGameObject(itemHalo);
                    }
                    //重新生成格子
                    switch (filterID)
                    {
                        case 0:
                            LimitListCreate(ConstData.All);
                            break;
                        case 1:
                            LimitListCreate(ConstData.Saber);
                            break;
                        case 2:
                            LimitListCreate(ConstData.Knight);
                            break;
                        case 3:
                            LimitListCreate(ConstData.Berserker);
                            break;
                        case 4:
                            LimitListCreate(ConstData.Caster);
                            break;
                        case 5:
                            LimitListCreate(ConstData.Hunter);
                            break;
                    }
                    //金币刷新
                    GoldCoin.text = CurrencyManager.Instance.GoldCoinDisplay();
                }
                break;
            case ConstData.ItemType:
                if (itemG.GetComponent<BagItem>().mydata_item.item_Type == ConstData.ListType_Consumable)
                {
                    //删除字典和表内容
                    SellConsumable(itemG.GetComponent<BagItem>().myGrid);
                    //获得金币
                    CurrencyManager.Instance.GoldCoinIncrease((int)(itemG.GetComponent<BagItem>().mydata_item.item_Price * 0.5f));
                    //回收光圈
                    if (itemHalo != null)
                    {
                        ObjectPoolManager.Instance.RecycleMyGameObject(itemHalo);
                    }
                    //重新生成格子
                    AllListCreate();
                    //金币刷新
                    GoldCoin.text = CurrencyManager.Instance.GoldCoinDisplay();
                }
                else
                {
                    //删除字典和表内容
                    SellMaterial(itemG.GetComponent<BagItem>().myGrid);
                    //获得金币
                    CurrencyManager.Instance.GoldCoinIncrease((int)(itemG.GetComponent<BagItem>().mydata_item.item_Price * 0.5f));
                    //回收光圈
                    if (itemHalo != null)
                    {
                        ObjectPoolManager.Instance.RecycleMyGameObject(itemHalo);
                    }
                    //重新生成格子
                    AllListCreate();
                    //金币刷新
                    GoldCoin.text = CurrencyManager.Instance.GoldCoinDisplay();
                }
                break;
        }
        //关闭窗口和按钮解绑
        confirmFrame.SetActive(false);
        confirmOK.GetComponent<UISceneWidget>().PointerClick -= ItemSell;
        cancelNO.GetComponent<UISceneWidget>().PointerClick -= Cancel;
    }
    void Cancel(PointerEventData eventData)
    {
        //按钮复位
        confirmOK.SetActive(true);
        cancelNO.transform.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(225, -245);
        //关闭窗口和按钮解绑
        switch (isSell)
        {
            case true:
                confirmOK.GetComponent<UISceneWidget>().PointerClick -= ItemSell;
                cancelNO.GetComponent<UISceneWidget>().PointerClick -= Cancel;
                break;
            case false:
                confirmOK.GetComponent<UISceneWidget>().PointerClick -= GoodsBuy;
                cancelNO.GetComponent<UISceneWidget>().PointerClick -= Cancel;
                break;
        }
        confirmFrame.SetActive(false);
    }

    //控制附属区
    void WeaponStore(PointerEventData eventData)
    {
        if (isSell == false)
        {
            StoreListBG_WP.SetActive(true);
            StoreListBG_EQ.SetActive(false);
            StoreListBG_CO.SetActive(false);
            StoreListBG_MT.SetActive(false);
            ItemListBG_WP.SetActive(false);
            ItemListBG_EQ.SetActive(false);
            ItemListBG_CO.SetActive(false);
            ItemListBG_MT.SetActive(false);
        }
        else
        {
            StoreListBG_WP.SetActive(false);
            StoreListBG_EQ.SetActive(false);
            StoreListBG_CO.SetActive(false);
            StoreListBG_MT.SetActive(false);
            ItemListBG_WP.SetActive(true);
            ItemListBG_EQ.SetActive(false);
            ItemListBG_CO.SetActive(false);
            ItemListBG_MT.SetActive(false);
        }
    }
    void EquipmentStore(PointerEventData eventData)
    {
        if (isSell == false)
        {
            StoreListBG_WP.SetActive(false);
            StoreListBG_EQ.SetActive(true);
            StoreListBG_CO.SetActive(false);
            StoreListBG_MT.SetActive(false);
            ItemListBG_WP.SetActive(false);
            ItemListBG_EQ.SetActive(false);
            ItemListBG_CO.SetActive(false);
            ItemListBG_MT.SetActive(false);
        }
        else
        {
            StoreListBG_WP.SetActive(false);
            StoreListBG_EQ.SetActive(false);
            StoreListBG_CO.SetActive(false);
            StoreListBG_MT.SetActive(false);
            ItemListBG_WP.SetActive(false);
            ItemListBG_EQ.SetActive(true);
            ItemListBG_CO.SetActive(false);
            ItemListBG_MT.SetActive(false);
        }
    }
    void ConsumableStore(PointerEventData eventData)
    {
        if (isSell == false)
        {
            StoreListBG_WP.SetActive(false);
            StoreListBG_EQ.SetActive(false);
            StoreListBG_CO.SetActive(true);
            StoreListBG_MT.SetActive(false);
            ItemListBG_WP.SetActive(false);
            ItemListBG_EQ.SetActive(false);
            ItemListBG_CO.SetActive(false);
            ItemListBG_MT.SetActive(false);
        }
        else
        {
            StoreListBG_WP.SetActive(false);
            StoreListBG_EQ.SetActive(false);
            StoreListBG_CO.SetActive(false);
            StoreListBG_MT.SetActive(false);
            ItemListBG_WP.SetActive(false);
            ItemListBG_EQ.SetActive(false);
            ItemListBG_CO.SetActive(true);
            ItemListBG_MT.SetActive(false);
        }
    }
    void MaterialStore(PointerEventData eventData)
    {
        if (isSell == false)
        {
            StoreListBG_WP.SetActive(false);
            StoreListBG_EQ.SetActive(false);
            StoreListBG_CO.SetActive(false);
            StoreListBG_MT.SetActive(true);
            ItemListBG_WP.SetActive(false);
            ItemListBG_EQ.SetActive(false);
            ItemListBG_CO.SetActive(false);
            ItemListBG_MT.SetActive(false);
        }
        else
        {
            StoreListBG_WP.SetActive(false);
            StoreListBG_EQ.SetActive(false);
            StoreListBG_CO.SetActive(false);
            StoreListBG_MT.SetActive(false);
            ItemListBG_WP.SetActive(false);
            ItemListBG_EQ.SetActive(false);
            ItemListBG_CO.SetActive(false);
            ItemListBG_MT.SetActive(true);
        }
    }

    //筛选区
    void FilterSaber(PointerEventData eventData)
    {
        if (transform.Find(ConstData.Filter_StoneSaberTag).GetComponent<Toggle>().isOn == false)
        {
            AllListCreate();
            filterID = 0;
        }
        else
        {
            LimitListCreate(ConstData.Saber);
            filterID = 1;
        }
    }
    void FilterKnight(PointerEventData eventData)
    {
        if (transform.Find(ConstData.Filter_StoneKnightTag).GetComponent<Toggle>().isOn == false)
        {
            AllListCreate();
            filterID = 0;
        }
        else
        {
            LimitListCreate(ConstData.Knight);
            filterID = 2;
        }
    }
    void FilterBerserker(PointerEventData eventData)
    {
        if (transform.Find(ConstData.Filter_StoneBerserkerTag).GetComponent<Toggle>().isOn == false)
        {
            AllListCreate();
            filterID = 0;
        }
        else
        {
            LimitListCreate(ConstData.Berserker);
            filterID = 3;
        }
    }
    void FilterCaster(PointerEventData eventData)
    {
        if (transform.Find(ConstData.Filter_StoneCasterTag).GetComponent<Toggle>().isOn == false)
        {
            AllListCreate();
            filterID = 0;
        }
        else
        {
            LimitListCreate(ConstData.Caster);
            filterID = 4;
        }
    }
    void FilterHunter(PointerEventData eventData)
    {
        if (transform.Find(ConstData.Filter_StoneHunterTag).GetComponent<Toggle>().isOn == false)
        {
            AllListCreate();
            filterID = 0;
        }
        else
        {
            LimitListCreate(ConstData.Hunter);
            filterID = 5;
        }
    }



    ////////////////////////////////////////////////////////////
    ////////////            非绑定方法           ///////////////
    ////////////////////////////////////////////////////////////

    
    /// <summary>
    /// 生成所有道具
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
    /// 仅生成武器和防具
    /// </summary>
    /// <param name="filterMode"></param>
    void LimitListCreate(string filterMode)
    {
        ItemClear(ConstData.Equipment);
        WeaponListCreate(filterMode);
        EquipmentListCreate(filterMode);
    }


    /// <summary>
    /// 根据清空限制来清空所有道具列表
    /// </summary>
    /// <param name="limitClear"></param>
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
    /// 根据背包类型来清空列表
    /// </summary>
    /// <param name="bagName"></param>
    void ListClearTool(string bagName)
    {
        switch (bagName)
        {
            case ConstData.WeaponBag:
                //商店部分
                for (int i = 0; i < storeList_WP.Count; i++)
                {
                    //解绑事件
                    if (storeList_WP[i].GetComponent<UISceneWidget>() != null)
                    {
                        storeList_WP[i].GetComponent<UISceneWidget>().PointerClick -= GoodsAndItemMessage;
                    }
                    //完成回收
                    ObjectPoolManager.Instance.RecycleMyGameObject(storeList_WP[i]);
                }
                storeList_WP.Clear();
                for (int j = 0; j < null_storeList_WP.Count; j++)
                {
                    //直接回收
                    ObjectPoolManager.Instance.RecycleMyGameObject(null_storeList_WP[j]);
                }
                null_storeList_WP.Clear();
                //道具部分
                for (int i = 0; i < itemList_WP.Count; i++)
                {
                    //解绑事件
                    if (itemList_WP[i].GetComponent<UISceneWidget>() != null)
                    {
                        itemList_WP[i].GetComponent<UISceneWidget>().PointerClick -= GoodsAndItemMessage;
                    }
                    //完成回收
                    ObjectPoolManager.Instance.RecycleMyGameObject(itemList_WP[i]);
                }
                itemList_WP.Clear();
                for (int j = 0; j < null_itemList_WP.Count; j++)
                {
                    //直接回收
                    ObjectPoolManager.Instance.RecycleMyGameObject(null_itemList_WP[j]);
                }
                null_itemList_WP.Clear();
                break;
            case ConstData.EquipmentBag:
                //商店部分
                for (int i = 0; i < storeList_EQ.Count; i++)
                {
                    //解绑事件
                    if (storeList_EQ[i].GetComponent<UISceneWidget>() != null)
                    {
                        storeList_EQ[i].GetComponent<UISceneWidget>().PointerClick -= GoodsAndItemMessage;
                    }
                    //完成回收
                    ObjectPoolManager.Instance.RecycleMyGameObject(storeList_EQ[i]);
                }
                storeList_EQ.Clear();
                for (int j = 0; j < null_storeList_EQ.Count; j++)
                {
                    //直接回收
                    ObjectPoolManager.Instance.RecycleMyGameObject(null_storeList_EQ[j]);
                }
                null_storeList_EQ.Clear();
                //道具部分
                for (int i = 0; i < itemList_EQ.Count; i++)
                {
                    //解绑事件
                    if (itemList_EQ[i].GetComponent<UISceneWidget>() != null)
                    {
                        itemList_EQ[i].GetComponent<UISceneWidget>().PointerClick -= GoodsAndItemMessage;
                    }
                    //完成回收
                    ObjectPoolManager.Instance.RecycleMyGameObject(itemList_EQ[i]);
                }
                itemList_EQ.Clear();
                for (int j = 0; j < null_itemList_EQ.Count; j++)
                {
                    //直接回收
                    ObjectPoolManager.Instance.RecycleMyGameObject(null_itemList_EQ[j]);
                }
                null_itemList_EQ.Clear();
                break;
            case ConstData.ConsumableBag:
                //商店部分
                for (int i = 0; i < storeList_CO.Count; i++)
                {
                    //解绑事件
                    if (storeList_CO[i].GetComponent<UISceneWidget>() != null)
                    {
                        storeList_CO[i].GetComponent<UISceneWidget>().PointerClick -= GoodsAndItemMessage;
                    }
                    //完成回收
                    ObjectPoolManager.Instance.RecycleMyGameObject(storeList_CO[i]);
                }
                storeList_CO.Clear();
                for (int j = 0; j < null_storeList_CO.Count; j++)
                {
                    //直接回收
                    ObjectPoolManager.Instance.RecycleMyGameObject(null_storeList_CO[j]);
                }
                null_storeList_CO.Clear();
                //道具部分
                for (int i = 0; i < itemList_CO.Count; i++)
                {
                    //解绑事件
                    if (itemList_CO[i].GetComponent<UISceneWidget>() != null)
                    {
                        itemList_CO[i].GetComponent<UISceneWidget>().PointerClick -= GoodsAndItemMessage;
                    }
                    //完成回收
                    ObjectPoolManager.Instance.RecycleMyGameObject(itemList_CO[i]);
                }
                itemList_CO.Clear();
                for (int j = 0; j < null_itemList_CO.Count; j++)
                {
                    //直接回收
                    ObjectPoolManager.Instance.RecycleMyGameObject(null_itemList_CO[j]);
                }
                null_itemList_CO.Clear();
                break;
            case ConstData.MaterialBag:
                //商店部分
                for (int i = 0; i < storeList_MT.Count; i++)
                {
                    //解绑事件
                    if (storeList_MT[i].GetComponent<UISceneWidget>() != null)
                    {
                        storeList_MT[i].GetComponent<UISceneWidget>().PointerClick -= GoodsAndItemMessage;
                    }
                    //完成回收
                    ObjectPoolManager.Instance.RecycleMyGameObject(storeList_MT[i]);
                }
                storeList_MT.Clear();
                for (int j = 0; j < null_storeList_MT.Count; j++)
                {
                    //直接回收
                    ObjectPoolManager.Instance.RecycleMyGameObject(null_storeList_MT[j]);
                }
                null_storeList_MT.Clear();
                //道具部分
                for (int i = 0; i < itemList_MT.Count; i++)
                {
                    //解绑事件
                    if (itemList_MT[i].GetComponent<UISceneWidget>() != null)
                    {
                        itemList_MT[i].GetComponent<UISceneWidget>().PointerClick -= GoodsAndItemMessage;
                    }
                    //完成回收
                    ObjectPoolManager.Instance.RecycleMyGameObject(itemList_MT[i]);
                }
                itemList_MT.Clear();
                for (int j = 0; j < null_itemList_MT.Count; j++)
                {
                    //直接回收
                    ObjectPoolManager.Instance.RecycleMyGameObject(null_itemList_MT[j]);
                }
                null_itemList_MT.Clear();
                break;
        }
    }
    /// <summary>
    /// 根据筛选模式生成武器道具
    /// </summary>
    /// <param 筛选模式="filterMode"></param>
    void WeaponListCreate(string filterMode)
    {
        //商店方面
        switch (filterMode)
            {
                case ConstData.All:
                    for (int i = 0; i < weaponSellList.Length; i++)
                    {
                        //生成物品
                        GameObject wp = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                        //指定父物体
                        wp.transform.parent = storeGridParent_WP.transform;
                        //物品大小、图片和标签设置
                        wp.transform.localScale = new Vector3(1, 1, 1);
                        wp.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                            ((weaponSellList[i]).ToString());
                        wp.transform.GetChild(0).tag = ConstData.EquipmentType;
                        wp.transform.GetChild(0).name = (weaponSellList[i]).ToString();
                        //添加数据脚本
                        if (wp.transform.GetChild(0).GetComponent<BagItem>() == null)
                        {
                            wp.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                        }
                        //获得数据
                        wp.transform.GetChild(0).GetComponent<BagItem>().GetData();
                        //绑定事件
                        if (wp.transform.GetComponent<UISceneWidget>() == null)
                        {
                            UISceneWidget EquipmentClick = UISceneWidget.Get(wp);
                            EquipmentClick.PointerClick += GoodsAndItemMessage;
                        }
                        else
                        {
                            wp.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                        }
                        storeList_WP.Add(wp);
                    }
                    if (storeList_WP.Count < ConstData.GridCount)
                    {
                        for (int i = 0; i < (ConstData.GridCount - storeList_WP.Count); i++)
                        {
                            GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                            //指定父物体
                            nullGrid.transform.parent = storeGridParent_WP.transform;
                            //图的大小设置
                            nullGrid.transform.localScale = new Vector3(1, 1, 1);
                            null_storeList_WP.Add(nullGrid);
                        }
                    }
                    break;
                case ConstData.Saber:
                    for (int i = 0; i < weaponSellList.Length; i++)
                    {
                        if (SQLiteManager.Instance.equipmentDataSource[weaponSellList[i]].equipmentClass == ConstData.Saber)
                        {
                            //生成物品
                            GameObject wp = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                            //指定父物体
                            wp.transform.parent = storeGridParent_WP.transform;
                            //物品大小、图片和标签设置
                            wp.transform.localScale = new Vector3(1, 1, 1);
                            wp.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                                ((weaponSellList[i]).ToString());
                            wp.transform.GetChild(0).tag = ConstData.EquipmentType;
                            wp.transform.GetChild(0).name = (weaponSellList[i]).ToString();
                            //添加数据脚本
                            if (wp.transform.GetChild(0).GetComponent<BagItem>() == null)
                            {
                                wp.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                            }
                            //获得数据
                            wp.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            //绑定事件
                            if (wp.transform.GetComponent<UISceneWidget>() == null)
                            {
                                UISceneWidget EquipmentClick = UISceneWidget.Get(wp);
                                EquipmentClick.PointerClick += GoodsAndItemMessage;
                            }
                            else
                            {
                                wp.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                            }
                            storeList_WP.Add(wp);
                        }
                    }
                    if (storeList_WP.Count < ConstData.GridCount)
                    {
                        for (int i = 0; i < (ConstData.GridCount - storeList_WP.Count); i++)
                        {
                            GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                            //指定父物体
                            nullGrid.transform.parent = storeGridParent_WP.transform;
                            //图的大小设置
                            nullGrid.transform.localScale = new Vector3(1, 1, 1);
                            null_storeList_WP.Add(nullGrid);
                        }
                    }
                    break;
                case ConstData.Knight:
                    for (int i = 0; i < weaponSellList.Length; i++)
                    {
                        if (SQLiteManager.Instance.equipmentDataSource[weaponSellList[i]].equipmentClass == ConstData.Knight)
                        {
                            //生成物品
                            GameObject wp = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                            //指定父物体
                            wp.transform.parent = storeGridParent_WP.transform;
                            //物品大小、图片和标签设置
                            wp.transform.localScale = new Vector3(1, 1, 1);
                            wp.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                                ((weaponSellList[i]).ToString());
                            wp.transform.GetChild(0).tag = ConstData.EquipmentType;
                            wp.transform.GetChild(0).name = (weaponSellList[i]).ToString();
                            //添加数据脚本
                            if (wp.transform.GetChild(0).GetComponent<BagItem>() == null)
                            {
                                wp.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                            }
                            //获得数据
                            wp.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            //绑定事件
                            if (wp.transform.GetComponent<UISceneWidget>() == null)
                            {
                                UISceneWidget EquipmentClick = UISceneWidget.Get(wp);
                                EquipmentClick.PointerClick += GoodsAndItemMessage;
                            }
                            else
                            {
                                wp.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                            }
                            storeList_WP.Add(wp);
                        }
                    }
                    if (storeList_WP.Count < ConstData.GridCount)
                    {
                        for (int i = 0; i < (ConstData.GridCount - storeList_WP.Count); i++)
                        {
                            GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                            //指定父物体
                            nullGrid.transform.parent = storeGridParent_WP.transform;
                            //图的大小设置
                            nullGrid.transform.localScale = new Vector3(1, 1, 1);
                            null_storeList_WP.Add(nullGrid);
                        }
                    }
                    break;
                case ConstData.Berserker:
                    for (int i = 0; i < weaponSellList.Length; i++)
                    {
                        if (SQLiteManager.Instance.equipmentDataSource[weaponSellList[i]].equipmentClass == ConstData.Berserker)
                        {
                            //生成物品
                            GameObject wp = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                            //指定父物体
                            wp.transform.parent = storeGridParent_WP.transform;
                            //物品大小、图片和标签设置
                            wp.transform.localScale = new Vector3(1, 1, 1);
                            wp.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                                ((weaponSellList[i]).ToString());
                            wp.transform.GetChild(0).tag = ConstData.EquipmentType;
                            wp.transform.GetChild(0).name = (weaponSellList[i]).ToString();
                            //添加数据脚本
                            if (wp.transform.GetChild(0).GetComponent<BagItem>() == null)
                            {
                                wp.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                            }
                            //获得数据
                            wp.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            //绑定事件
                            if (wp.transform.GetComponent<UISceneWidget>() == null)
                            {
                                UISceneWidget EquipmentClick = UISceneWidget.Get(wp);
                                EquipmentClick.PointerClick += GoodsAndItemMessage;
                            }
                            else
                            {
                                wp.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                            }
                            storeList_WP.Add(wp);
                        }
                    }
                    if (storeList_WP.Count < ConstData.GridCount)
                    {
                        for (int i = 0; i < (ConstData.GridCount - storeList_WP.Count); i++)
                        {
                            GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                            //指定父物体
                            nullGrid.transform.parent = storeGridParent_WP.transform;
                            //图的大小设置
                            nullGrid.transform.localScale = new Vector3(1, 1, 1);
                            null_storeList_WP.Add(nullGrid);
                        }
                    }
                    break;
                case ConstData.Caster:
                    for (int i = 0; i < weaponSellList.Length; i++)
                    {
                        if (SQLiteManager.Instance.equipmentDataSource[weaponSellList[i]].equipmentClass == ConstData.Caster)
                        {
                            //生成物品
                            GameObject wp = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                            //指定父物体
                            wp.transform.parent = storeGridParent_WP.transform;
                            //物品大小、图片和标签设置
                            wp.transform.localScale = new Vector3(1, 1, 1);
                            wp.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                                ((weaponSellList[i]).ToString());
                            wp.transform.GetChild(0).tag = ConstData.EquipmentType;
                            wp.transform.GetChild(0).name = (weaponSellList[i]).ToString();
                            //添加数据脚本
                            if (wp.transform.GetChild(0).GetComponent<BagItem>() == null)
                            {
                                wp.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                            }
                            //获得数据
                            wp.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            //绑定事件
                            if (wp.transform.GetComponent<UISceneWidget>() == null)
                            {
                                UISceneWidget EquipmentClick = UISceneWidget.Get(wp);
                                EquipmentClick.PointerClick += GoodsAndItemMessage;
                            }
                            else
                            {
                                wp.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                            }
                            storeList_WP.Add(wp);
                        }
                    }
                    if (storeList_WP.Count < ConstData.GridCount)
                    {
                        for (int i = 0; i < (ConstData.GridCount - storeList_WP.Count); i++)
                        {
                            GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                            //指定父物体
                            nullGrid.transform.parent = storeGridParent_WP.transform;
                            //图的大小设置
                            nullGrid.transform.localScale = new Vector3(1, 1, 1);
                            null_storeList_WP.Add(nullGrid);
                        }
                    }
                    break;
                case ConstData.Hunter:
                    for (int i = 0; i < weaponSellList.Length; i++)
                    {
                        if (SQLiteManager.Instance.equipmentDataSource[weaponSellList[i]].equipmentClass == ConstData.Hunter)
                        {
                            //生成物品
                            GameObject wp = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                            //指定父物体
                            wp.transform.parent = storeGridParent_WP.transform;
                            //物品大小、图片和标签设置
                            wp.transform.localScale = new Vector3(1, 1, 1);
                            wp.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                                ((weaponSellList[i]).ToString());
                            wp.transform.GetChild(0).tag = ConstData.EquipmentType;
                            wp.transform.GetChild(0).name = (weaponSellList[i]).ToString();
                            //添加数据脚本
                            if (wp.transform.GetChild(0).GetComponent<BagItem>() == null)
                            {
                                wp.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                            }
                            //获得数据
                            wp.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            //绑定事件
                            if (wp.transform.GetComponent<UISceneWidget>() == null)
                            {
                                UISceneWidget EquipmentClick = UISceneWidget.Get(wp);
                                EquipmentClick.PointerClick += GoodsAndItemMessage;
                            }
                            else
                            {
                                wp.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                            }
                            storeList_WP.Add(wp);
                        }
                    }
                    if (storeList_WP.Count < ConstData.GridCount)
                    {
                        for (int i = 0; i < (ConstData.GridCount - storeList_WP.Count); i++)
                        {
                            GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                            //指定父物体
                            nullGrid.transform.parent = storeGridParent_WP.transform;
                            //图的大小设置
                            nullGrid.transform.localScale = new Vector3(1, 1, 1);
                            null_storeList_WP.Add(nullGrid);
                        }
                    }
                    break;
            }
        //道具方面
        switch (filterMode)
            {
                case ConstData.All:
                    for (int i = 0; i < SQLiteManager.Instance.bagDataSource.Count; i++)
                    {
                        if (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon != 0)
                        {
                            //生成武器
                            GameObject wp = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                            //指定父物体
                            wp.transform.parent = itemGridParent_WP.transform;
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
                            }
                            //获得数据
                            wp.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            wp.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                            //绑定事件
                            if (wp.transform.GetComponent<UISceneWidget>() == null)
                            {
                                UISceneWidget EquipmentClick = UISceneWidget.Get(wp);
                                EquipmentClick.PointerClick += GoodsAndItemMessage;
                            }
                            else
                            {
                                wp.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                            }
                            itemList_WP.Add(wp);
                        }
                    }
                    if (itemList_WP.Count < ConstData.GridCount)
                    {
                        for (int i = 0; i < (ConstData.GridCount - itemList_WP.Count); i++)
                        {
                            GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                            //指定父物体
                            nullGrid.transform.parent = itemGridParent_WP.transform;
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
                                wp.transform.parent = itemGridParent_WP.transform;
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
                                }
                                //获得数据
                                wp.transform.GetChild(0).GetComponent<BagItem>().GetData();
                                wp.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                                //绑定事件
                                if (wp.transform.GetComponent<UISceneWidget>() == null)
                                {
                                    UISceneWidget EquipmentClick = UISceneWidget.Get(wp);
                                    EquipmentClick.PointerClick += GoodsAndItemMessage;
                                }
                                else
                                {
                                    wp.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                                }
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
                            nullGrid.transform.parent = itemGridParent_WP.transform;
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
                                wp.transform.parent = itemGridParent_WP.transform;
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
                                }
                                //获得数据
                                wp.transform.GetChild(0).GetComponent<BagItem>().GetData();
                                wp.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                                //绑定事件
                                if (wp.transform.GetComponent<UISceneWidget>() == null)
                                {
                                    UISceneWidget EquipmentClick = UISceneWidget.Get(wp);
                                    EquipmentClick.PointerClick += GoodsAndItemMessage;
                                }
                                else
                                {
                                    wp.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                                }
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
                            nullGrid.transform.parent = itemGridParent_WP.transform;
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
                                wp.transform.parent = itemGridParent_WP.transform;
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
                                }
                                //获得数据
                                wp.transform.GetChild(0).GetComponent<BagItem>().GetData();
                                wp.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                                //绑定事件
                                if (wp.transform.GetComponent<UISceneWidget>() == null)
                                {
                                    UISceneWidget EquipmentClick = UISceneWidget.Get(wp);
                                    EquipmentClick.PointerClick += GoodsAndItemMessage;
                                }
                                else
                                {
                                    wp.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                                }
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
                            nullGrid.transform.parent = itemGridParent_WP.transform;
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
                                wp.transform.parent = itemGridParent_WP.transform;
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
                                }
                                //获得数据
                                wp.transform.GetChild(0).GetComponent<BagItem>().GetData();
                                wp.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                                //绑定事件
                                if (wp.transform.GetComponent<UISceneWidget>() == null)
                                {
                                    UISceneWidget EquipmentClick = UISceneWidget.Get(wp);
                                    EquipmentClick.PointerClick += GoodsAndItemMessage;
                                }
                                else
                                {
                                    wp.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                                }
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
                            nullGrid.transform.parent = itemGridParent_WP.transform;
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
                                wp.transform.parent = itemGridParent_WP.transform;
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
                                }
                                //获得数据
                                wp.transform.GetChild(0).GetComponent<BagItem>().GetData();
                                wp.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                                //绑定事件
                                if (wp.transform.GetComponent<UISceneWidget>() == null)
                                {
                                    UISceneWidget EquipmentClick = UISceneWidget.Get(wp);
                                    EquipmentClick.PointerClick += GoodsAndItemMessage;
                                }
                                else
                                {
                                    wp.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                                }
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
                            nullGrid.transform.parent = itemGridParent_WP.transform;
                            //图的大小设置
                            nullGrid.transform.localScale = new Vector3(1, 1, 1);
                            null_itemList_WP.Add(nullGrid);
                        }
                    }
                    break;
            }
    }
    /// <summary>
    /// 根据筛选模式生成防具道具
    /// </summary>
    /// <param 少选模式="filterMode"></param>
    void EquipmentListCreate(string filterMode)
    {
        //商店方面
        switch (filterMode)
            {
                case ConstData.All:
                    for (int i = 0; i < equipmentSellList.Length; i++)
                    {
                        //生成物品
                        GameObject eq = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                        //指定父物体
                        eq.transform.parent = storeGridParent_EQ.transform;
                        //物品大小、图片和标签设置
                        eq.transform.localScale = new Vector3(1, 1, 1);
                        eq.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                            ((equipmentSellList[i]).ToString());
                        eq.transform.GetChild(0).tag = ConstData.EquipmentType;
                        eq.transform.GetChild(0).name = (equipmentSellList[i]).ToString();
                        //添加数据脚本
                        if (eq.transform.GetChild(0).GetComponent<BagItem>() == null)
                        {
                            eq.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                        }
                        //获得数据
                        eq.transform.GetChild(0).GetComponent<BagItem>().GetData();
                        //绑定事件
                        if (eq.transform.GetComponent<UISceneWidget>() == null)
                        {
                            UISceneWidget EquipmentClick = UISceneWidget.Get(eq);
                            EquipmentClick.PointerClick += GoodsAndItemMessage;
                        }
                        else
                        {
                            eq.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                        }
                        storeList_EQ.Add(eq);
                    }
                    if (storeList_EQ.Count < ConstData.GridCount)
                    {
                        for (int i = 0; i < (ConstData.GridCount - storeList_EQ.Count); i++)
                        {
                            GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                            //指定父物体
                            nullGrid.transform.parent = storeGridParent_EQ.transform;
                            //图的大小设置
                            nullGrid.transform.localScale = new Vector3(1, 1, 1);
                            null_storeList_EQ.Add(nullGrid);
                        }
                    }
                    break;
                case ConstData.Saber:
                    for (int i = 0; i < equipmentSellList.Length; i++)
                    {
                        if (SQLiteManager.Instance.equipmentDataSource[equipmentSellList[i]].equipmentClass == ConstData.Saber)
                        {
                            //生成物品
                            GameObject eq = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                            //指定父物体
                            eq.transform.parent = storeGridParent_EQ.transform;
                            //物品大小、图片和标签设置
                            eq.transform.localScale = new Vector3(1, 1, 1);
                            eq.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                                ((equipmentSellList[i]).ToString());
                            eq.transform.GetChild(0).tag = ConstData.EquipmentType;
                            eq.transform.GetChild(0).name = (equipmentSellList[i]).ToString();
                            //添加数据脚本
                            if (eq.transform.GetChild(0).GetComponent<BagItem>() == null)
                            {
                                eq.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                            }
                            //获得数据
                            eq.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            //绑定事件
                            if (eq.transform.GetComponent<UISceneWidget>() == null)
                            {
                                UISceneWidget EquipmentClick = UISceneWidget.Get(eq);
                                EquipmentClick.PointerClick += GoodsAndItemMessage;
                            }
                            else
                            {
                                eq.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                            }
                            storeList_EQ.Add(eq);
                        }
                    }
                    if (storeList_EQ.Count < ConstData.GridCount)
                    {
                        for (int i = 0; i < (ConstData.GridCount - storeList_EQ.Count); i++)
                        {
                            GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                            //指定父物体
                            nullGrid.transform.parent = storeGridParent_EQ.transform;
                            //图的大小设置
                            nullGrid.transform.localScale = new Vector3(1, 1, 1);
                            null_storeList_EQ.Add(nullGrid);
                        }
                    }
                    break;
                case ConstData.Knight:
                    for (int i = 0; i < equipmentSellList.Length; i++)
                    {
                        if (SQLiteManager.Instance.equipmentDataSource[equipmentSellList[i]].equipmentClass == ConstData.Knight)
                        {
                            //生成物品
                            GameObject eq = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                            //指定父物体
                            eq.transform.parent = storeGridParent_EQ.transform;
                            //物品大小、图片和标签设置
                            eq.transform.localScale = new Vector3(1, 1, 1);
                            eq.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                                ((equipmentSellList[i]).ToString());
                            eq.transform.GetChild(0).tag = ConstData.EquipmentType;
                            eq.transform.GetChild(0).name = (equipmentSellList[i]).ToString();
                            //添加数据脚本
                            if (eq.transform.GetChild(0).GetComponent<BagItem>() == null)
                            {
                                eq.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                            }
                            //获得数据
                            eq.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            //绑定事件
                            if (eq.transform.GetComponent<UISceneWidget>() == null)
                            {
                                UISceneWidget EquipmentClick = UISceneWidget.Get(eq);
                                EquipmentClick.PointerClick += GoodsAndItemMessage;
                            }
                            else
                            {
                                eq.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                            }
                            storeList_EQ.Add(eq);
                        }
                    }
                    if (storeList_EQ.Count < ConstData.GridCount)
                    {
                        for (int i = 0; i < (ConstData.GridCount - storeList_EQ.Count); i++)
                        {
                            GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                            //指定父物体
                            nullGrid.transform.parent = storeGridParent_EQ.transform;
                            //图的大小设置
                            nullGrid.transform.localScale = new Vector3(1, 1, 1);
                            null_storeList_EQ.Add(nullGrid);
                        }
                    }
                    break;
                case ConstData.Berserker:
                    for (int i = 0; i < equipmentSellList.Length; i++)
                    {
                        if (SQLiteManager.Instance.equipmentDataSource[equipmentSellList[i]].equipmentClass == ConstData.Berserker)
                        {
                            //生成物品
                            GameObject eq = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                            //指定父物体
                            eq.transform.parent = storeGridParent_EQ.transform;
                            //物品大小、图片和标签设置
                            eq.transform.localScale = new Vector3(1, 1, 1);
                            eq.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                                ((equipmentSellList[i]).ToString());
                            eq.transform.GetChild(0).tag = ConstData.EquipmentType;
                            eq.transform.GetChild(0).name = (equipmentSellList[i]).ToString();
                            //添加数据脚本
                            if (eq.transform.GetChild(0).GetComponent<BagItem>() == null)
                            {
                                eq.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                            }
                            //获得数据
                            eq.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            //绑定事件
                            if (eq.transform.GetComponent<UISceneWidget>() == null)
                            {
                                UISceneWidget EquipmentClick = UISceneWidget.Get(eq);
                                EquipmentClick.PointerClick += GoodsAndItemMessage;
                            }
                            else
                            {
                                eq.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                            }
                            storeList_EQ.Add(eq);
                        }
                    }
                    if (storeList_EQ.Count < ConstData.GridCount)
                    {
                        for (int i = 0; i < (ConstData.GridCount - storeList_EQ.Count); i++)
                        {
                            GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                            //指定父物体
                            nullGrid.transform.parent = storeGridParent_EQ.transform;
                            //图的大小设置
                            nullGrid.transform.localScale = new Vector3(1, 1, 1);
                            null_storeList_EQ.Add(nullGrid);
                        }
                    }
                    break;
                case ConstData.Caster:
                    for (int i = 0; i < equipmentSellList.Length; i++)
                    {
                        if (SQLiteManager.Instance.equipmentDataSource[equipmentSellList[i]].equipmentClass == ConstData.Caster)
                        {
                            //生成物品
                            GameObject eq = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                            //指定父物体
                            eq.transform.parent = storeGridParent_EQ.transform;
                            //物品大小、图片和标签设置
                            eq.transform.localScale = new Vector3(1, 1, 1);
                            eq.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                                ((equipmentSellList[i]).ToString());
                            eq.transform.GetChild(0).tag = ConstData.EquipmentType;
                            eq.transform.GetChild(0).name = (equipmentSellList[i]).ToString();
                            //添加数据脚本
                            if (eq.transform.GetChild(0).GetComponent<BagItem>() == null)
                            {
                                eq.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                            }
                            //获得数据
                            eq.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            //绑定事件
                            if (eq.transform.GetComponent<UISceneWidget>() == null)
                            {
                                UISceneWidget EquipmentClick = UISceneWidget.Get(eq);
                                EquipmentClick.PointerClick += GoodsAndItemMessage;
                            }
                            else
                            {
                                eq.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                            }
                            storeList_EQ.Add(eq);
                        }
                    }
                    if (storeList_EQ.Count < ConstData.GridCount)
                    {
                        for (int i = 0; i < (ConstData.GridCount - storeList_EQ.Count); i++)
                        {
                            GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                            //指定父物体
                            nullGrid.transform.parent = storeGridParent_EQ.transform;
                            //图的大小设置
                            nullGrid.transform.localScale = new Vector3(1, 1, 1);
                            null_storeList_EQ.Add(nullGrid);
                        }
                    }
                    break;
                case ConstData.Hunter:
                    for (int i = 0; i < equipmentSellList.Length; i++)
                    {
                        if (SQLiteManager.Instance.equipmentDataSource[equipmentSellList[i]].equipmentClass == ConstData.Hunter)
                        {
                            //生成物品
                            GameObject eq = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                            //指定父物体
                            eq.transform.parent = storeGridParent_EQ.transform;
                            //物品大小、图片和标签设置
                            eq.transform.localScale = new Vector3(1, 1, 1);
                            eq.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                                ((equipmentSellList[i]).ToString());
                            eq.transform.GetChild(0).tag = ConstData.EquipmentType;
                            eq.transform.GetChild(0).name = (equipmentSellList[i]).ToString();
                            //添加数据脚本
                            if (eq.transform.GetChild(0).GetComponent<BagItem>() == null)
                            {
                                eq.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                            }
                            //获得数据
                            eq.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            //绑定事件
                            if (eq.transform.GetComponent<UISceneWidget>() == null)
                            {
                                UISceneWidget EquipmentClick = UISceneWidget.Get(eq);
                                EquipmentClick.PointerClick += GoodsAndItemMessage;
                            }
                            else
                            {
                                eq.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                            }
                            storeList_EQ.Add(eq);
                        }
                    }
                    if (storeList_EQ.Count < ConstData.GridCount)
                    {
                        for (int i = 0; i < (ConstData.GridCount - storeList_EQ.Count); i++)
                        {
                            GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                            //指定父物体
                            nullGrid.transform.parent = storeGridParent_EQ.transform;
                            //图的大小设置
                            nullGrid.transform.localScale = new Vector3(1, 1, 1);
                            null_storeList_EQ.Add(nullGrid);
                        }
                    }
                    break;
            }
        //道具方面
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
                            eq.transform.parent = itemGridParent_EQ.transform;
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
                            }
                            //获得数据
                            eq.transform.GetChild(0).GetComponent<BagItem>().GetData();
                            eq.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                            //绑定事件
                            if (eq.transform.GetComponent<UISceneWidget>() == null)
                            {
                                UISceneWidget EquipmentClick = UISceneWidget.Get(eq);
                                EquipmentClick.PointerClick += GoodsAndItemMessage;
                            }
                            else
                            {
                                eq.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                            }
                            itemList_EQ.Add(eq);
                        }
                    }
                    if (itemList_EQ.Count < ConstData.GridCount)
                    {
                        for (int i = 0; i < (ConstData.GridCount - itemList_EQ.Count); i++)
                        {
                            GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                            //指定父物体
                            nullGrid.transform.parent = itemGridParent_EQ.transform;
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
                                eq.transform.parent = itemGridParent_EQ.transform;
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
                                }
                                //获得数据
                                eq.transform.GetChild(0).GetComponent<BagItem>().GetData();
                                eq.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                                //绑定事件
                                if (eq.transform.GetComponent<UISceneWidget>() == null)
                                {
                                    UISceneWidget EquipmentClick = UISceneWidget.Get(eq);
                                    EquipmentClick.PointerClick += GoodsAndItemMessage;
                                }
                                else
                                {
                                    eq.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                                }
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
                            nullGrid.transform.parent = itemGridParent_EQ.transform;
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
                                eq.transform.parent = itemGridParent_EQ.transform;
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
                                }
                                //获得数据
                                eq.transform.GetChild(0).GetComponent<BagItem>().GetData();
                                eq.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                                //绑定事件
                                if (eq.transform.GetComponent<UISceneWidget>() == null)
                                {
                                    UISceneWidget EquipmentClick = UISceneWidget.Get(eq);
                                    EquipmentClick.PointerClick += GoodsAndItemMessage;
                                }
                                else
                                {
                                    eq.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                                }
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
                            nullGrid.transform.parent = itemGridParent_EQ.transform;
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
                                eq.transform.parent = itemGridParent_EQ.transform;
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
                                }
                                //获得数据
                                eq.transform.GetChild(0).GetComponent<BagItem>().GetData();
                                eq.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                                //绑定事件
                                if (eq.transform.GetComponent<UISceneWidget>() == null)
                                {
                                    UISceneWidget EquipmentClick = UISceneWidget.Get(eq);
                                    EquipmentClick.PointerClick += GoodsAndItemMessage;
                                }
                                else
                                {
                                    eq.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                                }
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
                            nullGrid.transform.parent = itemGridParent_EQ.transform;
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
                                eq.transform.parent = itemGridParent_EQ.transform;
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
                                }
                                //获得数据
                                eq.transform.GetChild(0).GetComponent<BagItem>().GetData();
                                eq.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                                //绑定事件
                                if (eq.transform.GetComponent<UISceneWidget>() == null)
                                {
                                    UISceneWidget EquipmentClick = UISceneWidget.Get(eq);
                                    EquipmentClick.PointerClick += GoodsAndItemMessage;
                                }
                                else
                                {
                                    eq.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                                }
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
                            nullGrid.transform.parent = itemGridParent_EQ.transform;
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
                                eq.transform.parent = itemGridParent_EQ.transform;
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
                                }
                                //获得数据
                                eq.transform.GetChild(0).GetComponent<BagItem>().GetData();
                                eq.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                                //绑定事件
                                if (eq.transform.GetComponent<UISceneWidget>() == null)
                                {
                                    UISceneWidget EquipmentClick = UISceneWidget.Get(eq);
                                    EquipmentClick.PointerClick += GoodsAndItemMessage;
                                }
                                else
                                {
                                    eq.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                                }
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
                            nullGrid.transform.parent = itemGridParent_EQ.transform;
                            //图的大小设置
                            nullGrid.transform.localScale = new Vector3(1, 1, 1);
                            null_itemList_EQ.Add(nullGrid);
                        }
                    }
                    break;
            }
    }
    /// <summary>
    /// 生成消耗品道具
    /// </summary>
    void ConsumableListCreate()
    {
        //商店方面
        for (int i = 0; i < consumableSellList.Length; i++)
            {
                //生成消耗品
                GameObject co = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                //指定父物体
                co.transform.parent = storeGridParent_CO.transform;
                //消耗品的大小、图片和标签设置
                co.transform.localScale = new Vector3(1, 1, 1);
                co.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                    (consumableSellList[i].ToString());
                co.transform.GetChild(0).tag = ConstData.ItemType;
                co.transform.GetChild(0).name = consumableSellList[i].ToString();
                //添加数据脚本
                if (co.transform.GetChild(0).GetComponent<BagItem>() == null)
                {
                    co.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                }
                //获得数据
                co.transform.GetChild(0).GetComponent<BagItem>().GetData();
                //绑定事件
                if (co.transform.GetComponent<UISceneWidget>() == null)
                {
                    UISceneWidget ItemClick = UISceneWidget.Get(co);
                    ItemClick.PointerClick += GoodsAndItemMessage;
                }
                else
                {
                    co.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                }
                storeList_CO.Add(co);
            }
        if (storeList_CO.Count < ConstData.GridCount)
            {
                for (int i = 0; i < (ConstData.GridCount - storeList_CO.Count); i++)
                {
                    GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                    //指定父物体
                    nullGrid.transform.parent = storeGridParent_CO.transform;
                    //图的大小设置
                    nullGrid.transform.localScale = new Vector3(1, 1, 1);
                    null_storeList_CO.Add(nullGrid);
                }
            }
        //道具方面
        for (int i = 0; i < SQLiteManager.Instance.bagDataSource.Count; i++)
            {
                if (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Consumable != 0)
                {
                    //生成消耗品
                    GameObject co = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                    //指定父物体
                    co.transform.parent = itemGridParent_CO.transform;
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
                    }
                    //获得数据
                    co.transform.GetChild(0).GetComponent<BagItem>().GetData();
                    co.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                    //绑定事件
                    if (co.transform.GetComponent<UISceneWidget>() == null)
                    {
                        UISceneWidget ItemClick = UISceneWidget.Get(co);
                        ItemClick.PointerClick += GoodsAndItemMessage;
                    }
                    else
                    {
                        co.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                    }
                    itemList_CO.Add(co);
                }
            }
        if (itemList_CO.Count < ConstData.GridCount)
            {
                for (int i = 0; i < (ConstData.GridCount - itemList_CO.Count); i++)
                {
                    GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                    //指定父物体
                    nullGrid.transform.parent = itemGridParent_CO.transform;
                    //图的大小设置
                    nullGrid.transform.localScale = new Vector3(1, 1, 1);
                    null_itemList_CO.Add(nullGrid);
                }
            }
    }
    /// <summary>
    /// 生成材料道具
    /// </summary>
    void MaterialListCreate()
    {
        //商店方面
        for (int i = 0; i < materialSellList.Length; i++)
            {
                //生成消耗品
                GameObject mt = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                //指定父物体
                mt.transform.parent = storeGridParent_MT.transform;
                //消耗品的大小、图片和标签设置
                mt.transform.localScale = new Vector3(1, 1, 1);
                mt.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite
                    ((materialSellList[i]).ToString());
                mt.transform.GetChild(0).tag = ConstData.ItemType;
                mt.transform.GetChild(0).name = materialSellList[i].ToString();
                //添加数据脚本
                if (mt.transform.GetChild(0).GetComponent<BagItem>() == null)
                {
                    mt.transform.GetChild(0).gameObject.AddComponent<BagItem>();
                }
                //获得数据
                mt.transform.GetChild(0).GetComponent<BagItem>().GetData();
                //绑定事件
                if (mt.transform.GetComponent<UISceneWidget>() == null)
                {
                    UISceneWidget ItemClick = UISceneWidget.Get(mt);
                    ItemClick.PointerClick += GoodsAndItemMessage;
                }
                else
                {
                    mt.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                }
                storeList_MT.Add(mt);
            }
        if (storeList_MT.Count < ConstData.GridCount)
            {
                for (int i = 0; i < (ConstData.GridCount - storeList_MT.Count); i++)
                {
                    GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                    //指定父物体
                    nullGrid.transform.parent = storeGridParent_MT.transform;
                    //图的大小设置
                    nullGrid.transform.localScale = new Vector3(1, 1, 1);
                    null_storeList_MT.Add(nullGrid);
                }
            }
        //道具方面
        for (int i = 0; i < SQLiteManager.Instance.bagDataSource.Count; i++)
            {
                if (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Material != 0)
                {
                    //生成消耗品
                    GameObject mt = ObjectPoolManager.Instance.InstantiateMyGameObject(itemBar);
                    //指定父物体
                    mt.transform.parent = itemGridParent_MT.transform;
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
                    }
                    //获得数据
                    mt.transform.GetChild(0).GetComponent<BagItem>().GetData();
                    mt.transform.GetChild(0).GetComponent<BagItem>().myGrid = i + 1;
                    //绑定事件
                    if (mt.transform.GetComponent<UISceneWidget>() == null)
                    {
                        UISceneWidget ItemClick = UISceneWidget.Get(mt);
                        ItemClick.PointerClick += GoodsAndItemMessage;
                    }
                    else
                    {
                        mt.transform.GetComponent<UISceneWidget>().PointerClick += GoodsAndItemMessage;
                    }
                    itemList_MT.Add(mt);
                }
            }
        if (itemList_MT.Count < ConstData.GridCount)
            {
                for (int i = 0; i < (ConstData.GridCount - itemList_MT.Count); i++)
                {
                    GameObject nullGrid = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                    //指定父物体
                    nullGrid.transform.parent = itemGridParent_MT.transform;
                    //图的大小设置
                    nullGrid.transform.localScale = new Vector3(1, 1, 1);
                    null_itemList_MT.Add(nullGrid);
                }
            }
    }

    /// <summary>
    /// 出售武器后格子自适应
    /// </summary>
    /// <param name="grid"></param>
    void SellWeapon(int grid)
    {
        //改字典
        for (int i = grid; i < SQLiteManager.Instance.bagDataSource.Count; i++)
        {
            SQLiteManager.Instance.bagDataSource[i].Bag_Weapon = SQLiteManager.Instance.bagDataSource[i + 1].Bag_Weapon;
            SQLiteManager.Instance.UpdataDataFromTable
                (ConstData.Bag, ConstData.Bag_Weapon, SQLiteManager.Instance.bagDataSource[i + 1].Bag_Weapon, ConstData.Bag_Grid, i);
        }
        SQLiteManager.Instance.bagDataSource[SQLiteManager.Instance.bagDataSource.Count].Bag_Weapon = 0;
        SQLiteManager.Instance.UpdataDataFromTable
                (ConstData.Bag, ConstData.Bag_Weapon, 0, ConstData.Bag_Grid, SQLiteManager.Instance.bagDataSource.Count);
    }
    /// <summary>
    /// 出售防具后格子自适应
    /// </summary>
    /// <param name="grid"></param>
    void SellEquipment(int grid)
    {
        for (int i = grid; i < SQLiteManager.Instance.bagDataSource.Count; i++)
        {
            SQLiteManager.Instance.bagDataSource[i].Bag_Equipment = SQLiteManager.Instance.bagDataSource[i + 1].Bag_Equipment;
            SQLiteManager.Instance.UpdataDataFromTable
                (ConstData.Bag, ConstData.Bag_Equipment, SQLiteManager.Instance.bagDataSource[i + 1].Bag_Equipment, ConstData.Bag_Grid, i);
        }
        SQLiteManager.Instance.bagDataSource[SQLiteManager.Instance.bagDataSource.Count].Bag_Equipment = 0;
        SQLiteManager.Instance.UpdataDataFromTable
                (ConstData.Bag, ConstData.Bag_Equipment, 0, ConstData.Bag_Grid, SQLiteManager.Instance.bagDataSource.Count);
    }
    /// <summary>
    /// 出售消耗品后格子自适应
    /// </summary>
    /// <param name="grid"></param>
    void SellConsumable(int grid)
    {
        for (int i = grid; i < SQLiteManager.Instance.bagDataSource.Count; i++)
        {
            SQLiteManager.Instance.bagDataSource[i].Bag_Consumable = SQLiteManager.Instance.bagDataSource[i + 1].Bag_Consumable;
            SQLiteManager.Instance.UpdataDataFromTable
                (ConstData.Bag, ConstData.Bag_Consumable, SQLiteManager.Instance.bagDataSource[i + 1].Bag_Consumable, ConstData.Bag_Grid, i);
        }
        SQLiteManager.Instance.bagDataSource[SQLiteManager.Instance.bagDataSource.Count].Bag_Consumable = 0;
        SQLiteManager.Instance.UpdataDataFromTable
                (ConstData.Bag, ConstData.Bag_Consumable, 0, ConstData.Bag_Grid, SQLiteManager.Instance.bagDataSource.Count);
    }
    /// <summary>
    /// 出售材料后格子自适应
    /// </summary>
    /// <param name="grid"></param>
    void SellMaterial(int grid)
    {
        for (int i = grid; i < SQLiteManager.Instance.bagDataSource.Count; i++)
        {
            SQLiteManager.Instance.bagDataSource[i].Bag_Material = SQLiteManager.Instance.bagDataSource[i + 1].Bag_Material;
            SQLiteManager.Instance.UpdataDataFromTable
                (ConstData.Bag, ConstData.Bag_Material, SQLiteManager.Instance.bagDataSource[i + 1].Bag_Material, ConstData.Bag_Grid, i);
        }
        SQLiteManager.Instance.bagDataSource[SQLiteManager.Instance.bagDataSource.Count].Bag_Material = 0;
        SQLiteManager.Instance.UpdataDataFromTable
                (ConstData.Bag, ConstData.Bag_Material, 0, ConstData.Bag_Grid, SQLiteManager.Instance.bagDataSource.Count);
    }

    /// <summary>
    /// 购买武器后改字典和表
    /// </summary>
    /// <param name="itemID"></param>
    void BuyWeapon(int itemID)
    {
        for (int i = 0; i < SQLiteManager.Instance.bagDataSource.Count; i++)
        {
            if (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon == 0)
            {
                SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Weapon = itemID;
                SQLiteManager.Instance.UpdataDataFromTable(ConstData.Bag, ConstData.Bag_Weapon, itemID, ConstData.Bag_Grid, (i + 1));
                return;
            }
        }
    }
    /// <summary>
    /// 购买防具后改字典和表
    /// </summary>
    /// <param name="itemID"></param>
    void BuyEquipment(int itemID)
    {
        for (int i = 0; i < SQLiteManager.Instance.bagDataSource.Count; i++)
        {
            if (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment == 0)
            {
                SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Equipment = itemID;
                SQLiteManager.Instance.UpdataDataFromTable(ConstData.Bag, ConstData.Bag_Equipment, itemID, ConstData.Bag_Grid, (i + 1));
                return;
            }
        }
    }
    /// <summary>
    /// 购买消耗品后改字典和表
    /// </summary>
    /// <param name="itemID"></param>
    void BuyConsumable(int itemID)
    {
        for (int i = 0; i < SQLiteManager.Instance.bagDataSource.Count; i++)
        {
            if (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Consumable == 0)
            {
                SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Consumable = itemID;
                SQLiteManager.Instance.UpdataDataFromTable(ConstData.Bag, ConstData.Bag_Consumable, itemID, ConstData.Bag_Grid, (i + 1));
                return;
            }
        }
    }
    /// <summary>
    /// 购买材料后改字典和表
    /// </summary>
    /// <param name="itemID"></param>
    void BuyMaterial(int itemID)
    {
        for (int i = 0; i < SQLiteManager.Instance.bagDataSource.Count; i++)
        {
            if (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Material == 0)
            {
                SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Material = itemID;
                SQLiteManager.Instance.UpdataDataFromTable(ConstData.Bag, ConstData.Bag_Material, itemID, ConstData.Bag_Grid, (i + 1));
                return;
            }
        }
    }

    /// <summary>
    /// 装备格内容区域自适应长度
    /// </summary>
    /// <param 筛选模式="FilterClass"></param>
    void ItemBarContentAdaptive(int frameID)
    {
        switch (frameID)
        {
            case 1:
                int tempheight = 1200;
                transform.Find(ConstData.ControllerArea_StoreListBG_WP).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta
                    = new Vector2(1200, tempheight);
                if (storeList_WP.Count > ConstData.GridCount)
                {
                    tempheight = 1200 + ((int)((storeList_WP.Count - ConstData.GridCount) / 6) + 1) * 200;
                    transform.Find(ConstData.ControllerArea_StoreListBG_WP).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta
                        = new Vector2(1200, tempheight);
                }
                break;
            case 2:
                int tempheight2 = 1200;
                transform.Find(ConstData.ControllerArea_StoreListBG_EQ).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta
                    = new Vector2(1200, tempheight2);
                if (storeList_WP.Count > ConstData.GridCount)
                {
                    tempheight2 = 1200 + ((int)((storeList_EQ.Count - ConstData.GridCount) / 6) + 1) * 200;
                    transform.Find(ConstData.ControllerArea_StoreListBG_EQ).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta
                        = new Vector2(1200, tempheight2);
                }
                break;
            case 3:
                int tempheight3 = 1200;
                transform.Find(ConstData.ControllerArea_StoreListBG_CO).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta
                    = new Vector2(1200, tempheight3);
                if (storeList_WP.Count > ConstData.GridCount)
                {
                    tempheight3 = 1200 + ((int)((storeList_CO.Count - ConstData.GridCount) / 6) + 1) * 200;
                    transform.Find(ConstData.ControllerArea_StoreListBG_CO).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta
                        = new Vector2(1200, tempheight3);
                }
                break;
            case 4:
                int tempheight4 = 1200;
                transform.Find(ConstData.ControllerArea_StoreListBG_MT).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta
                    = new Vector2(1200, tempheight4);
                if (storeList_WP.Count > ConstData.GridCount)
                {
                    tempheight4 = 1200 + ((int)((storeList_MT.Count - ConstData.GridCount) / 6) + 1) * 200;
                    transform.Find(ConstData.ControllerArea_StoreListBG_MT).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta
                        = new Vector2(1200, tempheight4);
                }
                break;
        }
    }
}
