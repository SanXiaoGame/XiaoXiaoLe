using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testController : MonoBehaviour
{
    GameObject flagM;
    GameObject saber;
    GameObject knight;
    GameObject berserker;
    GameObject caster;
    GameObject hunter;

    GameObject setPoint;

    private void Awake()
    {
        //假team字典
        HeroData flagManData = new HeroData();
        HeroData saberData = new HeroData();
        HeroData knightData = new HeroData();
        HeroData berserkerData = new HeroData();
        HeroData casterData = new HeroData();
        HeroData hunterData = new HeroData();
        flagManData.playerData = SQLiteManager.Instance.playerDataSource[1300];
        flagManData.totalHP = flagManData.playerData.HP;
        flagManData.totalAD = flagManData.playerData.AD;
        flagManData.totalAP = flagManData.playerData.AP;
        flagManData.totalDEF = flagManData.playerData.DEF;
        flagManData.totalRES = flagManData.playerData.RES;
        saberData.playerData = SQLiteManager.Instance.playerDataSource[1306];
        saberData.totalHP = saberData.playerData.HP;
        saberData.totalAD = saberData.playerData.AD;
        saberData.totalAP = saberData.playerData.AP;
        saberData.totalDEF = saberData.playerData.DEF;
        saberData.totalRES = saberData.playerData.RES;
        knightData.playerData = SQLiteManager.Instance.playerDataSource[1302];
        knightData.totalHP = knightData.playerData.HP;
        knightData.totalAD = knightData.playerData.AD;
        knightData.totalAP = knightData.playerData.AP;
        knightData.totalDEF = knightData.playerData.DEF;
        knightData.totalRES = knightData.playerData.RES;
        berserkerData.playerData = SQLiteManager.Instance.playerDataSource[1303];
        berserkerData.totalHP = berserkerData.playerData.HP;
        berserkerData.totalAD = berserkerData.playerData.AD;
        berserkerData.totalAP = berserkerData.playerData.AP;
        berserkerData.totalDEF = berserkerData.playerData.DEF;
        berserkerData.totalRES = berserkerData.playerData.RES;
        casterData.playerData = SQLiteManager.Instance.playerDataSource[1304];
        casterData.totalHP = casterData.playerData.HP;
        casterData.totalAD = casterData.playerData.AD;
        casterData.totalAP = casterData.playerData.AP;
        casterData.totalDEF = casterData.playerData.DEF;
        casterData.totalRES = casterData.playerData.RES;
        hunterData.playerData = SQLiteManager.Instance.playerDataSource[1305];
        hunterData.totalHP = hunterData.playerData.HP;
        hunterData.totalAD = hunterData.playerData.AD;
        hunterData.totalAP = hunterData.playerData.AP;
        hunterData.totalDEF = hunterData.playerData.DEF;
        hunterData.totalRES = hunterData.playerData.RES;
        SQLiteManager.Instance.team.Add("FlagMan", flagManData);
        SQLiteManager.Instance.team.Add(ConstData.Saber, saberData);
        SQLiteManager.Instance.team.Add(ConstData.Knight, knightData);
        SQLiteManager.Instance.team.Add(ConstData.Berserker, berserkerData);
        SQLiteManager.Instance.team.Add(ConstData.Caster, casterData);
        SQLiteManager.Instance.team.Add(ConstData.Hunter, hunterData);

        flagM = ResourcesManager.Instance.FindPlayerPrefab("1001");
        saber = ResourcesManager.Instance.FindPlayerPrefab("1018");
        knight = ResourcesManager.Instance.FindPlayerPrefab("1003");
        berserker = ResourcesManager.Instance.FindPlayerPrefab("1004");
        caster = ResourcesManager.Instance.FindPlayerPrefab("1005");
        hunter = ResourcesManager.Instance.FindPlayerPrefab("1006");
        setPoint = transform.Find("/startPoint").gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.Find("/1018").GetComponent<HeroController>().Skill_A(ConstData.Saber);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.Find("/1018").GetComponent<HeroController>().Skill_B(ConstData.Saber);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.Find("/1018").GetComponent<HeroController>().Skill_C(ConstData.Saber);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Find("/1003").GetComponent<HeroController>().Skill_A(ConstData.Knight);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.Find("/1003").GetComponent<HeroController>().Skill_B(ConstData.Knight);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Find("/1003").GetComponent<HeroController>().Skill_C(ConstData.Knight);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.Find("/1004").GetComponent<HeroController>().Skill_A(ConstData.Berserker);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            transform.Find("/1004").GetComponent<HeroController>().Skill_B(ConstData.Berserker);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            transform.Find("/1004").GetComponent<HeroController>().Skill_C(ConstData.Berserker);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            transform.Find("/1005").GetComponent<HeroController>().Skill_A(ConstData.Caster);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            transform.Find("/1005").GetComponent<HeroController>().Skill_B(ConstData.Caster);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            transform.Find("/1005").GetComponent<HeroController>().Skill_C(ConstData.Caster);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            transform.Find("/1006").GetComponent<HeroController>().Skill_A(ConstData.Hunter);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            transform.Find("/1006").GetComponent<HeroController>().Skill_B(ConstData.Hunter);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            transform.Find("/1006").GetComponent<HeroController>().Skill_C(ConstData.Hunter);
        }


        if (Input.GetKeyDown(KeyCode.K))
        {
            transform.Find("/1001").GetComponent<FlagManController>().ClearAllTarget();
        }
        if (Input.GetKeyDown(KeyCode.L)) 
        {
            transform.Find("/1102").GetComponent<EnemyStates>().currentHP = 0;
        }

        //全局控制
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartGame();

        } //刷角色
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            FlagManController.flagMove = true;
        } //开始前进

        
        if (Input.GetKeyDown(KeyCode.V))    //史莱姆冲刺
        {
            transform.Find("/1101").GetComponent<EnemyControllers>().Enemy_Sprint();
        }
        if (Input.GetKeyDown(KeyCode.B)) //剑士沉默
        {
            transform.Find("/1018").GetComponent<HeroStates>().GetState(3204, 3f);
        }
        if (Input.GetKeyDown(KeyCode.N)) //史莱姆无敌
        {
            transform.Find("/1101").GetComponent<EnemyStates>().GetState(3211, 12f);
        }
        if (Input.GetKeyDown(KeyCode.M)) //史莱姆无敌
        {
            transform.Find("/1102").GetComponent<EnemyStates>().GetState(3211, 12f);
        }
    }

    public void StartGame()
    {
        GameObject p1 = ObjectPoolManager.Instance.InstantiateMyGameObject(flagM);
        GameObject p2 = ObjectPoolManager.Instance.InstantiateMyGameObject(saber);
        GameObject p3 = ObjectPoolManager.Instance.InstantiateMyGameObject(knight);
        GameObject p4 = ObjectPoolManager.Instance.InstantiateMyGameObject(berserker);
        GameObject p5 = ObjectPoolManager.Instance.InstantiateMyGameObject(caster);
        GameObject p6 = ObjectPoolManager.Instance.InstantiateMyGameObject(hunter);
        p1.name = flagM.name;
        p2.name = saber.name;
        p3.name = knight.name;
        p4.name = berserker.name;
        p5.name = caster.name;
        p6.name = hunter.name;
        p1.transform.position = setPoint.transform.position;
        p2.transform.position = setPoint.transform.position + new Vector3(1, 0, 0);
        p3.transform.position = setPoint.transform.position + new Vector3(1.8f, 0, 0);
        p4.transform.position = setPoint.transform.position + new Vector3(2.6f, 0, 0);
        p5.transform.position = setPoint.transform.position + new Vector3(3.4f, 0, 0);
        p6.transform.position = setPoint.transform.position + new Vector3(4.2f, 0, 0);
        p1.AddComponent<FlagManController>();
        p2.AddComponent<HeroController>();
        p2.AddComponent<HeroStates>();
        p3.AddComponent<HeroController>();
        p3.AddComponent<HeroStates>();
        p4.AddComponent<HeroController>();
        p4.AddComponent<HeroStates>();
        p5.AddComponent<HeroController>();
        p5.AddComponent<HeroStates>();
        p6.AddComponent<HeroController>();
        p6.AddComponent<HeroStates>();
        transform.Find("/GameCamera").transform.parent = p1.transform;
        p1.transform.Find("GameCamera").position -= new Vector3(0, 0, 10);
        transform.Find("/Main Camera").transform.parent = p1.transform;
        p1.transform.Find("Main Camera").position -= new Vector3(0, 0, 3);
        GameObject wp = Instantiate(Resources.Load("Prefabs/WeaponPrefabs/2005") as GameObject);
        wp.name = "2005";
        wp.transform.parent = p2.transform.Find("Bones/Torso/L-arm/L-fist/Weapon");
        wp.transform.localPosition = new Vector3(0, 0, 0);
        wp.transform.localRotation = (Resources.Load("Prefabs/WeaponPrefabs/2005") as GameObject).transform.rotation;
        GameObject wp2 = Instantiate(Resources.Load("Prefabs/WeaponPrefabs/2023") as GameObject);
        wp2.name = "2023";
        wp2.transform.parent = p3.transform.Find("Bones/Torso/L-arm/L-fist/Weapon");
        wp2.transform.localPosition = new Vector3(0, 0, 0);
        wp2.transform.localRotation = (Resources.Load("Prefabs/WeaponPrefabs/2023") as GameObject).transform.rotation;
        GameObject wp3 = Instantiate(Resources.Load("Prefabs/WeaponPrefabs/2029") as GameObject);
        wp3.name = "2029";
        wp3.transform.parent = p4.transform.Find("Bones/Torso/L-arm/L-fist/Weapon");
        wp3.transform.localPosition = new Vector3(0, 0, 0);
        wp3.transform.localRotation = (Resources.Load("Prefabs/WeaponPrefabs/2029") as GameObject).transform.rotation;
        GameObject wp4 = Instantiate(Resources.Load("Prefabs/WeaponPrefabs/2042") as GameObject);
        wp4.name = "2042";
        wp4.transform.parent = p5.transform.Find("Bones/Torso/L-arm/L-fist/Weapon");
        wp4.transform.localPosition = new Vector3(0, 0, 0);
        wp4.transform.localRotation = (Resources.Load("Prefabs/WeaponPrefabs/2042") as GameObject).transform.rotation;
        GameObject wp5 = Instantiate(Resources.Load("Prefabs/WeaponPrefabs/2054") as GameObject);
        wp5.name = "2054";
        wp5.transform.parent = p6.transform.Find("Bones/Torso/R-arm/R-fist/Weapon2");
        wp5.transform.localPosition = new Vector3(0, 0, 0);
        wp5.transform.localRotation = (Resources.Load("Prefabs/WeaponPrefabs/2054") as GameObject).transform.rotation;


        FlagManController.flagMove = true;
    }
}
