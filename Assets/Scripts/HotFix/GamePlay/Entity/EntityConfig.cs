using UnityEngine;

namespace HotFix.GamePlay
{
    [CreateAssetMenu(fileName = "NewEntity", menuName = "Game/Entity")]
    public class EntityConfig : ScriptableObject
    {
        public int ID;
        public string Name;
        public int MaxHP;
        public int Attack;
        public int Defense;
        public float CritRate;
        public float CritDamage;
        public int[] InitialSkills;
        public GameObject Prefab;
    }
}