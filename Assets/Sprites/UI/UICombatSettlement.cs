using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class UICombatSettlement : MonoBehaviour, IUIBase
{
    int currentGameLevel = 1;   //当前的游戏关卡数
    ////int originCoin;        //原有的金币
    int gotCoin;        //获取到的金币(关卡金币+战斗得到的金币)
    int getGameCoin;    //本关战斗获得的金币
    int UICoin;          //更新UI上获取到的金币
    int gotExp=1200;         //获取到的经验
    int perExp;         //每个英雄获取到的经验
    int perFinalExp;    //每个英雄结算后的经验
    GameObject[] group ;
    ////float iii = 0f;
    List<string> test;
    List<int> eqList = new List<int>();         //用来记录掉落的物品ID
    GameObject confirm;     //确认按钮
    Transform heroInfo;
    Transform itemInfo;
    Transform coinValue;    //金币显示组件
    Transform none;         //没有掉落物品的提示
    Transform itemGroup;    //掉落物品显示的父物体
    //添加事件基类
    UISceneWidget confirmButtonClick;

    /// <summary>
    /// UI界面进入初始化
    /// </summary>
    private void  UIStart()
    {
        getGameCoin = 100; ;//本关战斗额外获得的金币
        //将字典Team的key值存到list中,用来遍历
        test = new List<string>(SQLiteManager.Instance.team.Keys);
        //临时小组的长度值根据Team的小组成员数来的
        group = new GameObject[SQLiteManager.Instance.team.Count];
        heroInfo = transform.GetChild(0);   //找到heroInfo的物体
        itemInfo = transform.GetChild(1);   //找到ItemInfo的物体
        coinValue = itemInfo.GetChild(0).GetChild(1);       //找到金币显示物体
        confirm = transform.GetChild(2).GetChild(0).gameObject; //找到确认按钮

        #region 按钮事件的绑定
        confirmButtonClick = UISceneWidget.Get(confirm);
        if (confirmButtonClick != null)
        {
            confirmButtonClick.PointerClick -= ExitUI;
        }
        #endregion
        none = itemInfo.GetChild(1).GetChild(2);      //没有物品掉落时的用户提示
        itemGroup = itemInfo.GetChild(1).GetChild(1);      //没有物品掉落时的小组
        //Debug.Log("子物体的个数:" + heroInfo.childCount);
        //结算前的UI显示
        for (int i = 0; i < heroInfo.childCount; i++)
        {
            group[i] = heroInfo.GetChild(i).gameObject;
            group[i].transform.GetChild(0).GetComponent<Text>().text = SQLiteManager.Instance.team[test[i]].playerData.player_Name;       //Name
            group[i].transform.GetChild(1).GetComponent<Text>().text = string.Concat("等级:", SQLiteManager.Instance.team[test[i]].playerData.Level.ToString());         //等级
            group[i].transform.GetChild(2).GetComponent<Slider>().minValue = 0;
            if (SQLiteManager.Instance.team[test[i]].playerData.Level >1)
            {
                group[i].transform.GetChild(2).GetComponent<Slider>().minValue = SQLiteManager.Instance.lVDataSource[SQLiteManager.Instance.team[test[i]].playerData.Level - 1].level_MaxEXP;      //当前等级的最大经验值
            }
            group[i].transform.GetChild(2).GetComponent<Slider>().maxValue = SQLiteManager.Instance.lVDataSource[SQLiteManager.Instance.team[test[i]].playerData.Level].level_MaxEXP;      //当前等级的最大经验值
            //Debug.Log("maxValue:" + group[i].transform.GetChild(2).GetComponent<Slider>().maxValue);
            group[i].transform.GetChild(2).GetComponent<Slider>().value = SQLiteManager.Instance.team[test[i]].playerData.EXP;      //经验条
        }

        perExp = (int)gotExp / 6;
        for (int i = 0; i < heroInfo.childCount; i++)
        {
            perFinalExp= SQLiteManager.Instance.team[test[i]].playerData.EXP += perExp; //结算最终的经验值
            //获取的经验更新
            ExpUpdate(i, perFinalExp);
            UpdateHerosDCExp(i,perFinalExp);        //更新字典,更新每个英雄的经验
        }

        //战斗后先结算打怪得到的金币数
        SettlementCoin();
        //根据当前关卡数计算获取的金币数;并结算最终得到的金币数
        GetCoin(currentGameLevel);
        //更新获得金币值
        CoinUpdate();
        InvokeRepeating("UpdateUICoin", 0f, 0.1f);
        //根据关卡数获取掉落物品并显示
        GetEquipment(currentGameLevel);
        UpdateDCCoin(); //更新字典,更新金币
    }
    /// <summary>
    /// 用于显示金币
    /// </summary>
    private void UpdateUICoin()
    {
        if (UICoin<=gotCoin)
        {
            coinValue.GetComponent<Text>().text = UICoin.ToString();
        }
    }
    //经验条缓慢移动
    void ExpUpdate(int id, int finalExp)
    {
        UpdateExp(id, finalExp);
    }
    void UpdateExp(int id,int finalExp)
    {
        DOTween.To(() => group[id].transform.GetChild(2).GetComponent<Slider>().value,
                   xx => group[id].transform.GetChild(2).GetComponent<Slider>().value = xx, finalExp, 1f).SetEase(Ease.Linear).OnComplete(delegate ()
                   {
                        if (finalExp > group[id].transform.GetChild(2).GetComponent<Slider>().maxValue)
                        {
                           UpdateHerosLevel(id);
                           //设置显示等级值
                           group[id].transform.GetChild(1).GetComponent<Text>().text = StringSplicingTool.StringSplicing("等级:", SQLiteManager.Instance.team[test[id]].playerData.Level.ToString());        
                            //设置当前等级的最小经验值
                            group[id].transform.GetChild(2).GetComponent<Slider>().minValue = SQLiteManager.Instance.lVDataSource[SQLiteManager.Instance.team[test[id]].playerData.Level - 1].level_MaxEXP;
                            //设置当前等级的最大经验值
                            group[id].transform.GetChild(2).GetComponent<Slider>().maxValue = SQLiteManager.Instance.lVDataSource[SQLiteManager.Instance.team[test[id]].playerData.Level].level_MaxEXP;
                           //显示更新经验条的值
                           group[id].transform.GetChild(2).GetComponent<Slider>().value = group[id].transform.GetChild(2).GetComponent<Slider>().minValue;
                           //更新下一波进度条
                           UpdateExp(id, finalExp);
                        }
                       else
                       {
                           UpdataDB();                  //读条完毕更新数据库,更新了 金币 等级 经验
                           #region 按钮事件的绑定
                           confirmButtonClick = UISceneWidget.Get(confirm);
                           if (confirmButtonClick != null)
                           {
                               confirmButtonClick.PointerClick += ExitUI;
                           }
                           #endregion
                       }
                   });
    }
    //金币的动态显示
    void  CoinUpdate()
    {
        DOTween.To(() => UICoin,
                   xx => UICoin = xx, gotCoin, 1f);
    }
    /// <summary>
    /// //获得金币的计算
    /// </summary>
    /// <param name="gameLevel">关卡等级数</param>
    /// <returns></returns>
    int GetCoin(int gameLevel)
    {
        int coinLv = 0;
        switch (gameLevel)
        {
            case 0:
                coinLv = 3300;
                break;
            case 1:
                coinLv = 6700;
                break;
            case 2:
                coinLv = 8648;
                break;
            case 3:
                coinLv = 16358;
                break;
            case 4:
                coinLv = 23646;
                break;
            case 5:
                coinLv = 29531;
                break;
            case 6:
                coinLv = 36486;
                break;
            case 7:
                coinLv = 45318;
                break;
            case 8:
                coinLv = 56169;
                break;
            case 9:
                coinLv = 64248;
                break;
            case 10:
                coinLv = 79586;
                break;

            default:
                break;
        }
        gotCoin = coinLv + getGameCoin;
        return gotCoin;
    }
    /// <summary>
    /// 获取掉落的物品,并显示到UI
    /// </summary>
    /// <param name="gameLevel">关卡等级数</param>
    void GetEquipment(int gameLevel)
    {
        int randomNum ;       //用来记录产生的随机数
        bool isComlete = true;                      //是否通关
        if (isComlete)
        {
            switch (gameLevel)
            {
                case 1:
                    //是否掉落第一个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < 30) { eqList.Add(2003); }

                    //是否掉落第二个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (30 - eqList.Count * 15)) { eqList.Add(2018); }

                    //是否掉落第三个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (40 - eqList.Count * 10)) { eqList.Add(2053); }

                    //是否掉落第四个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (30 - eqList.Count * 15)) { eqList.Add(2054); }

                    //是否掉落第五个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 15)) { eqList.Add(2043); }

                    break;
                case 2:
                    //是否掉落第一个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < 40) { eqList.Add(2030); }

                    //是否掉落第二个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < 20) { eqList.Add(2019); }

                    //是否掉落第三个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2055); }

                    //是否掉落第四个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2004); }

                    //是否掉落第五个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2031); }
                    //是否掉落第六个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (15 - eqList.Count * 5)) { eqList.Add(2005); }

                    //是否掉落第七个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (15 - eqList.Count * 5)) { eqList.Add(2020); }

                    //是否掉落第八个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (15 - eqList.Count * 5)) { eqList.Add(2044); }

                    //是否掉落第九个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (15 - eqList.Count * 5)) { eqList.Add(2056); }

                    //是否掉落第十个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (10 - eqList.Count * 5)) { eqList.Add(2006); }
                    //是否掉落第十一个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (40 - eqList.Count * 10)) { eqList.Add(2112); }
                    //是否掉落第十二个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (10 - eqList.Count * 5)) { eqList.Add(2103); }
                    break;
                case 3:
                    //是否掉落第一个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < 20) { eqList.Add(2006); }

                    //是否掉落第二个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2007); }

                    //是否掉落第三个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (25 - eqList.Count * 5)) { eqList.Add(2021); }

                    //是否掉落第四个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2022); }

                    //是否掉落第五个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (30 - eqList.Count * 5)) { eqList.Add(2032); }
                    //是否掉落第六个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (25 - eqList.Count * 5)) { eqList.Add(2033); }

                    //是否掉落第七个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2034); }

                    //是否掉落第八个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (25 - eqList.Count * 5)) { eqList.Add(2045); }

                    //是否掉落第九个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2046); }

                    //是否掉落第十个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (25 - eqList.Count * 5)) { eqList.Add(2057); }
                  
                    //是否掉落第十一个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2058); }
                  
                    //是否掉落第十二个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (40 - eqList.Count * 20)) { eqList.Add(2107); }
                  
                    //是否掉落第十三个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (40 - eqList.Count * 20)) { eqList.Add(2117); }
                
                    //是否掉落第十四个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (40 - eqList.Count * 20)) { eqList.Add(2122); }
                    break;
                case 4:
                    //是否掉落第一个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < 20) { eqList.Add(2008); }

                    //是否掉落第二个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (35 - eqList.Count * 15)) { eqList.Add(2022); }

                    //是否掉落第三个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2023); }

                    //是否掉落第四个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (35 - eqList.Count * 15)) { eqList.Add(2034); }

                    //是否掉落第五个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2035); }
                    //是否掉落第六个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2047); }

                    //是否掉落第七个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2059); }

                    //是否掉落第八个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < 15) { eqList.Add(2123); }

                    break;
                case 5:
                    //是否掉落第一个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < 30) { eqList.Add(2008); }

                    //是否掉落第二个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2009); }

                    //是否掉落第三个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (35 - eqList.Count * 15)) { eqList.Add(2023); }

                    //是否掉落第四个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2024); }

                    //是否掉落第五个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2036); }
                    //是否掉落第六个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (35 - eqList.Count * 15)) { eqList.Add(2047); }

                    //是否掉落第七个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2048); }

                    //是否掉落第八个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (35 - eqList.Count * 15)) { eqList.Add(2059); }

                    //是否掉落第九个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2060); }

                    //是否掉落第十个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (50 - eqList.Count * 30)) { eqList.Add(2104); }

                    break;
                case 6:
                    //是否掉落第一个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < 30) { eqList.Add(2010); }

                    //是否掉落第二个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2011); }

                    //是否掉落第三个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2025); }

                    //是否掉落第四个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (35 - eqList.Count * 15)) { eqList.Add(2036); }

                    //是否掉落第五个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2037); }
                    //是否掉落第六个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (35 - eqList.Count * 15)) { eqList.Add(2048); }

                    //是否掉落第七个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2049); }

                    //是否掉落第八个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (35 - eqList.Count * 15)) { eqList.Add(2060); }

                    //是否掉落第九个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2061); }

                    //是否掉落第十个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (35 - eqList.Count * 10)) { eqList.Add(2113); }

                    //是否掉落第十一个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (30 - eqList.Count * 20)) { eqList.Add(2114); }

                    //是否掉落第十二个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (35 - eqList.Count * 10)) { eqList.Add(2118); }

                    break;
                case 7:
                    //是否掉落第一个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < 30) { eqList.Add(2011); }

                    //是否掉落第二个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (40 - eqList.Count * 15)) { eqList.Add(2012); }

                    //是否掉落第三个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2013); }

                    //是否掉落第四个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2026); }

                    //是否掉落第五个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (35 - eqList.Count * 10)) { eqList.Add(2037); }
                    //是否掉落第六个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2038); }

                    //是否掉落第七个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2050); }

                    //是否掉落第八个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (35 - eqList.Count * 10)) { eqList.Add(2061); }

                    //是否掉落第九个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2062); }

                    //是否掉落第十个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (35 - eqList.Count * 10)) { eqList.Add(2108); }

                    //是否掉落第十一个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (35 - eqList.Count * 10)) { eqList.Add(2119); }

                    //是否掉落第十二个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (10 - eqList.Count * 5)) { eqList.Add(2105); }

                    break;
                case 8:
                    //是否掉落第一个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < 30) { eqList.Add(2013); }

                    //是否掉落第二个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2014); }

                    //是否掉落第三个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2027); }

                    //是否掉落第四个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2039); }

                    //是否掉落第五个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (35 - eqList.Count * 10)) { eqList.Add(2050); }
                    //是否掉落第六个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2051); }

                    //是否掉落第七个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (35 - eqList.Count * 10)) { eqList.Add(2062); }

                    //是否掉落第八个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2063); }

                    //是否掉落第九个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (35 - eqList.Count * 10)) { eqList.Add(2109 ); }

                    //是否掉落第十个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (10 - eqList.Count * 5)) { eqList.Add(2115); }

                    break;
                case 9:
                    //是否掉落第一个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < 100) { eqList.Add(2014); }

                    //是否掉落第二个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2027); }

                    //是否掉落第三个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2039); }

                    //是否掉落第四个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2051); }

                    //是否掉落第五个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (20 - eqList.Count * 5)) { eqList.Add(2063); }
                    //是否掉落第六个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < 5) { eqList.Add(2015); }

                    //是否掉落第七个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < (40 - eqList.Count * 20)) { eqList.Add(2124); }

                    //是否掉落第八个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < 5) { eqList.Add(2110); }

                    break;
                case 10:
                    //是否掉落第一个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < 10) { eqList.Add(2120); }

                    //是否掉落第二个物品
                    randomNum = Random.Range(0, 100);
                    if (randomNum < 10) { eqList.Add(2125); }

                    break;

                default:
                    break;
            }
        }
        //Debug.Log("没有物品时,激活none,none的名字为:" + none.name);
        //Debug.Log("集合数量:" + eqList.Count);
        if (eqList.Count==0)
        {
            //激活显示没有物品
            //Debug.Log("没有物品时,激活none,none的名字为:" + none.name);
            none.gameObject.SetActive(true);
        }
        else
        {
            none.gameObject.SetActive(false);       //隐藏提示文字
            //Debug.Log("有物品掉落,关闭文字,Group的名字为:" + itemGroup.name);
            for (int i = 0; i < eqList.Count; i++)
            {
                //Debug.Log("生成的 物品的名字" + eqList[i].ToString());
                //Sprite eq = ResourcesManager.Instance.FindSprite(eqList[i].ToString());
                //Debug.Log("生成的 物品的名字" + eq.name);
                GameObject eqt = new GameObject(eqList[i].ToString());
                eqt.AddComponent<Image>().sprite = ResourcesManager.Instance.FindSprite(eqList[i].ToString()); 
                eqt.transform.parent = itemGroup;
                eqt.transform.localScale = Vector3.one;
                eqt.GetComponent<RectTransform>().localPosition = new Vector3(eqt.transform.localPosition.x,eqt.transform.localPosition.y,0);
            }
        }
    }
    /// <summary>
    /// 存档,将更新的数据更新存到数据库
    /// 先更新字典再更新数据库
    /// </summary>
    void UpdataDB( )
    {
        Debug.Log("按键确认过眼神");
        int coinDB = SQLiteManager.Instance.team["FlagMan"].playerData.GoldCoin;
        SQLiteManager.Instance.UpdataDataFromTable(ConstData.Player, "GoldCoin", coinDB, "ID", 1300);
        for (int i = 0; i < SQLiteManager.Instance.team.Count; i++)
        {
            int ExpDB = SQLiteManager.Instance.team[test[i]].playerData.EXP;                                      //获取最新的经验值
            int levelDB = SQLiteManager.Instance.team[test[i]].playerData.Level;                                  //获取最新的等级
            int heroID = SQLiteManager.Instance.team[test[i]].playerData.player_Id;                               //取每个英雄的自身ID
            SQLiteManager.Instance.UpdataDataFromTable(ConstData.Player, "player_EXP", ExpDB, "ID", heroID);      //根据英雄ID更新自身经验值  
            SQLiteManager.Instance.UpdataDataFromTable(ConstData.Player, "player_Level", levelDB, "ID", heroID);  //根据英雄ID更新自身等级值  
        }
    }
    /// <summary>
    /// 退出当前UI
    /// </summary>
    void ExitUI(PointerEventData eventData)
    {
        UICoin = 0;        //金币数置零
        eqList.Clear();
        CancelInvoke("UpdateUICoin");
        for (int i = 0; i < itemGroup.childCount; i++)
        {
            Destroy(itemGroup.GetChild(i).gameObject);     //清除生成的物品
            Debug.Log("清除掉落的物品");
        }
        UIManager.Instance.PopUIStack();        //退出当前界面
    }
    /// <summary>
    /// 更新字典中的金币数
    /// </summary>
    void UpdateDCCoin()
    {
        SQLiteManager.Instance.team["FlagMan"].playerData.GoldCoin += gotCoin;
    }
    /// <summary>
    /// 更新字典里每个出战英雄的经验值
    /// </summary>
    /// <param name="id"></param>
    void UpdateHerosDCExp(int id,int perFinalExp)
    {
        SQLiteManager.Instance.team[test[id]].playerData.EXP = perFinalExp;
    }
    /// <summary>
    /// 更新字典里每个出战英雄的等级
    /// </summary>
    void UpdateHerosLevel(int id)
    {
        SQLiteManager.Instance.team[test[id]].playerData.Level++;
    }
   
    /// <summary>
    /// 战斗中,根据敌人ID来累加结算经验值
    /// </summary>
    /// <param name="enemyID">敌人ID</param>
    public void SettlementExp(int enemyID)
    {
        gotExp += SQLiteManager.Instance.enemyDataSource[enemyID].EXP;
    }
    /// <summary>
    /// 战斗后先结算获得到的金币
    /// </summary>
    /// <returns></returns>
    public int SettlementCoin()
    {
        getGameCoin = GameManager.Instance.totalScore;      //从外部渠道获取金币值
        return getGameCoin;
    }
    //------------------------------------------------------------------------------------------------------------------//
    public void OnEntering()
    {
        gameObject.SetActive(true);
        UIStart();
    }

    public void OnExiting()
    {
        gameObject.SetActive(false);
    }

    public void OnPausing()
    {
        gameObject.SetActive(false);
    }

    public void OnResuming()
    {
        gameObject.SetActive(true);
        UIStart();
    }
    //-------------------------------------------------------------------------------------------//


}
