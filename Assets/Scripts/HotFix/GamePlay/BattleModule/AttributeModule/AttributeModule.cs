using System;
using System.Collections.Generic;

namespace HotFix.GamePlay.BattleModule
{
    public struct EntityAttributeUnit<T> where T : Enum
    {
        public T type;
        public int value;
    }

    public class AttributeModule : BaseEntityModule
    {

        private readonly Dictionary<AttributeType, AttributeBase> m_Attributes = new();

        /// <summary>
        /// 攻击力
        /// </summary>
        public long Attack { get; protected set; }
        /// <summary>
        /// 攻击力加成万分比
        /// </summary>
        public long AttackValuePtt => GetValue(AttributeType.AttackPtt);
        /// <summary>
        /// 魔法力
        /// </summary>
        public long Magic { get; protected set; }
        /// <summary>
        /// 魔法力加成万分比
        /// </summary>
        public long MagicValuePtt => GetValue(AttributeType.MagicPtt);
        // 血量
        /// <summary>
        /// 最大血量
        /// </summary>
        public int MaxHp { get; protected set; }
        /// <summary>
        /// 当前血量
        /// </summary>
        public int CurrentHp  { get; protected set; }
        /// <summary>
        /// 防御万分比
        /// </summary>
        public long AttackDefense  { get; protected set; }     
        /// <summary>
        /// 魔抗万分比
        /// </summary>
        public long MagicDefense  { get; protected set; }

        
        /// <summary>
        /// 暴击概率
        /// </summary>
        public int CritRate { get; protected set; }

        /// <summary>
        /// 暴击伤害加成万分比
        /// </summary>
        public int CritValuePtt { get; protected set; }


        protected override void OnInit()
        {
            ApplyInitAttributes();
        }

        protected override void OnRecycle()
        {
            foreach (var attribute in m_Attributes.Values)
            {
                attribute.Clear();
            }

            m_Attributes.Clear();
        }

        protected override void OnReset()
        {
            foreach (var attribute in m_Attributes.Values)
            {
                attribute.Clear();
            }

            m_Attributes.Clear();
        }

        public void ApplyInitAttributes()
        {
            m_Attributes.Clear();

            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
            {
                m_Attributes[type] = new AttributeBase(0);
            }

            // 初始化属性
            UpdateAttack();
            
            UpdateMagic();
            
            UpdateMaxHp();
            UpdateCurrentHp();
            
            UpdateAttackDefense();
            UpdateMagicDefense();

            // 订阅属性更新回调
            SubscribeUpdate();
        }

        public void ModifyAttributeByType(AttributeType type, int deltaValue)
        {
            if (deltaValue == 0)
            {
                return;
            }

            if (m_Attributes.TryGetValue(type, out var record))
            {
                record.ModifyValue(deltaValue);
            }
            else
            {
                m_Attributes[type] = new AttributeBase(deltaValue);
            }
        }
        

        public int GetValue(AttributeType type, int defaultVal = 0)
        {
            if (m_Attributes.TryGetValue(type, out var val))
            {
                return val.Value;
            }
            else
            {
                m_Attributes.Add(type, new AttributeBase(defaultVal));
                return defaultVal;
            }
        }

        protected virtual void SubscribeUpdate()
        {
            // 攻击力
            SubscribeUpdate(AttributeType.Attack, UpdateAttack);
            SubscribeUpdate(AttributeType.AttackPtt, UpdateAttack);
            
            // 魔法力
            SubscribeUpdate(AttributeType.Magic, UpdateMagic);
            SubscribeUpdate(AttributeType.MagicPtt, UpdateMagic);

            // 最大血量
            SubscribeUpdate(AttributeType.HPMax, UpdateMaxHp);
            SubscribeUpdate(AttributeType.CurrentHp, UpdateCurrentHp);
            
            // 防御
            SubscribeUpdate(AttributeType.AttackDefense, UpdateAttackDefense);
            
            // 魔抗
            SubscribeUpdate(AttributeType.MagicDefense, UpdateMagicDefense);
        }

        protected void SubscribeUpdate(AttributeType type, Action action)
        {
            if (m_Attributes.TryGetValue(type, out var attributeInt))
            {
                attributeInt.SubscribeUpdate(action);
            }
        }

        /// <summary>
        /// 面板攻击力
        /// </summary>
        protected virtual void UpdateAttack()
        {
            Attack = Math.Max(0, (long)(GetValue(AttributeType.Attack)));
            // Attack = (long)(GetValue(AttributeType.Attack) * (1 + GameLogic.PttToFloat(GetValue(AttributeType.AttackPtt))));
        }     
        
        /// <summary>
        /// 面板魔法力
        /// </summary>
        protected virtual void UpdateMagic()
        {
            Magic = Math.Max(0, (long)(GetValue(AttributeType.Magic)));
            // Magic = (long)(GetValue(AttributeType.Magic) * (1 + GameLogic.PttToFloat(GetValue(AttributeType.MagicPtt))));
        }

        protected virtual void UpdateMaxHp()
        {
            var newMaxHp = GetValue(AttributeType.HPMax);
            var oldMaxHp = MaxHp;

            //var newMaxHp = (long)(GetValue(AttributeType.HPMax) * (1 + GameLogic.PttToFloat(MaxHpAddPtt))) + ExtraMaxHp;
            // 更新最大血量
            MaxHp = Math.Max(0, newMaxHp);

            // 最大血量的增减会影响当前血量
            var currentHp = GetValue(AttributeType.CurrentHp);
            // 处理当前血量的调整
            if (newMaxHp != oldMaxHp)
            {
                // 计算血量差值并应用到当前血量
                int hpDifference = newMaxHp - oldMaxHp;
                ModifyAttributeByType(AttributeType.CurrentHp, hpDifference);
                
                // 确保当前血量不超过最大血量（安全钳制）
                if (GetValue(AttributeType.CurrentHp) > MaxHp)
                {
                    if (m_Attributes.TryGetValue(AttributeType.CurrentHp, out var currentHpAttr))
                    {
                        // 直接设置值
                        currentHpAttr.SetValueDirectly(MaxHp);
                    }
                }
            }
        }

        protected virtual void UpdateCurrentHp()
        {
            // 获取当前HP值
            var currentHpValue = GetValue(AttributeType.CurrentHp);

            // 钳制到 0-MaxHp 之间
            var clampedHp = Math.Max(0, Math.Min(currentHpValue, MaxHp));

            // 如果值被钳制，需要重新设置
            if (MaxHp != currentHpValue)
            {
                // 直接设置值而不是修改，避免无限递归
                if (m_Attributes.TryGetValue(AttributeType.CurrentHp, out var currentHpAttr))
                {
                    // 直接设置值
                    currentHpAttr.SetValueDirectly(clampedHp);
                }
            }

            CurrentHp = clampedHp;
        }

        protected virtual void UpdateAttackDefense()
        {
            AttackDefense = Math.Max(0, (long)(GetValue(AttributeType.AttackDefense)));
                             //* (1 + GameLogic.PttToFloat(GetValue(AttributeType.DefensePtt))));
        }   
        
        protected virtual void UpdateMagicDefense()
        {
            MagicDefense = Math.Max(0, (long)(GetValue(AttributeType.MagicDefense)));
                             //* (1 + GameLogic.PttToFloat(GetValue(AttributeType.DefensePtt))));
        }
    }
}