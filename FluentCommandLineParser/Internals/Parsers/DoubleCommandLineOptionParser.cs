using System.Globalization;

namespace Fclp.Internals.Parsers
{
    /// <summary>
    /// Parser used to convert to <see cref="System.Double"/>.
    /// </summary>
    public class DoubleCommandLineOptionParser : ICommandLineOptionParser<double>
    {
        /// <summary>
        /// Parses the specified <see cref="System.String"/> into a <see cref="System.Double"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public double Parse(string value)
        {
            return double.Parse(value, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.String"/> can be parsed by this <see cref="ICommandLineOptionParser{T}"/>.
        /// </summary>
        /// <param name="value">The <see cref="System.String"/> to check.</param>
        /// <returns><c>true</c> if the specified <see cref="System.String"/> can be parsed by this <see cref="ICommandLineOptionParser{T}"/>; otherwise <c>false</c>.</returns>
        public bool CanParse(string value)
        {
            double result;
            return double.TryParse(value, out result);
        }
    }
}