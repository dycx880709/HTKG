using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SC_AnalysisSystem_Common
{
    public class ObjectUtil
    {
        public static object Copy(object o)
        {
            Type t = o.GetType();
            PropertyInfo[] properties = t.GetProperties();
            object p = t.InvokeMember("", BindingFlags.CreateInstance, null, o, null);
            foreach (PropertyInfo pi in properties)
            {
                if (pi.CanWrite)
                {
                    object value = pi.GetValue(o, null);
                    pi.SetValue(p, value, null);
                }
            }
            return p;
        }
    }
}
