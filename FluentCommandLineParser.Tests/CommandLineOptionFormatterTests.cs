#region License
// CommandLineOptionFormatterTests.cs
// Copyright (c) 2013, Simon Williams
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification, are permitted provide
// d that the following conditions are met:
// 
// Redistributions of source code must retain the above copyright notice, this list of conditions and the
// following disclaimer.
// 
// Redistributions in binary form must reproduce the above copyright notice, this list of conditions and
// the following disclaimer in the documentation and/or other materials provided with the distribution.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED 
// WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
// PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
// TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
// POSSIBILITY OF SUCH DAMAGE.
#endregion

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

		[Test]
		public void Ensure_Header_Can_Be_Set()
		{
			var formatter = new CommandLineOptionFormatter();

			const string expected = "my custom header";

			formatter.Header = expected;

			Assert.AreEqual(expected, formatter.Header);   
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
			//  Short1          Description1
			//  Short2:Long2    Description2 

			var mockOptionA = CreateMockOption("a", "aaa", "a-description");
			var mockOptionB = CreateMockOption("b", null, "b-description1");
			var mockOptionC = CreateMockOption(null, "ccc", "c-description");

			var expectedSb = new StringBuilder();
			expectedSb.AppendLine();
			expectedSb.AppendFormat(CultureInfo.CurrentUICulture, CommandLineOptionFormatter.TextFormat, mockOptionA.ShortName + ":" + mockOptionA.LongName, mockOptionA.Description);
			expectedSb.AppendFormat(CultureInfo.CurrentUICulture, CommandLineOptionFormatter.TextFormat, mockOptionB.ShortName, mockOptionB.Description);
			expectedSb.AppendFormat(CultureInfo.CurrentUICulture, CommandLineOptionFormatter.TextFormat, mockOptionC.LongName, mockOptionC.Description);

			var expected = expectedSb.ToString();
			var actual = formatter.Format(new[] { mockOptionB, mockOptionA, mockOptionC });

			Assert.AreEqual(expected, actual, "Formatter returned unexpected string");
		}

		[Test]
		public void Ensure_Header_Is_Displayed_If_One_Is_Set()
		{
			var formatter = new CommandLineOptionFormatter();

			// expected:
			//  my custom header
			//
			//  Short1          Description1
			//  Short2:Long2    Description2 

			const string expectedHeader = "my custom header";

			formatter.Header = expectedHeader;

			var mockOption1 = CreateMockOption("Short1", null, "Description1");
			var mockOption2 = CreateMockOption("Short2", "Long2", "Description2");

			var expectedSb = new StringBuilder();
			expectedSb.AppendLine();
			expectedSb.AppendLine(expectedHeader);
			expectedSb.AppendLine();
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
		static ICommandLineOption CreateMockOption(string shortName, string longName, string description)
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

