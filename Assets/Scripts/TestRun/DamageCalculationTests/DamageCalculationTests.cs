using HotFix.GamePlay;
using HotFix.GamePlay.BattleModule;
using HotFix.GamePlay.BattleModule.DamageModule;
using HotFix.GamePlay.BattleModule.DamageModule.Module;
using NUnit.Framework;
using UnityEngine;

namespace TestRun.DamageCalculationTests
{
    [TestFixture]
    public class DamageCalculationTests
    {
        private GameObject attackerGameObject;
        private GameObject defenderGameObject;
        private EntityBase attacker;
        private EntityBase defender;
        private SkillConfig physicalSkill;
        private SkillConfig magicSkill;
        
        [SetUp]
        public void Setup()
        {
            // 创建攻击者和防御者实体
            attackerGameObject = new GameObject("Attacker");
            defenderGameObject = new GameObject("Defender");
            attacker = attackerGameObject.AddComponent<TestEntity>();
            defender = defenderGameObject.AddComponent<TestEntity>();
            
            var damageModule = (DamageModule)attacker.AddModule(EEntityModule.Damage);
            
            // 添加属性模块
            var attackerAttr =  (AttributeModule)attacker.AddModule(EEntityModule.Attribute);
            var defenderAttr = (AttributeModule)defender.AddModule(EEntityModule.Attribute);
        
            // 设置基本属性
            attackerAttr.ModifyAttributeByType(AttributeType.Attack,100);
            attackerAttr.ModifyAttributeByType(AttributeType.Magic,200);
            
            defenderAttr.ModifyAttributeByType(AttributeType.HPMax,50);
            
            // 创建测试技能
            physicalSkill = ScriptableObject.CreateInstance<SkillConfig>();
            physicalSkill.ID = 1;
            physicalSkill.Name = "Test Skill";
            physicalSkill.AttackType = AttackType.Physical;
            physicalSkill.PhysicalMultiplier = 1.5f;
            physicalSkill.Element = ElementType.Fire;
            
            // 创建测试技能
            magicSkill = ScriptableObject.CreateInstance<SkillConfig>();
            magicSkill.ID = 2;
            magicSkill.Name = "Test Skill2";
            magicSkill.AttackType = AttackType.Magical;
            magicSkill.MagicalMultiplier = 2f;
            magicSkill.Element = ElementType.Water;
        }
    
        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(attackerGameObject);
            Object.DestroyImmediate(defenderGameObject);
        }
        

        // 测试基础值与加成计算模块
        #region BaseValueAndBonusModule

        /// <summary>
        /// 倍率加成测试
        /// </summary>
        [Test]
        public void BaseValueAndBonusModule_CalculatesCorrectly()
        {
            // 移除所有已注册的模块，只保留BaseValueAndBonusModule进行测试
            if (attacker.GetModule(EEntityModule.Damage, out DamageModule damageModule))
            {
                // 获取当前所有模块
                var modules = damageModule.GetRegisteredModules();
        
                // 移除除了BaseValueAndBonusModule之外的所有模块
                foreach (var module in modules)
                {
                    if (!(module is BaseValueAndBonusModule))
                    {
                        damageModule.RemoveDamageModule(module);
                    }
                }
        
                // 确保防御属性为0，避免防御减伤影响测试
                if (defender.GetModule(EEntityModule.Attribute, out AttributeModule defenderAttr))
                {
                    defenderAttr.ModifyAttributeByType(AttributeType.AttackDefense, 0);
                    defenderAttr.ModifyAttributeByType(AttributeType.MagicDefense, 0);
                }
        
                // 测试物理攻击
                var physicalResult = damageModule.CalculateDamage(attacker, defender, physicalSkill);
                // 期望值: 100(攻击) * 1.5(物理倍率) = 150
                Assert.AreEqual(150, physicalResult.Damage);
        
                // 测试魔法攻击
                var magicalResult = damageModule.CalculateDamage(attacker, defender, magicSkill);
                // 期望值: 200(魔法) * 2.0(魔法倍率) = 400
                Assert.AreEqual(400, magicalResult.Damage);
            }
        }
        
