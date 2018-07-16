using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSettlementTeam : MonoBehaviour
{
    GameObject flagManPoint;
    GameObject saberPoint;
    GameObject knightPoint;
    GameObject casterPoint;
    GameObject berserkerPoint;
    GameObject hunterPoint;

    GameObject flagMan;
    GameObject saber;
    GameObject knight;
    GameObject caster;
    GameObject berserker;
    GameObject hunter;
    int heroID;
    private void Awake()
    {
        //查找生成点对象
        flagManPoint = transform.Find("FlagManPoint").gameObject;
        saberPoint = transform.Find("SaberPoint").gameObject;
        knightPoint = transform.Find("KnightPoint").gameObject;
        casterPoint = transform.Find("CasterPoint").gameObject;
        berserkerPoint = transform.Find("BerserkerPoint").gameObject;
        hunterPoint = transform.Find("HunterPoint").gameObject;

        //根据小队英雄身上的PrefabID,生成预制体到对应生成点位置
        //flagMan= Instantiate(ResourcesManager.Instance.FindPlayerPrefab("1001"), flagManPoint.transform);
        //heroID = SQLiteManager.Instance.team[ConstData.Saber].playerData.PrefabsID;
        //saber = Instantiate(ResourcesManager.Instance.FindPlayerPrefab(heroID.ToString()), saberPoint.transform); ;
        //heroID = SQLiteManager.Instance.team[ConstData.Knight].playerData.PrefabsID;
        //knight = Instantiate(ResourcesManager.Instance.FindPlayerPrefab(heroID.ToString()), knightPoint.transform);
        //heroID = SQLiteManager.Instance.team[ConstData.Caster].playerData.PrefabsID;
        //caster = Instantiate(ResourcesManager.Instance.FindPlayerPrefab(heroID.ToString()), casterPoint.transform);
        //heroID = SQLiteManager.Instance.team[ConstData.Berserker].playerData.PrefabsID;
        //berserker = Instantiate(ResourcesManager.Instance.FindPlayerPrefab(heroID.ToString()), berserkerPoint.transform);
        //heroID = SQLiteManager.Instance.team[ConstData.Hunter].playerData.PrefabsID;
        //hunter = Instantiate(ResourcesManager.Instance.FindPlayerPrefab(heroID.ToString()), hunterPoint.transform);
    }

    //确认结算后,销毁生成的英雄
    public void ConfirmButton()
    {
        Destroy(flagMan);
        Destroy(saber);
        Destroy(knight);
        Destroy(caster);
        Destroy(berserker);
        Destroy(hunter);
    }

}
