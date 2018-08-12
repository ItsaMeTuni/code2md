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
        static string xmlPath = "";
        static string assemblyPath = "";
        static string outFolder = "";

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
