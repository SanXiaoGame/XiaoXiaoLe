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
        if (transform.GetComponent<UISceneWidget>() == null)
        {
            UISceneWidget propsClick = UISceneWidget.Get(gameObject);
            propsClick.PointerDown += PropsOnClick;
        }
    }

    private void Start()
    {
        flagman = GameObject.FindGameObjectWithTag(ConstData.FlagMan);
    }

    private void PropsOnClick(PointerEventData eventData)
    {
        if (GameManager.Instance.props_CubeBreakSwitch == false
            && GameManager.Instance.props_CubeChangeSwitch == false
            && GameManager.Instance.props_SkillCubeSwitch == false)
        {
            switch (gameObject.name)
            {
                case ConstData.CureCapsule:
                    CureTeamHealth(0.2f);
                    UIBattle.itemObjList.Remove(gameObject);
                    Destroy(gameObject);
                    DeleteItemData(2201);
                    break;
                case ConstData.Stimulant:
                    TeamGetState(3207, 5.0f);
                    UIBattle.itemObjList.Remove(gameObject);
                    Destroy(gameObject);
                    DeleteItemData(2202);
                    break;
                case ConstData.ReviveCross:
                    if (UIBattle.deadCharaList != null)
                    {
                        ReviveCross(0.2f);
                        UIBattle.itemObjList.Remove(gameObject);
                        Destroy(gameObject);
                        DeleteItemData(2203);
                    }
                    break;
                case ConstData.CubeBreak:
                    CubeBreak();
                    UIBattle.itemObjList.Remove(gameObject);
                    Destroy(gameObject);
                    DeleteItemData(2204);
                    break;
                case ConstData.SkeletonSpiritism:
                    CubeResetALL(0);
                    UIBattle.itemObjList.Remove(gameObject);
                    Destroy(gameObject);
                    DeleteItemData(2205);
                    break;
                case ConstData.HealthSyringe:
                    CureTeamHealth(0.8f);
                    UIBattle.itemObjList.Remove(gameObject);
                    Destroy(gameObject);
                    DeleteItemData(2206);
                    break;
                case ConstData.ParanephrineSyringe:
                    TeamGetState(3207, 8.0f);
                    TeamGetState(3209, 8.0f);
                    UIBattle.itemObjList.Remove(gameObject);
                    Destroy(gameObject);
                    DeleteItemData(2207);
                    break;
                case ConstData.HolyCross:
                    if (UIBattle.deadCharaList != null)
                    {
                        ReviveCross(0.8f);
                        UIBattle.itemObjList.Remove(gameObject);
                        Destroy(gameObject);
                        DeleteItemData(2208);
                    }
                    break;
                case ConstData.CubeTransfer:
                    CubeTransfer();
                    UIBattle.itemObjList.Remove(gameObject);
                    Destroy(gameObject);
                    DeleteItemData(2209);
                    break;
                case ConstData.LuckyCatBalloon:
                    LuckyCat();
                    UIBattle.itemObjList.Remove(gameObject);
                    Destroy(gameObject);
                    DeleteItemData(2210);
                    break;
                case ConstData.GhostSpiritism:
                    CubeResetALL(1);
                    UIBattle.itemObjList.Remove(gameObject);
                    Destroy(gameObject);
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
        flagman.GetComponent<FlagManController>().currentHP += (int)(flagman.GetComponent<FlagManController>().maxHP * Percent);
        if (flagman.GetComponent<FlagManController>().currentHP > flagman.GetComponent<FlagManController>().maxHP)
        {
            flagman.GetComponent<FlagManController>().currentHP = flagman.GetComponent<FlagManController>().maxHP;
        }
        Array.Clear(allPlayer, 0, allPlayer.Length);
    }
    /// <summary>
    /// 全队获得[?]状态x秒
    /// </summary>
    /// <param name="状态ID"></param>
    /// <param name="状态持续时间"></param>
    void TeamGetState(int stateID, float keepTime)
    {
        GameObject[] allPlayer = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < allPlayer.Length; i++)
        {
            if (allPlayer[i].GetComponent<HeroController>().isAlive == true)
            {
                allPlayer[i].GetComponent<HeroStates>().GetState(stateID, keepTime);
            }
        }
        Array.Clear(allPlayer, 0, allPlayer.Length);
    }
    /// <summary>
    /// 复活最近阵亡的角色，并将生命值置为x%
    /// </summary>
    /// <param name="生命值百分比(小数)"></param>
    void ReviveCross(float HPpercent)
    {
        int deadID = Convert.ToInt32(UIBattle.deadCharaList[UIBattle.deadCharaList.Count].name);
        string deadIDstr = deadID.ToString();
        GameObject a1 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPlayerPrefab(deadIDstr));
        a1.transform.GetComponent<Animator>().SetTrigger("Reset");
        a1.GetComponent<HeroStates>().currentHP = (int)(a1.GetComponent<HeroStates>().maxHP * HPpercent);
        a1.transform.position = flagman.transform.position + new Vector3(0.3f, 0, 0);
        //播放复活音效
        AudioManager.Instance.PlayEffectMusic(SoundEffect.Revive);
        //播放复活特效
        GameObject tempEffect = ObjectPoolManager.Instance.InstantiateMyGameObject
            (ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_revive));
        tempEffect.transform.position = flagman.transform.position + new Vector3(3, 0, 0);
        vp_Timer.In(0.6f, new vp_Timer.Callback(delegate () { ObjectPoolManager.Instance.RecycleMyGameObject(tempEffect); }));
        //改变各种开关
        a1.transform.GetComponent<HeroController>().moveSwitch_Battle = true;
    }
    /// <summary>
    /// 全屏块消除，同时判断是否视为使用了一次高级旗手块，0为否，1为是
    /// </summary>
    void CubeResetALL(int a)
    {
        ColumnManager.Instance.MedicinalWaterProp();
        if (a == 1)
        {
            GameObject[] allPlayer = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < allPlayer.Length; i++)
            {
                if (allPlayer[i].GetComponent<HeroController>().isAlive == true)
                {
                    switch (allPlayer[i].GetComponent<HeroController>().myClass)
                    {
                        case ConstData.Saber:
                            allPlayer[i].GetComponent<HeroController>().Skill_C(ConstData.Saber);
                            break;
                        case ConstData.Knight:
                            allPlayer[i].GetComponent<HeroController>().Skill_C(ConstData.Knight);
                            break;
                        case ConstData.Berserker:
                            allPlayer[i].GetComponent<HeroController>().Skill_C(ConstData.Berserker);
                            break;
                        case ConstData.Caster:
                            allPlayer[i].GetComponent<HeroController>().Skill_C(ConstData.Caster);
                            break;
                        case ConstData.Hunter:
                            allPlayer[i].GetComponent<HeroController>().Skill_C(ConstData.Hunter);
                            break;
                    }
                }
            }
            Array.Clear(allPlayer, 0, allPlayer.Length);
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
