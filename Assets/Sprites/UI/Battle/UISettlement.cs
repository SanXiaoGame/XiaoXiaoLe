using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISettlement : MonoBehaviour, IUIBase
{
    //目前的关卡
    string nowStage;
    //最终总经验值池
    internal static int totalEXP;
    //最终获得的总金币池
    int totalGoldCoin;
    //是否抽中物品开关
    bool bingo;
    //武器背包剩余空格
    int weaponGrid;
    //防具背包剩余空格
    int equipmentGrid;
    //选中道具列表
    List<int> GetItemList;
    //各职业的名字、等级和经验
    Text flg_name;
    Text flg_lv;
    Slider flg_slider;
    int flg_nowEXP;
    Text sbr_name;
    Text sbr_lv;
    Slider sbr_slider;
    int sbr_nowEXP;
    Text knt_name;
    Text knt_lv;
    Slider knt_slider;
    int knt_nowEXP;
    Text bsk_name;
    Text bsk_lv;
    Slider bsk_slider;
    int bsk_nowEXP;
    Text cst_name;
    Text cst_lv;
    Slider cst_slider;
    int cst_nowEXP;
    Text hut_name;
    Text hut_lv;
    Slider hut_slider;
    int hut_nowEXP;
    //获得金币显示
    Text getGoldDisplay;
    //道具界面
    Vector3 itemCreatePoint;
    GameObject getAwardButton;
    //提示窗口
    Text warningMessage;
    GameObject cancelButton;
    //区域界面
    GameObject CharaMessage;
    GameObject GoldCoinFrame;
    GameObject ItemListFrame;
    GameObject MessageFrame;
    GameObject itemGrid;
    //选中光圈
    GameObject selectHaloPrefab;
    //Slider列表
    List<Slider> sliderList;
    //角色当前等级列表
    List<int> lvList;
    //角色最终经验值列表
    List<int> totalEXPList;

    private void Awake()
    {
        #region 初始化设置
        nowStage = GameObject.FindGameObjectWithTag(ConstData.Stage).name;
        bingo = false;
        CharaMessage = transform.Find(ConstData.UISettlement_CharaMessage).gameObject;
        GoldCoinFrame = transform.Find(ConstData.UISettlement_GoldCoinFrame).gameObject;
        ItemListFrame = transform.Find(ConstData.UISettlement_ItemListFrame).gameObject;
        MessageFrame = transform.Find(ConstData.UISettlement_MessageFrame).gameObject;
        GetItemList = new List<int>();
        GetItemList.Clear();
        sliderList = new List<Slider>();
        sliderList.Clear();
        lvList = new List<int>();
        lvList.Clear();
        totalEXPList = new List<int>();
        totalEXPList.Clear();
        flg_name = CharaMessage.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        flg_lv = CharaMessage.transform.GetChild(0).GetChild(1).GetComponent<Text>();
        flg_slider = CharaMessage.transform.GetChild(0).GetChild(2).GetComponent<Slider>(); ;
        sliderList.Add(flg_slider);
        flg_nowEXP = SQLiteManager.Instance.team[ConstData.FlagMan].playerData.EXP;
        sbr_name = CharaMessage.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        sbr_lv = CharaMessage.transform.GetChild(1).GetChild(1).GetComponent<Text>();
        sbr_slider = CharaMessage.transform.GetChild(1).GetChild(2).GetComponent<Slider>();
        sliderList.Add(sbr_slider);
        sbr_nowEXP = SQLiteManager.Instance.team[ConstData.Saber].playerData.EXP;
        knt_name = CharaMessage.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        knt_lv = CharaMessage.transform.GetChild(2).GetChild(1).GetComponent<Text>();
        knt_slider = CharaMessage.transform.GetChild(2).GetChild(2).GetComponent<Slider>();
        sliderList.Add(knt_slider);
        knt_nowEXP = SQLiteManager.Instance.team[ConstData.Knight].playerData.EXP;
        bsk_name = CharaMessage.transform.GetChild(3).GetChild(0).GetComponent<Text>();
        bsk_lv = CharaMessage.transform.GetChild(3).GetChild(1).GetComponent<Text>();
        bsk_slider = CharaMessage.transform.GetChild(3).GetChild(2).GetComponent<Slider>();
        sliderList.Add(bsk_slider);
        bsk_nowEXP = SQLiteManager.Instance.team[ConstData.Berserker].playerData.EXP;
        cst_name = CharaMessage.transform.GetChild(4).GetChild(0).GetComponent<Text>();
        cst_lv = CharaMessage.transform.GetChild(4).GetChild(1).GetComponent<Text>();
        cst_slider = CharaMessage.transform.GetChild(4).GetChild(2).GetComponent<Slider>();
        sliderList.Add(cst_slider);
        cst_nowEXP = SQLiteManager.Instance.team[ConstData.Caster].playerData.EXP;
        hut_name = CharaMessage.transform.GetChild(5).GetChild(0).GetComponent<Text>();
        hut_lv = CharaMessage.transform.GetChild(5).GetChild(1).GetComponent<Text>();
        hut_slider = CharaMessage.transform.GetChild(5).GetChild(2).GetComponent<Slider>();
        sliderList.Add(hut_slider);
        hut_nowEXP = SQLiteManager.Instance.team[ConstData.Hunter].playerData.EXP;
        getGoldDisplay = GoldCoinFrame.transform.GetChild(0).GetComponent<Text>();
        itemCreatePoint = ItemListFrame.transform.GetChild(0).transform.GetComponent<RectTransform>().anchoredPosition;
        getAwardButton = ItemListFrame.transform.GetChild(1).gameObject;
        if (getAwardButton.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget getAwardButtonClick = UISceneWidget.Get(getAwardButton);
            getAwardButtonClick.PointerClick += GetItemAndReturn;
        }
        else
        {
            getAwardButton.GetComponent<UISceneWidget>().PointerClick += GetItemAndReturn;
        }
        warningMessage = MessageFrame.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        cancelButton = MessageFrame.transform.GetChild(2).gameObject;
        selectHaloPrefab = ResourcesManager.Instance.FindUIPrefab(ConstData.pitchOn);
        //角色经验值存入列表
        lvList.Add(SQLiteManager.Instance.team[ConstData.FlagMan].playerData.Level);
        lvList.Add(SQLiteManager.Instance.team[ConstData.Saber].playerData.Level);
        lvList.Add(SQLiteManager.Instance.team[ConstData.Knight].playerData.Level);
        lvList.Add(SQLiteManager.Instance.team[ConstData.Berserker].playerData.Level);
        lvList.Add(SQLiteManager.Instance.team[ConstData.Caster].playerData.Level);
        lvList.Add(SQLiteManager.Instance.team[ConstData.Hunter].playerData.Level);

        itemGrid = ResourcesManager.Instance.FindUIPrefab(ConstData.GridEx);
        #endregion

        #region 背包空格数量获取
        weaponGrid = 0;
        equipmentGrid = 0;
        foreach (BagData item in SQLiteManager.Instance.bagDataSource.Values)
        {
            if (item.Bag_Weapon == 0)
            {
                weaponGrid++;
            }
        }
        foreach (BagData item in SQLiteManager.Instance.bagDataSource.Values)
        {
            if (item.Bag_Equipment == 0)
            {
                equipmentGrid++;
            }
        }
        #endregion
    }

    int delayTime;
    private void Update()
    {
        if (delayTime < 20)
        {
            delayTime++;
            return;
        }
        for (int i = 0; i < sliderList.Count; i++)
        {
            if (sliderList[i].value < totalEXPList[i])
            {
                sliderList[i].value += 1;
            }
            if (sliderList[i].value == sliderList[i].maxValue)
            {
                lvList[i]++;
                switch (i)
                {
                    case 0:
                        flg_lv.text = lvList[i].ToString();
                        break;
                    case 1:
                        sbr_lv.text = lvList[i].ToString();
                        break;
                    case 2:
                        knt_lv.text = lvList[i].ToString();
                        break;
                    case 3:
                        bsk_lv.text = lvList[i].ToString();
                        break;
                    case 4:
                        cst_lv.text = lvList[i].ToString();
                        break;
                    case 5:
                        hut_lv.text = lvList[i].ToString();
                        break;
                }
                sliderList[i].minValue = sliderList[i].maxValue;
                sliderList[i].maxValue = SQLiteManager.Instance.lVDataSource[lvList[i]].level_MaxEXP;
                sliderList[i].value = sliderList[i].minValue;
                //播放升级音效
                AudioManager.Instance.PlayEffectMusic(SoundEffect.LevelUp);
            }
        }
    }

    /// <summary>
    /// 进入界面
    /// </summary>
    public void OnEntering()
    {
        gameObject.SetActive(true);
        //设置文字和Slider
        MessageDisplay();
        Debug.Log(totalEXP);
        //处理经验值
        int everyHeroEXP = totalEXP / 6;
        int flgTotalEXP = SQLiteManager.Instance.team[ConstData.FlagMan].playerData.EXP + everyHeroEXP;
        totalEXPList.Add(flgTotalEXP);
        int sbrTotalEXP = SQLiteManager.Instance.team[ConstData.Saber].playerData.EXP + everyHeroEXP;
        totalEXPList.Add(sbrTotalEXP);
        int kntTotalEXP = SQLiteManager.Instance.team[ConstData.Knight].playerData.EXP + everyHeroEXP;
        totalEXPList.Add(kntTotalEXP);
        int bskTotalEXP = SQLiteManager.Instance.team[ConstData.Berserker].playerData.EXP + everyHeroEXP;
        totalEXPList.Add(bskTotalEXP);
        int cstTotalEXP = SQLiteManager.Instance.team[ConstData.Caster].playerData.EXP + everyHeroEXP;
        totalEXPList.Add(cstTotalEXP);
        int hutTotalEXP = SQLiteManager.Instance.team[ConstData.Hunter].playerData.EXP + everyHeroEXP;
        totalEXPList.Add(hutTotalEXP);
        getGoldDisplay.text = StringSplicingTool.StringSplicing
            ((GameManager.Instance.fixedGoldCoin[nowStage] + GameManager.Instance.totalScore).ToString());
        CreateGetItemList();
    }
    /// <summary>
    /// 退出界面
    /// </summary>
    public void OnExiting()
    {
        gameObject.SetActive(false);
    }
    /// <summary>
    /// 界面暂停
    /// </summary>
    public void OnPausing()
    {

    }
    /// <summary>
    /// 界面唤醒
    /// </summary>
    public void OnResuming()
    {
        
    }



    ////////////////////////////////////////////////////////
    //////////            UI绑定方法            ////////////
    ////////////////////////////////////////////////////////

    /// <summary>
    /// 关闭提示信息窗口
    /// </summary>
    /// <param name="data"></param>
    //void CloseMessageFrame(PointerEventData data)
    //{

    //}
    /// <summary>
    /// 获取选中的物品然后返回主城
    /// </summary>
    /// <param name="data"></param>
    void GetItemAndReturn(PointerEventData data)
    {
        GetEXP(ConstData.FlagMan, totalEXP / 6);
        GetEXP(ConstData.Saber, totalEXP / 6);
        GetEXP(ConstData.Knight, totalEXP / 6);
        GetEXP(ConstData.Berserker, totalEXP / 6);
        GetEXP(ConstData.Caster, totalEXP / 6);
        GetEXP(ConstData.Hunter, totalEXP / 6);
        GetGoldCoin();
        GetItem();
        //按钮解绑
        getAwardButton.GetComponent<UISceneWidget>().PointerClick -= GetItemAndReturn;
        totalEXP = 0;
        SceneAss_Manager.Instance.LoadingFunc(2);
    }


    ////////////////////////////////////////////////////////////
    ////////////            非绑定方法           ///////////////
    ////////////////////////////////////////////////////////////

    /// <summary>
    /// 获取金币到字典和数据库
    /// </summary>
    void GetGoldCoin()
    {
        int getCC = GameManager.Instance.fixedGoldCoin[nowStage] + GameManager.Instance.totalScore;
        //存字典和数据库
        CurrencyManager.Instance.GoldCoinIncrease(getCC);
    }
    /// <summary>
    /// 获取道具到字典和数据库
    /// </summary>
    void GetItem()
    {
        for (int i = 0; i < GetItemList.Count; i++)
        {
            string itemType = SQLiteManager.Instance.equipmentDataSource[GetItemList[i]].equipmentType;
            //字典和数据库
            foreach (BagData item in SQLiteManager.Instance.bagDataSource.Values)
            {
                if (itemType == ConstData.ListType_Weapon)
                {
                    if (item.Bag_Weapon == 0)
                    {
                        item.Bag_Weapon = GetItemList[i];
                        SQLiteManager.Instance.UpdataDataFromTable(ConstData.Bag, ConstData.Bag_Weapon, GetItemList[i],
                            ConstData.Bag_Grid, item.Bag_Grid);
                        break;
                    }
                }
                else
                {
                    if (item.Bag_Equipment == 0)
                    {
                        item.Bag_Equipment = GetItemList[i];
                        SQLiteManager.Instance.UpdataDataFromTable(ConstData.Bag, ConstData.Bag_Equipment, GetItemList[i],
                            ConstData.Bag_Grid, item.Bag_Grid);
                        break;
                    }
                }
            }
            
        }
    }
    /// <summary>
    /// 获得经验值存到字典和数据库
    /// </summary>
    void GetEXP(string heroClass, int exp)
    {
        int originLV = SQLiteManager.Instance.team[heroClass].playerData.Level;
        int totalLV = originLV;
        int maxEXP = SQLiteManager.Instance.lVDataSource[SQLiteManager.Instance.team[heroClass].playerData.Level].level_MaxEXP;
        int nowEXP = SQLiteManager.Instance.team[heroClass].playerData.EXP;
        int totalEXP = nowEXP + exp;
        SQLiteManager.Instance.team[heroClass].playerData.EXP = totalEXP;
        SQLiteManager.Instance.UpdataDataFromTable(ConstData.Player, ConstData.player_EXP, totalEXP,
            ConstData.player_ID, SQLiteManager.Instance.team[heroClass].playerData.player_Id);
        while (maxEXP < totalEXP)
        {
            //LevelUp(heroClass, SQLiteManager.Instance.team[heroClass].playerData.Level, 
            //    SQLiteManager.Instance.team[heroClass].playerData.Level + 1);
            SQLiteManager.Instance.team[heroClass].playerData.Level++;
            totalLV++;
            maxEXP = SQLiteManager.Instance.lVDataSource[SQLiteManager.Instance.team[heroClass].playerData.Level].level_MaxEXP;
        }
        LevelUp(heroClass, originLV, totalLV);
    }
    /// <summary>
    /// 升级数据存到字典和数据库
    /// </summary>
    /// <param name="nowLV"></param>
    /// <param name="totalLV"></param>
    void LevelUp(string heroClass, int nowLV, int totalLV)
    {
        if (nowLV != totalLV)
        {
            int newHP = 0;
            int newAD = 0;
            int newAP = 0;
            int newDEF = 0;
            int newRES = 0;
            while (nowLV < totalLV)
            {
                newHP += SQLiteManager.Instance.lVDataSource[nowLV].level_HP;
                newAD += SQLiteManager.Instance.lVDataSource[nowLV].level_AD;
                newAP += SQLiteManager.Instance.lVDataSource[nowLV].level_AP;
                newDEF += SQLiteManager.Instance.lVDataSource[nowLV].level_DEF;
                newRES += SQLiteManager.Instance.lVDataSource[nowLV].level_RES;
                nowLV++;
            }
            //字典
            SQLiteManager.Instance.team[heroClass].playerData.HP += newHP;
            SQLiteManager.Instance.team[heroClass].playerData.AD += newAD;
            SQLiteManager.Instance.team[heroClass].playerData.AP += newAP;
            SQLiteManager.Instance.team[heroClass].playerData.DEF += newDEF;
            SQLiteManager.Instance.team[heroClass].playerData.RES += newRES;
            //数据库
            SQLiteManager.Instance.UpdataDataFromTable(ConstData.Player,
                ConstData.player_Level, SQLiteManager.Instance.team[heroClass].playerData.Level,
                ConstData.player_ID, SQLiteManager.Instance.team[heroClass].playerData.player_Id);

            SQLiteManager.Instance.UpdataDataFromTable(ConstData.Player,
                ConstData.player_HP, SQLiteManager.Instance.team[heroClass].playerData.HP,
                ConstData.player_ID, SQLiteManager.Instance.team[heroClass].playerData.player_Id);

            SQLiteManager.Instance.UpdataDataFromTable(ConstData.Player,
                ConstData.player_AD, SQLiteManager.Instance.team[heroClass].playerData.AD,
                ConstData.player_ID, SQLiteManager.Instance.team[heroClass].playerData.player_Id);

            SQLiteManager.Instance.UpdataDataFromTable(ConstData.Player,
                ConstData.player_AP, SQLiteManager.Instance.team[heroClass].playerData.AP,
                ConstData.player_ID, SQLiteManager.Instance.team[heroClass].playerData.player_Id);

            SQLiteManager.Instance.UpdataDataFromTable(ConstData.Player,
                ConstData.player_DEF, SQLiteManager.Instance.team[heroClass].playerData.DEF,
                ConstData.player_ID, SQLiteManager.Instance.team[heroClass].playerData.player_Id);

            SQLiteManager.Instance.UpdataDataFromTable(ConstData.Player,
                ConstData.player_RES, SQLiteManager.Instance.team[heroClass].playerData.RES,
                ConstData.player_ID, SQLiteManager.Instance.team[heroClass].playerData.player_Id);
        }
    }
    /// <summary>
    /// 生成获取的道具列表
    /// </summary>
    void CreateGetItemList()
    {
        ExtractItem();
        Debug.Log(GetItemList.Count);
        if (GetItemList.Count >= 5)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject tempItem = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                tempItem.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite(GetItemList[i].ToString());
                tempItem.transform.parent = ItemListFrame.transform;
                tempItem.transform.localScale = new Vector3(1, 1, 1);
                if (i < 3)
                {
                    tempItem.transform.GetComponent<RectTransform>().anchoredPosition = itemCreatePoint + new Vector3(i * 300, 0, 0);
                }
                else
                {
                    tempItem.transform.GetComponent<RectTransform>().anchoredPosition = itemCreatePoint + new Vector3((i - 2) * -300, 0, 0);
                }
            }
        }
        else
        {
            for (int i = 0; i < GetItemList.Count; i++)
            {
                GameObject tempItem = ObjectPoolManager.Instance.InstantiateMyGameObject(itemGrid);
                tempItem.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite(GetItemList[i].ToString());
                tempItem.transform.parent = ItemListFrame.transform;
                tempItem.transform.localScale = new Vector3(1, 1, 1);
                if (i == 0 || i == 1)
                {
                    tempItem.transform.GetComponent<RectTransform>().anchoredPosition = itemCreatePoint + new Vector3(i * 300, 0, 0);
                }
                else if (i == 2)
                {
                    tempItem.transform.GetComponent<RectTransform>().anchoredPosition = itemCreatePoint + new Vector3((i - 1) * -300, 0, 0);
                }
                else if (i == 3)
                {
                    tempItem.transform.GetComponent<RectTransform>().anchoredPosition = itemCreatePoint + new Vector3((i - 1) * -300, 0, 0);
                }
                else
                {
                    tempItem.transform.GetComponent<RectTransform>().anchoredPosition = itemCreatePoint + new Vector3((i - 2) * 300, 0, 0);
                }
            }
        }
        
    }
    /// <summary>
    /// 抽取道具
    /// </summary>
    void ExtractItem()
    {
        List<int> iCanGet = GameManager.Instance.stageDropItemList[nowStage];
        switch (nowStage)
        {
            case ConstData.Stage01:
                int EI = Random.Range(1, 100);
                if (EI > 0 && EI <= 30)
                {
                    if (weaponGrid > 0)
                    {
                        GetItemList.Add(iCanGet[0]);
                        weaponGrid--;
                    }
                }
                EI = Random.Range(1, 100);
                if (EI > 0 && EI <= 30)
                {
                    if (weaponGrid > 0)
                    {
                        GetItemList.Add(iCanGet[1]);
                        weaponGrid--;
                    }
                }
                EI = Random.Range(1, 100);
                if (EI > 0 && EI <= 40)
                {
                    if (weaponGrid > 0)
                    {
                        GetItemList.Add(iCanGet[2]);
                        weaponGrid--;
                    }
                }
                EI = Random.Range(1, 100);
                if (EI > 0 && EI <= 30)
                {
                    if (weaponGrid > 0)
                    {
                        GetItemList.Add(iCanGet[3]);
                        weaponGrid--;
                    }
                }
                EI = Random.Range(1, 100);
                if (EI > 5 && EI <= 20)
                {
                    if (weaponGrid > 0)
                    {
                        GetItemList.Add(iCanGet[4]);
                        weaponGrid--;
                    }
                }
                break;
            case ConstData.Stage02:
                break;
            case ConstData.Stage03:
                break;
            case ConstData.Stage04:
                break;
            case ConstData.Stage05:
                break;
            case ConstData.Stage06:
                break;
            case ConstData.Stage07:
                break;
            case ConstData.Stage08:
                break;
            case ConstData.Stage09:
                break;
            case ConstData.Stage10:
                break;
        }
    }
    /// <summary>
    /// 怪物死亡传输经验值到这边的经验池
    /// </summary>
    /// <param name="upValue"></param>
    internal static void EXPPoolUp(int upValue)
    {
        totalEXP += upValue;
    }
    /// <summary>
    /// 显示信息
    /// </summary>
    void MessageDisplay()
    {
        //旗手
        flg_name.text = SQLiteManager.Instance.team[ConstData.FlagMan].playerData.player_Name;
        flg_lv.text = (SQLiteManager.Instance.team[ConstData.FlagMan].playerData.Level).ToString();
        flg_slider.minValue = SQLiteManager.Instance.lVDataSource
            [(SQLiteManager.Instance.team[ConstData.FlagMan].playerData.Level) - 1].level_MaxEXP;
        flg_slider.maxValue = SQLiteManager.Instance.lVDataSource
            [SQLiteManager.Instance.team[ConstData.FlagMan].playerData.Level].level_MaxEXP;
        flg_slider.value = SQLiteManager.Instance.team[ConstData.FlagMan].playerData.EXP;
        //剑士
        sbr_name.text = SQLiteManager.Instance.team[ConstData.Saber].playerData.player_Name;
        sbr_lv.text = (SQLiteManager.Instance.team[ConstData.Saber].playerData.Level).ToString();
        sbr_slider.minValue = SQLiteManager.Instance.lVDataSource
            [(SQLiteManager.Instance.team[ConstData.Saber].playerData.Level) - 1].level_MaxEXP;
        sbr_slider.maxValue = SQLiteManager.Instance.lVDataSource
            [SQLiteManager.Instance.team[ConstData.Saber].playerData.Level].level_MaxEXP;
        sbr_slider.value = SQLiteManager.Instance.team[ConstData.Saber].playerData.EXP;
        //骑士
        knt_name.text = SQLiteManager.Instance.team[ConstData.Knight].playerData.player_Name;
        knt_lv.text = (SQLiteManager.Instance.team[ConstData.Knight].playerData.Level).ToString();
        knt_slider.minValue = SQLiteManager.Instance.lVDataSource
            [(SQLiteManager.Instance.team[ConstData.Knight].playerData.Level) - 1].level_MaxEXP;
        knt_slider.maxValue = SQLiteManager.Instance.lVDataSource
            [SQLiteManager.Instance.team[ConstData.Knight].playerData.Level].level_MaxEXP;
        knt_slider.value = SQLiteManager.Instance.team[ConstData.Knight].playerData.EXP;
        //狂战士
        bsk_name.text = SQLiteManager.Instance.team[ConstData.Berserker].playerData.player_Name;
        bsk_lv.text = (SQLiteManager.Instance.team[ConstData.Berserker].playerData.Level).ToString();
        bsk_slider.minValue = SQLiteManager.Instance.lVDataSource
            [(SQLiteManager.Instance.team[ConstData.Berserker].playerData.Level) - 1].level_MaxEXP;
        bsk_slider.maxValue = SQLiteManager.Instance.lVDataSource
            [SQLiteManager.Instance.team[ConstData.Berserker].playerData.Level].level_MaxEXP;
        bsk_slider.value = SQLiteManager.Instance.team[ConstData.Berserker].playerData.EXP;
        //魔法师
        cst_name.text = SQLiteManager.Instance.team[ConstData.Caster].playerData.player_Name;
        cst_lv.text = (SQLiteManager.Instance.team[ConstData.Caster].playerData.Level).ToString();
        cst_slider.minValue = SQLiteManager.Instance.lVDataSource
            [(SQLiteManager.Instance.team[ConstData.Caster].playerData.Level) - 1].level_MaxEXP;
        cst_slider.maxValue = SQLiteManager.Instance.lVDataSource
            [SQLiteManager.Instance.team[ConstData.Caster].playerData.Level].level_MaxEXP;
        cst_slider.value = SQLiteManager.Instance.team[ConstData.Caster].playerData.EXP;
        //猎人
        hut_name.text = SQLiteManager.Instance.team[ConstData.Hunter].playerData.player_Name;
        hut_lv.text = (SQLiteManager.Instance.team[ConstData.Hunter].playerData.Level).ToString();
        hut_slider.minValue = SQLiteManager.Instance.lVDataSource
            [(SQLiteManager.Instance.team[ConstData.Hunter].playerData.Level) - 1].level_MaxEXP;
        hut_slider.maxValue = SQLiteManager.Instance.lVDataSource
            [SQLiteManager.Instance.team[ConstData.Hunter].playerData.Level].level_MaxEXP;
        hut_slider.value = SQLiteManager.Instance.team[ConstData.Hunter].playerData.EXP;
    }
}
