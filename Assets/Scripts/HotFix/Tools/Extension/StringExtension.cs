namespace HotFix.Tools.Extension
{
    public static class StringExtension
    {
        /// <summary>
        /// 安全地将字符串转为 int，失败时返回默认值
        /// </summary>
        public static int ToInt(this string selfStr, int defaultValue = 0)
        {
            return int.TryParse(selfStr, out var retValue) ? retValue : defaultValue;
        }
        
        /// <summary>
        /// 安全地将字符串转为 float，失败时返回默认值
        /// </summary>
        public static float ToFloat(this string selfStr, float defaultValue = 0)
        {
            return float.TryParse(selfStr, out var retValue) ? retValue : defaultValue;
        }
        
        /// <summary>
        /// 安全地将字符串转为 long，失败时返回默认值
        /// </summary>
        public static float ToLong(this string selfStr, long defaultValue = 0)
        {
            return long.TryParse(selfStr, out var retValue) ? retValue : defaultValue;
        }
    }
}