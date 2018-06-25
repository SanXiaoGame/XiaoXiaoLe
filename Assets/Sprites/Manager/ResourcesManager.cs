using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //所有英雄特效的预制体
    Object[] skillEffectAll;
    protected override void Awake()
    {
        base.Awake();
        blockAll = Resources.LoadAll(ConstData.BlockPrefabs);
        skillBlockAll = Resources.LoadAll(ConstData.SkillBlockPrefabs);
        audioClipAll = Resources.LoadAll<AudioClip>(ConstData.Sound);
        skillEffectAll= Resources.LoadAll(ConstData.SkillPrefabs);          //加载所有技能特效预制体到指定数组
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
    public GameObject FindSkillEffect(SkillEffectType.SkillEffect  skillEffect)
    {
        for (int i = 0; i < skillEffectAll.Length; i++)
        {
            if (skillEffectAll[i].name == skillEffect.ToString())
            {
                return skillEffectAll[i]as GameObject;
            }
        }
        return null;
    }
}
