/// <summary>
/// 存储所有常量路径
/// </summary>
public struct ConstData
{
    #region 所有UI预制体文件夹
    public const string UIPrefabsPath = "Prefabs/UIPrefabs";
    #endregion

    #region 所有英雄角色预制体文件夹
    public const string PlayerPrefabs = "Prefabs/PlayerPrefabs";
    #endregion

    #region 所有UI预制体的画布名
    public const string CanvasName = "/Canvas";
    #endregion

    #region 所有场景名
    public const string LoadingScene = "LoadingScene";
    #endregion

    #region 所有UI预制体名
    /// <summary>
    /// 加载界面预制体
    /// </summary>
    public const string LoadingPrefab = "LoadingBG";
    #endregion

    #region 所有游戏预制体文件夹
    //所有初始块的预制体
    public const string BlockPrefabs = "Prefabs/BlockPrefabs";
    //所有特殊块的预制体
    public const string SkillBlockPrefabs = "Prefabs/SkillBlockPrefabs";
    #endregion

    #region 所有游戏英雄技能特效的预制体文件夹
    //所有英雄技能特效的预制体
    public const string SkillPrefabs = "Prefabs/SkillPrefabs";
    #endregion

    #region 所有游戏英雄攻击特效的预制体文件夹
    //所有英雄技能特效的预制体
    public const string EffectPrefabs = "Prefabs/EffectPrefabs";
    #endregion

    #region 音乐文件夹
    public const string BGMusic = "Audios/BGMusic";
    #endregion

    #region 音效文件夹
    public const string Sound = "Audios";
    #endregion

    #region 职业进化所需材料
    /// <summary>
    /// 剑士进阶石
    /// </summary>
    public const int SwordsmanIntoTheStone = 2301;
    /// <summary>
    /// 骑士进阶石
    /// </summary>
    public const int CavaliersSteppingStone = 2302;
    /// <summary>
    /// 猎人进阶石
    /// </summary>
    public const int TheHunterEntersTheStone = 2303;
    /// <summary>
    /// 魔法师进阶石
    /// </summary>
    public const int TheMagicianIntoTheStone = 2304;
    /// <summary>
    /// 狂战士进阶石
    /// </summary>
    public const int FrenzyIntoAStone = 2305;
    #endregion

    #region 固定位置
    #endregion

    #region 金币上限
    public const int GoldCoinMax = 999999999;
    #endregion

    #region 动画的行为
    /// <summary>
    /// 空闲
    /// </summary>
    public const string Idle = "idle";
    /// <summary>
    /// 行走
    /// </summary>
    public const string Walk = "walk";
    /// <summary>
    /// 跑
    /// </summary>
    public const string Run = "run";
    /// <summary>
    /// 冲刺
    /// </summary>
    public const string Sprint = "sprint";
    /// <summary>
    /// 死亡
    /// </summary>
    public const string Death = "death";
    /// <summary>
    /// 跳跃
    /// </summary>
    public const string Jump = "jump";
    /// <summary>
    /// 蹲伏
    /// </summary>
    public const string Crouch = "crouch";
    /// <summary>
    /// 潜行
    /// </summary>
    public const string Sneak = "sneak";
    /// <summary>
    /// 攻击
    /// </summary>
    public const string Attack = "attack";
    #endregion

    #region 游戏数据库表名
    /// <summary>
    /// 要连接的数据库文件
    /// </summary>
    public const string SQLITE_NAME = "NereisQuest.sqlite";
    /// <summary>
    /// 背包表
    /// </summary>
    public const string Bag = "Bag";
    /// <summary>
    /// 人物列表
    /// </summary>
    public const string CharacterList = "CharacterList";
    /// <summary>
    /// 敌人表
    /// </summary>
    public const string Enemy = "Enemy";
    /// <summary>
    /// 装备表
    /// </summary>
    public const string Equipment = "Equipment";
    /// <summary>
    /// 物品表
    /// </summary>
    public const string Item = "Item";
    /// <summary>
    /// 等级经验表
    /// </summary>
    public const string Level = "Level";
    /// <summary>
    /// 角色存档表
    /// </summary>
    public const string Player = "Player";
    /// <summary>
    /// 技能表
    /// </summary>
    public const string Skill = "Skill";
    /// <summary>
    /// 状态表
    /// </summary>
    public const string State = "State";
    #endregion

    #region 角色或敌人默认速度值
    public const float attackSpeed = 0f;
    public const float movingSpeed = 1f;
    #endregion

