using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;

namespace XMLDocGen
{
    enum CommentType
    {
        Type,
        Struct,
        Enum,
        Method,
        Property,
        Field
    }

    struct Param
    {
        public string name;
        public string desc;
        public string type;
    }

    struct Comment
    {
        public CommentType type;
        public string name;
        public string summary;
        public string remarks;
        public string example;
        public List<Param> parameters;
        public string ret;
    }

    class Program
    {
        string inPath = "C:/Users/lucas/Source/Repos/XMLDocGen/Documentation.xml";
        string outFolder = "C:/Users/lucas/Source/Repos/XMLDocGen/MD";

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

            List<Comment> comments = new List<Comment>();

            XmlNodeList members = xml.SelectNodes("doc/members/member");

            for (int i = 0; i < members.Count; i++)
            {
                comments.Add(MakeComment(members[i]));
            }

            GenerateMarkdown(comments);
        }

        Comment MakeComment(XmlNode _node)
        {
            Comment comment = new Comment();

            comment.type = GetCommentTypeFromName(_node.Attributes["name"].Value);
            comment.name = RemoveNamePrefix(_node.Attributes["name"].Value);

            try
            {
                comment.summary = CleanString(_node.SelectSingleNode("summary").InnerText);
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

            comment.parameters = new List<Param>();
            XmlNodeList parameters = _node.SelectNodes("param");

            string[] parameterTypes = Regex.Matches(comment.name, @"(\(|\,)[a-z A-Z 0-9 \. \[\]]*").ToArray();
            parameterTypes = Utils.RegexReplaceOnArray(parameterTypes, @"\(|\,|\)", "");
            parameterTypes = Utils.RegexMatchOnArray(parameterTypes, @"([^\.]*)$");

            for (int i = 0; i < parameters.Count; i++)
            {
                Param param = new Param();

                param.name = parameters[i].Attributes["name"].Value;
                param.desc = parameters[i].InnerText;
                param.type = parameterTypes[i];

                comment.parameters.Add(param);
            }

            return comment;
        }

        void GenerateMarkdown(List<Comment> _comments)
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

        string ToMarkdown(List<Comment> _comments)
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
                        _comments[i].parameters.ListFieldToList<string, Param>("name").ToArray(),
                        _comments[i].parameters.ListFieldToList<string, Param>("type").ToArray(),
                        _comments[i].parameters.ListFieldToList<string, Param>("desc").ToArray());
                }

                str += "\n";
            }

            return str;
        }

        string RemoveNamePrefix(string _name)
        {
            return _name.Remove(0, 2);
        }

        string CleanString(string _in)
        {
            _in = Regex.Replace(_in, @"\t|\n|\r", ""); //Remove new lines
            _in = Regex.Replace(_in, @"[ ]{2,}", " "); //Remove consecutive spaces
            _in = Regex.Replace(_in, @"^\s", ""); //Remove space at the start of the string

            return _in;
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
    }
}
