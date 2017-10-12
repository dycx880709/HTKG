using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SC_AnalysisSystem_Core.AttachProperty
{
    public static class PasswordBindingHelper
    {
        public static bool GetIsPasswordBindingEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsPasswordBindingEnabledProperty);
        }

        public static void SetIsPasswordBindingEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsPasswordBindingEnabledProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsPasswordBindingEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsPasswordBindingEnabledProperty =
            DependencyProperty.RegisterAttached("IsPasswordBindingEnabled", typeof(bool), typeof(PasswordBindingHelper), new UIPropertyMetadata(false, OnIsPasswordBindingEnabledChanged));

        private static void OnIsPasswordBindingEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox)
            {
                var pd = d as PasswordBox;
                pd.PasswordChanged -= PasswordBoxPasswordChanged;
                if ((bool)e.NewValue)
                    pd.PasswordChanged += PasswordBoxPasswordChanged;
            }
        }

        private static void PasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
        {
            var pd = sender as PasswordBox;
            if (!string.Equals(GetBindedPassword(pd), pd.Password))
                SetBindedPassword(pd, pd.Password);
            SetPasswordBoxSelection(pd, pd.Password.Length + 1, pd.Password.Length + 1);
        }

        private static void SetPasswordBoxSelection(PasswordBox pd, int start, int length)
        {
            var select = pd.GetType().GetMethod("Select", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            select.Invoke(pd, new object[] { start, length });
        }

        public static string GetBindedPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(BindedPasswordProperty);
        }

        public static void SetBindedPassword(DependencyObject obj, string value)
        {
            obj.SetValue(BindedPasswordProperty, value);
        }

        // Using a DependencyProperty as the backing store for OnBindedPassword.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BindedPasswordProperty =
            DependencyProperty.RegisterAttached("BindedPassword", typeof(string), typeof(PasswordBindingHelper), new UIPropertyMetadata(string.Empty, OnBindedPasswordChanged));

        private static void OnBindedPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pd = d as PasswordBox;
            if (pd != null)
                pd.Password = e.NewValue == null ? string.Empty : e.NewValue.ToString();
        }
    }
}
