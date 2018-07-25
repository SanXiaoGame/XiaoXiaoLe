using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class PropsFunction : MonoBehaviour
{
    UISceneWidget propsClick;
    GameObject flagman;

    private void Awake()
    {
        propsClick = UISceneWidget.Get(gameObject);

        if (propsClick != null)
        {
            propsClick.PointerDown += PropsOnClick;
        }
    }

    private void PropsOnClick(PointerEventData eventData)
    {
        flagman = GameObject.FindGameObjectWithTag(ConstData.FlagMan);
        if (GameManager.Instance.props_CubeBreakSwitch == false
            && GameManager.Instance.props_CubeChangeSwitch == false
            && GameManager.Instance.props_SkillCubeSwitch == false)
        {
            switch (eventData.pointerEnter.name)
            {
                case ConstData.CureCapsule:
                    CureTeamHealth(0.2f);
                    Destroy(eventData.pointerEnter);
                    DeleteItemData(2201);
                    break;
                case ConstData.Stimulant:
                    TeamGetState(3207, 5);
                    Destroy(eventData.pointerEnter);
                    DeleteItemData(2202);
                    break;
                case ConstData.ReviveCross:
                    ReviveCross(0.2f);
                    Destroy(eventData.pointerEnter);
                    DeleteItemData(2203);
                    break;
                case ConstData.CubeBreak:
                    CubeBreak();
                    Destroy(eventData.pointerEnter);
                    DeleteItemData(2204);
                    break;
                case ConstData.SkeletonSpiritism:
                    CubeResetALL(0);
                    Destroy(eventData.pointerEnter);
                    DeleteItemData(2205);
                    break;
                case ConstData.HealthSyringe:
                    CureTeamHealth(0.8f);
                    Destroy(eventData.pointerEnter);
                    DeleteItemData(2206);
                    break;
                case ConstData.ParanephrineSyringe:
                    TeamGetState(3207, 8);
                    TeamGetState(3209, 8);
                    Destroy(eventData.pointerEnter);
                    DeleteItemData(2207);
                    break;
                case ConstData.HolyCross:
                    ReviveCross(0.8f);
                    Destroy(eventData.pointerEnter);
                    DeleteItemData(2208);
                    break;
                case ConstData.CubeTransfer:
                    CubeTransfer();
                    Destroy(eventData.pointerEnter);
                    DeleteItemData(2209);
                    break;
                case ConstData.LuckyCatBalloon:
                    LuckyCat();
                    Destroy(eventData.pointerEnter);
                    DeleteItemData(2210);
                    break;
                case ConstData.GhostSpiritism:
                    CubeResetALL(1);
                    Destroy(eventData.pointerEnter);
                    DeleteItemData(2211);
                    break;
            }
        }
    }

    /// <summary>
    /// 使用完毕后删除玩家背包里的道具信息
    /// </summary>
    /// <param name="要删除的道具ID"></param>
    void DeleteItemData(int ItemID)
    {
        foreach (BagData item in SQLiteManager.Instance.bagDataSource.Values)
        {
            if (item.Bag_Consumable == ItemID)
            {
                item.Bag_Consumable = 0;
                return;
            }
        }
    }
    /// <summary>
    /// 全队恢复x%生命值
    /// </summary>
    /// <param name="Percent"></param>
    void CureTeamHealth(float Percent)
    {
        GameObject[] allPlayer = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < allPlayer.Length; i++)
        {
            if (allPlayer[i].GetComponent<HeroController>().isAlive == true)
            {
                allPlayer[i].GetComponent<HeroStates>().currentHP += (int)(allPlayer[i].GetComponent<HeroStates>().maxHP * Percent);
                if (allPlayer[i].GetComponent<HeroStates>().currentHP > allPlayer[i].GetComponent<HeroStates>().maxHP)
                {
                    allPlayer[i].GetComponent<HeroStates>().currentHP = allPlayer[i].GetComponent<HeroStates>().maxHP;
                }
            }
        }
        flagman.GetComponent<HeroStates>().currentHP += (int)(flagman.GetComponent<HeroStates>().maxHP * Percent);
        if (flagman.GetComponent<HeroStates>().currentHP > flagman.GetComponent<HeroStates>().maxHP)
        {
            flagman.GetComponent<HeroStates>().currentHP = flagman.GetComponent<HeroStates>().maxHP;
        }
        Array.Clear(allPlayer, 0, allPlayer.Length);
    }
    /// <summary>
    /// 全队获得[?]状态x秒
    /// </summary>
    /// <param name="状态ID"></param>
    /// <param name="状态持续时间"></param>
    void TeamGetState(int stateID, int keepTime)
    {
        foreach (HeroData item in SQLiteManager.Instance.team.Values)
        {
            StateData tempData = SQLiteManager.Instance.stateDataSource[stateID];
            tempData.state_KeepTime = keepTime;
            //item.stateData.Add(tempData);
        }
    }
    /// <summary>
    /// 复活最近阵亡的角色，并将生命值置为x%
    /// </summary>
    /// <param name="生命值百分比(小数)"></param>
    void ReviveCross(float HPpercent)
    {
        //待写：获取最近阵亡角色ID
        string deadIDstr = " ";
        GameObject a1 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPlayerPrefab(deadIDstr));
        a1.transform.GetComponent<Animator>().SetTrigger("Reset");
        a1.GetComponent<HeroStates>().currentHP = (int)(a1.GetComponent<HeroStates>().maxHP * 0.2f);
        a1.transform.position = flagman.transform.position;
        //改变各种开关
    }
    /// <summary>
    /// 全屏块消除，同时判断是否视为使用了一次高级旗手块，0为否，1为是
    /// </summary>
    void CubeResetALL(int a)
    {
        ColumnManager.Instance.MedicinalWaterProp();
        if (a == 1)
        {
            //待写：所有角色放3技能
        }
    }
    /// <summary>
    /// 直接消除一个技能块
    /// </summary>
    void CubeBreak()
    {
        GameManager.Instance.props_CubeBreakSwitch = true;
    }
    /// <summary>
    /// 选择两个块交换位置
    /// </summary>
    void CubeTransfer()
    {
        GameManager.Instance.props_CubeChangeSwitch = true;
    }
    /// <summary>
    /// 将指定位置的技能块改变为一个旗手块
    /// </summary>
    void LuckyCat()
    {
        GameManager.Instance.props_SkillCubeSwitch = true;
    }
}
