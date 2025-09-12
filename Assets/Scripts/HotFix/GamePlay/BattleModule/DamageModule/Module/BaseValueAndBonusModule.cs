using HotFix.GamePlay.BattleModule.DamageModule.Interface;


namespace HotFix.GamePlay.BattleModule.DamageModule.Module
{
    /// <summary>
    /// 基础值与加成计算模块
    /// </summary>
    public class BaseValueAndBonusModule : IDamageModule
    {
        public string ModuleName => "BaseValueAndBonus";
        public int ExecutionOrder => 100;

        public void Process(DamageContext context)
        {
            if (context.Attacker.GetModule(EEntityModule.Attribute, out AttributeModule attackerAttr))
            {
                // 根据攻击类型选择基础值
                float baseValue;
                float damageBonus;
                float bonus;
                
                if (context.Skill.AttackType == AttackType.Physical)
                {
                    baseValue = attackerAttr.Attack;
                    bonus = 1 + GameLogic.PttToFloat(attackerAttr.AttackValuePtt);
                    //物理技能倍率
                    damageBonus = context.Skill.PhysicalMultiplier;
                }
                else
                {
                    baseValue = attackerAttr.Magic;
                    bonus = 1 + GameLogic.PttToFloat(attackerAttr.MagicValuePtt);
                    //魔法技能倍率
                    damageBonus = context.Skill.MagicalMultiplier;
                }
                
                // 应用伤害加成
                float damage = baseValue * bonus * damageBonus;
                
                context.BaseDamage = damage;
                context.FinalDamage = damage;
                context.IntermediateValues["BaseValueAndBonus"] = context.FinalDamage;
            }
        }
    }
}