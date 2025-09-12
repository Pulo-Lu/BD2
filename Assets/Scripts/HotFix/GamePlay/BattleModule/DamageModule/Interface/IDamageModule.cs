using System.Collections.Generic;

namespace HotFix.GamePlay.BattleModule.DamageModule.Interface
{
    /// <summary>
    /// 伤害计算模块接口
    /// </summary>
    public interface IDamageModule
    {
        string ModuleName { get; }
        int ExecutionOrder { get; }
        void Process(DamageContext context);
    }
    
    // 伤害结果
    public struct DamageResult
    {
        public int Damage;
        public bool IsCritical;
        public bool IsMiss;
        public ElementType Element;
    }
    
    /// <summary>
    /// 伤害计算上下文
    /// </summary>
    public class DamageContext
    {
        /// <summary>
        /// 攻击方
        /// </summary>
        public EntityBase Attacker { get; set; }
        /// <summary>
        /// 被攻击方
        /// </summary>
        public EntityBase Defender { get; set; }
        /// <summary>
        /// 技能
        /// </summary>
        public SkillConfig Skill { get; set; }
        /// <summary>
        /// 暴击
        /// </summary>
        public bool IsCritical { get; set; } 
        /// <summary>
        /// 闪避
        /// </summary>
        public bool IsMiss { get; set; }
        /// <summary>
        /// 基础伤害
        /// </summary>
        public float BaseDamage { get; set; }
        /// <summary>
        /// 最终伤害
        /// </summary>
        public float FinalDamage { get; set; }
    
        // 存储中间计算值
        public Dictionary<string, float> IntermediateValues { get; } = new Dictionary<string, float>();
    }
}