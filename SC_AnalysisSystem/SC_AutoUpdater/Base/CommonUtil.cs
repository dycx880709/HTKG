using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ezhu.AutoUpdater.Base
{
    public class CommonUtil
    {
        public static void UnZipFile(string zipFilePath, string targetDir)
        {
            ICCEmbedded.SharpZipLib.Zip.FastZipEvents evt = new ICCEmbedded.SharpZipLib.Zip.FastZipEvents();
            ICCEmbedded.SharpZipLib.Zip.FastZip fz = new ICCEmbedded.SharpZipLib.Zip.FastZip(evt);
            fz.ExtractZip(zipFilePath, targetDir, "");
        }

        public static void CopyDirectory(string sourceDirName, string destDirName)
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

        public static List<UpdateText> GetUpdateInfos(string describe)
        {
            var Infos = new List<UpdateText>();
            if (!string.IsNullOrEmpty(describe))
            {
                string[] split = describe.Split('\n');
                foreach (var item in split)
                    Infos.Add(new UpdateText(item.Trim()));
            }
            return Infos;
        }

        /// <summary>
        /// 计算文件的MD5码
        /// </summary>
        /// <param name="pathName"></param>
        /// <returns></returns>
        public static string GetMD5Hash(string pathName)
        {
            string strResult = "";
            byte[] arrbytHashValue;
            FileStream oFileStream = null;
            MD5CryptoServiceProvider oMD5Hasher = new MD5CryptoServiceProvider();
            try
            {
                oFileStream = new FileStream(pathName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                //计算指定Stream 对象的哈希值
                arrbytHashValue = oMD5Hasher.ComputeHash(oFileStream);
                oFileStream.Close();
                //由以连字符分隔的十六进制对构成的String，其中每一对表示value 中对应的元素；例如“F-2C-4A”
                string strHashData = BitConverter.ToString(arrbytHashValue);
                strHashData = strHashData.Replace("-", ""); //替换-
                strResult = strHashData;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return strResult;
        }
    }
}
