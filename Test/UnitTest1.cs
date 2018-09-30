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
    } 
}
