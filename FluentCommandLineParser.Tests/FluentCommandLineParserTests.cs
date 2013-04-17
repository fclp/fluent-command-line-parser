#region License
// FluentCommandLineParserTests.cs
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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Fclp.Internals;
using Fclp.Internals.Errors;
using Moq;
using NUnit.Framework;

namespace Fclp.Tests
{
	/// <summary>
	/// Contains unit tests for the <see cref="FluentCommandLineParser"/> class.
	/// </summary>
	[TestFixture]
	public class FluentCommandLineParserTests
	{
		#region HelperMethods

		/// <summary>
		/// Helper method to return the parser as its interface
		/// </summary>
		static IFluentCommandLineParser CreateFluentParser()
		{
			return new Fclp.FluentCommandLineParser();
		}

		static void CallParserWithAllKeyVariations(IFluentCommandLineParser parser, string key, string value, Action<string[], ICommandLineParserResult> assertCallback)
		{
			foreach (string[] args in CreateAllKeyVariations(key, value))
				assertCallback(args, parser.Parse(args));
		}

		static IEnumerable<string[]> CreateAllKeyVariations(string key, string value)
		{
			var keys = new[] { "-", "--", "/" };
			var valueIdentifiers = new[] { '=', ':' };

			foreach (string k in keys)
			{
				foreach (char valueIdentifier in valueIdentifiers)
				{
					yield return new[] { k + key + valueIdentifier + value };
				}

				yield return new[] { k + key, value };
			}
		}

		static string FormatArgs(string[] args)
		{
			return "Executed with args: " + string.Join(" ", args);
		}

		static void RunTest<T>(string value, T expected)
		{
			var parser = CreateFluentParser();
			T actual = default(T);

			parser.Setup<T>("s", "long")
				.Callback(val => actual = val);

			var assert = new Action<string[], ICommandLineParserResult>((args, result) =>
			{
				string msg = FormatArgs(args);
				Assert.AreEqual(expected, actual, msg);
				Assert.IsFalse(result.HasErrors, msg);
				Assert.IsFalse(result.Errors.Any(), msg);
			});

			CallParserWithAllKeyVariations(parser, "short", value, assert);
			CallParserWithAllKeyVariations(parser, "long", value, assert);
		}

		#endregion

		#region Description Tests

		[Test]
		public void Ensure_Description_Can_Be_Set()
		{
			var parser = CreateFluentParser();

			const string expected = "my description";

			var cmdOption = parser.Setup<string>("s").WithDescription(expected);

			var actual = ((ICommandLineOption)cmdOption).Description;

			Assert.AreSame(expected, actual);
		}

		#endregion Description Tests

		#region Top Level Tests

		#region String Option

		[Test]
		public void Ensure_Parser_Calls_The_Callback_With_Expected_String_When_Using_Short_option()
		{
			const string expected = "my-expected-string";
			RunTest(expected, expected);

			//const string key = "s";
			//string actual = null;

			//var parser = CreateFluentParser();

			//parser
			//    .Setup<string>(key)
			//    .Callback(val => actual = val);

			//CallParserWithAllKeyVariations(parser, key, expected, (args, result) =>
			//{
			//    string msg = "Executed with args: " + FormatArgs(args);
			//    Assert.AreEqual(expected, actual, msg);
			//    Assert.IsFalse(result.HasErrors, msg);
			//    Assert.IsFalse(result.Errors.Any(), msg);
			//});
		}

		[Test]
		public void Ensure_Parser_Calls_The_Callback_With_Expected_String_When_Using_Long_option()
		{
			const string expected = "my-expected-string";
			const string key = "string";
			string actual = null;

			var parser = CreateFluentParser();

			parser
				.Setup<string>("s", key)
				.Callback(val => actual = val);

			CallParserWithAllKeyVariations(parser, key, expected, (args, result) =>
			{
				string msg = "Executed with args: " + FormatArgs(args);
				Assert.AreEqual(expected, actual, msg);
				Assert.IsFalse(result.HasErrors, msg);
				Assert.IsFalse(result.Errors.Any(), msg);
			});
		}

		#endregion String Option

		#region Int32 Option

