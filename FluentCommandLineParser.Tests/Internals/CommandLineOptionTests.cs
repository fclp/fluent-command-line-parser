using System;
using Fclp;
using Fclp.Internals;
using Fclp.Internals.Parsers;
using Moq;
using NUnit.Framework;

namespace FluentCommandLineParser.Tests.Internals
{
    /// <summary>
    /// Contains unit tests for the <see cref="CommandLineOption{T}"/> class.
    /// </summary>
    [TestFixture]
    class CommandLineOptionTests
    {
        #region Constructor Tests

        [Test]
        public void Ensure_Can_Be_Constructed()
        {
            const string expectedShortName = "My short name";
            const string expectedLongName = "My long name";
            var mockParser = Mock.Of<ICommandLineOptionParser<object>>();

            var cmdOption = new CommandLineOption<object>(expectedShortName, expectedLongName, mockParser);

            Assert.AreEqual(expectedShortName, cmdOption.ShortName, "Specified ShortName was not as expected");
            Assert.AreEqual(expectedLongName, cmdOption.LongName, "Specified LongName was not as expected");
        }

        [Test]
        public void Ensure_Can_Be_Constructed_With_Null_LongName()
        {
            const string expectedShortName = "My short name";
            const string expectedLongName = null;
            var mockParser = Mock.Of<ICommandLineOptionParser<object>>();

            var cmdOption = new CommandLineOption<object>(expectedShortName, expectedLongName, mockParser);

            Assert.IsNull(cmdOption.LongName, "Could not instantiate with null LongName");
        }

        [Test]
        public void Ensure_Can_Be_Constructed_With_Empty_LongName()
        {
            const string expectedShortName = "My short name";
            const string expectedLongName = "";
            var mockParser = Mock.Of<ICommandLineOptionParser<object>>();

            var cmdOption = new CommandLineOption<object>(expectedShortName, expectedLongName, mockParser);

            Assert.AreEqual(expectedLongName, cmdOption.LongName, "Could not instantiate with empty LongName");
        }

        [Test]
        public void Ensure_Can_Be_Constructed_With_Whitespace_Only_LongName()
        {
            const string expectedShortName = "My short name";
            const string expectedLongName = " ";
            var mockParser = Mock.Of<ICommandLineOptionParser<object>>();

            var cmdOption = new CommandLineOption<object>(expectedShortName, expectedLongName, mockParser);

            Assert.AreEqual(expectedLongName, cmdOption.LongName, "Could not instantiate with whitespace only LongName");
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Ensure_Cannot_Be_Constructed_With_Null_ShortName()
        {
            const string expectedShortName = null;
            const string expectedLongName = "My long name";

            var mockParser = Mock.Of<ICommandLineOptionParser<object>>();

            new CommandLineOption<object>(expectedShortName, expectedLongName, mockParser);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Ensure_Cannot_Be_Constructed_With_Empty_ShortName()
        {
            const string expectedShortName = "";
            const string expectedLongName = "My long name";
            var mockParser = Mock.Of<ICommandLineOptionParser<object>>();

            new CommandLineOption<object>(expectedShortName, expectedLongName, mockParser);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Ensure_Cannot_Be_Constructed_With_Whitespace_Only_ShortName()
        {
            const string expectedShortName = " ";
            const string expectedLongName = "My long name";
            var mockParser = Mock.Of<ICommandLineOptionParser<object>>();

            new CommandLineOption<object>(expectedShortName, expectedLongName, mockParser);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ensure_Cannot_Be_Constructed_With_Null_Parser()
        {
            const string expectedShortName = "My short name";
            const string expectedLongName = "My long name";

            new CommandLineOption<object>(expectedShortName, expectedLongName, null);
        }

        #endregion Constructor Tests

        #region HasLongName Tests

        [Test]
        public void Ensure_Returns_False_If_Null_LongName_Provided()
        {
            ICommandLineOption cmdOption = new CommandLineOption<string>("s", null, Mock.Of<ICommandLineOptionParser<string>>());
            Assert.IsFalse(cmdOption.HasLongName);
        }

        [Test]
        public void Ensure_Returns_False_If_WhiteSpace_LongName_Provided()
        {
            ICommandLineOption cmdOption = new CommandLineOption<string>("s", " ", Mock.Of<ICommandLineOptionParser<string>>());
            Assert.IsFalse(cmdOption.HasLongName);
        }

        [Test]
        public void Ensure_Returns_False_If_Empty_LongName_Provided()
        {
            ICommandLineOption cmdOption = new CommandLineOption<string>("s", string.Empty, Mock.Of<ICommandLineOptionParser<string>>());
            Assert.IsFalse(cmdOption.HasLongName);
        }

        [Test]
        public void Ensure_Returns_True_If_LongName_Provided()
        {
            ICommandLineOption cmdOption = new CommandLineOption<string>("s", "long name", Mock.Of<ICommandLineOptionParser<string>>());
            Assert.IsTrue(cmdOption.HasLongName);
        }

        #endregion HasLongName Tests

        #region Bind Tests

        [Test]
        [ExpectedException(typeof(OptionSyntaxException))]
        public void Ensure_That_If_Value_Is_Null_Cannot_Be_Parsed_And_No_Default_Set_Then_optionSyntaxException_Is_Thrown()
        {
            const string value = null;
            var mockParser = new Mock<ICommandLineOptionParser<string>>();
            mockParser.Setup(x => x.CanParse(value)).Returns(false);

            var target = new CommandLineOption<string>("s", "long name", mockParser.Object);

            target.Bind(value);
        }


        [Test]
        [ExpectedException(typeof(OptionSyntaxException))]
        public void Ensure_That_If_Value_Is_Empty_Cannot_Be_Parsed_And_No_Default_Set_Then_optionSyntaxException_Is_Thrown()
        {
            const string value = "";
            var mockParser = new Mock<ICommandLineOptionParser<string>>();
            mockParser.Setup(x => x.CanParse(value)).Returns(false);

            var target = new CommandLineOption<string>("s", "long name", mockParser.Object);

            target.Bind(value);
        }


        [Test]
        [ExpectedException(typeof(OptionSyntaxException))]
        public void Ensure_That_If_Value_Is_Whitespace_Cannot_Be_Parsed_And_No_Default_Set_Then_optionSyntaxException_Is_Thrown()
        {
            const string value = " ";
            var mockParser = new Mock<ICommandLineOptionParser<string>>();
            mockParser.Setup(x => x.CanParse(value)).Returns(false);

            var target = new CommandLineOption<string>("s", "long name", mockParser.Object);

            target.Bind(value);
        }
        #endregion
    }
}

