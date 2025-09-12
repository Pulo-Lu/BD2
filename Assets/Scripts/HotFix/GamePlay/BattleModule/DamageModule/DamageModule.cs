using System;
using System.Collections.Generic;
using HotFix.GamePlay.BattleModule.DamageModule.Interface;
using HotFix.GamePlay.BattleModule.DamageModule.Module;

namespace HotFix.GamePlay.BattleModule.DamageModule
{
    public class DamageModule : BaseEntityModule
    {
        private ModularDamageCalculator calculator;

        public static event Action<DamageContext> OnDamageCalculated;

        protected override void OnInit()
        {
            calculator = new ModularDamageCalculator();

            // 注册所有伤害计算模块
            calculator.AddModule(new BaseValueAndBonusModule());
            // calculator.AddModule(new CritDamageModule());
            // calculator.AddModule(new VulnerabilityModule());
            calculator.AddModule(new DefenseReductionModule());
            // calculator.AddModule(new ElementalDamageModule());
            // calculator.AddModule(new DamageReductionModule());
            // calculator.AddModule(new ComboModule());
            // calculator.AddModule(new ElementalResistanceModule());
            // calculator.AddModule(new WeaknessModule());
        }

        protected override void OnRecycle()
        {
        }

        protected override void OnReset()
        {
        }

        public DamageResult CalculateDamage(EntityBase attacker, EntityBase defender, SkillConfig skill)
        {
            var result = calculator.CalculateDamage(attacker, defender, skill);

            // 触发事件
            //OnDamageCalculated?.Invoke(calculator.GetLastContext());

            return result;
        }

        // 动态添加/移除模块的方法
        public void AddDamageModule(IDamageModule module)
        {
            calculator.AddModule(module);
        }

        public void RemoveDamageModule(IDamageModule module)
        {
            calculator.RemoveModule(module);
        }

        // 获取所有已注册模块
        public List<IDamageModule> GetRegisteredModules()
        {
            // 使用反射获取所有模块（实际实现可能需要调整）
            return calculator.GetModules();
        }
    }
}