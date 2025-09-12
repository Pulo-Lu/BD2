using System.Collections.Generic;
using HotFix.GamePlay.BattleModule;
using HotFix.GamePlay;
using NUnit.Framework;
using UnityEngine;

namespace TestRun.AttributeModuleTests
{
    [TestFixture]
    public class AttributeModuleTests
    {
        private GameObject entityGameObject;
        private EntityBase entity;
        private AttributeModule attributeModule;

        [SetUp]
        public void Setup()
        {
            entityGameObject = new GameObject("TestEntity");
            entity = entityGameObject.AddComponent<TestEntity>();
            attributeModule = (AttributeModule)entity.AddModule(EEntityModule.Attribute);
        }

        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(entityGameObject);
        }


        #region 测试所有属性

        /// <summary>
        /// 测试所有属性类型的正常增加操作
        /// </summary>
        [Test]
        public void TestAllAttributes_Increase_NormalCase()
        {
            foreach (AttributeType type in System.Enum.GetValues(typeof(AttributeType)))
            {
                // 跳过依赖属性，它们会在联动测试中单独测试
                if (IsDependentAttribute(type)) continue;
                // 获取初始值
                float initialValue = attributeModule.GetValue(type);

                // 增加属性值
                attributeModule.ModifyAttributeByType(type, 1000);

                Debug.Log($"验证 {type}");
                // 验证属性值是否正确增加
                Assert.AreEqual(initialValue + 1000, attributeModule.GetValue(type),
                    $"验证属性值正确增加失败: {type}");
            }
        }

        /// <summary>
        /// 测试所有属性类型的正常减少操作
        /// </summary>
        [Test]
        public void TestAllAttributes_Decrease_NormalCase()
        {
            foreach (AttributeType type in System.Enum.GetValues(typeof(AttributeType)))
            {
                // 跳过依赖属性，它们会在联动测试中单独测试
                if (IsDependentAttribute(type)) continue;

                // 设置一个初始值
                attributeModule.ModifyAttributeByType(type, 1500);

                // 减少属性值
                attributeModule.ModifyAttributeByType(type, -500);

                // 验证属性值是否正确减少
                Assert.AreEqual(1000, attributeModule.GetValue(type),
                    $"验证属性值正确减少失败:  {type}");
            }
        }

        /// <summary>
        /// 测试所有属性类型修改量为0时属性值不变
        /// </summary>
        [Test]
        public void TestAllAttributes_ModifyByZero_ValueRemainsUnchanged()
        {
            foreach (AttributeType type in System.Enum.GetValues(typeof(AttributeType)))
            {
                // 设置一个默认值
                attributeModule.ModifyAttributeByType(type, 300);

                // 修改量为0
                attributeModule.ModifyAttributeByType(type, 0);

                // 验证属性值保持不变
                Assert.AreEqual(300, attributeModule.GetValue(type),
                    $"属性值保持不变: {type}");
            }
        }

        /// <summary>
        /// 测试所有属性类型的连续多次修改
        /// </summary>
        [Test]
        public void TestAllAttributes_MultipleModifications_AccumulatesCorrectly()
        {
            foreach (AttributeType type in System.Enum.GetValues(typeof(AttributeType)))
            {
                // 跳过依赖属性，它们会在联动测试中单独测试
                if (IsDependentAttribute(type)) continue;

                // 设置初始值
                attributeModule.ModifyAttributeByType(type, 1000);

                // 多次修改属性值
                attributeModule.ModifyAttributeByType(type, 500);
                attributeModule.ModifyAttributeByType(type, 300);
                attributeModule.ModifyAttributeByType(type, -200);
                attributeModule.ModifyAttributeByType(type, 400);

                // 验证最终结果
                Assert.AreEqual(2000, attributeModule.GetValue(type),
                    $"验证属性值连续多次修改失败：{type}");
            }
        }

        /// <summary>
        /// 测试所有属性类型的极大值处理
        /// </summary>
        [Test]
        public void TestAllAttributes_ExtremeValues_HandlesCorrectly()
        {
            foreach (AttributeType type in System.Enum.GetValues(typeof(AttributeType)))
            {
                // 跳过依赖属性，它们会在联动测试中单独测试
                if (IsDependentAttribute(type)) continue;

                // 获取初始值
                float initialValue = attributeModule.GetValue(type);

                // 修改为极大正值
                attributeModule.ModifyAttributeByType(type, int.MaxValue);

                // 验证属性值是否正确处理极大值
                float currentValue = attributeModule.GetValue(type);
                Assert.IsTrue(currentValue >= initialValue,
                    $"{type} did not handle extreme positive value correctly");

                // 重置并测试极大负值
                attributeModule.ModifyAttributeByType(type, 10000);
                attributeModule.ModifyAttributeByType(type, int.MinValue);

                // 验证属性值至少为0
                currentValue = attributeModule.GetValue(type);
                Assert.IsTrue(currentValue >= 0,
                    $"{type} did not handle extreme negative value correctly");
            }
        }

        /// <summary>
        /// 测试所有属性类型的小数/分数值修改
        /// </summary>
        [Test]
        public void TestAllAttributes_FractionalValues_HandlesPrecisionCorrectly()
        {
            // foreach (AttributeType type in System.Enum.GetValues(typeof(AttributeType)))
            // {
            //     // 跳过依赖属性，它们会在联动测试中单独测试
            //     if (IsDependentAttribute(type)) continue;
            //
            //     // 设置初始值
            //     attributeModule.ModifyAttributeByType(type, 100.5f);
            //
            //     // 增加一个小数值
            //     attributeModule.ModifyAttributeByType(type, 50.25f);
            //
            //     // 验证结果精度
            //     Assert.AreEqual(150.75f, attributeModule.GetAttributeValue(type), 0.001f,
            //         $"{type} did not handle fractional addition correctly");
            //
            //     // 减少一个小数值
            //     attributeModule.ModifyAttributeByType(type, -25.5f);
            //
            //     // 验证结果精度
            //     Assert.AreEqual(125.25f, attributeModule.GetAttributeValue(type), 0.001f,
            //         $"{type} did not handle fractional subtraction correctly");
            // }
        }

