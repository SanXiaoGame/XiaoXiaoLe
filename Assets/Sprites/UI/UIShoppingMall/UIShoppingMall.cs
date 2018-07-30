using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 商城界面
/// </summary>
public class UIShoppingMall : MonoBehaviour, IUIBase
{
    //钻石
    Text _diamonds;

    //商城购买界面按钮
    GameObject _shopBuyButton;
    //商城钻石购买按钮
    GameObject _superMarketBuyButton;
    //招募界面按钮
    GameObject _contractButton;
    //返回主城按钮
    GameObject _mainCityButton;

    //武器按钮
    GameObject _weapon;
    //装备按钮
    GameObject _equipment;
    //消耗品按钮
    GameObject _consumable;
    //材料按钮
    GameObject _material;

    //确认购买按钮
    GameObject confirmPurchaseButton;
    //确认界面
    GameObject _confirmFrame;
    //确认按钮
    GameObject _confirm;
    //取消按钮
    GameObject _cancel;

    //物品栏内容
    RectTransform listContent;

    //介绍栏
    Text _textContent;
    //确认购买详情
    Text _contentText;

    //选中光圈
    GameObject _aperture;
    //存储选中物品数据
    BagItem _bagItem;
    //记录物品价格
    int _price = 0;

    //确认购买按钮的绑定
    UISceneWidget bindingConfirmPurchaseButton;
    UISceneWidget bindingConfirm;
    UISceneWidget bindingCancel;
    //物品栏切换按钮
    UISceneWidget bindingWeaponButton;
    UISceneWidget bindingEquipmentButton;
    UISceneWidget bindingConsumableButton;
    UISceneWidget bindingMaterialButton;
    //记录Tab位置
    int _tabID = 1;
    //系统按钮的绑定
    UISceneWidget bindingShopBuyButton;
    UISceneWidget bindingSuperMarketBuyButton;
    UISceneWidget bindingContractButton;
    UISceneWidget bindingMainCityButton;

    //物品栏列
    int _column = 6;
    //物品栏行
    int _line = 6;
    //格子大小
    float _gridSize;
    //格子预制体
    GameObject grid;
    //物品预制体
    GameObject item;
    //格子二维数组
    GameObject[,] _grids;

    #region 物品数组
    int[] _itemWeapon = { 2014, 2015, 2026, 2027, 2038, 2039, 2050, 2051, 2062, 2063 };
    int[] _itemEquipment = { 2105, 2110, 2115, 2120, 2125 };
    int[] _itemConsumable = { 2206, 2207, 2208, 2209, 2210, 2211 };
    int[] _itemMaterial = { 2301, 2302, 2303, 2304, 2305, 2307 };
    #endregion

    //充值窗口
    GameObject _rechargeFrame;
    //充值关闭
    GameObject _rechargeCloseButton;
    //确认充值
    GameObject _rechargeConfirmButton;
    //充值输入框
    InputField _rechargeInputField;
    //充值显示内容
    Text _rechargeContent;
    //临时存储充值内容
    string _tempContent;
    //充值窗口按钮绑定
    UISceneWidget bindingRechargeCloseButton;
    UISceneWidget bindingRechargeConfirmButton;

    //带道具装备格对象表
    List<GameObject> itemList;

    #region 契约套餐系列相关
    GameObject mask;

    int projectID;
    int CardCount;
    GameObject controllerEX;
    GameObject superMarketParent;
    GameObject listParent;
    GameObject project01;
    GameObject project02;
    GameObject project03;
    GameObject project04;
    GameObject project05;
    Text projectMessage;
    GameObject ProjectConfirmFrame;
    GameObject ConfirmOK;
    GameObject CancelNO;

    GameObject getCharaFrame;
    GameObject propertyUpFrame;
    GameObject getFrame;
    GameObject upFrame;
    GameObject StateUp;
    Text StateUpText;

    GameObject getItemFrame;
    GameObject getItemFrameOK;
    #endregion

