using Ezhu.AutoUpdater.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using Path = System.IO.Path;

namespace Ezhu.AutoUpdater.UI
{
    /// <summary>
    /// DownFileTest.xaml 的交互逻辑
    /// </summary>
    public partial class DownFileTest : WindowBase
    {
        /// <summary>
        /// 更新文件存放的文件夹
        /// </summary>
        private string updateFileDir;
        private string callExeName;
        private string appDir;
        private string appName;
        private string appVersion;
        private string desc;
        /// <summary>
        /// 服务器根目录
        /// </summary>
        private string url;

        public DownFileTest(string callExeName, string updateFileDir, string appDir, string appVersion, string url, bool isTest)
        {
            InitializeComponent();
            Loaded += (s, o) =>
            {
                YesButton.Content = "现在更新";
                NoButton.Content = "暂不更新";
                YesButton.Click += (sender, e) =>
                {
                    startSp.Visibility = Visibility.Hidden;
                    processGd.Visibility = Visibility.Visible;
                    Process[] processes = Process.GetProcessesByName(this.callExeName);
                    if (processes.Length > 0)
                    {
                        foreach (var p in processes)
                            p.Kill();
                    }
                    DownloadUpdateFile();
                };
                NoButton.Click += (sender, e) => Close();
                CloseButton.Click += (sender, e) => Close();
                EndBtn.Click += EndBtn_Click;
                VersionTb.Text = appVersion;
                txtProcess.Text = appName + "发现新的版本(" + this.appVersion + "),是否现在更新?";
                getInfos(desc);

            };
            this.callExeName = callExeName;
            this.updateFileDir = updateFileDir;
            this.appDir = appDir;
            this.appVersion = appVersion;
            this.url = url;
            string sDesc = File.ReadAllText(Path.Combine(this.updateFileDir, "describe.txt"));
            desc = sDesc;
            appName = this.callExeName;
        }

        private void EndBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tempDir = Path.Combine(updateFileDir, "temp");
                string fileNameNoEx = Path.GetFileNameWithoutExtension(url);
                string filePath = Path.Combine(tempDir, fileNameNoEx, "YJY2Setup.msi");
                var info = new ProcessStartInfo(filePath);
                info.UseShellExecute = true;
                info.WorkingDirectory = appDir;
                Process.Start(info);
                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getInfos(string describe)
        {
            var Infos = new List<UpdateText>();
            if (!string.IsNullOrEmpty(describe))
            {
                string[] split = describe.Split('\n');
                foreach (var item in split)
                    Infos.Add(new UpdateText(item.Trim()));
            }
            listBox.ItemsSource = Infos;
        }

        public DownFileTest(string callExeName, string updateFileDir, string appDir, string appVersion, string url)
        {
            InitializeComponent();
            Loaded += (s, o) =>
            {
                YesButton.Content = "现在更新";
                NoButton.Content = "暂不更新";
                YesButton.Click += (sender, e) =>
                {
                    Process[] processes = Process.GetProcessesByName(this.callExeName);
                    if (processes.Length > 0)
                    {
                        foreach (var p in processes)
                            p.Kill();
                    }
                    DownloadUpdateFile();
                };
                NoButton.Click += (sender, e) => Close();
                txtProcess.Text = appName + "发现新的版本(" + this.appVersion + "),是否现在更新?";
                getInfos(desc);
            };
            this.callExeName = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(callExeName));
            this.updateFileDir = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(updateFileDir));
            this.appDir = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(appDir));
            this.appVersion = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(appVersion));
            this.url = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(url));
            string sDesc = File.ReadAllText(Path.Combine(this.updateFileDir, "describe.txt"));
            desc = "更新内容如下:\r\n" + sDesc;
            appName = this.callExeName;
        }

        public void DownloadUpdateFile()
        {
            var client = new WebClient();
            client.DownloadProgressChanged += (sender, e) =>
            {
                UpdateProcess(e.BytesReceived, e.TotalBytesToReceive);
            };
            client.DownloadDataCompleted += (sender, e) =>
            {
                string fileName = Path.GetFileName(url);
                string zipFilePath = Path.Combine(updateFileDir, fileName);
                byte[] data = e.Result;
                BinaryWriter writer = new BinaryWriter(new FileStream(zipFilePath, FileMode.OpenOrCreate));
                writer.Write(data);
                writer.Flush();
                writer.Close();

                string tempDir = Path.Combine(updateFileDir, "temp");
                if (!Directory.Exists(tempDir))
                    Directory.CreateDirectory(tempDir);
                UnZipFile(zipFilePath, tempDir);
                processGd.Visibility = Visibility.Hidden;
                EndBtn.Visibility = Visibility.Visible;
            };
            client.DownloadDataAsync(new Uri(url));
        }

        private static void UnZipFile(string zipFilePath, string targetDir)
        {
            ICCEmbedded.SharpZipLib.Zip.FastZipEvents evt = new ICCEmbedded.SharpZipLib.Zip.FastZipEvents();
            ICCEmbedded.SharpZipLib.Zip.FastZip fz = new ICCEmbedded.SharpZipLib.Zip.FastZip(evt);
            fz.ExtractZip(zipFilePath, targetDir, "");
        }

        public void UpdateProcess(long current, long total)
        {
            string status = (int)((float)current * 100 / (float)total) + "%";
            this.txtProcess.Text = status;
            rectProcess.Width = ((float)current / (float)total) * bProcess.ActualWidth;
        }

        public void CopyDirectory(string sourceDirName, string destDirName)
        {
            try
            {
                if (!Directory.Exists(destDirName))
                {
                    Directory.CreateDirectory(destDirName);
                    File.SetAttributes(destDirName, File.GetAttributes(sourceDirName));
                }
                if (destDirName[destDirName.Length - 1] != Path.DirectorySeparatorChar)
                    destDirName = destDirName + Path.DirectorySeparatorChar;
                string[] files = Directory.GetFiles(sourceDirName);
                foreach (string file in files)
                {
                    File.Copy(file, destDirName + Path.GetFileName(file), true);
                    File.SetAttributes(destDirName + Path.GetFileName(file), FileAttributes.Normal);
                }
                string[] dirs = Directory.GetDirectories(sourceDirName);
                foreach (string dir in dirs)
                {
                    CopyDirectory(dir, destDirName + Path.GetFileName(dir));
                }
            }
            catch (Exception)
            {
                throw new Exception("复制文件错误");
            }
        }
    }


}
