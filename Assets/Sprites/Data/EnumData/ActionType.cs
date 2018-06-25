using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionType 
{
    public Dictionary<int, string> ActionData = new Dictionary<int, string>();

    public  enum ActionEnum
    {
        Idle = 3251,
        Await = 3252,
        Move = 3253,
        Diz = 3254,
        Win = 3255,
        Dead = 3256,
        CommonAttack = 3257,
        SaberOneSkill = 3258,
        SaberTwoSkill = 3259,
        SaberThreeSkill = 3260,
        KnightOneSkill = 3261,
        KnightTwoSkill = 3262,
        KnightThreeSkill = 3263,
        CasterOneSkill = 3264,
        CasterTwoSkill = 3265,
        CasterThreeSkill = 3266,
        BerserkerOneSkill = 3267,
        BerserkerTwoSkill = 3268,
        BerserkerThreeSkill = 3269,
        HunterOneSkill = 3270,
        HunterTwoSkill = 3271,
        HunterThreeSkill = 3272,
    }

}