    /// <summary>
    /// 赋值
    /// </summary>
    private void Awake()
    {
        //充值窗口
        _rechargeFrame = transform.Find(ConstData.RechargeFrame).gameObject;
        //充值关闭
        _rechargeCloseButton = transform.Find(ConstData.RechargeCloseButton).gameObject;
        //确认充值
        _rechargeConfirmButton = transform.Find(ConstData.RechargeConfirmButton).gameObject;
        //充值输入框
        _rechargeInputField = transform.Find(ConstData.RechargeInputField).GetComponent<InputField>();
        //格子
        grid = ResourcesManager.Instance.FindUIPrefab(ConstData.Grid);
        //光圈
        _aperture = ResourcesManager.Instance.FindUIPrefab(ConstData.pitchOn);

        _diamonds = transform.Find(ConstData.GameArea_Diamonds).GetComponent<Text>();

        _shopBuyButton = transform.Find(ConstData.SystemArea_ShopBuyButton).gameObject;
        _superMarketBuyButton = transform.Find(ConstData.SystemArea_SuperMarketBuyButton).gameObject;
        _contractButton = transform.Find(ConstData.SystemArea_ContractButton).gameObject;
        _mainCityButton = transform.Find(ConstData.SystemArea_MainCityButton).gameObject;

        _weapon = transform.Find(ConstData.ShoppingMall_Weapon).gameObject;
        _equipment = transform.Find(ConstData.ShoppingMall_Equipment).gameObject;
        _consumable = transform.Find(ConstData.ShoppingMall_Consumable).gameObject;
        _material = transform.Find(ConstData.ShoppingMall_Material).gameObject;

        confirmPurchaseButton = transform.Find(ConstData.ShoppingMall_ConfirmButton).gameObject;
        _confirmFrame = transform.Find(ConstData.ConfirmFrame).gameObject;
        _confirm = transform.Find(ConstData.ConfirmFrame_ConfirmButton).gameObject;
        _cancel = transform.Find(ConstData.ConfirmFrame_CancelButton).gameObject;
        _contentText = transform.Find(ConstData.ConfirmFrame_ContentText).GetComponent<Text>();

        listContent = transform.Find(ConstData.ControllerArea_ListContent2).GetComponent<RectTransform>();

        _textContent = transform.Find(ConstData.ShoppingMalTextContent).GetComponent<Text>();

        _gridSize = grid.GetComponent<RectTransform>().sizeDelta.x;
        //物品
        item = ResourcesManager.Instance.FindUIPrefab(ConstData.ItemIcon);
        //实例化格子
        InstantiationGrid();

        //绑定
        bindingConfirmPurchaseButton = UISceneWidget.Get(confirmPurchaseButton);
        if (bindingConfirmPurchaseButton != null) { bindingConfirmPurchaseButton.PointerClick += ConfirmPurchaseFunc; }

        bindingShopBuyButton = UISceneWidget.Get(_shopBuyButton);
        if (bindingShopBuyButton != null) { bindingShopBuyButton.PointerClick += ShopBuyButtonFunc; }
        bindingSuperMarketBuyButton = UISceneWidget.Get(_superMarketBuyButton);
        if (bindingSuperMarketBuyButton != null) { bindingSuperMarketBuyButton.PointerClick += SuperMarketBuyButtonFunc; }

        bindingContractButton = UISceneWidget.Get(_contractButton);
        if (bindingContractButton != null) { bindingContractButton.PointerClick += ContractButtonFunc; }
        bindingMainCityButton = UISceneWidget.Get(_mainCityButton);
        if (bindingMainCityButton != null) { bindingMainCityButton.PointerClick += MainCityButtonFunc; }

        //取消与确定
        bindingConfirm = UISceneWidget.Get(_confirm);
        if (bindingConfirm != null) { bindingConfirm.PointerClick += ConfirmFunc; }
        bindingCancel = UISceneWidget.Get(_cancel);
        if (bindingCancel != null) { bindingCancel.PointerClick += CancelFunc; }

        bindingWeaponButton = UISceneWidget.Get(_weapon);
        if (bindingWeaponButton != null) { bindingWeaponButton.PointerClick += WeaponTab; }
        bindingEquipmentButton = UISceneWidget.Get(_equipment);
        if (bindingEquipmentButton != null) { bindingEquipmentButton.PointerClick += EquipmentTab; }
        bindingConsumableButton = UISceneWidget.Get(_consumable);
        if (bindingConsumableButton != null) { bindingConsumableButton.PointerClick += ConsumableTab; }
        bindingMaterialButton = UISceneWidget.Get(_material);
        if (bindingMaterialButton != null) { bindingMaterialButton.PointerClick += MaterialTab; }
        //充值界面按钮
        bindingRechargeCloseButton = UISceneWidget.Get(_rechargeCloseButton);
        if (bindingRechargeCloseButton != null) { bindingRechargeCloseButton.PointerClick += CancelRechargeFunc; }
        bindingRechargeConfirmButton = UISceneWidget.Get(_rechargeConfirmButton);
        if (bindingRechargeConfirmButton != null) { bindingRechargeConfirmButton.PointerClick += ConfirmRechargeFunc; }

        //显示充值结果
        _rechargeContent = transform.Find(ConstData.RechargeContent).GetComponent<Text>();
        _tempContent = _rechargeContent.text;
        //显示钻石数
        _diamonds.text = CurrencyManager.Instance.DiamondDisplay();

        //列表重置
        itemList = new List<GameObject>();
        itemList.Clear();

        //契约套餐相关
        mask = transform.Find("Mask").gameObject;
        UISceneWidget maskClick = UISceneWidget.Get(mask);
        maskClick.PointerClick += CloseMask;
        CardCount = 0;
        controllerEX = transform.Find("ControllerExArea").gameObject;
        superMarketParent = transform.Find(ConstData.ShoppingMall_ItemListBG).gameObject;
        listParent = transform.Find(ConstData.ShoppingMall_ContractSetListBG).gameObject;
        project01 = transform.Find(ConstData.ShoppingMall_CharaVIP).gameObject;
        project02 = transform.Find(ConstData.ShoppingMall_CharaFive).gameObject;
        project03 = transform.Find(ConstData.ShoppingMall_CharaLevelUP).gameObject;
        project04 = transform.Find(ConstData.ShoppingMall_CharaToBeStronge).gameObject;
        project05 = transform.Find(ConstData.ShoppingMall_Fruit).gameObject;

        ProjectConfirmFrame = transform.Find(ConstData.ContractSetConfirmFrame).gameObject;
        projectMessage = ProjectConfirmFrame.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        ConfirmOK = ProjectConfirmFrame.transform.GetChild(2).gameObject;
        CancelNO = ProjectConfirmFrame.transform.GetChild(3).gameObject;

        UISceneWidget P1Click = UISceneWidget.Get(project01);
        P1Click.PointerClick += SetClick;
        P1Click.Drag += OnDrag;
        UISceneWidget P2Click = UISceneWidget.Get(project02);
        P2Click.PointerClick += SetClick;
        P2Click.Drag += OnDrag;
        UISceneWidget P3Click = UISceneWidget.Get(project03);
        P3Click.PointerClick += SetClick;
        P3Click.Drag += OnDrag;
        UISceneWidget P4Click = UISceneWidget.Get(project04);
        P4Click.PointerClick += SetClick;
        P4Click.Drag += OnDrag;
        UISceneWidget P5Click = UISceneWidget.Get(project05);
        P5Click.PointerClick += SetClick;
        P5Click.Drag += OnDrag;

        getCharaFrame = ResourcesManager.Instance.FindUIPrefab(ConstData.getCharaFrame);
        propertyUpFrame = ResourcesManager.Instance.FindUIPrefab(ConstData.propertyUpFrame);
        StateUp = transform.Find(ConstData.StateUpText).gameObject;
        StateUpText = StateUp.GetComponent<Text>();

        getItemFrame = transform.Find(ConstData.GetItemFrame).gameObject;
        getItemFrameOK = getItemFrame.transform.GetChild(2).gameObject;
        UISceneWidget getItemFrameOKClick = UISceneWidget.Get(getItemFrameOK);
        getItemFrameOKClick.PointerClick += CloseGetFrame;

        ProjectList_Drag = transform.Find(ConstData.ShoppingMall_ContractSetListBG).gameObject;
    }

    //进入
    public void OnEntering()
    {
        gameObject.SetActive(true);
        ItemInstantiationFunc(_itemWeapon, ConstData.EquipmentType);
        _textContent.text = "请选择物品";
        //分类重置
        _weapon.GetComponent<Toggle>().isOn = true;
        _equipment.GetComponent<Toggle>().isOn = false;
        _consumable.GetComponent<Toggle>().isOn = false;
        _material.GetComponent<Toggle>().isOn = false;
    }
    //退出
    public void OnExiting()
    {
        gameObject.SetActive(false);
        ItemRecycleFunc();
    }
    //暂停
    public void OnPausing()
    {
        gameObject.SetActive(false);
    }
    //唤醒
    public void OnResuming()
    {
        gameObject.SetActive(true);
    }

