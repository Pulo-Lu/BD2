using HotFix.GamePlay.BattleModule.DamageModule.Interface;

namespace HotFix.GamePlay.BattleModule.DamageModule.Module
{
    /// <summary>
    /// 防御/魔抗 减伤模块
    /// </summary>
    public class DefenseReductionModule : IDamageModule
    {
        public string ModuleName => "DefenseReduction";
        public int ExecutionOrder => 500;
        
        public void Process(DamageContext context)
        {
            
            if (context.Defender.GetModule(EEntityModule.Attribute, out AttributeModule defenderAttr))
            {
                if (context.Skill.AttackType == AttackType.Physical)
                {
                    //防御
                    context.FinalDamage *= (1 - GameLogic.PttToFloat(defenderAttr.AttackDefense));
                    context.IntermediateValues["AfterDefenseReduction"] = context.FinalDamage;
                }
                else
                {
                    //魔抗
                    context.FinalDamage *= (1 - GameLogic.PttToFloat(defenderAttr.MagicDefense));
                    context.IntermediateValues["AfterDefenseReduction"] = context.FinalDamage;
                }
            }
        }
    }
}