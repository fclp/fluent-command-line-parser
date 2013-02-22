using Fclp.Internals.Extensions;

namespace Fclp.Internals.Parsers
{
    /// <summary>
    /// Parser used to convert to <see cref="System.String"/>.
    /// </summary>
    public class StringCommandLineOptionParser : ICommandLineOptionParser<string>
    {
        /// <summary>
        /// Parses the specified <see cref="System.String"/> into a <see cref="System.String"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Parse(string value)
        {
            return value;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.String"/> can be parsed by this <see cref="ICommandLineOptionParser{T}"/>.
        /// </summary>
        /// <param name="value">The <see cref="System.String"/> to check.</param>
        /// <returns><c>true</c> if the specified <see cref="System.String"/> can be parsed by this <see cref="ICommandLineOptionParser{T}"/>; otherwise <c>false</c>.</returns>
        public bool CanParse(string value)
        {
            return !value.IsNullOrWhiteSpace();
        }
    }
}