using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// 酒店界面
/// </summary>
public class UIDrunkery : MonoBehaviour,IUIBase {

    //人物名
    Text characterName;
    //招募价格
    Text characterPrice;
    //人物血量
    Text HP;
    //人物AD和AP
    Text AD_AP;
    //人物DEF和REF
    Text DEF_RES;
    //选中人物按钮
    Toggle[] CharacterButton;
    UISceneWidget[] bindingCharacterButton;
    //人物生成点数组
    Transform[] characterSpot;
    //确认招募按钮
    GameObject SummonButton;
    UISceneWidget bindingSummonButton;
    //确认窗口
    GameObject confirmFrameObj;
    Text confirmFrame;
    GameObject confirmButton;
    GameObject cancelButton;
    UISceneWidget bindingConfirmButton;
    UISceneWidget bindingcancelButton;
    //是否显示的SummonIcon
    bool summonIcon = false;
    //是否允许生成人物
    bool isInstantiationCharacter = true;

    //存储生成的人物数组
    GameObject[] characterObj;
    //选中人物编号
    int characteNumber = 0;

    //酒店人物出现数量（默认：3）
    int drunkeryCharacterNumber = 3;
    //酒店人物刷新的时间
    float drunkeryRefreshTime = 300f;

    //稀有角色数量(默认：21)
    const int rareCharacterNumber = 21;

    //存储选中人物的数据
    CharacterListData _characterListData;

    //返回主城按钮
    GameObject returnMainCityButton;
    UISceneWidget bindingReturnMainCityButton;

    //显示金币数
    Text _goldFrame;

    /// <summary>
    /// 赋值
    /// </summary>
    private void Start()
    {
        //金币
        _goldFrame = transform.Find(ConstData.GameArea_GoldCoin).GetComponent<Text>();
        _goldFrame.text = CurrencyManager.Instance.GoldCoinDisplay();

        characterName = transform.Find(ConstData.DrunkeryContentBG_Name).GetComponent<Text>();
        characterPrice = transform.Find(ConstData.DrunkeryContentBG_Price).GetComponent<Text>();
        HP = transform.Find(ConstData.DrunkeryContentBG_HP).GetComponent<Text>();
        AD_AP = transform.Find(ConstData.DrunkeryContentBG_AD_AP).GetComponent<Text>();
        DEF_RES = transform.Find(ConstData.DrunkeryContentBG_DEF_RES).GetComponent<Text>();
        returnMainCityButton = transform.Find(ConstData.SystemArea_MainCityIcon).gameObject;
        //清空内容
        ClearAllPurchaseData();

        //返回主城按钮绑定
        bindingReturnMainCityButton = UISceneWidget.Get(returnMainCityButton);
        bindingReturnMainCityButton.PointerClick += ReturnMainCityFunc;

        CharacterButton = transform.Find(ConstData.CharacterButton).GetComponentsInChildren<Toggle>();
        bindingCharacterButton = new UISceneWidget[drunkeryCharacterNumber];
        //绑定选中人物按钮
        for (int i = 0; i < bindingCharacterButton.Length; i++)
        {
            bindingCharacterButton[i] = UISceneWidget.Get(CharacterButton[i].gameObject);
            bindingCharacterButton[i].PointerClick += CharacterButtonPointerClick;
        }
        //人物生成点
        characterSpot = new Transform[drunkeryCharacterNumber];
        for (int i = 0; i < characterSpot.Length; i++)
        {
            characterSpot[i] = transform.Find(ConstData.CharacterSpot).GetChild(i);
        }

        SummonButton = transform.Find(ConstData.SummonButton).gameObject;
        confirmFrameObj = transform.Find(ConstData.ConfirmFrame).gameObject;
        confirmFrame = transform.Find(ConstData.ConfirmFrame_ContentText).GetComponent<Text>();
        confirmButton = transform.Find(ConstData.ConfirmFrame_ConfirmButton).gameObject;
        cancelButton = transform.Find(ConstData.ConfirmFrame_CancelButton).gameObject;

        bindingSummonButton = UISceneWidget.Get(SummonButton);
        bindingSummonButton.PointerClick += SummonButtonPointerClick;
        bindingConfirmButton = UISceneWidget.Get(confirmButton);
        bindingcancelButton = UISceneWidget.Get(cancelButton);
        bindingConfirmButton.PointerClick += ConfirmButtonClick;
        bindingcancelButton.PointerClick += CancelButtonClick;
        //人物数组初始化
        characterObj = new GameObject[drunkeryCharacterNumber];
        //随机生成人物协程
        StartCoroutine("RandomInstantiationCharacters");
    }

