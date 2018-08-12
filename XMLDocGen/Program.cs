using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Configuration;

namespace XMLDocGen
{
    /// <summary>
    /// Contains information about a method's parameter (such as reflected info and description).
    /// </summary>
    public class ParameterData
    {
        public ParameterInfo parameterInfo;

        public string desc;
    }

    /// <summary>
    /// Contains information about a method (such as reflected info, parameters and xml comment).
    /// </summary>
    public class MethodData
    {
        public MethodInfo methodInfo;

        public List<ParameterData> parameters = new List<ParameterData>();

        public CommentData commentData;
    }

    /// <summary>
    /// Contains information about a class (such as reflected info, members and xml comment).
    /// </summary>
    public class ClassData
    {
        public TypeInfo typeInfo;

        public List<MethodData> methods = new List<MethodData>();
        public List<FieldData> fields = new List<FieldData>();
        public List<PropertyData> properties = new List<PropertyData>();

        public CommentData commentData;
    }

    /// <summary>
    /// Contains information about a field (such as reflected info and xml comment).
    /// </summary>
    public class FieldData
    {
        public FieldInfo fieldInfo;

        public CommentData commentData;
    }

    /// <summary>
    /// Contains information about a property (such as reflected info and xml comment).
    /// </summary>
    public class PropertyData
    {
        public PropertyInfo propertyInfo;

        public CommentData commentData;
    }

    /// <summary>
    /// Contains information about an xml comment (such as summary and remarks).
    /// </summary>
    public class CommentData
    {
        public string summary;
        public string remarks;
        public string returns;
    }

    class Program
    {
        static string xmlPath = "";
        static string assemblyPath = "";
        static string outFolder = "";

        static List<ClassData> classes = new List<ClassData>();
        public static Assembly assembly;
        static XmlNodeList xml;

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Generate();

            Console.ReadLine();
        }

        void Generate()
        {
            LoadConfigs();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(GetXmlPath());

            XmlNodeList members = xmlDoc.SelectNodes("doc/members/member");

            //Load assembly

            /*
             * Currently we load the current assembly for testing purposes
            Assembly assembly = Assembly.ReflectionOnlyLoadFrom(assemblyPath);
            foreach (var assemblyName in assembly.GetReferencedAssemblies())
            {
                try
                {
                    Assembly.ReflectionOnlyLoad(assemblyName.FullName);
                }
                catch
                {
                    Assembly.ReflectionOnlyLoadFrom(Path.Combine(Path.GetDirectoryName(assemblyPath), assemblyName.Name + ".dll"));
                }
            }
            */

            assembly = Assembly.GetAssembly(typeof(Program));
            xml = members;

            ReadAssembly();
            ToMarkdown();
        }

        /// <summary>
        /// Reads the assembly and gathers together reflected information about types/members and their respective xml comments
        /// </summary>
        void ReadAssembly()
        {
            Type[] types = assembly.GetTypes();

            for (int type = 0; type < types.Length; type++)
            {
                if(types[type].IsCompilerGenerated())
                {
                    continue;
                }

                ClassData classData = new ClassData();

                classData.typeInfo = types[type].GetTypeInfo();

                XmlNode classNode = xml.FindMemberWithName(types[type].FullName);
                classData.commentData = GetCommentData(classNode);

                MethodInfo[] methods = types[type].GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                for (int method = 0; method < methods.Length; method++)
                {
                    if(methods[method].IsCompilerGenerated())
                    {
                        continue;
                    }

                    MethodData methodData = new MethodData();
                    methodData.methodInfo = methods[method];
                    
                    XmlNode methodNode = xml.FindMethodMemberWithName(types[type].FullName + "." + methods[method].Name);
                    methodData.commentData = GetCommentData(methodNode);

                    if (methodNode != null)
                    {
                        ParameterInfo[] parameters = methods[method].GetParameters();

                        XmlNodeList parameterNodes = methodNode.SelectNodes("param");

                        for (int param = 0; param < parameters.Length; param++)
                        {
                            ParameterData parameterData = new ParameterData();

                            parameterData.parameterInfo = parameters[param];
                            parameterData.desc = parameterNodes.FindMemberWithName(parameters[param].Name)?.InnerText.CleanString();

                            methodData.parameters.Add(parameterData);
                        }
                    }
                    classData.methods.Add(methodData);
                }

                FieldInfo[] fields = types[type].GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                for (int field = 0; field < fields.Length; field++)
                {

                    if (fields[field].IsCompilerGenerated())
                    {
                        continue;
                    }

                    FieldData fieldData = new FieldData();

                    fieldData.fieldInfo = fields[field];

                    XmlNode fieldNode = xml.FindFieldMemberWithName(types[type].FullName + "." + fields[field].Name);
                    fieldData.commentData = GetCommentData(fieldNode);

                    classData.fields.Add(fieldData);
                }

                PropertyInfo[] properties = types[type].GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                for (int property = 0; property < properties.Length; property++)
                {
                    if(properties[property].IsCompilerGenerated())
                    {
                        continue;
                    }

                    PropertyData propertyData = new PropertyData();

                    propertyData.propertyInfo = properties[property];

                    XmlNode propertyNode = xml.FindFieldMemberWithName(types[type].FullName + "." + properties[property].Name);
                    propertyData.commentData = GetCommentData(propertyNode);

                    classData.properties.Add(propertyData);
                }

                classes.Add(classData);
            }
        }

