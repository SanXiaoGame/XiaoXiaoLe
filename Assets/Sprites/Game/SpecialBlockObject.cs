using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBlockObject : MonoBehaviour
{
    BlockObject myPlayingObject;
    private void Start()
    {
        myPlayingObject = GetComponent<BlockObject>();
        if (myPlayingObject.objectType == BlockObjectType.SkillType)
        {

        }
    }
}
