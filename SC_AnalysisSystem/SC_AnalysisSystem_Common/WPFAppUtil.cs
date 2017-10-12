using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using YJYTemplate.Common;

namespace SC_AnalysisSystem_Common
{
    public static class WPFAppUtil
    {
        /// <summary>
        /// 去除用户数据目录最后的版本号
        /// http://weblogs.asp.net/psheriff/userappdatapath-in-wpf
        /// http://stackoverflow.com/questions/1736882/how-can-i-build-application-userappdatapath-inside-class-libraries-that-dont-h
        /// </summary>
        /// <returns></returns>
        public static string StripUserAppDataPathVersion()
        {
            var path = System.Windows.Forms.Application.UserAppDataPath;
            var lastDir = path.Substring(path.LastIndexOf('\\') + 1);
            if (!string.IsNullOrEmpty(lastDir))
            {
                if (lastDir.Equals(System.Windows.Forms.Application.ProductVersion))
                {
                    path = path.Substring(0, path.LastIndexOf('\\'));
                }
            }
            return path;
        }

        /// <summary>
        /// 强制运行唯一实例
        /// </summary>
        /// <returns></returns>
        public static bool ForceOneAppInstance()
        {
            var p = FindOtherRunningInstance();
            if (p != null)
            {
                // 显示已运行实例的主窗口
                Win32.ShowWindowAsync(p.MainWindowHandle, Win32.SW_RESTORE);
                Win32.SetForegroundWindow(p.MainWindowHandle);
                // 结束当前实例
                Application.Current.Shutdown();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 查找其他已运行实例
        /// </summary>
        /// <returns></returns>
        private static Process FindOtherRunningInstance()
        {
            var current = Process.GetCurrentProcess();
            var processes = Process.GetProcessesByName(current.ProcessName);
            foreach (var p in processes)
            {
                if ((p.Id != current.Id) && (p.ProcessName == current.ProcessName))
                    return p;
            }
            return null;
        }

        public static bool ForceOneAppInstance(string processName)
        {
            var processes = Process.GetProcessesByName(processName);
            foreach (var p in processes)
            {
                // 显示已运行实例的主窗口
                Win32.ShowWindowAsync(p.MainWindowHandle, Win32.SW_RESTORE);
                Win32.SetForegroundWindow(p.MainWindowHandle);
                return false;
            }
            return true;
        }

        //GetForegroundWindow API
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        //从Handle中获取Window对象
        private static Window GetWindowFromHwnd(IntPtr hwnd)
        {
            var source = HwndSource.FromHwnd(hwnd);
            if (source != null)
                return (Window)HwndSource.FromHwnd(hwnd).RootVisual;
            return null;
        }

        /////调用GetForegroundWindow然后调用GetWindowFromHwnd

        /// <summary>
        /// 获取当前顶级窗体，若获取失败则返回主窗体
        /// </summary>
        public static Window GetTopWindow()
        {
            var hwnd = GetForegroundWindow();
            if (hwnd == IntPtr.Zero)
                return Application.Current.MainWindow;
            else
            {
                var window = GetWindowFromHwnd(hwnd);
                return window == null ? Application.Current.MainWindow : window;
            }
        }

        /// <summary>
        /// 获取枚举类型的描述
        /// </summary>
        /// <param name="enumeration"></param>
        /// <returns></returns>
        //public static string GetDescription(this Enum enumeration)
        //{
        //    Type type = enumeration.GetType();
        //    MemberInfo[] memInfo = type.GetMember(enumeration.ToString());
        //    if (null != memInfo && memInfo.Length > 0)
        //    {
        //        object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        //        if (null != attrs && attrs.Length > 0)
        //            return ((DescriptionAttribute)attrs[0]).Description;
        //    }
        //    return enumeration.ToString();
        //}
    }
}
