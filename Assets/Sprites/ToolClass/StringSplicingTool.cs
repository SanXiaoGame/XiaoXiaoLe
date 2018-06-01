using UnityEngine;
using System;
/// <summary>
/// 字符串拼接工具
/// </summary>
public struct StringSplicingTool
{
    public static string StringSplicing(string stringA, string stringB)
    {

        Debug.Log(string.Format("{0}{1}", stringA, stringB));
        return string.Format("{0}{1}", stringA, stringB);
    }
    public static string StringSplicing(string[] stringN)
    {
        string SplicingString = stringN[0];
        for (int i = 1; i < stringN.Length; i++)
        {
            SplicingString = string.Format("{0}{1}", SplicingString, stringN[i]);
        }
        Debug.Log(SplicingString);
        return SplicingString;
    }
    /// <summary>
    /// 拼接金币专用
    /// </summary>
    /// <param 金币数组="coin"></param>
    /// <returns></returns>
    public static string StringSplicing(string coin)
    {
        string GoldCoinNumber = coin[coin.Length - 1].ToString();
        for (int i = coin.Length - 2; i >= 0; i--)
        {
            GoldCoinNumber = string.Format("{0}{1}", coin[i].ToString(), GoldCoinNumber);
            if (i != 0 && (coin.Length - i) % 3 == 0)
            {
                GoldCoinNumber = string.Format("{0}{1}", ",", GoldCoinNumber);
            }
        }
        Debug.Log(GoldCoinNumber);
        return GoldCoinNumber;
    }
}
