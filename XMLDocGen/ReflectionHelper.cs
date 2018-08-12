using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace XMLDocGen
{
    static class ReflectionHelper
    {
        public static string GetFullName(this FieldInfo _field)
        {
            return _field.ReflectedType.FullName + "." + _field.Name;
        }

        public static string GetFullName(this PropertyInfo _property)
        {
            return _property.ReflectedType.FullName + "." + _property.Name;
        }

        public static string GetFullName(this MethodInfo _property)
        {
            return _property.ReflectedType.FullName + "." + _property.Name;
        }
    }
}
