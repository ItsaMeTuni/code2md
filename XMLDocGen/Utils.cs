using System;
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
        static Dictionary<Type, string> customTypeNames = new Dictionary<Type, string>()
        {
            { typeof(String), "string" },
            { typeof(Boolean), "bool" },
            { typeof(Single), "float" },
            { typeof(Int32), "int" },
        };

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

        /// <summary>
        /// Searches for a method xml node with name _methodName (ignoring parameters and prefix)
        /// </summary>
        /// <param name="_nodeList">The xml node list to search in</param>
        /// <param name="_methodName">The name of the method you're looking for</param>
        public static XmlNode FindMethodMemberWithName(this XmlNodeList _nodeList, string _methodName)
        {
            for (int i = 0; i < _nodeList.Count; i++)
            {
                if (Extractor.GetMethodName(_nodeList[i].Attributes["name"].Value) == _methodName)
                {
                    return _nodeList[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Searches for an xml node with name _memberName
        /// </summary>
        /// <param name="_nodeList">The xml node list to search in</param>
        /// <param name="_memberName">The name of the member you're looking for</param>
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

        /// <summary>
        /// Searches for a field xml node with name _fieldName (ignoring prefix)
        /// </summary>
        /// <param name="_nodeList">The xml node list to search in</param>
        /// <param name="_fieldName">The name of the field you're looking for</param>
        public static XmlNode FindFieldMemberWithName(this XmlNodeList _nodeList, string _fieldName)
        {
            for (int i = 0; i < _nodeList.Count; i++)
            {
                if (Extractor.GetFieldName(_nodeList[i].Attributes["name"].Value) == _fieldName)
                {
                    return _nodeList[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Gets _type's name (if there's a custom name for this type the custom name will be returned)
        /// </summary>
        /// <param name="_type">The _type you want to get the name from</param>
        public static string GetTypeNameMarkdownText(this Type _type)
        {
            string str = "";

            if(customTypeNames.ContainsKey(_type))
            {
                str =  customTypeNames[_type];
            }
            else
            {
                str =  GetReadableGenericTypeName(_type);
            }

            str = MarkdownHelper.CreateCode(str);

            if(_type.IsFromAssembly(Program.assembly))
            {
                str = MarkdownHelper.CreateLink(str, _type.FullName, 1);
            }

            return str;
        }

        /// <summary>
        /// Gets the name of a generic type like List&lt;string&gt; instead of List`1
        /// </summary>
        /// <remarks>
        /// If _type is not a generic type then the output will be _type.Name
        /// </remarks>
        /// <param name="_type">The generic type name</param>
        /// <returns></returns>
        private static string GetReadableGenericTypeName(Type _type)
        {
            if (!_type.IsGenericType)
            {
                return _type.Name;
            }

            string genericTypeName = _type.GetGenericTypeDefinition().Name;

            genericTypeName = genericTypeName.Substring(0, genericTypeName.IndexOf('`'));

            string genericArgs = string.Join(",", _type.GetGenericArguments().Select(ta => GetReadableGenericTypeName(ta)).ToArray());

            return genericTypeName + "<" + genericArgs + ">";
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
    }
}
