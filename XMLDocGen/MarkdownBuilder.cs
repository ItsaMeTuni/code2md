﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace XMLDocGen
{
    public enum Alignment
    {
        Left,
        Right,
        Center
    }

    class MarkdownBuilder
    {
        /// <summary>
        /// Current value of the markdown text
        /// </summary>
        public string Value { get; private set; } = "";

        /// <summary>
        /// Adds a line of text and a new line at the end
        /// </summary>
        /// <param name="_text"></param>
        public void AddText(string _text)
        {
            if(_text == "" || _text == null)
            {
                return;
            }

            Value += _text;
            EmptyLine();
        }

        /// <summary>
        /// Adds an empty line to the text
        /// </summary>
        public void EmptyLine()
        {
            Value += "  \n";
        }

        /// <summary>
        /// Create a level 1 header (i.e. #_title)
        /// </summary>
        /// <param name="_title">Title of the header</param>
        public void H1(string _title)
        {
            AddHeader(1, _title);
        }

        /// <summary>
        /// Create a level 2 header  (i.e. ##_title)
        /// </summary>
        /// <param name="_title">Title of the header</param>
        public void H2(string _title)
        {
            AddHeader(2, _title);
        }

        /// <summary>
        /// Create a level 3 header (i.e. ###_title)
        /// </summary>
        /// <param name="_title">Title of the header</param>
        public void H3(string _title)
        {
            AddHeader(3, _title);
        }

        /// <summary>
        /// Create a level 4 header (i.e. ####_title)
        /// </summary>
        /// <param name="_title">Title of the header</param>
        public void H4(string _title)
        {
            AddHeader(4, _title);
        }

        public void Space1()
        {
            AddSpace(1);
        }

        public void Space2()
        {
            AddSpace(2);
        }

        public void Space3()
        {
            AddSpace(3);
        }

        public void Bullet(string _text)
        {
            AddText("- " + _text);
        }

        /// <summary>
        /// Create a header of level _i
        /// </summary>
        /// <param name="_i">Level of the header (i.e. how many "#" before the title)</param>
        /// <param name="_title">Title of the header</param>
        private void AddHeader(int _i, string _title)
        {
            if(_title == "")
            {
                return;
            }

            if(_i <= 0)
            {
                throw new Exception();
            }

            string str = "";

            for (int i = 0; i < _i; i++)
            {
                str += "#";
            }
            str += " " + _title;

            AddText(str);
        }

        private void AddSpace(int _i)
        {
            string str = "";

            for (int i = 0; i < _i; i++)
            {
                str += "<br/> \n";
            }

            Value += str + "\n";
        }

        public void CreateTable(string[] _headers, Alignment[] _alignments = null, params string[][] _data)
        {
            string table = "";

            for (int i = 0; i < _headers.Length; i++)
            {
                table += " | ";

                table += _headers[i];
            }

            table += "\n";

            for (int i = 0; i < _headers.Length; i++)
            {
                table += "|";

                if (_alignments == null || _alignments.Length <= i || _alignments[i] == Alignment.Left || _alignments[i] == Alignment.Center)
                {
                    table += ":";
                }

                table +="---";

                if ((_alignments != null && _alignments.Length > i) && (_alignments[i] == Alignment.Right || _alignments[i] == Alignment.Center))
                {
                    table += ":";
                }
            }

            table += "\n";

            for (int row = 0; row < _data[0].Length; row++)
            {
                for (int col = 0; col < _data.Length; col++)
                {
                    if(col > 0)
                    {
                        table += " | ";
                    }

                    //One column might be shorter than the other
                    try
                    {
                        table += _data[col][row];
                    }
                    catch { }
                }

                table += "\n";
            }

            AddText(table);
        }

        public static string CreateHeaderLink(string _text, string _toHeader, int _headerLevel)
        {
            string link = "[" + _text + "](";

            for (int i = 0; i < _headerLevel; i++)
            {
                link += "#";
            }

            _toHeader = Regex.Replace(_toHeader, @" ", "-");
            _toHeader = Regex.Replace(_toHeader, @"\.", "");
            _toHeader = Regex.Replace(_toHeader, @"\(|\)", "");
            _toHeader = _toHeader.ToLowerInvariant();

            link += _toHeader + ")";

            return link;
        }

        public static string CreatePageLink(string _text, string _pagePath)
        {
            return "[" + _text + "](" + _pagePath + ")";
        }

        public static string CreateCode(string _code)
        {
            return "`" + _code + "`";
        }

        public static string Escape(string _text)
        {
            string str = _text;

            str = Regex.Replace(str, @"_", @"\_");

            return str;
        }

        public static MarkdownBuilder operator +(MarkdownBuilder _l, string _r)
        {
            _l.AddText(_r);
            return _l;
        }
    }
}

