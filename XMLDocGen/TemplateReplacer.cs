﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace XMLDocGen
{
    public enum Tags
    {
        START_PAGE,
        END_PAGE,
        START_CLASS,
        END_CLASS,
        START_FIELD,
        END_FIELD,
        START_PROPERTY,
        END_PROPERTY,
        START_METHOD,
        END_METHOD,
        START_PARAM,
        END_PARAM,
        START_ENUM,
        END_ENUM,
        START_ENUM_ELEMENT,
        END_ENUM_ELEMENT,
        class_modifiers,
        class_name,
        class_summary,
        class_remarks,
        field_modifiers,
        field_type,
        field_name,
        field_summary,
        field_remarks,
        property_modifiers,
        property_type,
        property_name,
        property_acessors,
        property_summary,
        property_remarks,
        method_modifiers,
        method_name,
        method_signature,
        method_summary,
        method_remarks,
        param_modifiers,
        param_type,
        param_name,
        param_default_value,
        param_desc,
        enum_name,
        enum_underlying_type,
        enum_summary,
        enum_remarks,
        enum_element_name,
        enum_element_value,
        enum_element_summary,
        enum_element_remarks
    }

    class TemplateReplacer
    {
        public const char tagChar = '$';

        private string template;
        private List<TypeData> typeDatas = new List<TypeData>();

        private string classFragment;
        private string fieldFragment;
        private string methodFragment;
        private string propertyFragment;
        private string enumElementFragment;
        private string enumFragment;
        private string paramFragment;

        public TemplateReplacer(string _template, List<TypeData> _typeDatas)
        {
            template = _template;
            typeDatas = _typeDatas;
        }

        public void Replace()
        {
            string output = "";

            classFragment = GetInBetweenTags(template, Tags.START_CLASS, Tags.END_CLASS).RemoveFirstAndLastNewLine();
            fieldFragment = GetInBetweenTags(template, Tags.START_FIELD, Tags.END_FIELD).RemoveFirstAndLastNewLine();
            propertyFragment = GetInBetweenTags(template, Tags.START_PROPERTY, Tags.END_PROPERTY).RemoveFirstAndLastNewLine();

            methodFragment = GetInBetweenTags(template, Tags.START_METHOD, Tags.END_METHOD).RemoveFirstAndLastNewLine();
            paramFragment = GetInBetweenTags(template, Tags.START_PARAM, Tags.END_PARAM).RemoveFirstAndLastNewLine();

            enumFragment = GetInBetweenTags(template, Tags.START_ENUM, Tags.END_ENUM).RemoveFirstAndLastNewLine();
            enumElementFragment = GetInBetweenTags(template, Tags.START_ENUM_ELEMENT, Tags.END_ENUM_ELEMENT).RemoveFirstAndLastNewLine();

            classFragment = Regex.Replace(classFragment, @"\r?\n?" + Regex.Escape(fieldFragment), "");
            classFragment = Regex.Replace(classFragment, @"\r?\n?" + Regex.Escape(propertyFragment), "");
            classFragment = Regex.Replace(classFragment, @"\r?\n?" + Regex.Escape(methodFragment), "");

            methodFragment = Regex.Replace(methodFragment, @"\r?\n?" + Regex.Escape(paramFragment), "");

            enumFragment = Regex.Replace(enumFragment, @"\r?\n?" + Regex.Escape(enumElementFragment), "");

            var typees = typeDatas.Where(x => x.typeInfo == typeof(DummyClass) || x.typeInfo == typeof(DummyEnum));
            //foreach (var type in typeDatas)
            foreach(var type in typees)
            {
                string genTypeFragment;

                if(type.typeInfo.IsEnum)
                {
                    genTypeFragment = EnumTypeFragment(type);
                }
                else
                {
                    genTypeFragment = TypeFragment(type);
                }

                if(genTypeFragment == null || genTypeFragment == "")
                {
                    throw new Exception("genTypeFragment is null. This can't happen in any way, a mistake has been made.");
                }

                {
                    if (type.typeInfo.IsEnum)
                    {
                        int endTagIndex = Regex.Match(genTypeFragment, Tags.END_ENUM_ELEMENT.Str()).Index;
                        genTypeFragment = genTypeFragment.Insert(endTagIndex, EnumElementFragment(type));
                    }
                    else
                    {
                        int endTagIndex = Regex.Match(genTypeFragment, Tags.END_FIELD.Str()).Index;
                        genTypeFragment = genTypeFragment.Insert(endTagIndex, FieldFragments(type));
                    }
                }

                {
                    int endTagIndex = Regex.Match(genTypeFragment, Tags.END_PROPERTY.Str()).Index;
                    genTypeFragment = genTypeFragment.Insert(endTagIndex, PropertyFragments(type));
                }
                
                {
                    int endTagIndex = Regex.Match(genTypeFragment, Tags.END_METHOD.Str()).Index;
                    genTypeFragment = genTypeFragment.Insert(endTagIndex, MethodFragments(type));
                }

                genTypeFragment += "\n";

                output += genTypeFragment;

                Console.WriteLine("len: " + genTypeFragment.Length + "  method count: " + type.methods.Count + "  field count: " + type.fields.Count + "  property count: " + type.properties.Count);
            }

            Console.WriteLine("---GENERATION ENDED---");
            Console.WriteLine("Total file length: " + output.Length);

            System.IO.File.WriteAllText(Program.OutFolder + "/GeneratedExample.md", CleanUpTags(output));

        }

        string TypeFragment(TypeData _type)
        {
            string str = classFragment;

            str = Regex.Replace(str, Tags.class_name.Str(), _type.typeInfo.Name);
            str = Regex.Replace(str, Tags.class_summary.Str(), _type.xmlData.summary);
            str = Regex.Replace(str, Tags.class_remarks.Str(), _type.xmlData.remarks);

            return str;
        }

        string EnumTypeFragment(TypeData _type)
        {
            string str = enumFragment;

            str = Regex.Replace(str, Tags.enum_name.Str(), _type.typeInfo.Name);
            str = Regex.Replace(str, Tags.enum_underlying_type.Str(), Enum.GetUnderlyingType(_type.typeInfo).GetMarkdownTypeName());
            str = Regex.Replace(str, Tags.enum_summary.Str(), _type.xmlData.summary);
            str = Regex.Replace(str, Tags.enum_remarks.Str(), _type.xmlData.remarks);

            return str;
        }

        string FieldFragments(TypeData _type)
        {
            string retStr = "";

            for (int i = 0; i < _type.fields.Count; i++)
            {
                FieldData field = _type.fields[i];

                string str = fieldFragment;

                str = Regex.Replace(str, Tags.field_name.Str(), field.fieldInfo.Name);
                str = Regex.Replace(str, Tags.field_type.Str(), field.fieldInfo.FieldType.GetMarkdownTypeName());
                str = Regex.Replace(str, Tags.field_summary.Str(), field.xmlData.summary);
                str = Regex.Replace(str, Tags.field_remarks.Str(), field.xmlData.remarks);

                str += "\n";

                retStr += str;
            }

            return retStr;
        }

        string EnumElementFragment(TypeData _type)
        {
            string retStr = "";

            string[] names = Enum.GetNames(_type.typeInfo);
            Array values = Enum.GetValues(_type.typeInfo);

            for (int i = 0; i < names.Length; i++)
            {
                FieldData field = _type.fields[i+1]; //First field is the __value field

                string str = enumElementFragment;

                string elementValue = MarkdownBuilder.CreateCode(Convert.ChangeType(values.GetValue(i), Enum.GetUnderlyingType(_type.typeInfo)).ToString());

                str = Regex.Replace(str, Tags.enum_element_name.Str(), names[i]);
                str = Regex.Replace(str, Tags.enum_element_value.Str(), elementValue);
                str = Regex.Replace(str, Tags.enum_element_summary.Str(), field.xmlData.summary);
                str = Regex.Replace(str, Tags.enum_element_remarks.Str(), field.xmlData.remarks);

                str += "\n";

                retStr += str;
            }

            return retStr;
        }

        string PropertyFragments(TypeData _type)
        {
            string retStr = "";

            for (int i = 0; i < _type.properties.Count; i++)
            {
                PropertyData property = _type.properties[i];

                string str = propertyFragment;

                str = Regex.Replace(str, Tags.property_name.Str(), property.propertyInfo.Name);
                str = Regex.Replace(str, Tags.property_type.Str(), property.propertyInfo.PropertyType.GetMarkdownTypeName());
                str = Regex.Replace(str, Tags.property_acessors.Str(), property.propertyInfo.GetAcessorsStr());
                str = Regex.Replace(str, Tags.property_summary.Str(), property.xmlData.summary);
                str = Regex.Replace(str, Tags.property_remarks.Str(), property.xmlData.remarks);

                str += "\n";

                retStr += str;
            }

            return retStr;
        }

        string MethodFragments(TypeData _type)
        {
            string retStr = "";

            for (int i = 0; i < _type.methods.Count; i++)
            {
                MethodData method = _type.methods[i];

                string str = methodFragment;

                str = Regex.Replace(str, Tags.method_signature.Str(), method.methodInfo.GetTextSignature());
                str = Regex.Replace(str, Tags.method_summary.Str(), method.xmlData.summary);
                str = Regex.Replace(str, Tags.method_remarks.Str(), method.xmlData.remarks);

                int endTagIndex = Regex.Match(str, Tags.END_PARAM.Str()).Index;
                str = str.Insert(endTagIndex, ParamFragments(method));

                str += "\n";

                retStr += str;
            }

            return retStr;
        }

        string ParamFragments(MethodData _method)
        {
            string retStr = "";

            for (int i = 0; i < _method.parameters.Count; i++)
            {
                ParameterData param = _method.parameters[i];

                string str = paramFragment;

                string defaultValue = param.parameterInfo.DefaultValue.ToString();
                if(defaultValue == null || defaultValue == "")
                {
                    defaultValue = " ";
                }

                str = Regex.Replace(str, Tags.param_modifiers.Str(), param.parameterInfo.GetModifiersString());
                str = Regex.Replace(str, Tags.param_type.Str(), param.parameterInfo.ParameterType.GetMarkdownTypeName());
                str = Regex.Replace(str, Tags.param_name.Str(), param.parameterInfo.Name.MarkdownEscape());
                str = Regex.Replace(str, Tags.param_default_value.Str(), MarkdownBuilder.CreateCode(defaultValue));
                str = Regex.Replace(str, Tags.param_desc.Str(), param.xmlData.summary);

                str += "\n";

                retStr += str;
            }

            return retStr;
        }

        string CleanUpTags(string _in)
        {
            
            int nameCount = Enum.GetNames(typeof(Tags)).Length;
            for (int i = 0; i < nameCount; i++)
            {
                _in = Regex.Replace(_in, ((Tags)i).Str() + @"\r?\n?", "", RegexOptions.Singleline);
            }
            
            return _in;
        }

        string GetInBetweenTags(string _input, Tags _startTag, Tags _endTag)
        {
            return Regex.Match(_input, _startTag.Str() + "(?s)(.*)" + _endTag.Str()).Groups[1].Value;
        }
    }
}