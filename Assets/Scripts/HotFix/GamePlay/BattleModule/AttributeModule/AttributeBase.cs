using System;

namespace HotFix.GamePlay.BattleModule
{
    /// <summary>
    /// 属性基类
    /// </summary>
    public class AttributeBase
    {
        public int Value { get; private set; }

        private Action m_UpdateInvoker;
        
        public AttributeBase(int value)
        {
            Value = value;
        }
        
        public void SubscribeUpdate(Action action)
        {
            m_UpdateInvoker += action;
        }

        public void UnsubscribeUpdate(Action action)
        {
            m_UpdateInvoker -= action;
        }
        
        public void Clear()
        {
            m_UpdateInvoker = null;
        }

        public void ModifyValue(int delta)
        {
            Value += delta;
            m_UpdateInvoker?.Invoke();
        }
        
        /// <summary>
        /// 直接设置值的方法
        /// </summary>
        /// <param name="newValue"></param>
        public void SetValueDirectly(int newValue)
        {
            Value = newValue;
        }
    }
}