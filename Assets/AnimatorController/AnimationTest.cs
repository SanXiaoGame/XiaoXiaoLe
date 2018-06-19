using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetTrigger("AnimTest");
        }

        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("isRun", true);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("isRun", false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (transform.name == "OriginSaber")
            {
                anim.SetTrigger("Attack");
            }
            else if (transform.name == "OriginKnight")
            {
                anim.SetTrigger("AttackPoke");
            }
            
        }
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetTrigger("AttackPoke");
        }
        if (Input.GetMouseButtonDown(2))
        {
            anim.SetTrigger("AttackBow");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetTrigger("Dead");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetTrigger("Win");
        }
        if (Input.GetKey(KeyCode.Q))
        {
            anim.SetBool("isDiz", true);
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            anim.SetBool("isDiz", false);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetTrigger("Spellcaster");
        }
        if (Input.GetKey(KeyCode.T))
        {
            anim.SetBool("isWait",true);
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            anim.SetBool("isWait",false);
        }
        if (Input.GetKey(KeyCode.L))
        {
            anim.SetBool("isSprint", true);
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            anim.SetBool("isSprint", false);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetTrigger("ExplodingSword");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetTrigger("LeapAttack");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            anim.SetTrigger("BowSkill");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            anim.SetTrigger("SixSonicSlash");
        }
    }

}
