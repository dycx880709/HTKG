using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace YJYTemplate.Common
{
    public static partial class Win32
    {
        /// <summary>
        /// 加载动态链接库
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public extern static IntPtr LoadLibrary(string path);

        /// <summary>
        /// 卸载动态链接库
        /// </summary>
        /// <param name="hDll"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public extern static bool FreeLibrary(IntPtr hDll);
    }
}
