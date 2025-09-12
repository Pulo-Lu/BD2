using System.Collections.Generic;
using System.Linq;

namespace HotFix.GamePlay.BattleModule
{
    public class SkillModule : BaseEntityModule
    {
        private List<SkillConfig> knownSkills = new List<SkillConfig>();
        private Dictionary<int, int> skillCooldowns = new Dictionary<int, int>();

        public override void Init(EntityBase entity)
        {
            base.Init(entity);
        
            // 加载初始技能
            // var config = ConfigManager.GetEntityConfig(entity.EntityID);
            // foreach(var skillId in config.InitialSkills)
            // {
            //     LearnSkill(skillId);
            // }
        }
        
        public void LearnSkill(int skillId)
        {
            // var config = ConfigManager.GetSkillConfig(skillId);
            // if(config != null && !knownSkills.Contains(config))
            // {
            //     knownSkills.Add(config);
            // }
        }
        
        public SkillConfig[] GetAvailableSkills()
        {
            return knownSkills.Where(s => !IsOnCooldown(s.ID)).ToArray();
        }
        
        public void StartCooldown(int skillId, int cd = 0)
        {
            // if(cd <= 0) cd = ConfigManager.GetSkillConfig(skillId).Cooldown;
            // skillCooldowns[skillId] = cd;
        }
        
        private bool IsOnCooldown(int skillId) => skillCooldowns.ContainsKey(skillId) && skillCooldowns[skillId] > 0;
        
        // 回合结束时更新CD
        public void OnTurnEnd()
        {
            var keys = skillCooldowns.Keys.ToList();
            foreach(var skillId in keys)
            {
                skillCooldowns[skillId]--;
                if(skillCooldowns[skillId] <= 0) 
                    skillCooldowns.Remove(skillId);
            }
        }
        
        protected override void OnInit()
        {
        }

        protected override void OnRecycle()
        {
        }

        protected override void OnReset()
        {
        }
    }
}