        /// <summary>
        /// 百分比加成测试
        /// </summary>
        [Test]
        public void BaseValueAndBonusModule_WithPercentageBonus_CalculatesCorrectly()
        {
            // 设置攻击和魔法百分比加成
            if (attacker.GetModule(EEntityModule.Attribute, out AttributeModule attackerAttr))
            {
                attackerAttr.ModifyAttributeByType(AttributeType.AttackPtt, 2000); // 20%攻击加成
                attackerAttr.ModifyAttributeByType(AttributeType.MagicPtt, 3000); // 30%魔法加成
        
                // 确保防御属性为0，避免防御减伤影响测试
                if (defender.GetModule(EEntityModule.Attribute, out AttributeModule defenderAttr))
                {
                    defenderAttr.ModifyAttributeByType(AttributeType.AttackDefense, 0);
                    defenderAttr.ModifyAttributeByType(AttributeType.MagicDefense, 0);
                }
        
                // 移除所有已注册的模块，只保留BaseValueAndBonusModule进行测试
                if (attacker.GetModule(EEntityModule.Damage, out DamageModule damageModule))
                {
                    // 获取当前所有模块
                    var modules = damageModule.GetRegisteredModules();
            
                    // 移除除了BaseValueAndBonusModule之外的所有模块
                    foreach (var module in modules)
                    {
                        if (!(module is BaseValueAndBonusModule))
                        {
                            damageModule.RemoveDamageModule(module);
                        }
                    }
            
                    // 测试物理攻击（包含百分比加成）
                    var physicalResult = damageModule.CalculateDamage(attacker, defender, physicalSkill);
                    // 期望值: 100(攻击) * (1 + 0.2)(攻击加成) * 1.5(物理倍率) = 180
                    Assert.AreEqual(180, physicalResult.Damage, "验证 物理攻击百分比加成失败");
            
                    // 测试魔法攻击（包含百分比加成）
                    var magicalResult = damageModule.CalculateDamage(attacker, defender, magicSkill);
                    // 期望值: 200(魔法) * (1 + 0.3)(魔法加成) * 2.0(魔法倍率) = 520
                    Assert.AreEqual(520, magicalResult.Damage, "验证 魔法攻击百分比加成失败");
                }
            }
        }
        
        /// <summary>
        /// 负百分比加成测试
        /// </summary>
        [Test]
        public void BaseValueAndBonusModule_WithNegativePercentageBonus_CalculatesCorrectly()
        {
            // 设置负的攻击和魔法百分比加成
            if (attacker.GetModule(EEntityModule.Attribute, out AttributeModule attackerAttr))
            {
                attackerAttr.ModifyAttributeByType(AttributeType.AttackPtt, -2000); // -20%攻击加成
                attackerAttr.ModifyAttributeByType(AttributeType.MagicPtt, -3000); // -30%魔法加成
        
                // 确保防御属性为0，避免防御减伤影响测试
                if (defender.GetModule(EEntityModule.Attribute, out AttributeModule defenderAttr))
                {
                    defenderAttr.ModifyAttributeByType(AttributeType.AttackDefense, 0);
                    defenderAttr.ModifyAttributeByType(AttributeType.MagicDefense, 0);
                }
        
                // 移除所有已注册的模块，只保留BaseValueAndBonusModule进行测试
                if (attacker.GetModule(EEntityModule.Damage, out DamageModule damageModule))
                {
                    // 获取当前所有模块
                    var modules = damageModule.GetRegisteredModules();
            
                    // 移除除了BaseValueAndBonusModule之外的所有模块
                    foreach (var module in modules)
                    {
                        if (!(module is BaseValueAndBonusModule))
                        {
                            damageModule.RemoveDamageModule(module);
                        }
                    }
            
                    // 测试物理攻击（负百分比加成）
                    var physicalResult = damageModule.CalculateDamage(attacker, defender, physicalSkill);
                    // 期望值: 100(攻击) * (1 - 0.2)(攻击加成) * 1.5(物理倍率) = 120
                    Assert.AreEqual(120, physicalResult.Damage, "验证 物理攻击负百分比加成失败");
            
                    // 测试魔法攻击（负百分比加成）
                    var magicalResult = damageModule.CalculateDamage(attacker, defender, magicSkill);
                    // 期望值: 200(魔法) * (1 - 0.3)(魔法加成) * 2.0(魔法倍率) = 280
                    Assert.AreEqual(280, magicalResult.Damage, "验证 魔法攻击负百分比加成失败");
                }
            }
        }
        
