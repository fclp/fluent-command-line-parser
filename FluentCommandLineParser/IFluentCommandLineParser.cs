using System;

namespace Fclp
{
    /// <summary>
    /// Represents a command line parser which provides methods and properties 
    /// to easily and fluently parse command line arguments. 
    /// </summary>
    public interface IFluentCommandLineParser
    {
        /// <summary>
        /// Setup a new <see cref="ICommandLineOptionFluent{T}"/> using the specified short and long Option name.
        /// </summary>
        /// <param name="shortOption">The short name for the Option. This must not be <c>null</c>, <c>empty</c> or only <c>whitespace</c>.</param>
        /// <param name="longOption">The long name for the Option or <c>null</c> if not required.</param>
        /// <returns></returns>
        /// <exception cref="OptionAlreadyExistsException">
        /// A Option with the same <paramref name="shortOption"/> name or <paramref name="longOption"/> name
        /// already exists in the <see cref="IFluentCommandLineParser"/>.
        /// </exception>
        ICommandLineOptionFluent<T> Setup<T>(string shortOption, string longOption);

        /// <summary>
        /// Setup a new <see cref="ICommandLineOptionFluent{T}"/> using the specified short Option name.
        /// </summary>
        /// <param name="shortOption">The short name for the Option. This must not be <c>null</c>, <c>empty</c> or only <c>whitespace</c>.</param>
        /// <returns></returns>
        /// <exception cref="OptionAlreadyExistsException">
        /// A Option with the same <paramref name="shortOption"/> name 
        /// already exists in the <see cref="IFluentCommandLineParser"/>.
        /// </exception>
        ICommandLineOptionFluent<T> Setup<T>(string shortOption);

        /// <summary>
        /// Parses the specified <see><cref>T:System.String[]</cref></see> using the setup Options.
        /// </summary>
        /// <param name="args">The <see><cref>T:System.String[]</cref></see> to parse.</param>
        /// <returns>An <see cref="ICommandLineParserResult"/> representing the results of the parse operation.</returns>
        ICommandLineParserResult Parse(string[] args);

        /// <summary>
        /// Constructs the show usage text from the existing setup Options using the default formatter.
        /// </summary>
        /// <returns>A <see cref="System.String"/> describing all the Options.</returns>
        string CreateShowUsageText();

        /// <summary>
        /// Constructs the show usage text from the existing setup Options using the specified <see cref="ICommandLineOptionFormatter"/>.
        /// </summary>
        /// <param name="formatter">The <see cref="ICommandLineOptionFormatter"/> to use to format the usage text. This must not be <c>null</c>.</param>
        /// <returns>A <see cref="System.String"/> describing all the Options.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="formatter"/> is <c>null</c>.</exception>
        string CreateShowUsageText(ICommandLineOptionFormatter formatter);

        ///// <summary>
        ///// Gets or sets whether the parser is case-sentive.
        ///// </summary>
        //bool IsCaseSensitive { get; set; }
    }
}