    #region 契约套餐系列
    void CloseMask(PointerEventData data)
    {
        mask.SetActive(false);
        if (getFrame != null)
        {
            if (getFrame.transform.childCount == 2)
            {
                getFrame.transform.GetChild(1).GetComponent<Animator>().SetBool("isWait", false);
                ObjectPoolManager.Instance.RecycleMyGameObject(getFrame.transform.GetChild(1).gameObject);
                ObjectPoolManager.Instance.RecycleMyGameObject(getFrame);
            }
            else if (getFrame.transform.childCount == 3)
            {
                getFrame.transform.GetChild(1).GetComponent<Animator>().SetBool("isWait", false);
                ObjectPoolManager.Instance.RecycleMyGameObject(getFrame.transform.GetChild(1).gameObject);
                getFrame.transform.GetChild(1).GetComponent<Animator>().SetBool("isWait", false);
                ObjectPoolManager.Instance.RecycleMyGameObject(getFrame.transform.GetChild(1).gameObject);
                ObjectPoolManager.Instance.RecycleMyGameObject(getFrame);
            }
            else if (getFrame.transform.childCount == 4)
            {
                getFrame.transform.GetChild(1).GetComponent<Animator>().SetBool("isWait", false);
                ObjectPoolManager.Instance.RecycleMyGameObject(getFrame.transform.GetChild(1).gameObject);
                getFrame.transform.GetChild(1).GetComponent<Animator>().SetBool("isWait", false);
                ObjectPoolManager.Instance.RecycleMyGameObject(getFrame.transform.GetChild(1).gameObject);
                getFrame.transform.GetChild(1).GetComponent<Animator>().SetBool("isWait", false);
                ObjectPoolManager.Instance.RecycleMyGameObject(getFrame.transform.GetChild(1).gameObject);
                ObjectPoolManager.Instance.RecycleMyGameObject(getFrame);
            }
            else if (getFrame.transform.childCount == 5)
            {
                getFrame.transform.GetChild(1).GetComponent<Animator>().SetBool("isWait", false);
                ObjectPoolManager.Instance.RecycleMyGameObject(getFrame.transform.GetChild(1).gameObject);
                getFrame.transform.GetChild(1).GetComponent<Animator>().SetBool("isWait", false);
                ObjectPoolManager.Instance.RecycleMyGameObject(getFrame.transform.GetChild(1).gameObject);
                getFrame.transform.GetChild(1).GetComponent<Animator>().SetBool("isWait", false);
                ObjectPoolManager.Instance.RecycleMyGameObject(getFrame.transform.GetChild(1).gameObject);
                getFrame.transform.GetChild(1).GetComponent<Animator>().SetBool("isWait", false);
                ObjectPoolManager.Instance.RecycleMyGameObject(getFrame.transform.GetChild(1).gameObject);
                ObjectPoolManager.Instance.RecycleMyGameObject(getFrame);
            }
            else if (getFrame.transform.childCount == 6)
            {
                getFrame.transform.GetChild(1).GetComponent<Animator>().SetBool("isWait", false);
                ObjectPoolManager.Instance.RecycleMyGameObject(getFrame.transform.GetChild(1).gameObject);
                getFrame.transform.GetChild(1).GetComponent<Animator>().SetBool("isWait", false);
                ObjectPoolManager.Instance.RecycleMyGameObject(getFrame.transform.GetChild(1).gameObject);
                getFrame.transform.GetChild(1).GetComponent<Animator>().SetBool("isWait", false);
                ObjectPoolManager.Instance.RecycleMyGameObject(getFrame.transform.GetChild(1).gameObject);
                getFrame.transform.GetChild(1).GetComponent<Animator>().SetBool("isWait", false);
                ObjectPoolManager.Instance.RecycleMyGameObject(getFrame.transform.GetChild(1).gameObject);
                getFrame.transform.GetChild(1).GetComponent<Animator>().SetBool("isWait", false);
                ObjectPoolManager.Instance.RecycleMyGameObject(getFrame.transform.GetChild(1).gameObject);
                ObjectPoolManager.Instance.RecycleMyGameObject(getFrame);
            }
        }
        if (upFrame != null)
        {
            if (upFrame.transform.childCount > 1)
            {
                upFrame.transform.GetChild(1).GetComponent<Animator>().SetBool("isWait", false);
                ObjectPoolManager.Instance.RecycleMyGameObject(upFrame.transform.GetChild(1).gameObject);
            }
            ObjectPoolManager.Instance.RecycleMyGameObject(upFrame);
        }
    }
    void CloseGetFrame(PointerEventData data)
    {
        getItemFrame.SetActive(false);
        if (getItemFrame.transform.childCount > 3)
        {
            ObjectPoolManager.Instance.RecycleMyGameObject(getItemFrame.transform.GetChild(3).gameObject);
        }
    }
    void SetClick(PointerEventData data)
    {
        switch (data.pointerEnter.gameObject.name)
        {
            case "project01":
                projectID = 1;
                ProjectConfirmFrame.SetActive(true);
                projectMessage.text = "[VIP契约招募]\n支付一张VIP招募券，进行一次特殊招募，获得稀有角色的概率提升至50%。";
                break;
            case "project02":
                projectID = 2;
                ProjectConfirmFrame.SetActive(true);
                projectMessage.text = "[契约招募]\n支付2640钻石，连续进行五次特殊招募，获得稀有角色的概率提升至20%。";
                break;
            case "project03":
                projectID = 3;
                ProjectConfirmFrame.SetActive(true);
                projectMessage.text = "[荣誉契约]\n支付1260钻石，可购买一份荣誉契约，契约可选择一名已拥有英雄，使其等级立即达到最高。";
                break;
            case "project04":
                projectID = 4;
                ProjectConfirmFrame.SetActive(true);
                projectMessage.text = "[符文契约]\n支付690钻石，可购买一份符文契约，可进行五次符文判定，" +
                    "每次判定成功则选中英雄的随机一项属性提升1-10点，该项目活动期间仅可购买一次。";
                break;
            case "project05":
                projectID = 5;
                ProjectConfirmFrame.SetActive(true);
                projectMessage.text = "[冒险者契约]\n支付4568钻石，可获得一份传说契约箱，箱内有全套5枚传说果实。";
                break;
        }
        //绑定按钮
        if (ConfirmOK.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget confirmOKClick = UISceneWidget.Get(ConfirmOK);
            confirmOKClick.PointerClick += Project_OK;
        }
        else
        {
            ConfirmOK.GetComponent<UISceneWidget>().PointerClick += Project_OK;
        }
        if (CancelNO.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget CancelNOClick = UISceneWidget.Get(CancelNO);
            CancelNOClick.PointerClick += Project_NO;
        }
        else
        {
            CancelNO.GetComponent<UISceneWidget>().PointerClick += Project_NO;
        }
    }
    void Project_OK(PointerEventData data)
    {
        switch (projectID)
        {
            case 1:
                //数一下VIP券数量
                CardCount = 0;
                for (int i = 0; i < SQLiteManager.Instance.bagDataSource.Count; i++)
                {
                    if (SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Material == 2307)
                    {
                        CardCount++;
                    }
                }
                //一级判断：VIP券是否够
                if (CardCount > 0)
                {
                    //二级判断：角色列表是否还有空位
                    if (SQLiteManager.Instance.playerDataSource.Count < 15)
                    {
                        //删除VIP招募券
                        CostCard();
                        //生成招募界面
                        getFrame = ObjectPoolManager.Instance.InstantiateMyGameObject(getCharaFrame);
                        getFrame.transform.position = getCharaFrame.transform.position;
                        //生成招募到的角色
                        int getPlayerID = RandomManager.Instance.GetRandomCharacter(CharacterFieldType.VIP);
                        GameObject ply = ObjectPoolManager.Instance.InstantiateMyGameObject
                            (ResourcesManager.Instance.FindPlayerPrefab((getPlayerID).ToString()));
                        ply.transform.parent = getFrame.transform;
                        ply.transform.position = getFrame.transform.GetChild(0).transform.position;
                        ply.GetComponent<Animator>().SetBool("isWait", true);
                        //获得角色存入字典和数据库表
                        GetChara(getPlayerID);
                        //打开遮罩
                        mask.SetActive(true);
                        //按钮复位
                        ConfirmOK.SetActive(true);
                        CancelNO.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(225, -245);
                        //结束重置
                        ProjectConfirmFrame.SetActive(false);
                        ConfirmOK.GetComponent<UISceneWidget>().PointerClick -= Project_OK;
                        CancelNO.GetComponent<UISceneWidget>().PointerClick -= Project_NO;
                        projectID = 0;
                        //显示钻石数
                        _diamonds.text = CurrencyManager.Instance.DiamondDisplay();
                        //播放音效
                        AudioManager.Instance.PlayEffectMusic(SoundEffect.Clearing);
                    }
                    else
                    {
                        ConfirmOK.gameObject.SetActive(false);
                        CancelNO.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
                        projectMessage.text = "人数已满，招募失败。";
                    }
                }
                else
                {
                    ConfirmOK.gameObject.SetActive(false);
                    CancelNO.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
                    projectMessage.text = "VIP招募券不足。";
                }
                break;
            case 2:
                //一级判断：判断钻石是否足够
                if (CurrencyManager.Instance.diamond > 2640)
                {
                    //二级判断：角色列表是否还有空位
                    if (SQLiteManager.Instance.playerDataSource.Count <= 10)
                    {
                        //删除钻石
                        CurrencyManager.Instance.DiamondDecrease(2640);
                        //生成招募界面
                        getFrame = ObjectPoolManager.Instance.InstantiateMyGameObject(getCharaFrame);
                        getFrame.transform.position = getCharaFrame.transform.position;
                        //生成招募到的角色
                        //---1号---
                        int getPlayerID01 = RandomManager.Instance.GetRandomCharacter(CharacterFieldType.SuperMarket);
                        GameObject ply1 = ObjectPoolManager.Instance.InstantiateMyGameObject
                            (ResourcesManager.Instance.FindPlayerPrefab((getPlayerID01).ToString()));
                        ply1.transform.parent = getFrame.transform;
                        ply1.transform.position = getFrame.transform.GetChild(0).transform.position;
                        ply1.GetComponent<Animator>().SetBool("isWait", true);
                        //---2号---
                        int getPlayerID02 = RandomManager.Instance.GetRandomCharacter(CharacterFieldType.SuperMarket);
                        GameObject ply2 = ObjectPoolManager.Instance.InstantiateMyGameObject
                            (ResourcesManager.Instance.FindPlayerPrefab((getPlayerID02).ToString()));
                        ply2.transform.parent = getFrame.transform;
                        ply2.transform.position = getFrame.transform.GetChild(0).transform.position + new Vector3(1.0f, 0, 0);
                        ply2.GetComponent<Animator>().SetBool("isWait", true);
                        //---3号---
                        int getPlayerID03 = RandomManager.Instance.GetRandomCharacter(CharacterFieldType.SuperMarket);
                        GameObject ply3 = ObjectPoolManager.Instance.InstantiateMyGameObject
                            (ResourcesManager.Instance.FindPlayerPrefab((getPlayerID03).ToString()));
                        ply3.transform.parent = getFrame.transform;
                        ply3.transform.position = getFrame.transform.GetChild(0).transform.position - new Vector3(1.0f, 0, 0);
                        ply3.GetComponent<Animator>().SetBool("isWait", true);
                        //---4号---
                        int getPlayerID04 = RandomManager.Instance.GetRandomCharacter(CharacterFieldType.SuperMarket);
                        GameObject ply4 = ObjectPoolManager.Instance.InstantiateMyGameObject
                            (ResourcesManager.Instance.FindPlayerPrefab((getPlayerID04).ToString()));
                        ply4.transform.parent = getFrame.transform;
                        ply4.transform.position = getFrame.transform.GetChild(0).transform.position + new Vector3(2.0f, 0, 0);
                        ply4.GetComponent<Animator>().SetBool("isWait", true);
                        //---5号---
                        int getPlayerID05 = RandomManager.Instance.GetRandomCharacter(CharacterFieldType.SuperMarket);
                        GameObject ply5 = ObjectPoolManager.Instance.InstantiateMyGameObject
                            (ResourcesManager.Instance.FindPlayerPrefab((getPlayerID05).ToString()));
                        ply5.transform.parent = getFrame.transform;
                        ply5.transform.position = getFrame.transform.GetChild(0).transform.position - new Vector3(2.0f, 0, 0);
                        ply5.GetComponent<Animator>().SetBool("isWait", true);

                        //获得角色存入字典和数据库表
                        GetChara(getPlayerID01);
                        GetChara(getPlayerID02);
                        GetChara(getPlayerID03);
                        GetChara(getPlayerID04);
                        GetChara(getPlayerID05);
                        //打开遮罩
                        mask.SetActive(true);
                        //按钮复位
                        ConfirmOK.SetActive(true);
                        CancelNO.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(225, -245);
                        //结束重置
                        ProjectConfirmFrame.SetActive(false);
                        ConfirmOK.GetComponent<UISceneWidget>().PointerClick -= Project_OK;
                        CancelNO.GetComponent<UISceneWidget>().PointerClick -= Project_NO;
                        projectID = 0;
                        //显示钻石数
                        _diamonds.text = CurrencyManager.Instance.DiamondDisplay();
                        //播放音效
                        AudioManager.Instance.PlayEffectMusic(SoundEffect.Clearing);
                    }
                    else
                    {
                        ConfirmOK.gameObject.SetActive(false);
                        CancelNO.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
                        projectMessage.text = "角色列表空位不足，招募失败，请确保您所拥有的角色在10人或10人以下。";
                    }
                }
                else
                {
                    ConfirmOK.gameObject.SetActive(false);
                    CancelNO.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
                    projectMessage.text = "钻石不足，请充值。";
                }
                break;
            case 3:
                //一级判断：判断钻石是否足够
                if (CurrencyManager.Instance.diamond > 1260)
                {
                    if (SQLiteManager.Instance.bagDataSource[36].Bag_Material == 0)
                    {
                        //扣除钻石
                        CurrencyManager.Instance.DiamondDecrease(1260);
                        //获得道具
                        GetItem(2308);
                        //按钮复位
                        ConfirmOK.SetActive(true);
                        CancelNO.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(225, -245);
                        //结束重置
                        ProjectConfirmFrame.SetActive(false);
                        ConfirmOK.GetComponent<UISceneWidget>().PointerClick -= Project_OK;
                        CancelNO.GetComponent<UISceneWidget>().PointerClick -= Project_NO;
                        projectID = 0;
                        //打开获得道具窗口
                        getItemFrame.SetActive(true);
                        GameObject tempitem = ObjectPoolManager.Instance.InstantiateMyGameObject
                            (ResourcesManager.Instance.FindUIPrefab(ConstData.GridEx));
                        tempitem.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite("2308");
                        tempitem.transform.parent = getItemFrame.transform;
                        tempitem.transform.localScale = new Vector3(1, 1, 1);
                        tempitem.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                        //删除库存
                        SQLiteManager.Instance.itemDataSource[2308].Stockpile -= 1;
                        SQLiteManager.Instance.UpdataDataFromTable
                            (ConstData.Item, "item_Stockpile", SQLiteManager.Instance.itemDataSource[2308].Stockpile,
                            "ID", 2308);
                        //显示钻石数
                        _diamonds.text = CurrencyManager.Instance.DiamondDisplay();
                        //播放音效
                        AudioManager.Instance.PlayEffectMusic(SoundEffect.Clearing);
                    }
                    else
                    {
                        ConfirmOK.gameObject.SetActive(false);
                        CancelNO.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
                        projectMessage.text = "背包空间不足，购买失败。";
                    }

                }
                else
                {
                    ConfirmOK.gameObject.SetActive(false);
                    CancelNO.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
                    projectMessage.text = "钻石不足，请充值。";
                }
                break;
            case 4:
                //一级判断：判断钻石是否足够
                if (CurrencyManager.Instance.diamond > 690)
                {
                    if (SQLiteManager.Instance.itemDataSource[2309].Stockpile > 0)
                    {
                        if (SQLiteManager.Instance.bagDataSource[36].Bag_Material == 0)
                        {
                            //扣除钻石
                            CurrencyManager.Instance.DiamondDecrease(690);
                            //获得道具
                            GetItem(2309);
                            //按钮复位
                            ConfirmOK.SetActive(true);
                            CancelNO.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(225, -245);
                            //结束重置
                            ProjectConfirmFrame.SetActive(false);
                            ConfirmOK.GetComponent<UISceneWidget>().PointerClick -= Project_OK;
                            CancelNO.GetComponent<UISceneWidget>().PointerClick -= Project_NO;
                            projectID = 0;
                            //打开获得道具窗口
                            getItemFrame.SetActive(true);
                            GameObject tempitem = ObjectPoolManager.Instance.InstantiateMyGameObject
                                (ResourcesManager.Instance.FindUIPrefab(ConstData.GridEx));
                            tempitem.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite("2309");
                            tempitem.transform.parent = getItemFrame.transform;
                            tempitem.transform.localScale = new Vector3(1, 1, 1);
                            tempitem.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                            //删除库存
                            SQLiteManager.Instance.itemDataSource[2309].Stockpile -= 1;
                            SQLiteManager.Instance.UpdataDataFromTable
                                (ConstData.Item, "item_Stockpile", SQLiteManager.Instance.itemDataSource[2309].Stockpile,
                                "ID", 2309);
                            //显示钻石数
                            _diamonds.text = CurrencyManager.Instance.DiamondDisplay();
                            //播放音效
                            AudioManager.Instance.PlayEffectMusic(SoundEffect.Clearing);
                        }
                        else
                        {
                            ConfirmOK.gameObject.SetActive(false);
                            CancelNO.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
                            projectMessage.text = "背包空间不足，购买失败。";
                        }
                    }
                    else
                    {
                        ConfirmOK.gameObject.SetActive(false);
                        CancelNO.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
                        projectMessage.text = "活动结束，请勿重复购买。";
                    }
                }
                else
                {
                    ConfirmOK.gameObject.SetActive(false);
                    CancelNO.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
                    projectMessage.text = "钻石不足，请充值。";
                }
                break;
            case 5:
                //一级判断：判断钻石是否足够
                if (CurrencyManager.Instance.diamond > 4568)
                {
                    if (SQLiteManager.Instance.itemDataSource[2310].Stockpile > 0)
                    {
                        if (SQLiteManager.Instance.bagDataSource[36].Bag_Material == 0)
                        {
                            //扣除钻石
                            CurrencyManager.Instance.DiamondDecrease(4568);
                            //获得道具
                            GetItem(2310);
                            //按钮复位
                            ConfirmOK.SetActive(true);
                            CancelNO.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(225, -245);
                            //结束重置
                            ProjectConfirmFrame.SetActive(false);
                            ConfirmOK.GetComponent<UISceneWidget>().PointerClick -= Project_OK;
                            CancelNO.GetComponent<UISceneWidget>().PointerClick -= Project_NO;
                            projectID = 0;
                            //打开获得道具窗口
                            getItemFrame.SetActive(true);
                            GameObject tempitem = ObjectPoolManager.Instance.InstantiateMyGameObject
                                (ResourcesManager.Instance.FindUIPrefab(ConstData.GridEx));
                            tempitem.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite("2310");
                            tempitem.transform.parent = getItemFrame.transform;
                            tempitem.transform.localScale = new Vector3(1, 1, 1);
                            tempitem.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                            //删除库存
                            SQLiteManager.Instance.itemDataSource[2310].Stockpile -= 1;
                            SQLiteManager.Instance.UpdataDataFromTable
                                (ConstData.Item, "item_Stockpile", SQLiteManager.Instance.itemDataSource[2310].Stockpile,
                                "ID", 2310);
                            //显示钻石数
                            _diamonds.text = CurrencyManager.Instance.DiamondDisplay();
                            //播放音效
                            AudioManager.Instance.PlayEffectMusic(SoundEffect.Clearing);
                        }
                        else
                        {
                            ConfirmOK.gameObject.SetActive(false);
                            CancelNO.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
                            projectMessage.text = "背包空间不足，购买失败。";
                        }
                    }
                    else
                    {
                        ConfirmOK.gameObject.SetActive(false);
                        CancelNO.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
                        projectMessage.text = "仅新用户限购一次，请勿重复购买。";
                    }
                }
                else
                {
                    ConfirmOK.gameObject.SetActive(false);
                    CancelNO.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
                    projectMessage.text = "钻石不足，请充值。";
                }
                break;
        }
    }
    void Project_NO(PointerEventData data)
    {
        //按钮复位
        ConfirmOK.SetActive(true);
        CancelNO.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(225, -245);
        //关闭窗口和按钮解绑
        ProjectConfirmFrame.SetActive(false);
        ConfirmOK.GetComponent<UISceneWidget>().PointerClick -= Project_OK;
        CancelNO.GetComponent<UISceneWidget>().PointerClick -= Project_NO;
        projectID = 0;
    }
    #endregion

