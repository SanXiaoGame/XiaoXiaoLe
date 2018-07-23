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
        SceneAss_Manager.Instance.loadingSlider = transform.GetChild(0).GetComponent<Slider>();
        SceneAss_Manager.Instance.ExecutionOfEvent();
    }
}
