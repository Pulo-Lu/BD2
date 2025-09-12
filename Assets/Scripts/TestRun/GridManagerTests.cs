using HotFix.GamePlay;
using HotFix.GamePlay.Battle;
using HotFix.GamePlay.Grid;
using NUnit.Framework;
using UnityEngine;

namespace TestRun
{
    public class GridManagerTests
    {
        private GridManager gridManager;
        private Entity player1, player2;
        private Entity enemy1, enemy2;
        
        [SetUp]
        public void SetUp()
        {
            // 初始化 3x4 战场
            gridManager = new GridManager(3, 4, 4);
       
            // 创建玩家单位
            var playerObj1 = new GameObject();
            playerObj1.AddComponent<Entity>();
            player1 = playerObj1.GetComponent<Entity>();
            player1.Init("Player1");
            
            var playerObj2 = new GameObject();
            playerObj2.AddComponent<Entity>();
            player2 = playerObj2.GetComponent<Entity>();
            player2.Init("Player2");

            // 创建敌方单位
            var enemyObj1 = new GameObject();
            enemyObj1.AddComponent<Entity>();
            enemy1 = enemyObj1.GetComponent<Entity>();
            enemy1.Init("Enemy1");
            
            var enemyObj2 = new GameObject();
            enemyObj2.AddComponent<Entity>();
            enemy2 = enemyObj2.GetComponent<Entity>();
            enemy2.Init("Enemy2");

            // 放置单位
            gridManager.PlaceEntity(player1, EntityTeamType.Self, 1, 1);
            gridManager.PlaceEntity(player2, EntityTeamType.Self, 2, 2);

            gridManager.PlaceEntity(enemy1, EntityTeamType.Enemy, 1, 1);
            gridManager.PlaceEntity(enemy2, EntityTeamType.Enemy, 3, 4);
        }
        
        /// <summary>
        /// 初始化单位
        /// </summary>
        [Test]
        public void TestInitialPlacement()
        {
            // 玩家格子
            Assert.AreEqual(player1, gridManager.GetCell(EntityTeamType.Self, 1, 1).Occupant);
            Assert.AreEqual(player2, gridManager.GetCell(EntityTeamType.Self, 2, 2).Occupant);

            // 敌方格子
            Assert.AreEqual(enemy1, gridManager.GetCell(EntityTeamType.Enemy, 1, 1).Occupant);
            Assert.AreEqual(enemy2, gridManager.GetCell(EntityTeamType.Enemy, 3, 4).Occupant);
        }

        /// <summary>
        ///  移动单位
        /// </summary>
        [Test]
        public void TestMovePlayer()
        {
            // 移动玩家单位
            bool moved = gridManager.MoveEntity(player1, 1, 2);
            Assert.IsTrue(moved);
            Assert.IsNull(gridManager.GetCell(EntityTeamType.Self, 1, 1).Occupant);
            Assert.AreEqual(player1, gridManager.GetCell(EntityTeamType.Self, 1, 2).Occupant);
        }

        /// <summary>
        /// 推动单位
        /// </summary>
        [Test]
        public void TestPushEnemy()
        {
            // 推动敌方单位
            bool pushed = gridManager.PushEntity(enemy1, 2, 1);
            Assert.IsTrue(pushed);
            Assert.IsNull(gridManager.GetCell(EntityTeamType.Enemy, 1, 1).Occupant);
            Assert.AreEqual(enemy1, gridManager.GetCell(EntityTeamType.Enemy, 2, 1).Occupant);
        }

        /// <summary>
        /// 获取阵营单位
        /// </summary>
        [Test]
        public void TestGetCampEntities()
        {
            var playerEntities = gridManager.GetCampEntities(EntityTeamType.Self);
            var enemyEntities = gridManager.GetCampEntities(EntityTeamType.Enemy);

            Assert.AreEqual(2, playerEntities.Count);
            Assert.AreEqual(2, enemyEntities.Count);
            CollectionAssert.Contains(playerEntities, player1);
            CollectionAssert.Contains(enemyEntities, enemy1);
        }

        [Test]
        public void TestInvalidMove()
        {
            // 移动到已占用格子
            bool moved = gridManager.MoveEntity(player1, 2, 2); // player2 占用
            Assert.IsFalse(moved);

            // 移动到越界格子
            moved = gridManager.MoveEntity(player1, 4, 1);
            Assert.IsFalse(moved);
        }
    }
}