        /// <summary>
        /// 极端负百分比加成，确保不会出现负伤害
        /// </summary>
        [Test]
        public void BaseValueAndBonusModule_WithExtremeNegativePercentageBonus_CalculatesCorrectly()
        {
            // 设置极端的负攻击百分比加成
            if (attacker.GetModule(EEntityModule.Attribute, out AttributeModule attackerAttr))
            {
                attackerAttr.ModifyAttributeByType(AttributeType.AttackPtt, -15000); // -150%攻击加成
                attackerAttr.ModifyAttributeByType(AttributeType.MagicPtt, -20000); // -200%魔法加成
        
                // 确保防御属性为0，避免防御减伤影响测试
                if (defender.GetModule(EEntityModule.Attribute, out AttributeModule defenderAttr))
                {
                    defenderAttr.ModifyAttributeByType(AttributeType.AttackDefense, 0);
                    defenderAttr.ModifyAttributeByType(AttributeType.MagicDefense, 0);
                }
        
                // 移除所有已注册的模块，只保留BaseValueAndBonusModule进行测试
                if (attacker.GetModule(EEntityModule.Damage, out DamageModule damageModule))
                {
                    // 获取当前所有模块
                    var modules = damageModule.GetRegisteredModules();
            
                    // 移除除了BaseValueAndBonusModule之外的所有模块
                    foreach (var module in modules)
                    {
                        if (!(module is BaseValueAndBonusModule))
                        {
                            damageModule.RemoveDamageModule(module);
                        }
                    }
            
                    // 测试物理攻击（极端负百分比加成）
                    var physicalResult = damageModule.CalculateDamage(attacker, defender, physicalSkill);
                    // 期望值: 100(攻击) * (1 - 1.5)(攻击加成) * 1.5(物理倍率) = -75
                    // 但由于ModularDamageCalculator中有Mathf.Max(1, context.FinalDamage)，所以最终伤害应为1
                    Assert.AreEqual(1, physicalResult.Damage, "物理攻击极端负百分比加成测试失败");
                    
                    // 测试魔法攻击（极端负百分比加成）
                    var magicalResult = damageModule.CalculateDamage(attacker, defender, magicSkill);
                    // 期望值: 200(魔法) * (1 - 2)(魔法加成) * 2(魔法倍率) = -400
                    // 但由于ModularDamageCalculator中有Mathf.Max(1, context.FinalDamage)，所以最终伤害应为1
                    Assert.AreEqual(1, magicalResult.Damage, "魔法攻击极端负百分比加成测试失败");
                }
            }
        }
        
