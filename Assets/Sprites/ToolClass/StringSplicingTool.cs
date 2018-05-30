using UnityEngine;
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
}
