using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPointManagerStage01 : MonoBehaviour
{
    //怪物列表
    internal static List<GameObject> enemyList;
    //刷怪点是否已经准备妥当
    bool pointStandby;
    //该关卡中所有的怪物预制体
    GameObject slm1;
    GameObject slm2;
    GameObject slm3;
    //刷怪位置
    GameObject point;
    //触发器
    BoxCollider2D colid;

    private void Awake()
    {
        //准备妥当置否
        pointStandby = false;
        //刷怪位置获取
        point = transform.GetChild(0).gameObject;
        //触发器获取
        colid = GetComponent<BoxCollider2D>();
        //列表初始化
        enemyList = new List<GameObject>();
        //加载怪物预制体
        slm1 = ResourcesManager.Instance.FindPlayerPrefab("1101");
        slm2 = ResourcesManager.Instance.FindPlayerPrefab("1102");
        slm3 = ResourcesManager.Instance.FindPlayerPrefab("1103");
    }

    private void Update()
    {
        if (gameObject.tag == ConstData.MonsterPoint)
        {
            if (pointStandby == true && enemyList.Count == 0)
            {
                FlagManController.battleSwitch = false;
                FlagManController.flagMove = true;
                pointStandby = false;
            }
        }
        if (gameObject.tag == ConstData.BossPoint)
        {
            if (pointStandby == true && enemyList.Count == 0)
            {
                FlagManController.battleSwitch = false;
                FlagManController.flagMove = true;
                pointStandby = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == ConstData.MonsterPoint && collision.tag == "FlagMan")
        {
            switch (gameObject.name)
            {
                case "MonsterPoint1":
                    #region 第一刷怪点 
                    colid.enabled = false;
                    GameObject slm1_01 = ObjectPoolManager.Instance.InstantiateMyGameObject(slm1);
                    GameObject slm1_02 = ObjectPoolManager.Instance.InstantiateMyGameObject(slm1);
                    GameObject slm1_03 = ObjectPoolManager.Instance.InstantiateMyGameObject(slm1);
                    slm1_01.name = slm1.name;
                    slm1_02.name = slm1.name;
                    slm1_03.name = slm1.name;
                    if (slm1_01.GetComponent<EnemyControllers>() == null && slm1_01.GetComponent<EnemyStates>() == null)
                    {
                        slm1_01.AddComponent<EnemyControllers>();
                        slm1_01.AddComponent<EnemyStates>();
                    }
                    if (slm1_02.GetComponent<EnemyControllers>() == null && slm1_02.GetComponent<EnemyStates>() == null)
                    {
                        slm1_02.AddComponent<EnemyControllers>();
                        slm1_02.AddComponent<EnemyStates>();
                    }
                    if (slm1_03.GetComponent<EnemyControllers>() == null && slm1_03.GetComponent<EnemyStates>() == null)
                    {
                        slm1_03.AddComponent<EnemyControllers>();
                        slm1_03.AddComponent<EnemyStates>();
                    }
                    slm1_01.transform.position = point.transform.position;
                    slm1_02.transform.position = point.transform.position + new Vector3(0.5f, 0, 0);
                    slm1_03.transform.position = point.transform.position - new Vector3(0.5f, 0, 0);
                    slm1_01.GetComponent<EnemyStates>().StatesClearEnemy();
                    slm1_02.GetComponent<EnemyStates>().StatesClearEnemy();
                    slm1_03.GetComponent<EnemyStates>().StatesClearEnemy();
                    enemyList.Add(slm1_01);
                    enemyList.Add(slm1_02);
                    enemyList.Add(slm1_03);
                    Debug.Log(slm1_01.GetComponent<EnemyStates>().currentHP);
                    Debug.Log(slm1_02.GetComponent<EnemyStates>().currentHP);
                    Debug.Log(slm1_03.GetComponent<EnemyStates>().currentHP);
                    pointStandby = true;
                    #endregion
                    break;
                case "MonsterPoint2":
                    #region 第二刷怪点
                    colid.enabled = false;
                    GameObject slm1_04 = ObjectPoolManager.Instance.InstantiateMyGameObject(slm1);
                    GameObject slm1_05 = ObjectPoolManager.Instance.InstantiateMyGameObject(slm1);
                    GameObject slm1_06 = ObjectPoolManager.Instance.InstantiateMyGameObject(slm1);
                    GameObject slm2_01 = ObjectPoolManager.Instance.InstantiateMyGameObject(slm2);
                    slm1_04.name = slm1.name;
                    slm1_05.name = slm1.name;
                    slm1_06.name = slm1.name;
                    slm2_01.name = slm2.name;
                    if (slm1_04.GetComponent<EnemyControllers>() == null && slm1_04.GetComponent<EnemyStates>() == null)
                    {
                        slm1_04.AddComponent<EnemyControllers>();
                        slm1_04.AddComponent<EnemyStates>();
                    }
                    if (slm1_05.GetComponent<EnemyControllers>() == null && slm1_05.GetComponent<EnemyStates>() == null)
                    {
                        slm1_05.AddComponent<EnemyControllers>();
                        slm1_05.AddComponent<EnemyStates>();
                    }
                    if (slm1_06.GetComponent<EnemyControllers>() == null && slm1_06.GetComponent<EnemyStates>() == null)
                    {
                        slm1_06.AddComponent<EnemyControllers>();
                        slm1_06.AddComponent<EnemyStates>();
                    }
                    if (slm2_01.GetComponent<EnemyControllers>() == null && slm2_01.GetComponent<EnemyStates>() == null)
                    {
                        slm2_01.AddComponent<EnemyControllers>();
                        slm2_01.AddComponent<EnemyStates>();
                    }
                    slm1_04.transform.position = point.transform.position;
                    slm1_05.transform.position = point.transform.position + new Vector3(0.2f, 0, 0);
                    slm1_06.transform.position = point.transform.position - new Vector3(0.4f, 0, 0);
                    slm2_01.transform.position = point.transform.position - new Vector3(0.4f, 0, 0);
                    slm1_04.GetComponent<EnemyStates>().StatesClearEnemy();
                    slm1_05.GetComponent<EnemyStates>().StatesClearEnemy();
                    slm1_06.GetComponent<EnemyStates>().StatesClearEnemy();
                    slm2_01.GetComponent<EnemyStates>().StatesClearEnemy();
                    enemyList.Add(slm1_04);
                    enemyList.Add(slm1_05);
                    enemyList.Add(slm1_06);
                    enemyList.Add(slm2_01);
                    pointStandby = true;
                    #endregion
                    break;
                case "MonsterPoint3":
                    #region 第三刷怪点
                    colid.enabled = false;
                    GameObject slm1_07 = ObjectPoolManager.Instance.InstantiateMyGameObject(slm1);
                    GameObject slm1_08 = ObjectPoolManager.Instance.InstantiateMyGameObject(slm1);
                    GameObject slm1_09 = ObjectPoolManager.Instance.InstantiateMyGameObject(slm1);
                    GameObject slm1_10 = ObjectPoolManager.Instance.InstantiateMyGameObject(slm1);
                    GameObject slm1_11 = ObjectPoolManager.Instance.InstantiateMyGameObject(slm1);
                    GameObject slm2_02 = ObjectPoolManager.Instance.InstantiateMyGameObject(slm2);
                    GameObject slm2_03 = ObjectPoolManager.Instance.InstantiateMyGameObject(slm2);
                    slm1_07.name = slm1.name;
                    slm1_08.name = slm1.name;
                    slm1_09.name = slm1.name;
                    slm1_10.name = slm1.name;
                    slm1_11.name = slm1.name;
                    slm2_02.name = slm2.name;
                    slm2_03.name = slm2.name;
                    if (slm1_07.GetComponent<EnemyControllers>() == null && slm1_07.GetComponent<EnemyStates>() == null)
                    {
                        slm1_07.AddComponent<EnemyControllers>();
                        slm1_07.AddComponent<EnemyStates>();
                    }
                    if (slm1_08.GetComponent<EnemyControllers>() == null && slm1_08.GetComponent<EnemyStates>() == null)
                    {
                        slm1_08.AddComponent<EnemyControllers>();
                        slm1_08.AddComponent<EnemyStates>();
                    }
                    if (slm1_09.GetComponent<EnemyControllers>() == null && slm1_09.GetComponent<EnemyStates>() == null)
                    {
                        slm1_09.AddComponent<EnemyControllers>();
                        slm1_09.AddComponent<EnemyStates>();
                    }
                    if (slm1_10.GetComponent<EnemyControllers>() == null && slm1_10.GetComponent<EnemyStates>() == null)
                    {
                        slm1_10.AddComponent<EnemyControllers>();
                        slm1_10.AddComponent<EnemyStates>();
                    }
                    if (slm1_11.GetComponent<EnemyControllers>() == null && slm1_11.GetComponent<EnemyStates>() == null)
                    {
                        slm1_11.AddComponent<EnemyControllers>();
                        slm1_11.AddComponent<EnemyStates>();
                    }
                    if (slm2_02.GetComponent<EnemyControllers>() == null && slm2_02.GetComponent<EnemyStates>() == null)
                    {
                        slm2_02.AddComponent<EnemyControllers>();
                        slm2_02.AddComponent<EnemyStates>();
                    }
                    if (slm2_03.GetComponent<EnemyControllers>() == null && slm2_03.GetComponent<EnemyStates>() == null)
                    {
                        slm2_03.AddComponent<EnemyControllers>();
                        slm2_03.AddComponent<EnemyStates>();
                    }
                    slm1_07.transform.position = point.transform.position;
                    slm1_08.transform.position = point.transform.position + new Vector3(0.2f, 0, 0);
                    slm1_09.transform.position = point.transform.position - new Vector3(0.4f, 0, 0);
                    slm1_10.transform.position = point.transform.position - new Vector3(0.8f, 0, 0);
                    slm1_11.transform.position = point.transform.position - new Vector3(1.0f, 0, 0);
                    slm2_02.transform.position = point.transform.position - new Vector3(1.0f, 0, 0);
                    slm2_03.transform.position = point.transform.position - new Vector3(1.0f, 0, 0);
                    slm1_07.GetComponent<EnemyStates>().StatesClearEnemy();
                    slm1_08.GetComponent<EnemyStates>().StatesClearEnemy();
                    slm1_09.GetComponent<EnemyStates>().StatesClearEnemy();
                    slm1_10.GetComponent<EnemyStates>().StatesClearEnemy();
                    slm1_11.GetComponent<EnemyStates>().StatesClearEnemy();
                    slm2_02.GetComponent<EnemyStates>().StatesClearEnemy();
                    slm2_03.GetComponent<EnemyStates>().StatesClearEnemy();
                    enemyList.Add(slm1_07);
                    enemyList.Add(slm1_08);
                    enemyList.Add(slm1_09);
                    enemyList.Add(slm1_10);
                    enemyList.Add(slm1_11);
                    enemyList.Add(slm2_02);
                    enemyList.Add(slm2_03);
                    pointStandby = true;
                    #endregion
                    break;
                case "BossPoint":
                    #region 第四刷怪点
                    //切换音乐
                    AudioManager.Instance.ReplaceBGM(BGM.boss_intro);
                    PlayMusicAndPauseCancel();
                    colid.enabled = false;
                    GameObject slm1_12 = ObjectPoolManager.Instance.InstantiateMyGameObject(slm1);
                    GameObject slm1_13 = ObjectPoolManager.Instance.InstantiateMyGameObject(slm1);
                    GameObject slm1_14 = ObjectPoolManager.Instance.InstantiateMyGameObject(slm1);
                    GameObject slm1_15 = ObjectPoolManager.Instance.InstantiateMyGameObject(slm1);
                    GameObject slm1_16 = ObjectPoolManager.Instance.InstantiateMyGameObject(slm1);
                    GameObject slm2_04 = ObjectPoolManager.Instance.InstantiateMyGameObject(slm2);
                    GameObject slm2_05 = ObjectPoolManager.Instance.InstantiateMyGameObject(slm2);
                    GameObject slm3_01 = ObjectPoolManager.Instance.InstantiateMyGameObject(slm3);
                    slm1_12.name = slm1.name;
                    slm1_13.name = slm1.name;
                    slm1_14.name = slm1.name;
                    slm1_15.name = slm1.name;
                    slm1_16.name = slm1.name;
                    slm2_04.name = slm2.name;
                    slm2_05.name = slm2.name;
                    slm3_01.name = slm3.name;
                    if (slm1_12.GetComponent<EnemyControllers>() == null && slm1_12.GetComponent<EnemyStates>() == null)
                    {
                        slm1_12.AddComponent<EnemyControllers>();
                        slm1_12.AddComponent<EnemyStates>();
                    }
                    if (slm1_13.GetComponent<EnemyControllers>() == null && slm1_13.GetComponent<EnemyStates>() == null)
                    {
                        slm1_13.AddComponent<EnemyControllers>();
                        slm1_13.AddComponent<EnemyStates>();
                    }
                    if (slm1_14.GetComponent<EnemyControllers>() == null && slm1_14.GetComponent<EnemyStates>() == null)
                    {
                        slm1_14.AddComponent<EnemyControllers>();
                        slm1_14.AddComponent<EnemyStates>();
                    }
                    if (slm1_15.GetComponent<EnemyControllers>() == null && slm1_15.GetComponent<EnemyStates>() == null)
                    {
                        slm1_15.AddComponent<EnemyControllers>();
                        slm1_15.AddComponent<EnemyStates>();
                    }
                    if (slm1_16.GetComponent<EnemyControllers>() == null && slm1_16.GetComponent<EnemyStates>() == null)
                    {
                        slm1_16.AddComponent<EnemyControllers>();
                        slm1_16.AddComponent<EnemyStates>();
                    }
                    if (slm2_04.GetComponent<EnemyControllers>() == null && slm2_04.GetComponent<EnemyStates>() == null)
                    {
                        slm2_04.AddComponent<EnemyControllers>();
                        slm2_04.AddComponent<EnemyStates>();
                    }
                    if (slm2_05.GetComponent<EnemyControllers>() == null && slm2_05.GetComponent<EnemyStates>() == null)
                    {
                        slm2_05.AddComponent<EnemyControllers>();
                        slm2_05.AddComponent<EnemyStates>();
                    }
                    if (slm3_01.GetComponent<EnemyControllers>() == null && slm3_01.GetComponent<EnemyStates>() == null)
                    {
                        slm3_01.AddComponent<EnemyControllers>();
                        slm3_01.AddComponent<EnemyStates>();
                    }
                    slm1_12.transform.position = point.transform.position;
                    slm1_13.transform.position = point.transform.position + new Vector3(0.2f, 0, 0);
                    slm1_14.transform.position = point.transform.position - new Vector3(0.4f, 0, 0);
                    slm1_15.transform.position = point.transform.position - new Vector3(0.8f, 0, 0);
                    slm1_16.transform.position = point.transform.position - new Vector3(1.0f, 0, 0);
                    slm2_04.transform.position = point.transform.position - new Vector3(1.0f, 0, 0);
                    slm2_05.transform.position = point.transform.position - new Vector3(1.0f, 0, 0);
                    slm3_01.transform.position = point.transform.position + new Vector3(0.5f, 0, 0);
                    slm1_12.GetComponent<EnemyStates>().StatesClearEnemy();
                    slm1_13.GetComponent<EnemyStates>().StatesClearEnemy();
                    slm1_14.GetComponent<EnemyStates>().StatesClearEnemy();
                    slm1_15.GetComponent<EnemyStates>().StatesClearEnemy();
                    slm1_16.GetComponent<EnemyStates>().StatesClearEnemy();
                    slm2_04.GetComponent<EnemyStates>().StatesClearEnemy();
                    slm2_05.GetComponent<EnemyStates>().StatesClearEnemy();
                    slm3_01.GetComponent<EnemyStates>().StatesClearEnemy();
                    enemyList.Add(slm1_12);
                    enemyList.Add(slm1_13);
                    enemyList.Add(slm1_14);
                    enemyList.Add(slm1_15);
                    enemyList.Add(slm1_16);
                    enemyList.Add(slm2_04);
                    enemyList.Add(slm2_05);
                    enemyList.Add(slm3_01);
                    pointStandby = true;
                    #endregion
                    break;
            }
        }
    }

    private void PlayMusicAndPauseCancel()
    {
        vp_Timer.In(6.0f, new vp_Timer.Callback(delegate () {
            AudioManager.Instance.ReplaceBGM(BGM.boss_loop);
        }));
    }
}
