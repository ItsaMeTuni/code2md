using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace XMLDocGen
{
    public enum Tags
    {
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
        START_FIELDS,
        END_FIELDS,
        START_PROPERTIES,
        END_PROPERTIES,
        START_METHODS,
        END_METHODS,
        START_PARAMS,
        END_PARAMS,
        class_modifiers,
        class_name,
        escaped_class_name,
        class_summary,
        class_remarks,
        field_modifiers,
        field_type,
        field_name,
        escaped_field_name,
        field_summary,
        field_remarks,
        property_modifiers,
        property_type,
        property_name,
        escaped_property_name,
        property_acessors,
        property_summary,
        property_remarks,
        method_modifiers,
        method_name,
        escaped_method_name,
        method_signature,
        escaped_method_signature,
        method_summary,
        method_remarks,
        param_modifiers,
        param_type,
        param_name,
        escaped_param_name,
        param_default_value,
        param_desc,
        enum_name,
        escaped_enum_name,
        enum_underlying_type,
        enum_summary,
        enum_remarks,
        enum_element_name,
        escaped_enum_element_name,
        enum_element_value,
        enum_element_summary,
        enum_element_remarks,
        class_extra_doc,
        field_extra_doc,
        property_extra_doc,
        method_extra_doc,
        enum_extra_doc,
        class_full_name,
        escaped_class_full_name,
        enum_full_name,
        escaped_enum_full_name
    }

    class TemplateReplacer
    {
        public const char tagChar = '$';

        private string template;
        private List<TypeData> typeDatas = new List<TypeData>();

        private string classFragment;
        private string fieldsFragment;
        private string propertiesFragment;
        private string methodsFragment;
        private string paramsFragment;
        private string fieldFragment;
        private string methodFragment;
        private string propertyFragment;
        private string enumElementFragment;
        private string enumFragment;
        private string paramFragment;

        private string output = "";

        public TemplateReplacer(string _template, List<TypeData> _typeDatas)
        {
            template = _template;
            typeDatas = _typeDatas;
            SetupFragmentTemplates();
        }

        public List<PageData> Replace()
        {
            List<PageData> pages = new List<PageData>();

            //var typees = typeDatas.Where(x => x.typeInfo == typeof(DummyClass) || x.typeInfo == typeof(DummyEnum));
            foreach (var type in typeDatas)
            //foreach(var type in typees)
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
                        int endTagIndex = Regex.Match(genTypeFragment, Tags.END_FIELDS.Str()).Index;
                        genTypeFragment = genTypeFragment.Insert(endTagIndex, FieldFragments(type));
                    }
                }

                {
                    int endTagIndex = Regex.Match(genTypeFragment, Tags.END_PROPERTIES.Str()).Index;
                    genTypeFragment = genTypeFragment.Insert(endTagIndex, PropertyFragments(type));
                }
                
                {
                    int endTagIndex = Regex.Match(genTypeFragment, Tags.END_METHODS.Str()).Index;
                    genTypeFragment = genTypeFragment.Insert(endTagIndex, MethodFragments(type));
                }

                genTypeFragment += "\n";

                genTypeFragment = CleanUpTags(genTypeFragment);

                pages.Add(new PageData(type.typeInfo.FullName, ToPagePath(type.typeInfo.FullName), genTypeFragment));

                output += genTypeFragment;

                Console.WriteLine("len: " + genTypeFragment.Length + "  method count: " + type.methods.Count + "  field count: " + type.fields.Count + "  property count: " + type.properties.Count);
            }

            Console.WriteLine("---GENERATION ENDED---");
            Console.WriteLine("Total file length: " + output.Length);

            return pages;
        }
        
        void SetupFragmentTemplates()
        {
            //Get the fragments
            classFragment = GetInBetweenTags(template, Tags.START_CLASS, Tags.END_CLASS).RemoveFirstAndLastNewLine();

            fieldsFragment = GetInBetweenTags(template, Tags.START_FIELDS, Tags.END_FIELDS).RemoveFirstAndLastNewLine();
            propertiesFragment = GetInBetweenTags(template, Tags.START_PROPERTIES, Tags.END_PROPERTIES).RemoveFirstAndLastNewLine();
            methodsFragment = GetInBetweenTags(template, Tags.START_METHODS, Tags.END_METHODS).RemoveFirstAndLastNewLine();
            paramsFragment = GetInBetweenTags(template, Tags.START_PARAMS, Tags.END_PARAMS).RemoveFirstAndLastNewLine();

            fieldFragment = GetInBetweenTags(template, Tags.START_FIELD, Tags.END_FIELD).RemoveFirstAndLastNewLine();
            propertyFragment = GetInBetweenTags(template, Tags.START_PROPERTY, Tags.END_PROPERTY).RemoveFirstAndLastNewLine();
            methodFragment = GetInBetweenTags(template, Tags.START_METHOD, Tags.END_METHOD).RemoveFirstAndLastNewLine();
            paramFragment = GetInBetweenTags(template, Tags.START_PARAM, Tags.END_PARAM).RemoveFirstAndLastNewLine();

            enumFragment = GetInBetweenTags(template, Tags.START_ENUM, Tags.END_ENUM).RemoveFirstAndLastNewLine();
            enumElementFragment = GetInBetweenTags(template, Tags.START_ENUM_ELEMENT, Tags.END_ENUM_ELEMENT).RemoveFirstAndLastNewLine();

            //Remove inner fragments from outer fragments
            classFragment = Regex.Replace(classFragment, @"\r?\n?" + Regex.Escape(fieldsFragment), "");
            classFragment = Regex.Replace(classFragment, @"\r?\n?" + Regex.Escape(propertiesFragment), "");
            classFragment = Regex.Replace(classFragment, @"\r?\n?" + Regex.Escape(methodsFragment), "");

            fieldsFragment = Regex.Replace(fieldsFragment, @"\r?\n?" + Regex.Escape(fieldFragment), "");
            propertiesFragment = Regex.Replace(propertiesFragment, @"\r?\n?" + Regex.Escape(propertyFragment), "");
            methodsFragment = Regex.Replace(methodsFragment, @"\r?\n?" + Regex.Escape(methodFragment), "");

            methodFragment = Regex.Replace(methodFragment, @"\r?\n?" + Regex.Escape(paramsFragment), "");

            paramsFragment = Regex.Replace(paramsFragment, @"\r?\n?" + Regex.Escape(paramFragment), "");

            Console.WriteLine(methodFragment);
            Console.WriteLine("--------------------------------------");
            Console.WriteLine(paramsFragment);

            enumFragment = Regex.Replace(enumFragment, @"\r?\n?" + Regex.Escape(enumElementFragment), "");
        }

        string TypeFragment(TypeData _type)
        {
            string str = classFragment;

            str = Regex.Replace(str, Tags.class_name.Str(), _type.typeInfo.Name);
            str = Regex.Replace(str, Tags.escaped_class_name.Str(), _type.typeInfo.Name.MarkdownEscape());
            str = Regex.Replace(str, Tags.class_summary.Str(), _type.xmlData.summary);
            str = Regex.Replace(str, Tags.class_remarks.Str(), _type.xmlData.remarks);
            str = Regex.Replace(str, Tags.class_full_name.Str(), _type.typeInfo.FullName);

            return str;
        }

        string EnumTypeFragment(TypeData _type)
        {
            string str = enumFragment;

            str = Regex.Replace(str, Tags.enum_name.Str(), _type.typeInfo.Name);
            str = Regex.Replace(str, Tags.escaped_enum_name.Str(), _type.typeInfo.Name.MarkdownEscape());
            str = Regex.Replace(str, Tags.enum_underlying_type.Str(), Enum.GetUnderlyingType(_type.typeInfo).GetMarkdownTypeName());
            str = Regex.Replace(str, Tags.enum_summary.Str(), _type.xmlData.summary);
            str = Regex.Replace(str, Tags.enum_remarks.Str(), _type.xmlData.remarks);
            str = Regex.Replace(str, Tags.enum_full_name.Str(), _type.typeInfo.FullName);

            return str;
        }

        string FieldFragments(TypeData _type)
        {
            string retStr = "";

            if(_type.fields.Count > 0)
            {
                retStr = fieldsFragment;
            }

            for (int i = 0; i < _type.fields.Count; i++)
            {
                FieldData field = _type.fields[i];

                string str = fieldFragment;

                str = Regex.Replace(str, Tags.field_name.Str(), field.fieldInfo.Name);
                str = Regex.Replace(str, Tags.escaped_field_name.Str(), field.fieldInfo.Name.MarkdownEscape());
                str = Regex.Replace(str, Tags.field_type.Str(), field.fieldInfo.FieldType.GetMarkdownTypeName());
                str = Regex.Replace(str, Tags.field_summary.Str(), field.xmlData.summary);
                str = Regex.Replace(str, Tags.field_remarks.Str(), field.xmlData.remarks);

                str += "\n";

                int endTagIndex = Regex.Match(retStr, Tags.END_FIELD.Str()).Index;
                retStr = retStr.Insert(endTagIndex, str);
            }

            return retStr;
        }

        string EnumElementFragment(TypeData _type)
        {
            string retStr = "";

            for (int i = 0; i < _type.fields.Count; i++)
            {
                FieldData field = _type.fields[i];
                EnumFieldSpecialData specialData = (EnumFieldSpecialData)field.specialFieldData;

                string str = enumElementFragment;

                str = Regex.Replace(str, Tags.enum_element_name.Str(), field.fieldInfo.Name);
                str = Regex.Replace(str, Tags.escaped_enum_element_name.Str(), field.fieldInfo.Name.MarkdownEscape());
                str = Regex.Replace(str, Tags.enum_element_value.Str(), specialData.value);
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

            if (_type.properties.Count > 0)
            {
                retStr = propertiesFragment;
            }

            for (int i = 0; i < _type.properties.Count; i++)
            {
                PropertyData property = _type.properties[i];

                string str = propertyFragment;

                str = Regex.Replace(str, Tags.property_name.Str(), property.propertyInfo.Name);
                str = Regex.Replace(str, Tags.escaped_property_name.Str(), property.propertyInfo.Name.MarkdownEscape());
                str = Regex.Replace(str, Tags.property_type.Str(), property.propertyInfo.PropertyType.GetMarkdownTypeName());
                str = Regex.Replace(str, Tags.property_acessors.Str(), property.propertyInfo.GetAcessorsStr());
                str = Regex.Replace(str, Tags.property_summary.Str(), property.xmlData.summary);
                str = Regex.Replace(str, Tags.property_remarks.Str(), property.xmlData.remarks);

                str += "\n";

                int endTagIndex = Regex.Match(retStr, Tags.END_PROPERTY.Str()).Index;
                retStr = retStr.Insert(endTagIndex, str);
            }

            return retStr;
        }

        string MethodFragments(TypeData _type)
        {
            string retStr = "";

            if (_type.methods.Count > 0)
            {
                retStr = methodsFragment;
            }

            for (int i = 0; i < _type.methods.Count; i++)
            {
                MethodData method = _type.methods[i];

                string str = methodFragment;

                str = Regex.Replace(str, Tags.method_signature.Str(), method.methodInfo.GetTextSignature());
                str = Regex.Replace(str, Tags.escaped_method_signature.Str(), method.methodInfo.GetTextSignature().MarkdownEscape());
                str = Regex.Replace(str, Tags.method_name.Str(), method.methodInfo.Name);
                str = Regex.Replace(str, Tags.escaped_method_name.Str(), method.methodInfo.Name);
                str = Regex.Replace(str, Tags.method_summary.Str(), method.xmlData.summary);
                str = Regex.Replace(str, Tags.method_remarks.Str(), method.xmlData.remarks);

                {
                    int endTagIndex = Regex.Match(str, Tags.END_PARAM.Str()).Index;
                    str = str.Insert(endTagIndex, ParamFragments(method));
                }

                str += "\n";

                {
                    int endTagIndex = Regex.Match(retStr, Tags.END_METHOD.Str()).Index;
                    retStr = retStr.Insert(endTagIndex, str);
                }
            }

            return retStr;
        }

        string ParamFragments(MethodData _method)
        {
            string retStr = "";

            if(_method.parameters.Count > 0)
            {
                retStr = paramsFragment;
            }

            for (int i = 0; i < _method.parameters.Count; i++)
            {
                ParameterData param = _method.parameters[i];

                string str = paramFragment;

                string defaultValue = param.parameterInfo.DefaultValue?.ToString() ?? "";
                if(defaultValue == null || defaultValue == "")
                {
                    defaultValue = " ";
                }

                str = Regex.Replace(str, Tags.param_modifiers.Str(), param.parameterInfo.GetModifiersString());
                str = Regex.Replace(str, Tags.param_type.Str(), param.parameterInfo.ParameterType.GetMarkdownTypeName());
                str = Regex.Replace(str, Tags.param_name.Str(), param.parameterInfo.Name);
                str = Regex.Replace(str, Tags.escaped_param_name.Str(), param.parameterInfo.Name.MarkdownEscape());
                str = Regex.Replace(str, Tags.param_default_value.Str(), defaultValue);
                str = Regex.Replace(str, Tags.param_desc.Str(), param.xmlData.summary);

                str += "\n";

                int endTagIndex = Regex.Match(retStr, Tags.END_PARAM.Str()).Index;
                retStr = retStr.Insert(endTagIndex, str);
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
            return Regex.Match(_input, _startTag.Str() + "(?s)(.*?)" + _endTag.Str()).Groups[1].Value;
        }

        string ToPagePath(string _text)
        {
            string str = _text;

            str = Regex.Replace(str, @" |\.", "/");

            //Add - in between a lower-case and an upper-case letter (camelCase to "hypen-case" or whatever it's called)
            for (int i = 0; i < str.Length; i++)
            {
                if(i > 0 && Char.IsUpper(str[i]) && !Char.IsUpper(str[i-1]) && !Char.IsPunctuation(str[i - 1]))
                {
                    str = str.Insert(i, "-");

                    //by doing this we're actually incrementing i by 2 each iteration so if we add a - we don't get stuck on an endless loop
                    i++;
                }
            }

            str = str.ToLowerInvariant();

            return str + ".md";
        }

        void CleanOutputFolder()
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Program.OutFolder);
            System.IO.FileInfo[] files = dir.GetFiles();

            foreach (var file in files)
            {
                file.Delete();
            }
        }

        string GetTagValue(TagContext _context, Tags _tag)
        {
            EnumTypeSpecialData enumTypeSpecialData = null;
            EnumFieldSpecialData enumFieldSpecialData = null;

            if (_context.typeData.typeInfo.IsEnum)
            {
                enumTypeSpecialData = (EnumTypeSpecialData)_context.typeData.specialTypeData;
                enumFieldSpecialData = (EnumFieldSpecialData)_context.fieldData.specialFieldData;
            }

            //CLASS
            if (_tag == Tags.class_name)
            {
                return _context.typeData.typeInfo.Name;
            }
            else if(_tag == Tags.escaped_class_name)
            {
                return _context.typeData.typeInfo.Name.MarkdownEscape();
            }
            else if (_tag == Tags.class_summary)
            {
                return _context.typeData.xmlData.summary;
            }
            else if (_tag == Tags.class_remarks)
            {
                return _context.typeData.xmlData.remarks;
            }
            else if (_tag == Tags.class_full_name)
            {
                return _context.typeData.typeInfo.FullName;
            }
            //ENUM
            else if (_tag == Tags.enum_name)
            {
                return _context.typeData.typeInfo.Name;
            }
            else if (_tag == Tags.escaped_enum_name)
            {
                return _context.typeData.typeInfo.Name.MarkdownEscape();
            }
            else if (_tag == Tags.enum_underlying_type)
            {
                return enumTypeSpecialData.underlyingType.GetMarkdownTypeName();
            }
            else if (_tag == Tags.enum_summary)
            {
                return _context.typeData.xmlData.summary;
            }
            else if (_tag == Tags.enum_remarks)
            {
                return _context.typeData.xmlData.remarks;
            }
            else if (_tag == Tags.enum_full_name)
            {
                return _context.typeData.typeInfo.FullName;
            }
            //FIELD
            else if (_tag == Tags.field_name)
            {
                return _context.fieldData.fieldInfo.Name;
            }
            else if (_tag == Tags.escaped_field_name)
            {
                return _context.fieldData.fieldInfo.Name.MarkdownEscape();
            }
            else if (_tag == Tags.field_type)
            {
                return _context.fieldData.fieldInfo.FieldType.GetMarkdownTypeName();
            }
            else if (_tag == Tags.field_summary)
            {
                return _context.fieldData.xmlData.remarks;
            }
            else if (_tag == Tags.field_remarks)
            {
                return _context.fieldData.xmlData.summary;
            }
            //ENUM ELEMENT
            else if (_tag == Tags.enum_element_name)
            {
                return _context.fieldData.fieldInfo.Name;
            }
            else if (_tag == Tags.escaped_enum_element_name)
            {
                return _context.fieldData.fieldInfo.Name.MarkdownEscape();
            }
            else if (_tag == Tags.enum_element_value)
            {
                return enumFieldSpecialData.value;
            }
            else if (_tag == Tags.enum_element_summary)
            {
                return _context.fieldData.xmlData.summary;
            }
            else if (_tag == Tags.enum_element_remarks)
            {
                return _context.fieldData.xmlData.remarks;
            }
            //PROPERTY
            else if (_tag == Tags.property_name)
            {
                return _context.propertyData.propertyInfo.Name;
            }
            else if (_tag == Tags.escaped_property_name)
            {
                return _context.propertyData.propertyInfo.Name.MarkdownEscape();
            }
            else if (_tag == Tags.property_type)
            {
                return _context.propertyData.propertyInfo.PropertyType.GetMarkdownTypeName();
            }
            else if (_tag == Tags.property_acessors)
            {
                return _context.propertyData.propertyInfo.GetAcessorsStr();
            }
            else if (_tag == Tags.property_summary)
            {
                return _context.propertyData.xmlData.summary;
            }
            else if (_tag == Tags.property_remarks)
            {
                return _context.propertyData.xmlData.remarks;
            }
            //METHOD
            else if (_tag == Tags.method_signature)
            {
                return _context.methodData.methodInfo.GetTextSignature();
            }
            else if (_tag == Tags.escaped_method_signature)
            {
                return _context.methodData.methodInfo.GetTextSignature().MarkdownEscape();
            }
            else if (_tag == Tags.method_name)
            {
                return _context.methodData.methodInfo.Name;
            }
            else if (_tag == Tags.escaped_method_name)
            {
                return _context.methodData.methodInfo.Name.MarkdownEscape();
            }
            else if (_tag == Tags.method_summary)
            {
                return _context.methodData.xmlData.summary;
            }
            else if (_tag == Tags.method_remarks)
            {
                return _context.methodData.xmlData.remarks;
            }
            //PARAMETER
            else if (_tag == Tags.param_modifiers)
            {
                return _context.paramData.parameterInfo.GetModifiersString();
            }
            else if (_tag == Tags.param_type)
            {
                return _context.paramData.parameterInfo.ParameterType.GetMarkdownTypeName();
            }
            else if (_tag == Tags.param_name)
            {
                return _context.paramData.parameterInfo.Name;
            }
            else if (_tag == Tags.escaped_param_name)
            {
                return _context.paramData.parameterInfo.Name.MarkdownEscape();
            }
            else if (_tag == Tags.param_default_value)
            {
                return _context.paramData.parameterInfo.DefaultValue?.ToString() ?? "";
            }
            else if (_tag == Tags.param_desc)
            {
                return _context.paramData.xmlData.summary;
            }

            throw new ArgumentException("One of these happened: Invalid tag; tag not supported. Tag is: '" + _tag.ToString() + "'.");
        }

        void ReplaceTagWithValue(ref string _str, Tags _tag, TagContext _tagContext)
        {
            _str = Regex.Replace(_str, _tag.Str(), GetTagValue(_tagContext, _tag));
        }
    }

    struct PageData
    {
        public string name;
        public string path;
        public string content;

        public PageData(string _name, string _path, string _content)
        {
            this.name = _name;
            this.path = _path;
            this.content = _content;
        }
    }

    struct TagContext
    {
        public TypeData typeData;
        public FieldData fieldData;
        public PropertyData propertyData;
        public MethodData methodData;
        public ParameterData paramData;
    }
}
