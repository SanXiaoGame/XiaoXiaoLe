using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UItest : MonoBehaviour
{
    Text a;

    private void Awake()
    {
        a = transform.GetComponent<Text>();
    }

    private void Update()
    {
        switch (transform.name)
        {
            case "saberHP":
                if (transform.Find("/" + SQLiteManager.Instance.team[ConstData.Saber].playerData.PrefabsID) != null)
                {
                    a.text = (transform.Find("/" + SQLiteManager.Instance.team[ConstData.Saber].playerData.PrefabsID).
                                        GetComponent<HeroStates>().currentHP).ToString();
                }
                break;
            case "KnightHP":
                if (transform.Find("/1003") != null)
                {
                    a.text = (transform.Find("/1003").GetComponent<HeroStates>().currentHP).ToString();
                }
                break;
            case "slmHP":
                if (transform.Find("/1101") != null)
                {
                    a.text = (transform.Find("/1101").GetComponent<EnemyStates>().currentHP).ToString();
                }
                break;
            case "slmHP2":
                if (transform.Find("/1102") != null)
                {
                    a.text = (transform.Find("/1102").GetComponent<EnemyStates>().currentHP).ToString();
                }
                break;
        }
    }

}
