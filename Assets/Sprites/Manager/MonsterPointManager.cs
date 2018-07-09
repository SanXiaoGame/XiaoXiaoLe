using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPointManager : MonoBehaviour
{
    internal List<GameObject> enemyList;
    bool pointStandby;

    GameObject slm1;
    GameObject slm2;
    GameObject slm3;

    GameObject point;
    BoxCollider2D colid;

    private void Awake()
    {
        pointStandby = false;
        point = transform.GetChild(0).gameObject;
        colid = GetComponent<BoxCollider2D>();
        enemyList = new List<GameObject>();
    }

    private void Update()
    {
        if (gameObject.tag == "MonsterPoint")
        {
            if (pointStandby == true && enemyList.Count == 0)
            {
                FlagManController.battleSwitch = false;
                FlagManController.flagMove = true;
                Debug.Log("clear");
                Destroy(gameObject);
            }
            if (pointStandby == true && slm1.GetComponent<EnemyControllers>().isAlive == false)
            {
                enemyList.Remove(slm1);
            }
            if (pointStandby == true && slm2.GetComponent<EnemyControllers>().isAlive == false)
            {
                enemyList.Remove(slm2);
            }
            if (gameObject.name == "BattlePoint3")
            {
                if (pointStandby == true && slm3.GetComponent<EnemyControllers>().isAlive == false)
                {
                    enemyList.Remove(slm3);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "MonsterPoint" && collision.tag == "FlagMan")
        {
            if (gameObject.tag == "MonsterPoint" && gameObject.name == "BattlePoint")
            {
                colid.enabled = false;
                slm1 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindEnemyPrefab("1101"));
                slm2 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindEnemyPrefab("1102"));
                slm1.name = ResourcesManager.Instance.FindEnemyPrefab("1101").name;
                slm2.name = ResourcesManager.Instance.FindEnemyPrefab("1102").name;
                if (slm1.GetComponent<EnemyControllers>() == null && slm1.GetComponent<EnemyStates>() == null)
                {
                    slm1.AddComponent<EnemyControllers>();
                    slm1.AddComponent<EnemyStates>();
                }
                if (slm2.GetComponent<EnemyControllers>() == null && slm2.GetComponent<EnemyStates>() == null)
                {
                    slm2.AddComponent<EnemyControllers>();
                    slm2.AddComponent<EnemyStates>();
                }
                slm1.transform.position = point.transform.position;
                slm2.transform.position = point.transform.position + new Vector3(0.5f, 0, 0);
                slm1.GetComponent<EnemyStates>().currentHP += 200;
                slm2.GetComponent<EnemyStates>().currentHP += 200;
                enemyList.Add(slm1);
                enemyList.Add(slm2);
                pointStandby = true;
            }
            else if (gameObject.name == "BattlePoint2")
            {
                colid.enabled = false;
                slm1 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindEnemyPrefab("1101"));
                slm2 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindEnemyPrefab("1102"));
                slm1.name = ResourcesManager.Instance.FindEnemyPrefab("1101").name;
                slm2.name = ResourcesManager.Instance.FindEnemyPrefab("1102").name;
                if (slm1.GetComponent<EnemyControllers>() == null && slm1.GetComponent<EnemyStates>() == null)
                {
                    slm1.AddComponent<EnemyControllers>();
                    slm1.AddComponent<EnemyStates>();
                }
                if (slm2.GetComponent<EnemyControllers>() == null && slm2.GetComponent<EnemyStates>() == null)
                {
                    slm2.AddComponent<EnemyControllers>();
                    slm2.AddComponent<EnemyStates>();
                }
                slm1.transform.position = point.transform.position;
                slm2.transform.position = point.transform.position + new Vector3(0.5f, 0, 0);
                slm1.GetComponent<EnemyStates>().currentHP += 400;
                slm2.GetComponent<EnemyStates>().currentHP += 400;
                enemyList.Add(slm1);
                enemyList.Add(slm2);
                pointStandby = true;
            }
            else if (gameObject.name == "BattlePoint3")
            {
                colid.enabled = false;
                slm1 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindEnemyPrefab("1101"));
                slm2 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindEnemyPrefab("1102"));
                slm3 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindEnemyPrefab("1103"));
                slm1.name = ResourcesManager.Instance.FindEnemyPrefab("1101").name;
                slm2.name = ResourcesManager.Instance.FindEnemyPrefab("1102").name;
                slm3.name = ResourcesManager.Instance.FindEnemyPrefab("1103").name;
                if (slm1.GetComponent<EnemyControllers>() == null && slm1.GetComponent<EnemyStates>() == null)
                {
                    slm1.AddComponent<EnemyControllers>();
                    slm1.AddComponent<EnemyStates>();
                }
                if (slm2.GetComponent<EnemyControllers>() == null && slm2.GetComponent<EnemyStates>() == null)
                {
                    slm2.AddComponent<EnemyControllers>();
                    slm2.AddComponent<EnemyStates>();
                }
                if (slm3.GetComponent<EnemyControllers>() == null && slm3.GetComponent<EnemyStates>() == null)
                {
                    slm3.AddComponent<EnemyControllers>();
                    slm3.AddComponent<EnemyStates>();
                }
                slm1.transform.position = point.transform.position;
                slm2.transform.position = point.transform.position + new Vector3(0.5f, 0, 0);
                slm3.transform.position = point.transform.position - new Vector3(0.5f, 0, 0);
                slm1.GetComponent<EnemyStates>().currentHP += 600;
                slm2.GetComponent<EnemyStates>().currentHP += 700;
                slm3.GetComponent<EnemyStates>().currentHP += 900;
                enemyList.Add(slm1);
                enemyList.Add(slm2);
                enemyList.Add(slm3);
                pointStandby = true;
            }
        }

        
        
    }

}
