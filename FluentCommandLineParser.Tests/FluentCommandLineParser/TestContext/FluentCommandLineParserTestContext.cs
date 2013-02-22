using Fclp.Internals;
using Machine.Specifications;
using Moq;

namespace Fclp.Tests.FluentCommandLineParser
{
    namespace TestContext
    {
        [Subject(typeof(Fclp.FluentCommandLineParser), "Unit Tests")]
        public abstract class FluentCommandLineParserTestContext : TestContext<Fclp.FluentCommandLineParser>
        {
            private Establish context = () => { sut = new Fclp.FluentCommandLineParser(); };

            protected static void AutoMockAll()
            {
                AutoMockEngineParser();
                AutoMockOptionFactory();
            }

            protected static void AutoMockEngineParser()
            {
                sut.ParserEngine = Mock.Of<ICommandLineParserEngine>();
            }

            protected static void AutoMockOptionFactory()
            {
                var mock = new Mock<ICommandLineOptionFactory>();
                var mockOption = new Mock<ICommandLineOptionResult<TestType>>();

                mock.Setup(x => x.CreateOption<TestType>(Moq.It.IsAny<string>(), Moq.It.IsAny<string>()))
                    .Returns(mockOption.Object)
                    .Callback<string, string>((s, l) =>
                                {
                                    mockOption.SetupGet(x => x.ShortName).Returns(s);
                                    mockOption.SetupGet(x => x.LongName).Returns(l);
                                });


                sut.OptionFactory = mock.Object;
            }
        }
    }
}