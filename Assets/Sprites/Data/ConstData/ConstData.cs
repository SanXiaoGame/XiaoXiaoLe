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

    #region 所有敌人角色预制体文件夹
    public const string EnemyPrefabs = "Prefabs/EnemyPrefabs";
    #endregion

    #region 所有战斗武器预制体文件夹
    /// <summary>
    /// 武器预制体文件夹
    /// </summary>
    public const string WeaponPrefabs = "Prefabs/WeaponPrefabs";
    #endregion

    #region 所有图片文件夹
    public const string textureTemp = "textureTemp";
    #endregion

    #region 所有UI预制体的画布名
    public const string CanvasName = "/Canvas";
    #endregion

    #region 所有场景名
    /// <summary>
    /// 加载场景名
    /// </summary>
    public const string LoadingScene = "LoadingScene";
    /// <summary>
    /// 主城场景名
    /// </summary>
    public const string MainScene = "MainScene";
    #endregion

    #region 所有UI预制体名
    /// <summary>
    /// 退出游戏界面预制体
    /// </summary>
    public const string UIExitGame = "UIExitGame";
    /// <summary>
    /// 设置界面预制体
    /// </summary>
    public const string UISetting = "UISetting";
    /// <summary>
    /// 人物管理界面预制体
    /// </summary>
    public const string UICharacterManagePrefab = "UICharacterManagePrefab";
    /// <summary>
    /// 角色管理界面GameArea预制体
    /// </summary>
    public const string UICharacterManage_GameArea = "UICharacterManage_GameArea";
    /// <summary>
    /// 主城界面预制体
    /// </summary>
    public const string UIMainCityPrefab = "UIMainCityManagerController";
    /// <summary>
    /// 主城界面GameArea预制体
    /// </summary>
    public const string UIMainCityPrefab_GameArea = "UIMainCityPrefab_GameArea";
    /// <summary>
    /// 酒馆界面预制体
    /// </summary>
    public const string UIDrunkery = "UIDrunkery";
    /// <summary>
    /// 商城界面预制体
    /// </summary>
    public const string UIShoppingMall = "UIShoppingMall";
    /// <summary>
    /// 选择关卡界面预制体
    /// </summary>
    public const string UIChoiceLevelPrefab = "UIChoiceLevelPrefab";
    /// <summary>
    /// 战斗界面预制体
    /// </summary>
    public const string UIBattlePrefab = "UIBattlePrefab";
    /// <summary>
    /// 商店界面预制体
    /// </summary>
    public const string UIStorePrefab = "UIStorePrefab";
    /// <summary>
    /// 商店界面GameArea预制体
    /// </summary>
    public const string UIStore_GameArea = "UIStore_GameArea";
    
    /// 角色管理界面——角色条
    /// </summary>
    public const string CharacterBar = "CharacterBar";
    /// <summary>
    /// 装备格子
    /// </summary>
    public const string Grid = "Grid";
    /// <summary>
    /// 附加道具的格子
    /// </summary>
    public const string GridEx = "GridEx";
    /// <summary>
    /// 装备图标
    /// </summary>
    public const string ItemIcon = "ItemIcon";
	/// <summary>
    /// 格子光圈
    /// </summary>
    public const string pitchOn = "pitchOn";
    /// <summary>
    /// 第一关场景
    /// </summary>
    public const string Stage01 = "Stage01";
    #endregion

    #region UI区域名
    /// <summary>
    /// 系统操作区
    /// </summary>
    //----------------  角色管理页面  -------------------
    public const string SystemArea = "SystemArea";
    public const string SystemArea_CharacterIcon = "SystemArea/CharacterIcon";
    public const string SystemArea_TeamIcon = "SystemArea/TeamIcon";
    public const string SystemArea_EquipmentIcon = "SystemArea/EquipmentIcon";
    public const string SystemArea_MainCityIcon = "SystemArea/MainCityIcon";
    //------------------  主城方面  ---------------------
    public const string SystemArea_CharacterButton = "SystemArea/CharacterButton";
    public const string SystemArea_StoreButton = "SystemArea/StoreButton";
    public const string SystemArea_SuperMarketButton = "SystemArea/SuperMarketButton";
    public const string SystemArea_DrunkeryButton = "SystemArea/DrunkeryButton";
    public const string SystemArea_SettingButton = "SystemArea/SettingButton";
    public const string SystemArea_BattleButton = "SystemArea/BattleButton";
    //------------------  酒店  ---------------------
    public const string CharacterButton = "CharacterDisplayContent/CharacterButton";
    public const string CharacterSpot = "CharacterDisplayContent/CharacterSpot";
    public const string SummonButton = "SummonButton";
    //------------------  商城  ---------------------
    public const string SystemArea_ShopBuyButton = "SystemArea/ShopBuyButton";
    public const string SystemArea_SuperMarketBuyButton = "SystemArea/SuperMarketBuyButton";
    public const string SystemArea_ContractButton = "SystemArea/ContractButton";
    public const string SystemArea_MainCityButton = "SystemArea/MainCityButton";
    //------------------  商店  ---------------------
    public const string SystemArea_BuyIcon = "SystemArea/BuyIcon";
    public const string SystemArea_SellIcon = "SystemArea/SellIcon";
    //------------------  战斗  ---------------------
    public const string SystemArea_SettingIcon = "SystemArea/SettingIcon";
    /// <summary>
    /// 控制区
    /// </summary>
    //----------------  角色管理页面  -------------------
    public const string ControllerArea = "ControllerArea";
    public const string ControllerArea_ItemListBG = "ControllerArea/ItemListBG";
    public const string ControllerArea_ListContent2 = "ControllerArea/ItemListBG/ListViewport/ListContent";
    public const string ControllerArea_CharacterListBG = "ControllerArea/CharacterListBG";
    public const string ControllerArea_ListContent = "ControllerArea/CharacterListBG/ListViewport/ListContent";
    //------------------  主城方面  ---------------------
    public const string ControllerArea_ItemListBG_WP = "ControllerArea/ItemListBG_WP";
    public const string ControllerArea_ItemListBG_EQ = "ControllerArea/ItemListBG_EQ";
    public const string ControllerArea_ItemListBG_CO = "ControllerArea/ItemListBG_CO";
    public const string ControllerArea_ItemListBG_MT = "ControllerArea/ItemListBG_MT";
    //------------------  酒店  ---------------------
    public const string DrunkeryContentBG_Name = "DrunkeryContentBG/NameBG/Name";
    public const string DrunkeryContentBG_Price = "DrunkeryContentBG/PriceBG/Price";
    public const string DrunkeryContentBG_HP = "DrunkeryContentBG/HPBG/HP";
    public const string DrunkeryContentBG_AD_AP = "DrunkeryContentBG/AD_APBG/AD_AP";
    public const string DrunkeryContentBG_DEF_RES = "DrunkeryContentBG/DEF_RESBG/DEF_RES";
    public const string DrunkeryContentBG_ClassLogo = "DrunkeryContentBG/NameBG/LineIcon/ClassIcon";
    //------------------  商城  ---------------------
    public const string ShoppingMall_ConfirmButton = "ConfirmButton";
    public const string ShoppingMall_ItemListBG = "ControllerArea/ItemListBG";
    public const string ShoppingMall_ContractSetListBG = "ControllerArea/ContractSetListBG";
    public const string ShoppingMall_ContractSetListParent = "ControllerArea/ContractSetListBG/ListViewport/ListContent";
    public const string ShoppingMall_CharaVIP = "ControllerArea/ContractSetListBG/ListViewport/ListContent/project01";
    public const string ShoppingMall_CharaFive = "ControllerArea/ContractSetListBG/ListViewport/ListContent/project02";
    public const string ShoppingMall_CharaLevelUP = "ControllerArea/ContractSetListBG/ListViewport/ListContent/project03";
    public const string ShoppingMall_CharaToBeStronge = "ControllerArea/ContractSetListBG/ListViewport/ListContent/project04";
    public const string ShoppingMall_Fruit = "ControllerArea/ContractSetListBG/ListViewport/ListContent/project05";
    public const string getCharaFrame = "getCharaFrame";
    public const string propertyUpFrame = "propertyUpFrame";
    public const string StateUpText = "StateUpText";
    public const string GetItemFrame = "GetItemFrame";
    //------------------  关卡选择  ---------------------
    public const string ChoiceLevel_ConsumableBar = "ControllerExArea/ConsumableBar";
    public const string ChoiceLevel_ChoiceConfirmButton = "ControllerExArea/ChoiceConfirmButton";
    public const string ChoiceLevel_LeftButton = "ControllerExArea/LeftButton";
    public const string ChoiceLevel_RightButton = "ControllerExArea/RightButton";
    public const string ChoiceLevel_CheckpointContent = "ControllerExArea/ChoiceConfirmButton/CheckpointContent";
    public const string ChoiceLevel_ChoiceLevelContent = "ChoiceLevelContent";
    //------------------  商店  ---------------------
    public const string ControllerArea_StoreListBG_WP = "ControllerArea/StoreListBG_WP";
    public const string ControllerArea_StoreListBG_EQ = "ControllerArea/StoreListBG_EQ";
    public const string ControllerArea_StoreListBG_CO = "ControllerArea/StoreListBG_CO";
    public const string ControllerArea_StoreListBG_MT = "ControllerArea/StoreListBG_MT";
    public const string ControllerArea_StoreListContent_WP = "ControllerArea/StoreListBG_WP/ListViewport/ListContent";
    public const string ControllerArea_StoreListContent_EQ = "ControllerArea/StoreListBG_EQ/ListViewport/ListContent";
    public const string ControllerArea_StoreListContent_CO = "ControllerArea/StoreListBG_CO/ListViewport/ListContent";
    public const string ControllerArea_StoreListContent_MT = "ControllerArea/StoreListBG_MT/ListViewport/ListContent";
    public const string Store_BuyConfirm = "GameArea/BuyConfirm";
    //------------------  设置  ---------------------
    public const string MusicButton = "SettingBG/MusicButton";
    public const string SoundEffectButton = "SettingBG/SoundEffectButton";
    public const string MusicSlider = "SettingBG/MusicSlider";
    public const string SoundEffectSlider = "SettingBG/SoundEffectSlider";
    public const string SettingReturnGame = "SettingBG/ReturnGame";
    public const string SettingQuitGame = "SettingBG/QuitGame";
    /// <summary>
    /// 控制区附属
    /// </summary>
    //----------------  角色管理页面  -------------------
    public const string ControllerExArea = "ControllerExArea";
    public const string ControllerExArea_SkillMode = "ControllerExArea/SkillMode";
    public const string ControllerExArea_TeamMode = "ControllerExArea/TeamMode";
    public const string ControllerExArea_TeamModeUP = "ControllerExArea/TeamMode/TeamEditUp";
    public const string ControllerExArea_TeamModeDOWN = "ControllerExArea/TeamMode/TeamEditDown";
    public const string ControllerExArea_TeamModeCONFIRM = "ControllerExArea/TeamMode/TeamEditOK";
    public const string ControllerExArea_EquipmentMode = "ControllerExArea/EquipmentMode";
    //------------------  主城方面  ---------------------
    public const string ControllerExArea_WeaponBag = "ControllerExArea/WeaponBag";
    public const string ControllerExArea_EquipmentBag = "ControllerExArea/EquipmentBag";
    public const string ControllerExArea_ConsumableBag = "ControllerExArea/ConsumableBag";
    public const string ControllerExArea_MaterialBag = "ControllerExArea/MaterialBag";
    //------------------  商城  ---------------------
    public const string ShoppingMall_Weapon = "ControllerExArea/WeaponBag/WeaponIcon";
    public const string ShoppingMall_Equipment = "ControllerExArea/EquipmentBag/EquipmentIcon";
    public const string ShoppingMall_Consumable = "ControllerExArea/ConsumableBag/ConsumableIcon";
    public const string ShoppingMall_Material = "ControllerExArea/MaterialBag/MaterialIcon";
    //------------------  商店  ---------------------
    public const string ControllerExArea_WeaponStore = "ControllerExArea/WeaponStore";
    public const string ControllerExArea_EquipmentStore = "ControllerExArea/EquipmentStore";
    public const string ControllerExArea_ConsumableStore = "ControllerExArea/ConsumableStore";
    public const string ControllerExArea_MaterialStore = "ControllerExArea/MaterialStore";
    //------------------  战斗  ---------------------
    public const string ControllerExArea_ItemOne = "ControllerExArea/ItemOne";
    public const string ControllerExArea_ItemTwo = "ControllerExArea/ItemTwo";
    public const string ControllerExArea_ItemThree = "ControllerExArea/ItemThree";
    public const string ControllerExArea_ItemFour = "ControllerExArea/ItemFour";
    /// <summary>
    /// 筛选
    /// </summary>
    public const string Filter = "Filter";
    public const string Filter_StoneSaberTag = "Filter/StoneSaberTag";
    public const string StoneSaberTag = "StoneSaberTag";
    public const string Filter_StoneKnightTag = "Filter/StoneKnightTag";
    public const string StoneKnightTag = "StoneKnightTag";
    public const string Filter_StoneBerserkerTag = "Filter/StoneBerserkerTag";
    public const string StoneBerserkerTag = "StoneBerserkerTag";
    public const string Filter_StoneHunterTag = "Filter/StoneHunterTag";
    public const string StoneHunterTag = "StoneHunterTag";
    public const string Filter_StoneCasterTag = "Filter/StoneCasterTag";
    public const string StoneCasterTag = "StoneCasterTag";
    /// <summary>
    /// 游戏画面区
    /// </summary>
    //----------------  角色管理页面  -------------------
    public const string GameArea = "GameArea";
    public const string GameArea_MessageFrame = "GameArea/MessageFrame";
    public const string GameArea_MessageFrame_Name = "GameArea/MessageFrame/Name";
    public const string GameArea_MessageFrame_LV = "GameArea/MessageFrame/LV";
    public const string GameArea_MessageFrame_EXPSlider = "GameArea/MessageFrame/EXPSlider";
    public const string GameArea_MessageFrame_HP = "GameArea/MessageFrame/HP";
    public const string GameArea_MessageFrame_AD = "GameArea/MessageFrame/AD";
    public const string GameArea_MessageFrame_AP = "GameArea/MessageFrame/AP";
    public const string GameArea_MessageFrame_DEF = "GameArea/MessageFrame/DEF";
    public const string GameArea_MessageFrame_RES = "GameArea/MessageFrame/RES";
    public const string GameArea_Step01 = "GameArea/Step01";
    public const string GameArea_Step02 = "GameArea/Step02";
    public const string GameArea_Step03 = "GameArea/Step03";
    public const string GameArea_Step04 = "GameArea/Step04";
    public const string GameArea_Step05 = "GameArea/Step05";
    public const string GameArea_GoldCoin = "GameArea/GoldFrame/GoldCoin";
    //------------------  主城方面  ---------------------
    public const string GameArea_MessageFrame_NameAndClass = "GameArea/MessageFrame/NameAndClass";
    public const string GameArea_MessageFrame_Property = "GameArea/MessageFrame/Property";
    //------------------  商城  ---------------------
    public const string ShoppingMalTextContent = "GameArea/MessageFrame/Content";
    public const string GameArea_Diamonds = "GameArea/DiamondsFrame/DiamondsCoin";
    //------------------  商店  ---------------------
    public const string GameArea_GoodsIntro = "GameArea/MessageFrame/GoodsIntro";
    public const string GameArea_BuyConfirm = "GameArea/BuyConfirm";
    /// <summary>
    /// 介绍栏
    /// </summary>
    public const string Introduction = "Introduction";
    public const string Introduction_Content = "Introduction/ContentBG/Content";
    public const string Introduction_CloseButton = "Introduction/CloseButton";
    /// <summary>
    /// 确认窗口
    /// </summary>
    public const string ConfirmFrame = "ConfirmFrame";
    public const string ErrorFrame = "ErrorFrame";
    public const string TeamEditOverFrame = "TeamEditOverFrame";
    public const string ConfirmFrame_ContentText = "ConfirmFrame/ContentBG/Content";
    public const string ConfirmFrame_ConfirmButton = "ConfirmFrame/ConfirmButton";
    public const string ConfirmFrame_CancelButton = "ConfirmFrame/CancelButton";
    //------------------  商城  ---------------------
    public const string RechargeFrame = "RechargeFrame";
    public const string RechargeInputField = "RechargeFrame/RechargeInputField";
    public const string RechargeCloseButton = "RechargeFrame/CloseButton";
    public const string RechargeConfirmButton = "RechargeFrame/RechargeConfirmButton";
    public const string ContractSetConfirmFrame = "ContractSetConfirmFrame";
    /// <summary>
    /// 装备替换窗口
    /// </summary>
    public const string ConfirmFrame_EQ = "EquipmentConfirmFrame";
    public const string ConfirmFrame_EQ_ContentText = "EquipmentConfirmFrame/ContentBG/Content";
    public const string ConfirmFrame_EQ_ConfirmButton = "EquipmentConfirmFrame/ConfirmButton";
    public const string ConfirmFrame_EQ_CancelButton = "EquipmentConfirmFrame/CancelButton";
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

    #region 技能logo
    public const string Skill_saber1 = "3001";
    public const string Skill_saber2 = "3002";
    public const string Skill_saber3 = "3003";
    public const string Skill_knight1 = "3104";
    public const string Skill_knight2 = "3105";
    public const string Skill_knight3 = "3106";
    public const string Skill_berserker1 = "3004";
    public const string Skill_berserker2 = "3005";
    public const string Skill_berserker3 = "3006";
    public const string Skill_Caster1 = "3101";
    public const string Skill_Caster2 = "3102";
    public const string Skill_Caster3 = "3103";
    public const string Skill_Hunter1 = "3007";
    public const string Skill_Hunter2 = "3008";
    public const string Skill_Hunter3 = "3009";
    #endregion

    #region 固定父物体
    /// <summary>
    /// 列的父物体
    /// </summary>
    public const string ColumnParentObj = "/Canvas/UIBattlePrefab/ColumnParent";
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

    #region tag标签
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
    /// <summary>
    /// 玩家标签
    /// </summary>
    public const string PlayerTag = "Player";
    /// <summary>
    /// 敌人标签
    /// </summary>
    public const string EnemyTag = "Enemy";
    /// <summary>
    /// 旗手标签
    /// </summary>
    public const string FlagManTag = "FlagMan";
    /// <summary>
    /// 胜利点
    /// </summary>
    public const string WinFlagTag = "WinFlag";
    /// <summary>
    /// 战斗刷怪点
    /// </summary>
    public const string MonsterPoint = "MonsterPoint";
    /// <summary>
    /// BOSS刷怪点
    /// </summary>
    public const string BossPoint = "BossPoint";
    /// <summary>
    /// 墙壁
    /// </summary>
    public const string Wall = "Wall";
    /// <summary>
    /// 装备类型
    /// </summary>
    public const string EquipmentType = "EquipmentType";
    /// <summary>
    /// 道具类型
    /// </summary>
    public const string ItemType = "ItemType";
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

    #region 块和职业的名字
    /// <summary>
    /// 旗手职业名
    /// </summary>
    public const string FlagMan = "FlagMan";
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

    #region Player表的字段
    public const string player_ID = "ID";
    public const string player_Name = "player_Name";
    public const string player_Class = "player_Class";
    public const string player_Description = "player_Description";
    public const string player_HP = "player_HP";
    public const string player_AD = "player_AD";
    public const string player_AP = "player_AP";
    public const string player_DEF = "player_DEF";
    public const string player_RES = "player_RES";
    public const string player_SkillOneID = "player_SkillOneID";
    public const string player_SkillTwoID = "player_SkillTwoID";
    public const string player_SkillThreeID = "player_SkillThreeID";
    public const string player_EXHP = "player_EXHP";
    public const string player_EXAD = "player_EXAD";
    public const string player_EXAP = "player_EXAP";
    public const string player_EXDEF = "player_EXDEF";
    public const string player_EXRES = "player_EXRES";
    public const string player_Weapon = "player_Weapon";
    public const string player_Equipment = "player_Equipment";
    public const string player_Level = "player_Level";
    public const string player_EXP = "player_EXP";
    public const string player_GoldCoin = "GoldCoin";
    public const string player_Diamond = "Diamond";
    public const string player_PrefabsID = "PrefabsID";
    #endregion

    #region 背包表的字段
    public const string Bag_Grid = "Bag_Grid";
    public const string Bag_Weapon = "Bag_Weapon";
    public const string Bag_Equipment = "Bag_Equipment";
    public const string Bag_Consumable = "Bag_Consumable";
    public const string Bag_Material = "Bag_Material";
    #endregion

    //主手位置路径
    public const string MainFist = "Bones/Torso/L-arm/L-fist/Weapon";
    //副手位置路径
    public const string MinorFist = "Bones/Torso/R-arm/R-fist/Weapon2";

    //角色条三色
    public const string HeroBar = "LineList";
    public const string HeroBar_Select = "LineList_PitchOn";
    public const string HeroBar_Team = "LineList_PitchOn2";
    //技能图标空
    public const string SkillNull = "GameControllerArea_EXFrameMask";

    public const string All = "All";
    //装备格子总数
    public const int GridCount = 36;
    //装备种类
    public const string ListType_Weapon = "Weapon";
    public const string ListType_Equipment = "Equipment";
    public const string ListType_Consumable = "Consumable";
    public const string ListType_Material = "Material";
    //背包种类
    public const string WeaponBag = "WeaponBag";
    public const string EquipmentBag = "EquipmentBag";
    public const string ConsumableBag = "ConsumableBag";
    public const string MaterialBag = "MaterialBag";
}
