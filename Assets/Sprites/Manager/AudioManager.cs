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
    Queue<AudioSource> effectMusic = new Queue<AudioSource>();
    //临时存储队列的播放器
    AudioSource tempAudio;

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
        PlayerPrefs.SetFloat("effectVolum", 1);
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
        clip = ResourcesManager.Instance.FindAudioClip(bgmType);
        bgMusic.clip = null;
        bgMusic.clip = clip;
        bgMusic.spatialBlend = 0f;
        bgMusic.loop = true;
        bgMusic.Play();
    }

    /// <summary>
    /// 返回指定的音效播放器(暂存)
    /// </summary>
    /// <param 播放器所在游戏物体名="audioNumber"></param>
    /// <returns></returns>
    /*public AudioSource GameAudio(SoundEffect audioType)
    {
        foreach (SoundEffect key in effectMusic.Keys)
        {
            if (key == audioType)
            {
                return effectMusic[audioType];
            }
        }
        return null;
    }*/

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
    /// <param 音效名="musicType"></param>
    /// <param 是否是2D播发器="is2D"></param>
    public void PlayEffectMusic(SoundEffect musicType, float is2D = 0f)
    {
        //根据查找路径加载对应的音频剪辑
        clip = ResourcesManager.Instance.FindAudioClip(musicType);
        if (clip == null)
        {
            Debug.Log("没有找到音效片段");
            return;
        }
        //是否已经有音效播放器,并且没有播放音效
        if (effectMusic.Count > 0)
        {
            //备用播放器出队
            tempAudio = effectMusic.Dequeue();
        }
        if (tempAudio != null && !tempAudio.isPlaying)
        {
            //换音效
            tempAudio.clip = clip;
            //开始播放
            tempAudio.Play();
            //启动状态延迟
            EffectMusicEnqueue(tempAudio);
        }
        else
        {
            //没有就创建新的
            tempAudio = new GameObject(musicType.ToString()).AddComponent<AudioSource>();
            //添加声音文件
            tempAudio.clip = clip;
            //成为AudioManager的子物体
            tempAudio.transform.parent = transform;
            //不使用循环播放
            tempAudio.loop = false;
            //关闭开始就播放
            tempAudio.playOnAwake = false;
            //设置2D/3D 默认2D
            tempAudio.spatialBlend = is2D;
            //设置音量 默认1
            tempAudio.volume = 1;
            //播放
            tempAudio.Play();
            //启动状态延迟
            EffectMusicEnqueue(tempAudio);
        }
    }

    /// <summary>
    /// 播放器重新入列
    /// </summary>
    /// <param 对应播放器="tempAudio"></param>
    void EffectMusicEnqueue(AudioSource tempAudio)
    {
        vp_Timer.In(tempAudio.clip.length, new vp_Timer.Callback(delegate ()
        {
            //暂停播放
            tempAudio.Stop();
            tempAudio.clip = null;
            //加入队列
            effectMusic.Enqueue(tempAudio);
        }));
        Debug.Log("回收");
    }
}
