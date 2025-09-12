

using System.Collections.Generic;
using HotFix.GamePlay.BattleModule;
using UnityEngine;

namespace HotFix.GamePlay
{
    public class Entity : EntityBase, IElement
    {
        public int Element { get; private set; }

        public string Name { get; private set; }
        
        public void Init(string name)
        {
            Name = name;
            AddModule(EEntityModule.Attribute);
        }

        public bool IsAlive()
        {
            if (GetModule(EEntityModule.Attribute, out AttributeModule attr))
            {
                return attr.CurrentHp > 0;
            }
            return false;
        }
    }
}