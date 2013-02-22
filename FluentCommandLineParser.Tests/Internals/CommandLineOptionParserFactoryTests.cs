using System;
using Fclp;
using Fclp.Internals;
using Fclp.Internals.Parsers;
using Moq;
using NUnit.Framework;

namespace FluentCommandLineParser.Tests.Internals
{
    [TestFixture]
    public class CommandLineOptionParserFactoryTests
    {
        [Test]
        public void Enure_Can_Be_Constructed()
        {
            new CommandLineOptionParserFactory();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ensure_Cannot_Add_Null_Parser()
        {
            var factory = new CommandLineOptionParserFactory();
            factory.AddOrReplace<string>(null);
        }

        [Test]
        public void Ensure_Can_Add_Custom_Parser()
        {
            var factory = new CommandLineOptionParserFactory();

            var mockParser = new Mock<ICommandLineOptionParser<CommandLineOptionParserFactoryTests>>();

            factory.AddOrReplace(mockParser.Object);

            var actual = factory.CreateParser<CommandLineOptionParserFactoryTests>();

            Assert.AreSame(mockParser.Object, actual);
        }

        [Test]
        public void Ensure_Can_Replace_Existing_Parser()
        {
            var factory = new CommandLineOptionParserFactory();

            factory.Parsers.Clear();

            factory.Parsers.Add(typeof(CommandLineOptionParserFactoryTests), Mock.Of<ICommandLineOptionParser<CommandLineOptionParserFactoryTests>>());

            var mockParser = new Mock<ICommandLineOptionParser<CommandLineOptionParserFactoryTests>>();

            factory.AddOrReplace(mockParser.Object);

            var actual = factory.CreateParser<CommandLineOptionParserFactoryTests>();

            Assert.AreSame(mockParser.Object, actual);
        }

        [Test]
        [ExpectedException(typeof(UnsupportedTypeException))]
        public void Ensure_UnsupportedTypeException_Thrown_If_Factory_Is_Unable_To_Create_Requested_Type()
        {
            var factory = new CommandLineOptionParserFactory();

            factory.CreateParser<CommandLineOptionParserFactoryTests>();
        }

        [Test]
        public void Ensure_Factory_Supports_Out_Of_The_Box_Parsers()
        {
            var factory = new CommandLineOptionParserFactory();

            var stringParser = factory.CreateParser<string>();
            var int32Parser = factory.CreateParser<int>();
            var doubleParser = factory.CreateParser<double>();
            var dtParser = factory.CreateParser<DateTime>();
            var boolParser = factory.CreateParser<bool>();

            Assert.IsInstanceOf<StringCommandLineOptionParser>(stringParser);
            Assert.IsInstanceOf<Int32CommandLineOptionParser>(int32Parser);
            Assert.IsInstanceOf<DoubleCommandLineOptionParser>(doubleParser);
            Assert.IsInstanceOf<DateTimeCommandLineOptionParser>(dtParser);
            Assert.IsInstanceOf<BoolCommandLineOptionParser>(boolParser);
        }
    }
}