    /// <summary>
    /// 物品实例化方法
    /// </summary>
    void ItemInstantiationFunc(int[] itemArr, string type)
    {
        //列表重置
        itemList.Clear();
        //记录格子号
        int number = 0;
        for (int i = 0; i < _grids.GetLength(0); i++)
        {
            for (int j = 0; j < _grids.GetLength(1); j++)
            {
                if (_grids[i, j].transform.childCount > 0)
                {
                    return;
                }

                if (number >= itemArr.Length)
                {
                    number = 0;
                    return;
                }
                //生成物品
                GameObject tempitem = ObjectPoolManager.Instance.InstantiateMyGameObject(item);
                BagItem _tempBagItem;
                if (tempitem.GetComponent<BagItem>() == null)
                {
                    tempitem.AddComponent<BagItem>();
                }
                _tempBagItem = tempitem.GetComponent<BagItem>();
                switch (type)
                {
                    case ConstData.ItemType:
                        _tempBagItem.mydata_item = SQLiteManager.Instance.itemDataSource[itemArr[number]];
                        tempitem.tag = ConstData.ItemType;
                        break;
                    case ConstData.EquipmentType:
                        _tempBagItem.mydata_equipt = SQLiteManager.Instance.equipmentDataSource[itemArr[number]];
                        tempitem.tag = ConstData.EquipmentType;
                        break;
                }
                //设置物体属性
                tempitem.name = item.name;
                tempitem.transform.parent = _grids[i, j].transform;
                tempitem.transform.localPosition = Vector3.zero;
                tempitem.transform.localScale = Vector3.one;
                tempitem.GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite(itemArr[number].ToString());
                UISceneWidget itemClickEvent = UISceneWidget.Get(tempitem);
                if (itemClickEvent != null)
                {
                    itemClickEvent.PointerClick += SelectItemFunc;
                }
                //记录格子号
                number++;
                //添加列表
                itemList.Add(tempitem);
            }
        }
        ItemBarContentAdaptive();
    }

