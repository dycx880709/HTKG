using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Management;
using System.Text;
using System.Windows;

namespace SC_AnalysisSystem_Common
{
    public class SystemUtil
    {
        /// <summary>
        /// 检查系统是否满足要求
        /// </summary>
        /// <returns></returns>
        public bool CheckNecessarySytem()
        {
            OperatingSystem osInfo = Environment.OSVersion;
            if (osInfo.Version > new Version("6.1") && osInfo.Version < new Version("6.2"))
            {
                if (string.IsNullOrEmpty(osInfo.ServicePack))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 判断系统为32位还是64位
        /// </summary>
        /// <returns></returns>
        public string Distinguish64or32System()
        {
            try
            {
                string addressWidth = string.Empty;
                ConnectionOptions mConnOption = new ConnectionOptions();
                ManagementScope mMs = new ManagementScope(@"\\localhost", mConnOption);
                ObjectQuery mQuery = new ObjectQuery("select AddressWidth from Win32_Processor");
                ManagementObjectSearcher mSearcher = new ManagementObjectSearcher(mMs, mQuery);
                ManagementObjectCollection mObjectCollection = mSearcher.Get();
                foreach (ManagementObject mObject in mObjectCollection)
                    addressWidth = mObject["AddressWidth"].ToString();
                return addressWidth;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// 检查是否存在.Net Framework4.6.1
        /// </summary>
        /// <returns></returns>
        public bool CheckFor461DotVersion()
        {
            if (GetFramework461Registry() < 394254)
                return false;
            return true;
        }

        /// <summary>
        /// 获取注册表中Framework Release值
        /// </summary>
        /// <returns></returns>
        private int GetFramework461Registry()
        {
            var frameworkUrl = @"SOFTWARE\MICROSOFT\NET Framework Setup\NDP\v4\Full\";
            var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(frameworkUrl);
            int releaseKey = Convert.ToInt32(ndpKey.GetValue("Release"));
            ndpKey.Close();
            return releaseKey;
        }
    }
}