		[Test]
		public void Ensure_Parser_Calls_The_Callback_With_Expected_Int32_When_Using_Short_option()
		{
			const int expected = int.MaxValue;
			RunTest(expected.ToString(CultureInfo.InvariantCulture), expected);
			//const string shortKey = "i";
			//int actual = default(int);

			//var parser = CreateFluentParser();

			//parser
			//    .Setup<int>(shortKey)
			//    .Callback(val => actual = val);

			//CallParserWithAllKeyVariations(parser, shortKey, expected.ToString(CultureInfo.InvariantCulture), (args, result) =>
			//{
			//    string msg = "Executed with args: " + FormatArgs(args);
			//    Assert.AreEqual(expected, actual, msg);
			//    Assert.IsFalse(result.HasErrors, msg);
			//    Assert.IsFalse(result.Errors.Any(), msg);
			//});
		}

		[Test]
		public void Ensure_Parser_Calls_The_Callback_With_Expected_Int32_When_Using_Long_option()
		{
			const int expected = int.MaxValue;
			const string shortKey = "i";
			const string longKey = "int32";
			int actual = default(int);

			var parser = CreateFluentParser();

			parser
				.Setup<int>(shortKey, longKey)
				.Callback(val => actual = val);

			CallParserWithAllKeyVariations(parser, longKey, expected.ToString(CultureInfo.InvariantCulture), (args, result) =>
			{
				string msg = "Executed with args: " + FormatArgs(args);
				Assert.AreEqual(expected, actual, msg);
				Assert.IsFalse(result.HasErrors, msg);
				Assert.IsFalse(result.Errors.Any(), msg);
			});
		}

		#endregion Int32 Option

		#region Double Option

		[Test]
		public void Ensure_Parser_Calls_The_Callback_With_Expected_Double_When_Using_Short_option()
		{
			const double expected = 1.23456789d;
			RunTest(expected.ToString(CultureInfo.InvariantCulture), expected);
			//const string shortKey = "d";
			//double actual = default(double);

			//var parser = CreateFluentParser();

			//parser
			//    .Setup<double>(shortKey)
			//    .Callback(val => actual = val);

			//CallParserWithAllKeyVariations(parser, shortKey, expected.ToString(CultureInfo.InvariantCulture), (args, result) =>
			//{
			//    Assert.AreEqual(expected, actual, FormatArgs(args));
			//    Assert.IsFalse(result.HasErrors, FormatArgs(args));
			//    Assert.IsFalse(result.Errors.Any(), FormatArgs(args));
			//});
		}

		[Test]
		public void Ensure_Parser_Calls_The_Callback_With_Expected_Double_When_Using_Long_option()
		{
			const double expected = 1.23456789d;
			const string shortKey = "d";
			const string longKey = "double";
			double actual = default(double);

			var parser = CreateFluentParser();

			parser
				.Setup<double>(shortKey, longKey)
				.Callback(val => actual = val);

			CallParserWithAllKeyVariations(parser, longKey, expected.ToString(CultureInfo.InvariantCulture), (args, result) =>
			{
				Assert.AreEqual(expected, actual, FormatArgs(args));
				Assert.IsFalse(result.HasErrors, FormatArgs(args));
				Assert.IsFalse(result.Errors.Any(), FormatArgs(args));
			});
		}

		#endregion Double Option

		#region Enum Option

		//enum TestEnum
		//{
		//    Value0 = 0,
		//    Value1 = 1
		//}

		//[Test]
		//public void Ensure_Parser_Calls_The_Callback_With_Expected_Enum_When_Using_Short_option()
		//{
		//    const TestEnum expected = TestEnum.Value1;

		//    TestEnum actual = TestEnum.Value0;

		//    IFluentCommandLineParser parser = new FluentCommandLineParser();

		//    parser
		//        .Setup<TestEnum>("e")
		//        .Callback(val => actual = val);

		//    parser.Parse(new[] { "-e", expected.ToString() });

		//    Assert.AreEqual(expected, actual);
		//}

		//[Test]
		//public void Ensure_Parser_Calls_The_Callback_With_Expected_Enum_When_Using_Long_option()
		//{
		//    const TestEnum expected = TestEnum.Value1;

		//    TestEnum actual = TestEnum.Value0;

		//    IFluentCommandLineParser parser = new FluentCommandLineParser();

