using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 矩形引导组件
/// </summary>
public class RectGuidanceController : MonoBehaviour
{

    /// <summary>
    /// 高亮显示的目标
    /// </summary>
    public Image Target;
    public int targetNum;
	//public Image[] Target2= new Image[4];
    public List<Image> Target3 = new List<Image>() ;
    public List<Vector3[]> cornerList = new List<Vector3[]>();
    private Vector3[] myConer = new Vector3[4];
    private Vector3[] _corners1 = new Vector3[4];
    private Vector3[] _corners2 = new Vector3[4];
    private Vector3[] _corners3 = new Vector3[4];
    private Vector3[] _corners4 = new Vector3[4];
    private Vector3[] targetCorners = new Vector3[4];
    List<Vector3[]> list = new List<Vector3 []>();
    /// <summary>
    /// 区域范围缓存
    /// </summary>
    private Vector3[] _corners = new Vector3[4];

	/// <summary>
	/// 镂空区域中心
	/// </summary>
	private Vector4 _center;

	/// <summary>
	/// 最终的偏移值X
	/// </summary>
	private float _targetOffsetX = 0f;

	/// <summary>
	/// 最终的偏移值Y
	/// </summary>
	private float _targetOffsetY = 0f;

	/// <summary>
	/// 遮罩材质
	/// </summary>
	private Material _material;
	
	/// <summary>
	/// 当前的偏移值X
	/// </summary>
	private float _currentOffsetX = 0f;

	/// <summary>
	/// 当前的偏移值Y
	/// </summary>
	private float _currentOffsetY = 0f;

	/// <summary>
	/// 动画收缩时间
	/// </summary>
	private float _shrinkTime = 0.5f;

	/// <summary>
	/// 事件渗透组件
	/// </summary>
	private GuidanceEventPenetrate _eventPenetrate;

