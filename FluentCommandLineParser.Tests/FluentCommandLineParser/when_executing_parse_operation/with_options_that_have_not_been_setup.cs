using System.Collections.Generic;
using Fclp.Tests.FluentCommandLineParser.TestContext;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Fclp.Tests.FluentCommandLineParser
{
    namespace when_executing_parse_operation
    {
        class with_options_that_have_not_been_setup : FluentCommandLineParserTestContext
        {
            static string[] args;
            static ICommandLineParserResult result;
            static IEnumerable<KeyValuePair<string, string>> additionalOptions;

            Establish context = () =>
            {
                additionalOptions = new Dictionary<string, string>
                {
                    {"arg1", "1"},
                    {"arg2", "2"},
                    {"arg3", "3"},
                };

                args = CreateArgsFromKvp(additionalOptions);

                var mockEngine = new Mock<Fclp.Internals.ICommandLineParserEngine>();
                mockEngine.Setup(x => x.Parse(args)).Returns(additionalOptions);
                sut.ParserEngine = mockEngine.Object;
            };

            Because of = () => CatchAnyError(() => result = sut.Parse(args));

            It should_not_throw_any_errors = () => error.ShouldBeNull();
            It should_return_a_result = () => result.ShouldNotBeNull();
            It should_return_them_as_additional = () => result.AdditionalOptionsFound.ShouldContainOnly(additionalOptions);
        }
    }
}
