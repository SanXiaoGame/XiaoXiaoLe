using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCityBGDrag : MonoBehaviour
{
    //偏移量
    Vector3 offset;

    private void OnMouseDown()
    {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousepos2D = new Vector2(mousepos.x, mousepos.y);
        RaycastHit2D hitpoint;
        if (hitpoint = Physics2D.Raycast(mousepos2D, Vector2.zero))
        {
            offset = transform.position - new Vector3(hitpoint.point.x,hitpoint.point.y,0);
        }
    }

    private void OnMouseDrag()
    {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousepos2D = new Vector2(mousepos.x, mousepos.y);
        RaycastHit2D hitpoint;
        if (hitpoint = Physics2D.Raycast(mousepos2D, Vector2.zero))
        {
            transform.position = new Vector3(hitpoint.point.x + offset.x, transform.position.y, transform.position.z);
            if (Camera.main.WorldToScreenPoint(transform.position).x > 0)
            {
                transform.position = ResourcesManager.Instance.FindUIPrefab(ConstData.UIMainCityPrefab_GameArea).transform.position;
            }
            if (Camera.main.WorldToScreenPoint(transform.position).x < -3222)
            {

                transform.position = ResourcesManager.Instance.FindUIPrefab(ConstData.UIMainCityPrefab_GameArea).transform.position -
                    new Vector3(12.582f, 0, 0);
            }
        }
    }
}