		//    parser
		//        .Setup<TestEnum>("e", "enum")
		//        .Callback(val => actual = val);

		//    parser.Parse(new[] { "--enum", expected.ToString() });

		//    Assert.AreEqual(expected, actual);
		//}

		#endregion Enum Option

		#region DateTime Option

		[Test]
		public void Ensure_Parser_Calls_The_Callback_With_Expected_DateTime_When_Using_Short_option()
		{
			var expected = new DateTime(2012, 2, 29, 01, 01, 01);
			RunTest(expected.ToString("yyy-MM-ddThh:mm:ss", CultureInfo.InvariantCulture), expected);
			//DateTime actual = default(DateTime);

			//var parser = CreateFluentParser();

			//parser
			//    .Setup<DateTime>("dt")
			//    .Callback(val => actual = val);

			//var result = parser.Parse(new[] { "-dt", expected.ToString("yyyy-MM-ddThh:mm:ss", CultureInfo.CurrentCulture) });

			//Assert.AreEqual(expected, actual);
			//Assert.IsFalse(result.HasErrors);
			//Assert.IsFalse(result.Errors.Any());
		}

		[Test]
		public void Ensure_Parser_Calls_The_Callback_With_Expected_DateTime_When_Using_Long_option()
		{
			var expected = new DateTime(2012, 2, 29, 01, 01, 01);

			DateTime actual = default(DateTime);

			var parser = CreateFluentParser();

			parser
				.Setup<DateTime>("d", "datetime")
				.Callback(val => actual = val);

			var result = parser.Parse(new[] { "--datetime", expected.ToString("yyyy-MM-ddThh:mm:ss", CultureInfo.CurrentCulture) });

			Assert.AreEqual(expected, actual);
			Assert.IsFalse(result.HasErrors);
			Assert.IsFalse(result.Errors.Any());
		}

		#endregion DateTime Option

        #region Long Option Only

        [Test]
        public void Can_have_long_option_only()
        {
            var parser = CreateFluentParser();
            var s = "";

            parser.Setup<string>(null, "my-feature")
                  .Callback(val => s = val);

            var result = parser.Parse(new[] { "--my-feature", "somevalue" });

            Assert.IsFalse(result.HasErrors);
            Assert.IsFalse(result.EmptyArgs);
            Assert.IsFalse(result.HelpCalled);

            Assert.AreEqual("somevalue", s);
        }

        #endregion

        #region Required

        [Test]
		public void Ensure_Expected_Error_Is_Returned_If_A_Option_Is_Required_And_Null_Args_Are_Specified()
		{
			var parser = CreateFluentParser();

			parser.Setup<string>("s")
				.Required();

			var result = parser.Parse(null);

			Assert.IsTrue(result.HasErrors);

			Assert.AreEqual(1, result.Errors.Count());

			Assert.IsInstanceOf(typeof(ExpectedOptionNotFoundParseError), result.Errors.First());
		}

		[Test]
		public void Ensure_Expected_Error_Is_Returned_If_A_Option_Is_Required_And_Empty_Args_Are_Specified()
		{
			var parser = CreateFluentParser();

			parser.Setup<string>("s")
				.Required();

			var result = parser.Parse(new string[0]);

			Assert.IsTrue(result.HasErrors);

			Assert.AreEqual(1, result.Errors.Count());

			Assert.IsInstanceOf(typeof(ExpectedOptionNotFoundParseError), result.Errors.First());
		}

		[Test]
		public void Ensure_Expected_Error_Is_Returned_If_Required_Option_Is_Provided()
		{
			var parser = CreateFluentParser();

			parser.Setup<string>("s")
				.Required();

			var result = parser.Parse(new[] { "-d" });

			Assert.IsTrue(result.HasErrors);

			Assert.AreEqual(1, result.Errors.Count());

			Assert.IsInstanceOf(typeof(ExpectedOptionNotFoundParseError), result.Errors.First());
		}

		[Test]
		public void Ensure_No_Error_Returned_If_Required_Option_Is_Not_Provided()
		{
			var parser = CreateFluentParser();

			parser.Setup<string>("s");

			var result = parser.Parse(new[] { "-d" });

			Assert.IsFalse(result.HasErrors);
			Assert.IsFalse(result.Errors.Any());
		}

