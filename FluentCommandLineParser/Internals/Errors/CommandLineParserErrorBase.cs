using System;

namespace Fclp.Internals.Errors
{
    /// <summary>
    /// Contains error information regarding a failed parsing of a Option.
    /// </summary>
    public abstract class CommandLineParserErrorBase : ICommandLineParserError
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="CommandLineParserErrorBase"/> class.
        /// </summary>
        /// <param name="cmdOption">The <see cref="ICommandLineOption"/> this error relates too. This must not be <c>null</c>.</param>
        /// <param name="message">The message explaining the error.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="cmdOption"/> is <c>null</c>.</exception>
        protected CommandLineParserErrorBase(ICommandLineOption cmdOption, string message)
        {
            if (cmdOption == null) throw new ArgumentNullException("cmdOption");
            this.Option = cmdOption;
            this.Message = message;
        }

        /// <summary>
        /// Gets the <see cref="ICommandLineOption"/> this error belongs too.
        /// </summary>
        public virtual ICommandLineOption Option { get; set; }

        /// <summary>
        /// Gets the message describing the error.
        /// </summary>        
        public virtual string Message { get; set; }
    }
}