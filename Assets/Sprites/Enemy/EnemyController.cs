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
            Debug.Log("敌人被剑士所伤");
            hp -= 10;
        }
        else if (collision.tag == "BerserkerWeapon")
        {
            Debug.Log("敌人被狂战所伤");
            hp -= 10;
        }
        else if (collision.tag == "HunterWeapon")
        {
            Debug.Log("敌人被猎人所伤");
            hp -= 10;
        }
        else if (collision.tag == "CasterWeapon")
        {
            Debug.Log("敌人被法师所伤");
            hp -= 10;
        }
        else if (collision.tag == "KnightWeapon")
        {
            Debug.Log("敌人被剑士所伤");
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
