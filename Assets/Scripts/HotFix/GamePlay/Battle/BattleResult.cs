using System.Collections.Generic;

namespace HotFix.GamePlay.Battle
{
    public class BattleResult
    {
        /// <summary>
        /// 胜利方: "Player" / "Enemy" / "Draw"
        /// </summary>
        public string Winner { get; set; }

        /// <summary>
        /// 战斗过程日志
        /// </summary>
        public List<string> Log { get; set; } = new List<string>();

        /// <summary>
        /// 最终玩家存活情况
        /// </summary>
        // public List<UnitSnapshot> PlayerSnapshots { get; set; }

        /// <summary>
        /// 最终敌方存活情况
        /// </summary>
        // public List<UnitSnapshot> EnemySnapshots { get; set; }

        /// <summary>
        /// 总回合数
        /// </summary>
        public int TotalRounds { get; set; }

        /// <summary>
        /// 总伤害统计（玩家对敌方造成的伤害）
        /// </summary>
        public int TotalDamageToEnemy { get; set; }

        /// <summary>
        /// 总伤害统计（敌方对玩家造成的伤害）
        /// </summary>
        public int TotalDamageToPlayer { get; set; }
    }

}