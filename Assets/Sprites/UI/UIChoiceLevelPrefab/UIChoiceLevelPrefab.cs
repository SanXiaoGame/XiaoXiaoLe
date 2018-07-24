using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 关卡选择界面
/// </summary>
public class UIChoiceLevelPrefab : MonoBehaviour, IUIBase
{
    //消耗品栏数组
    GameObject[] _consumableBarArr;
    //确认窗口
    GameObject _confirmFrame;
    //确认窗口内容
    Text _contentText;
    //关卡数内容
    Text _checkpointNumberText;
    //选关消耗品栏内容
    Transform _choiceLevelContent;

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
    //选中光圈
    GameObject _aperture;
    //格子二维数组
    GameObject[,] _grids;
    //存储选中物品数据
    BagItem _bagItem;

    //消耗品列表
    List<int> _itemConsumableList = new List<int>();

    //确认窗口类型
    UIChoiceLevelConfirmFrameType _uiChoiceLevelConfirmFrameType;

    //关卡字典(临时使用)
    Dictionary<int, string> CheckpointDic = new Dictionary<int, string>();

    //临时存达到关卡
    int CheckpointDicNumber;

    private void Awake()
    {
        //关卡字典临死添加
        CheckpointDic.Add(1, "女装山脉");
        CheckpointDic.Add(2, "冲绳萝莉岛");
        CheckpointDic.Add(3, "Van♂更衣室");
        CheckpointDic.Add(4, "尻♂名山");
        CheckpointDic.Add(5, "魔女之家");
        CheckpointDic.Add(6, "洋馆");
        //以到达关卡数
        PlayerPrefs.SetInt("Checkpoint", 1);
        CheckpointDicNumber = PlayerPrefs.GetInt("Checkpoint");
        

        //返回主城按钮
        GameObject _mainCityButton = transform.Find(ConstData.SystemArea_MainCityIcon).gameObject;
        //绑定返回主城按钮
        UISceneWidget bindingCityButton = UISceneWidget.Get(_mainCityButton);
        if (bindingCityButton != null) { bindingCityButton.PointerClick += MainCityFunc; }
        //消耗量栏赋值
        Transform _choiceLevel_ConsumableBar = transform.Find(ConstData.ChoiceLevel_ConsumableBar);
        _consumableBarArr = new GameObject[_choiceLevel_ConsumableBar.childCount];
        for (int i = 0; i < _consumableBarArr.Length; i++)
        {
            _consumableBarArr[i] = _choiceLevel_ConsumableBar.GetChild(i).gameObject;
            //绑定消耗量栏按钮
            UISceneWidget bindingConsumableBar = UISceneWidget.Get(_consumableBarArr[i]);
            if (bindingConsumableBar != null) { bindingConsumableBar.PointerClick += ConsumableBarFunc; }
        }
        //确认窗口
        _confirmFrame = transform.Find(ConstData.ConfirmFrame).gameObject;
        _contentText= transform.Find(ConstData.ConfirmFrame_ContentText).GetComponent<Text>();
        GameObject _confirmButton = transform.Find(ConstData.ConfirmFrame_ConfirmButton).gameObject;
        GameObject _cancelButton = transform.Find(ConstData.ConfirmFrame_CancelButton).gameObject;
        //绑定确认窗口按钮
        UISceneWidget bindingConfirmButton = UISceneWidget.Get(_confirmButton);
        if (bindingConfirmButton != null) { bindingConfirmButton.PointerClick += ConfirmButton; }
        UISceneWidget bindingCancelButton = UISceneWidget.Get(_cancelButton);
        if (bindingCancelButton != null) { bindingCancelButton.PointerClick += CancelButton; }
        //选关按钮
        GameObject _leftButton = transform.Find(ConstData.ChoiceLevel_LeftButton).gameObject;
        GameObject _rightButton = transform.Find(ConstData.ChoiceLevel_RightButton).gameObject;
        GameObject _choiceConfirmButton = transform.Find(ConstData.ChoiceLevel_ChoiceConfirmButton).gameObject;
        _checkpointNumberText = transform.Find(ConstData.ChoiceLevel_CheckpointContent).GetComponent<Text>();
        //绑定选关按钮
        UISceneWidget bindingLeftButton = UISceneWidget.Get(_leftButton);
        if (bindingLeftButton != null) { bindingLeftButton.PointerClick += ChoiceLevelLeftButton; }
        UISceneWidget bindingRightButton = UISceneWidget.Get(_rightButton);
        if (bindingRightButton != null) { bindingRightButton.PointerClick += ChoiceLevelRightButton; }
        UISceneWidget bindingChoiceConfirmButton = UISceneWidget.Get(_choiceConfirmButton);
        if (bindingChoiceConfirmButton != null) { bindingChoiceConfirmButton.PointerClick += ChoiceConfirmButton; }
        //赋值消耗品栏
        _choiceLevelContent = transform.Find(ConstData.ChoiceLevel_ChoiceLevelContent);
        //物品
        item= ResourcesManager.Instance.FindUIPrefab(ConstData.ItemIcon);
        //格子
        grid = ResourcesManager.Instance.FindUIPrefab(ConstData.Grid);
        //光圈
        _aperture = ResourcesManager.Instance.FindUIPrefab(ConstData.pitchOn);
        //获取大小
        _gridSize = grid.GetComponent<RectTransform>().sizeDelta.x;
        //生成格子
        InstantiationGrid();

        //关卡内容显示
        _checkpointNumberText.text = StringSplicingTool.StringSplicing(new string[] { "<color=#ff0000>第", CheckpointDicNumber.ToString(), "关 - ", CheckpointDic[CheckpointDicNumber], "</color>" });
    }

