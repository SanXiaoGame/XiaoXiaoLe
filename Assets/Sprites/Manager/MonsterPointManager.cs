using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPointManager : MonoBehaviour
{
    internal List<GameObject> enemyList;
    bool pointStandby;

    GameObject slm1;
    GameObject slm2;

    GameObject point;
    BoxCollider2D colid;

    private void Awake()
    {
        pointStandby = false;
        point = transform.Find("/monster").gameObject;
        colid = GetComponent<BoxCollider2D>();
        enemyList = new List<GameObject>();
    }

    private void Update()
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FlagMan")
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
            slm1.GetComponent<EnemyStates>().currentHP += 800;
            slm2.GetComponent<EnemyStates>().currentHP += 800;
            enemyList.Add(slm1);
            enemyList.Add(slm2);
            pointStandby = true;
        }
    }

}
