using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace SC_AnalysisSystem_Common
{
    public class ConfigUtil
    {
        /// <summary>
        /// 从配置文件中读取配置参数
        /// </summary>
        /// <typeparam name="T">能实例化的类型</typeparam>
        /// <param name="obj">配置赋值的对象</param>
        /// <param name="path">配置文件路径</param>
        public static void Load<T>(T obj, string path = null) where T : new()
        {
            Configuration config;
            if (string.IsNullOrEmpty(path) && !File.Exists(path))
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            else
                config = ConfigurationManager.OpenExeConfiguration(path);
            var setting = config.AppSettings.Settings;
            var type = obj.GetType();
            foreach (var property in type.GetProperties())
            {
                if (config.AppSettings.Settings[property.Name] != null && !string.IsNullOrEmpty(config.AppSettings.Settings[property.Name].Value))
                    property.SetValue(obj, Convert.ChangeType(config.AppSettings.Settings[property.Name].Value, property.PropertyType), null);
            }
        }

        /// <summary>
        /// 从配置文件中读取配置参数
        /// </summary>
        /// <param name="propertyName">配置赋值的对象名称</param>
        /// <param name="path">配置文件路径</param>
        public static string Load(string propertyName, string path = null)
        {
            Configuration config;
            if (string.IsNullOrEmpty(path) && !File.Exists(path))
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            else
                config = ConfigurationManager.OpenExeConfiguration(path);
            var setting = config.AppSettings.Settings;
            if (config.AppSettings.Settings[propertyName] != null && !string.IsNullOrEmpty(config.AppSettings.Settings[propertyName].Value))
                return config.AppSettings.Settings[propertyName].Value;
            return null;
        }
    }
}
