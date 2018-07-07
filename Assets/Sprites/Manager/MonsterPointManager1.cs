using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPointManager1 : MonoBehaviour
{
    List<GameObject> playerList;
    GameObject flagMan;

    private void Awake()
    {
        playerList = new List<GameObject>();
    }

    private void OnEnable()
    {
        playerList.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "WinFlag" && collision.tag == "Player")
        {
            if (flagMan == null)
            {
                flagMan = transform.Find("/1001").gameObject;
                playerList.Add(flagMan);
            }
            playerList.Add(collision.gameObject);
            FlagManController.battleSwitch = false;
            FlagManController.flagMove = false;
            vp_Timer.In(2.8f, new vp_Timer.Callback(delegate ()
            {
                for (int i = 0; i < playerList.Count; i++)
                {
                    if (playerList[i].tag != "FlagMan")
                    {
                        if (playerList[i].GetComponent<HeroController>().myClass != ConstData.Hunter)
                        {
                            GameObject wpTemp = ObjectPoolManager.Instance.InstantiateMyGameObject
                            (
                                ResourcesManager.Instance.FindWeaponPrefab
                                (
                                    (collision.transform.GetComponent<HeroStates>().mydata.playerData.Weapon).ToString()
                                    )
                            );
                            wpTemp.transform.parent = collision.transform.Find("Bones/Torso/R-arm/R-fist/Weapon2").transform;
                            wpTemp.transform.localPosition = Vector3.zero;
                            wpTemp.transform.localRotation = ResourcesManager.Instance.FindWeaponPrefab
                            ((collision.transform.GetComponent<HeroStates>().mydata.playerData.Weapon).ToString()).transform.rotation;
                            collision.transform.Find("Bones/Torso/L-arm/L-fist/Weapon").transform.GetChild(transform.childCount - 1).
                            gameObject.SetActive(false);
                        }
                    }
                    collision.transform.GetComponent<Animator>().SetTrigger("Win");
                }
            }));
            switch (collision.transform.GetComponent<HeroController>().myClass)
            {
                case ConstData.Saber:
                    vp_Timer.In(0.2f, new vp_Timer.Callback
                        (delegate () { collision.transform.GetComponent<HeroController>().moveSwitch = false; }));
                    break;
                case ConstData.Knight:
                    vp_Timer.In(0.8f, new vp_Timer.Callback
                        (delegate () { collision.transform.GetComponent<HeroController>().moveSwitch = false; }));
                    break;
                case ConstData.Berserker:
                    vp_Timer.In(1.4f, new vp_Timer.Callback
                        (delegate () { collision.transform.GetComponent<HeroController>().moveSwitch = false; }));
                    break;
                case ConstData.Caster:
                    vp_Timer.In(2.0f, new vp_Timer.Callback
                        (delegate () { collision.transform.GetComponent<HeroController>().moveSwitch = false; }));
                    break;
                case ConstData.Hunter:
                    vp_Timer.In(2.6f, new vp_Timer.Callback
                        (delegate () { collision.transform.GetComponent<HeroController>().moveSwitch = false; }));
                    break;
            }
        }
    }

}
