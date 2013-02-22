using System;
using System.Globalization;

namespace Fclp.Internals.Parsers
{
    /// <summary>
    /// Parser used to convert to <see cref="System.DateTime"/>.
    /// </summary>
    public class DateTimeCommandLineOptionParser : ICommandLineOptionParser<DateTime>
    {
        /// <summary>
        /// Parses the specified <see cref="System.String"/> into a <see cref="System.DateTime"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DateTime Parse(string value)
        {
            return DateTime.Parse(value, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.String"/> can be parsed by this <see cref="ICommandLineOptionParser{T}"/>.
        /// </summary>
        /// <param name="value">The <see cref="System.String"/> to check.</param>
        /// <returns><c>true</c> if the specified <see cref="System.String"/> can be parsed by this <see cref="ICommandLineOptionParser{T}"/>; otherwise <c>false</c>.</returns>
        public bool CanParse(string value)
        {
            DateTime dtOut;
            return DateTime.TryParse(value, out dtOut);
        }
    }
}