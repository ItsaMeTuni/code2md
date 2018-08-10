using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

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

        public static string CleanString(this string _in)
        {
            _in = Regex.Replace(_in, @"\t|\n|\r", ""); //Remove new lines
            _in = Regex.Replace(_in, @"[ ]{2,}", " "); //Remove consecutive spaces
            _in = Regex.Replace(_in, @"^\s", ""); //Remove space at the start of the string

            return _in;
        }

        public static XmlNode FindMethodMemberWithName(this XmlNodeList _nodeList, string _membeName)
        {
            for (int i = 0; i < _nodeList.Count; i++)
            {
                if (Extractor.GetMethodName(_nodeList[i].Attributes["name"].Value) == _membeName)
                {
                    return _nodeList[i];
                }
            }

            return null;
        }

        public static XmlNode FindMemberWithName(this XmlNodeList _nodeList, string _memberName)
        {
            for (int i = 0; i < _nodeList.Count; i++)
            {
                if (_nodeList[i].Attributes["name"].Value == _memberName)
                {
                    return _nodeList[i];
                }
            }

            return null;
        }
    }
}
