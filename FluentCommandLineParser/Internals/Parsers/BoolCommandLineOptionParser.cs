using Fclp.Internals.Extensions;

namespace Fclp.Internals.Parsers
{
    /// <summary>
    /// Parser used to convert to <see cref="System.Boolean"/>.
    /// </summary>
    /// <remarks>For <see cref="System.Boolean"/> types the value is optional. If no value is provided for the Option then <c>true</c> is returned.</remarks>
    public class BoolCommandLineOptionParser : ICommandLineOptionParser<bool>
    {
        /// <summary>
        /// Parses the specified <see cref="System.String"/> into a <see cref="System.Boolean"/>.
        /// </summary>
        /// <param name="value">The value to parse, or <c>null</c> if none available.</param>
        /// <returns>
        /// A <see cref="System.Boolean"/> representing the parsed value.
        /// The value is optional. If no value is provided then <c>true</c> is returned.
        /// </returns>
        public bool Parse(string value)
        {
            return value.IsNullOrWhiteSpace() || bool.Parse(value);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.String"/> can be parsed by this <see cref="ICommandLineOptionParser{T}"/>.
        /// </summary>
        /// <param name="value">The <see cref="System.String"/> to check.</param>
        /// <returns><c>true</c> if the specified <see cref="System.String"/> can be parsed by this <see cref="ICommandLineOptionParser{T}"/>; otherwise <c>false</c>.</returns>
        public bool CanParse(string value)
        {
            bool result;
            return value.IsNullOrWhiteSpace() || bool.TryParse(value, out result);
        }
    }
}