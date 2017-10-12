using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SC_AnalysisSystem_Common
{
    public class BitmapUtil
    {
        public static System.Windows.Size GetRect(string imgUri)
        {
            using (Bitmap bitmap = new Bitmap(imgUri))
            {
                System.Windows.Size size = new System.Windows.Size();
                size.Height = bitmap.Height;
                size.Width = bitmap.Width;
                bitmap.Dispose();
                return size;
            }
        }

        public static Bitmap GetBitmap(string imgUri)
        {
            return new Bitmap(imgUri);
        }
    }
}
