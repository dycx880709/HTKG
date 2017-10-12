using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SC_AnalysisSystem_Common
{
    public static class MimeUtil
    {
        public const string MIME_TEXT_PLAIN = "text/plain";
        public const string MIME_IMAGE_JPEG = "image/jpeg";
        public const string MIME_DEFAULT = "application/octet-stream";

        // 映射表
        private static Dictionary<string, string> dict;

        // 初始化映射表
        private static void _InitMappingTable()
        {
            dict = new Dictionary<string, string>();
            dict.Add(".txt", MIME_TEXT_PLAIN);
            dict.Add(".jpg", MIME_IMAGE_JPEG);
            dict.Add(".jpeg", MIME_IMAGE_JPEG);
        }

        // 通过文件名获取MIME类型名
        public static string GetMimeType(string filename)
        {
            // 第一次调用时初始化映射表
            if (dict == null)
            {
                _InitMappingTable();
            }

            // 获取文件扩展名并转换为小写
            string ext = Path.GetExtension(filename).ToLower();

            // 查表
            string mime;
            dict.TryGetValue(ext, out mime);

            if (mime == null)
                return MIME_DEFAULT; // 查表失败，返回默认值
            else
                return mime;
        }
    }
}
