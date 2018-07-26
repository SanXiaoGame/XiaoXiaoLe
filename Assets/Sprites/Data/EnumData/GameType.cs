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

//人物场地类型
public enum CharacterFieldType
{
    /// <summary>
    /// 酒店
    /// </summary>
    Hotel = 0,
    /// <summary>
    /// 商城
    /// </summary>
    SuperMarket,
    /// <summary>
    /// 商城VIP
    /// </summary>
    VIP,
}

//选关确认窗口类型
public enum UIChoiceLevelConfirmFrameType
{
    /// <summary>
    /// 选关用
    /// </summary>
    ChoiceLevel = 0,
    /// <summary>
    /// 物品用
    /// </summary>
    Item,
    /// <summary>
    /// 消耗品栏用
    /// </summary>   
    ConsumableBar
}