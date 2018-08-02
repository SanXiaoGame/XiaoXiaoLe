using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 设置界面
/// </summary>
public class UISetting : MonoBehaviour, IUIBase
{
    //音乐按钮
    Toggle musicToggle;
    //音效按钮
    Toggle soundEffectToggle;
    //音乐滑条
    Slider musicSlider;
    //音效滑条
    Slider soundEffectSlider;

    //赋值
    private void Awake()
    {
        //音乐按钮
        musicToggle = transform.Find(ConstData.MusicButton).GetComponent<Toggle>();
        UISceneWidget bindingMusicToggle = UISceneWidget.Get(musicToggle.gameObject);
        if (bindingMusicToggle != null) { bindingMusicToggle.PointerClick += MusicToggleFunc; }
        //音效按钮
        soundEffectToggle = transform.Find(ConstData.SoundEffectButton).GetComponent<Toggle>();
        UISceneWidget bindinSoundEffectToggle = UISceneWidget.Get(soundEffectToggle.gameObject);
        if (bindinSoundEffectToggle != null) { bindinSoundEffectToggle.PointerClick += SoundEffectToggleFunc; }
        //返回按钮
        GameObject returnGame = transform.Find(ConstData.SettingReturnGame).gameObject;
        UISceneWidget bindinReturnGame = UISceneWidget.Get(returnGame);
        if (bindinReturnGame != null) { bindinReturnGame.PointerClick += ReturnGameFunc; }
        //退出按钮
        GameObject quitGame = transform.Find(ConstData.SettingQuitGame).gameObject;
        UISceneWidget bindinQuitGame = UISceneWidget.Get(quitGame);
        if (bindinQuitGame != null) { bindinQuitGame.PointerClick += QuitGameFunc; }
        //音乐滑条
        musicSlider = transform.Find(ConstData.MusicSlider).GetComponent<Slider>();
        UISceneWidget bindinMusicSlider = UISceneWidget.Get(musicSlider.gameObject);
        if (bindinMusicSlider != null) { bindinMusicSlider.PointerUp += MusicSliderFunc; }
        //音效滑条
        soundEffectSlider = transform.Find(ConstData.SoundEffectSlider).GetComponent<Slider>();
        UISceneWidget bindinSoundEffectSlider = UISceneWidget.Get(soundEffectSlider.gameObject);
        if (bindinSoundEffectSlider != null) { bindinSoundEffectSlider.PointerUp += SoundEffectSliderFunc; }
    }

    /// <summary>
    /// 音乐滑条
    /// </summary>
    /// <param name="data"></param>
    void MusicSliderFunc(PointerEventData data)
    {
        AudioManager.Instance.BgVolume(musicSlider.value);
    }

    /// <summary>
    /// 音效滑条
    /// </summary>
    /// <param name="data"></param>
    void SoundEffectSliderFunc(PointerEventData data)
    {
        AudioManager.Instance.EffectVolmue(soundEffectSlider.value);
    }

    /// <summary>
    /// 音乐按钮方法
    /// </summary>
    /// <param name="data"></param>
    void MusicToggleFunc(PointerEventData data)
    {
        AudioManager.Instance.BGMute(musicToggle.isOn);
    }

    /// <summary>
    /// 音效按钮方法
    /// </summary>
    /// <param name="data"></param>
    void SoundEffectToggleFunc(PointerEventData data)
    {
        AudioManager.Instance.EffectMute(soundEffectToggle.isOn);
    }

    /// <summary>
    /// 返回按钮方法
    /// </summary>
    /// <param name="data"></param>
    void ReturnGameFunc(PointerEventData data)
    {
        UIManager.Instance.PopUIStack();
    }

    /// <summary>
    /// 退出按钮方法
    /// </summary>
    /// <param name="data"></param>
    void QuitGameFunc(PointerEventData data)
    {
        print("退出游戏");
        Application.Quit();
    }

    //进入
    public void OnEntering()
    {
        //赋值存档的位置
        musicToggle.enabled = false;
        soundEffectToggle.enabled = false;
        musicToggle.isOn = AudioManager.Instance.bgMusicMute;
        soundEffectToggle.isOn = AudioManager.Instance.effectMusicMute;
        musicToggle.enabled = true;
        soundEffectToggle.enabled = true;

        musicSlider.value = AudioManager.Instance.bgMusicVolume;
        soundEffectSlider.value = AudioManager.Instance.effectVolume;

        gameObject.SetActive(true);

        UIManager.Instance.GamePause();
    }
    //离开
    public void OnExiting()
    {
        gameObject.SetActive(false);
        UIManager.Instance.GamePause();
    }
    //暂停
    public void OnPausing()
    {
        gameObject.SetActive(false);
        UIManager.Instance.GamePause();
    }
    //唤醒
    public void OnResuming()
    {
        gameObject.SetActive(true);
        UIManager.Instance.GamePause();
    }
}
