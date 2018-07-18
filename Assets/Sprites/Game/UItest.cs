using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UItest : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.PushUIStack(ConstData.UIMainCityPrefab);
    }
}
