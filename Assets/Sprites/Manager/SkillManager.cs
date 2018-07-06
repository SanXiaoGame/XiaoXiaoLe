using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 技能管理
// 消块等释放技能，统一到这来调用方法
public class SkillManager : ManagerBase<SkillManager>
{
    //各英雄对象
    GameObject saber;
    GameObject knight;
    GameObject berserker;
    GameObject caster;
    GameObject hunter;

    protected override void Awake()
    {
        base.Awake();
        //通过team字典里的预制体字段获取各职业对应的场内对象
        saber = transform.Find("/" + SQLiteManager.Instance.team[ConstData.Saber].playerData.PrefabsID).gameObject;
        knight = transform.Find("/" + SQLiteManager.Instance.team[ConstData.Knight].playerData.PrefabsID).gameObject;
        berserker = transform.Find("/" + SQLiteManager.Instance.team[ConstData.Berserker].playerData.PrefabsID).gameObject;
        caster = transform.Find("/" + SQLiteManager.Instance.team[ConstData.Caster].playerData.PrefabsID).gameObject;
        hunter = transform.Find("/" + SQLiteManager.Instance.team[ConstData.Hunter].playerData.PrefabsID).gameObject;
    }

    private void OnEnable()
    {
        //通过team字典里的预制体字段获取各职业对应的场内对象
        saber = transform.Find("/" + SQLiteManager.Instance.team[ConstData.Saber].playerData.PrefabsID).gameObject;
        knight = transform.Find("/" + SQLiteManager.Instance.team[ConstData.Knight].playerData.PrefabsID).gameObject;
        berserker = transform.Find("/" + SQLiteManager.Instance.team[ConstData.Berserker].playerData.PrefabsID).gameObject;
        caster = transform.Find("/" + SQLiteManager.Instance.team[ConstData.Caster].playerData.PrefabsID).gameObject;
        hunter = transform.Find("/" + SQLiteManager.Instance.team[ConstData.Hunter].playerData.PrefabsID).gameObject;
    }

    /// <summary>
    /// 一阶技能调用方法：谁调用，参数就写谁的职业
    /// </summary>
    /// <param 英雄职业="heroClass"></param>
    internal void A_ClassSkill(string heroClass)
    {
        switch (heroClass)
        {
            case ConstData.Saber:
                saber.GetComponent<HeroController>().Skill_A(ConstData.Saber);
                break;
            case ConstData.Knight:
                knight.GetComponent<HeroController>().Skill_A(ConstData.Knight);
                break;
            case ConstData.Berserker:
                berserker.GetComponent<HeroController>().Skill_A(ConstData.Berserker);
                break;
            case ConstData.Caster:
                caster.GetComponent<HeroController>().Skill_A(ConstData.Caster);
                break;
            case ConstData.Hunter:
                hunter.GetComponent<HeroController>().Skill_A(ConstData.Hunter);
                break;
        }
    }
    /// <summary>
    /// 二阶技能调用方法：谁调用，参数就写谁的职业
    /// </summary>
    /// <param 英雄职业="heroClass"></param>
    internal void B_ClassSkill(string heroClass)
    {
        switch (heroClass)
        {
            case ConstData.Saber:
                saber.GetComponent<HeroController>().Skill_B(ConstData.Saber);
                break;
            case ConstData.Knight:
                knight.GetComponent<HeroController>().Skill_B(ConstData.Knight);
                break;
            case ConstData.Berserker:
                berserker.GetComponent<HeroController>().Skill_B(ConstData.Berserker);
                break;
            case ConstData.Caster:
                caster.GetComponent<HeroController>().Skill_B(ConstData.Caster);
                break;
            case ConstData.Hunter:
                hunter.GetComponent<HeroController>().Skill_B(ConstData.Hunter);
                break;
        }
    }
    /// <summary>
    /// 三阶技能调用方法：谁调用，参数就写谁的职业
    /// </summary>
    /// <param 英雄职业="heroClass"></param>
    internal void C_ClassSkill(string heroClass)
    {
        switch (heroClass)
        {
            case ConstData.Saber:
                saber.GetComponent<HeroController>().Skill_C(ConstData.Saber);
                break;
            case ConstData.Knight:
                knight.GetComponent<HeroController>().Skill_C(ConstData.Knight);
                break;
            case ConstData.Berserker:
                berserker.GetComponent<HeroController>().Skill_C(ConstData.Berserker);
                break;
            case ConstData.Caster:
                caster.GetComponent<HeroController>().Skill_C(ConstData.Caster);
                break;
            case ConstData.Hunter:
                hunter.GetComponent<HeroController>().Skill_C(ConstData.Hunter);
                break;
        }
    }

}
