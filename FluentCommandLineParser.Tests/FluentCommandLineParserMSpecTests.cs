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