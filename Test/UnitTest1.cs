using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XMLDocGen;
using System.Reflection;
using System.Xml;

namespace Test
{
    [TestClass]
    public class TemplateParserTester
    {
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        public string fragmn = "$param_modifiers$?{MODIFIERS} $param_name$?{NAME: $param_desc$?{DESC}}";

        public string fragmn2 = 
            "$START_CLASS$ " +
            "$START_FIELDS$ " +
            "Fields: " +
            "$START_FIELD$ " +
            "this is a field. " +
            "$field_name$ " +
            "$END_FIELD$ " +
            "this aint a field anymore. " +
            "$END_FIELDS$ " +
            "Proceed to methods. " +
            "$END_CLASS$";

        public string fragmn3 =
            "$START_CLASS$" +
            "$class_name$" +
            "$END_CLASS$" +
            "$START_ENUM$" +
            "$enum_name$" +
            "$END_ENUM$";

        [TestMethod]
        public void TestParse()
        {
            TemplateParser parser = new TemplateParser();
            var parsedParts = parser.ParseParts(ref fragmn2);

            for (int i = 0; i < parsedParts.Count; i++)
            {
                PrintPart(parsedParts[i], 0);
            }
        }

        [TestMethod]
        public void TestLink()
        {
            TemplateParser parser = new TemplateParser();
            var parsedParts = parser.ParseParts(ref fragmn3);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("XMLDocGen.xml");
            XmlNodeList xml = xmlDoc.SelectNodes("doc/members/member");

            DataGatherer dataGatherer = new DataGatherer(Assembly.GetAssembly(typeof(DataGatherer)), xml);
            var typeDatas = dataGatherer.GetTypeData();

            OutputBuilder builder = new OutputBuilder();
            var linkedStuff = builder.LinkPartsToData(parsedParts, typeDatas, TagContext.Empty);

            
            for (int i = 0; i < linkedStuff.Count; i++)
            {
                PrintLinkedPart(linkedStuff[i], 0);
            }
            
        }

        void PrintPart(ParsedPart _part, int level)
        {
            string str = "- " + _part.type.ToString() + ": ";

            if (_part.type == ParsedPart.PartType.Area)
            {
                str += _part.areaTag.ToString();
            }
            else if (_part.type == ParsedPart.PartType.Conditional)
            {
                str += _part.tag.ToString();
            }
            else if (_part.type == ParsedPart.PartType.Data)
            {
                str += _part.tag.ToString();
            }
            else
            {
                str += "\"" + _part.text + "\"";
            }

            for (int i = 0; i < level; i++)
            {
                str = str.Insert(0, "    ");
            }

            Console.WriteLine(str);

            for (int i = 0; i < _part.childParts.Count; i++)
            {
                PrintPart(_part.childParts[i], level + 1);
            }
        }

        void PrintLinkedPart(LinkedPart _part, int level)
        {
            string str = "- ";

            if (_part.parsedPart.type == ParsedPart.PartType.Area)
            {
                str += _part.parsedPart.areaTag;
            }
            else if (_part.parsedPart.type == ParsedPart.PartType.Conditional || _part.parsedPart.type == ParsedPart.PartType.Data)
            {
                str += _part.parsedPart.tag;
            }
            else
            {
                str += "\"" + _part.parsedPart.text + "\"";
            }

            str += "(" + _part.parsedPart.type.ToString() + "): {";

            str += _part.context.typeData != null ? _part.context.typeData.typeInfo.Name + " " : " ";
            str += _part.context.fieldData != null ? _part.context.fieldData.fieldInfo.Name + " " : " ";
            str += _part.context.methodData != null ? _part.context.methodData.methodInfo.Name + " " : " ";
            str += _part.context.paramData != null ? _part.context.paramData.parameterInfo.Name + " " : " ";
            str += _part.context.propertyData != null ? _part.context.propertyData.propertyInfo.Name + " " : " ";

            str += "}";

            for (int i = 0; i < level; i++)
            {
                str = str.Insert(0, "    ");
            }

            Console.WriteLine(str);

            for (int i = 0; i < _part.children.Count; i++)
            {
                PrintLinkedPart(_part.children[i], level + 1);
            }
        }

    } 
}
