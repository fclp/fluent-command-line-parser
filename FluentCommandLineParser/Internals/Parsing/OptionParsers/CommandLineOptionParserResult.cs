using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fclp.Internals.Parsing.OptionParsers
{
    /// <summary>
    /// result of an option parsing
    /// </summary>
    public class CommandLineOptionParserResult<T>
    {
        /// <summary>
        /// direct result of the parsing
        /// </summary>
        public T ParsedValue { get; set; }

        /// <summary>
        /// values that were not used in the parsing
        /// </summary>
        public IEnumerable<string> UnMatchedValues { get; set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="CommandLineOptionParserResult{T}"/> class.
        /// with parsed values and unmatchedValues
        /// </summary>
        /// <param name="parsedValue">direct result of the parsing</param>
        /// <param name="unMatchedValues">values that were not used in the parsing</param>
        public CommandLineOptionParserResult(T parsedValue, IEnumerable<string> unMatchedValues)
        {
            this.ParsedValue = parsedValue;
            this.UnMatchedValues = unMatchedValues;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="CommandLineOptionParserResult{T}"/> class.
        /// </summary>
        /// <param name="parsedValue">direct result of the parsing</param>
        public CommandLineOptionParserResult(T parsedValue)
        {
            this.ParsedValue = parsedValue;
            this.UnMatchedValues = Enumerable.Empty<string>();
        }

    }
}
