using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 块的功能类
/// </summary>
public class BlockObject : MonoBehaviour
{
    //这个块当前的类型
    internal BlockObjectType objectType;
    //技能块预制体
    GameObject skillBlock;
    //清屏块预制体
    GameObject highSkillBlock;
    //特殊块的形成
    internal GameObject specialObjectToForm = null;
    //邻近的块身上的“块基类”
    public BlockObject[] adjacentItems;
    //用于实例化时赋值ColumnScript类
    internal ColumnScript myColumnScript;
    //可否摧毁
    internal bool brust = false;
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

    private void Start()
    {
        skillBlock = ResourcesManager.Instance.FindBlock(BlockObjectType.SkillType);
        highSkillBlock = ResourcesManager.Instance.FindBlock(BlockObjectType.HighSkillType);
        //默认邻近的块为8个
        adjacentItems = new BlockObject[8];
    }

    /// <summary>
    /// 为上下左右的块赋值名字
    /// </summary>
    private void AssignLRUD()
    {
        if (tag == ConstData.SpecialBlock)
        {
            return;
        }
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
            objectType = BlockObjectType.HighSkillType;
        }
        //垂直5连方块
        else if (objName == up2 && objName == up1 && objName == down1 && objName == down2)
        {
            parentCallingScript.specialObjectToForm = highSkillBlock;
            objectType = BlockObjectType.HighSkillType;
        }
        //水平4连方块
        else if ((objName == left2 && objName == left1 && objName == right1) || (objName == left1 && objName == right1 && objName == right2))
        {
            parentCallingScript.specialObjectToForm = skillBlock;
            objectType = BlockObjectType.SkillType;
        }
        //垂直4连方块
        else if ((objName == up2 && objName == up1 && objName == down1) || (objName == up1 && objName == down1 && objName == down2))
        {
            parentCallingScript.specialObjectToForm = skillBlock;
            objectType = BlockObjectType.SkillType;
        }
    }

    /// <summary>
    /// 是否可以移动
    /// </summary>
    /// <param 反向ID="dir"></param>
    /// <returns></returns>
    internal bool isMovePossibleInDirection(int dir)
    {
        parentCallingScript = this;

        if (adjacentItems[dir])
        {
            if (adjacentItems[dir].JustCheckIfCanBrust(name, dir))
            {
                return true;
            }
        }

        return false;
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
    /// 检测是否能消
    /// </summary>
    internal void CheckIfCanBrust()
    {
        if (isDestroyed)
        {
            return;
        }

        AssignLRUD();

        if ((name == left1 && name == left2) || (name == left1 && name == right1) || (name == right1 && name == right2) || (name == up1 && name == up2) || (name == up1 && name == down1) || (name == down1 && name == down2))
        {
            brust = true;
            GameManager.Instance.doesHaveBrustItem = true;
        }
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

        //执行消除动画
        transform.DOScale(Vector3.zero, 0.2f).OnComplete(delegate() 
        {
            //动画结束后摧毁块,延迟时间未定
            AnimationeEndDestroyBlock();
        });
    }
    void AnimationeEndDestroyBlock()
    {
        //可否摧毁
        brust = false;
        //是否被消
        isDestroyed = false;

        //重置块的邻近组合
        left1 = "left1";
        left2 = "left2";
        left3 = "left3";
        right1 = "right1";
        right2 = "right2";
        right3 = "right3";
        up1 = "up1";
        up2 = "up2";
        up3 = "up3";
        down1 = "down1";
        down2 = "down2";
        down3 = "down3";

        specialObjectToForm = null;
        myColumnScript = null;
        ColumnNumber = -1;
        ObjectPoolManager.Instance.RecycleBlockObject(gameObject);

        //Destroy(gameObject);
    }
}
