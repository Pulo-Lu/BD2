using System.ComponentModel;
using Sirenix.OdinInspector;

namespace HotFix.Enums
{
    /// <summary>
    /// 职业
    /// </summary>
    public enum EnumCharacterJob
    {
        /// <summary>
        /// 先锋
        /// </summary>
        [Description("先锋")]
        [LabelText("先锋")]
        Vanguard,
        /// <summary>
        /// 狙击
        /// </summary>
        [Description("狙击")]
        [LabelText("狙击")]
        Sniper,
        /// <summary>
        /// 近卫
        /// </summary>
        [Description("近卫")]
        [LabelText("近卫")]
        Guard,
        /// <summary>
        /// 医疗
        /// </summary>
        [Description("医疗")]
        [LabelText("医疗")]
        Medic,
        /// <summary>
        /// 辅助
        /// </summary>
        [Description("辅助")]
        [LabelText("辅助")]
        Support,
        /// <summary>
        /// 重装
        /// </summary>
        [Description("重装")]
        [LabelText("重装")]
        Defender,
        /// <summary>
        /// 术师
        /// </summary>
        [Description("术师")]
        [LabelText("术师")]
        Caster,
        /// <summary>
        /// 特种
        /// </summary>
        [Description("特种")]
        [LabelText("特种")]
        Specialist
    }

    /// <summary>
    /// 分支
    /// </summary>
    public enum EnumCharacterJobSub
    {
        None,

        #region 先锋 Vanguard
        /// <summary>冲锋手</summary>
        [Description("冲锋手")]
        [LabelText("冲锋手")]
        Charger,
        
        /// <summary>尖兵</summary>
        [Description("尖兵")]
        [LabelText("尖兵")]
        Pioneer,
        
        /// <summary>战术家</summary>
        [Description("战术家")]
        [LabelText("战术家")]
        Tactician,
        
        /// <summary>情报官</summary>
        [Description("情报官")]
        [LabelText("情报官")]
        Agent,
        
        /// <summary>执旗手</summary>
        [Description("执旗手")]
        [LabelText("执旗手")]
        StandardBearer,
        #endregion

        #region 近卫 Guard
        
        /// <summary>斗士</summary>
        [Description("斗士")]
        [LabelText("斗士")]
        Fighter,
        
        /// <summary>剑豪</summary>
        [Description("剑豪")]
        [LabelText("剑豪")]
        Swordmaster,
        
        /// <summary>教官</summary>
        [Description("教官")]
        [LabelText("教官")]
        Instructor,
        
        /// <summary>解放者</summary>
        [Description("解放者")]
        [LabelText("解放者")]
        Liberator,
        
        /// <summary>领主</summary>
        [Description("领主")]
        [LabelText("领主")]
        Lord,
        
        /// <summary>强攻手</summary>
        [Description("强攻手")]
        [LabelText("强攻手")]
        Centurion,
        
        /// <summary>收割者</summary>
        [Description("收割者")]
        [LabelText("收割者")]
        Reaper,
        
        /// <summary>术战者</summary>
        [Description("术战者")]
        [LabelText("术战者")]
        ArtsFighter,
        
        /// <summary>无畏者</summary>
        [Description("无畏者")]
        [LabelText("无畏者")]
        Dreadnought,
        
        /// <summary>重剑手</summary>
        [Description("重剑手")]
        [LabelText("重剑手")]
        Crusher,
        
        /// <summary>撼地者</summary>
        [Description("撼地者")]
        [LabelText("撼地者")]
        Earthshaker,
        
        /// <summary>武者</summary>
        [Description("武者")]
        [LabelText("武者")]
        Soloblade,
        
        #endregion

        #region 重装 Defender
        
        /// <summary>不屈者</summary>
        [Description("不屈者")]
        [LabelText("不屈者")]
        JuggernautDefender,

        /// <summary>决战者</summary>
        [Description("决战者")]
        [LabelText("决战者")]
        Duelist,

        /// <summary>守护者</summary>
        [Description("守护者")]
        [LabelText("守护者")]
        Guardian,

        /// <summary>铁卫</summary>
        [Description("铁卫")]
        [LabelText("铁卫")]
        Protector,

        /// <summary>要塞</summary>
        [Description("要塞")]
        [LabelText("要塞")]
        Fortress,

        /// <summary>哨戒铁卫</summary>
        [Description("哨戒铁卫")]
        [LabelText("哨戒铁卫")]
        SentryProtector,

        /// <summary>御法铁卫</summary>
        [Description("御法铁卫")]
        [LabelText("御法铁卫")]
        ArtsProtector,
        
        #endregion

        #region 狙击 Sniper
        /// <summary>攻城手</summary>
        [Description("攻城手")]
        [LabelText("攻城手")]
        Besieger,

