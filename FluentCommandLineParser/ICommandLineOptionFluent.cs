using System;

namespace Fclp
{
    /// <summary>
    /// Provides the fluent interface for a <see cref="ICommandLineOptionFluent{T}"/> object.
    /// </summary>
    public interface ICommandLineOptionFluent<T>
    {
        /// <summary>
        /// Adds the specified description to the <see cref="ICommandLineOptionFluent{T}"/>.
        /// </summary>
        /// <param name="description">The <see cref="System.String"/> representing the description to use. This should be localised text.</param>
        /// <returns>A <see cref="ICommandLineOptionFluent{T}"/>.</returns>
        ICommandLineOptionFluent<T> WithDescription(string description);

        /// <summary>
        /// Declares that this <see cref="ICommandLineOptionFluent{T}"/> is required.
        /// </summary>
        /// <returns>A <see cref="ICommandLineOptionFluent{T}"/>.</returns>
        ICommandLineOptionFluent<T> Required();

        /// <summary>
        /// Specifies the method to invoke when the <see cref="ICommandLineOptionFluent{T}"/>. 
        /// is parsed. If a callback is not required either do not call it, or specify <c>null</c>.
        /// </summary>
        /// <param name="callback">The return callback to execute with the parsed value of the Option.</param>
        /// <returns>A <see cref="ICommandLineOptionFluent{T}"/>.</returns>
        ICommandLineOptionFluent<T> Callback(Action<T> callback);

        /// <summary>
        /// Specifies the default value to use if no value is found whilst parsing this <see cref="ICommandLineOptionFluent{T}"/>.
        /// </summary>
        /// <param name="value">The value to use.</param>
        /// <returns>A <see cref="ICommandLineOptionFluent{T}"/>.</returns>
        ICommandLineOptionFluent<T> SetDefault(T value);
    }
}
