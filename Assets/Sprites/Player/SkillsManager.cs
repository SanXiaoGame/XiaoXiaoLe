﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 剑士: Saber
 * 骑士: Knight
 * 法师: Caster
 * 猎人: Hunter
 * 狂战: Berserker
*/
public class SkillsManager : ManagerBase<SkillsManager>
{
    public delegate void ListionHandle();
    public static event ListionHandle Listion;
    public static void ListionChangeState()
    {
        if (Listion != null)
        {
            Listion();
        }

    }

    protected override void Awake()
    {

    }

    #region 英雄技能释放的接口方法
    /// <summary>
    /// 外界调用接口
    /// </summary>
    /// <param name="heroType"></param>
    /// <param name="skillLevel"></param>
    public void FireSkill(Hero hero, int skillLevel)
    {

        string heroType = hero.playerData.player_Class;
        switch (heroType)
        {
            case "剑士":
            case "圣剑士":
                SaberSkillBySkillLevel(hero, skillLevel);
                break;
            case "骑士":
            case "圣骑士":
                KnightFireSkillBySkillLevel(hero, skillLevel);
                break;
            case "法师":
                CasterFireSkillBySkillLevel(hero, skillLevel);
                break;
            case "猎人":
                HunterFireSkillBySkillLevel(hero, skillLevel);
                break;
            case "狂战":
                BerserkerFireSkillBySkillLevel(hero, skillLevel);
                break;
            default:
                break;
        }
    }
    #endregion