        /// <summary>
        /// Converts the class list into a markdown page and outputs it into a file
        /// </summary>
        void ToMarkdown()
        {
            MarkdownHelper md = new MarkdownHelper();

            foreach (var c in classes)
            {
                md.H1(c.typeInfo.FullName);
                md += c.commentData.summary;

                if (c.fields.Count > 0)
                {
                    md.H2("Fields");

                    List<string> fieldModifiers = new List<string>();
                    List<string> fieldTypes = new List<string>();
                    List<string> fieldNames = new List<string>();
                    List<string> fieldDescs = new List<string>();

                    foreach (var field in c.fields)
                    {
                        fieldModifiers.Add(field.fieldInfo.GetModifiersString());
                        fieldTypes.Add(field.fieldInfo.FieldType.GetTypeNameMarkdownText());
                        fieldNames.Add(field.fieldInfo.Name);

                        string desc = "";

                        if (!field.commentData.summary.IsEmpty())
                        {
                            desc += "**Summary:** ";
                            desc += field.commentData.summary;
                            desc += "  ";
                        }

                        if (!field.commentData.remarks.IsEmpty())
                        {
                            desc += "**Remarks:** ";
                            desc += field.commentData.remarks;
                            desc += "  ";
                        }

                        fieldDescs.Add(desc);
                    }

                    md.CreateTable(new string[] { "Modifiers","Type", "Name", "Description" }, null, fieldModifiers.ToArray(), fieldTypes.ToArray(), fieldNames.ToArray(), fieldDescs.ToArray());
                }

                if (c.properties.Count > 0)
                {
                    md.H2("Properties");
                
                    List<string> propertyTypes = new List<string>();
                    List<string> propertyNames = new List<string>();
                    List<string> propertyDescs = new List<string>();
                    List<string> propertyAcessors = new List<string>();

                    foreach (var property in c.properties)
                    {
                        propertyTypes.Add(property.propertyInfo.PropertyType.GetTypeNameMarkdownText());
                        propertyNames.Add(property.propertyInfo.Name);

                        string desc = "";

                        if (!property.commentData.summary.IsEmpty())
                        {
                            desc += "**Summary:** ";
                            desc += property.commentData.summary;
                            desc += "  ";
                        }

                        if (!property.commentData.remarks.IsEmpty())
                        {
                            desc += "**Remarks:** ";
                            desc += property.commentData.remarks;
                            desc += "  ";
                        }

                        propertyDescs.Add(desc);

                        string acessors = "";

                        if (property.propertyInfo.CanRead)
                        {
                            if(property.propertyInfo.GetMethod.IsPublic)
                            {
                                acessors += "public ";
                            }

                            if (property.propertyInfo.GetMethod.IsPrivate)
                            {
                                acessors += "private ";
                            }

                            acessors +=  "get; ";
                        }

                        if (property.propertyInfo.CanWrite)
                        {
                            if (property.propertyInfo.SetMethod.IsPublic)
                            {
                                acessors += "public ";
                            }

                            if (property.propertyInfo.SetMethod.IsPrivate)
                            {
                                acessors += "private ";
                            }

                            acessors += "set; ";
                        }

                        propertyAcessors.Add(acessors);
                    }

                    md.CreateTable(new string[] { "Type", "Name", "Description", "Acessors" }, null, propertyTypes.ToArray(), propertyNames.ToArray(), propertyDescs.ToArray(), propertyAcessors.ToArray());
                }

                if (c.methods.Count > 0)
                {
                    md.H2("Methods");

                    foreach (var method in c.methods)
                    {
                        string methodSignature = method.methodInfo.GetModfiersString() + method.methodInfo.ReturnType.GetTypeNameMarkdownText() + " " + method.methodInfo.Name + "(";

                        for (int i = 0; i < method.parameters.Count; i++)
                        {
                            if (i > 0)
                            {
                                methodSignature += ", ";
                            }

                            methodSignature += method.parameters[i].parameterInfo.ParameterType.GetTypeNameMarkdownText() + " " + method.parameters[i].parameterInfo.Name;
                        }

                        methodSignature += ")";

                        md.H3("**" + methodSignature + "**");

                        if(!method.commentData.summary.IsEmpty())
                        {
                            md += "**Summary:** " + method.commentData.summary;
                        }

                        if (!method.commentData.remarks.IsEmpty())
                        {
                            md += "**Remarks:** " + method.commentData.remarks;
                        }

                        if (method.parameters.Count > 0)
                        {
                            List<string> paramModifiers = new List<string>();
                            List<string> paramTypes = new List<string>();
                            List<string> paramNames = new List<string>();
                            List<string> paramDescriptions = new List<string>();

                            for (int i = 0; i < method.parameters.Count; i++)
                            {
                                paramModifiers.Add(method.parameters[i].parameterInfo.GetModifiersString());
                                paramTypes.Add(method.parameters[i].parameterInfo.ParameterType.GetTypeNameMarkdownText());
                                paramNames.Add(method.parameters[i].parameterInfo.Name);
                                paramDescriptions.Add(method.parameters[i].desc);
                            }

                            md.CreateTable(new string[] { "Modifiers", "Type", "Parameter", "Description" },
                                new Alignment[] { Alignment.Left, Alignment.Left, Alignment.Center, Alignment.Left },
                                paramModifiers.ToArray(), paramTypes.ToArray(), paramNames.ToArray(), paramDescriptions.ToArray());
                        }
                    }
                }
            }

            File.WriteAllText(Environment.CurrentDirectory + outFolder + "/GeneratedExample.md", md.Value);
        }

