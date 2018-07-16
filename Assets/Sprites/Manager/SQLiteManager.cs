using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 数据库管理类
/// </summary>
public class SQLiteManager : ManagerBase<SQLiteManager>
{
    //拥有一个拷贝路径类的实例
    DataStreamsLoading StreamsLoading;

    //读数据功能
    ReadData readData;
    //写数据功能
    WriteData writeData;

    //数据源：用来保存读到的数据模型
    public Dictionary<int, BagData> bagDataSource = new Dictionary<int, BagData>();
    public Dictionary<int, CharacterListData> characterDataSource = new Dictionary<int, CharacterListData>();
    public Dictionary<int, EnemyData> enemyDataSource = new Dictionary<int, EnemyData>();
    public Dictionary<int, EquipmentData> equipmentDataSource = new Dictionary<int, EquipmentData>();
    public Dictionary<int, ItemData> itemDataSource = new Dictionary<int, ItemData>();
    public Dictionary<int, LVData> lVDataSource = new Dictionary<int, LVData>();
    public  Dictionary<int, PlayerData> playerDataSource = new Dictionary<int, PlayerData>();
    public  Dictionary<int, SkillData> skillDataSource = new Dictionary<int, SkillData>();
    public  Dictionary<int, StateData> stateDataSource = new Dictionary<int, StateData>();
    public  Dictionary<string, HeroData> team = new Dictionary<string, HeroData>();        //小队字典--from Duke 
    protected override void Awake()
    {
        base.Awake();
        //启动流路径(将需要操作的文件从流路径拷贝到沙盒中)
        StreamsLoading = gameObject.AddComponent<DataStreamsLoading>();

        //是否拷贝完成
        StreamsLoading.onCopyFinished += OnLoadFinished;
        //开始拷贝（输入对应的表名）
        StreamsLoading.LoadWitgPath(ConstData.SQLITE_NAME);
    }

    /// <summary>
    /// 当文件从流路径拷贝到沙盒路径完成时执行的回调函数，一旦该函数被成功回调，则意味着拷贝文件已完成，此时可执行存取等数据库操作
    /// </summary>
    string dataBasePath;
    internal void OnLoadFinished()
    {
        Debug.Log("路径拷贝完成");
        //数据库存放沙盒的路径
        //dataBasePath = System.IO.Path.Combine(Application.persistentDataPath, ConstData.SQLITE_NAME);
        dataBasePath = StringSplicingTool.StringSplicing(new string[] { Application.persistentDataPath,"/", ConstData.SQLITE_NAME });
        //初始化读和写的功能
        readData = new ReadData(dataBasePath);
        writeData = new WriteData(dataBasePath);

        //清空数据
        bagDataSource.Clear();
        characterDataSource.Clear();
        enemyDataSource.Clear();
        equipmentDataSource.Clear();
        itemDataSource.Clear();
        lVDataSource.Clear();
        playerDataSource.Clear();
        skillDataSource.Clear();
        stateDataSource.Clear();

        //取出数据存入字典
        readData.GetData(ConstData.Bag);
        readData.GetData(ConstData.CharacterList);
        readData.GetData(ConstData.Enemy);
        readData.GetData(ConstData.Equipment);
        readData.GetData(ConstData.Item);
        readData.GetData(ConstData.Level);
        readData.GetData(ConstData.Skill);
        readData.GetData(ConstData.State);
        readData.GetData(ConstData.Player);
        print("获取完数据");
        //队伍字典初始化键
        team.Add(ConstData.FlagMan, null);
        team.Add(ConstData.Saber, null);
        team.Add(ConstData.Knight, null);
        team.Add(ConstData.Berserker, null);
        team.Add(ConstData.Caster, null);
        team.Add(ConstData.Hunter, null);

        //执行读取结束委托
        SceneAss_Manager.Instance.ExecutionOfEvent(1);
    }

    /// <summary>
    /// 插入一条新的数据到存档中
    /// </summary>
    /// <param 表名="tbName"></param>
    /// <param 表id="id"></param>
    /// <param 角色名="player_Name"></param>
    /// <param 角色类型="player_Class"></param>
    /// <param 角色介绍="player_Description"></param>
    /// <param 血量="player_HP"></param>
    /// <param 物攻="player_AD"></param>
    /// <param 魔攻="player_AP"></param>
    /// <param 物防="player_DEF"></param>
    /// <param 魔防="player_RES"></param>
    /// <param 技能01="player_SkillOneID"></param>
    /// <param 技能02="player_SkillTwoID"></param>
    /// <param 技能03="player_SkillThreeID"></param>
    /// <param 进化后血量="player_EXHP"></param>
    /// <param 进化后物攻="player_EXAD"></param>
    /// <param 进化后魔攻="player_EXAP"></param>
    /// <param 进化后物防="player_EXDEF"></param>
    /// <param 进化后魔防="player_EXRES"></param>
    /// <param 携带武器="player_Weapon"></param>
    /// <param 携带装备="player_Equipment"></param>
    /// <param 等级="player_Level"></param>
    /// <param 经验="player_EXP"></param>
    public void InsetDataToTable(int id, string player_Name, string player_Class, string player_Description, int player_HP, int player_AD, int player_AP, int player_DEF, int player_RES, int player_SkillOneID, int player_SkillTwoID, int player_SkillThreeID, int player_EXHP, int player_EXAD, int player_EXAP, int player_EXDEF, int player_EXRES, int player_Weapon, int player_Equipment, int player_Level, int player_EXP)
    {
        string[] values = new string[] { id.ToString(), player_Name, player_Class, player_Description, player_HP.ToString(), player_AD.ToString(), player_AP.ToString(), player_DEF.ToString(), player_RES.ToString(), player_SkillOneID.ToString(), player_SkillTwoID.ToString(), player_SkillThreeID.ToString(), player_EXHP.ToString(), player_EXAD.ToString(), player_EXAP.ToString(), player_EXDEF.ToString(), player_EXRES.ToString(), player_Weapon.ToString(), player_Equipment.ToString(), player_Level.ToString(), player_EXP.ToString() };
        writeData.InsertDataToSQL(ConstData.Player, values);
    }

    /// <summary>
    /// 更新指定存档数据
    /// </summary>
    /// <param 表名="tbName"></param>
    /// <param 更新的字段="field"></param>
    /// <param 更新字段的值="fieldValue"></param>
    /// <param 条件字段="key"></param>
    /// <param 条件字段的值="keyValue"></param>
    public void UpdataDataFromTable(string tbName, string field, int fieldValue, string key, int keyValue)
    {
        writeData.UpdataDataFromSQL(tbName, field, fieldValue, key, keyValue);
    }

    /// <summary>
    /// 删除存档中一行指定的数据
    /// </summary>
    /// <param 表名="tbName"></param>
    /// <param 对应字段="key"></param>
    /// <param 对应ID号="keyValue"></param>
    public void DeleteTableData(string tbName, string key, int keyValue)
    {
        writeData.DeleteInTableData(tbName, key, keyValue);
    }
}