    /// <summary>
    /// 生成格子
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
                rt.parent = _choiceLevelContent;
                rt.anchorMax = new Vector2(0, 1);
                rt.anchorMin = new Vector2(0, 1);
                rt.anchoredPosition3D = new Vector3(j * _gridSize + _gridSize * 0.5f, -i * _gridSize - _gridSize * 0.5f, 0f);
                rt.localScale = Vector3.one;
            }
        }
    }

    /// <summary>
    /// 物品实例化方法
    /// </summary>
    void ItemInstantiationFunc(List<int> itemList)
    {
        //记录格子号
        int number = 0;
        for (int i = 0; i < _grids.GetLength(0); i++)
        {
            for (int j = 0; j < _grids.GetLength(1); j++)
            {
                if (_grids[i, j].transform.childCount > 0) { return; }
                if (number >= itemList.Count) { number = 0; return; }
                //生成物品
                GameObject tempitem = ObjectPoolManager.Instance.InstantiateMyGameObject(item);
                BagItem _tempBagItem;
                if (tempitem.GetComponent<BagItem>() == null)
                {
                    tempitem.AddComponent<BagItem>();
                }
                _tempBagItem = tempitem.GetComponent<BagItem>();
                //赋值Item类
                _tempBagItem.mydata_item = SQLiteManager.Instance.itemDataSource[itemList[number]];
                tempitem.tag = ConstData.ItemType;
                //设置物体属性
                tempitem.name = item.name;
                tempitem.transform.parent = _grids[i, j].transform;
                tempitem.transform.localPosition = Vector3.zero;
                tempitem.transform.localScale = Vector3.one;
                tempitem.GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite(itemList[number].ToString());
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
            for (int j = 0; j < _grids.GetLength(1); j++)
            {
                if (_grids[i, j].transform.childCount < 1)
                {
                    return;
                }
                GameObject item = _grids[i, j].transform.GetChild(0).gameObject;
                item.GetComponent<UISceneWidget>().PointerClick -= SelectItemFunc;
                ObjectPoolManager.Instance.RecycleMyGameObject(item);
            }
        }
        //回收消耗品栏
        for (int i = 0; i < _consumableBarArr.Length; i++)
        {
            if (_consumableBarArr[i].transform.childCount < 0)
            {
                continue;
            }
            GameObject item = _consumableBarArr[i].transform.GetChild(0).gameObject;
            item.GetComponent<UISceneWidget>().PointerClick -= SelectItemFunc;
            ObjectPoolManager.Instance.RecycleMyGameObject(item);
            //回收之前的光圈
            if (_bagItem != null)
            {
                _bagItem.transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
            }
        }
        //回收之前的光圈
        if (_bagItem != null)
        {
            ObjectPoolManager.Instance.RecycleMyGameObject(_bagItem.transform.GetChild(0).gameObject);
            _bagItem = null;
        }
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
    /// 选中物品的方法
    /// </summary>
    void SelectItemFunc(PointerEventData data)
    {
        //回收之前的光圈
        if (_bagItem != null && _bagItem.transform.childCount > 1)
        {
            ObjectPoolManager.Instance.RecycleMyGameObject(_bagItem.transform.GetChild(0).gameObject);
        }
        //赋值新的物品
        _bagItem = data.pointerEnter.GetComponent<BagItem>();

        _contentText.text = StringSplicingTool.StringSplicing(new string[] { "[道具名]<color=#ff0000>", _bagItem.mydata_item.item_Name, "</color>\n", _bagItem.mydata_item.item_Description });
        GenerateAperture(_bagItem.transform);
        //固定类型
        _uiChoiceLevelConfirmFrameType = UIChoiceLevelConfirmFrameType.Item;
        _confirmFrame.SetActive(true);
    }

    /// <summary>
    /// 确认选关按钮方法
    /// </summary>
    /// <param name="data"></param>
    void ChoiceConfirmButton(PointerEventData data)
    {
        _contentText.text = "<color=#ff0000>进入关卡</color>";
        //固定类型
        _uiChoiceLevelConfirmFrameType = UIChoiceLevelConfirmFrameType.ChoiceLevel;
        _confirmFrame.SetActive(true);
    }

    /// <summary>
    /// 选关左按钮方法
    /// </summary>
    /// <param name="data"></param>
    void ChoiceLevelLeftButton(PointerEventData data)
    {
        if (CheckpointDicNumber == 1)
        {
            CheckpointDicNumber = 1;
            _checkpointNumberText.text = StringSplicingTool.StringSplicing(new string[] { "<color=#ff0000>第", CheckpointDicNumber.ToString(), "关 - ", CheckpointDic[CheckpointDicNumber], "</color>" });
            return;
        }
        CheckpointDicNumber--;
        _checkpointNumberText.text = StringSplicingTool.StringSplicing(new string[] { "<color=#ff0000>第", CheckpointDicNumber.ToString(), "关 - ", CheckpointDic[CheckpointDicNumber], "</color>" });
    }

    /// <summary>
    /// 选关右按钮方法
    /// </summary>
    /// <param name="data"></param>
    void ChoiceLevelRightButton(PointerEventData data)
    {
        if (CheckpointDicNumber == CheckpointDic.Count)
        {
            CheckpointDicNumber = CheckpointDic.Count;
            _checkpointNumberText.text = StringSplicingTool.StringSplicing(new string[] { "<color=#ff0000>第", CheckpointDicNumber.ToString(), "关 - ", CheckpointDic[CheckpointDicNumber], "</color>" });
            return;
        }
        CheckpointDicNumber++;
        _checkpointNumberText.text = StringSplicingTool.StringSplicing(new string[] { "<color=#ff0000>第", CheckpointDicNumber.ToString(), "关 - ", CheckpointDic[CheckpointDicNumber], "</color>" });
    }

    /// <summary>
    /// 确认按钮
    /// </summary>
    /// <param name="data"></param>
    void ConfirmButton(PointerEventData data)
    {
        JudgeConfirmFrameType();
    }

    /// <summary>
    /// 取消按钮
    /// </summary>
    /// <param name="data"></param>
    void CancelButton(PointerEventData data)
    {
        _confirmFrame.SetActive(false);
    }
    /// <summary>
    /// 消耗栏按钮方法
    /// </summary>
    /// <param name="data"></param>
    void ConsumableBarFunc(PointerEventData data)
    {
        if (data.pointerEnter.transform.childCount < 1)
        {
            return;
        }
        _bagItem = data.pointerEnter.transform.GetChild(0).GetComponent<BagItem>();
        string _itemName = _bagItem.mydata_item.item_Name;
        _contentText.text = StringSplicingTool.StringSplicing(new string[] { "取消使用<color=#ff0000>", _itemName, "</color>吗？" });
        //固定类型
        _uiChoiceLevelConfirmFrameType = UIChoiceLevelConfirmFrameType.ConsumableBar;
        _confirmFrame.SetActive(true);
    }

    /// <summary>
    /// 返回主城
    /// </summary>
    /// <param name="data"></param>
    void MainCityFunc(PointerEventData data)
    {
        UIManager.Instance.PopUIStack();
    }

    /// <summary>
    /// 装上物品
    /// </summary>
    void EquipmentItemFunc()
    {
        //装物品
        for (int i = 0; i < _consumableBarArr.Length; i++)
        {
            if (_consumableBarArr[i].transform.childCount < 1)
            {
                if (_bagItem != null)
                {
                    ObjectPoolManager.Instance.RecycleMyGameObject(_bagItem.transform.GetChild(0).gameObject);
                }
                _bagItem.transform.parent = _consumableBarArr[i].transform;
                _bagItem.transform.localPosition = Vector3.zero;
                _bagItem.GetComponent<Image>().raycastTarget = false;
                _bagItem = null;
                break;
            }
        }
        //整理物品栏位置
        for (int i = 0; i < _grids.GetLength(0); i++)
        {
            for (int j = 0; j < _grids.GetLength(1); j++)
            {
                if (_grids[i, j].transform.childCount < 1)
                {
                    if (j + 1 < _grids.GetLength(1))
                    {
                        if (_grids[i, j + 1].transform.childCount < 1)
                        {
                            return;
                        }
                        _grids[i, j + 1].transform.GetChild(0).parent = _grids[i, j].transform;
                        _grids[i, j].transform.GetChild(0).localPosition = Vector3.zero;
                    }
                    else if (j < _grids.GetLength(1) && i + 1 < _grids.GetLength(0))
                    {
                        if (_grids[i + 1, 0].transform.childCount < 1)
                        {
                            return;
                        }
                        _grids[i + 1, 0].transform.GetChild(0).parent = _grids[i, j].transform;
                        _grids[i, j].transform.GetChild(0).localPosition = Vector3.zero;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 卸下物品
    /// </summary>
    void UnloadItemFunc()
    {
        for (int i = 0; i < _grids.GetLength(0); i++)
        {
            for (int j = 0; j < _grids.GetLength(1); j++)
            {
                if (_grids[i, j].transform.childCount < 1)
                {
                    _bagItem.transform.parent = _grids[i, j].transform;
                    _bagItem.transform.localPosition = Vector3.zero;
                    _bagItem.GetComponent<Image>().raycastTarget = true;
                    return;
                }
            }
        }
    }

    /// <summary>
    /// 判断确认界面类型
    /// </summary>
    void JudgeConfirmFrameType()
    {
        switch (_uiChoiceLevelConfirmFrameType)
        {
            case UIChoiceLevelConfirmFrameType.ChoiceLevel:
                print("进入战斗场景");
                break;
            case UIChoiceLevelConfirmFrameType.Item:
                EquipmentItemFunc();
                break;
            case UIChoiceLevelConfirmFrameType.ConsumableBar:
                UnloadItemFunc();
                break;
        }
        _confirmFrame.SetActive(false);
    }

    //进入
    public void OnEntering()
    {
        _itemConsumableList.Clear();
        //赋值背包物品
        foreach (int item in SQLiteManager.Instance.bagDataSource.Keys)
        {
            int itemID = SQLiteManager.Instance.bagDataSource[item].Bag_Consumable;
            if (itemID == 0)
            {
                break;
            }
            _itemConsumableList.Add(itemID);
        }
        ItemInstantiationFunc(_itemConsumableList);
        gameObject.SetActive(true);
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
}
