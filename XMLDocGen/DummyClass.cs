using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLDocGen
{
    /// <summary>
    /// It is a disguised int! Be careful...
    /// </summary>
    enum DummyEnum
    {
        /// <summary>
        /// First letter of the alphabet!
        /// </summary>
        element_a,
        /// <summary>
        /// Second letter of the alphabet!
        /// </summary>
        element_b,
        /// <summary>
        /// Whoah you skipped one.
        /// </summary>
        element_d,
        /// <summary>
        /// This ain't a letter anymore.
        /// </summary>
        element_5,
        /// <summary>
        /// what
        /// </summary>
        element_alpha
    }

    /// <summary>
    /// It's such a fool!
    /// </summary>
    /// <remarks>
    /// None, pretty useless. Undefined behaviour. Whatever.
    /// </remarks>
    class DummyClass
    {
        /// <summary>
        /// What's this
        /// </summary>
        /// <remarks>
        /// Sure, why not?
        /// </remarks>
        public string bar = "";

        /// <summary>
        /// WHAT EVER
        /// </summary>
        /// <remarks>
        /// SuRR, wHy N00t?
        /// </remarks>
        public string fubar = "";

        /// <summary>
        /// I'm lost already
        /// </summary>
        /// <remarks>
        /// LorEm ipSuM DolOR sIT aMeT
        /// </remarks>
        public string dafuq = "";

        /// <summary>
        /// It has intersting qualities
        /// </summary>
        /// <remarks>
        /// HEYA E HEYEHEYEHYEHYA E
        /// </remarks>
        public string aProp { get; set; }

        /// <summary>
        /// It has second tier qualities
        /// </summary>
        /// <remarks>
        /// HEYAFASDFEAS
        /// </remarks>
        public string bProp { get; set; }

        /// <summary>
        /// Does foo
        /// </summary>
        /// <remarks>
        /// Remarkable indeed
        /// </remarks>
        /// <param name="_a">I dont know what this does</param>
        /// <param name="_b">neither this</param>
        /// <param name="_c">or this</param>
        /// <returns>returns either true or false, might be random</returns>
        public bool Foo(ref List<string> _a, int _b, out float _c)
        {
            _c = 0;
            return false;
        }
    }
}
