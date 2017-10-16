using SC_AnalysisSystem_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SC_AnalysisSystem.AttachProperty
{
    public class LocalAttachProperty
    {
        public static GamerState GetGamerState(DependencyObject obj)
        {
            return (GamerState)obj.GetValue(GamerStateProperty);
        }

        public static void SetGamerState(DependencyObject obj, GamerState value)
        {
            obj.SetValue(GamerStateProperty, value);
        }

        public static readonly DependencyProperty GamerStateProperty =
            DependencyProperty.RegisterAttached("GamerState", typeof(GamerState), typeof(LocalAttachProperty), new PropertyMetadata(GamerState.OffLine));
    }
}
