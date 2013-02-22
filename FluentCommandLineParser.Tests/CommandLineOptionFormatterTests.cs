using System;
using System.Globalization;
using System.Text;
using Fclp.Internals;
using Moq;
using NUnit.Framework;

namespace FluentCommandLineParser.Tests
{
    /// <summary>
    /// Contains unit test for the <see cref="CommandLineOptionFormatter"/> test.
    /// </summary>
    [TestFixture]
    public class CommandLineOptionFormatterTests
    {
        #region Constructors

        [Test]
        public void Ensure_Can_Be_Constructed()
        {
            new CommandLineOptionFormatter();
        }

        #endregion Constructors

        #region Properties

        [Test]
        public void Ensure_ValueText_Can_Be_Set()
        {
            var formatter = new CommandLineOptionFormatter();

            const string expected = "my value text";

            formatter.ValueText = expected;

            Assert.AreEqual(expected, formatter.ValueText);            
        }

        [Test]
        public void Ensure_DescriptionText_Can_Be_Set()
        {
            var formatter = new CommandLineOptionFormatter();

            const string expected = "my description text";

            formatter.DescriptionText = expected;

            Assert.AreEqual(expected, formatter.DescriptionText);
        }

        [Test]
        public void Ensure_NoOptionsText_Can_Be_Set()
        {
            var formatter = new CommandLineOptionFormatter();

            const string expected = "no Options setup text";

            formatter.NoOptionsText = expected;

            Assert.AreEqual(expected, formatter.NoOptionsText);
        }

        #endregion Properties

        #region Format

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ensure_Cannot_Specify_Null_options_Param()
        {
            var formatter = new CommandLineOptionFormatter();

            formatter.Format(null);
        }

        [Test]
        public void Ensure_Format_Returns_Expected_String()
        {
            var formatter = new CommandLineOptionFormatter();

            // expected:
            //  Value           Description
            //  Short1          Description1
            //  Short2:Long2    Description2 

            var mockOption1 = CreateMockOption("Short1", null, "Description1");
            var mockOption2 = CreateMockOption("Short2", "Long2", "Description2");

            var expectedSb = new StringBuilder();
            expectedSb.AppendFormat(CultureInfo.CurrentUICulture, CommandLineOptionFormatter.TextFormat, "Value", "Description");
            expectedSb.AppendFormat(CultureInfo.CurrentUICulture, CommandLineOptionFormatter.TextFormat, mockOption1.ShortName, mockOption1.Description);
            expectedSb.AppendFormat(CultureInfo.CurrentUICulture, CommandLineOptionFormatter.TextFormat, mockOption2.ShortName + ":" + mockOption2.LongName, mockOption2.Description);

            var expected = expectedSb.ToString();
            var actual = formatter.Format(new[] { mockOption1, mockOption2 });

            Assert.AreEqual(expected, actual, "Formatter returned unexpected string");
        }
        
        [Test]
        public void Ensure_NoOptionsText_Returned_If_No_options_Have_Been_Setup()
        {
            var formatter = new CommandLineOptionFormatter();

            var actual = formatter.Format(new ICommandLineOption[0]);

            Assert.AreEqual(formatter.NoOptionsText, actual);
        }

        #endregion Format

        #region Helper Methods

        /// <summary>
        /// Helper method to return a mocked <see cref="ICommandLineOption"/>.
        /// </summary>
        static Fclp.Internals.ICommandLineOption CreateMockOption(string shortName, string longName, string description)
        {
            var mockOption = new Mock<ICommandLineOption>();
            mockOption.SetupGet(x => x.ShortName).Returns(shortName);
            mockOption.SetupGet(x => x.LongName).Returns(longName);
            mockOption.SetupGet(x => x.Description).Returns(description);
            return mockOption.Object;
        }

        #endregion Helper Methods
    }
}

