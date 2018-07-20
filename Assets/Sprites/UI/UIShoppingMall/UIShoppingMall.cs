using System;
using System.Collections;
using System.Collections.Generic;
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
    int[] _itemMaterial = { 2301, 2302, 2303, 2304, 2305 };
    #endregion

    /// <summary>
    /// 赋值
    /// </summary>
    private void Awake()
    {
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

        //显示钻石数
        _diamonds.text = CurrencyManager.Instance.DiamondDisplay();
    }

    //进入
    public void OnEntering()
    {
        gameObject.SetActive(true);
        ItemInstantiationFunc(_itemWeapon, ConstData.EquipmentType);
        _textContent.text = "请选择物品";
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

    /// <summary>
    /// 物品实例化方法
    /// </summary>
    void ItemInstantiationFunc(int[] itemArr,string type)
    {
        //记录格子号
        int number = 0;
        for (int i = 0; i < _grids.GetLength(i); i++)
        {
            for (int j = 0; j < _grids.GetLength(i); j++)
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
                BagItem _bagItem;
                if (tempitem.GetComponent<BagItem>() == null)
                {
                    tempitem.AddComponent<BagItem>();
                }
                _bagItem = tempitem.GetComponent<BagItem>();
                switch (type)
                {
                    case ConstData.ItemType:
                        _bagItem.mydata_item = SQLiteManager.Instance.itemDataSource[itemArr[number]];
                        tempitem.tag = ConstData.ItemType;
                        break;
                    case ConstData.EquipmentType:
                        _bagItem.mydata_equipt = SQLiteManager.Instance.equipmentDataSource[itemArr[number]];
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
            }
        }
    }

    /// <summary>
    /// 回收物品方法
    /// </summary>
    void ItemRecycleFunc()
    {
        for (int i = 0; i < _grids.GetLength(0); i++)
        {
            for (int j = 0; j < _grids.GetLength(i); j++)
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
                    temp = "战士";
                    break;
                case ConstData.Caster:
                    temp = "法师";
                    break;
                case ConstData.Hunter:
                    temp = "弓箭手";
                    break;
                case ConstData.Knight:
                    temp = "骑士";
                    break;
                case ConstData.Saber:
                    temp = "剑士";
                    break;
            }
            _price = _bagItem.mydata_equipt.equipmentPrice;
            _itemContent = new string[] { "[", _bagItem.mydata_equipt.equipmentNmae, "] ", "装备职业：", temp, " 价格：", _price.ToString(), "\n", "HP：", _bagItem.mydata_equipt.equipment_HP.ToString(), "\nAD：", _bagItem.mydata_equipt.equipment_AD.ToString(), "  AP：", _bagItem.mydata_equipt.equipment_AP.ToString(), _bagItem.mydata_equipt.equipment_AD.ToString(), "  DEF：", _bagItem.mydata_equipt.equipment_DEF.ToString(), "  RES：", _bagItem.mydata_equipt.equipment_RES.ToString() };
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
        string[] str = { "你确定要购买 ", "<color=#ff0000>", name, "</color>", " 吗？" };
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
                    break;
                }
                else if (SQLiteManager.Instance.bagDataSource[key].Bag_Grid == _grids.Length)
                {
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
                    break;
                }
                else if (SQLiteManager.Instance.bagDataSource[key].Bag_Grid == _grids.Length)
                {
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
                    break;
                }
                else if (SQLiteManager.Instance.bagDataSource[key].Bag_Grid == _grids.Length)
                {
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
                    break;
                }
                else if (SQLiteManager.Instance.bagDataSource[key].Bag_Grid == _grids.Length)
                {
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
        _confirmFrame.SetActive(false);
    }

    /// <summary>
    /// 切换商店界面按钮
    /// </summary>
    void ShopBuyButtonFunc(PointerEventData data)
    {
        print("激活商店内容");
    }

    /// <summary>
    /// 切换商店充值界面按钮
    /// </summary>
    void SuperMarketBuyButtonFunc(PointerEventData data)
    {
        print("切换商店充值界面按钮");
    }

    /// <summary>
    /// 切换招募内容按钮
    /// </summary>
    void ContractButtonFunc(PointerEventData data)
    {
        print("激活招募内容");
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
    /// 返回主城按钮
    /// </summary>
    void MainCityButtonFunc(PointerEventData data)
    {
        UIManager.Instance.PopUIStack();
    }
}