using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    /// <summary>
    /// 赋值
    /// </summary>
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == ConstData.MainScene)
        {
            UIManager.Instance.PushUIStack(ConstData.UIMainCityPrefab);
            return;
        }
        GameObject GameStartBG = transform.Find("/Canvas/BG").gameObject;
        UISceneWidget BGWidget = UISceneWidget.Get(GameStartBG);
        if (BGWidget != null)
        {
            BGWidget.PointerClick += LoadLoadingScene;
        }
        //启动数据库
        SQLiteManager.Instance.LoadDataFunc();
    }
    void LoadLoadingScene(PointerEventData data)
    {
        SceneAss_Manager.Instance.newSceneID = 2;
        SceneManager.LoadScene(1);
    }
}
