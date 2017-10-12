using System;
using System.Windows.Controls;

namespace SC_AnalysisSystem_Core.Converter
{
    /// <summary>
    /// 常用转换器的静态引用
    /// 使用实例：Converter={x:Static local:XConverter.TrueToFalseConverter}
    /// </summary>
    public sealed class XConverter
    {
        public static BooleanToVisibilityConverter BooleanToVisibilityConverter
        {
            get { return Singleton<BooleanToVisibilityConverter>.GetInstance(); }
        }

        public static TrueToFalseConverter TrueToFalseConverter
        {
            get { return Singleton<TrueToFalseConverter>.GetInstance(); }
        }
    }
}