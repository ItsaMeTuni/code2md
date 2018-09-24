﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using System.Runtime.CompilerServices;

namespace XMLDocGen
{
    /// <summary>
    /// This class has miscellaneous helper functions.
    /// </summary>
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

        /// <summary>
        /// Does the same as Regex.Replace but on a whole array.
        /// </summary>
        /// <param name="_arr">The array to operate on</param>
        /// <param name="_regex">The regular expression</param>
        /// <param name="_replace">What to put in the place of a regex match</param>
        public static string[] RegexReplaceOnArray(string[] _arr, string _regex, string _replace)
        {
            string[] outArr = new string[_arr.Length];

            for (int i = 0; i < outArr.Length; i++)
            {
                outArr[i] = Regex.Replace(_arr[i], _regex, _replace);
            }

            return outArr;
        }

        /// <summary>
        /// Gets the Regex Match value from each string on an array
        /// </summary>
        /// <param name="_arr">The array to operate on</param>
        /// <param name="_regex">The regular expression</param>
        public static string[] RegexMatchOnArray(string[] _arr, string _regex)
        {
            string[] outArr = new string[_arr.Length];

            for (int i = 0; i < outArr.Length; i++)
            {
                outArr[i] = Regex.Match(_arr[i], _regex).Value;
            }

            return outArr;
        }

        /// <summary>
        /// Cleans a string (i.e. removes new lines, removes consecutive spacesm removes spaces at the start of the string)
        /// </summary>
        /// <param name="_in">The string you want to clean</param>
        public static string CleanString(this string _in)
        {
            _in = Regex.Replace(_in, @"\t|\n|\r", ""); //Remove new lines
            _in = Regex.Replace(_in, @"[ ]{2,}", " "); //Remove consecutive spaces
            _in = Regex.Replace(_in, @"^\s", ""); //Remove space at the start of the string
            return _in;
        }

        public static string RemoveFirstAndLastNewLine(this string _in)
        {
            _in = Regex.Replace(_in, @"^\r?\n?|\r?\n?$", "", RegexOptions.Singleline);

            return _in;
        }

        /// <summary>
        /// Cehcks if a string is null or empty
        /// </summary>
        /// <param name="_string">The string to check</param>
        /// <returns></returns>
        public static bool IsEmpty(this string _string)
        {
            return _string == null || _string == "";
        }

        /// <summary>
        /// Checks if a member was generated by the compiler
        /// </summary>
        /// <param name="_member">Member to check</param>
        public static bool IsCompilerGenerated(this MemberInfo _member)
        {
            return _member.GetCustomAttribute(typeof(CompilerGeneratedAttribute)) != null;
        }

        /// <summary>
        /// Checks if the type _type is declared in the assembly we're reading
        /// </summary>
        /// <param name="_type">Type to check</param>
        public static bool IsFromAssembly(this Type _type, Assembly _assembly)
        {
            return _type.Assembly == _assembly;
        }

        public static string RemoveNamePrefix(string _name)
        {
            return _name.Remove(0, 2);
        }

        public static string Str(this Tags _tag, bool _regex = true)
        {
            return _regex ? Regex.Escape("$" + _tag + "$") : "$" + _tag + "$";
        }

        public static string CondStr(this Tags _tag, bool _regex = true)
        {
            return _regex ? Regex.Escape("?" + _tag + "?") : "?" + _tag + "?";
        }

        public static string MarkdownEscape(this string _text)
        {
            return MarkdownBuilder.Escape(_text);
        }
    }
}
