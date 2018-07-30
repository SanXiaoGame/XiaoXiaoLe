using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 创建人物界面
/// </summary>
public class UIEstablishCharacter : MonoBehaviour, IUIBase
{
    //人物名字输入
    InputField characterInputName;
    //人物生成点
    Transform characterSpot;
    //人物预制体
    GameObject characterPreform;
    //一级确认窗口的内容
    Text characterContentNumberOne;
    //确认窗口
    GameObject confirmFrame;
    //显示内容
    Text characterContent;
    //临时存储一级确认窗口的内容
    string tempConfirm;
    //人物动画
    Animator _characterAnimator;
    //赋值
    private void Start()
    {
        characterInputName = transform.Find(ConstData.NameInputField).GetComponent<InputField>();
        characterSpot = transform.Find(ConstData.EstablishCharacterSpot);
        characterPreform = ResourcesManager.Instance.FindPlayerPrefab("1001");
        characterContent = transform.Find(ConstData.ConfirmFrame_ContentText).GetComponent<Text>();
        characterContentNumberOne = transform.Find(ConstData.EstablishCharacterConfirmFrame).GetComponent<Text>();
        confirmFrame = transform.Find(ConstData.ConfirmFrame).gameObject;
        tempConfirm = characterContentNumberOne.text;
        //一级确定按钮
        GameObject confirmButtonNumberOne = transform.Find(ConstData.EstablishCancelButton).gameObject;
        UISceneWidget bindingConfirmButtonNumberOne = UISceneWidget.Get(confirmButtonNumberOne);
        if (bindingConfirmButtonNumberOne != null) { bindingConfirmButtonNumberOne.PointerClick += ConfirmButtonNumberOneFunc; }
        //二级确定按钮
        GameObject confirmButtonNumberTwo = transform.Find(ConstData.ConfirmFrame_ConfirmButton).gameObject;
        UISceneWidget bindingConfirmButtonNumberTwo = UISceneWidget.Get(confirmButtonNumberTwo);
        if (bindingConfirmButtonNumberTwo != null) { bindingConfirmButtonNumberTwo.PointerClick += ConfirmButtonNumberTwoFunc; }
        //取消按钮
        GameObject cancelButton = transform.Find(ConstData.ConfirmFrame_CancelButton).gameObject;
        UISceneWidget bindingCancelButton = UISceneWidget.Get(cancelButton);
        if (bindingCancelButton != null) { bindingCancelButton.PointerClick += CancelButtonFunc; }
        //生成人物
        Transform _character = ObjectPoolManager.Instance.InstantiateMyGameObject(characterPreform).transform;
        _character.name = characterPreform.name;
        _character.parent = characterSpot;
        _character.localPosition = Vector3.zero;
        _characterAnimator = _character.GetComponent<Animator>();
        _characterAnimator.SetBool("isWait", true);
    }

    /// <summary>
    /// 一级确认按钮方法
    /// </summary>
    void ConfirmButtonNumberOneFunc(PointerEventData data)
    {
        if (characterInputName.text == "")
        {
            characterContentNumberOne.text = "<color=#ff0000>输入内容不能为空！</color>";
            DelayContentShow();
            return;
        }
        characterContent.text = StringSplicingTool.StringSplicing(new string[] { "确定要使用<color=#ff0000>", characterInputName.text, "</color>这个名字吗？" });
        confirmFrame.SetActive(true);
    }

    /// <summary>
    /// 二级确认按钮方法
    /// </summary>
    void ConfirmButtonNumberTwoFunc(PointerEventData data)
    {
        confirmFrame.SetActive(false);
        _characterAnimator.SetBool("isWait", false);
        SQLiteManager.Instance.playerDataSource[1300].player_Name = characterInputName.text;
        SQLiteManager.Instance.UpdataDataFromTable(ConstData.Player, ConstData.player_Name, characterInputName.text, ConstData.player_ID, 1300);
        UIManager.Instance.PushUIStack(ConstData.UIMainCity);
    }

    /// <summary>
    /// 取消按钮方法
    /// </summary>
    void CancelButtonFunc(PointerEventData data)
    {
        confirmFrame.SetActive(false);
    }

    /// <summary>
    /// 延时显示内容
    /// </summary>
    void DelayContentShow()
    {
        vp_Timer.In(3f, new vp_Timer.Callback(delegate () 
        {
            characterContentNumberOne.text = tempConfirm;
        }));
    }

    //进入
    public void OnEntering()
    {
        gameObject.SetActive(true);
    }
    //离开
    public void OnExiting()
    {
        gameObject.SetActive(false);
    }
    //暂停
    public void OnPausing()
    {
        gameObject.SetActive(false);
    }
    //重启
    public void OnResuming()
    {
        gameObject.SetActive(true);
    }
}