        /// <summary>
        /// 测试属性修改的独立性 - 修改一个属性不会影响其他无关属性
        /// </summary>
        [Test]
        public void TestAllAttributes_ModificationIndependence()
        {
            // 记录所有属性的初始值
            Dictionary<AttributeType, int> initialValues = new Dictionary<AttributeType, int>();
            foreach (AttributeType type in System.Enum.GetValues(typeof(AttributeType)))
            {
                initialValues[type] = attributeModule.GetValue(type);
            }

            // 随机选择一个非依赖属性进行修改
            AttributeType modifiedType = GetNonDependentAttribute();
            attributeModule.ModifyAttributeByType(modifiedType, 1000);

            // 验证只有目标属性被修改，其他无关属性保持不变
            foreach (AttributeType type in System.Enum.GetValues(typeof(AttributeType)))
            {
                if (type == modifiedType)
                {
                    Assert.AreEqual(initialValues[type] + 1000, attributeModule.GetValue(type),
                        $"Modified {type} did not change correctly");
                }
                else if (!HasDependencyRelationship(modifiedType, type))
                {
                    Assert.AreEqual(initialValues[type], attributeModule.GetValue(type),
                        $"Unrelated attribute {type} changed when {modifiedType} was modified");
                }
                // 如果有依赖关系，不进行断言，因为这是预期的联动效果
            }
        }
        
        #endregion

        #region 测试属性联动效果
        
        /// <summary>
        /// 测试属性联动效果 - 当前血量不能超过最大血量
        /// </summary>
        [Test]
        public void TestAttributeLinkage_CurrentHealthCannotExceedMaxHealth()
        {
            // 设置最大血量
            attributeModule.ModifyAttributeByType(AttributeType.HPMax, 1000);

            // 尝试设置当前血量超过最大血量
            attributeModule.ModifyAttributeByType(AttributeType.CurrentHp, 1500);

            // 验证当前血量被限制为最大血量
            Assert.AreEqual(1000, attributeModule.GetValue(AttributeType.CurrentHp),
                "CurrentHealth should not exceed MaxHealth");

            // 增加当前血量超过最大血量
            attributeModule.ModifyAttributeByType(AttributeType.CurrentHp, 500);

            // 验证当前血量仍然被限制为最大血量
            Assert.AreEqual(1000, attributeModule.GetValue(AttributeType.CurrentHp),
                "CurrentHealth should not exceed MaxHealth even when increased");
        }

        /// <summary>
        /// 测试属性联动效果 - 最大血量减少时，当前血量应相应调整
        /// </summary>
        [Test]
        public void TestAttributeLinkage_CurrentHealthAdjustsWhenMaxHealthDecreases()
        {
            // 设置最大血量和当前血量
            attributeModule.ModifyAttributeByType(AttributeType.HPMax, 1000);
            attributeModule.ModifyAttributeByType(AttributeType.CurrentHp, 800);

            // 减少最大血量到低于当前血量
            attributeModule.ModifyAttributeByType(AttributeType.HPMax, -400);

            // 验证当前血量被调整为新的最大血量
            Assert.AreEqual(600, attributeModule.GetValue(AttributeType.CurrentHp),
                "CurrentHealth should adjust to new MaxHealth when MaxHealth decreases below CurrentHealth");
        }

        /// <summary>
        /// 测试属性联动效果 - 最大血量增加时，当前血量保持不变
        /// </summary>
        [Test]
        public void TestAttributeLinkage_CurrentHealthUnaffectedWhenMaxHealthIncreases()
        {
            // 设置最大血量和当前血量
            attributeModule.ModifyAttributeByType(AttributeType.HPMax, 1000);
            attributeModule.ModifyAttributeByType(AttributeType.CurrentHp, -200);

            // 增加最大血量
            attributeModule.ModifyAttributeByType(AttributeType.HPMax, 500);

            // 验证当前血量增加相同的值
            Assert.AreEqual(1300, attributeModule.GetValue(AttributeType.CurrentHp),
                "CurrentHealth should remain unchanged when MaxHealth increases");
        }

        #endregion

        #region 辅助方法
        
        /// <summary>
        /// 判断属性是否为依赖属性（有联动效果）
        /// </summary>
        private bool IsDependentAttribute(AttributeType type)
        {
            // 根据实际需求定义哪些属性有依赖关系
            return type == AttributeType.CurrentHp ||
                   type == AttributeType.HPMax;
        }
        
        /// <summary>
        /// 获取一个非依赖属性用于测试
        /// </summary>
        private AttributeType GetNonDependentAttribute()
        {
            // 返回一个没有依赖关系的属性
            foreach (AttributeType type in System.Enum.GetValues(typeof(AttributeType)))
            {
                if (!IsDependentAttribute(type))
                    return type;
            }

            // 如果没有非依赖属性，返回第一个属性
            return (AttributeType)System.Enum.GetValues(typeof(AttributeType)).GetValue(0);
        }

        /// <summary>
        /// 判断两个属性之间是否有依赖关系
        /// </summary>
        private bool HasDependencyRelationship(AttributeType type1, AttributeType type2)
        {
            // 定义属性之间的依赖关系
            // 例如：当前血量依赖于最大血量
            if ((type1 == AttributeType.CurrentHp && type2 == AttributeType.HPMax))
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}