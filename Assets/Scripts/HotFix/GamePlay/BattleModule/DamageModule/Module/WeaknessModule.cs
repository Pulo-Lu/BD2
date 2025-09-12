using HotFix.GamePlay.BattleModule.DamageModule.Interface;

namespace HotFix.GamePlay.BattleModule.DamageModule.Module
{
    public class WeaknessModule: IDamageModule
    {
        public string ModuleName => "Weakness";
        public int ExecutionOrder => 1000;
    
        public void Process(DamageContext context)
        {
            if (context.Defender.GetModule(EEntityModule.Status, out StatusModule defenderStatus))
            {
                bool isWeak = defenderStatus?.IsWeakTo(context.Skill.Element) ?? false;
                float weaknessBonus = isWeak ? 1.5f : 1f;

                context.FinalDamage *= weaknessBonus;
                context.IntermediateValues["AfterWeakness"] = context.FinalDamage;
            }
        }
    }
}