using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 块的功能类
/// </summary>
public class BlockObject : MonoBehaviour
{
    //块的初始类型选择
    internal BlockObjectType blockObjectType = BlockObjectType.NormalType;
    //技能块预制体
    GameObject skillBlock;
    //清屏块预制体
    GameObject highSkillBlock;
    //不能移动块预制体
    GameObject movementStopBlock;
    //邻近的块身上的“块基类”
    internal BlockObject[] adjacentItems;
    //特殊块的形成
    internal GameObject specialObjectToForm = null;
    //用于实例化时赋值ColumnScript类
    internal ColumnScript myColumnScript;
    //是否被消
    bool isDestroyed = false;
    //块所在的列编号
    internal int ColumnNumber;

    #region 相邻块的名字
    string left1 = "left1";
    string left2 = "left2";
    string left3 = "left3";
    string right1 = "right1";
    string right2 = "right2";
    string right3 = "right3";
    string up1 = "up1";
    string up2 = "up2";
    string up3 = "up3";
    string down1 = "down1";
    string down2 = "down2";
    string down3 = "down3";
    #endregion

    static BlockObject parentCallingScript;

    private void Awake()
    {
        if (GetComponent<BlockObjectTouch>() == null)
        {
            gameObject.AddComponent<BlockObjectTouch>();
        }
        if (GetComponent<UISceneWidget>() == null)
        {
            gameObject.AddComponent<UISceneWidget>();
        }
    }
    private void Start()
    {
        skillBlock = ResourcesManager.Instance.FindBlock(BlockObjectType.SkillType);
        highSkillBlock = ResourcesManager.Instance.FindBlock(BlockObjectType.HighSkillType);
        movementStopBlock = ResourcesManager.Instance.FindBlock(BlockObjectType.MovementStopType);
        //默认邻近的块为4个
        adjacentItems = new BlockObject[4];
    }

    /// <summary>
    /// 为上下左右的块赋值名字
    /// </summary>
    private void AssignLRUD()
    {
        //选中块左边有块
        if (adjacentItems[0])
        {
            left1 = adjacentItems[0].name;
            //选中块左边第2格有块
            if (adjacentItems[0].adjacentItems[0])
            {
                left2 = adjacentItems[0].adjacentItems[0].name;
                //选中块左边第3格有块
                if (adjacentItems[0].adjacentItems[0].adjacentItems[0])
                {
                    left3 = adjacentItems[0].adjacentItems[0].adjacentItems[0].name;
                } 
            }
        }
        //选中块右边有块
        if (adjacentItems[1])
        {
            right1 = adjacentItems[1].name;
            //选中块右边第2格有块
            if (adjacentItems[1].adjacentItems[1])
            {
                right2 = adjacentItems[1].adjacentItems[1].name;
                //选中块右边第3格有块
                if (adjacentItems[1].adjacentItems[1].adjacentItems[1])
                {
                    right3 = adjacentItems[1].adjacentItems[1].adjacentItems[1].name;
                } 
            }
        }
        //选中块上面有块
        if (adjacentItems[2])
        {
            up1 = adjacentItems[2].name;
            //选中块上面第2格有块
            if (adjacentItems[2].adjacentItems[2])
            {
                up2 = adjacentItems[2].adjacentItems[2].name;
                //选中块上面第3格有块
                if (adjacentItems[2].adjacentItems[2].adjacentItems[2])
                {
                    up3 = adjacentItems[2].adjacentItems[2].adjacentItems[2].name;
                } 
            }
        }
        //选中块下面有块
        if (adjacentItems[3])
        {
            down1 = adjacentItems[3].name;
            //选中块下面第2格有块
            if (adjacentItems[3].adjacentItems[3])
            {
                down2 = adjacentItems[3].adjacentItems[3].name;
                //选中块下面第3格有块
                if (adjacentItems[3].adjacentItems[3].adjacentItems[3])
                {
                    down3 = adjacentItems[3].adjacentItems[3].adjacentItems[3].name;
                }
            }
        }
    }

    /// <summary>
    /// 特殊块形成的方法
    /// </summary>
    /// <param 换位成功的块名="objName"></param>
    void CheckForSpecialBlockFormation(string objName)
    {
        //水平5连方块
        if (objName == left2 && objName == left1 && objName == right1 && objName == right2)
        {
            parentCallingScript.specialObjectToForm = highSkillBlock;
        }
        //垂直5连方块
        else if (objName == up2 && objName == up1 && objName == down1 && objName == down2)
        {
            parentCallingScript.specialObjectToForm = highSkillBlock;
        }
        //水平4连方块
        else if ((objName == left2 && objName == left1 && objName == right1) || (objName == left1 && objName == right1 && objName == right2))
        {
            parentCallingScript.specialObjectToForm = skillBlock;
        }
        //垂直4连方块
        else if ((objName == up2 && objName == up1 && objName == down1) || (objName == up1 && objName == down1 && objName == down2))
        {
            parentCallingScript.specialObjectToForm = skillBlock;
        }
    }

    /// <summary>
    /// 检测是否可以交换
    /// </summary>
    /// <param 被移动的块名="objName"></param>
    /// <param 反向编号0-3="parentIndex"></param>
    /// <returns></returns>
    internal bool JustCheckIfCanBrust(string objName, int parentIndex)
    {
        AssignLRUD();

        if (parentIndex == 0)
            right1 = "right1";
        if (parentIndex == 1)
            left1 = "left1";
        if (parentIndex == 2)
            down1 = "down1";
        if (parentIndex == 3)
            up1 = "up1";

        //是否能消块
        bool canBurst = false;

        if ((objName == left1 && objName == left2) || (objName == left1 && objName == right1) || (objName == right1 && objName == right2) || (objName == up1 && objName == up2) || (objName == up1 && objName == down1) || (objName == down1 && objName == down2))
        {
            canBurst = true;
            CheckForSpecialBlockFormation(objName);
        }
        return canBurst;
    }

    /// <summary>
    /// 摧毁块自己
    /// </summary>
    internal void DestroyBlock()
    {
        if (isDestroyed)
        {
            return;
        }
        //摧毁开关
        isDestroyed = true;
        //累加消除的块数
        GameManager.Instance.RemoveBlockNumber++;

        //执行消除动画

        //动画结束后摧毁块,延迟时间未定
        Invoke("AnimationeEndDestroyBlock", 1f);
    }
    void AnimationeEndDestroyBlock()
    {
        Destroy(gameObject);
    }
}
