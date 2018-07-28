using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 资源管理类
/// </summary>
public class ResourcesManager : ManagerBase<ResourcesManager>
{
    //所有声音
    Dictionary<string, AudioClip> audioClipAll = new Dictionary<string, AudioClip>();
    //所有特殊块的预制体
    Dictionary<string, GameObject> skillBlockAll = new Dictionary<string, GameObject>();
    //所有初始块的预制体
    Object[] blockAll;
    //所有UI预制体
    Dictionary<string, GameObject> UIPrefabAll = new Dictionary<string, GameObject>();
    //所有英雄特效的预制体
    Dictionary<string, GameObject> skillPrefabsAll = new Dictionary<string, GameObject>();
    //所有英雄的预制体
    Dictionary<string, GameObject> HeroAll = new Dictionary<string, GameObject>();
    //所有武器的预制体
    Dictionary<string, GameObject> WeaponAll = new Dictionary<string, GameObject>();
    //所有职业Logo、技能图标、背包物品图片
    Dictionary<string, Sprite> SpriteAll = new Dictionary<string, Sprite>();

    //所有Obj临时使用数组
    Object[] tempArr;

    protected override void Awake()
    {
        base.Awake();
        #region 清空资源字典
        audioClipAll.Clear();
        skillBlockAll.Clear();
        UIPrefabAll.Clear();
        skillPrefabsAll.Clear();
        HeroAll.Clear();
        WeaponAll.Clear();
        SpriteAll.Clear();
        #endregion

        //所有初始块的预制体
        blockAll = Resources.LoadAll(ConstData.BlockPrefabs);
        //所有特殊块的预制体
        tempArr = Resources.LoadAll(ConstData.SkillBlockPrefabs);
        for (int i = 0; i < tempArr.Length; i++)
        {
            skillBlockAll.Add(tempArr[i].name, tempArr[i] as GameObject);
        }
        //所有声音
        AudioClip[] audioClipArr = Resources.LoadAll<AudioClip>(ConstData.Sound);
        for (int i = 0; i < audioClipArr.Length; i++)
        {
            audioClipAll.Add(audioClipArr[i].name, audioClipArr[i]);
        }
        //所有英雄特效的预制体
        tempArr = Resources.LoadAll(ConstData.SkillPrefabs);
        for (int i = 0; i < tempArr.Length; i++)
        {
            skillPrefabsAll.Add(tempArr[i].name, tempArr[i] as GameObject);
        }
        //所有UI预制体
        tempArr = Resources.LoadAll(ConstData.UIPrefabsPath);
        for (int i = 0; i < tempArr.Length; i++)
        {
            UIPrefabAll.Add(tempArr[i].name, tempArr[i] as GameObject);
        }
        //所有英雄的预制体
        tempArr = Resources.LoadAll(ConstData.PlayerPrefabs);
        for (int i = 0; i < tempArr.Length; i++)
        {
            HeroAll.Add(tempArr[i].name, tempArr[i] as GameObject);
        }
        //所有武器的预制体
        tempArr = Resources.LoadAll(ConstData.WeaponPrefabs);
        for (int i = 0; i < tempArr.Length; i++)
        {
            WeaponAll.Add(tempArr[i].name, tempArr[i] as GameObject);
        }
        //所有职业Logo、技能图标、背包物品图片
        Sprite[] SpriteArr= Resources.LoadAll<Sprite>(ConstData.textureTemp);
        for (int i = 0; i < SpriteArr.Length; i++)
        {
            SpriteAll.Add(SpriteArr[i].name, SpriteArr[i]);
        }
    }

    /// <summary>
    /// 返回一个指定的歌
    /// </summary>
    /// <param 歌名="clipName"></param>
    /// <returns></returns>
    public AudioClip FindAudioClip<T>(T clipType) where T : struct
    {
        return audioClipAll[clipType.ToString()];
    }

    /// <summary>
    /// 返回所有指定的块预制体
    /// </summary>
    /// <param 块名="clipName"></param>
    /// <returns></returns>
    public Object[] FindBlockAll(BlockObjectType blockType)
    {
        if (blockType == BlockObjectType.NormalType)
        {
            return blockAll;
        }
        return null;
    }

    /// <summary>
    /// 返回一个指定的块预制体
    /// </summary>
    /// <param 块名="clipName"></param>
    /// <returns></returns>
    public GameObject FindBlock(BlockObjectType blockType)
    {
        return skillBlockAll[blockType.ToString()];
    }

    /// <summary>
    /// 根据名字查找技能特效预制体
    /// </summary>
    /// <param name="skillEffect"></param>
    /// <returns></returns>
    public GameObject FindPrefab(SkillPrefabs skillEffect)
    {
        return skillPrefabsAll[skillEffect.ToString()];
    }

    /// <summary>
    /// 根据名字查找指定的UI预制体
    /// </summary>
    /// <param UI预制体名="name"></param>
    /// <returns></returns>
    public GameObject FindUIPrefab(string name)
    {
        return UIPrefabAll[name];
    }
    /// <summary>
    /// 根据名字查找指定的角色预制体
    /// </summary>
    /// <param 角色预制体名="name"></param>
    /// <returns></returns>
    public GameObject FindPlayerPrefab(string name)
    {
        return HeroAll[name];
    }
    /// <summary>
    /// 根据名字查找指定的武器预制体
    /// </summary>
    /// <param 武器预制体名="name"></param>
    /// <returns></returns>
    public GameObject FindWeaponPrefab(string name)
    {
        return WeaponAll[name];
    }
    /// <summary>
    /// 根据名字查找指定的图片
    /// </summary>
    /// <param 图片名="name"></param>
    /// <returns></returns>
    public Sprite FindSprite(string name)
    {
        return SpriteAll[name];
    }
}