    /// <summary>
    /// 回收物品方法
    /// </summary>
    void ItemRecycleFunc()
    {
        for (int i = 0; i < _grids.GetLength(0); i++)
        {
            for (int j = 0; j < _grids.GetLength(1); j++)
            {
                if (_grids[i, j].transform.childCount < 1)
                {
                    return;
                }
                GameObject item = _grids[i, j].transform.GetChild(0).gameObject;
                item.GetComponent<UISceneWidget>().PointerClick -= SelectItemFunc;
                ObjectPoolManager.Instance.RecycleMyGameObject(item);
                //回收之前的光圈
                if (_bagItem != null)
                {
                    ObjectPoolManager.Instance.RecycleMyGameObject(_bagItem.transform.GetChild(0).gameObject);
                    _bagItem = null;
                }
            }
        }
    }

    /// <summary>
    /// 选中物品的方法
    /// </summary>
    void SelectItemFunc(PointerEventData data)
    {
        //回收之前的光圈
        if (_bagItem != null)
        {
            ObjectPoolManager.Instance.RecycleMyGameObject(_bagItem.transform.GetChild(0).gameObject);
        }
        //赋值新的物品
        _bagItem = data.pointerEnter.GetComponent<BagItem>();
        //临时存类型
        string temp = "";
        string[] _itemContent;

        if (data.pointerEnter.tag == ConstData.ItemType)
        {
            _price = _bagItem.mydata_item.item_Diamond;
            temp = _bagItem.mydata_item.item_Type == ConstData.ListType_Consumable ? "消耗品" : "材料";
            _itemContent = new string[] { "[", _bagItem.mydata_item.item_Name, "] ", "类型：", temp, " 价格：", _price.ToString(), "\n", _bagItem.mydata_item.item_Description };
        }
        else
        {
            switch (_bagItem.mydata_equipt.equipmentClass)
            {
                case ConstData.Berserker:
                    temp = "狂战士";
                    break;
                case ConstData.Caster:
                    temp = "魔法师";
                    break;
                case ConstData.Hunter:
                    temp = "猎人";
                    break;
                case ConstData.Knight:
                    temp = "骑士";
                    break;
                case ConstData.Saber:
                    temp = "剑士";
                    break;
            }
            _price = _bagItem.mydata_equipt.equipmentPrice;
            _itemContent = new string[] { "[", _bagItem.mydata_equipt.equipmentNmae, "] ", "装备职业：", temp, " 价格：", _price.ToString(), "\n", "HP：", _bagItem.mydata_equipt.equipment_HP.ToString(), "  AD：", _bagItem.mydata_equipt.equipment_AD.ToString(), "  AP：", _bagItem.mydata_equipt.equipment_AP.ToString(), "  DEF：", _bagItem.mydata_equipt.equipment_DEF.ToString(), "  RES：", _bagItem.mydata_equipt.equipment_RES.ToString() };
        }
        _textContent.text = StringSplicingTool.StringSplicing(_itemContent);
        GenerateAperture(_bagItem.transform);
    }

