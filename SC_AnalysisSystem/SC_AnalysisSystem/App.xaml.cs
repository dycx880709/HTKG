using SC_AnalysisSystem.Helper;
using SC_AnalysisSystem.Resources.Controls;
using SC_AnalysisSystem.View;
using SC_AnalysisSystem_Common;
using SC_AnalysisSystem_Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;

namespace SC_AnalysisSystem
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static Messenger Messenger { get; set; }
        public static string AppDataPath { get; private set; }
        public static string UpdateDataPath { get; private set; }
        public static string TempDataDir { get; set; }
        public static string ProductName { get; private set; }
        public static string ProductVersion { get; private set; }
        public static AppConfig Config { get; set; }
        public static LogHelper LogHelper { get; set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (!WPFAppUtil.ForceOneAppInstance())
                return;
            InitThisProperty();
            if (!CheckComputerSetting())
                return;
            AppDomain.CurrentDomain.UnhandledException += (o, ex) => LogHelper.WriteError("不可恢复的多线程异常 ", ex.ExceptionObject as Exception);
            StartupProgramWindow();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            LogHelper.WriteError("不可恢复的主线程异常", e.Exception);
        }

        private void StartupProgramWindow()
        {
            try
            {
                if (Config.CheckUpdater)
                {
                    // 显示加载界面，在此检查新版本

                }
                else
                {
                    var dialog = new LoginWindow();
                    dialog.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                // 主线程未处理异常
                LogHelper.WriteError("主线程未处理异常", ex);
            }
        }

        private bool CheckComputerSetting()
        {
            try
            {
                SystemUtil helper = new SystemUtil();
                if (Config.SystemValidate && !helper.CheckNecessarySytem())
                {
                    var msg = "当前操作系统不符合程序运行要求，是否下载必要的系统补丁?";
                    var result = MessageBoxX.Question(msg, MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        var system = helper.Distinguish64or32System();
                        if (system != string.Empty)
                        {
                            string url = string.Empty;
                            if (system == "64")
                                url = ConfigurationManager.AppSettings["win7_64_url"];
                            else
                                url = ConfigurationManager.AppSettings["win7_32_url"];
                            Process.Start(url);
                        }
                    }
                    return false;
                }
                if (Config.FrameworkValidate && !helper.CheckFor461DotVersion())
                {
                    var msg = "系统不包含程序运行所需的.NET Framework4.6.1组件和语言包，是否现在下载";
                    var result = MessageBoxX.Question(msg, MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        var url1 = ConfigurationManager.AppSettings["framework461_ENU_url"];
                        var url2 = ConfigurationManager.AppSettings["framework461_CHS_url"];
                        Process.Start(url1);
                        Process.Start(url2);
                    }
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("程序异常:{0}", ex), "程序出错");
                LogHelper.WriteError("系统检查失败", ex);
                return false;
            }
        }

        /// <summary>
        /// 目录初始化
        /// </summary>
        private void InitThisProperty()
        {
            AppDataPath = FileUtil.IsDirectoryWritable(Directory.GetCurrentDirectory())
                ? Directory.GetCurrentDirectory() : WPFAppUtil.StripUserAppDataPathVersion();
            UpdateDataPath = Path.Combine(AppDataPath, "update");
            TempDataDir = Path.Combine(AppDataPath, "temp");
            FileUtil.CreateDirectory(UpdateDataPath);
            FileUtil.CreateDirectory(TempDataDir);
            ProductName = System.Windows.Forms.Application.ProductName;
            ProductVersion = System.Windows.Forms.Application.ProductVersion;
            Config = new AppConfig();
            LogHelper = new LogHelper();
            Messenger = new Messenger();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            //断开和服务器的连接
            base.OnExit(e);
        }
    }
}
