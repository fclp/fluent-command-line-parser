using Fclp.Internals;

namespace FluentCommandLineParser.Tests
{
    public static class HelperExtensions
    {
        /// <summary>
        /// Returns the specified <see cref="FluentCommandLineParser"/> represented as its interface <see cref="IFluentCommandLineParser"/>
        /// </summary>
        public static Fclp.IFluentCommandLineParser AsInterface(this Fclp.FluentCommandLineParser parser)
        {
            return parser;
        }

        /// <summary>
        /// Returns the specified <see cref="CommandLineParserEngine"/> represented as its interface <see cref="ICommandLineParserEngine"/>
        /// </summary>
        public static ICommandLineParserEngine AsInterface(this CommandLineParserEngine parserEngine)
        {
            return parserEngine;
        }
    }
}
