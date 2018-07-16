using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShoot : MonoBehaviour
{
    
	void Update ()
    {
        transform.position += Vector3.right * Time.deltaTime * 4.8f;
        transform.position += Vector3.up * Time.deltaTime * 6f;
	}

}
