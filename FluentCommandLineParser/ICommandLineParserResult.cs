using System.Collections.Generic;
using Fclp.Internals;

namespace Fclp
{
    /// <summary>
    /// Represents all the information gained from the result of a parse operation.
    /// </summary>
    public interface ICommandLineParserResult
    {
        /// <summary>
        /// Gets whether the parse operation encountered any errors.
        /// </summary>
        bool HasErrors { get; }

        /// <summary>
        /// Gets the errors which occurred during the parse operation.
        /// </summary>
        IEnumerable<ICommandLineParserError> Errors { get; }

        /// <summary>
        /// Contains a list of options that were specified in the args but not setup and therefore were not expected.
        /// </summary>
        IEnumerable<KeyValuePair<string, string>> AdditionalOptionsFound { get; }

        /// <summary>
        /// Contains all the setup options that were not matched during the parse operation.
        /// </summary>
        IEnumerable<ICommandLineOption> UnMatchedOptions { get; }
    }
}
