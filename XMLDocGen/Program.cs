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
    class Program
    {
        public static string XmlPath { get; private set; }
        public static string AssemblyPath { get; private set; }
        public static string OutFolder { get; private set; }
        public static string TemplatePath { get; private set; }
        public static string CusomPagesFolderPath { get; private set; }

        public static Assembly assembly;
        static XmlNodeList xml;

        static void Main(string[] args)
        {
            LoadConfigs();

            Program program = new Program();
            program.Generate();

            Console.ReadLine();
        }

        void Generate()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XmlPath);
            xml = xmlDoc.SelectNodes("doc/members/member");

            //Load assembly


            //Currently we load the current assembly for testing purposes
            assembly = Assembly.GetAssembly(typeof(Program));

            //assembly = Assembly.ReflectionOnlyLoadFrom(assemblyPath);
            /*
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

            string template = File.ReadAllText(TemplatePath);
            List<TypeData> typeDatas = new DataGatherer(assembly, xml).GetTypeData();

            TemplateReplacer replacer = new TemplateReplacer(template, typeDatas);

            PageGenerator generator = new PageGenerator(replacer.Replace());
            generator.Generate();
        }

        public static string ToAbsolutePath(string _path)
        {
            if (File.Exists(Environment.CurrentDirectory + _path) || Directory.Exists(Environment.CurrentDirectory + _path))
            {
                return Environment.CurrentDirectory + _path;
            }
            else if (File.Exists(_path) || Directory.Exists(_path))
            {
                return _path;
            }
            else
            {
                throw new Exception();
            }
        }

        static void LoadConfigs()
        {
            //AssemblyPath = ToAbsolutePath(ConfigurationManager.AppSettings["assemblyFilePath"]);
            XmlPath = ToAbsolutePath(ConfigurationManager.AppSettings["xmlFilePath"]);
            OutFolder = ToAbsolutePath(ConfigurationManager.AppSettings["outputFolderPath"]);
            TemplatePath = ToAbsolutePath(ConfigurationManager.AppSettings["templateFilePath"]);
            CusomPagesFolderPath = ToAbsolutePath(ConfigurationManager.AppSettings["cusomPagesFolderPath"]);

            Console.WriteLine("assemblyPath: " + AssemblyPath);
            Console.WriteLine("xmlPath: " + XmlPath);
            Console.WriteLine("outFolder: " + OutFolder);
            Console.WriteLine("templatePath: " + TemplatePath);
        }
    }
}
