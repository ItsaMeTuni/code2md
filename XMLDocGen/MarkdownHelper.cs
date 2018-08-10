using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLDocGen
{
    class MarkdownHelper
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
            Value += _text + "\n";
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
        /// Create a header of level _i
        /// </summary>
        /// <param name="_i">Level of the header (i.e. how many "#" before the title)</param>
        /// <param name="_title">Title of the header</param>
        private void AddHeader(int _i, string _title)
        {
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

        public void CreateTable(string[] _headers, params string[][] _data)
        {
            string table = "";

            for (int i = 0; i < _headers.Length; i++)
            {
                if (i > 0)
                {
                    table += " | ";
                }

                table += _headers[i];
            }

            table += "\n";

            for (int i = 0; i < _headers.Length; i++)
            {
                if (i > 0)
                {
                    table += " | ";
                }

                table +="---";
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

        public static MarkdownHelper operator +(MarkdownHelper _l, string _r)
        {
            _l.AddText(_r);
            return _l;
        }
    }
}

