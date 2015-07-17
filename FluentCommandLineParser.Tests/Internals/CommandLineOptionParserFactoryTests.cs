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
using Fclp.Internals.Parsing.OptionParsers;
using Fclp.Tests.FluentCommandLineParser;
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
            var int64Parser = factory.CreateParser<long>();
			var doubleParser = factory.CreateParser<double>();
			var dtParser = factory.CreateParser<DateTime>();
			var boolParser = factory.CreateParser<bool>();

			Assert.IsInstanceOf<StringCommandLineOptionParser>(stringParser);
			Assert.IsInstanceOf<Int32CommandLineOptionParser>(int32Parser);
            Assert.IsInstanceOf<Int64CommandLineOptionParser>(int64Parser);
			Assert.IsInstanceOf<DoubleCommandLineOptionParser>(doubleParser);
			Assert.IsInstanceOf<DateTimeCommandLineOptionParser>(dtParser);
			Assert.IsInstanceOf<BoolCommandLineOptionParser>(boolParser);
		}

        [Test]
        public void Ensure_Factory_Supports_List_Of_Int64()
        {
            var factory = new CommandLineOptionParserFactory();

            var int64ListParser = factory.CreateParser<System.Collections.Generic.List<long>>();

            Assert.IsInstanceOf<ListCommandLineOptionParser<long>>(int64ListParser);
        }

	    [Test]
	    public void Ensure_Factory_Supports_Enum()
	    {
            var factory = new CommandLineOptionParserFactory();

	        var enumParser = factory.CreateParser<TestEnum>();

            Assert.IsInstanceOf<EnumCommandLineOptionParser<TestEnum>>(enumParser);
	    }

        [Test]
        public void Ensure_Factory_Supports_EnumFlags()
        {
            var factory = new CommandLineOptionParserFactory();

            var enumParser = factory.CreateParser<TestEnumFlag>();

            Assert.IsInstanceOf<EnumFlagCommandLineOptionParser<TestEnumFlag>>(enumParser);
        }

        [Test]
	    public void Ensure_Factory_Returns_Custom_Enum_Formatter()
	    {
            var factory = new CommandLineOptionParserFactory();

            var customParser = new CustomEnumCommandLineOptionParser();
            factory.AddOrReplace(customParser);

            var enumParser = factory.CreateParser<TestEnum>();
            Assert.AreSame(customParser, enumParser);
	    }
	}

    public class CustomEnumCommandLineOptionParser : ICommandLineOptionParser<TestEnum>
    {
        public TestEnum Parse(Fclp.Internals.Parsing.ParsedOption parsedOption)
        {
            throw new NotImplementedException();
        }

        public bool CanParse(Fclp.Internals.Parsing.ParsedOption parsedOption)
        {
            throw new NotImplementedException();
        }
    }
}
