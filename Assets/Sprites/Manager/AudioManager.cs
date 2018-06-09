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
    Dictionary<SoundEffect, AudioSource> effectMusic = new Dictionary<SoundEffect, AudioSource>();

    //是否静音
    bool bgMusicMute = false;
    bool effectMusicMute = false;
    //临时存储静音值
    int intMute;
    //临时存储歌
    AudioClip clip;

    //音乐音量和音效音量 默认1f
    float bgMusicVolume = 1f;
    float effectVolume = 1f;

    //初始化播放器管理类
    protected override void Awake()
    {
        base.Awake();
        //添加背景播放器
        bgMusic = gameObject.AddComponent<AudioSource>();
        //获取是否静音
        bgMusicMute = PlayerPrefs.GetInt("BGMMute") == 1 ? false : true;
        effectMusicMute = PlayerPrefs.GetInt("effectMute") == 1 ? false : true;
        //获取设置音量
        bgMusicVolume = PlayerPrefs.GetFloat("bgMusicVolume");
        effectVolume = PlayerPrefs.GetFloat("effectVolume");
    }

    /// <summary>
    /// 换歌
    /// </summary>
    /// <param 歌名="bgmName"></param>
    public void ReplaceBGM(BGM bgmType)
    {
        switch (bgmType)
        {
            case BGM.MainCity:
                clip = ResourcesManager.Instance.FindAudioClip(bgmType);
                break;
        }
        bgMusic.spatialBlend = 0f;
        bgMusic.loop = true;
    }

    /// <summary>
    /// 返回指定的音效播放器(暂存)
    /// </summary>
    /// <param 播放器所在游戏物体名="audioNumber"></param>
    /// <returns></returns>
    public AudioSource GameAudio(SoundEffect audioType)
    {
        foreach (SoundEffect key in effectMusic.Keys)
        {
            if (key == audioType)
            {
                return effectMusic[audioType];
            }
        }
        return null;
    }

    //背景播放器静音开关
    public void BGMute(bool isMute)
    {
        bgMusicMute = isMute;
        intMute = isMute == false ? 1 : 0;
        PlayerPrefs.SetInt("BGMMute", intMute);
    }

    //音效播放器静音开关
    public void EffectMute(bool isMute)
    {
        effectMusicMute = isMute;
        intMute = isMute == false ? 1 : 0;
        PlayerPrefs.SetInt("effectMute", intMute);
    }

    //控制音乐音量大小
    public void BgVolume(float value)
    {
        bgMusicVolume = value;
        PlayerPrefs.SetFloat("bgMusicVolume", value);
    }

    //控制音效音量大小
    public void EffectVolmue(float value)
    {
        effectVolume = value;
        PlayerPrefs.SetFloat("effectVolume", value);
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="musicName"></param>
    /// <param name="volume"></param>
    /// <param name="is2D"></param>
    public void PlayEffectMusic(SoundEffect musicType, float is2D = 0f)
    {
        //根据查找路径加载对应的音频剪辑  
        clip = ResourcesManager.Instance.FindAudioClip(musicType);
        if (clip == null)
        {
            Debug.Log("没有找到音效片段");
            return;
        }
        //是否已经有这个音效播放器了
        if (effectMusic[musicType] != null)
        {
            //重启播放器物体
            effectMusic[musicType].gameObject.SetActive(true);
            //开始播放
            effectMusic[musicType].Play();
        }
        else
        {
            //没有就创建新的
            AudioSource effectAudio = new GameObject(musicType.ToString()).AddComponent<AudioSource>();
            //添加声音文件
            effectAudio.clip = clip;
            //成为AudioManager的子物体
            effectAudio.transform.parent = transform;
            //加入字典
            effectMusic.Add(musicType, effectAudio);
            //不使用循环播放
            effectAudio.loop = false;
            //设置2D/3D 默认2D
            effectAudio.spatialBlend = is2D;
            //设置音量 默认1
            effectAudio.volume = effectVolume;
            //启动状态延迟的协程
            StartCoroutine("EffectMusicState", effectAudio);
        }
    }
    /// <summary>
    /// 音效状态
    /// </summary>
    /// <param 对应播放器="effectAudio"></param>
    /// <returns></returns>
    IEnumerator EffectMusicState(AudioSource effectAudio)
    {
        //等待音效播放完
        yield return new WaitForSeconds(effectAudio.clip.length);
        //暂停播放
        effectAudio.Stop();
        //隐藏播放器物体
        effectAudio.gameObject.SetActive(false);
        StopCoroutine("EffectMusicState");
    }
}
