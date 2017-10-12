using Ezhu.AutoUpdater.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Ezhu.AutoUpdater.UI
{
    public partial class DownFileProcess : WindowBase
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

        public DownFileProcess(string callExeName, string updateFileDir, string appDir, string appVersion, string url, bool isTest)
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
                    downloadUpdateFile();
                };
                borderTitle.MouseMove += (sender, e)=>
                {
                    if (e.LeftButton == MouseButtonState.Pressed)
                        DragMove();
                };
                NoButton.Click += (sender, e) => Close();
                CloseButton.Click += (sender, e) => Close();
                EndBtn.Click += EndBtn_Click;
                title.Text = "发现新版本（" + appVersion + "），是否现在更新？";
                listBox.ItemsSource = CommonUtil.GetUpdateInfos(desc);
            };
            this.callExeName = callExeName;
            this.updateFileDir = updateFileDir;
            this.appDir = appDir;
            this.appVersion = appVersion;
            this.url = url;
            desc = File.ReadAllText(Path.Combine(this.updateFileDir, "describe.txt"));
            appName = this.callExeName;
        }

        private void EndBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tempDir = Path.Combine(updateFileDir, "temp");
                string fileNameNoEx = Path.GetFileNameWithoutExtension(url);
                string filePath = Path.Combine(tempDir, fileNameNoEx, appName, ".msi");
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

        public DownFileProcess(string callExeName, string updateFileDir, string appDir, string appVersion, string url)
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
                    
                };
                borderTitle.MouseMove += delegate (object sender, MouseEventArgs e)
                {
                    if (e.LeftButton == MouseButtonState.Pressed)
                        DragMove();
                };
                NoButton.Click += (sender, e) => Close();
                CloseButton.Click += (sender, e) => Close();
                EndBtn.Click += EndBtn_Click;
                title.Text = "发现新版本（" + appVersion + "），是否现在更新？";
                listBox.ItemsSource = CommonUtil.GetUpdateInfos(desc);
            };
            this.callExeName = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(callExeName));
            this.updateFileDir = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(updateFileDir));
            this.appDir = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(appDir));
            this.appVersion = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(appVersion));
            this.url = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(url));
            desc = File.ReadAllText(Path.Combine(this.updateFileDir, "describe.txt"));
            appName = this.callExeName;
        }

        private void downloadUpdateFile()
        {
            Cursor = Cursors.Wait;
            title.Text = "软件升级中";
            YesButton.IsEnabled = false;
            Thread thread = new Thread(new ParameterizedThreadStart(downloadUpdateFile));
            thread.IsBackground = true;
            thread.Start();
        }

        private void downloadUpdateFile(object o)
        {
            var client = new WebClient();
            client.DownloadProgressChanged += (sender, e) =>
            {
                Dispatcher.Invoke(() => UpdateProcess(e.BytesReceived, e.TotalBytesToReceive));
            };
            client.DownloadDataCompleted += (sender, e) =>
            {
                updateComplete();
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
                CommonUtil.UnZipFile(zipFilePath, tempDir);
            };
            client.DownloadDataAsync(new Uri(url));
        }

        private void updateComplete()
        {
            Dispatcher.Invoke(() =>
            {
                Cursor = Cursors.Arrow;
                YesButton.IsEnabled = true;
                txtProcess.Text = "100%";
                title.Text = "软件升级完成";
                YesButton.IsDefault = false;
                EndBtn.IsDefault = true;
                EndBtn.Visibility = Visibility.Visible;
                processGd.Visibility = Visibility.Hidden;
            });
        }

        public void UpdateProcess(long current, long total)
        {
            string status = (int)((float)current * 100 / (float)total) + "%";
            txtProcess.Text = status;
            rectProcess.Width = ((float)current / (float)total) * bProcess.ActualWidth;
        }
    }
}
