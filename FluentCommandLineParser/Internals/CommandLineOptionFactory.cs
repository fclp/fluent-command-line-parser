using System;

namespace Fclp.Internals
{
    /// <summary>
    /// Factory used to create command line Options
    /// </summary>
    public class CommandLineOptionFactory : ICommandLineOptionFactory
    {

        ICommandLineOptionParserFactory _parserFactory;

        /// <summary>
        /// Gets or sets the <see cref="ICommandLineOptionParserFactory"/> to use.
        /// </summary>
        /// <remarks>If <c>null</c> a new instance of the <see cref="ParserFactory"/> will be returned.</remarks>
        public ICommandLineOptionParserFactory ParserFactory
        {
            get { return _parserFactory ?? (_parserFactory = new CommandLineOptionParserFactory()); }
            set { _parserFactory = value; }
        }

        /// <summary>
        /// Creates a new <see cref="ICommandLineOptionFluent{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="ICommandLineOptionResult{T}"/> to create.</typeparam>
        /// <param name="shortName">The short name for this Option. This must not be <c>null</c>, <c>empty</c> or contain only <c>whitespace</c>.</param>
        /// <param name="longName">The long name for this Option or <c>null</c> if not required.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="shortName"/> is <c>null</c>, <c>empty</c> or contains only <c>whitespace</c>.</exception>
        /// <returns>A <see cref="ICommandLineOptionResult{T}"/>.</returns>
        public ICommandLineOptionResult<T> CreateOption<T>(string shortName, string longName)
        {
            return new CommandLineOption<T>(shortName, longName, this.ParserFactory.CreateParser<T>());
        }
    }
}
