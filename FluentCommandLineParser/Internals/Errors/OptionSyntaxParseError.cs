using System;

namespace Fclp.Internals.Errors
{
    /// <summary>
    /// Represents a parse error that has occurred because the syntax was in an unexpected format.
    /// </summary>
    public class OptionSyntaxParseError : CommandLineParserErrorBase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="CommandLineParserErrorBase"/> class.
        /// </summary>
        /// <param name="cmdOption">The <see cref="ICommandLineOption"/> this error relates too. This must not be <c>null</c>.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="cmdOption"/> is <c>null</c>.</exception>
        public OptionSyntaxParseError(ICommandLineOption cmdOption) :
            base(cmdOption, "Expected Option was not specified.")
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UnexpectedValueParseError : CommandLineParserErrorBase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="CommandLineParserErrorBase"/> class.
        /// </summary>
        /// <param name="cmdOption">The <see cref="ICommandLineOption"/> this error relates too. This must not be <c>null</c>.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="cmdOption"/> is <c>null</c>.</exception>
        public UnexpectedValueParseError(ICommandLineOption cmdOption) :
            base(cmdOption, "Expected Option was not specified.")
        {
        }        
    }
}
