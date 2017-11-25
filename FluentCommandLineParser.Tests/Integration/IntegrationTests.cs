#region License
// IntegrationTests.cs
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

using Fclp.Tests.FluentCommandLineParser;
using Fclp.Tests.Internals;
using Machine.Specifications;
using Xunit;
using Xunit.Extensions;

namespace Fclp.Tests.Integration
{
	public class IntegrationTests : TestContextBase<Fclp.FluentCommandLineParser>
	{
		[Theory]
		[BoolInlineData("-b", true)]
		[BoolInlineData("-b+", true)]
		[BoolInlineData("-b-", false)]
		[BoolInlineData("/b:true", true)]
		[BoolInlineData("/b:false", false)]
		[BoolInlineData("-b true", true)]
		[BoolInlineData("-b false", false)]
		[BoolInlineData("-b=true", true)]
		[BoolInlineData("-b=false", false)]
        [BoolInlineData("-b on", true)]
        [BoolInlineData("-b off", false)]
        [BoolInlineData("-b ON", true)]
        [BoolInlineData("-b OFF", false)]
		[BoolInlineData("-b:on", true)]
		[BoolInlineData("-b:off", false)]
        [BoolInlineData("-b=on", true)]
        [BoolInlineData("-b=off", false)]
        [BoolInlineData("/b:1", true)]
        [BoolInlineData("/b:0", false)]
        [BoolInlineData("-b 1", true)]
        [BoolInlineData("-b 0", false)]
        [BoolInlineData("-b=1", true)]
        [BoolInlineData("-b=0", false)]
        [BoolInlineData("-b 1", true)]
        [BoolInlineData("-b 0", false)]
        [BoolInlineData("-b:1", true)]
        [BoolInlineData("-b:0", false)]
        [BoolInlineData("-b=1", true)]
        [StringInlineData("-s {0}", "Hello World")]
		[StringInlineData("-s:{0}", "Hello World")]
		[StringInlineData("-s={0}", "Hello World")]
		[Int32InlineData("-i 123", 123)]
		[Int32InlineData("-i:123", 123)]
		[Int32InlineData("-i=123", 123)]
        [Int64InlineData("-l 2147483649", 2147483649)]
        [Int64InlineData("-l:2147483649", 2147483649)]
        [Int64InlineData("-l=2147483649", 2147483649)]
		[DoubleInlineData("-d 123.456", 123.456)]
		[DoubleInlineData("-d:123.456", 123.456)]
		[DoubleInlineData("-d=123.456", 123.456)]
		[Int32EnumInlineData("-e 1", TestEnum.Value1)]
		[Int32EnumInlineData("-e:1", TestEnum.Value1)]
		[Int32EnumInlineData("-e=1", TestEnum.Value1)]
		[EnumInlineData("-e Value1", TestEnum.Value1)]
		[EnumInlineData("-e:Value1", TestEnum.Value1)]
		[EnumInlineData("-e=Value1", TestEnum.Value1)]
		public void SimpleShortOptionsAreParsedCorrectly(
			string arguments,
			bool? expectedBoolean,
			string expectedString,
			int? expectedInt32,
            long? expectedInt64,
			double? expectedDouble,
			TestEnum? expectedEnum)
		{
			sut = new Fclp.FluentCommandLineParser();

			bool? actualBoolean = null;
			string actualString = null;
			int? actualInt32 = null;
            long? actualInt64 = null;
			double? actualDouble = null;
			TestEnum? actualEnum = null;

			sut.Setup<bool>('b').Callback(b => actualBoolean = b);
			sut.Setup<string>('s').Callback(s => actualString = s);
			sut.Setup<int>('i').Callback(i => actualInt32 = i);
            sut.Setup<long>('l').Callback(l => actualInt64 = l);
			sut.Setup<double>('d').Callback(d => actualDouble = d);
			sut.Setup<TestEnum>('e').Callback(d => actualEnum = d);

			var args = ParseArguments(arguments);

			var results = sut.Parse(args);

			results.HasErrors.ShouldBeFalse();

			actualBoolean.ShouldEqual(expectedBoolean);
			actualString.ShouldEqual(expectedString);
			actualInt32.ShouldEqual(expectedInt32);
            actualInt64.ShouldEqual(expectedInt64);
			actualDouble.ShouldEqual(expectedDouble);
			actualEnum.ShouldEqual(expectedEnum);
		}

		[Theory]
		[InlineData("-xyz", true)]
		[InlineData("-xyz+", true)]
		[InlineData("-xyz-", false)]
		public void combined_bool_short_options_should_be_parsed_correctly(string arguments,bool expectedValue)
		{
			sut = new Fclp.FluentCommandLineParser();

			bool? actualXValue = null;
			bool? actualYValue = null;
			bool? actualZValue = null;

			sut.Setup<bool>('x').Callback(x => actualXValue = x);
			sut.Setup<bool>('y').Callback(y => actualYValue = y);
			sut.Setup<bool>('z').Callback(z => actualZValue = z);

			var args = ParseArguments(arguments);

			var results = sut.Parse(args);

			results.HasErrors.ShouldBeFalse();

			actualXValue.HasValue.ShouldBeTrue();
			actualYValue.HasValue.ShouldBeTrue();
			actualZValue.HasValue.ShouldBeTrue();

			actualXValue.Value.ShouldEqual(expectedValue);
			actualYValue.Value.ShouldEqual(expectedValue);
			actualZValue.Value.ShouldEqual(expectedValue);
		}

		[Theory]
		[InlineData("-xyz 'apply this to x, y and z'", "apply this to x, y and z")]
		[InlineData("-xyz salmon", "salmon")]
		[InlineData("-xyz 'salmon'", "salmon")]
		public void combined_short_options_should_have_the_same_value(string arguments, string expectedValue)
		{
			arguments = ReplaceWithDoubleQuotes(arguments);
			expectedValue = ReplaceWithDoubleQuotes(expectedValue);

			sut = new Fclp.FluentCommandLineParser();

			string actualXValue = null;
			string actualYValue = null;
			string actualZValue = null;

			sut.Setup<string>('x').Callback(x => actualXValue = x);
			sut.Setup<string>('y').Callback(y => actualYValue = y);
			sut.Setup<string>('z').Callback(z => actualZValue = z);

			var args = ParseArguments(arguments);

			var results = sut.Parse(args);

			results.HasErrors.ShouldBeFalse();

			actualXValue.ShouldEqual(expectedValue);
			actualYValue.ShouldEqual(expectedValue);
			actualZValue.ShouldEqual(expectedValue);
		}
	}
}
