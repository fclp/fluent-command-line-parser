namespace Fclp.Internals
{
    /// <summary>
    /// Used to encapsulate both command Option interfaces which are returned from the factory.
    /// </summary>
    /// <typeparam name="T">The type of Option.</typeparam>
    public interface ICommandLineOptionResult<T> : ICommandLineOption, ICommandLineOptionFluent<T>
    {

    }
}
