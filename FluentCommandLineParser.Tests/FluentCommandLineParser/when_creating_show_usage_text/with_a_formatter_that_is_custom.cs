using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Fclp.Tests.FluentCommandLineParser
{
    namespace when_creating_show_usage_text
    {
        class with_a_formatter_that_is_custom : TestContext<Fclp.FluentCommandLineParser>
        {
            static Mock<ICommandLineOptionFormatter> mockOptionFormatter;
            static ICommandLineOptionFormatter customOptionFormatter { get { return mockOptionFormatter.Object; } }
            static string result;
            static string expectedResult;

            Establish context = () =>
                                    {
                                        sut = new Fclp.FluentCommandLineParser();

                                        expectedResult = "my expected results";

                                        mockOptionFormatter = new Mock<ICommandLineOptionFormatter>();
                                        mockOptionFormatter
                                            .Setup(x => x.Format(sut.Options))
                                            .Returns(expectedResult)
                                            .Verifiable();
                                    };

            Because of = () => CatchAnyError(() => result = sut.CreateShowUsageText(customOptionFormatter));

            It should_not_throw_an_error = () => error.ShouldBeNull();
            It should_call_it_with_all_the_options = () => mockOptionFormatter.Verify(x => x.Format(sut.Options));
            It should_return_the_results_obtained_from_it = () => expectedResult.ShouldEqual(result);
        }
    }
}