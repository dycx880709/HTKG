using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace SC_AnalysisSystem_Core.Converter
{
    public class FalseToCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool visible = (bool)value;
            if (visible)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visible = (Visibility)value;
            if (visible == Visibility.Visible)
                return true;
            return false;
        }
    }
}
