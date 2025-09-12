using HotFix.GamePlay.BattleModule.DamageModule.Interface;

namespace HotFix.GamePlay.BattleModule.DamageModule.Module
{
    /// <summary>
    /// 连锁数模块
    /// </summary>
    public class ComboModule : IDamageModule
    {
        public string ModuleName => "Combo";
        public int ExecutionOrder => 800;

        public void Process(DamageContext context)
        {
            if (context.Attacker.GetModule(EEntityModule.Attribute, out AttributeModule attackerAttr))
            {
                // float comboBonus = 1 + (attackerAttr.ComboCount * 0.1f);
                //
                // context.FinalDamage *= comboBonus;
                // context.IntermediateValues["AfterCombo"] = context.FinalDamage;
            }
        }
    }
}