    #region 数据库命令大全
    /// <summary>
    /// 查询命令SELECT:SELECT
    /// </summary>
    public const string SELECT = "SELECT ";
    /// <summary>
    /// 带*的FROM命令:*FROM
    /// </summary>
    public const string FROM01 = " *FROM ";
    /// <summary>
    /// 没有*的FROM命令:FROM
    /// </summary>
    public const string FROM02 = " FROM ";
    /// <summary>
    /// 指定范围命令:WHERE
    /// </summary>
    public const string WHERE = " WHERE ";
    /// <summary>
    /// 删除命名:DELETE
    /// </summary>
    public const string DELETE = "DELETE ";
    /// <summary>
    /// 更新:UPDATE
    /// </summary>
    public const string UPDATE = "UPDATE ";
    /// <summary>
    /// 改:SET
    /// </summary>
    public const string SET = " SET ";
    /// <summary>
    /// 过滤重复数据:DISTINCT
    /// </summary>
    public const string DISTINCT = " DISTINCT ";
    /// <summary>
    /// 插入数据:INSERT INTO
    /// </summary>
    public const string InsertLine01 = "INSERT INTO ";
    /// <summary>
    /// 插入数据:VALUES(
    /// </summary>
    public const string InsertLine02 = " VALUES(";
    /// <summary>
    /// 删除表:DROP TABLE
    /// </summary>
    public const string DeleteTable = "DROP TABLE ";
    /// <summary>
    /// 排序:ORDER BY
    /// </summary>
    public const string AscendingOrder01 = " ORDER BY ";
    /// <summary>
    /// 升序:ASC
    /// </summary>
    public const string AscendingOrder02 = " ASC";
    /// <summary>
    /// 降序:DESC
    /// </summary>
    public const string DescendingOrder02 = " DESC";
    #endregion

    #region 块的tag标签
    /// <summary>
    /// 初始块
    /// </summary>
    public const string Block = "Block";
    /// <summary>
    /// 技能块
    /// </summary>
    public const string SkillBlock = "SkillBlock";
    /// <summary>
    /// 高级块
    /// </summary>
    public const string SpecialBlock = "SpecialBlock";
    #endregion

    #region 消块分数
    /// <summary>
    /// 三消技能
    /// </summary>
    public const int SkillOne = 9;
    /// <summary>
    /// 四消技能
    /// </summary>
    public const int SkillTwo = 12;
    /// <summary>
    /// 五消技能
    /// </summary>
    public const int SkillThree = 15;
    /// <summary>
    /// 九宫技能
    /// </summary>
    public const int BlastSkill = 35;
    /// <summary>
    /// 全屏技能
    /// </summary>
    public const int SpecialSkill = 148;
    #endregion

    #region 块的名字
    /// <summary>
    /// 战士块名
    /// </summary>
    public const string Berserker = "Berserker";
    /// <summary>
    /// 法师块名
    /// </summary>
    public const string Caster = "Caster";
    /// <summary>
    /// 猎人块名
    /// </summary>
    public const string Hunter = "Hunter";
    /// <summary>
    /// 骑士块名
    /// </summary>
    public const string Knight = "Knight";
    /// <summary>
    /// 剑士块名
    /// </summary>
    public const string Saber = "Saber";
    #endregion

    #region 道具ID
    /// <summary>
    /// 恢复胶囊
    /// </summary>
    public const string CureCapsule = "2201";
    /// <summary>
    /// 兴奋剂
    /// </summary>
    public const string Stimulant = "2202";
    /// <summary>
    /// 复活十字架
    /// </summary>
    public const string ReviveCross = "2203";
    /// <summary>
    /// 方块粉碎剂
    /// </summary>
    public const string CubeBreak = "2204";
    /// <summary>
    /// 骸骨招魂幡
    /// </summary>
    public const string SkeletonSpiritism = "2205";
    /// <summary>
    /// 生命注射器
    /// </summary>
    public const string HealthSyringe = "2206";
    /// <summary>
    /// 肾上腺素注射器
    /// </summary>
    public const string ParanephrineSyringe = "2207";
    /// <summary>
    /// 神圣十字架
    /// </summary>
    public const string HolyCross = "2208";
    /// <summary>
    /// 方块传送剂
    /// </summary>
    public const string CubeTransfer = "2209";
    /// <summary>
    /// 幸运猫气球
    /// </summary>
    public const string LuckyCatBalloon = "2210";
    /// <summary>
    /// 恶灵招魂幡
    /// </summary>
    public const string GhostSpiritism = "2211";
    /// <summary>
    /// 剑士进阶石
    /// </summary>
    public const string SaberStone = "2301";
    /// <summary>
    /// 骑士进阶石
    /// </summary>
    public const string KnightStone = "2302";
    /// <summary>
    /// 猎人进阶石
    /// </summary>
    public const string HunterStone = "2303";
    /// <summary>
    /// 法师进阶石
    /// </summary>
    public const string CasterStone = "2304";
    /// <summary>
    /// 狂战士进阶石
    /// </summary>
    public const string BerserkerStone = "2305";
    /// <summary>
    /// 通关证明
    /// </summary>
    public const string GameClearProve = "2306";

    #endregion
}
