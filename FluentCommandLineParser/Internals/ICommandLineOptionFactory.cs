using System;

namespace Fclp.Internals
{
    /// <summary>
    /// Represents a factory capable of creating command line Options.
    /// </summary>
    public interface ICommandLineOptionFactory
    {
        /// <summary>
        /// Creates a new <see cref="ICommandLineOptionFluent{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="ICommandLineOptionResult{T}"/> to create.</typeparam>
        /// <param name="shortName">The short name for this Option. This must not be <c>null</c>, <c>empty</c> or contain only <c>whitespace</c>.</param>
        /// <param name="longName">The long name for this Option or <c>null</c> if not required.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="shortName"/> is <c>null</c>, <c>empty</c> or contains only <c>whitespace</c>.</exception>
        /// <returns>A <see cref="ICommandLineOptionResult{T}"/>.</returns>
        ICommandLineOptionResult<T> CreateOption<T>(string shortName, string longName);
    }
}
