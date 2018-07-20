using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_GuidenStory : MonoBehaviour,IUIBase
{
    //确定按钮
    GameObject next;
    //logo按钮
    GameObject icon ;
    //取消按钮
    GameObject cancel;
    //添加事件基类
    UISceneWidget iconClick;
    UISceneWidget nextButtonClick;
    UISceneWidget cancelGuidenClick;
    Text tips;
    void ChangeTips()
    {

    }
    private void Start()
    {
        next = transform.Find("NPC/Story").gameObject;
        icon = transform.Find("NPC/Tips").gameObject;
        cancel = transform.Find("Cancel").gameObject;
        tips = next.GetComponent<Text>();
        nextButtonClick = UISceneWidget.Get(next);
        iconClick = UISceneWidget.Get(icon);
        cancelGuidenClick = UISceneWidget.Get(cancel);
        //绑定事件
        if (nextButtonClick != null && nextButtonClick != null)
        {
            nextButtonClick.PointerClick += NextButtonFunc;
            iconClick.PointerClick += NextButtonFunc;
            cancelGuidenClick.PointerClick += CancelGuidenFunc;
        }
    }
    public void OnEntering()
    {
        gameObject.SetActive(true);

    }

    public void OnExiting()
    {
        gameObject.SetActive(false);

    }

    public void OnPausing()
    {
        gameObject.SetActive(false);

    }

    public void OnResuming()
    {
        gameObject.SetActive(true);

    }
    int tipsID = 0;
    public void NextButtonFunc(PointerEventData eventData)
    {
        tipsID++;
        Debug.Log("下一波引导内容");
        switch (tipsID)
        {
            case 1:
                tips.text = "欢迎来到纳雷克斯战绩!";
                break;
            case 2:
                tips.text = "向玩家交代故事背景后开始!";
                break;
            case 3:
                tips.text = "介绍旗手和旗手的特点及重要性!";
                break;
            case 4:
                tips.text = "接下来告诉玩家在关卡中如果当前没有敌人时，旗手会自己向前移动，一旦遇到怪物则会停下!";
                break;
             case 5:
                tipsID = 0;
                UIManager.Instance.PushUIStack("UI_GuidenStoryA");
                break;
            default:
                break;
        }

    }
    public  void CancelGuidenFunc(PointerEventData eventData)
    {
        Debug.Log("取消引导");
        UIManager.Instance.PopUIStack();        //退出当前界面

    }
}
