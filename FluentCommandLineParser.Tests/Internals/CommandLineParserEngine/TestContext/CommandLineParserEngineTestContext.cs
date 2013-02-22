using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;

namespace Fclp.Tests
{
    namespace CommandLineParserEngine
    {
        [Subject(typeof(Fclp.Internals.CommandLineParserEngine), "CommandLineParserEngine")]
        public abstract class CommandLineParserEngineTestContext : TestContext<Fclp.Internals.CommandLineParserEngine>
        {
            protected static IEnumerable<KeyValuePair<string, string>> results;
            protected static string[] args;

            Establish context = () => sut = new Fclp.Internals.CommandLineParserEngine();

            protected static void RunParserWith(string[] args)
            {
                CatchAnyError(() => results = sut.Parse(args).ToList());
            }
        }
    }
}