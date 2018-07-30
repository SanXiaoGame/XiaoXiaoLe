using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarColorChange : MonoBehaviour
{
    internal GameObject target;
    Image myColor;
    Color cl;
    Slider sld;
    RectTransform rectrans;
    internal int HPmin;
    internal int HPmax;
    Image myClassLogo;

    private void Awake()
    {
        myColor = transform.GetComponent<Image>();
        cl = myColor.color;
        sld = transform.GetComponent<Slider>();
        rectrans = transform.GetComponent<RectTransform>();
        HPmin = 0;
        myClassLogo = transform.GetChild(1).GetComponent<Image>();
    }

    void Update ()
    {
        if (target != null)
        {
            //如果死亡则血条消失
            if (sld.value <= 0)
            {
                //ObjectPoolManager.Instance.RecycleMyGameObject(gameObject);
                //if (UIBattle.hpBarList.Contains(gameObject) == true)
                //{
                //    UIBattle.hpBarList.Remove(gameObject);
                //}
                //return;
                sld.value = 0;
            }
            //位置跟随
            //Vector3 targetPos = Camera.main.WorldToScreenPoint(target.transform.position);
            //if (target.tag == ConstData.Player)
            //{
            //    switch (target.GetComponent<HeroController>().myClass)
            //    {
            //        case ConstData.Saber:
            //            rectrans.anchoredPosition = new Vector2(targetPos.x, targetPos.y) - new Vector2(709f, 1048.8f);
            //            break;
            //        case ConstData.Knight:
            //            rectrans.anchoredPosition = new Vector2(targetPos.x, targetPos.y) - new Vector2(709f, 1078.8f);
            //            break;
            //        case ConstData.Berserker:
            //            rectrans.anchoredPosition = new Vector2(targetPos.x, targetPos.y) - new Vector2(709f, 1108.8f);
            //            break;
            //        case ConstData.Caster:
            //            rectrans.anchoredPosition = new Vector2(targetPos.x, targetPos.y) - new Vector2(709f, 1078.8f);
            //            break;
            //        case ConstData.Hunter:
            //            rectrans.anchoredPosition = new Vector2(targetPos.x, targetPos.y) - new Vector2(709f, 1048.8f);
            //            break;
            //    }
            //}
            //else
            //{
            //    rectrans.anchoredPosition = new Vector2(targetPos.x, targetPos.y) - new Vector2(709f, 1108.8f);
            //}
            //生命值读取
            switch (target.tag)
            {
                case ConstData.FlagMan:
                    sld.value = target.GetComponent<FlagManController>().currentHP;
                    break;
                case ConstData.Player:
                    sld.value = target.GetComponent<HeroStates>().currentHP;
                    break;
                case ConstData.Enemy:
                    sld.value = target.GetComponent<EnemyStates>().currentHP;
                    break;
            }
            //变色
            if (sld.value <= (int)(HPmax * 0.2f))
            {
                cl = new Color(1, 0, 0, 1);
                myColor.color = cl;
            }
            else if (sld.value <= (int)(HPmax * 0.5))
            {
                cl = new Color(1, 1, 0, 1);
                myColor.color = cl;
            }
            else
            {
                cl = new Color(1, 1, 1, 1);
                myColor.color = cl;
            }
        }
	}

    internal void GetTarget(GameObject targetInput)
    {
        target = targetInput;
        switch (target.tag)
        {
            case ConstData.FlagMan:
                HPmax = target.GetComponent<FlagManController>().maxHP;
                sld.minValue = HPmin;
                sld.maxValue = HPmax;
                myClassLogo.sprite = ResourcesManager.Instance.FindSprite(ConstData.FlagMan);
                break;
            case ConstData.Player:
                HPmax = target.GetComponent<HeroStates>().maxHP;
                sld.minValue = HPmin;
                sld.maxValue = HPmax;
                myClassLogo.sprite = ResourcesManager.Instance.FindSprite(target.GetComponent<HeroController>().myClass);
                break;
            case ConstData.Enemy:
                HPmax = target.GetComponent<EnemyStates>().maxHP;
                sld.minValue = HPmin;
                sld.maxValue = HPmax;
                break;
        }
    }
}
