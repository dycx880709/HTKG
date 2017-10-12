using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SC_AnalysisSystem_Common
{
    public static class HttpUtil
    {
        public const string GET = "GET";
        public const string POST = "POST";

        public const string TWO_HYPHENS = "--";
        public const string LINE_END = "\r\n";

        public static string MakeFileFormDataHead(string boundary, string name, string filename, string contentType)
        {
            // --<boundary>
            // Content-Disposition: form-data; name="<name>"; filename="<filename>"
            // Content-Type: <contentType>
            // 
            StringBuilder sb = new StringBuilder();
            sb.Append(TWO_HYPHENS);
            sb.Append(boundary);
            sb.Append(LINE_END);
            sb.Append("Content-Disposition: form-data; name=\"");
            sb.Append(name);
            sb.Append("\"; filename=\"");
            sb.Append(filename);
            sb.Append("\"");
            sb.Append(LINE_END);
            sb.Append("Content-Type: ");
            sb.Append(contentType);
            sb.Append(LINE_END);
            sb.Append(LINE_END);
            return sb.ToString();
        }

        public static string MakeLastBoundaryLine(string boundary)
        {
            // --<boundary>--
            StringBuilder sb = new StringBuilder();
            sb.Append(TWO_HYPHENS);
            sb.Append(boundary);
            sb.Append(TWO_HYPHENS);
            sb.Append(LINE_END);
            return sb.ToString();
        }

        public static string MakeBoundary()
        {
            return "----------" + DateTime.Now.Ticks.ToString("x");
        }

        public static string MakeUrlEncodedBody(Dictionary<string, string> arguments)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var pair in arguments)
            {
                sb.Append(pair.Key);
                sb.Append("=");
                sb.Append(pair.Value);
                sb.Append("&");
            }
            sb.Remove(sb.Length - 1, 1); // 移除最后一个'&'
            return sb.ToString();
        }

        public static string AppendMoreQuery(string url, Dictionary<string, string> arguments)
        {
            StringBuilder sb = new StringBuilder(url);
            foreach (var pair in arguments)
            {
                sb.Append("&");
                sb.Append(pair.Key);
                sb.Append("=");
                sb.Append(pair.Value);
            }
            return sb.ToString();
        }
    }
}
