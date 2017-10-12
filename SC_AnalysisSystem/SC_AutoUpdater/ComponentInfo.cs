using AutoUpdater;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ezhu.AutoUpdater
{
    [DataContract]
    public class ComponentInfo
    {
        [DataMember(IsRequired = true)]
        public string name { get; set; }

        [DataMember(IsRequired = false)]
        public string md5 { get; set; }
    }

    [DataContract]
    public class Component
    {
        [DataMember(IsRequired = true)]
        public ComponentInfo[] components { get; set; }
    }

    public class ComponentHelper
    {
        public static List<string> Downloads { get; set; } // 下载文件列表

        public static List<string> Deletes { get; set; } // 删除文件列表

        static ComponentHelper()
        {
            Downloads = new List<string>();
            Deletes = new List<string>();
        }

        public static bool GetDownloadAndDeleteCmps(string newCmpFile, string curCmpFile)
        {
            if (!File.Exists(newCmpFile)) return false;
            var newCmpInfo = JsonUtil.DeserializeToFile<Component>(newCmpFile);
            // 下载删除组件判定 最新componets数组，转为字典
            Dictionary<string, ComponentInfo> newestComponentsDict = new Dictionary<string, ComponentInfo>();
            foreach (var component in newCmpInfo.components)
                newestComponentsDict.Add(component.name, component);
            if (File.Exists(curCmpFile))
            {
                var curComponent = JsonUtil.DeserializeToFile<Component>(curCmpFile);
                // 遍历旧表，计算更新和删除项
                foreach (var component in curComponent.components)
                {
                    ComponentInfo cmpInfo = null;
                    // 如果在新表中
                    if (newestComponentsDict.TryGetValue(component.name, out cmpInfo))
                    {
                        // 有新版本，加入下载列表 
                        // 新的版本校验机制将对比md5码
                        if ((!(string.IsNullOrEmpty(component.md5)) && !cmpInfo.md5.Equals(component.md5)))
                            Downloads.Add(component.name);
                        newestComponentsDict.Remove(component.name);  // 从新表字典中移除
                    }
                    else
                        Deletes.Add(component.name); // 不在新表中，加入删除列表
                }
                // 新表字典中剩余项都是新组件，都加入下载列表
                foreach (var newest in newestComponentsDict)
                    Downloads.Add(newest.Key);
            }
            else
            {
                // 新表字典中剩余项都是新组件，都加入下载列表
                foreach (var newest in newestComponentsDict)
                    Downloads.Add(newest.Key);
            }
            return Downloads.Count > 0;
        }

        public static void CopyCmpFile(string newCmpFile, string curCmpFile)
        {
            File.Copy(newCmpFile, curCmpFile, true);
        }
    }
}
