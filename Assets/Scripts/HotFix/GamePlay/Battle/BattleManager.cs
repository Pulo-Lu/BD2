using System.Collections.Generic;
using HotFix.GamePlay.BattleModule;

namespace HotFix.GamePlay.Battle
{
    public class BattleManager
    {
        private BattleState state;
        private int round;
        
        public BattleManager(List<Entity> players, List<Entity> enemies)
        {
            state = new BattleState(players, enemies);
            round = 0;
        }
        
        // 执行整局战斗
        public BattleResult RunBattle()
        {
            while (!state.IsBattleOver())
            {
                ExecuteRound();
            }

            return new BattleResult
            {
                Winner = state.GetWinner(),
                Log = state.Log
            };
        }
        
        // 执行单回合（不使用 while，逐步推进）
        private void ExecuteRound()
        {
            round++;
            state.Log.Add($"--- 回合 {round} ---");

            // var actionOrder = state.GetActionOrder();
            //
            // foreach (var unit in actionOrder)
            // {
            //     if (!unit.IsAlive) continue;
            //
            //     SkillConfig chosenSkill = unit.DecideSkill(state);
            //     Entity target = TargetSelector.ChooseTarget(chosenSkill, unit, state);
            //
            //     chosenSkill.Execute(unit, target, state);
            // }
        }
    }
}