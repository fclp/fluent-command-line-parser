using System;
using System.Collections.Generic;
using Fclp.Internals;

namespace Fclp
{
    /// <summary>
    /// Represents a formatter used to display command line options to the user.
    /// </summary>
    public interface ICommandLineOptionFormatter
    {
        /// <summary>
        /// Formats the list of <see cref="ICommandLineOption"/> to be displayed to the user.
        /// </summary>
        /// <param name="options">The list of <see cref="ICommandLineOption"/> to format.</param>
        /// <returns>A <see cref="System.String"/> representing the format</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="options"/> is <c>null</c>.</exception>
        string Format(IEnumerable<ICommandLineOption> options);
    }
}