        /// <summary>
        /// 零基础值，负百分比加成
        /// </summary>
        [Test]
        public void BaseValueAndBonusModule_WithZeroBaseValueAndNegativeBonus_CalculatesCorrectly()
        {
            // 设置基础攻击和魔法为0
            if (attacker.GetModule(EEntityModule.Attribute, out AttributeModule attackerAttr))
            {
                attackerAttr.ModifyAttributeByType(AttributeType.Attack, -100);
                attackerAttr.ModifyAttributeByType(AttributeType.Magic, -200);
                
                attackerAttr.ModifyAttributeByType(AttributeType.AttackPtt, -5000); // -50%攻击加成
                attackerAttr.ModifyAttributeByType(AttributeType.MagicPtt, -6000); // -60%魔法加成
        
                // 确保防御属性为0，避免防御减伤影响测试
                if (defender.GetModule(EEntityModule.Attribute, out AttributeModule defenderAttr))
                {
                    defenderAttr.ModifyAttributeByType(AttributeType.AttackDefense, 0);
                    defenderAttr.ModifyAttributeByType(AttributeType.MagicDefense, 0);
                }
        
                // 移除所有已注册的模块，只保留BaseValueAndBonusModule进行测试
                if (attacker.GetModule(EEntityModule.Damage, out DamageModule damageModule))
                {
                    // 获取当前所有模块
                    var modules = damageModule.GetRegisteredModules();
            
                    // 移除除了BaseValueAndBonusModule之外的所有模块
                    foreach (var module in modules)
                    {
                        if (!(module is BaseValueAndBonusModule))
                        {
                            damageModule.RemoveDamageModule(module);
                        }
                    }
            
                    // 测试物理攻击（零基础值，负百分比加成）
                    var physicalResult = damageModule.CalculateDamage(attacker, defender, physicalSkill);
                    // 期望值: 0(攻击) * (1 - 0.5)(攻击加成) * 1.5(物理倍率) = 0
                    // 但由于ModularDamageCalculator中有Mathf.Max(1, context.FinalDamage)，所以最终伤害应为1
                    Assert.AreEqual(1, physicalResult.Damage, "验证 物理攻击零基础值，负百分比加成失败");
                    
                    // 测试魔法攻击（零基础值，负百分比加成）
                    var magicalResult = damageModule.CalculateDamage(attacker, defender, physicalSkill);
                    // 期望值: 0(魔法) * (1 - 0.6)(攻击加成) * 2(魔法倍率) = 0
                    // 但由于ModularDamageCalculator中有Mathf.Max(1, context.FinalDamage)，所以最终伤害应为1
                    Assert.AreEqual(1, magicalResult.Damage, "验证 魔法攻击零基础值，负百分比加成失败");
                }
            }
        }
   
        #endregion
        
        // 测试防御/魔抗 减伤模块
        #region DefenseReductionModule
        
        /// <summary>
        /// 测试防御/魔抗
        /// </summary>
        [Test]
        public void DefenseReductionModule_PhysicalDefense_CalculatesCorrectly()
        {
            if (attacker.GetModule(EEntityModule.Damage, out DamageModule damageModule))
            {
                if (defender.GetModule(EEntityModule.Attribute, out AttributeModule defenderAttr))
                {
                    // 30%防御 (3000 = 30%)
                    defenderAttr.ModifyAttributeByType(AttributeType.AttackDefense, 3000);
                    // 60%魔抗 (6000 = 60%)
                    defenderAttr.ModifyAttributeByType(AttributeType.MagicDefense, 6000);
                }
                
                // 测试防御
                var physicalResult = damageModule.CalculateDamage(attacker, defender, physicalSkill);
                // 期望值: 100(攻击) * 1.5(物理倍率) * (1- 0.3) = 105
                Assert.AreEqual(105, physicalResult.Damage);
        
                // 测试魔抗
                var magicalResult = damageModule.CalculateDamage(attacker, defender, magicSkill);
                // 期望值: 200(魔法) * 2.0(魔法倍率) * (1-0.6) = 160
                Assert.AreEqual(160, magicalResult.Damage);
            }
        }
        
        /// <summary>
        /// 测试 0防御/0魔抗
        /// </summary>
        [Test]
        public void DefenseReductionModule_ZeroDefense_NoReduction()
        {
            if (attacker.GetModule(EEntityModule.Damage, out DamageModule damageModule))
            {
                if (defender.GetModule(EEntityModule.Attribute, out AttributeModule defenderAttr))
                {
                    defenderAttr.ModifyAttributeByType(AttributeType.AttackDefense, 0);
                    defenderAttr.ModifyAttributeByType(AttributeType.MagicDefense, 0);
                }
                
                // 测试防御
                var physicalResult = damageModule.CalculateDamage(attacker, defender, physicalSkill);
                // 期望值: 100(攻击) * 1.5(物理倍率) * (1 - 0) = 150
                Assert.AreEqual(150, physicalResult.Damage);
        
                // 测试魔抗
                var magicalResult = damageModule.CalculateDamage(attacker, defender, magicSkill);
                // 期望值: 200(魔法) * 2.0(魔法倍率) * (1 - 0) = 400
                Assert.AreEqual(400, magicalResult.Damage);
            }
        }
        
