using HotFix.GamePlay.BattleModule.DamageModule.Interface;

namespace HotFix.GamePlay.BattleModule.DamageModule.Module
{
    /// <summary>
    /// 属性抵抗模块
    /// </summary>
    public class ElementalResistanceModule : IDamageModule
    {
        public string ModuleName => "ElementalResistance";
        public int ExecutionOrder => 900;
    
        public void Process(DamageContext context)
        {
            if (context.Defender.GetModule(EEntityModule.Status, out StatusModule defenderStatus))
            {
                float elementalResistance = defenderStatus?.GetElementalResistance(context.Skill.Element) ?? 0;
        
                context.FinalDamage *= (1 - elementalResistance);
                context.IntermediateValues["AfterElementalResistance"] = context.FinalDamage;
            }
        }
    }
}