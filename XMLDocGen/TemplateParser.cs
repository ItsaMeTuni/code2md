using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace XMLDocGen
{
    public class TemplateParser
    {
        public List<ParsedConditionalPart> ParseConditionals(ref string _fragment)
        {
            /*
            long tagStartPos = -1;
            long tagEndPos = -1;

            for (int i = 0; i < _fragment.Length; i++)
            {
                if(_fragment[i] == '$' && (i > 0 && _fragment[i-1] != '\\'))
                {
                    tagStartPos = i;
                }
                else if (tagStartPos >= 0 && tagEndPos == -1)
                {
                    if(_fragment[i] == '$')
                    {
                        tagEndPos = i;
                    }
                    else if (_fragment[i] == ' ' || i + 1 >= _fragment.Length)
                    {
                        Console.WriteLine("Tag starting at " + tagStartPos.ToString() + " is not closed. ");
                        tagStartPos = -1;
                        tagEndPos = -1;
                    }
                }
            }
            */

            List<ParsedConditionalPart> parsedParts = new List<ParsedConditionalPart>();

            int removedChars = 0;

            while(true)
            {
                //match $tag$?{ and capture the tag name
                Match match = Regex.Match(_fragment, @"\$([^\s]*)\$\?\{");

                if(!match.Success)
                {
                    break;
                }

                string conditionalTrueString = "";

                int hierarchyLevels = 0;
                for (int i = match.Index + match.Length; i < _fragment.Length; i++)
                {
                    if(_fragment[i] == '{')
                    {
                        hierarchyLevels++;
                    }
                    else if (_fragment[i] == '}')
                    {
                        if (hierarchyLevels > 0)
                        {
                            hierarchyLevels--;
                        }
                        else
                        {
                            break;
                        }
                    }

                    conditionalTrueString += _fragment[i];
                }

                Tags tag = 0;

                try
                {
                    tag = (Tags)Enum.Parse(typeof(Tags), match.Captures[0].Value.Remove(match.Groups[0].Value.Length - 3, 3).Remove(0, 1)/*Removes $ and $?{ from $tag$?{, leaving only tag*/);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Tag '" + match.Groups[0].Value + "' is invalid.");
                    throw e;
                }

                ParsedConditionalPart part = new ParsedConditionalPart()
                {
                    insertionIndex = match.Index,
                    text = conditionalTrueString,
                    conditionalTag = tag
                };

                part.childrenParts = ParseConditionals(ref part.text); //Recursive parsing

                parsedParts.Add(part);

                _fragment = _fragment.Remove(match.Index, match.Length + conditionalTrueString.Length + 1/*The final }*/);
            }

            return parsedParts;
        }
    }

    public class ParsedConditionalPart
    {
        public int insertionIndex;
        public string text;
        public Tags conditionalTag;

        public List<ParsedConditionalPart> childrenParts = new List<ParsedConditionalPart>();
    }
}
