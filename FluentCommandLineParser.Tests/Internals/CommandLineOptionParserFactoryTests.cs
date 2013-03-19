#region License
// CommandLineOptionParserFactoryTests.cs
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
