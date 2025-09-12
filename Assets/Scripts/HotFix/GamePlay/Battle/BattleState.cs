using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace HotFix.GamePlay.Battle
{
    public class BattleState
    {
        public List<Entity> PlayerUnits { get; private set; }
        public List<Entity> EnemyUnits { get; private set; }
        public List<string> Log { get; private set; } = new List<string>();

        public BattleState(List<Entity> players, List<Entity> enemies)
        {
            PlayerUnits = players;
            EnemyUnits = enemies;
        }

        public bool IsBattleOver()
        {
            return PlayerUnits.All(u => !u.IsAlive()) || EnemyUnits.All(u => !u.IsAlive());
        }

        public string GetWinner()
        {
            if (PlayerUnits.All(u => !u.IsAlive())) return "Enemy";
            if (EnemyUnits.All(u => !u.IsAlive())) return "Player";
            return "None";
        }

        // public List<Entity.Entity> GetActionOrder()
        // {
        //     var players = PlayerUnits.Where(u => u.IsAlive()).OrderBy(u => u.Position);
        //     var enemies = EnemyUnits.Where(u => u.IsAlive()).OrderBy(u => u.Position);
        //     return players.Concat(enemies).ToList();
        // }
        //
        // public BattleState DeepClone()
        // {
        //     return new BattleState(
        //         PlayerUnits.Select(u => u.DeepClone()).ToList(),
        //         EnemyUnits.Select(u => u.DeepClone()).ToList()
        //     );
        // }
    }
}