using Fclp.Internals;

namespace Fclp
{
    /// <summary>
    /// Represents an error that has occurred whilst parsing a Option.
    /// </summary>
    public interface ICommandLineParserError
    {
        /// <summary>
        /// Gets the <see cref="ICommandLineOption"/> this error belongs too.
        /// </summary>
        ICommandLineOption Option { get; }

        /// <summary>
        /// Gets the message describing the error.
        /// </summary>
        string Message { get; }
    }
}