using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace XMLDocGen
{
    static class ReflectionToText
    {
        static Dictionary<Type, string> customTypeNames = new Dictionary<Type, string>()
        {
            { typeof(String), "string" },
            { typeof(Boolean), "bool" },
            { typeof(Single), "float" },
            { typeof(Int32), "int" },
        };

        /// <summary>
        /// Gets _type's name (if there's a custom name for this type the custom name will be returned)
        /// </summary>
        /// <param name="_type">The _type you want to get the name from</param>
        public static string GetTypeNameMarkdownText(this Type _type)
        {
            bool isArray = _type.IsArray;

            _type = _type = GetElementTypeRecursively(_type, out int typeWrapLevelCount);

            string str = "";

            if (customTypeNames.ContainsKey(_type))
            {
                str = customTypeNames[_type];
            }
            else
            {
                str = GetReadableGenericTypeName(_type);
            }

            if (isArray)
            {
                for (int i = 0; i < typeWrapLevelCount; i++)
                {
                    str += "[]";
                }
            }

            str = MarkdownBuilder.CreateCode(str);

            if (_type.IsFromAssembly(Program.assembly))
            {
                str = MarkdownBuilder.CreateLink(str, _type.FullName, 1);
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
        public static string GetReadableGenericTypeName(Type _type)
        {
            _type = GetElementTypeRecursively(_type);

            if (!_type.IsGenericType)
            {
                return _type.Name;
            }

            string genericTypeName = _type.GetGenericTypeDefinition().Name;

            genericTypeName = genericTypeName.Substring(0, genericTypeName.IndexOf('`'));

            string genericArgs = string.Join(",", _type.GetGenericArguments().Select(ta => GetReadableGenericTypeName(ta)).ToArray());

            return genericTypeName + "<" + genericArgs + ">";
        }

        public static string GetModifiersString(this ParameterInfo _param)
        {
            string str = "";

            if (_param.GetCustomAttribute<ParamArrayAttribute>() != null)
            {
                str += "params ";
            }

            if (_param.IsIn)
            {
                str += "in ";
            }
            else if (_param.IsOut)
            {
                str += "out ";
            }
            else if (_param.ParameterType.IsByRef)
            {
                str += "ref ";
            }

            if (str == "")
            {
                str = "---";
            }

            return str;
        }

        public static string GetModifiersString(this FieldInfo _field)
        {
            string str = "";

            if (_field.IsPublic)
            {
                str += "public ";
            }
            else if (_field.IsPrivate)
            {
                str += "private ";
            }
            else if (_field.IsFamily)
            {
                str += "protected ";
            }

            if (_field.IsStatic)
            {
                str += "static ";
            }

            if (_field.IsLiteral)
            {
                if (_field.IsInitOnly)
                {
                    str += "readonly ";
                }
                else
                {
                    str += "const ";
                }
            }

            if (str == "")
            {
                str = "---";
            }

            return str;
        }

        public static string GetModfiersString(this MethodInfo _method)
        {
            string str = "";

            if (_method.IsPublic)
            {
                str += "public ";
            }
            else if (_method.IsPrivate)
            {
                str += "private ";
            }
            else if (_method.IsFamily)
            {
                str += "protected ";
            }

            if (_method.IsVirtual)
            {
                str += "virtual ";
            }
            else if (_method.DeclaringType != _method.ReflectedType)
            {
                str += "override ";
            }

            if (_method.IsAbstract)
            {
                str += "abstract ";
            }

            if (str == "")
            {
                str = "---";
            }

            return str;
        }

        public static Type GetElementTypeRecursively(Type _type, out int _count)
        {
            Type returnType = _type;
            _count = 0;

            while (returnType.GetElementType() != null)
            {
                returnType = returnType.GetElementType();
                _count++;
            }

            return returnType;
        }

        public static Type GetElementTypeRecursively(Type _type)
        {
            return GetElementTypeRecursively(_type, out int val);
        }
    }
}
