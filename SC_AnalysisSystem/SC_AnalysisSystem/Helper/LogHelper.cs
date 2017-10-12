using log4net;
using log4net.Appender;
using log4net.Config;
using SC_AnalysisSystem_Common;
using System;
using System.IO;
using System.Linq;
using YJYTemplate.Common;

namespace SC_AnalysisSystem.Helper
{
    /// <summary>
    /// log4net日志帮助类
    /// </summary>
    public class LogHelper
    {
        private static ILog ILog;

        public LogHelper(){ }

        static LogHelper()
        {
            XmlConfigurator.Configure();
            //clearLogCache();
            ILog = LogManager.GetLogger("Log");
        }

        /// <summary>
        /// 获取日志文件路径
        /// </summary>
        /// <returns></returns>
        private static string getLogFilePath()
        {
            var repository = LogManager.GetRepository();
            var appenders = repository.GetAppenders();
            var targetApder = appenders.First() as RollingFileAppender;
            if (string.IsNullOrEmpty(targetApder.File))
            {
                targetApder.File = Path.Combine(App.AppDataPath, "logs", "log.txt");
                targetApder.ActivateOptions();
            }
            return targetApder.File;
        }

        /// <summary>
        /// 清理日志缓存
        /// </summary>
        private static void clearLogCache()
        {
            string logFilePath = getLogFilePath();
            int threshold = 20;
            var curLogPath = Directory.GetParent(logFilePath).FullName;
            var files = Directory.GetFiles(curLogPath);
            if (!ArrayUtil.IsNullOrEmpty(files))
            {
                if (files.Length > threshold)
                {
                    Array.Sort(files);
                    int count = files.Length - threshold;
                    for (int i = 0; i < count; i++)
                        FileUtil.QuietDelete(files[i]);
                }
            }
        }

        /// <summary>
        /// 写普通信息
        /// </summary>
        /// <param name="msg">消息</param>
        public void WriteInfo(string msg)
        {
            ILog.Info(msg);
        }

        /// <summary>
        /// 写调试信息
        /// </summary>
        /// <param name="msg">消息</param>
        public void WriteDebug(string msg)
        {
            ILog.Debug(msg);
        }

        /// <summary>
        /// 写错误信息
        /// </summary>
        /// <param name="msg">消息</param>
        public void WriteError(string msg)
        {
            ILog.Error(msg);
        }

        /// <summary>
        /// 写错误信息
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">错误信息</param>
        public void WriteError(string msg, Exception ex)
        {
            ILog.Error(msg, ex);
        }
    }

}
