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

namespace XMLDocGen
{
    public class ParameterData
    {
        public ParameterInfo parameterInfo;

        public string desc;
    }

    public class MethodData
    {
        public MethodInfo methodInfo;

        public List<ParameterData> parameters = new List<ParameterData>();
        public string returns;
        public string remarks;
        public string summary;
    }

    public class ClassData
    {
        public TypeInfo typeInfo;

        public List<MethodData> methods = new List<MethodData>();
        public List<FieldData> fields = new List<FieldData>();
        public List<PropertyData> properties = new List<PropertyData>();
        public string summary;
        public string remarks;
    }

    public class FieldData
    {
        public FieldInfo fieldInfo;

        public string summary;
        public string remarks;
    }

    public class PropertyData
    {
        public PropertyInfo propertyInfo;

        public string summary;
        public string remarks;
    }

    class Program
    {
        static string xmlPath = "/XMLDocGen.xml";
        static string assemblyPath = "/Assembly-CSharp.dll";
        static string outFolder = "/../../../";

        List<ClassData> classes = new List<ClassData>();

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Generate();

            Console.ReadLine();
        }

        void Generate()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(GetXmlPath());

            XmlNodeList members = xml.SelectNodes("doc/members/member");

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

            ReadAssembly(Assembly.GetAssembly(typeof(Program)), members);
            ToMarkdown();
        }

        /// <summary>
        /// Reads the assembly and creates class/method/field data used to generate the markdown
        /// </summary>
        /// <param name="_assembly"></param>
        /// <param name="_xml"></param>
        void ReadAssembly(Assembly _assembly, XmlNodeList _xml)
        {
            Type[] types = _assembly.GetTypes();

            for (int type = 0; type < types.Length; type++)
            {
                if(types[type].IsCompilerGenerated())
                {
                    continue;
                }

                ClassData classData = new ClassData();

                classData.typeInfo = types[type].GetTypeInfo();

                XmlNode classNode = _xml.FindMemberWithName(types[type].FullName);
                if (classNode != null)
                {
                    classData.summary = classNode.SelectSingleNode("summary")?.InnerText.CleanString();
                    classData.remarks = classNode.SelectSingleNode("remarks")?.InnerText.CleanString();
                }


                MethodInfo[] methods = types[type].GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                for (int method = 0; method < methods.Length; method++)
                {
                    if(methods[method].IsCompilerGenerated())
                    {
                        continue;
                    }

                    MethodData methodData = new MethodData();
                    methodData.methodInfo = methods[method];
                    
                    XmlNode methodNode = _xml.FindMethodMemberWithName(types[type].FullName + "." + methods[method].Name);
                    if (methodNode != null)
                    {
                        methodData.summary = methodNode.SelectSingleNode("summary")?.InnerText.CleanString();
                        methodData.remarks = methodNode.SelectSingleNode("remarks")?.InnerText.CleanString();
                        methodData.returns = methodNode.SelectSingleNode("returns")?.InnerText.CleanString();


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

                    XmlNode fieldNode = _xml.FindFieldMemberWithName(types[type].FullName + "." + fields[field].Name);
                    fieldData.summary = fieldNode?.SelectSingleNode("summary")?.InnerText.CleanString();
                    fieldData.remarks = fieldNode?.SelectSingleNode("remarks")?.InnerText.CleanString();

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

                    XmlNode propertyNode = _xml.FindFieldMemberWithName(types[type].FullName + "." + properties[property].Name);
                    if (propertyNode != null)
                    {
                        propertyData.summary = propertyNode.SelectSingleNode("summary")?.InnerText.CleanString();
                        propertyData.remarks = propertyNode.SelectSingleNode("remarks")?.InnerText.CleanString();
                    }

                    classData.properties.Add(propertyData);
                }

                classes.Add(classData);
            }
        }

        void PrintEverything()
        {
            foreach (var c in classes)
            {
                string str = "";

                str += c.typeInfo.FullName + "\n";
                str += "  " + c.summary + "\n";

                foreach (var method in c.methods)
                {
                    str += "    " + method.methodInfo.Name + "\n";
                    str += "        " + method.summary + "\n";
                }

                Console.WriteLine(str);
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
                md += c.summary;

                if (c.fields.Count > 0)
                {
                    md.H2("Fields");
                
                    List<string> fieldTypes = new List<string>();
                    List<string> fieldNames = new List<string>();
                    List<string> fieldDescs = new List<string>();

                    foreach (var field in c.fields)
                    {
                        fieldTypes.Add(field.fieldInfo.FieldType.GetTypeNameMarkdownText());
                        fieldNames.Add(field.fieldInfo.Name);

                        string desc = "";

                        if (!field.summary.IsEmpty())
                        {
                            desc += "**Summary:** ";
                            desc += field.summary;
                            desc += "  ";
                        }

                        if (!field.remarks.IsEmpty())
                        {
                            desc += "**Remarks:** ";
                            desc += field.remarks;
                            desc += "  ";
                        }

                        fieldDescs.Add(desc);
                    }

                    md.CreateTable(new string[] { "Type", "Name", "Description" }, null, fieldTypes.ToArray(), fieldNames.ToArray(), fieldDescs.ToArray());
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

                        if (!property.summary.IsEmpty())
                        {
                            desc += "**Summary:** ";
                            desc += property.summary;
                            desc += "  ";
                        }

                        if (!property.remarks.IsEmpty())
                        {
                            desc += "**Remarks:** ";
                            desc += property.remarks;
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
                        string methodSignature = method.methodInfo.ReturnType.GetTypeNameMarkdownText() + " " + method.methodInfo.Name + "(";

                        for (int i = 0; i < method.parameters.Count; i++)
                        {
                            if (i > 0)
                            {
                                methodSignature += ", ";
                            }

                            methodSignature += method.parameters[i].parameterInfo.ParameterType.GetTypeNameMarkdownText() + " " + method.parameters[i].parameterInfo.Name;
                        }

                        methodSignature += ")";

                        md.H3(methodSignature);

                        if(!method.summary.IsEmpty())
                        {
                            md += "**Summary:** " + method.summary;
                        }

                        if (!method.remarks.IsEmpty())
                        {
                            md += "**Remarks:** " + method.remarks;
                        }

                        if (method.parameters.Count > 0)
                        {
                            List<string> paramTypes = new List<string>();
                            List<string> paramNames = new List<string>();
                            List<string> paramDescriptions = new List<string>();

                            for (int i = 0; i < method.parameters.Count; i++)
                            {
                                paramTypes.Add(method.parameters[i].parameterInfo.ParameterType.GetTypeNameMarkdownText());
                                paramNames.Add(method.parameters[i].parameterInfo.Name);
                                paramDescriptions.Add(method.parameters[i].desc);
                            }

                            md.CreateTable(new string[] { "Type", "Parameter", "Description" },
                                new Alignment[] { Alignment.Center, Alignment.Center, Alignment.Left },
                                paramTypes.ToArray(), paramNames.ToArray(), paramDescriptions.ToArray());
                        }
                    }
                }
            }

            File.WriteAllText(Environment.CurrentDirectory + outFolder + "/GeneratedExample.md", md.Value);
        }

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
    }
}
