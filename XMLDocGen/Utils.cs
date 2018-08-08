using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Text.RegularExpressions;

namespace XMLDocGen
{
    public static class Utils
    {
        public static List<T> ListFieldToList<T, U>(this List<U> _list, string _fieldName)
        {
            List<T> outList = new List<T>();

            FieldInfo fieldInfo = typeof(U).GetField(_fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            for (int i = 0; i < _list.Count; i++)
            {
                outList.Add((T)fieldInfo.GetValue(_list[i]));
            }

            return outList;
        }

        public static string[] ToArray(this MatchCollection _collection)
        {
            string[] arr = new string[_collection.Count];

            for (int i = 0; i < _collection.Count; i++)
            {
                arr[i] = _collection[0].Value;
            }

            return arr;
        }

        public static string[] RegexReplaceOnArray(string[] _arr, string _regex, string _replace)
        {
            string[] outArr = new string[_arr.Length];

            for (int i = 0; i < outArr.Length; i++)
            {
                outArr[i] = Regex.Replace(_arr[i], _regex, _replace);
            }

            return outArr;
        }

        public static string[] RegexMatchOnArray(string[] _arr, string _regex)
        {
            string[] outArr = new string[_arr.Length];

            for (int i = 0; i < outArr.Length; i++)
            {
                outArr[i] = Regex.Match(_arr[i], _regex).Value;
            }

            return outArr;
        }
    }
}
