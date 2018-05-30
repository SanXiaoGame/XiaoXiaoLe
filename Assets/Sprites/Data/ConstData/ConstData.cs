/// <summary>
/// 存储所有常量路径
/// </summary>
public struct ConstData
{
    #region 所有UI预制体文件夹
    public const string UIPrefabsPath = "Prefabs/UIPrefabs";
    #endregion

    #region 音乐文件夹
    public const string BGMusic = "Audios/BGMusic";
    #endregion

    #region 音效文件夹
    public const string SoundEffect = "Audios/SoundEffect";
    #endregion

    #region 主要常用名称
    #endregion

    #region 固定位置
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
    public const string TableName01 = "还没有";
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
}
