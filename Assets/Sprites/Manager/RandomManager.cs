using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 随机的概率管理
/// </summary>
public class RandomManager : ManagerBase<RandomManager>
{
    //默认随机最小等于1
    const float randomMinNumber = 1f;
    //默认随机最大等于100
    const float randomMaxNumber = 100f;
    //初级商城人物概率
    float randomShopCharacter = 80f;
    //初级酒店人物概率
    float randomCharacterOne = 97f;
    //二级普通人物概率
    float randomOrdinaryCharacterTwo = 86f;
    //二级稀有人物概率
    float randomRareCharacterTwo = 76f;

    #region 酒店、商店人物奖池
    //酒店普通角色A级数组
    int[] ordinaryCharacterArr01 = new int[]
        {
            1002,1003,1004,1005,1006,1007,1008,1009,
            1011,1013,1014,1016,1017,1020,1022,1025,
            1026,1027,1028,1030,1032,1033,1034,1035,
            1036,1037,1038,1040,1044,1046,1047,1049,
            1052,1054,1056,1057,1060,1061,1064,1065,
            1068,1069,1071,1072,1073,1076
        };
    //酒店普通角色B级数组
    int[] ordinaryCharacterArr02 = new int[]
        {
            1012,1021,1041,1045,1051,1053,1066,1075
        };
    //酒店稀有角色A级数组
    int[] rareCharacterArr01 = new int[]
        {
            1010,1015,1019,1023,1024,1031,1039,1042,
            1043,1050,1058,1059,1062,1063,1067,1070
        };
    //酒店稀有角色B级数组
    int[] rareCharacterArr02 = new int[]
        {
            1018,1029,1048,1055,1074
        };
    #endregion

    /// <summary>
    /// 根据概率获取随机人物
    /// </summary>
    /// <param 人物对应的场地类型="type"></param>
    /// <returns></returns>
    internal int GetRandomCharacter(CharacterFieldType type)
    {
        //用于存储不同场地概率
        float prob = 0f;
        //概率切换
        prob = type == CharacterFieldType.Hotel ? randomCharacterOne : randomShopCharacter;
        //临时存储结果值
        int result = 0;
        //百分比随机数
        float x = RandomCharacterFunc(randomMaxNumber);

        if (x <= prob)
        {
            //第二轮随机
            x = RandomCharacterFunc(randomMaxNumber);
            if (x <= randomOrdinaryCharacterTwo)
            {
                //抽取对应人物奖池
                result = (int)RandomCharacterFunc(ordinaryCharacterArr01.Length);
                result = ordinaryCharacterArr01[result];
            }
            else
            {
                //抽取对应人物奖池
                result = (int)RandomCharacterFunc(ordinaryCharacterArr02.Length);
                result = ordinaryCharacterArr02[result];
            }
        }
        else
        {
            //第二轮随机
            x = RandomCharacterFunc(randomMaxNumber);
            if (x <= randomRareCharacterTwo)
            {
                //抽取对应人物奖池
                result = (int)RandomCharacterFunc(rareCharacterArr01.Length);
                result = rareCharacterArr01[result];
            }
            else
            {
                //抽取对应人物奖池
                result = (int)RandomCharacterFunc(rareCharacterArr02.Length);
                result = rareCharacterArr02[result];
            }
        }
        return result;
    }

    /// <summary>
    /// 获取对应奖励(抽奖用)
    /// </summary>
    /// <param 奖励概率数组="prob"></param>
    /// <returns></returns>
    internal int GetPrize(float[] prob)
    {
        //临时存储结果值
        int result = 0;
        //随机值
        float x = RandomCharacterFunc(ArraySum(prob));
        //对比所有概率
        for (int i = 0; i < prob.Length; i++)
        {
            //最小区间值
            float pre = GetSectionFunc(i, prob);
            //最大区间值
            float next = GetSectionFunc(i + 1, prob);
            //对比
            if (x >= pre && x < next)
            {
                result = i;
                break;
            }
        }
        return result;
    }

    /// <summary>
    /// 取概率总和
    /// </summary>
    /// <param 概率数组="prob"></param>
    /// <returns></returns>
    float ArraySum(float[] prob)
    {
        float sum = 0;
        for (int i = 0; i < prob.Length; i++)
        {
            sum += prob[i];
        }
        return sum;
    }

    /// <summary>
    /// 获取随机到区间
    /// </summary>
    /// <param 奖池类型数量="num"></param>
    /// <param 概率数组="prob"></param>
    /// <returns></returns>
    float GetSectionFunc(int num, float[] prob)
    {
        float section = 0;
        for (int i = 0; i < num; i++)
        {
            section += prob[i];
        }
        return section;
    }

    /// <summary>
    /// 返回随机概率值
    /// </summary>
    /// <param 随机的最大值="maxNum"></param>
    /// <returns></returns>
    float RandomCharacterFunc(float maxNum)
    {
        float prob = Random.Range(randomMinNumber, maxNum);
        return prob;
    }
}
