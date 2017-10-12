using SC_AnalysisSystem.Resources.Controls;
using SC_AnalysisSystem_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SC_AnalysisSystem.Helper
{
    public class VersionEventArgs : EventArgs
    {
        public string message;
        public VersionEventArgs(string message)
        {
            this.message = message;
        }
    }

    public class UpdateHelper
    {
        // 开始时间
        private DateTime startTime;

        // 加载界面最短显示时间，一闪而过用户体验不好
        private readonly int delayTime;

        private readonly string TAG;

        public EventHandler<VersionEventArgs> UpdateCompleteEvent;

        public UpdateHelper(string _tag, int _delayTime)
        {
            startTime = DateTime.Now;
            TAG = _tag;
            delayTime = _delayTime;
        }

        public bool CheckClientVersion(bool isLoading = false)
        {
            var updateName = Updater.GetProcessName();
            if (WPFAppUtil.ForceOneAppInstance(updateName) || isLoading)
            {
                Console.WriteLine(ApiConfig.VERSION_JSON_URL);
                var proxy = new ApiProxyHttpGet(ApiConfig.VERSION_JSON_URL, null);
                responseProductVersion(proxy, proxy.SyncResult);
                return true;
            }
            else
                return false;
        }

        private void responseProductVersion(object sender, ApiResultEventArgs e)
        {
            if (e.ErrorInfo != null)
            {
                var msg = string.Format("获取版本失败,原因为:{0}", e.ErrorInfo.errorMsg);
                MessageBoxX.Warning(msg);
                return;
            }
            TimeSpan timeSpan = DateTime.Now - startTime;
            int dt = delayTime - (int)timeSpan.TotalMilliseconds;
            if (dt > 0)
            {
                Console.WriteLine("{0} sleep {1} ms", TAG, dt);
                Thread.Sleep(dt);
            }
            processResult(e.Result as string);
        }

        private void processResult(string text)
        {
            if (text != null)
            {
                try
                {
                    var versionInfo = JsonUtil.Deserialize<VersionInfo>(text);
                    int[] ns1 = parseVersionString(versionInfo.version);
                    int[] ns2 = parseVersionString(App.ProductVersion);
                    int[] ns3 = parseVersionString(versionInfo.requiredMinVersion);
                    if (isVersionCompare(ns1, ns2))
                    {
                        Console.WriteLine("发现可升级版本，当前版本{0},目标版本{1}", App.ProductVersion, versionInfo.version);
                        Updater.CheckUpdateStatus(App.AppDataPath, ApiConfig.VERSION_JSON_URL, !App.Config.ignoreCmpUpdate);
                        UpdateCompleteEvent?.Invoke(this, new VersionEventArgs(string.Empty));
                    }
                    else
                        UpdateCompleteEvent?.Invoke(this, new VersionEventArgs("当前版本已是最新版本"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    var msg = string.Format("获取版本失败,原因为:{0}", ex.Message);
                    MessageBoxX.Question(msg);
                    UpdateCompleteEvent?.Invoke(this, new VersionEventArgs("获取版本失败，请稍后再试"));
                }
            }
            else
                UpdateCompleteEvent?.Invoke(this, new VersionEventArgs(string.Empty));
        }

        private int[] parseVersionString(string text)
        {
            string[] ss = text.Split('.');
            int n = ss.Length;
            int[] ns = new int[n];
            for (int i = 0; i < n; i++)
            {
                ns[i] = int.Parse(ss[i]);
            }
            return ns;
        }

        private bool isVersionCompare(int[] news, int[] olds)
        {
            int n1 = news.Length;
            int n2 = olds.Length;
            int n = Math.Min(n1, n2);

            for (int i = 0; i < n; i++)
            {
                int d = news[i] - olds[i];
                if (d > 0)
                    return true;
                else if (d < 0)
                    return false;
            }

            return (n1 > n2);
        }
    }
}
