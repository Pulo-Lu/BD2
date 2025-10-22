using System;
using System.Linq;
using HotFix.GamePlay;
using HotFix.GamePlay.Grid;
using UnityEditor;

namespace HotFix
{
    public class GameLogic
    {
        #region 属性相关
        //
        // public static bool SpecialAttribute(AttributeType type)
        // {
        //     return type is AttributeType.Attack or AttributeType.HPMax;
        // }
        //
        // /// <summary>
        // /// 转换角色属性单元
        // /// </summary>
        // public static List<EntityAttributeUnit<T>> GetEntityAttributeData<T>(Dictionary<int, int> attributes) where T : Enum
        // {
        //     var result = new List<EntityAttributeUnit<T>>();
        //     foreach (var attribute in attributes)
        //     {
        //         var unit = new EntityAttributeUnit<T>
        //         {
        //             type = (T)Enum.Parse(typeof(T), attribute.Key.ToString()),
        //             value = attribute.Value,
        //         };
        //         result.Add(unit);
        //     }
        //
        //     return result;
        // }
        //
        
        /// <summary>
        /// 万分比转float
        /// </summary>
        public static float PttToFloat(int ptt)
        {
            return ptt / 10000f;
        }
        
        /// <summary>
        /// 万分比转float
        /// </summary>
        public static float PttToFloat(long ptt)
        {
            return ptt / 10000f;
        }
        #endregion

        #region 索敌

        /// <summary>
        ///  索敌
        /// </summary>
        /// <param name="attack">攻击者</param>
        /// <param name="targetMode">索敌方式</param>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static EntityBase FindTarget(EntityBase attack, TargetMode targetMode, GridManager grid)
        {
            var enemies = grid.GetCampEntities(attack.EntityTeam == EntityTeamType.Self
                ? EntityTeamType.Enemy
                : EntityTeamType.Self);

            //  我方                                      敌方
            // （1,4）（1,3）（1,2）（1,1）                （1,1）（1,2）（1,3）（1,4）
            // （2,4）（2,3）（2,2）（2,1）                （2,1）（2,2）（2,3）（2,4）
            // （3,4）（3,3）（3,2）（3,1）                （3,1）（3,2）（3,3）（3,4）

            // 同一行，前方
            var sameColFront = enemies
                .Where(e => e.Row == attack.Row)
                .OrderBy(e => e.Col) // 同行按最前方列数
                .ToList();

            if (sameColFront.Count > 0)
            {
                //最前方
                if(targetMode == TargetMode.Frontmost)
                    return sameColFront[0];
               
                //越过
                return sameColFront.Count > 1 ? sameColFront[1] : sameColFront[0];
            }

            // 按列顺序搜索下一行的最前方敌人
            var nextColFront = enemies
                .Where(e => e.Row > attack.Row) // 行越大越前
                .OrderBy(e => e.Col) // 列顺序靠前
                .ToList();

            if (nextColFront.Count > 0)
            {
                //最前方
                if(targetMode == TargetMode.Frontmost)
                    return nextColFront[0];
               
                //越过
                return nextColFront.Count > 1 ? nextColFront[1] : nextColFront[0];
            }

            // 如果没有比当前行靠前的行，再找比当前行靠后的行
            var prevColEnemies = enemies
                .Where(e => e.Row < attack.Row)
                .OrderBy(e => e.Col) // 列顺序靠前
                .ToList();

            if (prevColEnemies.Count > 0)
            {
                //最前方
                if(targetMode == TargetMode.Frontmost)
                    return prevColEnemies[0];
               
                //越过
                return prevColEnemies.Count > 1 ? prevColEnemies[1] : prevColEnemies[0];
            }

            return null;
        }
        #endregion
    }
}