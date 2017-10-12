using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;

namespace SC_AnalysisSystem_Common
{
    public class FileUtil
    {
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path"></param>
        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 清空目录
        /// http://blog.sina.com.cn/s/blog_4c6e822d0102driy.html
        /// </summary>
        /// <param name="path"></param>
        public static void ClearDirectory(string path)
        {
            if (!Directory.Exists(path))
                return;

            try
            {
                foreach (string d in Directory.GetFileSystemEntries(path))
                {
                    //Console.WriteLine(d);
                    if (File.Exists(d))
                    {
                        FileInfo fi = new FileInfo(d);
                        if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                            fi.Attributes = FileAttributes.Normal;

                        File.Delete(d);
                    }
                    else
                    {
                        ClearDirectory(d);
                        Directory.Delete(d);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 静默删除文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool QuietDelete(string filePath)
        {
            if (filePath == null)
                return true;
            if (!File.Exists(filePath))
                return true;
            try
            {
                File.Delete(filePath);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// 目录是否可写
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsDirectoryWritable(string path)
        {
            var name = "判断目录是否可写生成的测试文件名";
            var filepath = Path.Combine(path, name);
            FileUtil.QuietDelete(filepath);
            try
            {
                File.Create(filepath).Close();
                File.Delete(filepath);
                return true;
            }
            catch
            {
            }
            return false;
        }

        /// <summary>
        /// 文件是否被占用
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsFileInUse(string path)
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

        /// <summary>
        /// 判断目录相等
        /// http://stackoverflow.com/questions/2281531/how-can-i-compare-directory-paths-in-c
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        public static bool AreSameDirectories(string path1, string path2)
        {
            return string.Compare(
                Path.GetFullPath(path1).TrimEnd('\\'),
                Path.GetFullPath(path2).TrimEnd('\\'),
                StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        /// <summary>
        /// 判断两个文件内容是否相同
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        public static bool SameContents(string path1, string path2)
        {
            try
            {
                var buf1 = File.ReadAllBytes(path1);
                var buf2 = File.ReadAllBytes(path2);
                return ArrayUtil.Equals(buf1, buf2);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 文件移动目录
        /// </summary>
        /// <param name="srcPath"></param>
        /// <param name="dstDir"></param>
        public static void MoveToDir(string srcPath, string dstDir)
        {
            var filename = Path.GetFileName(srcPath);
            var dstPath = Path.Combine(dstDir, filename);
            if (File.Exists(dstPath))
            {
                File.Delete(dstPath);
            }
            File.Move(srcPath, dstPath);
        }

        public static void CopyFile(string srcPath, string dstDir)
        {
            var filename = Path.GetFileName(srcPath);
            var dstPath = Path.Combine(dstDir, filename);
            if (File.Exists(dstPath))
            {
                File.Delete(dstPath);
            }
            File.Copy(srcPath, dstPath);
        }
    }
}
