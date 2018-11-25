using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLDocGen
{
    public class OutputBuilder
    {
        public List<LinkedPart> LinkPartsToData(List<ParsedPart> _parts, List<TypeData> _typeDatas, TagContext _parentContext)
        {
            List<LinkedPart> linkedParts = new List<LinkedPart>();

            for (int i = 0; i < _parts.Count; i++)
            {
                bool wantsRepeat = false;

                ParsedPart part = _parts[i];

                TagContext context;

                if (part.type == ParsedPart.PartType.Area)
                {
                    if (_parentContext.IsEmpty() && (part.areaTag == AreaTags.CLASS || part.areaTag == AreaTags.ENUM))
                    {
                        bool wantsEnum = part.areaTag == AreaTags.ENUM;
                        context = new TagContext()
                        {
                            typeData = _typeDatas.Pop(x => x.typeInfo.IsEnum == wantsEnum)
                        };

                        wantsRepeat = _typeDatas.Count > 0;

                        //We didn't find a type matching our needs (and enum or a class)
                        if (context.typeData == null)
                        {
                            //so we continue to the next part on the parts list
                            continue;
                        }
                    }
                    else
                    {
                        context = GetContextBasedOnTag(part.areaTag, _parentContext);
                    }
                }
                else
                {
                    context = _parentContext;
                }

                LinkedPart linkedPart = new LinkedPart()
                {
                    parsedPart = part,
                    context = context,
                    children = LinkPartsToData(part.childParts, _typeDatas, context)
                };

                linkedParts.Add(linkedPart);

                if(wantsRepeat)
                {
                    i--;
                }
            }

            return linkedParts;
        }
        
        /// <summary>
        /// Gets a context for the provided tag, given its parent context.
        /// </summary>
        private TagContext GetContextBasedOnTag(AreaTags _tag, TagContext _parentContext)
        {
            if(_tag == AreaTags.FIELD || _tag == AreaTags.ENUM_ELEMENT)
            {
                _parentContext.fieldData = _parentContext.typeData.fields.Pop();
            }
            else if(_tag == AreaTags.METHOD)
            {
                _parentContext.methodData = _parentContext.typeData.methods.Pop();
            }
            else if (_tag == AreaTags.PARAM)
            {
                _parentContext.paramData = _parentContext.methodData.parameters.Pop();
            }
            else if (_tag == AreaTags.PROPERTY)
            {
                _parentContext.propertyData = _parentContext.typeData.properties.Pop();
            }

            return _parentContext;
        }

        public static string GetTagValue(TagContext _context, Tags _tag)
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
            else if (_tag == Tags.escaped_class_name)
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
    }

    public class LinkedPart
    {
        public ParsedPart parsedPart;
        public TagContext context;
        public List<LinkedPart> children = new List<LinkedPart>();
    }

}