    /// <summary>
    /// 实例化格子
    /// </summary>
    void InstantiationGrid()
    {
        //初始化格子长度
        _grids = new GameObject[_column, _line];
        //生成格子(默认6*6 36个)
        for (int i = 0; i < _column; i++)
        {
            for (int j = 0; j < _line; j++)
            {
                _grids[i, j] = ObjectPoolManager.Instance.InstantiateMyGameObject(grid);
                //父物体
                RectTransform rt = _grids[i, j].GetComponent<RectTransform>();
                rt.name = grid.name;
                rt.parent = listContent;
                rt.anchorMax = new Vector2(0, 1);
                rt.anchorMin = new Vector2(0, 1);
                rt.anchoredPosition3D = new Vector3(j * _gridSize + _gridSize * 0.5f, -i * _gridSize - _gridSize * 0.5f, 0f);
                rt.localScale = Vector3.one;
            }
        }
    }

    /// <summary>
    /// 拼接名字
    /// </summary>
    void SplicingName()
    {
        if (_bagItem == null)
        {
            _contentText.text = "请选择物品";
            return;
        }
        string name = _bagItem.tag == ConstData.ItemType ? _bagItem.mydata_item.item_Name : _bagItem.mydata_equipt.equipmentNmae;
        string[] str = { "你确定要购买 ", "<color=#ff0000>", name, "</color>吗？" };
        _contentText.text = StringSplicingTool.StringSplicing(str);
    }

