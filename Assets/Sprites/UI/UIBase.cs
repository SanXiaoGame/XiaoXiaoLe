/// <summary>
/// UI的基础接口
/// </summary>
interface IUIBase
{
    //进入界面
    void OnEntering();
    //界面暂停
    void OnPausing();
    //界面唤醒
    void OnResuming();
    //界面退出
    void OnExiting();
}