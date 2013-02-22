namespace Fclp.Internals
{
    /// <summary>
    /// Represents a setup command line Option
    /// </summary>
    public interface ICommandLineOption
    {
        /// <summary>
        /// Gets whether this <see cref="ICommandLineOption"/> is required.
        /// </summary>
        bool IsRequired { get; }

        /// <summary>
        /// Gets the description set for this <see cref="ICommandLineOption"/>.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Binds the specified <see cref="System.String"/> to this <see cref="ICommandLineOption"/>.
        /// </summary>
        /// <param name="value">The <see cref="System.String"/> to bind.</param>
        void Bind(string value);

        /// <summary>
        /// Binds the default value for this <see cref="ICommandLineOption"/> if available.
        /// </summary>
        void BindDefault();

        /// <summary>
        /// Gets the short name of this <see cref="ICommandLineOption"/>.
        /// </summary>
        string ShortName { get; }

        /// <summary>
        /// Gets the long name of this <see cref="ICommandLineOption"/>.
        /// </summary>
        string LongName { get; }

        /// <summary>
        /// Gets whether this <see cref="ICommandLineOption"/> has a long name.
        /// </summary>
        bool HasLongName { get; }

        /// <summary>
        /// Gets whether this <see cred="ICommandLineOption"/> has a callback setup.
        /// </summary>
        bool HasCallback { get; }

        /// <summary>
        /// Gets whether this <see cref="ICommandLineOption"/> has a default value setup.
        /// </summary>
        bool HasDefault { get; }
    }
}
