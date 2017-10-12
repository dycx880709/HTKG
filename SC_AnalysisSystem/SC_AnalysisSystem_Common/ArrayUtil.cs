using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SC_AnalysisSystem_Common
{
    public static class ArrayUtil
    {
        public static T[] ExtractPropertyValueArray<T>(object[] objects, string propertyName)
        {
            int count = objects.Length;
            T[] values = new T[count];
            PropertyInfo propertyInfo = objects[0].GetType().GetProperty(propertyName);
            for (int i = 0; i < count; i++)
            {
                values[i] = (T)propertyInfo.GetValue(objects[i], null);
            }
            return values;
        }

        public static bool IsNullOrEmpty(object[] array)
        {
            return ((array == null) || (array.Length <= 0));
        }

        // 合并数组
        public static T[] Merge<T>(T[] arr1, T[] arr2)
        {
            int len1 = arr1.Length;
            int len2 = arr2.Length;
            var arr = new T[len1 + len2];
            Array.Copy(arr1, arr, len1);
            Array.Copy(arr2, 0, arr, len1, len2);
            return arr;
        }

        // 对象合并到数组前
        public static T[] Merge<T>(T obj, T[] arr2)
        {
            int len2 = arr2.Length;
            var arr = new T[1 + len2];
            arr[0] = obj;
            Array.Copy(arr2, 0, arr, 1, len2);
            return arr;
        }

        // 对象合并到数组后
        public static T[] Merge<T>(T[] arr1, T obj)
        {
            int len1 = arr1.Length;
            var arr = new T[len1 + 1];
            Array.Copy(arr1, arr, len1);
            arr[len1] = obj;
            return arr;
        }

        /// <summary>
        /// 移动一个元素，其他元素保持先后顺序不变
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static void Move<T>(T[] arr, int from, int to)
        {
            var tmp = arr[from];
            if (from < to)
            {
                for (int i = from; i < to; i++)
                    arr[i] = arr[i+1];
            }
            else if (from > to)
            {
                for (int i = from; i > to; i--)
                    arr[i] = arr[i-1];
            }
            arr[to] = tmp;
        }

        public static int _Equals<T>(T[] a1, T[] a2)
        {
            if (a1 == null)
                return (a2 == null) ? 1 : 0;
            else if (a2 == null)
                return 0;
            else if (a1 == a2)
                return 1;
            else if (a1.Length != a2.Length)
                return 0;
            else
                return -1; // undetermined
        }

        /// <summary>
        /// 比较字节数组内容是否相同
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <returns></returns>
        public static bool Equals(byte[] a1, byte[] a2)
        {
            int f = _Equals(a1, a2);
            if (f == 0)
                return false;
            else if (f == 1)
                return true;
            else
            {
                for (int i = 0; i < a1.Length; ++i)
                {
                    if (a1[i] != a2[i])
                        return false;

                    a1[i] = a2[i];
                }
                return true;
            }
        }
    }
}
