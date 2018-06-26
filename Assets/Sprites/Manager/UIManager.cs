using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// UI界面管理
/// </summary>
public class UIManager : ManagerBase<UIManager>
{
    Stack<IUIBase> UIStack = new Stack<IUIBase>();
    //保存所有进栈的UI界面
    Dictionary<string, IUIBase> CurrentUI = new Dictionary<string, IUIBase>();
    //所有UI的父级画布
    Transform uiParent;

    /// <summary>
    /// 实例化界面,并返回当前界面
    /// </summary>
    /// <param UI的名字="uiname"></param>
    /// <returns></returns>
    IUIBase GetCurrentUI(string uiname)
    {
        //遍历是否有对应元素
        foreach (string name in CurrentUI.Keys)
        {
            if (uiname == name)
            {
                return CurrentUI[uiname];
            }
        }
        //给新加的UI找画布
        uiParent = transform.Find(ConstData.CanvasName);
        //生成资源中取出的预制体
        GameObject obj = Instantiate(ResourcesManager.Instance.FindUIPrefab(uiname));
        //UI界面名字一致性
        obj.name = uiname;
        //统一父级
        obj.GetComponent<RectTransform>().parent = uiParent;
        //局部坐标
        obj.GetComponent<RectTransform>().localPosition = Vector3.zero;
        //UI的缩放
        obj.GetComponent<RectTransform>().localScale = Vector3.one;
        //取得界面脚本的基础接口
        IUIBase iuibase = obj.GetComponent<IUIBase>();
        //新生成的UI加入CurrentUI的字典
        CurrentUI.Add(uiname, iuibase);
        //返回一个新生成的UI界面
        return iuibase;
    }

    /// <summary>
    /// 界面入栈
    /// </summary>
    /// <param name="name"></param>
    public void PushUIStack(string uiname)
    {
        //UIStack栈没有元素
        if (UIStack.Count > 0)
        {
            //返回栈顶的界面，且不移除
            IUIBase old_Pop = UIStack.Peek();
            old_Pop.OnEntering();
        }
        //创建界面
        IUIBase new_Pop = GetCurrentUI(uiname);
        //进入当前界面
        new_Pop.OnEntering();
        //界面进栈
        UIStack.Push(new_Pop);
    }

    public void PopUIStack()
    {
        if (UIStack.Count == 0)
        {
            return;
        }

        if (UIStack.Count > 0)
        {
            //展示新界面
            IUIBase newPop = UIStack.Peek();
            newPop.OnEntering();
        }
        //出栈,并移除界面
        IUIBase old_pop = UIStack.Pop();
        old_pop.OnExiting();
    }
}
