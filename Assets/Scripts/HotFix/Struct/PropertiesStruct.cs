using System;
using HotFix.Enums;
using Sirenix.OdinInspector;

namespace HotFix.Struct
{
    [Serializable]
    public struct PropertiesStruct
    {
        /// <summary>
        ///     属性类型
        /// </summary>
        [LabelText("属性类型")]
        public EnumPropertiesType propertiesType;
        /// <summary>
        ///     值
        /// </summary>
        [LabelText("值")]
        public long value;
        /// <summary>
        ///     是百分比 如果为true 则value为百分比值
        /// </summary>
        [LabelText("是否百分比")]
        public bool isPercentage;


        public PropertiesStruct(EnumPropertiesType propertiesType, int value, bool isPercentage) {
            this.propertiesType = propertiesType;
            this.value = value;
            this.isPercentage = isPercentage;
        }

        public PropertiesStruct(EnumPropertiesType propertiesType, int value) : this() {
            this.propertiesType = propertiesType;
            this.value = value;
        }

        public float GetPercentageValue() {
            // 10 / 100 = 0.1
            return (float)value / 100;
        }

        /// <summary>
        ///     计算属性
        /// </summary>
        /// <param name="equipPs"></param>
        public void Merge(PropertiesStruct equipPs) {
            value += equipPs.value;
        }

        /// <summary>
        ///     计算百分比的属性
        /// </summary>
        /// <param name="equipPercentPs"></param>
        public void MergePercent(PropertiesStruct equipPercentPs) {
            value += value * equipPercentPs.value / 100;
        }
    }
}