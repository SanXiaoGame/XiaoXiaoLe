/// <summary>
/// 游戏中所需的所有枚举类型
/// </summary>

//块的类型(暂存)
public enum BlockObjectType
{
    /// <summary>
    /// 初始块
    /// </summary>
    NormalType = 0,
    /// <summary>
    /// 技能块
    /// </summary>
    SkillType,
    /// <summary>
    /// 全屏块
    /// </summary>
    HighSkillType
}

//块的名字
public enum BlockName
{
    /// <summary>
    /// 战士块名
    /// </summary>
    Berserker = 0,
    /// <summary>
    /// 法师块名
    /// </summary>
    Caster,
    /// <summary>
    /// 弓箭手块名
    /// </summary>
    Hunter,
    /// <summary>
    /// 骑士块名
    /// </summary>
    Knight,
    /// <summary>
    /// 弓箭手块名
    /// </summary>
    Saber
}