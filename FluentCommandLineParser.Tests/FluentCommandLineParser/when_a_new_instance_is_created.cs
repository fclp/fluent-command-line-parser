using Fclp.Internals;
using Fclp.Tests.FluentCommandLineParser.TestContext;
using Machine.Specifications;

namespace Fclp.Tests
{
    namespace FluentCommandLineParser
    {
        public class when_a_new_instance_is_created : FluentCommandLineParserTestContext
        {
            It should_create_a_default_parser_engine = () => sut.ParserEngine.ShouldBeOfType(typeof(Fclp.Internals.CommandLineParserEngine));
            It should_create_a_default_option_factory = () => sut.OptionFactory.ShouldBeOfType(typeof(CommandLineOptionFactory));
            It should_set_IsCaseSensitive_to_default_value_of_false = () => sut.StringComparison.ShouldEqual(System.StringComparison.CurrentCultureIgnoreCase);
            It should_have_setup_no_options_internally = () => sut.Options.ShouldBeEmpty();
            It should_have_a_default_option_formatter = () => sut.DefaultOptionFormatter.ShouldBeOfType(typeof(CommandLineOptionFormatter));
        }
    }
}
