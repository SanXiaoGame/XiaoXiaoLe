using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 所有场景管理类
/// </summary>
public class SceneAss_Manager : ManagerBase<SceneAss_Manager>
{
    //新的场景名
    internal int newSceneID;

    //加载条
    internal Slider loadingSlider;
    /// <summary>
    /// 启用协程
    /// </summary>
    /// <param 场景名="name"></param>
    internal void ExecutionOfEvent()
    {
        if (SceneManager.GetActiveScene().name == ConstData.LoadingScene)
        {
            StartCoroutine("loadScene", newSceneID);
        }
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
                }
            }
        }
    }
}
