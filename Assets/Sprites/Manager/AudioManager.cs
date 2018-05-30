using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏播放器管理类
/// </summary>
public class AudioManager : ManagerBase<AudioManager>
{
    //保存背景播放器
    AudioSource bgMusic;

    //保存所有音效播放器
    Dictionary<string, AudioSource> effectMusic = new Dictionary<string, AudioSource>();

    //初始化播放器管理类
    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 返回指定的播放器
    /// </summary>
    /// <param 播放器所在游戏物体名="audioNumber"></param>
    /// <returns></returns>
    public AudioSource GameAudio(string audioName)
    {
        foreach (string key in effectMusic.Keys)
        {
            if (key == audioName)
            {
                return effectMusic[name];
            }
        }
        return null;
    }

    void FindAlleffectMusicAudioSource()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            effectMusic.Add(transform.GetChild(i).name, transform.GetChild(i).GetComponent<AudioSource>());
        }
    }

    //背景播放器静音开关
    public bool BGMute
    {
        get
        {
            return bgMusic.mute;
        }
        set
        {
            bgMusic.mute = value;
        }
    }

    //音效播放器静音开关
    public void EffectMute(bool isMute)
    {
        FindAlleffectMusicAudioSource();
        foreach (string key in effectMusic.Keys)
        {
            effectMusic[key].mute = isMute;
        }
    }

    //控制音乐音量大小
    public float BgVolume
    {
        get
        {
            return bgMusic.volume;
        }
        set
        {
            bgMusic.volume = value;
        }
    }

    //控制音效音量大小
    public void EffectVolmue(float value)
    {
        FindAlleffectMusicAudioSource();
        foreach (string key in effectMusic.Keys)
        {
            effectMusic[key].volume = value;
        }
    }

    //播放器总开关
    public bool AudioMute(bool isMute)
    {
        bgMusic.mute = isMute;
        foreach (string key in effectMusic.Keys)
        {
            effectMusic[key].mute = isMute;
        }
        return bgMusic.mute;
    }

    //总音量控制
    public float AudioVolume(float value)
    {
        FindAlleffectMusicAudioSource();
        bgMusic.volume = value;
        foreach (string key in effectMusic.Keys)
        {
            effectMusic[key].volume = value;
        }
        return bgMusic.volume;
    }

    /// <summary>
    /// 播放指定的音效
    /// </summary>
    /// <param 音效名="effectName"></param>
    /// <param 指定生成播放器的位置="AudioPosition"></param>
    /// <param 播放器初始音量="volume"></param>
    /// <param 是否直接播放="defAudio"></param>
    public void PlayEffectBase(string effectName, Vector3 AudioPosition, float volume, bool defAudio = true)
    {
        //根据查找路径加载对应的音频剪辑  
        AudioClip clip = ResourcesManager.Instance.FindAudioClip(effectName);
        //如果为空的画，直接报错，然后跳出  
        if (clip == null)
        {
            Debug.Log("没有找到音效片段");
            return;
        }
        //如果defAudio=true，直接播放  
        if (defAudio)
        {
            //指定点播放  
            AudioSource.PlayClipAtPoint(clip, AudioPosition, volume);
        }
    }
}
