using System.Linq;
using Fclp.Internals.Extensions;

namespace Fclp.Tests.Extensions
{
    public static class StringExtensions
    {
        public static string[] AsCmdArgs(this string args)
        {
            args = ReplaceWithDoubleQuotes(args);
            return args.SplitOnWhitespace().ToArray();
        }

        private static string ReplaceWithDoubleQuotes(string args)
        {
            if (args == null) return null;
            return args.Replace('\'', '"');
        }
    }
}
