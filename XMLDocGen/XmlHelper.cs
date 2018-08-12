using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLDocGen
{
    static class XmlHelper
    {
        /// <summary>
        /// Searches for a method xml node with name _methodName (ignoring parameters and prefix)
        /// </summary>
        /// <param name="_nodeList">The xml node list to search in</param>
        /// <param name="_methodName">The name of the method you're looking for</param>
        public static XmlNode FindMethodMemberWithName(this XmlNodeList _nodeList, string _methodName)
        {
            for (int i = 0; i < _nodeList.Count; i++)
            {
                if (Extractor.ExtractMethodNameFromXmlName(_nodeList[i].Attributes["name"].Value) == _methodName)
                {
                    return _nodeList[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Searches for an xml node with name _memberName
        /// </summary>
        /// <param name="_nodeList">The xml node list to search in</param>
        /// <param name="_memberName">The name of the member you're looking for</param>
        public static XmlNode FindMemberWithName(this XmlNodeList _nodeList, string _memberName)
        {
            for (int i = 0; i < _nodeList.Count; i++)
            {
                if (_nodeList[i].Attributes["name"].Value == _memberName)
                {
                    return _nodeList[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Searches for a field xml node with name _fieldName (ignoring prefix)
        /// </summary>
        /// <param name="_nodeList">The xml node list to search in</param>
        /// <param name="_fieldName">The name of the field you're looking for</param>
        public static XmlNode FindFieldMemberWithName(this XmlNodeList _nodeList, string _fieldName)
        {
            for (int i = 0; i < _nodeList.Count; i++)
            {
                if (Extractor.ExtractFieldNameFromXmlName(_nodeList[i].Attributes["name"].Value) == _fieldName)
                {
                    return _nodeList[i];
                }
            }

            return null;
        }
    }
}
