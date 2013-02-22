using Fclp.Internals;
using Fclp.Tests.FluentCommandLineParser.TestContext;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Fclp.Tests.FluentCommandLineParser
{
    namespace when_using_an_option_factory
    {
        public class that_has_been_set_to_null : FluentCommandLineParserTestContext
        {
            Because of = () => sut.OptionFactory = null;

            It should_be_unable_to_assign_to_null = () => sut.OptionFactory.ShouldNotBeNull();
            It should_use_the_default_one_instead = () => sut.OptionFactory.ShouldBeOfType(typeof(CommandLineOptionFactory));
        }
    }
}