    /// <summary>
    /// 生成光圈
    /// </summary>
    /// <param 光圈父物体="parent"></param>
    void GenerateAperture(Transform parent)
    {
        GameObject obj = ObjectPoolManager.Instance.InstantiateMyGameObject(_aperture);
        obj.name = _aperture.name;
        obj.transform.parent = parent;
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// 确认购买
    /// </summary>
    void ConfirmPurchaseFunc(PointerEventData data)
    {
        SplicingName();
        _confirmFrame.SetActive(true);
    }

    /// <summary>
    /// 确认
    /// </summary>
    void ConfirmFunc(PointerEventData data)
    {
        if (CurrencyManager.Instance.diamond < _price)
        {
            _confirm.SetActive(false);
            _cancel.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
            _contentText.text = "所需钻石不足";
            return;
        }
        int itemID = 0;
        int gridNum = 0;
        string itemtType = "";
        //是否满格
        bool isFullLattice = false;

        foreach (int key in SQLiteManager.Instance.bagDataSource.Keys)
        {
            if (_tabID == 1)
            {
                if (SQLiteManager.Instance.bagDataSource[key].Bag_Weapon == 0)
                {
                    gridNum = SQLiteManager.Instance.bagDataSource[key].Bag_Grid;
                    itemtType = ConstData.Bag_Weapon;
                    itemID = _bagItem.mydata_equipt.equipment_Id;
                    SQLiteManager.Instance.bagDataSource[key].Bag_Weapon = itemID;
                    _confirm.SetActive(true);
                    _cancel.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(225, -245);
                    //播放音效
                    AudioManager.Instance.PlayEffectMusic(SoundEffect.Buy);
                    break;
                }
                else if (SQLiteManager.Instance.bagDataSource[key].Bag_Grid == _grids.Length)
                {
                    _confirm.SetActive(false);
                    _cancel.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
                    isFullLattice = true;
                    _contentText.text = "背包满了";
                }
            }
            else if (_tabID == 2)
            {
                if (SQLiteManager.Instance.bagDataSource[key].Bag_Equipment == 0)
                {
                    gridNum = SQLiteManager.Instance.bagDataSource[key].Bag_Grid;
                    itemtType = ConstData.Bag_Equipment;
                    itemID = _bagItem.mydata_equipt.equipment_Id;
                    SQLiteManager.Instance.bagDataSource[key].Bag_Equipment = itemID;
                    _confirm.SetActive(true);
                    _cancel.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(225, -245);
                    //播放音效
                    AudioManager.Instance.PlayEffectMusic(SoundEffect.Buy);
                    break;
                }
                else if (SQLiteManager.Instance.bagDataSource[key].Bag_Grid == _grids.Length)
                {
                    _confirm.SetActive(false);
                    _cancel.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
                    isFullLattice = true;
                    _contentText.text = "背包满了";
                }
            }
            else if (_tabID == 3)
            {
                if (SQLiteManager.Instance.bagDataSource[key].Bag_Consumable == 0)
                {
                    gridNum = SQLiteManager.Instance.bagDataSource[key].Bag_Grid;
                    itemtType = ConstData.Bag_Consumable;
                    itemID = _bagItem.mydata_item.item_Id;
                    SQLiteManager.Instance.bagDataSource[key].Bag_Consumable = itemID;
                    _confirm.SetActive(true);
                    _cancel.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(225, -245);
                    //播放音效
                    AudioManager.Instance.PlayEffectMusic(SoundEffect.Buy);
                    break;
                }
                else if (SQLiteManager.Instance.bagDataSource[key].Bag_Grid == _grids.Length)
                {
                    _confirm.SetActive(false);
                    _cancel.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
                    isFullLattice = true;
                    _contentText.text = "背包满了";
                }
            }
            else if (_tabID == 4)
            {
                if (SQLiteManager.Instance.bagDataSource[key].Bag_Material == 0)
                {
                    gridNum = SQLiteManager.Instance.bagDataSource[key].Bag_Grid;
                    itemtType = ConstData.Bag_Material;
                    itemID = _bagItem.mydata_item.item_Id;
                    SQLiteManager.Instance.bagDataSource[key].Bag_Material = itemID;
                    _confirm.SetActive(true);
                    _cancel.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(225, -245);
                    //播放音效
                    AudioManager.Instance.PlayEffectMusic(SoundEffect.Buy);
                    break;
                }
                else if (SQLiteManager.Instance.bagDataSource[key].Bag_Grid == _grids.Length)
                {
                    _confirm.SetActive(false);
                    _cancel.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
                    isFullLattice = true;
                    _contentText.text = "背包满了";
                }
            }
        }

        if (!isFullLattice)
        {
            SQLiteManager.Instance.UpdataDataFromTable(ConstData.Bag, itemtType, itemID, ConstData.Bag_Grid, gridNum);
            CurrencyManager.Instance.DiamondDecrease(_price);
            _diamonds.text = CurrencyManager.Instance.DiamondDisplay();
            _confirmFrame.SetActive(false);
        }
    }

    /// <summary>
    /// 取消
    /// </summary>
    void CancelFunc(PointerEventData data)
    {
        _confirm.SetActive(true);
        _cancel.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(225, -245);
        _confirmFrame.SetActive(false);
    }

    /// <summary>
    /// 切换商店界面按钮
    /// </summary>
    void ShopBuyButtonFunc(PointerEventData data)
    {
        //控制区
        superMarketParent.SetActive(true);
        listParent.SetActive(false);
        //控制附属区
        controllerEX.SetActive(true);
        //确认按钮
        transform.Find("ConfirmButton").gameObject.SetActive(true);
        //更改文字
        _textContent.gameObject.transform.parent.gameObject.SetActive(true);
    }

    /// <summary>
    /// 切换商店充值界面按钮
    /// </summary>
    void SuperMarketBuyButtonFunc(PointerEventData data)
    {
        _rechargeFrame.SetActive(true);
    }

    /// <summary>
    /// 切换招募内容按钮
    /// </summary>
    void ContractButtonFunc(PointerEventData data)
    {
        //控制区
        superMarketParent.SetActive(false);
        listParent.SetActive(true);
        //控制附属区
        controllerEX.SetActive(false);
        //确认按钮
        transform.Find("ConfirmButton").gameObject.SetActive(false);
        //更改文字
        _textContent.gameObject.transform.parent.gameObject.SetActive(false);
    }

    /// <summary>
    /// 武器按钮
    /// </summary>
    void WeaponTab(PointerEventData data)
    {
        _tabID = 1;
        ItemRecycleFunc();
        ItemInstantiationFunc(_itemWeapon, ConstData.EquipmentType);
    }
    /// <summary>
    /// 装备按钮
    /// </summary>
    void EquipmentTab(PointerEventData data)
    {
        _tabID = 2;
        ItemRecycleFunc();
        ItemInstantiationFunc(_itemEquipment, ConstData.EquipmentType);
    }
    /// <summary>
    /// 消耗品按钮
    /// </summary>
    void ConsumableTab(PointerEventData data)
    {
        _tabID = 3;
        ItemRecycleFunc();
        ItemInstantiationFunc(_itemConsumable, ConstData.ItemType);
    }
    /// <summary>
    /// 材料按钮
    /// </summary>
    void MaterialTab(PointerEventData data)
    {
        _tabID = 4;
        ItemRecycleFunc();
        ItemInstantiationFunc(_itemMaterial, ConstData.ItemType);
    }

    /// <summary>
    /// 确认充值
    /// </summary>
    /// <param name="data"></param>
    void ConfirmRechargeFunc(PointerEventData data)
    {
        foreach (int id in SQLiteManager.Instance.diamondCode.Keys)
        {
            if (_rechargeInputField.text == SQLiteManager.Instance.diamondCode[id].Code)
            {
                if (SQLiteManager.Instance.diamondCode[id].Stockpile < 1)
                {
                    //充值失败
                    RechargeFail("<color=#ff0000>充值失败！</color>");
                    return;
                }
                CurrencyManager.Instance.DiamondIncrease(SQLiteManager.Instance.diamondCode[id].Diamond);
                _rechargeInputField.text = "请输入兑换码...";
                _diamonds.text = CurrencyManager.Instance.DiamondDisplay();
                //写入表
                SQLiteManager.Instance.UpdataDataFromTable(ConstData.DiamondCode, ConstData.Stockpile, 0, "ID", id);
                SQLiteManager.Instance.diamondCode[id].Stockpile = 0;
                RechargeFail("<color=#ff0000>充值成功！</color>");
                //播放音效
                AudioManager.Instance.PlayEffectMusic(SoundEffect.FoodCure);
                return;
            }
        }
        //没有对应码
        RechargeFail("<color=#ff0000>充值失败！</color>");
    }

    /// <summary>
    /// 取消充值
    /// </summary>
    /// <param name="data"></param>
    void CancelRechargeFunc(PointerEventData data)
    {
        _rechargeFrame.SetActive(false);
    }

    /// <summary>
    /// 返回主城按钮
    /// </summary>
    void MainCityButtonFunc(PointerEventData data)
    {
        UIManager.Instance.PopUIStack();
    }

    /// <summary>
    /// 支付VIP卡
    /// </summary>
    void CostCard()
    {
        for (int i = 1; i < SQLiteManager.Instance.bagDataSource.Count; i++)
        {
            if (SQLiteManager.Instance.bagDataSource[i].Bag_Material == 2307)
            {
                SQLiteManager.Instance.bagDataSource[i].Bag_Material = SQLiteManager.Instance.bagDataSource[(i + 1)].Bag_Material;
                SQLiteManager.Instance.UpdataDataFromTable
                    (ConstData.Bag, ConstData.Bag_Material, SQLiteManager.Instance.bagDataSource[i + 1].Bag_Material, ConstData.Bag_Grid, i);
            }
        }
        SQLiteManager.Instance.bagDataSource[SQLiteManager.Instance.bagDataSource.Count].Bag_Material = 0;
        SQLiteManager.Instance.UpdataDataFromTable
                (ConstData.Bag, ConstData.Bag_Material, 0, ConstData.Bag_Grid, SQLiteManager.Instance.bagDataSource.Count);
    }
    /// <summary>
    /// 抽取获得的角色存入字典和数据库
    /// </summary>
    /// <param name="charaID"></param>
    void GetChara(int charaID)
    {
        int newPlayerID = SQLiteManager.Instance.playerDataSource.Keys.Last() + 1;
        CharacterListData getPlayerData = SQLiteManager.Instance.characterDataSource[charaID];
        PlayerData newPlayerData = new PlayerData();
        newPlayerData.player_Id = newPlayerID;
        newPlayerData.player_Name = getPlayerData.character_Name;
        newPlayerData.player_Class = getPlayerData.character_Class;
        newPlayerData.player_Description = getPlayerData.character_Description;
        newPlayerData.HP = getPlayerData.character_HP;
        newPlayerData.AD = getPlayerData.character_AD;
        newPlayerData.AP = getPlayerData.character_AP;
        newPlayerData.DEF = getPlayerData.character_DEF;
        newPlayerData.RES = getPlayerData.character_RES;
        newPlayerData.skillOneID = getPlayerData.character_SkillOneID;
        newPlayerData.skillTwoID = getPlayerData.character_SkillTwoID;
        newPlayerData.skillThreeID = getPlayerData.character_SkillThreeID;
        newPlayerData.EXHP = getPlayerData.character_EXHP;
        newPlayerData.EXAD = getPlayerData.character_EXAD;
        newPlayerData.EXAP = getPlayerData.character_EXAP;
        newPlayerData.EXDEF = getPlayerData.character_EXDEF;
        newPlayerData.EXRES = getPlayerData.character_EXRES;
        newPlayerData.Level = getPlayerData.character_Level;
        newPlayerData.EXP = getPlayerData.character_EXP;
        newPlayerData.Weapon = getPlayerData.character_Weapon;
        newPlayerData.Equipment = getPlayerData.character_Equipment;
        newPlayerData.GoldCoin = getPlayerData.GoldCoin;
        newPlayerData.Diamond = getPlayerData.Diamond;
        newPlayerData.PrefabsID = getPlayerData.PrefabsID;

        //存进字典
        SQLiteManager.Instance.playerDataSource.Add(newPlayerID, newPlayerData);
        //存到数据库
        SQLiteManager.Instance.InsetDataToTable
            (newPlayerID, getPlayerData.character_Name, getPlayerData.character_Class, getPlayerData.character_Description,
            getPlayerData.character_HP, getPlayerData.character_AD, getPlayerData.character_AP,
            getPlayerData.character_DEF, getPlayerData.character_RES,
            getPlayerData.character_SkillOneID, getPlayerData.character_SkillTwoID, getPlayerData.character_SkillThreeID,
            getPlayerData.character_EXHP, getPlayerData.character_EXAD, getPlayerData.character_EXAP,
            getPlayerData.character_EXDEF, getPlayerData.character_EXRES,
            getPlayerData.character_Weapon, getPlayerData.character_Equipment,
            getPlayerData.character_Level, getPlayerData.character_EXP,
            getPlayerData.GoldCoin, getPlayerData.Diamond, getPlayerData.PrefabsID);
    }
    /// <summary>
    /// 获取道具存入字典和数据库
    /// </summary>
    /// <param name="itemID"></param>
    void GetItem(int itemID)
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

    #region 解决拖拽和点击冲突的问题
    GameObject ProjectList_Drag;
    void OnBeginDrag(PointerEventData data)
    {
        ProjectList_Drag.GetComponent<ScrollRect>().OnBeginDrag(data);
    }
    void OnDrag(PointerEventData data)
    {
        ProjectList_Drag.GetComponent<ScrollRect>().OnDrag(data);
    }
    void OnEndDrag(PointerEventData data)
    {
        ProjectList_Drag.GetComponent<ScrollRect>().OnEndDrag(data);
    }
    #endregion

    /// <summary>
    /// 充值内容修改
    /// </summary>
    void RechargeFail(string content)
    {
        _rechargeContent.text = content;
        DelayShowRechargeContent();
    }
    void DelayShowRechargeContent()
    {
        vp_Timer.In(3f, new vp_Timer.Callback(delegate () 
        {
            _rechargeContent.text = _tempContent;
        }));
    }
	/// <summary>
    /// 装备格内容区域自适应长度
    /// </summary>
    /// <param 筛选模式="FilterClass"></param>
    void ItemBarContentAdaptive()
    {
        int tempheight = 1200;
        transform.Find(ConstData.ControllerArea_ListContent2).GetComponent<RectTransform>().sizeDelta = new Vector2(1200, tempheight);
        if (itemList.Count > ConstData.GridCount)
        {
            tempheight = 1200 + ((int)((itemList.Count - ConstData.GridCount) / 6) + 1) * 200;
            transform.Find(ConstData.ControllerArea_ListContent2).GetComponent<RectTransform>().sizeDelta = new Vector2(1200, tempheight);
        }
    }
	
}