using HotFix.GamePlay;
using HotFix.GamePlay.BattleModule;
using NUnit.Framework;
using UnityEngine;

namespace TestRun.SkillModuleTests
{
    [TestFixture]
    public class SkillModuleTests
    {
        private GameObject entityGameObject;
        private EntityBase entity;
        private SkillModule skillModule;
        
        [SetUp]
        public void Setup()
        {
            entityGameObject = new GameObject("TestEntity");
            entity = entityGameObject.AddComponent<TestEntity>();
            skillModule = (SkillModule)entity.AddModule(EEntityModule.Skill);
        }
    
        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(entityGameObject);
        }
    }
}