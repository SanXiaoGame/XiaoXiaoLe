using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    //临时存储初始的UI
    private string UIName;
    /// <summary>
    /// 赋值
    /// </summary>
    private void Awake()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case ConstData.MainScene:
                UIName = SQLiteManager.Instance.playerDataSource[1300].player_Name == "初始旗手" ? ConstData.UIEstablishCharacter : ConstData.UIMainCity;
                UIManager.Instance.PushUIStack(UIName);
                return;
            case "TestScene":
                UIManager.Instance.PushUIStack(ConstData.UIBattle);
                return;
        }
        //启动数据库
        SQLiteManager.Instance.LoadDataFunc();

        GameObject GameStartBG = transform.Find("/Canvas/BG").gameObject;
        UISceneWidget BGWidget = UISceneWidget.Get(GameStartBG);
        if (BGWidget != null)
        {
            BGWidget.PointerClick += LoadLoadingScene;
        }
        
    }
    void LoadLoadingScene(PointerEventData data)
    {
        SceneAss_Manager.Instance.LoadingFunc(2);
    }
}
