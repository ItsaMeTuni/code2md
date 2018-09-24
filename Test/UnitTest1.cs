using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XMLDocGen;

namespace Test
{
    [TestClass]
    public class TemplateParserTester
    {
        public string fragmn = "$param_modifiers$?{MODIFIERS} $param_name$?{NAME: $param_desc$?{DESC}}";

        [TestMethod]
        public void TestConditionalParse()
        {
            TemplateParser parser = new TemplateParser();
            var parsedParts = parser.ParseConditionals(ref fragmn);

            Assert.IsTrue(parsedParts.Count == 2);
            Assert.IsTrue(parsedParts[0].conditionalTag == Tags.param_modifiers);
            Assert.IsTrue(parsedParts[0].text == "MODIFIERS");

            Assert.IsTrue(parsedParts[1].conditionalTag == Tags.param_name);
            Assert.IsTrue(parsedParts[1].text == "NAME: ");

            Assert.IsTrue(parsedParts[1].childrenParts.Count == 1);
            Assert.IsTrue(parsedParts[1].childrenParts[0].conditionalTag == Tags.param_desc);
            Assert.IsTrue(parsedParts[1].childrenParts[0].text == "DESC");
        }
    }
}
