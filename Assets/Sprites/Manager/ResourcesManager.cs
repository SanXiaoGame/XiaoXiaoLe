using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 资源管理类
/// </summary>
public class ResourcesManager : ManagerBase<ResourcesManager>
{
    //所有音效
    AudioClip[] audioClipAll;
    //所有特殊块的预制体
    Object[] skillBlockAll;
    //所有初始块的预制体
    Object[] blockAll;
    protected override void Awake()
    {
        base.Awake();
        blockAll = Resources.LoadAll(ConstData.BlockPrefabs);
        skillBlockAll = Resources.LoadAll(ConstData.SkillBlockPrefabs);
        audioClipAll = Resources.LoadAll<AudioClip>(ConstData.SoundEffect);
    }
    /// <summary>
    /// 返回一个指定的音效
    /// </summary>
    /// <param 音效名="clipName"></param>
    /// <returns></returns>
    public AudioClip FindAudioClip(string clipName)
    {
        for (int i = 0; i < audioClipAll.Length; i++)
        {
            if (audioClipAll[i].name == clipName)
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
        for (int i = 0; i < skillBlockAll.Length; i++)
        {
            if (skillBlockAll[i].name == blockType.ToString())
            {
                return skillBlockAll[i] as GameObject;
            }
        }
        return null;
    }
}
