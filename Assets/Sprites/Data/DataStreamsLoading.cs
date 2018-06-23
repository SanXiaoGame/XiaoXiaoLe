using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 该脚本用来拷贝文件到沙盒中
/// </summary>
public class DataStreamsLoading : MonoBehaviour
{
    //用于监听拷贝操作是否完成
    public delegate void OnCopyFinished();
    public event OnCopyFinished onCopyFinished;

    /// <summary>
    /// 加载本地路径下数据
    /// </summary>
    /// <param 需要拷贝到沙盒路径的路径数组="paths"></param>
    public void LoadWitgPath(string[] paths)
    {
        //开启一个协程
        StartCoroutine("CoypFile", paths);
    }

    /// <summary>
    /// 拷贝文件
    /// </summary>
    /// <param 需要拷贝到沙盒路径的路径数组="paths"></param>
    /// <returns></returns>
    IEnumerator CoypFile(string[] paths)
    {
        //首先获取流路径
        string streamPath;
        for (int i = 0; i < paths.Length; i++)
        {
            //合并路径
            streamPath = System.IO.Path.Combine(Application.streamingAssetsPath, paths[i]);
#if UNITY_ANDROID
            streamPath = "jar:file://" + streamPath;
#elif UNITY_EDITOR
            streamPath = "file://" + streamPath;
#endif
            //下载该路径下的文件（本地的）
            WWW www = new WWW(streamPath);
            yield return www;
            //拼接沙盒路径
            string persistentPath = System.IO.Path.Combine(Application.persistentDataPath, paths[i]);
            Debug.Log(persistentPath);
            
            //拷贝streamPath到persistentPath
            WirteBytes(www.bytes, persistentPath);
        }

        //通知sqliteManager拷贝文件完成
        onCopyFinished();
    }

    /// <summary>
    /// 写入沙盒
    /// </summary>
    /// <param 写入文件的字节数="copyTargetBtyes"></param>
    /// <param 写入文件拷贝的地方="copyToPath"></param>
    private void WirteBytes(byte[] copyTargetBtyes, string copyToPath)
    {
        if (!System.IO.File.Exists(copyToPath))
        {
            //创建文件流
            System.IO.FileStream fs = new System.IO.FileStream(copyToPath, System.IO.FileMode.Create);
            //写入文件流
            fs.Write(copyTargetBtyes, 0, copyTargetBtyes.Length);
            //清除
            fs.Flush();
            //关闭
            fs.Close();
        }
    }
}