		[Test]
		[ExpectedException(typeof(OptionAlreadyExistsException))]
		public void Ensure_Expected_Exception_Thrown_If_Adding_A_Option_With_A_ShortName_Which_Has_Already_Been_Setup()
		{
			var parser = CreateFluentParser();

			parser.Setup<string>("s", "string");

			parser.Setup<int>("s", "int32");
		}

		[Test]
		[ExpectedException(typeof(OptionAlreadyExistsException))]
		public void Ensure_Expected_Exception_Thrown_If_Adding_A_Option_With_A_ShortName_And_LongName_Which_Has_Already_Been_Setup()
		{
			var parser = CreateFluentParser();

			parser.Setup<string>("s", "string");

			parser.Setup<int>("s", "string");
		}

		[Test]
		[ExpectedException(typeof(OptionAlreadyExistsException))]
		public void Ensure_Expected_Exception_Thrown_If_Adding_A_Option_With_A_LongName_Which_Has_Already_Been_Setup()
		{
			var parser = CreateFluentParser();

			parser.Setup<string>("s", "string");

			parser.Setup<int>("i", "string");
		}

		#endregion

		#region Default

		[Test]
		public void Ensure_Default_Value_Returned_If_No_Value_Specified()
		{
			var parser = CreateFluentParser();

			const string expected = "my expected value";
			string actual = null;

			parser.Setup<string>("s")
				.Callback(s => actual = s)
				.SetDefault(expected);

			var result = parser.Parse(new[] { "-s" });

			Assert.AreSame(expected, actual);
			Assert.IsTrue(result.HasErrors);
		}

		[Test]
		public void Ensure_Default_Value_Returned_If_No_Option_Or_Value_Specified()
		{
			var parser = CreateFluentParser();

			const string expected = "my expected value";
			string actual = null;

			parser.Setup<string>("s")
				.Callback(s => actual = s)
				.SetDefault(expected);

			var result = parser.Parse(new string[0]);

			Assert.AreSame(expected, actual);
			Assert.IsFalse(result.HasErrors);
			Assert.IsFalse(result.Errors.Any());
		}

		#endregion

		#region No Args

		[Test]
		public void Ensure_Can_Specify_Empty_Args()
		{
			var parser = CreateFluentParser();

			var result = parser.Parse(new string[0]);

			Assert.IsFalse(result.HasErrors);
			Assert.IsTrue(result.EmptyArgs);
			Assert.IsFalse(result.Errors.Any());
		}

		[Test]
		public void Ensure_Can_Specify_Null_Args()
		{
			var parser = CreateFluentParser();

			var result = parser.Parse(null);

			Assert.IsFalse(result.HasErrors);
			Assert.IsTrue(result.EmptyArgs);
			Assert.IsFalse(result.Errors.Any());
		}

		[Test]
		public void Ensure_Defaults_Are_Called_When_Empty_Args_Specified()
		{
			var parser = CreateFluentParser();

			const int expectedInt = 123;
			const double expectedDouble = 123.456;
			const string expectedString = "my string";
			const bool expectedBool = true;

			int actualInt = 0;
			double actualDouble = 0;
			string actualString = null;
			bool actualBool = false;

			parser.Setup<int>("i").Callback(i => actualInt = i).SetDefault(expectedInt);
			parser.Setup<string>("s").Callback(s=> actualString = s).SetDefault(expectedString);
			parser.Setup<bool>("b").Callback(b => actualBool = b).SetDefault(expectedBool);
			parser.Setup<double>("d").Callback(d => actualDouble = d).SetDefault(expectedDouble);

			var result = parser.Parse(null);

			Assert.IsFalse(result.HasErrors);
			Assert.IsTrue(result.EmptyArgs);
			Assert.AreEqual(expectedInt, actualInt);
			Assert.AreEqual(expectedDouble, actualDouble);
			Assert.AreEqual(expectedString, actualString);
			Assert.AreEqual(expectedBool, actualBool);
		}

		#endregion No Args

		#region Example

