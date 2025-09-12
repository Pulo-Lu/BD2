using UnityEngine;

namespace HotFix.GamePlay.BattleModule
{
    [CreateAssetMenu(fileName = "NewSkill", menuName = "Game/Skill")]
    public class SkillConfig : ScriptableObject
    {
        public int ID;
        public string Name;
        public string Description;
        public AttackType AttackType;//攻击类型
        public ElementType Element;//元素类型
        // 技能倍率
        public float PhysicalMultiplier = 1.0f; // 物理倍率
        public float MagicalMultiplier = 1.0f;  // 魔法倍率
        public float ElementalMultiplier = 1.0f; // 元素倍率
        
        
        public int Cooldown;
        public float CastTime;
        public Sprite Icon;
        public SkillEffect[] Effects;
    }
    
    [System.Serializable]
    public struct SkillEffect
    {
        public SkillEffectType Type;
        public StatusType StatusType; // 状态类型
        public ElementType Element;   // 元素类型
        public float Value;           // 效果值
        public int Duration;          // 持续时间
        public int SP;                // SP消耗
    }

    public enum SkillEffectType
    {
        Damage,
        Heal,
        Status,
        // 其他效果类型...
    }
    
 

    public enum AttackType
    {
        Physical,   // 物理攻击
        Magical     // 魔法攻击
    }
}