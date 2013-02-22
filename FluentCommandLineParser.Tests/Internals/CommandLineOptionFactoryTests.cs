using Fclp.Internals;
using Fclp.Internals.Parsers;
using Moq;
using NUnit.Framework;

namespace FluentCommandLineParser.Tests.Internals
{
    /// <summary>
    /// Contains unit tests for the <see cref="CommandLineOptionFactory"/> class.
    /// </summary>
    [TestFixture]
    public class CommandLineOptionFactoryTests
    {
        [Test]
        public void Ensure_Can_Be_Constructed()
        {
            new CommandLineOptionFactory();
        }

        [Test]
        public void Ensure_CreateCommandLineOption_Returns_Expected_Object()
        {
            var factory = new CommandLineOptionFactory();

            const string expectedShortName = "my short name";
            const string expectedLongName = "my long name";

            var mockParserFactory = new Mock<ICommandLineOptionParserFactory>();
            mockParserFactory.Setup(x => x.CreateParser<string>()).Returns(Mock.Of<ICommandLineOptionParser<string>>);
            factory.ParserFactory = mockParserFactory.Object;

            var actual = factory.CreateOption<string>(expectedShortName, expectedLongName);

            Assert.IsInstanceOf<CommandLineOption<string>>(actual, "Factory returned unexpected object");
            Assert.AreEqual(expectedShortName, actual.ShortName, "Factory returned Option with unexpected ShortName");
            Assert.AreEqual(expectedShortName, actual.ShortName, "Factory returned Option with unexpected LongName");
        }
    }
}
