using Fclp.Tests.Internals;
using Machine.Specifications;

namespace Fclp.Tests
{
	class FluentCommandLineParserMSpecTests
	{
		[Subject(typeof(Fclp.FluentCommandLineParser))]
		abstract class FluentCommandLineParserTestContext : TestContextBase<Fclp.FluentCommandLineParser>
		{
			Establish context = () => CreateSut();
		}

		sealed class Constructor
		{
			class when_initialised : FluentCommandLineParserTestContext
			{
				It should_enable_case_sensitive = () =>
					sut.IsCaseSensitive.ShouldBeTrue();

				It should_have_an_error_formatter = () => 
					sut.ErrorFormatter.ShouldNotBeNull();

				It should_have_a_help_option = () => 
					sut.HelpOption.ShouldNotBeNull();

				It should_have_a_option_factory = () => 
					sut.OptionFactory.ShouldNotBeNull();

				It should_have_a_option_formatter = () => 
					sut.OptionFormatter.ShouldNotBeNull();

				It should_have_a_option_validator = () => 
					sut.OptionValidator.ShouldNotBeNull();

				It should_not_have_any_options_setup = () => 
					sut.Options.ShouldBeEmpty();

				It should_have_an_parser_engine = () => 
					sut.ParserEngine.ShouldNotBeNull();
			}
		}

		sealed class IsCaseSensitive
		{
			abstract class IsCaseSensitiveTestContext : FluentCommandLineParserTestContext { }

			class when_enabled : IsCaseSensitiveTestContext
			{
				Because of = () => sut.IsCaseSensitive = true;

				It should_return_enabled = () =>
					sut.IsCaseSensitive.ShouldBeTrue();

				It should_set_the_comparison_type_to_case_sensitive = () =>
					sut.StringComparison.ShouldEqual(Fclp.FluentCommandLineParser.CaseSensitiveComparison);
			}

			class when_disabled : IsCaseSensitiveTestContext
			{
				Because of = () => sut.IsCaseSensitive = false;

				It should_return_enabled = () =>
					sut.IsCaseSensitive.ShouldBeFalse();

				It should_set_the_comparison_type_to_ignore_case = () =>
					sut.StringComparison.ShouldEqual(Fclp.FluentCommandLineParser.IgnoreCaseComparison);
			}
		}
	}
}