using System.Collections.Generic;
using System.Linq;
using HotFix.GamePlay.BattleModule.DamageModule.Interface;
using HotFix.GamePlay.BattleModule.DamageModule.Module;
using UnityEngine;

namespace HotFix.GamePlay.BattleModule.DamageModule
{
    /// <summary>
    /// 模块化伤害计算器
    /// </summary>
    public class ModularDamageCalculator
    {
        private List<IDamageModule> modules = new List<IDamageModule>();
        public void AddModule(IDamageModule module)
        {
            modules.Add(module);
            modules.Sort((a, b) => a.ExecutionOrder.CompareTo(b.ExecutionOrder));
        }
        
        public void RemoveModule(IDamageModule module)
        {
            modules.Remove(module);
        }
        
        public DamageResult CalculateDamage(EntityBase attacker, EntityBase defender, SkillConfig skill)
        {
            var context = new DamageContext
            {
                Attacker = attacker,
                Defender = defender,
                Skill = skill,
                BaseDamage = 0,
                FinalDamage = 0
            };
        
            // 确定是否暴击
            bool isCritical = DetermineCriticalHit(attacker);
            context.IsCritical = isCritical;
        
            // 按顺序执行所有模块
            foreach (var module in modules)
            {
                module.Process(context);
            }
        
            // 如果暴击，应用暴击伤害乘区
            if (isCritical)
            {
                var critModule = (CritDamageModule)modules.FirstOrDefault(m => m is CritDamageModule);
                if (critModule != null)
                {
                    critModule.ApplyCriticalDamage(context);
                }
            }
        
            // 确保最小伤害为1
            context.FinalDamage = Mathf.Max(1, context.FinalDamage);
        
            return new DamageResult
            {
                Damage = Mathf.RoundToInt(context.FinalDamage),
                IsCritical = isCritical,
                Element = skill.Element
            };
        }
    
        private bool DetermineCriticalHit(EntityBase attacker)
        {
            if (attacker.GetModule<AttributeModule>(EEntityModule.Attribute, out var attribute))
            {
                return UnityEngine.Random.value < attribute.CritRate;
            }
            return false;
        }
        
        public List<IDamageModule> GetModules()
        {
            return new List<IDamageModule>(modules);
        }
    }
}