        /// <summary>
        /// 测试 100防御/100魔抗
        /// </summary>
        [Test]
        public void DefenseReductionModule_MaxDefense_MinimumDamage()
        {
            if (attacker.GetModule(EEntityModule.Damage, out DamageModule damageModule))
            {
                if (defender.GetModule(EEntityModule.Attribute, out AttributeModule defenderAttr))
                {
                    defenderAttr.ModifyAttributeByType(AttributeType.AttackDefense, 10000);
                    defenderAttr.ModifyAttributeByType(AttributeType.MagicDefense, 10000);
                }
                
                // 测试防御
                var physicalResult = damageModule.CalculateDamage(attacker, defender, physicalSkill);
                // 期望值: 100(攻击) * 1.5(物理倍率) * (1 - 1) = 0
                // 但由于ModularDamageCalculator中有Mathf.Max(1, context.FinalDamage)，所以最终伤害应为1
                Assert.AreEqual(1, physicalResult.Damage);
        
                // 测试魔抗
                var magicalResult = damageModule.CalculateDamage(attacker, defender, magicSkill);
                // 期望值: 200(魔法) * 2.0(魔法倍率) * (1 - 1) = 0
                // 但由于ModularDamageCalculator中有Mathf.Max(1, context.FinalDamage)，所以最终伤害应为1
                Assert.AreEqual(1, magicalResult.Damage);
            }
        }
        
        /// <summary>
        /// 测试 120防御/130魔抗
        /// </summary>
        [Test]
        public void DefenseReductionModule_OverMaxDefense_NegativeDamage()
        {
            if (attacker.GetModule(EEntityModule.Damage, out DamageModule damageModule))
            {
                if (defender.GetModule(EEntityModule.Attribute, out AttributeModule defenderAttr))
                {
                    defenderAttr.ModifyAttributeByType(AttributeType.AttackDefense, 12000);
                    defenderAttr.ModifyAttributeByType(AttributeType.MagicDefense, 13000);
                }
                
                // 测试防御
                var physicalResult = damageModule.CalculateDamage(attacker, defender, physicalSkill);
                // 期望值: 100(攻击) * 1.5(物理倍率) * (1 - 1.2) = -30
                // 但由于ModularDamageCalculator中有Mathf.Max(1, context.FinalDamage)，所以最终伤害应为1
                Assert.AreEqual(1, physicalResult.Damage);
        
                // 测试魔抗
                var magicalResult = damageModule.CalculateDamage(attacker, defender, magicSkill);
                // 期望值: 200(魔法) * 2.0(魔法倍率) * (1 - 1.3) = -120
                // 但由于ModularDamageCalculator中有Mathf.Max(1, context.FinalDamage)，所以最终伤害应为1
                Assert.AreEqual(1, magicalResult.Damage);
            }
        }
        
        #endregion
        
        /// <summary>
        /// 集成测试
        /// </summary>
        [Test]
        public void DamageCalculation_BasicAttack_CalculatesCorrectly()
        {
            // Arrange
            if (defender.GetModule(EEntityModule.Attribute, out AttributeModule defenderAttr))
            {
                // 30%防御
                defenderAttr.ModifyAttributeByType(AttributeType.AttackDefense, 3000);
                // 60%魔抗
                defenderAttr.ModifyAttributeByType(AttributeType.MagicDefense, 6000);
            }
            
            
            if (attacker.GetModule(EEntityModule.Damage, out DamageModule damageModule))
            {
                // Act
                var result1 = damageModule.CalculateDamage(attacker, defender, physicalSkill);
                var result2 = damageModule.CalculateDamage(attacker, defender, magicSkill);
                
                // Assert - 基础伤害 = 100(攻击) * 1.5(倍率) * (1 - 30%)(防御) = 105
                Assert.AreEqual(105, result1.Damage);
                // Assert - 基础伤害 = 200(魔法) * 2(倍率) * (1 - 60%)(魔抗) = 160
                Assert.AreEqual(160, result2.Damage);
            }
        }

    }
}