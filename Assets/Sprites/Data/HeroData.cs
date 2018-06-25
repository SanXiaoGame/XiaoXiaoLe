using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroData
{
    public GameObject enemy;
    public Rigidbody2D myRigidbody;
    public Animator animator;

    public PlayerData playerData;
    public StateData stateData;
    public SkillData skillData;

    public int starHP;
    public int currentHP;
    public int currentAD;
    public int currentAP;
    public int currentDEF;
    public int currentRES;
    public int currentStateID;

    public Transform transform;
    //public int heroID;
}
