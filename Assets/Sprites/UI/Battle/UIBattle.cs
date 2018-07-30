using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIBattle : MonoBehaviour, IUIBase
{
    //预制体外：游戏画面区域
    GameObject GameArea;
    //获取相机
    GameObject gameCamera;
    //显示信息
    Text GoldCoin;
    Text message;
    //角色预制体
    GameObject _flg;
    GameObject _sbr;
    GameObject _knt;
    GameObject _bsk;
    GameObject _cst;
    GameObject _hut;
    //角色列表
    List<GameObject> playerList;
    //道具框列表
    List<GameObject> itemFrameList;
    //道具列表
    List<int> itemList;
    //道具对象列表
    internal static List<GameObject> itemObjList;
    //血条列表
    internal static List<GameObject> hpBarList;
    //阵亡角色列表
    internal static List<GameObject> deadCharaList;

    //需要操作的界面
    GameObject settingIcon;
    GameObject mainCityIcon;
    GameObject ConfirmFrame;
    GameObject ConfirmButton;
    GameObject CancelButton;
    GameObject itemOne;
    GameObject itemTwo;
    GameObject itemThree;
    GameObject itemFour;
    GameObject HPBarPrefab;
    GameObject CanvasParent;

    GameObject WIN;
    GameObject LOSE;

    #region 战斗专用
    internal static int ItemOneID;
    internal static int ItemTwoID;
    internal static int ItemThreeID;
    internal static int ItemFourID;
    #endregion


    private void Awake()
    {
        //金币对象获取
        GoldCoin = transform.Find(ConstData.GameArea_GoldCoin).GetComponent<Text>();
        //各种按钮绑定事件
        settingIcon = transform.Find(ConstData.SystemArea_SettingIcon).gameObject;
        if (settingIcon.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget settingIconClick = UISceneWidget.Get(settingIcon);
            settingIconClick.PointerClick += SettingFrameOpen;
        }
        mainCityIcon = transform.Find(ConstData.SystemArea_MainCityIcon).gameObject;
        if (mainCityIcon.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget mainCityIconClick = UISceneWidget.Get(mainCityIcon);
            mainCityIconClick.PointerClick += ReturnMainCityConfirm;
        }
        ConfirmFrame = transform.Find(ConstData.ConfirmFrame).gameObject;
        message = ConfirmFrame.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        ConfirmButton = ConfirmFrame.transform.GetChild(2).gameObject;
        if (ConfirmButton.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget ConfirmButtonClick = UISceneWidget.Get(ConfirmButton);
            ConfirmButtonClick.PointerClick += ReturnConfirm;
        }
        CancelButton = ConfirmFrame.transform.GetChild(3).gameObject;
        if (CancelButton.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget CancelButtonClick = UISceneWidget.Get(CancelButton);
            CancelButtonClick.PointerClick += ReturnCancle;
        }
        //道具栏对象获取
        itemOne = transform.Find(ConstData.ControllerExArea_ItemOne).gameObject;
        itemTwo = transform.Find(ConstData.ControllerExArea_ItemTwo).gameObject;
        itemThree = transform.Find(ConstData.ControllerExArea_ItemThree).gameObject;
        itemFour = transform.Find(ConstData.ControllerExArea_ItemFour).gameObject;
        HPBarPrefab = ResourcesManager.Instance.FindUIPrefab(ConstData.HPBar);
        CanvasParent = transform.parent.gameObject;
        //列表初始化
        playerList = new List<GameObject>();
        itemFrameList = new List<GameObject>();
        itemList = new List<int>();
        itemObjList = new List<GameObject>();
        hpBarList = new List<GameObject>();
        deadCharaList = new List<GameObject>();
        //道具栏列表添加
        itemFrameList.Add(itemOne);
        itemFrameList.Add(itemTwo);
        itemFrameList.Add(itemThree);
        itemFrameList.Add(itemFour);
        //获取胜利失败
        WIN = transform.Find(ConstData.WIN).gameObject;
        LOSE = transform.Find(ConstData.LOSE).gameObject;
    }

    private void Start()
    {
        GameManager.Instance.AssignNeighbours(0.5f);
    }

    //进入界面
    public void OnEntering()
    {
        gameObject.SetActive(true);
        //获取相机
        gameCamera = transform.Find("/GameCamera").gameObject;
        //游戏画面区域生成
        GameArea = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindUIPrefab(ConstData.Stage01));
        GameArea.transform.position = ResourcesManager.Instance.FindUIPrefab(ConstData.Stage01).transform.position;
        GameArea.name = ResourcesManager.Instance.FindUIPrefab(ConstData.Stage01).name;
        //重置信息
        playerList.Clear();
        itemList.Clear();
        itemObjList.Clear();
        hpBarList.Clear();
        deadCharaList.Clear();
        //生成创建队伍列表的所有角色
        CreateHero();
        //金币信息
        GoldCoin.text = CurrencyManager.Instance.GoldCoinDisplay();
        if (GameArea != null)
        {
            GameArea.SetActive(true);
        }
        //获得道具信息
        itemList.Add(ItemOneID);
        itemList.Add(ItemTwoID);
        itemList.Add(ItemThreeID);
        itemList.Add(ItemFourID);
        //生成道具
        CreateItem();
        //开始游戏
        vp_Timer.In(1.5f, new vp_Timer.Callback(delegate () { FlagManController.flagMove = true; }));
    }
    //退出界面
    public void OnExiting()
    {
        gameObject.SetActive(false);
        GameArea.SetActive(false);
    }
    //界面暂停
    public void OnPausing()
    {
        
    }
    //界面唤醒
    public void OnResuming()
    {
        
    }



    ////////////////////////////////////////////////////////
    //////////            UI绑定方法            ////////////
    ////////////////////////////////////////////////////////

    
    
    /// <summary>
    /// 打开设置界面
    /// </summary>
    void SettingFrameOpen(PointerEventData eventData)
    {
        //设置界面推入栈
    }
    /// <summary>
    /// 返回主城确认窗口
    /// </summary>
    void ReturnMainCityConfirm(PointerEventData eventData)
    {
        //打开返回主城的窗口
        ConfirmFrame.SetActive(true);
        UIManager.Instance.GamePause();
    }
    /// <summary>
    /// 确认返回主城
    /// </summary>
    void ReturnConfirm(PointerEventData eventData)
    {
        //清空所有需要清空的东西
        ClearAll();
        SceneAss_Manager.Instance.LoadingFunc(2);
    }
    /// <summary>
    /// 取消返回
    /// </summary>
    void ReturnCancle(PointerEventData eventData)
    {
        //关闭返回主城的窗口
        ConfirmFrame.SetActive(false);
        UIManager.Instance.GamePause();
    }
    
    
    
    ///////////////////////////////////////////////////////////
    ////////////            非绑定方法           //////////////
    ///////////////////////////////////////////////////////////

    
    
    /// <summary>
    /// 生成道具
    /// </summary>
    void CreateItem()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] != 0)
            {
                GameObject tempitem01 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindUIPrefab(ConstData.ItemIcon));
                tempitem01.GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite((itemList[i]).ToString());
                tempitem01.transform.parent = itemFrameList[i].transform;
                tempitem01.transform.localScale = new Vector3(1, 1, 1);
                tempitem01.transform.position = itemFrameList[i].transform.GetChild(0).transform.position;
                tempitem01.name = itemList[i].ToString();
                if (tempitem01.GetComponent<PropsFunction>() == null)
                {
                    tempitem01.AddComponent<PropsFunction>();
                }
                itemObjList.Add(tempitem01);
            }
        }
    }
    /// <summary>
    /// 清除所有对象，还原到初始形态
    /// </summary>
    internal void ClearAll()
    {
        //回收墙
        _flg.GetComponent<FlagManController>().RecWall();
        //清空道具栏的对象
        for (int i = 0; i < itemObjList.Count; i++)
        {
            ObjectPoolManager.Instance.RecycleMyGameObject(itemObjList[i]);
        }
        itemObjList.Clear();
        itemList.Clear();
        //相机打捞
        for (int k = 0; k < playerList.Count; k++)
        {
            if (playerList[k].tag == ConstData.FlagMan)
            {
                GameObject cmra = playerList[k].transform.Find("GameCamera").gameObject;
                cmra.transform.parent = transform.Find("/Canvas").transform;
                cmra.transform.parent = null;
                cmra.transform.position = new Vector3(0, 0, -0.5f);
            }
        }
        //清空玩家
        for (int j = 0; j < playerList.Count; j++)
        {
            //重置玩家动作
            playerList[j].GetComponent<Animator>().SetTrigger("toIdle");
            //playerList[j].transform.Find(ConstData.MainFist).GetChild(0).transform.rotation = ResourcesManager.Instance.FindWeaponPrefab
            //    (playerList[j].transform.Find(ConstData.MainFist).GetChild(0).name).transform.rotation;
            //没收玩家主手武器
            if (playerList[j].transform.Find(ConstData.MainFist).childCount > 0)
            {
                ObjectPoolManager.Instance.RecycleMyGameObject(playerList[j].transform.Find(ConstData.MainFist).GetChild(0).gameObject);
            }
            //没收玩家副手武器
            if (playerList[j].transform.Find(ConstData.MinorFist).childCount > 0)
            {
                ObjectPoolManager.Instance.RecycleMyGameObject(playerList[j].transform.Find(ConstData.MinorFist).GetChild(0).gameObject);
            }
            //回收玩家物体
            ObjectPoolManager.Instance.RecycleMyGameObject(playerList[j]);
        }
        //清空敌人
        for (int k = 0; k < MonsterPointManagerStage01.enemyList.Count; k++)
        {
            //回收怪物物体
            ObjectPoolManager.Instance.RecycleMyGameObject(MonsterPointManagerStage01.enemyList[k]);
        }
        //回收血条们
        for (int l = 0; l < hpBarList.Count; l++)
        {
            ObjectPoolManager.Instance.RecycleMyGameObject(hpBarList[l]);
        }
        //单例们重置
        FlagManController.battleSwitch = false;
        FlagManController.flagMove = false;
        ItemOneID = 0;
        ItemTwoID = 0;
        ItemThreeID = 0;
        ItemFourID = 0;
        playerList.Clear();
        hpBarList.Clear();
    }
    /// <summary>
    /// 创建角色
    /// </summary>
    void CreateHero()
    {
        //召唤旗手
        _flg = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPlayerPrefab("1001"));
        _flg.name = (SQLiteManager.Instance.team[ConstData.FlagMan].playerData.PrefabsID).ToString();
        _flg.transform.position = GameArea.transform.GetChild(3).transform.position;
        _flg.AddComponent<FlagManController>();
        gameCamera.transform.parent = _flg.transform;
        playerList.Add(_flg);
        _flg.transform.parent = null;
        //设置旗手的血条
        GameObject hpBarFlg = ObjectPoolManager.Instance.InstantiateMyGameObject(HPBarPrefab);
        hpBarFlg.transform.parent = CanvasParent.transform;
        hpBarFlg.transform.localScale = new Vector3(1, 1, 1);
        hpBarFlg.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-550, 1000);
        hpBarFlg.GetComponent<HPBarColorChange>().GetTarget(_flg);
        hpBarList.Add(hpBarFlg);
        //召唤剑士
        _sbr = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPlayerPrefab
            ((SQLiteManager.Instance.team[ConstData.Saber].playerData.PrefabsID).ToString())
            );
        _sbr.name = (SQLiteManager.Instance.team[ConstData.Saber].playerData.PrefabsID).ToString();
        _sbr.transform.position = _flg.transform.position + new Vector3(0.8f, 0, 0);
        _sbr.AddComponent<HeroController>();
        _sbr.AddComponent<HeroStates>();
        GameObject wp = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindWeaponPrefab
            ((SQLiteManager.Instance.team[ConstData.Saber].playerData.Weapon).ToString())
            );
        wp.name = (SQLiteManager.Instance.team[ConstData.Saber].playerData.Weapon).ToString();
        wp.transform.parent = _sbr.transform.Find(ConstData.MainFist).transform;
        wp.transform.localPosition = new Vector3(0, 0, 0);
        wp.transform.localRotation = ResourcesManager.Instance.FindWeaponPrefab
            ((SQLiteManager.Instance.team[ConstData.Saber].playerData.Weapon).ToString()).transform.rotation;
        playerList.Add(_sbr);
        SkillManager.Instance.saber = _sbr;
        _sbr.transform.parent = null;
        //设置剑士的血条
        GameObject hpBarSbr = ObjectPoolManager.Instance.InstantiateMyGameObject(HPBarPrefab);
        hpBarSbr.transform.parent = CanvasParent.transform;
        hpBarSbr.transform.localScale = new Vector3(1, 1, 1);
        hpBarSbr.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-300, 1000);
        hpBarSbr.GetComponent<HPBarColorChange>().GetTarget(_sbr);
        hpBarList.Add(hpBarSbr);
        //召唤骑士
        _knt = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPlayerPrefab
            ((SQLiteManager.Instance.team[ConstData.Knight].playerData.PrefabsID).ToString())
            );
        _knt.name = (SQLiteManager.Instance.team[ConstData.Knight].playerData.PrefabsID).ToString();
        _knt.transform.position = _flg.transform.position + new Vector3(1.6f, 0, 0);
        _knt.AddComponent<HeroController>();
        _knt.AddComponent<HeroStates>();
        GameObject wp2 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindWeaponPrefab
            ((SQLiteManager.Instance.team[ConstData.Knight].playerData.Weapon).ToString())
            );
        wp2.name = (SQLiteManager.Instance.team[ConstData.Knight].playerData.Weapon).ToString();
        wp2.transform.parent = _knt.transform.Find(ConstData.MainFist).transform;
        wp2.transform.localPosition = new Vector3(0, 0, 0);
        wp2.transform.localRotation = ResourcesManager.Instance.FindWeaponPrefab
            ((SQLiteManager.Instance.team[ConstData.Knight].playerData.Weapon).ToString()).transform.rotation;
        playerList.Add(_knt);
        SkillManager.Instance.knight = _knt;
        _knt.transform.parent = null;
        //设置骑士的血条
        GameObject hpBarKnt = ObjectPoolManager.Instance.InstantiateMyGameObject(HPBarPrefab);
        hpBarKnt.transform.parent = CanvasParent.transform;
        hpBarKnt.transform.localScale = new Vector3(1, 1, 1);
        hpBarKnt.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-300, 940);
        hpBarKnt.GetComponent<HPBarColorChange>().GetTarget(_knt);
        hpBarList.Add(hpBarKnt);
        //召唤狂战士
        _bsk = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPlayerPrefab
            ((SQLiteManager.Instance.team[ConstData.Berserker].playerData.PrefabsID).ToString())
            );
        _bsk.name = (SQLiteManager.Instance.team[ConstData.Berserker].playerData.PrefabsID).ToString();
        _bsk.transform.position = _flg.transform.position + new Vector3(2.4f, 0, 0);
        _bsk.AddComponent<HeroController>();
        _bsk.AddComponent<HeroStates>();
        GameObject wp3 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindWeaponPrefab
            ((SQLiteManager.Instance.team[ConstData.Berserker].playerData.Weapon).ToString())
            );
        wp3.name = (SQLiteManager.Instance.team[ConstData.Berserker].playerData.Weapon).ToString();
        wp3.transform.parent = _bsk.transform.Find(ConstData.MainFist).transform;
        wp3.transform.localPosition = new Vector3(0, 0, 0);
        wp3.transform.localRotation = ResourcesManager.Instance.FindWeaponPrefab
            ((SQLiteManager.Instance.team[ConstData.Berserker].playerData.Weapon).ToString()).transform.rotation;
        playerList.Add(_bsk);
        SkillManager.Instance.berserker = _bsk;
        _bsk.transform.parent = null;
        //设置狂战士的血条
        GameObject hpBarBsk = ObjectPoolManager.Instance.InstantiateMyGameObject(HPBarPrefab);
        hpBarBsk.transform.parent = CanvasParent.transform;
        hpBarBsk.transform.localScale = new Vector3(1, 1, 1);
        hpBarBsk.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-300, 880);
        hpBarBsk.GetComponent<HPBarColorChange>().GetTarget(_bsk);
        hpBarList.Add(hpBarBsk);
        //召唤魔法师
        _cst = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPlayerPrefab
            ((SQLiteManager.Instance.team[ConstData.Caster].playerData.PrefabsID).ToString())
            );
        _cst.name = (SQLiteManager.Instance.team[ConstData.Caster].playerData.PrefabsID).ToString();
        _cst.transform.position = _flg.transform.position + new Vector3(3.2f, 0, 0);
        _cst.AddComponent<HeroController>();
        _cst.AddComponent<HeroStates>();
        GameObject wp4 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindWeaponPrefab
            ((SQLiteManager.Instance.team[ConstData.Caster].playerData.Weapon).ToString())
            );
        wp4.name = (SQLiteManager.Instance.team[ConstData.Caster].playerData.Weapon).ToString();
        wp4.transform.parent = _cst.transform.Find(ConstData.MainFist).transform;
        wp4.transform.localPosition = new Vector3(0, 0, 0);
        wp4.transform.localRotation = ResourcesManager.Instance.FindWeaponPrefab
            ((SQLiteManager.Instance.team[ConstData.Caster].playerData.Weapon).ToString()).transform.rotation;
        playerList.Add(_cst);
        SkillManager.Instance.caster = _cst;
        _cst.transform.parent = null;
        //设置魔法师的血条
        GameObject hpBarCst = ObjectPoolManager.Instance.InstantiateMyGameObject(HPBarPrefab);
        hpBarCst.transform.parent = CanvasParent.transform;
        hpBarCst.transform.localScale = new Vector3(1, 1, 1);
        hpBarCst.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-550, 940);
        hpBarCst.GetComponent<HPBarColorChange>().GetTarget(_cst);
        hpBarList.Add(hpBarCst);
        //召唤猎人
        _hut = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPlayerPrefab
            ((SQLiteManager.Instance.team[ConstData.Hunter].playerData.PrefabsID).ToString())
            );
        _hut.name = (SQLiteManager.Instance.team[ConstData.Hunter].playerData.PrefabsID).ToString();
        _hut.transform.position = _flg.transform.position + new Vector3(4.0f, 0, 0);
        _hut.AddComponent<HeroController>();
        _hut.AddComponent<HeroStates>();
        GameObject wp5 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindWeaponPrefab
            ((SQLiteManager.Instance.team[ConstData.Hunter].playerData.Weapon).ToString())
            );
        wp5.name = (SQLiteManager.Instance.team[ConstData.Hunter].playerData.Weapon).ToString();
        wp5.transform.parent = _hut.transform.Find(ConstData.MinorFist).transform;
        wp5.transform.localPosition = new Vector3(0, 0, 0);
        wp5.transform.localRotation = ResourcesManager.Instance.FindWeaponPrefab
            ((SQLiteManager.Instance.team[ConstData.Hunter].playerData.Weapon).ToString()).transform.rotation;
        playerList.Add(_hut);
        SkillManager.Instance.hunter = _hut;
        _hut.transform.parent = null;
        //设置猎人的血条
        GameObject hpBarHut = ObjectPoolManager.Instance.InstantiateMyGameObject(HPBarPrefab);
        hpBarHut.transform.parent = CanvasParent.transform;
        hpBarHut.transform.localScale = new Vector3(1, 1, 1);
        hpBarHut.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-550, 880);
        hpBarHut.GetComponent<HPBarColorChange>().GetTarget(_hut);
        hpBarList.Add(hpBarHut);
    }

    internal void PushLose()
    {
        LOSE.SetActive(true);
    }
    internal void PushWin()
    {
        WIN.SetActive(true);
    }
}
