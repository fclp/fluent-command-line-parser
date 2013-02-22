using System.Collections.Generic;
using System.Linq;

namespace Fclp.Internals
{
    /// <summary>
    /// Contains all information about the result of a parse operation.
    /// </summary>
    public class CommandLineParserResult : ICommandLineParserResult
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="CommandLineParserResult"/> class.
        /// </summary>
        public CommandLineParserResult()
        {
            this.Errors = new List<ICommandLineParserError>();
            this.AdditionalOptionsFound = new List<KeyValuePair<string,string>>();
            this.UnMatchedOptions = new List<ICommandLineOption>();
        }

        /// <summary>
        /// Gets whether the parse operation encountered any errors.
        /// </summary>
        public bool HasErrors
        {
            get { return this.Errors.Any(); }
        }

        /// <summary>
        /// 
        /// </summary>
        internal IList<ICommandLineParserError> Errors { get; set; }

        /// <summary>
        /// Gets the errors which occurred during the parse operation.
        /// </summary>
        IEnumerable<ICommandLineParserError> ICommandLineParserResult.Errors
        {
            get { return this.Errors; }
        }

        /// <summary>
        /// Contains a list of options that were specified in the args but not setup and therefore were not expected.
        /// </summary>
        IEnumerable<KeyValuePair<string, string>> ICommandLineParserResult.AdditionalOptionsFound
        {
            get { return this.AdditionalOptionsFound; }
        }

        /// <summary>
        /// Contains a list of options that were specified in the args but not setup and therefore were not expected.
        /// </summary>
        public IList<KeyValuePair<string, string>> AdditionalOptionsFound { get; set; }

        /// <summary>
        /// Contains all the setup options that were not matched during the parse operation.
        /// </summary>
        IEnumerable<ICommandLineOption> ICommandLineParserResult.UnMatchedOptions
        {
            get { return this.UnMatchedOptions; }
        }

        /// <summary>
        /// Contains all the setup options that were not matched during the parse operation.
        /// </summary>
        public IList<ICommandLineOption> UnMatchedOptions { get; set; }
    }
}