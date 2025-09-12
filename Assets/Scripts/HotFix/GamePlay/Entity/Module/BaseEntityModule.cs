namespace HotFix.GamePlay
{
    public enum EEntityModule
    {
        Command,
        Attribute,
        Status,
        Damage,
        Weapon,
        Element,
        Shield,
        Skill,
        Buff,
        Move,
        MakeUp,
        Anim,
        Posture,
    }


    public abstract class BaseEntityModule
    {
        #region 属性

        protected EntityBase Entity;

        #endregion

        #region 生命周期

        public virtual void Init(EntityBase entity)
        {
            this.Entity = entity;
            OnInit();
        }

        public void Recycle()
        {
            OnRecycle();
            Entity = null;
        }

        public void Reset()
        {
            OnReset();
        }

        protected abstract void OnInit();

        protected abstract void OnRecycle();

        protected abstract void OnReset();

        #endregion
    }
}