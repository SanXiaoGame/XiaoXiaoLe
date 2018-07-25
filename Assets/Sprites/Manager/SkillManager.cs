using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 技能管理
// 消块等释放技能，统一到这来调用方法
public class SkillManager : ManagerBase<SkillManager>
{
    //各英雄对象
    internal GameObject saber;
    internal GameObject knight;
    internal GameObject berserker;
    internal GameObject caster;
    internal GameObject hunter;

    protected override void Awake()
    {
        base.Awake();
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
