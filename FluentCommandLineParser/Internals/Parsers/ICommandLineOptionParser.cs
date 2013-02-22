namespace Fclp.Internals.Parsers
{
    /// <summary>
    /// Represents a parser for a Option that can convert a value into the required type.
    /// </summary>
    public interface ICommandLineOptionParser<T>
    {
        /// <summary>
        /// Parses the specified <see cref="System.String"/> into the return type.
        /// </summary>
        /// <param name="value">The <see cref="System.String"/> representing the value to parse. This may be <c>null</c>, <c>empty</c> or contain only <c>whitespace</c>.</param>
        /// <returns>The parsed value.</returns>
        T Parse(string value);

        /// <summary>
        /// Determines whether the specified <see cref="System.String"/> can be parsed by this <see cref="ICommandLineOptionParser{T}"/>.
        /// </summary>
        /// <param name="value">The <see cref="System.String"/> to check.</param>
        /// <returns><c>true</c> if the specified <see cref="System.String"/> can be parsed by this <see cref="ICommandLineOptionParser{T}"/>; otherwise <c>false</c>.</returns>
        bool CanParse(string value);
    }
}
