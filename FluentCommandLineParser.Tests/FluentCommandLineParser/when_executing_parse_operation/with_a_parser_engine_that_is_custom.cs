using System.Collections.Generic;
using Fclp.Internals;
using Fclp.Tests.FluentCommandLineParser.TestContext;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Fclp.Tests.FluentCommandLineParser
{
    namespace when_executing_parse_operation
    {
        public class with_a_parser_engine_that_is_custom : FluentCommandLineParserTestContext
        {
            static string[] args;
            static ICommandLineParserEngine customEngine { get { return mockedEngine.Object; } }
            static Mock<ICommandLineParserEngine> mockedEngine;

            Establish context = () =>
                                    {
                                        sut = new Fclp.FluentCommandLineParser();
                                        mockedEngine = new Mock<ICommandLineParserEngine>();

                                        args = new string[0];

                                        mockedEngine
                                            .Setup(x => x.Parse(args))
                                            .Returns(new List<KeyValuePair<string, string>>())
                                            .Verifiable();
                                    };

            Because of = () =>
            {
                sut.ParserEngine = customEngine;
                sut.Parse(args);
            };

            It should_replace_the_old_engine = () => sut.ParserEngine.ShouldBeTheSameAs(customEngine);
            It should_be_used_to_parse_the_args = () => mockedEngine.Verify(x => x.Parse(args));
        }
    }
}
