using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUI : MonoBehaviour
{

    public void ButtonA()
    {
        UIManager.Instance.PushUIStack("UICombatSettlementPrefab");
    }
    public void ButtonB()
    {
        UIManager.Instance.PushUIStack("UI_GuidenStart");
    }
    public void ButtonC()
    {
        UIManager.Instance.PushUIStack("UI_GuidenStory");
    }

}