        /// <summary>
        /// Gets the path of the xml documentation file in xmlPath (can be a relative or an absolute path)
        /// </summary>
        string GetXmlPath()
        {
            if(File.Exists(xmlPath))
            {
                return xmlPath;
            }
            else if (File.Exists(Environment.CurrentDirectory + xmlPath))
            {
                return Environment.CurrentDirectory + xmlPath;
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Extracts xml comment information into a CommentData given an xml node
        /// </summary>
        /// <param name="_node">The node to extract the information from</param>
        /// <returns>The xml comment data populated with the xml's information</returns>
        CommentData GetCommentData(XmlNode _node)
        {
            CommentData commentData = new CommentData();

            if (_node != null)
            {
                commentData.summary = _node.SelectSingleNode("summary")?.InnerText.CleanString();
                commentData.remarks = _node.SelectSingleNode("remarks")?.InnerText.CleanString();
            }

            return commentData;
        }

        void LoadConfigs()
        {
            assemblyPath = ConfigurationManager.AppSettings["assemblyFilePath"];
            xmlPath = ConfigurationManager.AppSettings["xmlFilePath"];
            outFolder = ConfigurationManager.AppSettings["outputFolderPath"];

            Console.WriteLine("assemblyPath: " + assemblyPath);
            Console.WriteLine("xmlPath: " + xmlPath);
            Console.WriteLine("outFolder: " + outFolder);
        }
    }
}
