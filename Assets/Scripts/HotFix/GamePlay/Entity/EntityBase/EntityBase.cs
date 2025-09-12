using System;
using System.Collections.Generic;
using HotFix.GamePlay.BattleModule;
using HotFix.GamePlay.BattleModule.DamageModule;
using UnityEngine;

namespace HotFix.GamePlay
{
    public abstract class EntityBase : MonoBehaviour
    {
        /// <summary>
        /// 唯一标识符
        /// </summary>
        public string EntityID { get; private set; }

        public EntityTeamType EntityTeam;
        
        public int Row;
        public int Col;
        
        
        /// <summary>
        /// 模块容器
        /// </summary>
        private Dictionary<EEntityModule, BaseEntityModule> modules = new Dictionary<EEntityModule, BaseEntityModule>();
        // 初始属性
        public List<EntityAttributeUnit<AttributeType>> InitAttributes { get; private set; } = new();
        protected virtual void Awake()
        {
            EntityID = Guid.NewGuid().ToString();
        }
        
        /// <summary>
        /// 添加模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public BaseEntityModule AddModule(EEntityModule type)
        {
            var module = NewModule(type);
            if (module == null)
            {
                return null;
            }
        
            modules.Add(type, module);
            
            return module;
        }

        private BaseEntityModule NewModule(EEntityModule type)
        {
            BaseEntityModule module = type switch
            {
                EEntityModule.Attribute => new AttributeModule(),
                EEntityModule.Damage => new DamageModule(),
                EEntityModule.Skill => new SkillModule(),
                _ => null,
            };

            if (module == null)
            {
                return null;
            }

            module.Init(this);

            // if (module is IUpdate updatable)
            // {
            //     m_UpdateModules.Add(updatable);
            // }
            //
            // if (module is ILateUpdate lateUpdate)
            // {
            //     m_LateUpdateModules.Add(lateUpdate);
            // }
            //
            return module;
        }


        /// <summary>
        /// 获取模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool GetModule<T>(EEntityModule type, out T output) where T : BaseEntityModule
        {
            if (modules.TryGetValue(type, out var module))
            {
                output = (T)module;
                return true;
            }
            else
            {
                output = null;
                return false;
            }
        }
        
        /// <summary>
        /// 移除模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void RemoveModule(EEntityModule type)
        {
            modules.Remove(type, out var module);
            module.Recycle();
        }

        /// <summary>
        /// 检查是否有指定模块
        /// </summary>
        public bool HasModule(EEntityModule type)
        {
            return modules.ContainsKey(type);
        }
    }
}