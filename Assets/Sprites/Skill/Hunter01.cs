using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter01 : MonoBehaviour
{

    //碰到敌人就销毁特效
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            //先生成爆炸特效
            GameObject boomEffect = ResourcesManager.Instance.FindPrefab(EffectPrefabs.Effect_arrowBoom);
            Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y - 0.2f);
            boomEffect.transform.position = pos;
            GameObject boomEffect1 = Instantiate(boomEffect) as GameObject;

            Destroy(this.gameObject, 0.1f);
            Destroy(boomEffect1, 0.3f);
        }
    }
}
