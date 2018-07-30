using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurePoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ConstData.Player)
        {
            collision.GetComponent<HeroStates>().currentHP += (int)(collision.GetComponent<HeroStates>().maxHP * 0.5f);
            if (collision.GetComponent<HeroStates>().currentHP > collision.GetComponent<HeroStates>().maxHP)
            {
                collision.GetComponent<HeroStates>().currentHP = collision.GetComponent<HeroStates>().maxHP;
                GameObject cure = ObjectPoolManager.Instance.InstantiateMyGameObject
                    (ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_CureStone));
                cure.transform.position = collision.transform.position + new Vector3(0, 0.6f, 0);
                cure.transform.parent = collision.transform;
                cure.transform.localScale = new Vector3(2, 2, 2);
                Destroy(cure, 1.0f);
            }
        }
        else if (collision.tag == ConstData.FlagMan)
        {
            collision.GetComponent<FlagManController>().currentHP += (int)(collision.GetComponent<FlagManController>().maxHP * 0.5f);
            if (collision.GetComponent<FlagManController>().currentHP > collision.GetComponent<FlagManController>().maxHP)
            {
                collision.GetComponent<FlagManController>().currentHP = collision.GetComponent<FlagManController>().maxHP;
                GameObject cure = ObjectPoolManager.Instance.InstantiateMyGameObject
                    (ResourcesManager.Instance.FindPrefab(SkillPrefabs.Effect_CureStone));
                cure.transform.position = collision.transform.position + new Vector3(0, 0.6f, 0);
                cure.transform.parent = collision.transform;
                cure.transform.localScale = new Vector3(2, 2, 2);
                Destroy(cure, 1.0f);
            }
        }
    }
}
