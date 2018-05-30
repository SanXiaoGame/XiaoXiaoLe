using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlatformManager : ManagerBase<GamePlatformManager>
{
    protected override void Awake()
    {
        base.Awake();
    }
    string platformPath;
    public string PlatformPath(string path)
    {
#if UNITY_EDITOR
        platformPath = "data source=" + path;
#elif UNITY_ANDROID
        platformPath = "uri=file:" + path;
#endif
        return platformPath;
    }
}
