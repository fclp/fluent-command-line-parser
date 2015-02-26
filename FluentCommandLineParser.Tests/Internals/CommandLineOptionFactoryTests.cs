#region License
// CommandLineOptionFactoryTests.cs
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

using Fclp.Internals;
using Fclp.Internals.Parsing;
using Fclp.Internals.Parsing.OptionParsers;
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
