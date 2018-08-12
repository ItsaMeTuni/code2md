using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace XMLDocGen
{
    public class Extractor
    {
        public static string ExtractMethodNameFromXmlName(string _XMLsignature)
        {
            string str = _XMLsignature;
            str = RemoveNamePrefix(str);

            str = Regex.Match(str, @"([^\(]*)").Value; //Read up until first parenthesis

            return str;
        }

        public static string ExtractFieldNameFromXmlName(string _XMLsignature)
        {
            string str = _XMLsignature;
            str = RemoveNamePrefix(str);

            return str;
        }

        /*
        private string[] ExtractParameterTypesFromMethodSigature(string _signature)
        {
            string[] parameterTypes = Regex.Matches(_signature, @"(\(|\,)[a-z A-Z 0-9 \. \[\]]*").ToArray();    //Separate parameters
            parameterTypes = Utils.RegexReplaceOnArray(parameterTypes, @"\(|\,|\)", "");                        //Remove '(' ')' ','
            //parameterTypes = Utils.RegexMatchOnArray(parameterTypes, @"([^\.]*)$");                             //Get only last name of the parameter type (remove parent name)

            return parameterTypes;
        }
        */

        static string RemoveNamePrefix(string _name)
        {
            return _name.Remove(0, 2);
        }

        public static string CleanString(string _in)
        {
            _in = Regex.Replace(_in, @"\t|\n|\r", ""); //Remove new lines
            _in = Regex.Replace(_in, @"[ ]{2,}", " "); //Remove consecutive spaces
            _in = Regex.Replace(_in, @"^\s", ""); //Remove space at the start of the string

            return _in;
        }
    }
}