    //激活
    public void OnEntering()
    {
        gameObject.SetActive(true);
    }
    //退出
    public void OnExiting()
    {
        gameObject.SetActive(false);
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
    /// 选中人物的按钮
    /// </summary>
    /// <param name="data"></param>
    void CharacterButtonPointerClick(PointerEventData data)
    {
        summonIcon = true;
        for (int i = 0; i < CharacterButton.Length; i++)
        {
            if (CharacterButton[i].isOn)
            {
                characteNumber = i;
                break;
            }
        }
        int temp = int.Parse(characterObj[characteNumber].name);
        _characterListData = SQLiteManager.Instance.characterDataSource[temp];
        //更新内容栏
        characterName.text = _characterListData.character_Name;
        characterPrice.text = StringSplicingTool.StringSplicing("招募价格：", _characterListData.GoldCoin.ToString());
        HP.text = StringSplicingTool.StringSplicing("HP：", _characterListData.character_HP.ToString());
        string[] AD_APText = { "AD：", _characterListData.character_AD.ToString(), "  ", "AP：", _characterListData.character_AP.ToString() };
        AD_AP.text = StringSplicingTool.StringSplicing(AD_APText);
        string[] DEF_RESText = { "DEF：", _characterListData.character_DEF.ToString(), "  ", "RES：", _characterListData.character_RES.ToString() };
        DEF_RES.text = StringSplicingTool.StringSplicing(DEF_RESText);
    }
    /// <summary>
    /// 确认人物的按钮
    /// </summary>
    /// <param name="data"></param>
    void SummonButtonPointerClick(PointerEventData data)
    {
        if (summonIcon)
        {
            string[] str = { "你确定要招募“", _characterListData.character_Name, "”吗？" };
            confirmFrame.text = StringSplicingTool.StringSplicing(str);
            confirmFrameObj.SetActive(true);
        }
    }
    /// <summary>
    /// 确认按钮
    /// </summary>
    /// <param name="data"></param>
    void ConfirmButtonClick(PointerEventData data)
    {
        //检测人物数量
        if (SQLiteManager.Instance.playerDataSource.Count >= 16)
        {
            confirmButton.SetActive(false);
            cancelButton.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
            confirmFrame.text = "<color=#ff0000>人物已满！</color>";
            return;
        }
        //检测金币数量
        if (_characterListData.GoldCoin > CurrencyManager.Instance.goldCoin)
        {
            confirmButton.SetActive(false);
            cancelButton.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
            confirmFrame.text = "<color=#ff0000>金币不足！</color>";
            return;
        }

        int playerMaxID = SQLiteManager.Instance.playerDataSource.Keys.Last() + 1;
        #region 创造新人物
        PlayerData playerData = new PlayerData();
        playerData.player_Id = playerMaxID;
        playerData.player_Name = _characterListData.character_Name;
        playerData.player_Class = _characterListData.character_Class;
        playerData.player_Description = _characterListData.character_Description;
        playerData.HP = _characterListData.character_HP;
        playerData.AD = _characterListData.character_AD;
        playerData.AP = _characterListData.character_AP;
        playerData.DEF = _characterListData.character_DEF;
        playerData.RES = _characterListData.character_RES;
        playerData.EXP = _characterListData.character_EXP;
        playerData.skillOneID = _characterListData.character_SkillOneID;
        playerData.skillTwoID = _characterListData.character_SkillTwoID;
        playerData.skillThreeID = _characterListData.character_SkillThreeID;
        playerData.EXHP = _characterListData.character_EXHP;
        playerData.EXAD = _characterListData.character_EXAD;
        playerData.EXAP = _characterListData.character_EXAP;
        playerData.EXDEF = _characterListData.character_EXDEF;
        playerData.EXRES = _characterListData.character_EXRES;
        playerData.Level = _characterListData.character_Level;
        playerData.Weapon = _characterListData.character_Weapon;
        playerData.Equipment = _characterListData.character_Equipment;
        playerData.GoldCoin = _characterListData.GoldCoin;
        playerData.Diamond = _characterListData.Diamond;
        playerData.PrefabsID = _characterListData.PrefabsID;
        //存进字典
        SQLiteManager.Instance.playerDataSource.Add(playerMaxID, playerData);
        #endregion
        SQLiteManager.Instance.InsetDataToTable(playerMaxID, _characterListData.character_Name, _characterListData.character_Class, _characterListData.character_Description, _characterListData.character_HP, _characterListData.character_AD, _characterListData.character_AP, _characterListData.character_DEF, _characterListData.character_RES, _characterListData.character_SkillOneID, _characterListData.character_SkillTwoID, _characterListData.character_SkillThreeID, _characterListData.character_EXHP, _characterListData.character_EXAD, _characterListData.character_EXAP, _characterListData.character_EXDEF, _characterListData.character_EXRES, _characterListData.character_Weapon, _characterListData.character_Equipment, _characterListData.character_Level, _characterListData.character_EXP, _characterListData.GoldCoin, _characterListData.Diamond, _characterListData.PrefabsID);
        confirmFrameObj.SetActive(false);
        //刷新金币
        CurrencyManager.Instance.GoldCoinDecrease(_characterListData.GoldCoin);
        _goldFrame.text = CurrencyManager.Instance.GoldCoinDisplay();
        //清空之前数据
        ClearAllPurchaseData();
        summonIcon = false;
        for (int i = 0; i < CharacterButton.Length; i++)
        {
            CharacterButton[i].enabled = false;
            CharacterButton[i].isOn = false;
            CharacterButton[i].enabled = true;
        }
        //回收购买过的人物
        ObjectPoolManager.Instance.RecycleMyGameObject(characterObj[characteNumber]);
        //刷新出新人物
        RandomInstantiationCharacterFunc(characteNumber);
    }
    /// <summary>
    /// 取消按钮
    /// </summary>
    /// <param name="data"></param>
    void CancelButtonClick(PointerEventData data)
    {
        confirmButton.SetActive(true);
        cancelButton.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(225, -245);
        confirmFrameObj.SetActive(false);
    }

    /// <summary>
    /// 返回到主城
    /// </summary>
    /// <param name="data"></param>
    void ReturnMainCityFunc(PointerEventData data)
    {
        UIManager.Instance.PopUIStack();
    }

    /// <summary>
    /// 随机生成人物的方法
    /// </summary>
    /// <param 人物生成点="spot"></param>
    void RandomInstantiationCharacterFunc(int spot)
    {
        int characterID = RandomManager.Instance.GetRandomCharacter(CharacterFieldType.Hotel);
        GameObject tempResources = ResourcesManager.Instance.FindPlayerPrefab(characterID.ToString());
        GameObject tempCharacter = ObjectPoolManager.Instance.InstantiateMyGameObject(tempResources);
        tempCharacter.name = tempResources.name;
        tempCharacter.transform.parent = characterSpot[spot];
        tempCharacter.transform.localPosition = Vector3.zero;
        tempCharacter.GetComponent<Animator>().SetBool("isWait", true);
        characterObj[spot] = tempCharacter;
    }

    /// <summary>
    /// 随机生成人物协程
    /// </summary>
    /// <returns></returns>
    IEnumerator RandomInstantiationCharacters()
    {
        if (isInstantiationCharacter)
        {
            isInstantiationCharacter = false;
            
            for (int i = 0; i < drunkeryCharacterNumber; i++)
            {
                RandomInstantiationCharacterFunc(i);
            }
        }
        yield return new WaitForSeconds(drunkeryRefreshTime);
        for (int i = 0; i < characterObj.Length; i++)
        {
            characterObj[i].GetComponent<Animator>().SetBool("isWait", false);
            ObjectPoolManager.Instance.RecycleMyGameObject(characterObj[i]);
        }
        isInstantiationCharacter = true;
        StopCoroutine("RandomInstantiationCharacters");
        StartCoroutine("RandomInstantiationCharacters");
    }

    /// <summary>
    /// 初始化之前数据
    /// </summary>
    void ClearAllPurchaseData()
    {
        characterName.text = "请选择";
        characterPrice.text = "请选择";
        HP.text = "请选择";
        AD_AP.text = "请选择";
        DEF_RES.text = "请选择";
    }
}
