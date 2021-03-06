﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinflagController : MonoBehaviour
{
    List<GameObject> playerList;
    GameObject flagMan;
    //开关
    bool isTrigger = false;
    //音乐开关
    bool musicSwitch = true;

    private void Awake()
    {
        playerList = new List<GameObject>();
        musicSwitch = true;
    }

    private void OnEnable()
    {
        playerList.Clear();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ConstData.Player)
        {
            if (musicSwitch == true)
            {
                //播放音乐
                AudioManager.Instance.ReplaceBGM(BGM.victory);
                musicSwitch = false;
            }
            if (flagMan == null)
            {
                flagMan = GameObject.FindGameObjectWithTag(ConstData.FlagMan);
                playerList.Add(flagMan);
            }
            playerList.Add(collision.gameObject);
            FlagManController.battleSwitch = false;
            FlagManController.flagMove = false;
            if (isTrigger == false)
            {
                ClearWeapon();
                WinAnim();
                isTrigger = true;
            }
        }
    }

    void ClearWeapon()
    {
        vp_Timer.In(3.8f, new vp_Timer.Callback(delegate ()
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                if (playerList[i].tag != "FlagMan")
                {
                    if (playerList[i].GetComponent<HeroController>().myClass != ConstData.Hunter)
                    {
                        GameObject wpTemp = ObjectPoolManager.Instance.InstantiateMyGameObject
                        (
                            ResourcesManager.Instance.FindWeaponPrefab
                            (
                                (playerList[i].transform.GetComponent<HeroStates>().mydata.playerData.Weapon).ToString()
                                )
                        );
                        wpTemp.transform.parent = playerList[i].transform.Find(ConstData.MinorFist).transform;
                        wpTemp.transform.localPosition = Vector3.zero;
                        wpTemp.transform.localRotation = ResourcesManager.Instance.FindWeaponPrefab
                        ((playerList[i].transform.GetComponent<HeroStates>().mydata.playerData.Weapon).ToString()).transform.rotation;
                        if (playerList[i].transform.Find(ConstData.MainFist).transform.childCount > 0)
                        {
                            playerList[i].transform.Find(ConstData.MainFist).transform.
                            GetChild(0).gameObject.SetActive(false);
                        }
                    }
                }
            }
        }));
    }
    void WinAnim()
    {
        vp_Timer.In(3.8f, new vp_Timer.Callback(delegate () 
        {
            transform.Find("/Canvas/UIBattle").GetComponent<UIBattle>().PushWin();
            for (int i = 0; i < playerList.Count; i++)
            {
                playerList[i].transform.GetComponent<Animator>().SetTrigger("Win");
            }
            vp_Timer.In(1.5f, new vp_Timer.Callback(delegate () { UIManager.Instance.PushUIStack(ConstData.UISettlement); }));
        }));
    }
}
