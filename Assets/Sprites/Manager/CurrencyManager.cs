using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//货币管理类（金币、钻石）
//goldCoin:金币   diamond:钻石
public class CurrencyManager : ManagerBase<CurrencyManager>
{
    internal int goldCoin;
    internal int diamond;

    /// <summary>
    /// 进入游戏时执行从数据库加载货币数据的方法
    /// </summary>
    private void Start()
    {
        LoadCurrencyDataFromSQLite();
    }

    /// <summary>
    /// 从数据库加载货币数据到该管理类
    /// </summary>
    public void LoadCurrencyDataFromSQLite()
    {
        goldCoin = SQLiteManager.Instance.playerDataSource[1300].GoldCoin;
        diamond = SQLiteManager.Instance.playerDataSource[1300].Diamond;
    }

    /// <summary>
    /// 保存更新金币数据到数据库
    /// </summary>
    public void UpdateGoldCoinDataToSQLite()
    {
        SQLiteManager.Instance.UpdataDataFromTable(ConstData.Player,"GoldCoin",goldCoin,"ID",1300);
    }

    /// <summary>
    /// 保存更新钻石数据到数据库
    /// </summary>
    public void UpdateDiamondDataToSQLite()
    {
        SQLiteManager.Instance.UpdataDataFromTable(ConstData.Player, "Diamond", diamond, "ID", 1300);
    }

    /// <summary>
    /// 金币数量增加
    /// </summary>
    /// <param name="增加的数值"></param>
    public void GoldCoinIncrease(int increaseValue)
    {
        if (goldCoin < ConstData.GoldCoinMax)
        {
            goldCoin += increaseValue;
            if (goldCoin > ConstData.GoldCoinMax)
            {
                goldCoin = ConstData.GoldCoinMax;
            }
        }
        else
        {
            goldCoin = ConstData.GoldCoinMax;
        }
        UpdateGoldCoinDataToSQLite();
    }

    /// <summary>
    /// 金币数量减少
    /// </summary>
    /// <param name="减少的数值"></param>
    public void GoldCoinDecrease(int decreaseValue)
    {
        if (goldCoin > 0)
        {
            goldCoin -= decreaseValue;
            if (goldCoin < 0)
            {
                goldCoin = 0;
            }
        }
        else
        {
            goldCoin = 0;
        }
        UpdateGoldCoinDataToSQLite();
    }

    /// <summary>
    /// 钻石数量增加
    /// </summary>
    /// <param name="增加的数值"></param>
    public void DiamondIncrease(int increaseValue)
    {
        if (diamond < ConstData.GoldCoinMax)
        {
            diamond += increaseValue;
            if (diamond > ConstData.GoldCoinMax)
            {
                diamond = ConstData.GoldCoinMax;
            }
        }
        else
        {
            diamond = ConstData.GoldCoinMax;
        }
        UpdateDiamondDataToSQLite();
    }

    /// <summary>
    /// 钻石数量减少
    /// </summary>
    /// <param name="减少的数值"></param>
    public void DiamondDecrease(int decreaseValue)
    {
        if (diamond > 0)
        {
            diamond -= decreaseValue;
            if (diamond < 0)
            {
                diamond = 0;
            }
        }
        else
        {
            diamond = 0;
        }
        UpdateDiamondDataToSQLite();
    }

    /// <summary>
    /// 金币数量展示；会返回一个格式规范化后的String返回值，请直接使用返回值
    /// </summary>
    public string GoldCoinDisplay()
    {
        string goldCoinString = goldCoin.ToString();
        string resultGoldCoin = StringSplicingTool.StringSplicing(goldCoinString);
        return resultGoldCoin;
    }

    /// <summary>
    /// 钻石数量展示；会返回一个格式规范化后的String返回值，请直接使用返回值
    /// </summary>
    public string DiamondDisplay()
    {
        string diamondString = diamond.ToString();
        string resultDiamond = StringSplicingTool.StringSplicing(diamondString);
        return resultDiamond;
    }
}
