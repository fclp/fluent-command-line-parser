using Fclp.Tests.FluentCommandLineParser.TestContext;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Fclp.Tests.FluentCommandLineParser
{
    namespace when_executing_parse_operation
    {
        public class with_a_parser_engine_that_is_null : FluentCommandLineParserTestContext
        {
            Because of = () => sut.ParserEngine = null;

            It should_be_unable_to_assign_to_null = () => sut.ParserEngine.ShouldNotBeNull();
            It should_use_the_default_one_instead = () => sut.ParserEngine.ShouldBeOfType(typeof(Fclp.Internals.CommandLineParserEngine));
        }
    }
}