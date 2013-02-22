namespace Fclp.Internals
{
    /// <summary>
    /// Contains special characters used throughout the parser.
    /// </summary>
    public static class SpecialCharacters
    {
        /// <summary>
        /// Characters used for value assignment.
        /// </summary>
        public static readonly char[] ValueAssignments = new[] { '=', ':' };

        /// <summary>
        /// Assign a name to the whitespace character.
        /// </summary>
        public const char Whitespace = ' ';

        /// <summary>
        /// Characters that define the start of an option.
        /// </summary>
        public static readonly string[] OptionKeys = new[] { "/", "--", "-" };
    }
}
