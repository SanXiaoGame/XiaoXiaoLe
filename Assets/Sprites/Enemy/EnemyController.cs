using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    int hp = 100;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="SaberWeapon")
        {
            //Debug.Log("敌人被剑士所伤");
            hp -= 10;
           
        }
        else if (collision.tag == "BerserkerWeapon")
        {
            Debug.Log("敌人被狂战所伤");
            hp -= 10;
        }
        if (hp <= 0)
        {
            hp = 0;
            Destroy(this.gameObject,1f);
            PlayerController.isAttackEnemy = false;
        }
    }
}
