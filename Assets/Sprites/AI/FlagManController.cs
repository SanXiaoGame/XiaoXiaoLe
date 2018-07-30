using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManController : MonoBehaviour
{
    //全局战斗开关
    internal static bool battleSwitch = false;
    //旗手移动开关
    internal static bool flagMove = false;
    //旗手是否在移动中（二级）
    bool isMove = false;
    //旗手是否在存活中（二级）
    bool alive = true;
    //是否存在墙
    bool isWall = false;
    //旗手存活开关
    internal bool isAlive = true;

    //旗手数据
    internal HeroData mydata;
    //旗手HP
    internal int maxHP;
    internal int currentHP;
    //旗手DEF
    internal int currentDEF;
    //旗手RES
    internal int currentRES;

    //旗手触发器
    CircleCollider2D triggerFlag;
    //旗手状态机
    Animator anim;
    //墙物体
    GameObject wall;
    //墙位置
    Transform wallPoint;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        triggerFlag = GetComponent<CircleCollider2D>();
        mydata = SQLiteManager.Instance.team["FlagMan"];
        maxHP = mydata.totalHP;
        currentHP = mydata.totalHP;
        currentDEF = mydata.totalDEF;
        currentRES = mydata.totalRES;
    }

    private void Update()
    {
        if (isAlive == true)
        {
            //判断血量是否没了，如果是，存活开关关闭并且跳出，此后除非复活否则再也进不来
            if (currentHP <= 0 && alive == true)
            {
                isAlive = false;
                alive = false;
                anim.SetTrigger("Dead");
                return;
            }
            //如果复活了，进入该判断会恢复到存活状态
            if (alive == false)
            {
                alive = true;
            }
            if (flagMove == true)
            {
                if (isMove == false)
                {
                    anim.SetBool("isRun", true);
                    isMove = true;
                }
                transform.position += Vector3.right * Time.deltaTime * 1f;
            }
            else
            {
                if (isMove == true)
                {
                    anim.SetBool("isRun", false);
                    isMove = false;
                }
            }
        }
        //一级判断：战斗状态开关是否打开
        if (battleSwitch == true)
        {
            if (isWall == false)
            {
                wallPoint = transform.Find("GameCamera/WallPoint").transform;
                wall = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPlayerPrefab("Wall"));
                wall.transform.position = wallPoint.position;
                isWall = true;
            }
        }
        else if(battleSwitch == false)
        {
            if(isWall == true)
            {
                ObjectPoolManager.Instance.RecycleMyGameObject(wall);
                isWall = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ConstData.MonsterPoint)
        {
            battleSwitch = true;
            flagMove = false;
        }
    }

    internal void ClearAllTarget()
    {
        transform.Find("/" + SQLiteManager.Instance.team[ConstData.Saber].playerData.PrefabsID)
            .GetComponent<HeroController>().targetEnemy = null;
        transform.Find("/" + SQLiteManager.Instance.team[ConstData.Saber].playerData.PrefabsID)
            .GetComponent<HeroController>().moveSwitch_Battle = true;
        transform.Find("/" + SQLiteManager.Instance.team[ConstData.Knight].playerData.PrefabsID)
            .GetComponent<HeroController>().targetEnemy = null;
        transform.Find("/" + SQLiteManager.Instance.team[ConstData.Knight].playerData.PrefabsID)
            .GetComponent<HeroController>().moveSwitch_Battle = true;
        transform.Find("/" + SQLiteManager.Instance.team[ConstData.Berserker].playerData.PrefabsID)
            .GetComponent<HeroController>().targetEnemy = null;
        transform.Find("/" + SQLiteManager.Instance.team[ConstData.Berserker].playerData.PrefabsID)
            .GetComponent<HeroController>().moveSwitch_Battle = true;
        transform.Find("/" + SQLiteManager.Instance.team[ConstData.Caster].playerData.PrefabsID)
            .GetComponent<HeroController>().targetEnemy = null;
        transform.Find("/" + SQLiteManager.Instance.team[ConstData.Caster].playerData.PrefabsID)
            .GetComponent<HeroController>().moveSwitch_Battle = true;
        transform.Find("/" + SQLiteManager.Instance.team[ConstData.Hunter].playerData.PrefabsID)
            .GetComponent<HeroController>().targetEnemy = null;
        transform.Find("/" + SQLiteManager.Instance.team[ConstData.Hunter].playerData.PrefabsID)
            .GetComponent<HeroController>().moveSwitch_Battle = true;

        GameObject[] enemyAll = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemyAll.Length; i++)
        {
            enemyAll[i].GetComponent<EnemyControllers>().targetPlayer = null;
            enemyAll[i].GetComponent<EnemyControllers>().moveSwitch_Battle = true;
        }
    }

    internal void RecWeapon()
    {

    }

    internal void RecWall()
    {
        if (wall != null)
        {
            Destroy(wall);
        }
    }
}
