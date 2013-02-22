using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Fclp.Tests.FluentCommandLineParser
{
    namespace when_creating_show_usage_text
    {
        class with_no_formatter : TestContext<Fclp.FluentCommandLineParser>
        {
            static Mock<ICommandLineOptionFormatter> mockOptionFormatter;
            static ICommandLineOptionFormatter customOptionFormatter { get { return mockOptionFormatter.Object; } }
            static string result;
            static string expectedResult;

            Establish context = () =>
                                    {
                                        sut = new Fclp.FluentCommandLineParser();

                                        expectedResult = "expected results";

                                        mockOptionFormatter = new Mock<ICommandLineOptionFormatter>();

                                        mockOptionFormatter
                                            .Setup(x => x.Format(sut.Options))
                                            .Returns(expectedResult);

                                        sut.DefaultOptionFormatter = customOptionFormatter;
                                    };

            Because of = () => CatchAnyError(() => result = sut.CreateShowUsageText());

            It should_use_the_default_one = () => mockOptionFormatter.Verify(x => x.Format(sut.Options));
            It should_return_the_results_obtained_from_the_default_one = () => expectedResult.ShouldEqual(result);
        }
    }
}