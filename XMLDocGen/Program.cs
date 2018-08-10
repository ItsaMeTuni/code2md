using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;

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
        const string inPath = "C:/Users/lucas/Source/Repos/XMLDocGen/XMLDocGen/bin/Debug/XMLDocGen.xml";
        const string assemblyPath = "C:/Users/lucas/Source/Repos/XMLDocGen/bin/Debug/Assembly-CSharp.dll";
        const string outFolder = "C:/Users/lucas/Source/Repos/XMLDocGen/";

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
            xml.Load(inPath);

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
                ClassData classData = new ClassData();

                classData.typeInfo = types[type].GetTypeInfo();

                XmlNode classNode = _xml.FindMemberWithName(types[type].FullName);
                if (classNode != null)
                {
                    classData.summary = classNode.SelectSingleNode("summary")?.InnerText.CleanString();
                    classData.summary = classNode.SelectSingleNode("remarks")?.InnerText.CleanString();
                }


                MethodInfo[] methods = types[type].GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

                for (int method = 0; method < methods.Length; method++)
                {
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
                    FieldData fieldData = new FieldData();

                    fieldData.fieldInfo = fields[field];

                    XmlNode fieldNode = _xml.FindFieldMemberWithName(types[type].FullName + "." + fields[field].Name);
                    fieldData.summary = fieldNode?.SelectSingleNode("summary")?.InnerText.CleanString();
                    fieldData.remarks = fieldNode?.SelectSingleNode("remarks")?.InnerText.CleanString();

                    classData.fields.Add(fieldData);
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

                foreach (var method in c.methods)
                {
                    string h2 = method.methodInfo.ReturnType.GetTypeNameMarkdownText() + " " + method.methodInfo.Name + "(";

                    for (int i = 0; i < method.parameters.Count; i++)
                    {
                        if(i > 0)
                        {
                            h2 += ", ";
                        }

                        h2 += method.parameters[i].parameterInfo.ParameterType.GetTypeNameMarkdownText() + " " + method.parameters[i].parameterInfo.Name;
                    }

                    h2 += ")";

                    md.H2(h2);
                    md +=  method.summary;

                    if (!method.remarks.IsEmpty())
                    {
                        md.H3("Remarks");
                        md += method.remarks;
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
                            new Alignment[] { Alignment.Center, Alignment.Center, Alignment.Left}, 
                            paramTypes.ToArray(), paramNames.ToArray(), paramDescriptions.ToArray());
                    }
                }

                if(c.fields.Count > 0)
                {
                    md.H2("Fields");
                }

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

            File.WriteAllText(outFolder + "/GeneratedExample.md", md.Value);
        }

        /*
        OldComment MakeComment(XmlNode _node)
        {
            OldComment comment = new OldComment();

            comment.type = GetCommentTypeFromName(_node.Attributes["name"].Value);
            //comment.name = RemoveNamePrefix(_node.Attributes["name"].Value);

            try
            {
                //comment.summary = CleanString(_node.SelectSingleNode("summary").InnerText);
            }
            catch
            {
                comment.summary = null;
            }

            try
            {
                comment.remarks = _node.SelectSingleNode("remarks").Value;
            }
            catch
            {
                comment.remarks = null;
            }

            comment.parameters = new List<ParameterData>();
            XmlNodeList parameters = _node.SelectNodes("param");

            string[] parameterTypes = Regex.Matches(comment.name, @"(\(|\,)[a-z A-Z 0-9 \. \[\]]*").ToArray();
            parameterTypes = Utils.RegexReplaceOnArray(parameterTypes, @"\(|\,|\)", "");
            parameterTypes = Utils.RegexMatchOnArray(parameterTypes, @"([^\.]*)$");

            for (int i = 0; i < parameters.Count; i++)
            {
                ParameterData param = new ParameterData();

                param.name = parameters[i].Attributes["name"].Value;
                param.desc = parameters[i].InnerText;
                param.type = parameterTypes[i];

                comment.parameters.Add(param);
            }

            return comment;
        }

        void GenerateMarkdown(List<OldComment> _comments)
        {
            using (FileStream fileStream = new FileStream(outFolder + "/gen.md", FileMode.OpenOrCreate))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    string str = ToMarkdown(_comments);

                    streamWriter.Write(str);
                }
            }
        }

        string ToMarkdown(List<OldComment> _comments)
        {
            string str = "";

            for (int i = 0; i < _comments.Count; i++)
            {
                str += "# " + _comments[i].name + " (" + _comments[i].type.ToString() + ")" + "\n";
                str += _comments[i].summary + "\n";

                str += "\n";

                if (_comments[i].type == CommentType.Method)
                {
                    str += MarkdownHelper.CreateTable(
                        new string[] { "Parameter", "Type", "Description" },
                        _comments[i].parameters.ListFieldToList<string, ParameterData>("name").ToArray(),
                        _comments[i].parameters.ListFieldToList<string, ParameterData>("type").ToArray(),
                        _comments[i].parameters.ListFieldToList<string, ParameterData>("desc").ToArray());
                }

                str += "\n";
            }

            return str;
        }



        CommentType GetCommentTypeFromName(string _name)
        {
            if (_name[0] == 'T')
            {
                return CommentType.Type;
            }
            else if (_name[0] == 'M')
            {
                return CommentType.Method;
            }
            else if (_name[0] == 'S')
            {
                return CommentType.Struct;
            }
            else if (_name[0] == 'P')
            {
                return CommentType.Property;
            }
            else if (_name[0] == 'E')
            {
                return CommentType.Enum;
            }
            else if (_name[0] == 'F')
            {
                return CommentType.Field;
            }
            else
            {
                throw new Exception();
            }
        }
        */
    }
}
