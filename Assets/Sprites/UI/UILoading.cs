using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 加载场景
/// </summary>
public class UILoading : MonoBehaviour,IUIBase
{
    //加载条
    Slider loadingSlider;

    /// <summary>
    /// 进入界面
    /// </summary>
    public void OnEntering()
    {
        gameObject.SetActive(true);
        loadingSlider = transform.GetChild(0).GetComponent<Slider>();
        SceneAss_Manager.Instance.readDataEnd += isLpad;
    }
    /// <summary>
    /// 界面暂停
    /// </summary>
    public void OnPausing()
    {
        gameObject.SetActive(false);
    }
    /// <summary>
    /// 界面唤醒
    /// </summary>
    public void OnResuming()
    {
        gameObject.SetActive(true);
    }
    /// <summary>
    /// 界面退出
    /// </summary>
    public void OnExiting()
    {
        gameObject.SetActive(false);
    }

    //用于承接事件
    void isLpad(int sceneID)
    {
        StartCoroutine("loadScene", sceneID);
    }

    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <param 场景ID="sceneID"></param>
    /// <returns></returns>
    IEnumerator loadScene(int sceneID)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneID);
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
                    UIManager.Instance.PopUIStack();
                }
            }
        }
    }
}
