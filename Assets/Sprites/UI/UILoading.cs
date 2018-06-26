using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILoading : MonoBehaviour, IUIBase
{
    //加载条
    Slider loadingSlider;

    private void Awake()
    {
        loadingSlider = transform.GetChild(0).GetComponent<Slider>();
        SceneAss_Manager.Instance.readDataEnd += isLpad;
    }

    void isLpad(string sceneName)
    {
        StartCoroutine("loadScene", sceneName);
    }

    IEnumerator loadScene(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;
        while (async != null && !async.isDone)
        {
            yield return new WaitForSeconds(0.01f);
            // 更新滑动条
            if (loadingSlider.value <= .9f)
            {
                if (loadingSlider.value <= async.progress)
                {
                    loadingSlider.value += Time.deltaTime;
                }
            }
            else
            {
                if (loadingSlider.value < loadingSlider.maxValue)
                {
                    loadingSlider.value += Time.deltaTime;
                }
                else if (loadingSlider.value == loadingSlider.maxValue)
                {
                    async.allowSceneActivation = true;
                }
            }
        }
    }

    /// <summary>
    /// 进入界面
    /// </summary>
    public void OnEntering()
    {

    }
    /// <summary>
    /// //界面暂停
    /// </summary>
    public void OnExiting()
    {

    }
    /// <summary>
    /// 界面唤醒
    /// </summary>
    public void OnPausing()
    {

    }
    /// <summary>
    /// 界面退出
    /// </summary>
    public void OnResuming()
    {

    }
}
