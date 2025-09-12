using HotFix.GamePlay.BattleModule.DamageModule.Interface;

namespace HotFix.GamePlay.BattleModule.DamageModule.Module
{
    /// <summary>
    /// 暴击伤害模块
    /// </summary>
    public class CritDamageModule : IDamageModule
    {
        public string ModuleName => "CritDamage";
        public int ExecutionOrder => 200;

        public void Process(DamageContext context)
        {
            // 正常流程中不处理暴击，只在确定暴击后调用ApplyCriticalDamage
            context.IntermediateValues["AfterCritDamage"] = context.FinalDamage;
        }
        
        public void ApplyCriticalDamage(DamageContext context)
        {
            if (context.Attacker.GetModule(EEntityModule.Attribute, out AttributeModule attackerAttr))
            {
                float critMultiplier = 1 + attackerAttr.CritValuePtt;
                
                context.FinalDamage *= critMultiplier;
                context.IntermediateValues["AfterCritDamage"] = context.FinalDamage;
            }
        }
    }
}