using System.Collections.Generic;
using System.Linq;

namespace HotFix.GamePlay.BattleModule
{
    /// <summary>
    /// 状态模块
    /// </summary>
    public class StatusModule : BaseEntityModule
    {
        private List<StatusEffect> activeStatuses = new List<StatusEffect>();

        /// <summary>
        /// 获取易伤效果
        /// </summary>
        /// <returns></returns>
        public float GetVulnerability()
        {
            return activeStatuses.Where(s => s.Type == StatusType.Vulnerability)
                .Sum(s => s.Value);
        }

        /// <summary>
        /// 获取增伤效果
        /// </summary>
        /// <returns></returns>
        public float GetDamageIncrease()
        {
            return activeStatuses.Where(s => s.Type == StatusType.DamageIncrease)
                .Sum(s => s.Value);
        }

        /// <summary>
        /// 获取减伤效果
        /// </summary>
        /// <returns></returns>
        public float GetDamageReduction()
        {
            return activeStatuses.Where(s => s.Type == StatusType.DamageReduction)
                .Sum(s => s.Value);
        }

        /// <summary>
        /// 获取元素抗性
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public float GetElementalResistance(ElementType element)
        {
            return activeStatuses.Where(s => s.Type == StatusType.ElementalResistance && s.Element == element)
                .Sum(s => s.Value);
        }

        /// <summary>
        /// 检查是否对某元素弱点
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool IsWeakTo(ElementType element)
        {
            return activeStatuses.Any(s => s.Type == StatusType.Weakness && s.Element == element);
        }

        /// <summary>
        /// 添加状态效果
        /// </summary>
        /// <param name="status"></param>
        public void AddStatus(StatusEffect status)
        {
            activeStatuses.Add(status);
        }

        /// <summary>
        /// 移除状态效果
        /// </summary>
        /// <param name="status"></param>
        public void RemoveStatus(StatusEffect status)
        {
            activeStatuses.Remove(status);
        }

        /// <summary>
        /// 回合结束时更新状态持续时间
        /// </summary>
        public void OnTurnEnd()
        {
            for (int i = activeStatuses.Count - 1; i >= 0; i--)
            {
                activeStatuses[i].Duration--;
                if (activeStatuses[i].Duration <= 0)
                {
                    activeStatuses.RemoveAt(i);
                }
            }
        }

        protected override void OnInit()
        {
        }

        protected override void OnRecycle()
        {
        }

        protected override void OnReset()
        {
        }
    }

    public enum ElementType
    {
        Fire,       // 火
        Water,      // 水
        Light,      // 光
        Dark,       // 暗
        Wind        // 风
    }

    public class StatusEffect
    {
        public StatusType Type;
        public ElementType Element;
        public float Value;
        public int Duration;
    }

    public enum StatusType
    {
        Vulnerability, // 易伤
        DamageIncrease, // 增伤
        DamageReduction, // 减伤
        ElementalResistance, // 元素抗性
        Weakness, // 弱点
    }
}