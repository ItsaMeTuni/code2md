using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml;

namespace XMLDocGen
{
    public class DataGatherer
    {
        const BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        Assembly assembly;
        XmlNodeList nodeList;

        public DataGatherer(Assembly _assembly, XmlNodeList _xmlNodes)
        {
            assembly = _assembly;
            nodeList = _xmlNodes;
        }

        public List<TypeData> GetTypeData()
        {
            List<TypeData> typesData = new List<TypeData>();

            Type[] types = assembly.GetTypes();
            for (int iType = 0; iType < types.Length; iType++)
            {
                Type type = types[iType];

                if (type.IsCompilerGenerated())
                {
                    continue;
                }


                TypeData typeData = new TypeData();

                typeData.typeInfo = type.GetTypeInfo();
                typeData.fields = GetFieldData(type);
                typeData.properties = GetPropertyData(type);
                typeData.methods = GetMethodData(type);
                typeData.xmlData = GetXmlData(nodeList.FindMemberWithName(type.FullName));

                if (type.IsEnum)
                {
                    typeData.specialTypeData = new EnumTypeSpecialData()
                    {
                        underlyingType = Enum.GetUnderlyingType(type)
                    };
                }
                else
                {
                    typeData.specialTypeData = null;
                }

                typesData.Add(typeData);
            }

            return typesData;
        }

        List<FieldData> GetFieldData(Type _type)
        {
            List<FieldData> fieldsData = new List<FieldData>();

            string[] enumStrValues = null;
            if (_type.IsEnum)
            {
                Array values = Enum.GetValues(_type);
                Type underlyingType = Enum.GetUnderlyingType(_type);

                enumStrValues = new string[values.Length];

                for (int i = 0; i < values.Length; i++)
                {
                    enumStrValues[i] = Convert.ChangeType(values.GetValue(i), underlyingType).ToString();
                }
            }

            FieldInfo[] fields = _type.GetFields(bindingFlags);
            for (int iField = 0; iField < fields.Length; iField++)
            {
                //Skip first field (first field in an enum is __value)
                if (_type.IsEnum && iField == 0)
                {
                    continue;
                }

                FieldInfo field = fields[iField];

                if (field.IsCompilerGenerated())
                {
                    continue;
                }

                FieldData fieldData = new FieldData();

                fieldData.fieldInfo = field;
                fieldData.xmlData = GetXmlData(nodeList.FindFieldMemberWithName(field.GetFullName()));

                if(_type.IsEnum)
                {
                    fieldData.specialFieldData = new EnumFieldSpecialData()
                    {
                        value = enumStrValues[iField]
                    };
                }

                fieldsData.Add(fieldData);
            }

            return fieldsData;
        }

        List<PropertyData> GetPropertyData(Type _type)
        {
            List<PropertyData> propertyDatas = new List<PropertyData>();

            PropertyInfo[] properties = _type.GetProperties(bindingFlags);
            for (int iProperty = 0; iProperty < properties.Length; iProperty++)
            {
                PropertyInfo property = properties[iProperty];

                if(property.IsCompilerGenerated())
                {
                    continue;
                }

                PropertyData propertyData = new PropertyData();

                propertyData.propertyInfo = property;
                propertyData.xmlData = GetXmlData(nodeList.FindFieldMemberWithName(property.GetFullName()));

                propertyDatas.Add(propertyData);
            }

            return propertyDatas;
        }

        List<MethodData> GetMethodData(Type _type)
        {
            List<MethodData> methodDatas = new List<MethodData>();

            MethodInfo[] methods = _type.GetMethods(bindingFlags);
            for (int iMethod = 0; iMethod < methods.Length; iMethod++)
            {
                MethodInfo method = methods[iMethod];

                if(method.IsCompilerGenerated())
                {
                    continue;
                }

                MethodData methodData = new MethodData();

                methodData.methodInfo = method;
                methodData.xmlData = GetXmlData(nodeList.FindMethodMemberWithName(method.GetFullName()));
                methodData.parameters = GetParameterData(method);

                methodDatas.Add(methodData);
            }

            return methodDatas;
        }

        List<ParameterData> GetParameterData(MethodInfo _method)
        {
            List<ParameterData> parameterDatas = new List<ParameterData>();

            XmlNodeList paramNodes = nodeList.FindMethodMemberWithName(_method.GetFullName())?.SelectNodes("param");

            ParameterInfo[] parameters = _method.GetParameters();

            for (int iParameter = 0; iParameter < parameters.Length; iParameter++)
            {
                ParameterInfo parameter = parameters[iParameter];

                ParameterData parameterData = new ParameterData();

                parameterData.parameterInfo = parameter;

                parameterData.xmlData = GetXmlData(paramNodes?.FindMemberWithName(parameter.Name));

                parameterDatas.Add(parameterData);
            }

            return parameterDatas;
        }

        XmlData GetXmlData(XmlNode _node)
        {
            XmlData data = new XmlData();

            data.summary = _node?.SelectSingleNode("summary")?.InnerText.CleanString() ?? "";
            data.remarks = _node?.SelectSingleNode("remarks")?.InnerText.CleanString() ?? "";
            data.returns = _node?.SelectSingleNode("returns")?.InnerText.CleanString() ?? "";
            data.example = _node?.SelectSingleNode("example")?.InnerText.CleanString() ?? "";

            return data;
        }
    }

    public class TypeData
    {
        public TypeInfo typeInfo;
        public XmlData xmlData;

        public List<FieldData> fields;
        public List<PropertyData> properties;
        public List<MethodData> methods;

        public SpecialData specialTypeData;
    }

    public class MethodData
    {
        public MethodInfo methodInfo;
        public XmlData xmlData;

        public List<ParameterData> parameters;
    }

    public class FieldData
    {
        public FieldInfo fieldInfo;
        public XmlData xmlData;

        public SpecialData specialFieldData;
    }

    public class PropertyData
    {
        public PropertyInfo propertyInfo;
        public XmlData xmlData;
    }

    public class ParameterData
    {
        public ParameterInfo parameterInfo;
        public XmlData xmlData;
    }

    public class XmlData
    {
        public string summary;
        public string remarks;
        public string returns;
        public string example;
    }

    public class SpecialData { }

    public class EnumFieldSpecialData : SpecialData
    {
        //Value is stored in a string because it can be one of many types (like uint, int, long, ushort, etc)
        public string value;
    }

    public class EnumTypeSpecialData : SpecialData
    {
        public Type underlyingType;
    }
}
