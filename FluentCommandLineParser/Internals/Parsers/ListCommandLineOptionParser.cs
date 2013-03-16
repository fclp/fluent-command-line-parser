#region License
// ListCommandLineOptionParser.cs
// Copyright (c) 2013, Simon Williams
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification, are permitted provide
// d that the following conditions are met:
// 
// Redistributions of source code must retain the above copyright notice, this list of conditions and the
// following disclaimer.
// 
// Redistributions in binary form must reproduce the above copyright notice, this list of conditions and
// the following disclaimer in the documentation and/or other materials provided with the distribution.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED 
// WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
// PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
// TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
// POSSIBILITY OF SUCH DAMAGE.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace Fclp.Internals.Parsers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListCommandLineOptionParser<T> : ICommandLineOptionParser<List<T>>
    {
        private readonly ICommandLineOptionParserFactory _parserFactory;

        /// <summary>
        /// Initialises a new instance of the <see cref="ListCommandLineOptionParser{T}"/>.
        /// </summary>
        /// <param name="parserFactory"></param>
        public ListCommandLineOptionParser(ICommandLineOptionParserFactory parserFactory)
        {
            _parserFactory = parserFactory;
        }

        /// <summary>
        /// Parses the specified <see cref="System.String"/> into the return type.
        /// </summary>
        /// <param name="value">The <see cref="System.String"/> representing the value to parse. This may be <c>null</c>, <c>empty</c> or contain only <c>whitespace</c>.</param>
        /// <returns>The parsed value.</returns>
        public List<T> Parse(string value)
        {
            var parser = _parserFactory.CreateParser<T>();

            return SplitOnWhitespace(value)
                .Where(parser.CanParse)
                .Select(parser.Parse)
                .ToList();
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.String"/> can be parsed by this <see cref="ICommandLineOptionParser{T}"/>.
        /// </summary>
        /// <param name="value">The <see cref="System.String"/> to check.</param>
        /// <returns><c>true</c> if the specified <see cref="System.String"/> can be parsed by this <see cref="ICommandLineOptionParser{T}"/>; otherwise <c>false</c>.</returns>
        public bool CanParse(string value)
        {
            if(string.IsNullOrEmpty(value)) return false;

            var parser = _parserFactory.CreateParser<T>();

            return SplitOnWhitespace(value).All(parser.CanParse);
        }

        static IEnumerable<string> SplitOnWhitespace(string args)
        {
            if (string.IsNullOrEmpty(args)) return null;
            char[] parmChars = args.ToCharArray();
            bool inQuote = false;
            for (int index = 0; index < parmChars.Length; index++)
            {
                if (parmChars[index] == '"')
                    inQuote = !inQuote;
                if (!inQuote && parmChars[index] == ' ')
                    parmChars[index] = '\n';
            }
            return (new string(parmChars)).Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
