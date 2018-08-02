using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 块的点击功能类
/// </summary>
public class BlockObjectTouch : MonoBehaviour
{
    //块的Image组件
    Image blockImage;
    //定义UI点击事件的基类
    UISceneWidget blockClick;
    //临时存储选中块和交换块的位置
    Transform blockPos1;
    Transform blockPos2;

    #region 存放鼠标移动前后的位置
    float fingerBeginX;
    float fingerBeginY;

    float fingerCurrentX;
    float fingerCurrentY;

    float fingerSegmentX;
    float fingerSegmentY;
    #endregion

    private void Awake()
    {
        blockClick = null;
        blockImage = GetComponent<Image>();
        blockPos1 = null;
        blockPos2 = null;
    }

    private void Start()
    {
        //绑定块的拖拽事件
        blockClick = UISceneWidget.Get(gameObject);

        if (blockClick != null)
        {
            blockClick.PointerDown += BlockOnPointerDown;
            blockClick.PointerUp += BlockOnPointerUp;
            blockClick.Drag += BlockOnDrag;
            blockClick.EndDrag += BlockOnEndDrag;
        }
    }

    //点击块的方法
    private void BlockOnPointerDown(PointerEventData eventData)
    {
        //检测是否是吃了交换药
        if (GameManager.Instance.props_CubeChangeSwitch)
        {
            ColumnManager.Instance.exchangeBlock.Add(eventData.pointerEnter.GetComponent<BlockObject>());

            blockImage.color = Color.black;
            if (ColumnManager.Instance.exchangeBlock.Count > 1)
            {
                if (eventData.pointerEnter.GetComponent<BlockObject>() == ColumnManager.Instance.exchangeBlock[0])
                {
                    ColumnManager.Instance.exchangeBlock.RemoveAt(ColumnManager.Instance.exchangeBlock.Count - 1);
                    return;
                }

                for (int i = 0; i < ColumnManager.Instance.exchangeBlock.Count; i++)
                {
                    ColumnManager.Instance.exchangeBlock[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }

                //交换的动画
                ColumnManager.Instance.exchangeBlock[0].transform.DOMove(ColumnManager.Instance.exchangeBlock[1].transform.position, 0.2f);
                ColumnManager.Instance.exchangeBlock[1].transform.DOMove(ColumnManager.Instance.exchangeBlock[0].transform.position, 0.2f).OnComplete(delegate ()
                {
                    GameManager.Instance.props_CubeChangeSwitch = false;
                });

                ColumnManager.Instance.exchangeBlock[0].ExternalTestingpecialBlock(ColumnManager.Instance.exchangeBlock[1]);
                ColumnManager.Instance.exchangeBlock[1].ExternalTestingpecialBlock(ColumnManager.Instance.exchangeBlock[0]);
                SwapTwoBlock(ColumnManager.Instance.exchangeBlock[0], ColumnManager.Instance.exchangeBlock[1]);

                ColumnManager.Instance.exchangeBlock.Clear();
            }
            return;
        }

        try
        {
            if (eventData.pointerEnter.tag == "Block")
            {
                blockPos1 = eventData.pointerEnter.transform;
                //检测是否是吃了删除药
                if (GameManager.Instance.props_CubeBreakSwitch)
                {
                    GameManager.Instance.props_CubeBreakSwitch = false;
                    blockPos1.GetComponent<BlockObject>().brust = true;
                    blockPos1 = null;
                    GameManager.Instance.RemoveBlock();
                    GameManager.Instance.AddMissingBlock();
                    return;
                }
                //检测是否是吃了变块药
                if (GameManager.Instance.props_SkillCubeSwitch)
                {
                    GameManager.Instance.props_SkillCubeSwitch = false;
                    blockPos1.GetComponent<BlockObject>().brust = true;
                    blockPos1.GetComponent<BlockObject>().specialObjectToForm = ResourcesManager.Instance.FindBlock(BlockObjectType.Flag);
                    GameManager.Instance.RemoveBlock();
                    GameManager.Instance.AddMissingBlock();
                    blockPos1 = null;
                    return;
                }

                blockImage.color = Color.red;

                fingerBeginX = Input.mousePosition.x;
                fingerBeginY = Input.mousePosition.y;
            }
        }
        catch (System.Exception ex)
        {
            blockPos1 = transform;
            Debug.Log(StringSplicingTool.StringSplicing("点击了屏幕外，选中块为空", ex.ToString()));
        }
    }

    //块完成点击的方法(只适用于编辑器)
    private void BlockOnPointerUp(PointerEventData eventData)
    {
        blockImage.color = new Color(1, 1, 1, 1);
    }
    //拖拽中
    private void BlockOnDrag(PointerEventData eventData)
    {
        if (eventData.pointerEnter.tag == "Block" && !GameManager.Instance.isBusy && !GameManager.Instance.props_CubeChangeSwitch && !GameManager.Instance.props_CubeBreakSwitch && !GameManager.Instance.props_SkillCubeSwitch)
        {
            GameManager.Instance.isBusy = true;
            fingerCurrentX = Input.mousePosition.x;
            fingerCurrentY = Input.mousePosition.y;

            fingerSegmentX = fingerCurrentX - fingerBeginX;
            fingerSegmentY = fingerCurrentY - fingerBeginY;
            //获取鼠标移动方向
            MouseMovementDirection();

            //交换的动画
            blockPos1.DOMove(blockPos2.position, 0.2f);
            blockPos2.DOMove(blockPos1.position, 0.2f);

            if ((!blockPos2.GetComponent<BlockObject>().isMovePossibleInDirection(GetDirectionOfSecondObject(blockPos2, blockPos1)) && !blockPos1.GetComponent<BlockObject>().isMovePossibleInDirection(GetDirectionOfSecondObject(blockPos1, blockPos2)))
                || (!blockPos1.GetComponent<BlockObject>().isMovePossibleInDirection(GetDirectionOfSecondObject(blockPos1, blockPos2)) && !blockPos2.GetComponent<BlockObject>().isMovePossibleInDirection(GetDirectionOfSecondObject(blockPos2, blockPos1))))
            {
                ChangePositionBack(blockPos1, blockPos2);
            }
            else
            {
                SwapTwoBlock(blockPos1.GetComponent<BlockObject>(), blockPos2.GetComponent<BlockObject>());
            }
        }
    }

    //块拖拽结束
    private void BlockOnEndDrag(PointerEventData eventData)
    {
        blockImage.color = new Color(1, 1, 1, 1);
    }

    /// <summary>
    /// 鼠标移动方向
    /// </summary>
    void MouseMovementDirection()
    {
        if (Mathf.Abs(fingerSegmentX) > Mathf.Abs(fingerSegmentY))
        {
            fingerSegmentY = 0;
        }
        else
        {
            fingerSegmentX = 0;
        }

        try
        {
            // fingerSegmentX=0 则是上下拖动
			if (fingerSegmentX == 0)
			{
				if (fingerSegmentY > 0)
				{
					blockPos2 = blockPos1.GetComponent<BlockObject>().adjacentItems[2].transform;
				}
				else
				{
					blockPos2 = blockPos1.GetComponent<BlockObject>().adjacentItems[3].transform;
				}
			}
			else if (fingerSegmentY == 0)
			{
				if (fingerSegmentX > 0)
				{
					blockPos2 = blockPos1.GetComponent<BlockObject>().adjacentItems[1].transform;
				}
				else
				{
					blockPos2 = blockPos1.GetComponent<BlockObject>().adjacentItems[0].transform;
				}
			}
        }
        catch (System.Exception ex)
        {
            if (blockPos2 == null)
            {
                print("blockPos2等于null" + ex);
                GameManager.Instance.isBusy = false;
            }
        }
    }

    /// <summary>
    /// 获取第二块的方向
    /// </summary>
    /// <param 块01="obj1"></param>
    /// <param 块02="obj2"></param>
    /// <returns></returns>
    int GetDirectionOfSecondObject(Transform obj1, Transform obj2)
    {
        int index = -1;
        if (obj1.position.x == obj2.position.x)
        {
            index = obj1.position.y < obj2.position.y ? 2 : 3;
        }
        else
        {
            index = obj1.position.x < obj2.position.x ? 1 : 0;
        }
        return index;
    }

    /// <summary>
    /// 块回来
    /// </summary>
    void ChangePositionBack(Transform block1, Transform block2)
    {
        //播放交换的音效(暂时不用)
        //AudioManager.Instance.PlayEffectMusic(SoundEffect.Attack);
        //交换回来的动画
        Vector3 tempPos = block1.position;
        blockPos1.DOMove(blockPos1.position, 0.2f).SetDelay(0.25f);
        blockPos2.DOMove(blockPos2.position, 0.2f).SetDelay(0.25f).OnComplete(delegate ()
        {
            GameManager.Instance.isBusy = false;
            block1 = null;
            block2 = null;
        });
    }

    /// <summary>
    /// 交换两个块
    /// </summary>
    /// <param 块1="item1"></param>
    /// <param 块2="item2"></param>
    void SwapTwoBlock(BlockObject block1, BlockObject block2)
    {
        ColumnScript firstColumn = block1.GetComponent<BlockObject>().myColumnScript;
        ColumnScript secondColumn = block2.GetComponent<BlockObject>().myColumnScript;

        block1.transform.parent = block2.transform.parent;
        block2.transform.parent = firstColumn.transform;

        block1.myColumnScript = secondColumn;
        block2.myColumnScript = firstColumn;

        firstColumn.BlockObjectsScriptList.RemoveAt(block1.GetComponent<BlockObject>().ColumnNumber);
        firstColumn.BlockObjectsScriptList.Insert(block1.GetComponent<BlockObject>().ColumnNumber, block2.GetComponent<BlockObject>());

        secondColumn.BlockObjectsScriptList.RemoveAt(block2.GetComponent<BlockObject>().ColumnNumber);
        secondColumn.BlockObjectsScriptList.Insert(block2.GetComponent<BlockObject>().ColumnNumber, block1.GetComponent<BlockObject>());

        int tempIndex = block1.ColumnNumber;
        block1.ColumnNumber = block2.ColumnNumber;
        block2.ColumnNumber = tempIndex;
        //交换完就重新分配邻居
        GameManager.Instance.AssignNeighbours(0f);
    }
}
