namespace Fclp.Internals.Extensions
{
    /// <summary>
    /// Contains some simple extension methods that are useful throughout the library.
    /// </summary>
    public static class UsefulExtension
    {
        /// <summary>
        /// Indicates whether the specified <see cref="System.String"/> is <c>null</c>, <c>empty</c> or contains only <c>whitespace</c>.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>This method mimics the String.IsNullOrWhiteSpace method available in .Net 4 framework.</remarks>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrEmpty(value.Trim());
        }
    }
}
