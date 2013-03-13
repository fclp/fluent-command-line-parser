#region License
// CommandLineParserEngine.cs
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
using System.Globalization;
using System.Linq;

namespace Fclp.Internals
{
    /// <summary>
    /// Simple parser for transforming command line arguments into simple key and value pairs.
    /// </summary>
    public class CommandLineParserEngine : ICommandLineParserEngine
    {
        /// <summary>
        /// Parses the specified <see><cref>T:System.String[]</cref></see> into key value pairs.
        /// </summary>
        /// <param name="args">The <see><cref>T:System.String[]</cref></see> to parse.</param>
        /// <returns>An <see cref="ICommandLineParserResult"/> representing the results of the parse operation.</returns>
        public IEnumerable<ParsedOption> Parse(string[] args)
        {
            args = args ?? new string[0];

            for (int index = 0; index < args.Length; index++)
            {
                string item = args[index];

                // we only want to find keys at this point
                if (IsAKey(item) == false) continue; 

                string key = ExtractKey(item);

                // setup the option and remove the special key characters from the option.
                var option = new ParsedOption { Key = item.Remove(0, key.Length) };

                // value should be the next in list
                int nextIndex = index + 1; 

                // key may contains value i.e. opt=value or opt:value
                if (SpecialCharacters.ValueAssignments.Any(option.Key.Contains))
                {
                    TryGetValueFromKey(option);
                }
                else if (option.HasValue == false)
                {
                    option.Value = nextIndex < args.Length ? args[nextIndex] : null; // find value (may not exist)
                }

                TryParseBooleanSyntax(option);

                yield return option;
            }
        }

        private static void TryGetValueFromKey(ParsedOption option)
        {
            var splitted = option.Key.Split(SpecialCharacters.ValueAssignments, 2, StringSplitOptions.RemoveEmptyEntries);

            option.Key = splitted[0];

            if (splitted.Length > 1)
                option.Value = splitted[1].Trim('"');
        }

        private static void TryParseBooleanSyntax(ParsedOption option)
        {
            if (option.HasValue) return;

            // support boolean names
            bool? boolValue = null;

            if (option.Key.EndsWith("-"))
            {
                boolValue = false;
                option.Key = option.Key.TrimEnd('-');
            }
            else if (option.Key.EndsWith("+"))
            {
                boolValue = true;
                option.Key = option.Key.TrimEnd('+');
            }

            if (boolValue.HasValue)
                option.Value = boolValue.Value.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Gets whether the specified <see cref="System.String"/> is a Option key.
        /// </summary>
        /// <param name="arg">The <see cref="System.String"/> to examine.</param>
        /// <returns><c>true</c> if <paramref name="arg"/> is a Option key; otherwise <c>false</c>.</returns>
        static bool IsAKey(string arg)
        {
            return arg != null && SpecialCharacters.OptionKeys.Any(arg.StartsWith);
        }

        /// <summary>
        /// Extracts the key identifier from the specified <see cref="System.String"/>.
        /// </summary>
        /// <param name="arg">The <see cref="System.String"/> to extract the key identifier from.</param>
        /// <returns>A <see cref="System.String"/> representing the key identifier if found; otherwise <c>null</c>.</returns>
        static string ExtractKey(string arg)
        {
            return arg != null ? SpecialCharacters.OptionKeys.FirstOrDefault(arg.StartsWith) : null;
        }
    }
}