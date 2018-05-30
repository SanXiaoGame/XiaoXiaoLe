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
    protected override void Awake()
    {
        base.Awake();
        audioClipAll = Resources.LoadAll<AudioClip>(ConstData.SoundEffect);
    }
    /// <summary>
    /// 返回一个指定的音效
    /// </summary>
    /// <param 音效名="clipName"></param>
    /// <returns></returns>
    public AudioClip FindAudioClip(string clipName)
    {
        foreach (AudioClip audioClip in audioClipAll)
        {
            if (audioClip.name == clipName)
            {
                return audioClip;
            }
        }
        return null;
    }
}
