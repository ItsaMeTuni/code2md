﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLDocGen
{
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
        public string bar = "";

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
        public bool Foo(string _a, int _b, float _c)
        {
            return false;
        }
    }
}
