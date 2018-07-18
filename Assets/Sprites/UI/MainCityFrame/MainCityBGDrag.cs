using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCityBGDrag : MonoBehaviour
{
    //偏移量
    Vector3 offset;

    private void OnMouseDown()
    {
        Debug.Log("a");
        offset = transform.position - Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        //transform.position = new Vector3(Input.mousePosition.x + offset.x, transform.position.y, transform.position.z);
        Debug.Log("b");
        transform.position = Input.mousePosition;
    }
}