        /// <summary>炮手</summary>
        [Description("炮手")]
        [LabelText("炮手")]
        Artilleryman,

        /// <summary>散射手</summary>
        [Description("散射手")]
        [LabelText("散射手")]
        Spreadshooter,

        /// <summary>神射手</summary>
        [Description("神射手")]
        [LabelText("神射手")]
        Deadeye,

        /// <summary>速射手</summary>
        [Description("速射手")]
        [LabelText("速射手")]
        Marksman,

        /// <summary>投掷手</summary>
        [Description("投掷手")]
        [LabelText("投掷手")]
        Flinger,

        /// <summary>猎手</summary>
        [Description("猎手")]
        [LabelText("猎手")]
        Hunter,

        /// <summary>回环射手</summary>
        [Description("回环射手")]
        [LabelText("回环射手")]
        Loopshooter,

        /// <summary>重射手</summary>
        [Description("重射手")]
        [LabelText("重射手")]
        Heavyshooter,
        #endregion

        #region 术师 Caster
        /// <summary>中坚术师</summary>
        [Description("中坚术师")]
        [LabelText("中坚术师")]
        CoreCaster,

        /// <summary>扩散术师</summary>
        [Description("扩散术师")]
        [LabelText("扩散术师")]
        SplashCaster,

        /// <summary>链术师</summary>
        [Description("链术师")]
        [LabelText("链术师")]
        ChainCaster,

        /// <summary>秘术师</summary>
        [Description("秘术师")]
        [LabelText("秘术师")]
        MysticCaster,

        /// <summary>御械术师</summary>
        [Description("御械术师")]
        [LabelText("御械术师")]
        MechAccordCaster,

        /// <summary>阵法术师</summary>
        [Description("阵法术师")]
        [LabelText("阵法术师")]
        PhalanxCaster,

        /// <summary>轰击术师</summary>
        [Description("轰击术师")]
        [LabelText("轰击术师")]
        BlastCaster,

        /// <summary>本源术师</summary>
        [Description("本源术师")]
        [LabelText("本源术师")]
        PrimalCaster,
        #endregion

        #region 医疗 Medic
        /// <summary>行医</summary>
        [Description("行医")]
        [LabelText("行医")]
        WanderingMedic,

        /// <summary>疗养师</summary>
        [Description("疗养师")]
        [LabelText("疗养师")]
        Therapist,

        /// <summary>群愈师</summary>
        [Description("群愈师")]
        [LabelText("群愈师")]
        MultiTargetMedic,

        /// <summary>咒愈师</summary>
        [Description("咒愈师")]
        [LabelText("咒愈师")]
        IncantationMedic,

        /// <summary>链愈师</summary>
        [Description("链愈师")]
        [LabelText("链愈师")]
        ChainMedic,

        /// <summary>医师</summary>
        [Description("医师")]
        [LabelText("医师")]
        Medic,
        #endregion

        #region 辅助 Supporter
        /// <summary>工匠</summary>
        [Description("工匠")]
        [LabelText("工匠")]
        Artificer,

        /// <summary>护佑者</summary>
        [Description("护佑者")]
        [LabelText("护佑者")]
        Abjurer,

        /// <summary>凝滞师</summary>
        [Description("凝滞师")]
        [LabelText("凝滞师")]
        DecelBinder,

        /// <summary>削弱者</summary>
        [Description("削弱者")]
        [LabelText("削弱者")]
        Hexer,

        /// <summary>吟游者</summary>
        [Description("吟游者")]
        [LabelText("吟游者")]
        Bard,

        /// <summary>巫役</summary>
        [Description("巫役")]
        [LabelText("巫役")]
        Ritualist,

        /// <summary>召唤师</summary>
        [Description("召唤师")]
        [LabelText("召唤师")]
        Summoner,
        #endregion

        #region 特种 Specialist
        /// <summary>处决者</summary>
        [Description("处决者")]
        [LabelText("处决者")]
        Executor,

        /// <summary>伏击客</summary>
        [Description("伏击客")]
        [LabelText("伏击客")]
        Stalker,

        /// <summary>钩锁师</summary>
        [Description("钩锁师")]
        [LabelText("钩锁师")]
        Hookmaster,

        /// <summary>怪杰</summary>
        [Description("怪杰")]
        [LabelText("怪杰")]
        Geek,

        /// <summary>傀儡师</summary>
        [Description("傀儡师")]
        [LabelText("傀儡师")]
        Dollkeeper,

        /// <summary>行商</summary>
        [Description("行商")]
        [LabelText("行商")]
        Merchant,

        /// <summary>推击手</summary>
        [Description("推击手")]
        [LabelText("推击手")]
        PushStroker,

        /// <summary>陷阱师</summary>
        [Description("陷阱师")]
        [LabelText("陷阱师")]
        Trapmaster
        #endregion
    }

}