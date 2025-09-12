using HotFix.GamePlay.BattleModule.DamageModule.Interface;

namespace HotFix.GamePlay.BattleModule.DamageModule.Module
{
    /// <summary>
    /// 属性伤害模块
    /// </summary>
    public class ElementalDamageModule : IDamageModule
    {
        public string ModuleName => "ElementalDamage";
        public int ExecutionOrder => 600;
    
        public void Process(DamageContext context)
        {
            if (context.Defender.GetModule(EEntityModule.Attribute, out AttributeModule attackerAttr))
            {
                // float elementalBonus = 1 + attackerAttr.ElementalDamage;
                //
                // context.FinalDamage *= elementalBonus;
                // context.IntermediateValues["AfterElementalDamage"] = context.FinalDamage;
            }
        }
    }
}