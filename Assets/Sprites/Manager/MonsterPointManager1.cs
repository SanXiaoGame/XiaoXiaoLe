using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPointManager1 : MonoBehaviour
{
    internal List<GameObject> enemyList2;
    bool pointStandby2;

    GameObject slm12;
    GameObject slm22;

    GameObject point2;
    BoxCollider2D colid2;

    private void Awake()
    {
        pointStandby2 = false;
        point2 = transform.Find("/monster (1)").gameObject;
        colid2 = GetComponent<BoxCollider2D>();
        enemyList2 = new List<GameObject>();
    }

    private void Update()
    {
        if (pointStandby2 == true && enemyList2.Count == 0)
        {
            FlagManController.battleSwitch = false;
            FlagManController.flagMove = true;
            Debug.Log("clear");
            Destroy(gameObject);
        }
        if (pointStandby2 == true && slm12.GetComponent<EnemyControllers>().isAlive == false)
        {
            enemyList2.Remove(slm12);
        }
        if (pointStandby2 == true && slm22.GetComponent<EnemyControllers>().isAlive == false)
        {
            enemyList2.Remove(slm22);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FlagMan")
        {
            colid2.enabled = false;
            slm12 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindEnemyPrefab("1101"));
            slm22 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindEnemyPrefab("1102"));
            slm12.name = ResourcesManager.Instance.FindEnemyPrefab("1101").name;
            slm22.name = ResourcesManager.Instance.FindEnemyPrefab("1102").name;
            if (slm12.GetComponent<EnemyControllers>() == null && slm12.GetComponent<EnemyStates>() == null)
            {
                slm12.AddComponent<EnemyControllers>();
                slm12.AddComponent<EnemyStates>();
            }
            if (slm22.GetComponent<EnemyControllers>() == null && slm22.GetComponent<EnemyStates>() == null)
            {
                slm22.AddComponent<EnemyControllers>();
                slm22.AddComponent<EnemyStates>();
            }
            slm12.transform.position = point2.transform.position;
            slm22.transform.position = point2.transform.position + new Vector3(0.5f, 0, 0);
            enemyList2.Add(slm12);
            enemyList2.Add(slm22);
            slm12.transform.parent = null;
            slm22.transform.parent = null;
            pointStandby2 = true;
        }
    }

}
