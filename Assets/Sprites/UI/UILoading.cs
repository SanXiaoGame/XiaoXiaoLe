using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 加载场景
/// </summary>
public class UILoading : MonoBehaviour
{
    private void Start()
    {
        if (SceneAss_Manager.Instance.newSceneID == 2)
        {
            transform.GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite("BG3");
        }
        else if (SceneAss_Manager.Instance.newSceneID == 3)
        {
            transform.GetComponent<Image>().sprite = ResourcesManager.Instance.FindSprite("BG4");
        }
        else
        {
            transform.GetComponent<Image>().sprite = null;
        }
        SceneAss_Manager.Instance.loadingSlider = transform.GetChild(0).GetComponent<Slider>();
        SceneAss_Manager.Instance.ExecutionOfEvent();
    }
}
