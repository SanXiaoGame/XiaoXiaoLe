using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 资源管理类
/// </summary>
public class ResourcesManager : ManagerBase<ResourcesManager>
{
    //所有声音
    AudioClip[] audioClipAll;
    //所有特殊块的预制体
    Object[] skillBlockAll;
    //所有初始块的预制体
    Object[] blockAll;
    //所有UI预制体
    Object[] UIPrefabAll;
    //所有英雄特效的预制体
    Object[] skillPrefabsAll;
    //所有英雄特效的预制体
    Object[] effectPrefabsAll;
    //所有英雄的预制体
    Object[] HeroAll;
    //所有敌人的预制体
    Object[] EnemyAll;
    //所有武器的预制体
    Object[] WeaponAll;

    //所有职业Logo、技能图标、背包物品图片
    Sprite[] SpriteAll;

    protected override void Awake()
    {
        base.Awake();
        blockAll = Resources.LoadAll(ConstData.BlockPrefabs);
        skillBlockAll = Resources.LoadAll(ConstData.SkillBlockPrefabs);
        audioClipAll = Resources.LoadAll<AudioClip>(ConstData.Sound);
        skillPrefabsAll = Resources.LoadAll(ConstData.SkillPrefabs);          //加载所有技能特效预制体到指定数组
        effectPrefabsAll = Resources.LoadAll(ConstData.EffectPrefabs);          //加载所有特效预制体到指定数组
        UIPrefabAll = Resources.LoadAll(ConstData.UIPrefabsPath);
        HeroAll = Resources.LoadAll(ConstData.PlayerPrefabs);
        EnemyAll = Resources.LoadAll(ConstData.EnemyPrefabs);
        WeaponAll = Resources.LoadAll(ConstData.WeaponPrefabs);
        SpriteAll = Resources.LoadAll<Sprite>(ConstData.textureTemp);
    }

    /// <summary>
    /// 返回一个指定的歌
    /// </summary>
    /// <param 歌名="clipName"></param>
    /// <returns></returns>
    public AudioClip FindAudioClip<T>(T clipType) where T : struct
    {
        for (int i = 0; i < audioClipAll.Length; i++)
        {
            if (audioClipAll[i].name == clipType.ToString())
            {
                return audioClipAll[i];
            }
        }
        return null;
    }

    /// <summary>
    /// 返回所有指定的块预制体
    /// </summary>
    /// <param 块名="clipName"></param>
    /// <returns></returns>
    public Object[] FindBlockAll(BlockObjectType blockType)
    {
        if (blockType == BlockObjectType.NormalType)
        {
            return blockAll;
        }
        return null;
    }

    /// <summary>
    /// 返回一个指定的块预制体
    /// </summary>
    /// <param 块名="clipName"></param>
    /// <returns></returns>
    public GameObject FindBlock(BlockObjectType blockType)
    {
        return (blockType == BlockObjectType.SkillType ? skillBlockAll[0] : skillBlockAll[1]) as GameObject;
    }

    /// <summary>
    /// 根据名字查找技能特效预制体
    /// </summary>
    /// <param name="skillEffect"></param>
    /// <returns></returns>
    public GameObject FindPrefab(SkillPrefabs skillEffect)
    {
        for (int i = 0; i < skillPrefabsAll.Length; i++)
        {
            if (skillPrefabsAll[i].name == skillEffect.ToString())
            {
                return skillPrefabsAll[i]as GameObject;
            }
        }
        return null;
    }

    /// <summary>
    /// 根据名字查找攻击特效预制体
    /// </summary>
    /// <param name="skillEffect"></param>
    /// <returns></returns>
    public GameObject FindPrefab(EffectPrefabs effect)
    {
        for (int i = 0; i < effectPrefabsAll.Length; i++)
        {
            if (effectPrefabsAll[i].name == effect.ToString())
            {
                return effectPrefabsAll[i] as GameObject;
            }
        }
        return null;
    }
    /// <summary>
    /// 根据名字查找指定的UI预制体
    /// </summary>
    /// <param UI预制体名="name"></param>
    /// <returns></returns>
    public GameObject FindUIPrefab(string name)
    {
        for (int i = 0; i < UIPrefabAll.Length; i++)
        {
            if (UIPrefabAll[i].name == name)
            {
                return UIPrefabAll[i] as GameObject;
            }
        }
        return null;
    }
    /// <summary>
    /// 根据名字查找指定的角色预制体
    /// </summary>
    /// <param 角色预制体名="name"></param>
    /// <returns></returns>
    public GameObject FindPlayerPrefab(string name)
    {
        for (int i = 0; i < HeroAll.Length; i++)
        {
            if (HeroAll[i].name == name)
            {
                return HeroAll[i] as GameObject;
            }
        }
        return null;
    }
    /// <summary>
    /// 根据名字查找指定的敌人预制体
    /// </summary>
    /// <param 敌人预制体名="name"></param>
    /// <returns></returns>
    public GameObject FindEnemyPrefab(string name)
    {
        for (int i = 0; i < EnemyAll.Length; i++)
        {
            if (EnemyAll[i].name == name)
            {
                return EnemyAll[i] as GameObject;
            }
        }
        return null;
    }
    /// <summary>
    /// 根据名字查找指定的武器预制体
    /// </summary>
    /// <param 武器预制体名="name"></param>
    /// <returns></returns>
    public GameObject FindWeaponPrefab(string name)
    {
        for (int i = 0; i < WeaponAll.Length; i++)
        {
            if (WeaponAll[i].name == name)
            {
                return WeaponAll[i] as GameObject;
            }
        }
        return null;
    }
    /// <summary>
    /// 根据名字查找指定的图片
    /// </summary>
    /// <param 图片名="name"></param>
    /// <returns></returns>
    public Sprite FindSprite(string name)
    {
        for (int i = 0; i < SpriteAll.Length; i++)
        {
            if (SpriteAll[i].name == name)
            {
                return SpriteAll[i];
            }
        }
        return null;
    }
}
