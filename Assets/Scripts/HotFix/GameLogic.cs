using System;
using System.Collections.Generic;

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
    }
}