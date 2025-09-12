using HotFix.GamePlay.BattleModule.DamageModule.Interface;

namespace HotFix.GamePlay.BattleModule.DamageModule.Module
{
    /// <summary>
    /// 减伤模块
    /// </summary>
    public class DamageReductionModule : IDamageModule
    {
        public string ModuleName => "DamageReduction";
        public int ExecutionOrder => 600;
    
        public void Process(DamageContext context)
        {
            
            if (context.Defender.GetModule(EEntityModule.Status, out StatusModule defenderStatus))
            {
                // var defenderStatus = context.Defender.GetModule<StatusModule>();
                // float damageReduction = defenderStatus?.GetDamageReduction() ?? 0;
                //
                // context.FinalDamage *= (1 - damageReduction);
                // context.IntermediateValues["AfterDamageReduction"] = context.FinalDamage;
            }
        }
    }
}