using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IceCubeBoom : MonoBehaviour
{
    //子物体数组
    GameObject[] cubes;
    //触发器
    BoxCollider2D cld;
    //使用者
    GameObject user;
    //最终伤害
    int totalDamage;
    //找到旗手
    GameObject flagM;
    //被击中的目标列表
    List<GameObject> enemyList;

    private void Awake()
    {
        cubes = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            cubes[i] = transform.GetChild(i).gameObject;
        }
        cld = transform.GetComponent<BoxCollider2D>();
        if (SceneManager.GetActiveScene().name != "LoadingScene")
        {
            user = transform.Find("/" + SQLiteManager.Instance.team[ConstData.Caster].playerData.PrefabsID).gameObject;
        }
        flagM = transform.Find("/1001").gameObject;
        enemyList = new List<GameObject>();
    }

    private void OnEnable()
    {
        switch (transform.name)
        {
            case "Skill_Caster01_IceCubeOne":
                for (int i = 0; i < cubes.Length; i++)
                {
                    cubes[i].GetComponent<PolygonCollider2D>().enabled = false;
                    cubes[i].GetComponent<Rigidbody2D>().gravityScale = 0;
                    cubes[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    cubes[i].transform.position =
                        ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Caster01_IceCubeOne).transform.GetChild(i).position;
                    cubes[i].transform.rotation =
                        ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Caster01_IceCubeOne).transform.GetChild(i).rotation;
                    cubes[i].SetActive(true);
                }
                break;
            case "Skill_Caster01_IceCubeTwo":
                for (int i = 0; i < cubes.Length; i++)
                {
                    cubes[i].GetComponent<PolygonCollider2D>().enabled = false;
                    cubes[i].GetComponent<Rigidbody2D>().gravityScale = 0;
                    cubes[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    cubes[i].transform.position =
                        ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Caster01_IceCubeTwo).transform.GetChild(i).position;
                    cubes[i].transform.rotation =
                        ResourcesManager.Instance.FindPrefab(SkillPrefabs.Skill_Caster01_IceCubeTwo).transform.GetChild(i).rotation;
                    cubes[i].SetActive(true);
                }
                break;
        }
        totalDamage = 0;
        enemyList.Clear();
        if (cld.enabled == false)
        {
            cld.enabled = true;
        }
        //1秒后破裂
        vp_Timer.In(1.0f, new vp_Timer.Callback(delegate ()
        {
            if (SceneManager.GetActiveScene().name != "LoadingScene")
            {
                IceBoom();
            }
        }));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (enemyList.Contains(collision.gameObject) == false)
            {
                enemyList.Add(collision.gameObject);
                //生成击打特效
                GameObject hit1 = ObjectPoolManager.Instance.InstantiateMyGameObject(ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_hit));
                hit1.transform.position = collision.transform.position;
                //回收击打特效
                vp_Timer.In(1f, new vp_Timer.Callback(delegate ()
                {
                    if (SceneManager.GetActiveScene().name != "LoadingScene")
                    {
                        ObjectPoolManager.Instance.RecycleMyGameObject(hit1);
                    }
                }));
                //计算伤害
                if (collision.GetComponent<EnemyStates>().god == false)
                {
                    totalDamage = (int)
                        (
                        (
                        user.GetComponent<HeroStates>().currentAP * 1f -
                        (user.GetComponent<HeroStates>().currentAP * 1f) *
                        (collision.GetComponent<EnemyStates>().currentRES * 0.01f)
                        ) * 0.5f
                        );
                    collision.GetComponent<EnemyStates>().currentHP -= totalDamage;
                    //敌人进入眩晕
                    collision.transform.GetComponent<EnemyStates>().GetState(3201, 1.0f);
                }
                //清空所有锁定目标
                vp_Timer.In(0.3f, new vp_Timer.Callback(delegate ()
                {
                    if (SceneManager.GetActiveScene().name != "LoadingScene")
                    {
                        flagM.GetComponent<FlagManController>().ClearAllTarget();
                    }
                }));
            }
        }
    }


    void IceBoom()
    {
        AudioManager.Instance.PlayEffectMusic(SoundEffect.Freeze_Broken);
        for (int i = 0; i < cubes.Length; i++)
        {
            if (cubes[i] != null)
            {
                cubes[i].GetComponent<PolygonCollider2D>().enabled = true;
                cubes[i].GetComponent<Rigidbody2D>().gravityScale = 1;
            }
        }
        vp_Timer.In(1.0f, new vp_Timer.Callback(delegate ()
        {
            if (SceneManager.GetActiveScene().name != "LoadingScene")
            {
                ObjectPoolManager.Instance.RecycleMyGameObject(gameObject);
            }
        }));
    }



}