	/// <summary>
	/// 世界坐标到画布坐标的转换
	/// </summary>
	/// <param name="canvas">画布</param>
	/// <param name="world">世界坐标</param>
	/// <returns>转换后在画布的坐标</returns>
	private Vector2 WorldToCanvasPos(Canvas canvas, Vector3 world)
	{
		Vector2 position;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, world,
			canvas.GetComponent<Camera>(), out position);
		return position;
	}
    float min_x, max_x, min_y, max_y;
    /// <summary>
    /// 求目标死角顶点
    /// </summary>
    /// <param name="list"></param>
    /// <param name="min_x"></param>
    /// <param name="max_x"></param>
    /// <param name="min_y"></param>
    /// <param name="max_y"></param>
    private  void  Min_Max(List<Vector3[]> list ,out float min_x, out float max_x, out float min_y, out float max_y)
    {
        min_x = list[0][0].x;
        max_x = list[0][0].x;
        min_y = list[0][0].y;
        max_y = list[0][0].y;
        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                //Debug.Log(i+"这是min_x" + min_x);
                if (min_x > list[i][j].x)
                {
                    min_x = list[i][j].x;
                }
            }
        }

        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (max_x < list[i][j].x)
                {
                    max_x = list[i][j].x;
                }
            }
        }

        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (min_y > list[i][j].y)
                {
                    min_y = list[i][j].y;
                }
            }
        }

        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (max_y < list[i][j].y)
                {
                    max_y = list[i][j].y;
                }
            }
        }

    }
    private void Start()
	{
        //Debug.Log("Target长度:" + Target3.Count);
        cornerList.Clear();
        list.Clear();
        _eventPenetrate = GetComponent<GuidanceEventPenetrate>();
        if (_eventPenetrate != null)
        {
            for (int i = 0; i < Target3.Count; i++)
            {
                _eventPenetrate.SetTargetImage(Target3[i]);
                cornerList.Add(_corners);
            }
            //_eventPenetrate.SetTargetImage(Target2[0]);
            //_eventPenetrate.SetTargetImage(Target2[1]);
            //_eventPenetrate.SetTargetImage(Target2[2]);
            //_eventPenetrate.SetTargetImage(Target2[3]);
        }
			
        Debug.Log("cornerlist长度" + cornerList.Count);
		//获取画布
		Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        //获取高亮区域四个顶点的世界坐标
        //Target.rectTransform.GetWorldCorners(_corners);
        //Debug.Log(_corners[0]);
        //Debug.Log(_corners[1]);
        //Debug.Log(_corners[2]);
        //Debug.Log(_corners[3]);
        //for (int i = 0; i < Target3.Count; i++)
        //{
        //    Target3[i].rectTransform.GetWorldCorners(cornerList[i]);
        //    //for (int j = 0; j < 4; j++)
        //    //{
        //    //    Debug.Log("第" + i + "个X轴度:" + cornerList[i][j].x + "Y轴度:" + cornerList[i][j].y);
        //    //    if (j==0)
        //    //    {
        //    //        list.Add(cornerList[i]);
        //    //    }
        //    //}
        //}
        ////Target3[0].rectTransform.GetWorldCorners(cornerList[0]);
        ////Target3[1].rectTransform.GetWorldCorners(cornerList[1]);
        ////Target3[2].rectTransform.GetWorldCorners(cornerList[2]);
        ////Target3[3].rectTransform.GetWorldCorners(cornerList[3]);
        ////list.Add(cornerList[0]);
        ////list.Add(cornerList[1]);
        ////list.Add(cornerList[2]);
        ////list.Add(cornerList[3]);
        ////for (int i = 0; i < cornerList.Count; i++)
        ////{
        ////    for (int j = 0; j < 4; j++)
        ////    {
        ////        Debug.Log("第" + i + "个X轴度:" + cornerList[i][j].x + "Y轴度:" + cornerList[i][j].y);
        ////    }
        ////    list.Add(cornerList[i]);
        ////}

        Target3[0].rectTransform.GetWorldCorners(_corners1);
        Target3[1].rectTransform.GetWorldCorners(_corners2);
        Target3[2].rectTransform.GetWorldCorners(_corners3);
        Target3[3].rectTransform.GetWorldCorners(_corners4);
        list.Add(_corners1);
        list.Add(_corners2);
        list.Add(_corners3);
        list.Add(_corners4);
        Min_Max(list, out min_x, out max_x, out min_y, out max_y);
        Debug.Log("x轴最小值" + min_x + "x轴最大值" + max_x + "y轴最小值" + min_y + "y轴最大值" + max_y);
  //      _corners[0] = _corners1[0];
  //      _corners[1] = _corners4[1];
  //      _corners[2] = _corners4[2];
  //      _corners[3] = _corners1[3];
  //      Debug.Log(_corners[0]);
  //      Debug.Log(_corners[1]);
  //      Debug.Log(_corners[2]);
  //      Debug.Log(_corners[3]);
  //      //计算高亮显示区域咋画布中的范围
  //      _targetOffsetX = Vector2.Distance(WorldToCanvasPos(canvas, _corners[0]), WorldToCanvasPos(canvas, _corners[3])) / 2f;
		//_targetOffsetY = Vector2.Distance(WorldToCanvasPos(canvas, _corners[0]), WorldToCanvasPos(canvas, _corners[1])) / 2f;

        _targetOffsetX = (max_x-min_x) / 2f;
        _targetOffsetY = (max_y-min_y) / 2f;
        //计算高亮显示区域的中心
        //float x = _corners[0].x + ((_corners[3].x - _corners[0].x) / 2f);
        //float y = _corners[0].y + ((_corners[1].y - _corners[0].y) / 2f);
        float x = (max_x + min_x) / 2f; 
        float y = (max_y + min_y) / 2f; 
        Vector3 centerWorld = new Vector3(x,y,0);
		Vector2 center = WorldToCanvasPos(canvas, centerWorld);
		//设置遮罩材料中中心变量
		Vector4 centerMat = new Vector4(center.x,center.y,0,0);
		_material = GetComponent<Image>().material;
		_material.SetVector("_Center",centerMat);
		//计算当前偏移的初始值
		RectTransform canvasRectTransform = (canvas.transform as RectTransform);
		if (canvasRectTransform != null)
		{
			//获取画布区域的四个顶点
			canvasRectTransform.GetWorldCorners(_corners);
			//求偏移初始值
			for (int i = 0; i < _corners.Length; i++)
			{
				if (i % 2 == 0)
					_currentOffsetX = Mathf.Max(Vector3.Distance(WorldToCanvasPos(canvas, _corners[i]), center), _currentOffsetX);
				else
					_currentOffsetY = Mathf.Max(Vector3.Distance(WorldToCanvasPos(canvas, _corners[i]), center), _currentOffsetY);
			}
		}
		//设置遮罩材质中当前偏移的变量
		_material.SetFloat("_SliderX",_currentOffsetX);
		_material.SetFloat("_SliderY",_currentOffsetY);
	}

	private float _shrinkVelocityX = 0f;
	private float _shrinkVelocityY = 0f;

	private void Update()
	{
		//从当前偏移值到目标偏移值差值显示收缩动画
		float valueX = Mathf.SmoothDamp(_currentOffsetX, _targetOffsetX, ref _shrinkVelocityX, _shrinkTime);
		float valueY = Mathf.SmoothDamp(_currentOffsetY, _targetOffsetY, ref _shrinkVelocityY, _shrinkTime);
		if (!Mathf.Approximately(valueX, _currentOffsetX))
		{
			_currentOffsetX = valueX;
			_material.SetFloat("_SliderX",_currentOffsetX);
		}

		if (!Mathf.Approximately(valueY, _currentOffsetY))
		{
			_currentOffsetY = valueY;
			_material.SetFloat("_SliderY",_currentOffsetY);
		}
	}
}
