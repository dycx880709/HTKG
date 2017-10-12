using AutoUpdater;
using Ezhu.AutoUpdater.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Ezhu.AutoUpdater.UI
{
    public partial class DownCmpProcess : WindowBase
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
        private const string COMPONENTFILENAME = "components.json";
        private const string COMPONENTFOLDERNAME = "components";
        /// <summary>
        /// 服务器根目录
        /// </summary>
        private string url;

        public DownCmpProcess(string callExeName, string updateFileDir, string appDir, string appVersion, string url, bool isTest)
        {
            InitializeComponent();
            Loaded += (s, o) =>
            {
                initControl();
                checkLocalFiles();
                downloadComponentFile();
            };
            this.callExeName = callExeName;
            this.updateFileDir = updateFileDir;
            this.appDir = appDir;
            this.appVersion = appVersion;
            getRemoteRootUrl(url);
            desc = File.ReadAllText(Path.Combine(this.updateFileDir, "describe.txt"));
            appName = this.callExeName;
        }

        public DownCmpProcess(string callExeName, string updateFileDir, string appDir, string appVersion, string url)
        {
            InitializeComponent();
            Loaded += (s, o) =>
            {
                initControl();
                checkLocalFiles();
                downloadComponentFile();
            };
            this.callExeName = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(callExeName));
            this.updateFileDir = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(updateFileDir));
            this.appDir = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(appDir));
            this.appVersion = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(appVersion));
            string remoteFilePath = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(url));
            getRemoteRootUrl(remoteFilePath);
            desc = File.ReadAllText(Path.Combine(this.updateFileDir, "describe.txt"));
            appName = this.callExeName;
        }

        private void checkLocalFiles()
        {
            var curCmpFile = Path.Combine(appDir, COMPONENTFILENAME);
            if (File.Exists(curCmpFile))
            {
                var curCmpInfo = JsonUtil.DeserializeToFile<Component>(curCmpFile);
                var components = curCmpInfo.components.ToList() ;
                for (int i = 0; i < components.Count;)
                {
                    var cmpPath = Path.Combine(appDir, components[i].name);
                    if (File.Exists(cmpPath))
                    {
                        var md5 = CommonUtil.GetMD5Hash(cmpPath);
                        if (md5 != components[i].md5)
                            components[i].md5 = md5;
                        i++;
                    }
                    else
                        components.RemoveAt(i);
                }
                curCmpInfo.components = components.ToArray();
                var info = JsonUtil.Serialize(curCmpInfo);
                File.WriteAllText(curCmpFile, info);
            }
        }

        private void getRemoteRootUrl(string remoteFilePath)
        {
            string[] split = remoteFilePath.Split(new char[2] { '\\', '/' });
            for (int i = 0; i < split.Length - 1; i++)
                url += split[i] + '/';
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
            var localCmpPath = Path.Combine(updateFileDir, COMPONENTFOLDERNAME);
            var curDownloadList = new List<string>(ComponentHelper.Downloads);
            var completeDownloadList = new List<string>();
            if (!Directory.Exists(localCmpPath))
                Directory.CreateDirectory(localCmpPath);
            var client = new WebClient();
            client.DownloadProgressChanged += (sender, e) =>
            {
                UpdateProcess(e.BytesReceived, e.TotalBytesToReceive);
            };
            client.DownloadDataCompleted += (sender, e) =>
            {
                if (e.Error == null)
                {
                    createNewDirectionary(localCmpPath, curDownloadList[0]);
                    string filePath = Path.Combine(localCmpPath, curDownloadList[0]);
                    byte[] data = e.Result;
                    BinaryWriter writer = new BinaryWriter(new FileStream(filePath, FileMode.OpenOrCreate));
                    writer.Write(data);
                    writer.Flush();
                    writer.Close();
                    try
                    {
                        completeDownloadList.Add(curDownloadList[0]);
                        curDownloadList.RemoveAt(0);
                        if (curDownloadList.Count > 0)
                        {
                            var cmpPath = Path.Combine(url, COMPONENTFOLDERNAME, curDownloadList[0]);
                            client.DownloadDataAsync(new Uri(cmpPath));
                        }
                        else
                        {
                            //关闭客户端主程序
                            Dispatcher.Invoke(() =>
                            {
                                Process[] processes = Process.GetProcessesByName(callExeName);
                                if (processes.Length > 0)
                                {
                                    foreach (var p in processes)
                                        p.Kill();
                                }
                            });
                            Thread.Sleep(500);
                            //下载完了在整体替换
                            try
                            {
                                foreach (var item in completeDownloadList)
                                {
                                    createNewDirectionary(appDir, item);
                                    var targetFilePath = Path.Combine(appDir, item);
                                    var sourceFilePath = Path.Combine(localCmpPath, item);
                                    File.Copy(sourceFilePath, targetFilePath, true);
                                }
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("文件更新异常，升级失败");
                            }
                            //更新状态
                            updateComplete();
                            try
                            {
                                string newCmpFile = Path.Combine(updateFileDir, COMPONENTFILENAME);
                                var curCmpFile = Path.Combine(appDir, COMPONENTFILENAME);
                                ComponentHelper.CopyCmpFile(newCmpFile, curCmpFile);
                                //清空缓存文件夹
                                string rootUpdateDir = updateFileDir.Substring(0, updateFileDir.LastIndexOf(Path.DirectorySeparatorChar));
                                foreach (string p in Directory.EnumerateDirectories(rootUpdateDir))
                                {
                                    if (!p.ToLower().Equals(updateFileDir.ToLower()))
                                        Directory.Delete(p, true);
                                }
                            }
                            catch (Exception) { }
                        }
                    }
                    catch (Exception ex)
                    {
                        var msg = string.Format("具体原因为:{0}\r\n请重新下载或登陆官方网站下载最新版本", ex);
                        MessageBox.Show(msg, "更新出现异常");
                    }
                }
                else
                {
                    var str = string.Format("具体原因为:{0}", e.Error.Message);
                    MessageBox.Show(str, "更新出现异常");
                }
            };
            var remoteCmpPath = Path.Combine(url, COMPONENTFOLDERNAME, curDownloadList[0]);
            client.DownloadDataAsync(new Uri(remoteCmpPath));
        }

        private void createNewDirectionary(string localCmpPath, string fileName)
        {
            string[] split = fileName.Split(new char[2] { '\\', '/' });
            if (split.Length > 1)
            {
                int i = 0;
                string dirPath = localCmpPath;
                //创建下载文件目录
                while (split.Length - 1 > i)
                {
                    dirPath = Path.Combine(dirPath, split[i]);
                    if (!Directory.Exists(dirPath))
                        Directory.CreateDirectory(dirPath);
                    i++;
                }
            }
        }

        private void downloadComponentFile()
        {
            var client = new WebClient();
            string newCmpFile = Path.Combine(updateFileDir, COMPONENTFILENAME);
            var curCmpFile = Path.Combine(appDir, COMPONENTFILENAME);
            client.DownloadDataCompleted += (sender, e) =>
            {
                byte[] data = e.Result;
                var fs = new FileStream(newCmpFile, FileMode.OpenOrCreate);
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    writer.Write(data);
                    writer.Flush();
                    writer.Close();
                }
                if (!ComponentHelper.GetDownloadAndDeleteCmps(newCmpFile, curCmpFile))
                {
                    deleteNonFuncFile();
                    Close();
                }
            };
            string remoteCmpPath = Path.Combine(url, COMPONENTFILENAME);
            client.DownloadDataAsync(new Uri(remoteCmpPath));
        }

        private void initControl()
        {
            string newCmpFile = Path.Combine(updateFileDir, COMPONENTFILENAME);
            var curCmpFile = Path.Combine(appDir, COMPONENTFILENAME);
            YesButton.Content = "现在更新";
            NoButton.Content = "暂不更新";
            YesButton.Click += (o, ex) =>
            {
                startSp.Visibility = Visibility.Hidden;
                processGd.Visibility = Visibility.Visible;
                deleteNonFuncFile();
                downloadUpdateFile();
            };
            borderTitle.MouseMove += (sender, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    DragMove();
            };
            NoButton.Click += (o, ex) => Close();
            CloseButton.Click += (sender, e) => Close();
            EndBtn.Click += EndBtn_Click;
            title.Text = "发现新版本（" + appVersion + "），是否现在更新？";
            listBox.ItemsSource = CommonUtil.GetUpdateInfos(desc);
        }

        private void EndBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string exePath = Path.Combine(appDir, callExeName + ".exe");
                var info = new ProcessStartInfo(exePath);
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

        private void deleteNonFuncFile()
        {
            if (ComponentHelper.Deletes.Count > 0)
            {
                string filePath = null;
                foreach (var name in ComponentHelper.Deletes)
                {
                    filePath = Path.Combine(appDir, name);
                    if (File.Exists(filePath))
                        File.Delete(filePath);
                }
            }
        }

        private static void pasteDirectory(string sourcePath, string desPath)
        {
            if (!Directory.Exists(desPath))
            {
                Directory.CreateDirectory(desPath);
                Console.WriteLine("创建文件夹{0}", desPath);
            }
            try
            {
                foreach (string entry in Directory.GetFileSystemEntries(sourcePath))
                {
                    FileInfo fi = new FileInfo(entry);
                    if (fi.Attributes == FileAttributes.Directory)
                    {
                        var appEntryDir = Path.Combine(desPath, fi.Name);
                        Console.WriteLine("进入文件夹{0}", appEntryDir);
                        pasteDirectory(entry, appEntryDir);
                    }
                    else
                    {
                        if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                            fi.Attributes = FileAttributes.Normal;
                        var entryName = Path.GetFileName(entry);
                        var appEntryName = Path.Combine(desPath, entryName);
                        File.Copy(entry, appEntryName, true);
                        Console.WriteLine("复制文件从{0}到{1}", entry, appEntryName);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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

        private void UpdateProcess(long current, long total)
        {
            Dispatcher.Invoke(() =>
            {
                string status = (int)((float)current * 100 / (float)total) + "%";
                txtProcess.Text = status;
                rectProcess.Width = ((float)current / (float)total) * bProcess.ActualWidth;
            });
        }

        /// <summary>
        /// 文件是否被占用
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool IsFileInUse(string path)
        {
            if (!File.Exists(path))
                return false;

            bool inUse = true;
            FileStream fs = null;
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
                inUse = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
            }
            return inUse;
        }
    }
}
