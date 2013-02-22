using System.Collections.Generic;

namespace Fclp.Internals
{
    /// <summary>
    /// Responsible for parsing command line arguments into simple key and value pairs.
    /// </summary>
    public interface ICommandLineParserEngine
    {
        /// <summary>
        /// Parses the specified <see><cref>T:System.String[]</cref></see> into key value pairs.
        /// </summary>
        /// <param name="args">The <see><cref>T:System.String[]</cref></see> to parse.</param>
        /// <returns>An <see cref="ICommandLineParserResult"/> representing the results of the parse operation.</returns>
        IEnumerable<KeyValuePair<string, string>> Parse(string[] args);
    }
}
