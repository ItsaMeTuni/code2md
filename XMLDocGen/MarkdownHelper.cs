using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLDocGen
{
    class MarkdownHelper
    {
        public static string CreateTable(string[] _headers, params string[][] _data)
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
                    table += "|";
                }

                table += "---";
            }

            table += "\n";

            for (int iRow = 0; iRow < _data[0].Length; iRow++)
            {
                for (int iCol = 0; iCol < _data.Length; iCol++)
                {
                    if(iCol > 0)
                    {
                        table += " | ";
                    }

                    table += _data[iCol][iRow];
                }

                table += "\n";
            }

            return table;
        }
    }
}

