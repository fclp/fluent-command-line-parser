#region License
// FluentCommandLineParserBuilderTests.cs
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

using Fclp.Tests.Internals;
using Machine.Specifications;

namespace Fclp.Tests
{
	public class FluentCommandLineParserBuilderTests
	{
		[Subject(typeof(FluentCommandLineBuilder<>))]
		abstract class FluentCommandLineParserBuilderTestContext<TArg> : TestContextBase<FluentCommandLineBuilder<TArg>> where TArg : new()
		{
			Establish context = () => CreateSut();
		}

		sealed class Parse
		{
			abstract class ParseTestContext<TArg> : FluentCommandLineParserBuilderTestContext<TArg> where TArg : new()
			{
				protected static string[] args;
				protected static ICommandLineParserResult result;

				Because of = () =>
					result = sut.Parse(args);
			}

			class when_invoked_with_example : ParseTestContext<TestApplicationArgs>
			{
				Establish context = () =>
				{
					sut.Setup(x => x.NewValue)
						.As('v', "value");

					sut.Setup(x => x.RecordId)
						.As('r', "recordId");

					sut.Setup(x => x.Silent)
						.As('s', "silent");

					args = new[] { "-r", "10", "-v", "Mr. Smith", "--silent" };
				};

				It should_enable_silent = () =>
					sut.Object.Silent.ShouldBeTrue();

				It should_assign_the_record_id = () =>
					sut.Object.RecordId.ShouldEqual(10);

				It should_assign_the_new_value = () =>
					sut.Object.NewValue.ShouldEqual("Mr. Smith");
			}

			class when_required_option_is_not_provided : ParseTestContext<TestApplicationArgs>
			{
				Establish context = () =>
				{
					sut.Setup(x => x.NewValue)
						.As('v', "value")
						.Required();

					args = new[] { "-r", "10", "--silent" };
				};

				It should_report_an_error = () =>
					result.HasErrors.ShouldBeTrue();
			}

			class when_default_is_specified_on_an_option_that_is_not_specified : ParseTestContext<TestApplicationArgs>
			{
				protected static string expectedDefaultValue;

				Establish context = () =>
				{
					Create(out expectedDefaultValue);

					sut.Setup(x => x.RecordId)
						.As('r', "recordId");

					sut.Setup(x => x.Silent)
						.As('s', "silent");

					sut.Setup(x => x.NewValue)
						.As('v', "value")
						.SetDefault(expectedDefaultValue);

					args = new[] { "-r", "10", "--silent" };
				};

				It should_assign_the_specified_default_as_the_new_value = () =>
					sut.Object.NewValue.ShouldEqual(expectedDefaultValue);

			}

			class when_building_objects_with_list_properties : ParseTestContext<ListTestApplicationArgs>
			{
				Establish context = () =>
				{
					sut.Setup(x => x.Booleans)
						.As("booleans");

					sut.Setup(x => x.Doubles)
						.As("doubles");

					sut.Setup(x => x.Integers)
						.As("integers");

					sut.Setup(x => x.Strings)
						.As("strings");

					args = new[]
					{
						"--booleans", "true", "false", "false", "true",
						"--doubles", "123.456", "789.456", "456.123",
						"--integers", "1", "0", "5", "10", "15",
						"--strings", "a", "list", "of", "strings"
					};
				};

				It should_contains_only_the_expected_integers = () =>
					sut.Object.Integers.ShouldContainOnly(1, 0, 5, 10, 15);

				It should_contains_only_the_expected_doubles = () =>
					sut.Object.Doubles.ShouldContainOnly(123.456d, 789.456d, 456.123d);

				It should_contains_only_the_expected_booleans = () =>
					sut.Object.Booleans.ShouldContainOnly(true, false, false, true);

				It should_contains_only_the_expected_strings = () =>
					sut.Object.Strings.ShouldContainOnly("a", "list", "of", "strings");
			}

			class when_building_objects_with_enumerable_properties : ParseTestContext<EnumerableApplicationArgs>
			{
				Establish context = () =>
				{
					sut.Setup(x => x.Booleans)
						.As("booleans");

					sut.Setup(x => x.Doubles)
						.As("doubles");

					sut.Setup(x => x.Integers)
						.As("integers");

					sut.Setup(x => x.Strings)
						.As("strings");

					args = new[]
					{
						"--booleans", "true", "false", "false", "true",
						"--doubles", "123.456", "789.456", "456.123",
						"--integers", "1", "0", "5", "10", "15",
						"--strings", "a", "list", "of", "strings"
					};
				};

				It should_contains_only_the_expected_integers = () =>
					sut.Object.Integers.ShouldContainOnly(1, 0, 5, 10, 15);

				It should_contains_only_the_expected_doubles = () =>
					sut.Object.Doubles.ShouldContainOnly(123.456d, 789.456d, 456.123d);

				It should_contains_only_the_expected_booleans = () =>
					sut.Object.Booleans.ShouldContainOnly(true, false, false, true);

				It should_contains_only_the_expected_strings = () =>
					sut.Object.Strings.ShouldContainOnly("a", "list", "of", "strings");
			}
		}
	}
}