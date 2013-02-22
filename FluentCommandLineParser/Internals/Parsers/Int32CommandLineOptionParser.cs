using System.Globalization;

namespace Fclp.Internals.Parsers
{
    /// <summary>
    /// Parser used to convert to <see cref="System.Int32"/>.
    /// </summary>
    public class Int32CommandLineOptionParser : ICommandLineOptionParser<int>
    {
        /// <summary>
        /// Converts the string representation of a number in a specified culture-specific format to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Parse(string value)
        {
            return int.Parse(value, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.String"/> can be parsed by this <see cref="ICommandLineOptionParser{T}"/>.
        /// </summary>
        /// <param name="value">The <see cref="System.String"/> to check.</param>
        /// <returns><c>true</c> if the specified <see cref="System.String"/> can be parsed by this <see cref="ICommandLineOptionParser{T}"/>; otherwise <c>false</c>.</returns>
        public bool CanParse(string value)
        {
            int result;
            return int.TryParse(value, out result);
        }
    }
}