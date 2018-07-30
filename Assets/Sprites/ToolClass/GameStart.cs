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
        //播放音乐
        AudioManager.Instance.ReplaceBGM(BGM.Login_Intro);
        vp_Timer.In(1.7f, new vp_Timer.Callback(delegate () { AudioManager.Instance.ReplaceBGM(BGM.Login_Loop); }));
    }
    void LoadLoadingScene(PointerEventData data)
    {
        SceneAss_Manager.Instance.LoadingFunc(2);
    }
}