    #region 英雄们释放不同的技能
    /// <summary>
    /// 剑士技能释放
    /// </summary>
    /// <param name="skillLevel"></param>
    public void SaberSkillBySkillLevel(Hero hero, int skillLevel)
    {

        switch (skillLevel)
        {
            case 0:
                SaberCommonAttack(hero);
                break;
            case 1:
                SaberOneSkill(hero);
                break;
            case 2:
                SaberTwoSkill(hero);
                break;
            case 3:
                SaberThreeSkill(hero);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 骑士技能释放
    /// </summary>
    /// <param name="skillLevel"></param>
    public void KnightFireSkillBySkillLevel(Hero hero, int skillLevel)
    {
        switch (skillLevel)
        {
            case 0:
                KnightCommonAttack(hero);
                break;
            case 1:
                KnightOneSkill(hero);
                break;
            case 2:
                KnightTwoSkill(hero);
                break;
            case 3:
                KnightThreeSkill(hero);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 法师技能释放
    /// </summary>
    /// <param name="skillLevel"></param>
    public void CasterFireSkillBySkillLevel(Hero hero, int skillLevel)
    {
        switch (skillLevel)
        {
            case 0:
                CasterCommonAttack(hero);
                break;
            case 1:
                CasterOneSkill(hero);
                break;
            case 2:
                CasterTwoSkill(hero);
                break;
            case 3:
                CasterThreeSkill(hero);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 猎人技能释放
    /// </summary>
    /// <param name="skillLevel"></param>
    public void HunterFireSkillBySkillLevel(Hero hero, int skillLevel)
    {
        switch (skillLevel)
        {
            case 0:
                HunterCommonAttack(hero);
                break;
            case 1:
                HunterOneSkill(hero);
                break;
            case 2:
                HunterTwoSkill(hero);
                break;
            case 3:
                HunterThreeSkill(hero);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 狂战士技能释放
    /// </summary>
    /// <param name="skillLevel"></param>
    public void BerserkerFireSkillBySkillLevel(Hero hero, int skillLevel)
    {
        switch (skillLevel)
        {
            case 0:
                BerserkerCommonAttack(hero);
                break;
            case 1:
                BerserkerOneSkill(hero);
                break;
            case 2:
                BerserkerTwoSkill(hero);
                break;
            case 3:
                BerserkerThreeSkill(hero);
                break;
            default:
                break;
        }
    }
    #endregion

    #region 英雄们的普通攻击
    /// <summary>
    /// 剑士的普通攻击
    /// </summary>
    public void SaberCommonAttack(Hero hero)
    {
      
        Debug.Log("释放剑士普通攻击");
    }
    /// <summary>
    /// 骑士的普通攻击
    /// </summary>
    public void KnightCommonAttack(Hero hero)
    {
        Debug.Log("释放骑士普通攻击");

    }
    /// <summary>
    /// 法师的普通攻击
    /// </summary>
    public void CasterCommonAttack(Hero hero)
    {
        Debug.Log("释放法师普通攻击");

    }
    /// <summary>
    /// 猎人的普通攻击
    /// </summary>
    public void HunterCommonAttack(Hero hero)
    {
        Debug.Log("释放猎人普通攻击");

    }
    /// <summary>
    /// 狂战士的普通攻击
    /// </summary>
    public void BerserkerCommonAttack(Hero hero)
    {
        Debug.Log("释放狂战士普通攻击");
    }
    #endregion

    #region 剑士的技能列表
    /// <summary>
    /// 技能名:突刺;技能等级:一级
    /// </summary>
    /// <param name="hero">Hero.</param>
    public void SaberOneSkill(Hero hero)
    {
        hero.skillData = SQLiteManager.Instance.skillDataSource[SwordsmanSkillID.oneSkill.GetHashCode()];

        Debug.Log(" 突刺:这是" + hero.playerData.player_Name +
                  " 技能ID:" + SQLiteManager.Instance.skillDataSource[SwordsmanSkillID.oneSkill.GetHashCode()].skill_ID +
                  " 释放的攻击,技能名为:" + hero.skillData.skill_Name + 
                  " 技能等级:" + hero.skillData.skill_DamageLevel);        //播放动画....//播放一技能动画

        //播放音效

        //伤害计算业务处理

        //释放技能后回到一定状态:普通攻击状态
        //ChangeHeroState(hero.playerData.player_Id, HeroState.commonAttack.GetHashCode());
    }
    /// <summary>
    /// 技能名:剑气;技能等级:二级
    /// </summary>
    /// <param name="hero">Hero.</param>
    public void SaberTwoSkill(Hero hero)
    {
        hero.skillData = SQLiteManager.Instance.skillDataSource[SwordsmanSkillID.twoSkill.GetHashCode()];
        Debug.Log(" 这是" + hero.playerData.player_Name + 
                  " 技能ID:"+hero.skillData.skill_ID +
                  " 释放的攻击,技能名为:" + hero.skillData.skill_Name + 
                  " 技能等级:" + hero.skillData.skill_DamageLevel);

        //请求技能数据,并打印输出其描述
        //PlayerData playerData = hero.playerData;
        //Animator heroAnimator = hero.animator;
        //StateData stateData = hero.stateData;

        //播放动画

        //播放音效

        //伤害计算业务处理

      
        //释放技能后回到一定状态:普通攻击状态
        //ChangeHeroState(hero.playerData.player_Id, HeroState.commonAttack.GetHashCode());
    }
    /// <summary>
    /// 技能名:六光连斩;技能等级:三级
    /// </summary>
    /// <param name="hero">Hero.</param>
    public void SaberThreeSkill(Hero hero)
    {
        hero.skillData = SQLiteManager.Instance.skillDataSource[SwordsmanSkillID.threeSkill.GetHashCode()];
        Debug.Log(" 这是" + hero.playerData.player_Name +
                  " 技能ID:" + hero.skillData.skill_ID +
                  " 释放的攻击,技能名为:" + hero.skillData.skill_Name + 
                  " 技能等级:" + hero.skillData.skill_DamageLevel);
        //播放动画

        //播放音效

        //伤害计算业务处理

      
        //释放技能后回到一定状态:普通攻击状态
        //ChangeHeroState(hero.playerData.player_Id, HeroState.commonAttack.GetHashCode());
    }
    #endregion

    #region 骑士的技能列表

    /// <summary>
    /// 技能名:信仰;技能等级:一级
    /// </summary>
    /// <param name="hero">Hero.</param>
    public void KnightOneSkill(Hero hero)
    {

        hero.skillData = SQLiteManager.Instance.skillDataSource[KnightSkillID.oneSkill.GetHashCode()];
        Debug.Log(" 这是" + hero.playerData.player_Name +
                  " 技能ID:" + hero.skillData.skill_ID +
                  " 释放的攻击,技能名为:" + hero.skillData.skill_Name +
                  " 技能等级:" + hero.skillData.skill_DamageLevel);
        //播放动画

        //播放音效

        //伤害计算业务处理


        //释放技能后回到一定状态:普通攻击状态
        //ChangeHeroState(hero.playerData.player_Id, HeroState.commonAttack.GetHashCode());

    }
    /// <summary>
    /// 技能名:圣盾;技能等级:二级
    /// </summary>
    /// <param name="hero">Hero.</param>
    public void KnightTwoSkill(Hero hero)
    {

        hero.skillData = SQLiteManager.Instance.skillDataSource[KnightSkillID.twoSkill.GetHashCode()];
        Debug.Log(" 这是" + hero.playerData.player_Name +
                  " 技能ID:" + hero.skillData.skill_ID +
                  " 释放的攻击,技能名为:" + hero.skillData.skill_Name +
                  " 技能等级:" + hero.skillData.skill_DamageLevel);
        //播放动画

        //播放音效

        //伤害计算业务处理


        //释放技能后回到一定状态:普通攻击状态
        //ChangeHeroState(hero.playerData.player_Id, HeroState.commonAttack.GetHashCode());
    }
    /// <summary>
    /// 技能名:圣矛投掷;技能等级:三级
    /// </summary>
    /// <param name="hero">Hero.</param>
    public void KnightThreeSkill(Hero hero)
    {

        hero.skillData = SQLiteManager.Instance.skillDataSource[KnightSkillID.threeSkill.GetHashCode()];
        Debug.Log(" 这是" + hero.playerData.player_Name +
                  " 技能ID:" + hero.skillData.skill_ID +
                  " 释放的攻击,技能名为:" + hero.skillData.skill_Name +
                  " 技能等级:" + hero.skillData.skill_DamageLevel);
        //播放动画

        //播放音效

        //伤害计算业务处理


        //释放技能后回到一定状态:普通攻击状态
        //ChangeHeroState(hero.playerData.player_Id, HeroState.commonAttack.GetHashCode());
    }
    #endregion

    #region 法师的技能列表
    /// <summary>
    /// 技能名:冰冻术;技能等级:一级
    /// </summary>
    /// <param name="hero">Hero.</param>
    public void CasterOneSkill(Hero hero)
    {
        hero.skillData = SQLiteManager.Instance.skillDataSource[MasterSkillID.oneSkill.GetHashCode()];
        Debug.Log(" 这是" + hero.playerData.player_Name +
                  " 技能ID:" + hero.skillData.skill_ID +
                  " 释放的攻击,技能名为:" + hero.skillData.skill_Name +
                  " 技能等级:" + hero.skillData.skill_DamageLevel);
        //播放动画....//播放一技能动画

        //播放音效

        //伤害计算业务处理

        //释放技能后回到一定状态:普通攻击状态
        //ChangeHeroState(hero.playerData.player_Id, HeroState.commonAttack.GetHashCode());

    }
    /// <summary>
    /// 技能名:陨石术;技能等级:二级
    /// </summary>
    /// <param name="hero">Hero.</param>
    public void CasterTwoSkill(Hero hero)
    {
        hero.skillData = SQLiteManager.Instance.skillDataSource[MasterSkillID.twoSkill.GetHashCode()];
        Debug.Log(" 这是" + hero.playerData.player_Name +
                  " 技能ID:" + hero.skillData.skill_ID +
                  " 释放的攻击,技能名为:" + hero.skillData.skill_Name +
                  " 技能等级:" + hero.skillData.skill_DamageLevel);

        //请求技能数据,并打印输出其描述
        //PlayerData playerData = hero.playerData;
        //Animator heroAnimator = hero.animator;
        //StateData stateData = hero.stateData;

        //播放动画

        //播放音效

        //伤害计算业务处理

        //释放技能后回到一定状态:普通攻击状态
        //ChangeHeroState(hero.playerData.player_Id, HeroState.commonAttack.GetHashCode());
    }
    /// <summary>
    /// 技能名:黑魔法;技能等级:三级
    /// </summary>
    /// <param name="hero">Hero.</param>
    public void CasterThreeSkill(Hero hero)
    {
        hero.skillData = SQLiteManager.Instance.skillDataSource[MasterSkillID.threeSkill.GetHashCode()];
        Debug.Log(" 这是" + hero.playerData.player_Name +
                  " 技能ID:" + hero.skillData.skill_ID +
                  " 释放的攻击,技能名为:" + hero.skillData.skill_Name +
                  " 技能等级:" + hero.skillData.skill_DamageLevel);
        //播放动画

        //播放音效

        //伤害计算业务处理

        //释放技能后回到一定状态:普通攻击状态
        //ChangeHeroState(hero.playerData.player_Id, HeroState.commonAttack.GetHashCode());
    }
    #endregion

    #region 猎人的技能列表
    /// <summary>
    /// 技能名:爆破箭;技能等级:一级
    /// </summary>
    /// <param name="hero">Hero.</param>
    public void HunterOneSkill(Hero hero)
    {
        hero.skillData = SQLiteManager.Instance.skillDataSource[HunterSkillID.oneSkill.GetHashCode()];
        Debug.Log(" 这是" + hero.playerData.player_Name +
                  " 技能ID:" + hero.skillData.skill_ID +
                  " 释放的攻击,技能名为:" + hero.skillData.skill_Name +
                  " 技能等级:" + hero.skillData.skill_DamageLevel);
        //播放动画....//播放一技能动画

        //播放音效

        //伤害计算业务处理

        //释放技能后回到一定状态:普通攻击状态
        //ChangeHeroState(hero.playerData.player_Id, HeroState.commonAttack.GetHashCode());

    }
    /// <summary>
    /// 技能名:螺旋剑;技能等级:二级
    /// </summary>
    /// <param name="hero">Hero.</param>
    public void HunterTwoSkill(Hero hero)
    {
        hero.skillData = SQLiteManager.Instance.skillDataSource[HunterSkillID.twoSkill.GetHashCode()];
        Debug.Log(" 这是" + hero.playerData.player_Name +
                  " 技能ID:" + hero.skillData.skill_ID +
                  " 释放的攻击,技能名为:" + hero.skillData.skill_Name +
                  " 技能等级:" + hero.skillData.skill_DamageLevel);
        //请求技能数据,并打印输出其描述
        //PlayerData playerData = hero.playerData;
        //Animator heroAnimator = hero.animator;
        //StateData stateData = hero.stateData;

        //播放动画

        //播放音效

        //伤害计算业务处理

        //释放技能后回到一定状态:普通攻击状态
        //ChangeHeroState(hero.playerData.player_Id, HeroState.commonAttack.GetHashCode());
    }
    /// <summary>
    /// 技能名:火箭雨;技能等级:三级
    /// </summary>
    /// <param name="hero">Hero.</param>
    public void HunterThreeSkill(Hero hero)
    {
        hero.skillData = SQLiteManager.Instance.skillDataSource[HunterSkillID.threeSkill.GetHashCode()];
        Debug.Log(" 这是" + hero.playerData.player_Name +
                  " 技能ID:" + hero.skillData.skill_ID +
                  " 释放的攻击,技能名为:" + hero.skillData.skill_Name +
                  " 技能等级:" + hero.skillData.skill_DamageLevel);
        //播放动画

        //播放音效

        //伤害计算业务处理

        //释放技能后回到一定状态:普通攻击状态
        //ChangeHeroState(hero.playerData.player_Id, HeroState.commonAttack.GetHashCode());
    }
    #endregion

    #region 狂战的技能列表
    /// <summary>
    /// 技能名:跳劈;技能等级:一级
    /// </summary>
    /// <param name="hero">Hero.</param>
    public void BerserkerOneSkill(Hero hero)
    {
        hero.skillData = SQLiteManager.Instance.skillDataSource[BerserkerSkillID.oneSkill.GetHashCode()];
        Debug.Log(" 这是" + hero.playerData.player_Name +
                  " 技能ID:" + hero.skillData.skill_ID +
                  " 释放的攻击,技能名为:" + hero.skillData.skill_Name +
                  " 技能等级:" + hero.skillData.skill_DamageLevel);
        //播放动画....//播放一技能动画

        //播放音效

        //伤害计算业务处理

        //释放技能后回到一定状态:普通攻击状态
        //ChangeHeroState(hero.playerData.player_Id, HeroState.commonAttack.GetHashCode());
    }
    /// <summary>
    /// 技能名:嗜血;技能等级:二级
    /// </summary>
    /// <param name="hero">Hero.</param>
    public void BerserkerTwoSkill(Hero hero)
    {
        hero.skillData = SQLiteManager.Instance.skillDataSource[BerserkerSkillID.twoSkill.GetHashCode()];
        Debug.Log(" 这是" + hero.playerData.player_Name +
                  " 技能ID:" + hero.skillData.skill_ID +
                  " 释放的攻击,技能名为:" + hero.skillData.skill_Name +
                  " 技能等级:" + hero.skillData.skill_DamageLevel);
        //请求技能数据,并打印输出其描述
        //PlayerData playerData = hero.playerData;
        //Animator heroAnimator = hero.animator;
        //StateData stateData = hero.stateData;

        //播放动画

        //播放音效

        //伤害计算业务处理

        //释放技能后回到一定状态:普通攻击状态
        //ChangeHeroState(hero.playerData.player_Id, HeroState.commonAttack.GetHashCode());
    }
    /// <summary>
    /// 技能名:地裂;技能等级:三级
    /// </summary>
    /// <param name="hero">Hero.</param>
    public void BerserkerThreeSkill(Hero hero)
    {
        hero.skillData = SQLiteManager.Instance.skillDataSource[BerserkerSkillID.threeSkill.GetHashCode()];
        Debug.Log(" 这是" + hero.playerData.player_Name +
                  " 技能ID:" + hero.skillData.skill_ID +
                  " 释放的攻击,技能名为:" + hero.skillData.skill_Name +
                  " 技能等级:" + hero.skillData.skill_DamageLevel);
        //播放动画

        //播放音效

        //伤害计算业务处理

        //释放技能后回到一定状态:普通攻击状态
        //ChangeHeroState(hero.playerData.player_Id, HeroState.commonAttack.GetHashCode());
    }
    #endregion

    #region public void ChangeHeroState(int heroID, int stateID)  改变玩家英雄状态的接口方法
    /// <summary>
    /// 改变玩家英雄状态的接口方法
    /// </summary>
    /// <param name="heroID">英雄的职业ID.</param>
    /// <param name="stateID">英雄的状态ID.</param>
    public void ChangeHeroState(int heroID, int stateID)
    {
        //在PlayerDataSource字典里,将对应英雄的状态ID更改;
        SQLiteManager.Instance.playerDataSource[heroID].stateID = stateID;
        //英雄hero的PlayerDate数据重新从字典里获取PlayerData
        SQLiteManager.Instance.team[heroID].playerData =
                         SQLiteManager.Instance.playerDataSource[heroID];
        //英雄的状态stateData数据,重新根据更改后的stateID获取
        SQLiteManager.Instance.team[heroID].stateData =
            SQLiteManager.Instance.stateDataSource[SQLiteManager.Instance.team[heroID].playerData.stateID];
        //经过状态更新后,以后再监听的状态才会改变

        ListionChangeState();
    }
    #endregion

    #region MyRegion

    #endregion
}


