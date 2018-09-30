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
        /*
        public List<ParsedPart> ParseConditionals(ref string _fragment)
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
            *

            List<ParsedPart> parsedParts = new List<ParsedPart>();

            int removedChars = 0;

            while (true)
            {
                //match $tag$?{ and capture the tag name
                Match match = Regex.Match(_fragment, @"\$([^\s]*)\$\?\{");

                if (!match.Success)
                {
                    break;
                }

                string conditionalTrueString = "";

                int hierarchyLevels = 0;
                for (int i = match.Index + match.Length; i < _fragment.Length; i++)
                {
                    if (_fragment[i] == '{')
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
                    tag = (Tags)Enum.Parse(typeof(Tags), match.Captures[0].Value.Remove(match.Groups[0].Value.Length - 3, 3).Remove(0, 1)/*Removes $ and $?{ from $tag$?{, leaving only tag*);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Tag '" + match.Groups[0].Value + "' is invalid.");
                    throw e;
                }

                ParsedPart part = new ParsedPart()
                {
                    insertionIndex = match.Index,
                    text = conditionalTrueString,
                    type = ParsedPart.PartType.Conditional,
                    tag = tag
                };

                part.childParts = ParseConditionals(ref part.text); //Recursive parsing

                parsedParts.Add(part);

                _fragment = _fragment.Remove(match.Index, match.Length + conditionalTrueString.Length + 1/*The final }*);
            }

            return parsedParts;
        }

        public List<ParsedPart> ParseAreas(ref string _fragment)
        {
            List<ParsedPart> parsedParts = new List<ParsedPart>();

            while(true)
            {
                Match match = null;

                int areaTagsCount = Enum.GetNames(typeof(AreaTags)).Length;
                AreaTags tag = 0;

                for (int i = 0; i < areaTagsCount; i++)
                {
                    match = Regex.Match(_fragment,  ((AreaTags)i).Str(false) + "(.*?)" + ((AreaTags)i).Str(true));

                    if(match.Success)
                    {
                        tag = (AreaTags)i;
                        break;
                    }
                    else
                    {
                        match = null;
                    }
                }

                if(match == null)
                {
                    break;
                }

                ParsedPart part = new ParsedPart()
                {
                    type = ParsedPart.PartType.Area,
                    text = match.Groups[1].Value, //Group 0 is the full match, group 1 is the capture
                    insertionIndex = match.Index,
                    areaTag = tag,
                };

                part.childParts = ParseAreas(ref part.text);

                parsedParts.Add(part);

                _fragment = _fragment.Remove(match.Index, match.Length);
            }

            return parsedParts;
        }
        */

        public List<ParsedPart> ParseParts(ref string _str)
        {
            /*
             Example of the structure of the returned list:
                - Area
                    - Plain text
                    - Conditional
                    - Plain text
                    - Area
                        - Plain text
                        -Data
                        - Area
                            - Plain text
                            - Data
                        - Conditional
                            - Plain text
                            - Data
                - Area
                    - Conditional
                        - Plain text
                        - Data
                    - Plain text
                    - Data
                    - Area
                        - Plain text
                        - Data
             */

            List<ParsedPart> parsedParts = new List<ParsedPart>();

            while (true)
            {
                //Break if there isn't anything else to parse
                if(_str == null || _str == "")
                {
                    break;
                }

                //Try to match an area, a conditional or a data tag
                //We need to parse the one that comes first in the string to preserve the hierarchies
                bool areaMatchSuccess = TryToMatchArea(_str, out Match areaMatch, out AreaTags areaTag, out string areaContent);
                bool conditionalMatchSuccess = TryToMatchConditional(_str, out Match conditionalMatch, out Tags conditionalTag, out string conditionalContent);
                bool dataMatchSuccess = TryToMatchData(_str, out Match dataMatch, out Tags dataTag);

                {
                    //Choose which one comes first
                    areaMatchSuccess = areaMatchSuccess && (!conditionalMatchSuccess || areaMatch.Index < conditionalMatch.Index);
                    areaMatchSuccess = areaMatchSuccess && (!dataMatchSuccess || areaMatch.Index < dataMatch.Index);

                    conditionalMatchSuccess = conditionalMatchSuccess && (!areaMatchSuccess || conditionalMatch.Index < areaMatch.Index);
                    conditionalMatchSuccess = conditionalMatchSuccess && (!dataMatchSuccess || conditionalMatch.Index < dataMatch.Index);

                    dataMatchSuccess = dataMatchSuccess && (!areaMatchSuccess || dataMatch.Index < areaMatch.Index);
                    dataMatchSuccess = dataMatchSuccess && (!conditionalMatchSuccess || dataMatch.Index < conditionalMatch.Index);
                }

                if(areaMatchSuccess || conditionalMatchSuccess || dataMatchSuccess)
                {
                    Match match;
                    ParsedPart.PartType partType;
                    Tags tag;
                    string content;

                    if (areaMatchSuccess)
                    {
                        match = areaMatch;

                        //We won't ever use the normal tag when reconstructing an area, but this is just so we don't get confused when debugging
                        tag = 0;

                        partType = ParsedPart.PartType.Area;
                        content = areaContent;
                    }
                    else if (conditionalMatchSuccess)
                    {
                        match = conditionalMatch;

                        //We won't ever use the area tag when reconstructing a conditional, but this is just so we don't get confused when debugging
                        areaTag = 0;

                        tag = conditionalTag;
                        partType = ParsedPart.PartType.Conditional;
                        content = conditionalContent;
                    }
                    else //Data match success
                    {
                        match = dataMatch;

                        //We won't ever use the area tag when reconstructing a data part, but this is just so we don't get confused when debugging
                        areaTag = 0;

                        tag = dataTag;

                        partType = ParsedPart.PartType.Data;
                        content = "";
                    }

                    List<ParsedPart> childParts = new List<ParsedPart>();
                    if (areaMatchSuccess || conditionalMatchSuccess)
                    {
                        childParts = ParseParts(ref content); //Recursion
                    }

                    //Get everything before the match and make a plain text part out of it
                    string contentBefore = _str.Substring(0, match.Index);
                    ParsedPart contentBeforePart = new ParsedPart()
                    {
                        type = ParsedPart.PartType.PlainText,
                        text = contentBefore,
                    };

                    //Make the area part
                    ParsedPart part = new ParsedPart()
                    {
                        type = partType,
                        areaTag = areaTag,
                        tag = tag,
                        childParts = childParts,
                    };

                    
                    parsedParts.Add(contentBeforePart); //The part of the content before this part has to go into the list before!
                    parsedParts.Add(part);

                    //Remove tags and body of what we matched so it won't get matched again on the next iteration
                    _str = _str.Remove(0, match.Index + match.Length);
                }
                else //nothing matched, we only have plain text
                {
                    ParsedPart part = new ParsedPart()
                    {
                        type = ParsedPart.PartType.PlainText,
                        text = _str,
                    };

                    parsedParts.Add(part);

                    _str = "";
                }
            }

            return parsedParts;
        }

        bool TryToMatchData(string _str, out Match _match, out Tags _tag)
        {
            //We need to match all the tags because we could match a tag that comes
            //first in the Tags enum, while there's another tag before it in the string
            //e.g. $class_name$ $class_modifiers$ would match class_modifiers first, since it comes before class_name in Tags
            //To solve this we match all area tags and then find the one that comes first in the string

            List<Match> matches = new List<Match>();
            List<Tags> tags = new List<Tags>(); //Tags of the matches (they correspond by index)

            int tagsCount = Enum.GetNames(typeof(Tags)).Length;

            for (int i = 0; i < tagsCount; i++)
            {
                Match match = Regex.Match(_str, ((Tags)i).Str());

                if (match.Success)
                {
                    matches.Add(match);
                    tags.Add((Tags)i);
                }
            }

            if (matches.Count > 0)
            {
                int firstMatchInString = 0;
                for (int i = 1; i < matches.Count; i++)
                {
                    if (matches[i].Index < matches[firstMatchInString].Index)
                    {
                        firstMatchInString = i;
                    }
                }

                _match = matches[firstMatchInString];
                _tag = tags[firstMatchInString];
                return true;
            }
            else
            {
                _match = null;
                _tag = 0;
                return false;
            }
        }

        bool TryToMatchArea(string _str, out Match _match, out AreaTags _tag, out string _content)
        {
            //We need to match all the tags because we could match a tag that comes
            //first in the AreaTags enum, while there's another tag before it in the string
            //e.g. $START_METHODS$ $START_FIELDS$ would match FIELDS first, since it comes before METHODS in AreaTags
            //To solve this we match all area tags and then find the one that comes first in the string

            List<Match> matches = new List<Match>();
            List<AreaTags> areaTags = new List<AreaTags>(); //Tags of the matches (they correspond by index)

            int areaTagsCount = Enum.GetNames(typeof(AreaTags)).Length;

            for (int i = 0; i < areaTagsCount; i++)
            {
                Match match = Regex.Match(_str, ((AreaTags)i).Str(_endTag: false) + "(.*?)" + ((AreaTags)i).Str(_endTag: true));

                if (match.Success)
                {
                    matches.Add(match);
                    areaTags.Add((AreaTags)i);
                }
            }

            if (matches.Count > 0)
            {
                int firstMatchInString = 0;
                for (int i = 1; i < matches.Count; i++)
                {
                    if (matches[i].Index < matches[firstMatchInString].Index)
                    {
                        firstMatchInString = i;
                    }
                }

                _tag = areaTags[firstMatchInString];
                _match = matches[firstMatchInString];
                _content = _match.Groups[1].Value; //First group is the full match
                return true;
            }
            else
            {
                _tag = 0;
                _match = null;
                _content = "";
                return false;
            }
        }

        bool TryToMatchConditional(string _str, out Match _match, out Tags _tag, out string _content)
        {
            //We need to match all the tags because we could match a tag that comes
            //first in the Tags enum, while there's another tag before it in the string
            //e.g. $class_name$ $class_modifiers$ would match class_modifiers first, since it comes before class_name in Tags
            //To solve this we match all area tags and then find the one that comes first in the string

            List<Match> matches = new List<Match>();
            List<Tags> tags = new List<Tags>(); //Tags of the matches (they correspond by index)
            List<string> contents = new List<string>(); //Contents of the matches (they correspond by index)

            int tagsCount = Enum.GetNames(typeof(Tags)).Length;

            for (int i = 0; i < tagsCount; i++)
            {
                Match match = Regex.Match(_str, ((Tags)i).Str() + @"\?\{");

                if (match.Success)
                {
                    string subStr = "";

                    int level = 0;
                    for (int j = match.Index + match.Length; j < _str.Length; j++)
                    {
                        char c = _str[j];
                        if(c == '{')
                        {
                            level++;
                        }
                        else if (c == '}')
                        {
                            if(c > 0)
                            {
                                level--;
                            }
                            else
                            {
                                break;
                            }
                        }

                        subStr += c;
                    }

                    matches.Add(match);
                    tags.Add((Tags)i);
                    contents.Add(subStr);
                }
            }

            if (matches.Count > 0)
            {
                int firstMatchInString = 0;
                for (int i = 1; i < matches.Count; i++)
                {
                    if (matches[i].Index < matches[firstMatchInString].Index)
                    {
                        firstMatchInString = i;
                    }
                }

                _match = matches[firstMatchInString];
                _tag = tags[firstMatchInString];
                _content = _match.Groups[1].Value; //First group is the full match
                return true;
            }
            else
            {
                _match = null;
                _tag = 0;
                _content = "";
                return false;
            }
        }

        bool GetPlainTextPart(ref string _str, int startIndex, int endIndex, out ParsedPart _part)
        {
            string subStr = _str.Substring(startIndex, endIndex);

            if (subStr == null ||subStr == "")
            {
                _part = null;
                return false;
            }

            _part = new ParsedPart()
            {
                text = subStr,
                type = ParsedPart.PartType.PlainText
            };
            return true;
        }
    }

    public class ParsedPart
    {
        public enum PartType
        {
            Area,
            Conditional,
            Data,
            PlainText
        }

        //public int insertionIndex;
        public string text;

        public PartType type;
        public Tags tag;
        public AreaTags areaTag;

        public List<ParsedPart> childParts = new List<ParsedPart>();
    }
}
