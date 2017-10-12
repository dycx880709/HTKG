using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using Ezhu.AutoUpdater.UI;

namespace Ezhu.AutoUpdater
{
    class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                //App app = new App();
                //DownCmpProcess downUI = new DownCmpProcess("阅卷易扫描客户端", @"E:\YJYClient\AutoUpdater\bin\Debug\update-test2\0dd8e8e7-666e-4721-9109-cc7aa6ef938b", @"E:\YJYClient\AutoUpdater\bin\Debug", "2.1.0", "http://test3.yuejuanyi.com/csclient/YJYClient-v2.0.7Setup.zip", true) { WindowStartupLocation = WindowStartupLocation.CenterScreen };
                //app.Run(downUI);
                return;
            }
            else if (args[0] == "update")
            {
                try
                {
                    string callExeName = args[1];
                    string updateFileDir = args[2];
                    string appDir = args[3];
                    string appVersion = args[4];
                    string url = args[5];
                    bool isDownloadSetup = bool.Parse(Encoding.UTF8.GetString(Convert.FromBase64String(args[6])));
                    App app = new App();
                    if (isDownloadSetup)
                    {
                        DownFileProcess downUI = new DownFileProcess(callExeName, updateFileDir, appDir, appVersion, url) { WindowStartupLocation = WindowStartupLocation.CenterScreen };
                        app.Run(downUI);
                    }
                    else
                    {
                        DownCmpProcess downUI = new DownCmpProcess(callExeName, updateFileDir, appDir, appVersion, url) { WindowStartupLocation = WindowStartupLocation.CenterScreen };
                        app.Run(downUI);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("升级操作发生异常，请重新升级或登录官方下载地址下载最新软件安装包");
                }
            }
        }
    }
}