        [Test]
        public void Ensure_Example_Works_As_Expected()
        {
            const int expectedRecordId = 10;
            const string expectedValue = "Mr. Smith";
            const bool expectedSilentMode = true;
            const bool expectedSwitchA = true;
            const bool expectedSwitchB = true;
            const bool expectedSwitchC = false;

            var args = new[] { "-r", expectedRecordId.ToString(CultureInfo.InvariantCulture), "-v", "\"Mr. Smith\"", "--silent", "-ab", "-c-" };

            int recordId = 0;
            string newValue = null;
            bool inSilentMode = false;
            bool switchA = false;
            bool switchB = false;
            bool switchC = true;

            IFluentCommandLineParser parser = CreateFluentParser();

            parser.Setup<bool>("a")
                  .Callback(value => switchA = value);

            parser.Setup<bool>("b")
                  .Callback(value => switchB = value);

            parser.Setup<bool>("c")
                  .Callback(value => switchC = value);

            // create a new Option using a short and long name
            parser.Setup<int>("r", "record")
                    .WithDescription("The record id to update (required)")
                    .Callback(record => recordId = record) // use callback to assign the record value to the local RecordID property
                    .Required(); // fail if this Option is not provided in the arguments

            parser.Setup<bool>(null, "silent")
                  .WithDescription("Execute the update in silent mode without feedback (default is false)")
                  .Callback(silent => inSilentMode = silent)
                  .SetDefault(false); // explicitly set the default value to use if this Option is not specified in the arguments


            parser.Setup<string>("v", "value")
                    .WithDescription("The new value for the record (required)") // used when help is requested e.g -? or --help 
                    .Callback(value => newValue = value)
                    .Required();

            // do the work
            ICommandLineParserResult result = parser.Parse(args);

            Assert.IsFalse(result.HasErrors);
            Assert.IsFalse(result.Errors.Any());

            Assert.AreEqual(expectedRecordId, recordId);
            Assert.AreEqual(expectedValue, newValue);
            Assert.AreEqual(expectedSilentMode, inSilentMode);
            Assert.AreEqual(expectedSwitchA, switchA);
            Assert.AreEqual(expectedSwitchB, switchB);
            Assert.AreEqual(expectedSwitchC, switchC);
        }

		#endregion

		#region Setup Help

		[Test]
		public void Setup_Help_And_Ensure_It_Is_Called_With_Custom_Formatter()
		{
			var parser = new Fclp.FluentCommandLineParser();

			var formatter = new Mock<ICommandLineOptionFormatter>();

			var args = new[] {"/help", "i", "s"};
			const string expectedCallbackResult = "blah";
			string callbackResult = null;

			parser.SetupHelp("?", "HELP", "h")
					.Callback(s => callbackResult = s)
					.WithCustomFormatter(formatter.Object);

			parser.Setup<int>("i");
			parser.Setup<string>("s");

			formatter.Setup(x => x.Format(parser.Options)).Returns(expectedCallbackResult);

			var result = parser.Parse(args);

			Assert.AreSame(expectedCallbackResult, callbackResult);
			Assert.IsFalse(result.HasErrors);
			Assert.IsTrue(result.HelpCalled);
		}

		[Test]
		public void Setup_Help_And_Ensure_It_Is_Called()
		{
			var parser = new Fclp.FluentCommandLineParser();

			var formatter = new Mock<ICommandLineOptionFormatter>();

			var args = new[] { "/help", "i", "s" };
			const string expectedCallbackResult = "blah";
			bool wasCalled = false;

			parser.SetupHelp("?", "HELP", "h")
					.Callback(() => wasCalled = true);

			parser.Setup<int>("i");
			parser.Setup<string>("s");

			formatter.Setup(x => x.Format(parser.Options)).Returns(expectedCallbackResult);

			var result = parser.Parse(args);

			Assert.IsTrue(wasCalled);
			Assert.IsFalse(result.HasErrors);
			Assert.IsTrue(result.HelpCalled);
		}

		#endregion

		#endregion Top Level Tests

		#region Duplicate Options Tests

		[Test]
		public void Ensure_First_Value_Is_Stored_When_Duplicate_Options_Are_Specified()
		{
			var parser = CreateFluentParser();

			int? number = 0;
			parser.Setup<int>("n").Callback(n => number = n);

			parser.Parse(new[] { "/n=1", "/n=2", "-n=3", "--n=4" });

			Assert.AreEqual(1, number);
		}

		#endregion
	}
}

