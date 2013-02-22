using Fclp.Internals.Parsers;

namespace Fclp.Internals
{
    /// <summary>
    /// Represents a factory capable of creating <see cref="ICommandLineOptionParser{T}"/>.
    /// </summary>
    public interface ICommandLineOptionParserFactory
    {
        /// <summary>
        /// Creates a <see cref="ICommandLineOptionParser{T}"/> to handle the specified type.
        /// </summary>
        /// <typeparam name="T">The type of parser to create.</typeparam>
        /// <returns>A <see cref="ICommandLineOptionParser{T}"/> suitable for the specified type.</returns>
        /// <exception cref="UnsupportedTypeException">If the specified type is not supported by this factory.</exception>
        ICommandLineOptionParser<T> CreateParser<T>();
    }
}