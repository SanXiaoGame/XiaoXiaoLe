using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hunter01 : MonoBehaviour
{
    BoxCollider2D cld;
    
    //使用者
    GameObject user;
    //最终伤害
    int totalDamage;
    //找到旗手
    GameObject flagM;
    //一次开关
    bool isTrigger = false;

    private void Awake()
    {
        cld = transform.GetComponent<BoxCollider2D>();
        if (SceneManager.GetActiveScene().name != "LoadingScene")
        {
            user = transform.Find("/" + SQLiteManager.Instance.team[ConstData.Hunter].playerData.PrefabsID).gameObject;
        }
        flagM = transform.Find("/1001").gameObject;
    }

    private void OnEnable()
    {
        if (cld.enabled == false)
        {
            cld.enabled = true;
        }
        totalDamage = 0;
        isTrigger = false;
    }

    private void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * 3f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy" && isTrigger == false)
        {
            isTrigger = true;
            ObjectPoolManager.Instance.RecycleMyGameObject(gameObject);
            GameObject bom = ObjectPoolManager.Instance.InstantiateMyGameObject
                (ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_arrowBoom));
            bom.transform.position = transform.position;
            AudioManager.Instance.PlayEffectMusic(SoundEffect.ExplodeArrow_Explosion);
            vp_Timer.In(0.8f, new vp_Timer.Callback(delegate () { ObjectPoolManager.Instance.RecycleMyGameObject(bom); }));
        }
    }
}
