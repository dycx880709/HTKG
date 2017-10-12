using AutoUpdater;
using System;
using System.IO;
using System.Reflection;
using Encoding = System.Text.Encoding;
using System.Diagnostics;
using System.Threading;
using System.Windows;

namespace Ezhu.AutoUpdater
{
    public class Updater
    {
        private static Updater _instance;
        public static Updater Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Updater();
                return _instance;
            }
        }

        public static string GetProcessName()
        {
            return Assembly.GetAssembly(typeof(Updater)).GetName().Name;
        }

        public static void CheckUpdateStatus(string appPath, string versionPath, bool supportCmpUpdate)
        {
            ThreadPool.QueueUserWorkItem((s) =>
            {
                var client = new System.Net.WebClient();
                client.DownloadDataCompleted += (x, y) =>
                {
                    try
                    {
                        MemoryStream stream = new MemoryStream(y.Result);
                        var sr = new StreamReader(stream);
                        var str = sr.ReadToEnd();
                        var versionInfo = JsonUtil.Deserialize<VersionInfo>(str);
                        stream.Close();
                        Instance.StartUpdate(versionInfo, appPath, supportCmpUpdate);
                    }
                    catch(Exception)
                    { }
                };
                client.DownloadDataAsync(new Uri(versionPath));
            });
        }

        public void StartUpdate(VersionInfo versionInfo, string appDir, bool supportCmpUpdate)
        {
            bool isDownloadSetup = false;
            //当前版本比需要的版本小，不更新
            if (!supportCmpUpdate || (versionInfo.requiredMinVersion != null && Instance.CurrentVersion < new Version(versionInfo.requiredMinVersion)))
                isDownloadSetup = true;
            //当前版本是最新的，不更新
            if (Instance.CurrentVersion >= new Version(versionInfo.version)) return;
            string updateFileDir = Path.Combine(appDir, "update");
            if (!Directory.Exists(updateFileDir))
                Directory.CreateDirectory(updateFileDir);
            updateFileDir = Path.Combine(updateFileDir, Guid.NewGuid().ToString());
            if (!Directory.Exists(updateFileDir))
                Directory.CreateDirectory(updateFileDir);
            string exePath = Path.Combine(updateFileDir, "AutoUpdater.exe");
            File.Copy(Path.Combine(appDir, "AutoUpdater.exe"), exePath, true);

            var descFile = Path.Combine(updateFileDir, "describe.txt");
            File.WriteAllText(descFile, versionInfo.describe);

            var info = new ProcessStartInfo(exePath);
            info.UseShellExecute = true;
            info.WorkingDirectory = exePath.Substring(0, exePath.LastIndexOf(Path.DirectorySeparatorChar));
            info.Arguments = "update"
            + " " + Convert.ToBase64String(Encoding.UTF8.GetBytes(CallExeName)) 
            + " " + Convert.ToBase64String(Encoding.UTF8.GetBytes(updateFileDir))
            + " " + Convert.ToBase64String(Encoding.UTF8.GetBytes(appDir))
            + " " + Convert.ToBase64String(Encoding.UTF8.GetBytes(versionInfo.version))
            + " " + Convert.ToBase64String(Encoding.UTF8.GetBytes(versionInfo.url))
            + " " + Convert.ToBase64String(Encoding.UTF8.GetBytes(isDownloadSetup.ToString()));
            try
            {
                Process.Start(info);
            }
            catch (Exception)
            {
                MessageBox.Show("升级过程中出现异常，请重新升级或通过在网上下载安装包完成软件升级", "温馨提示");
            }
        }

        private string _callExeName;
        public string CallExeName
        {
            get
            {
                if (string.IsNullOrEmpty(_callExeName))
                {
                    _callExeName = Assembly.GetEntryAssembly().Location.Substring(Assembly.GetEntryAssembly().Location.LastIndexOf(Path.DirectorySeparatorChar) + 1).Replace(".exe", "");
                }
                return _callExeName;
            }
        }

        /// <summary>
        /// 获得当前应用软件的版本
        /// </summary>
        public virtual Version CurrentVersion
        {
            get { return new Version(FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).ProductVersion); }
        }

        /// <summary>
        /// 获得当前应用程序的根目录
        /// </summary>
        public virtual string CurrentApplicationDirectory
        {
            get { return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location); }
        }
    }
}
