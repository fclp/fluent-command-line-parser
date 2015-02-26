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

using System.Globalization;
using System.Linq;
using Fclp.Tests.FluentCommandLineParser;
using Fclp.Tests.Internals;
using Machine.Specifications;

namespace Fclp.Tests
{
	public class FluentCommandLineParserBuilderTests
	{
		[Subject(typeof(FluentCommandLineParser<>))]
		abstract class FluentCommandLineParserBuilderTestContext : TestContextBase<FluentCommandLineParser<TestApplicationArgs>>
		{
			Establish context = () => CreateSut();
		}

		sealed class Constructor
		{
			class when_initialised : FluentCommandLineParserBuilderTestContext
			{
				It should_enable_case_sensitive = () =>
					sut.IsCaseSensitive.ShouldBeTrue();

				It should_have_the_fluent_parser_by_default = () =>
					sut.Parser.ShouldBeOfType<IFluentCommandLineParser>();

				It should_have_initialised_the_object = () =>
					sut.Object.ShouldNotBeNull();
			}
		}

		sealed class IsCaseSensitive
		{
			abstract class IsCaseSensitiveTestContext : FluentCommandLineParserBuilderTestContext { }

			class when_enabled : IsCaseSensitiveTestContext
			{
				Because of = () => sut.IsCaseSensitive = true;

				It should_return_enabled = () =>
					sut.IsCaseSensitive.ShouldBeTrue();

				It should_enable_case_sensitivity_on_the_parser = () =>
					sut.Parser.IsCaseSensitive.ShouldBeTrue();
			}

			class when_disabled : IsCaseSensitiveTestContext
			{
				Because of = () => sut.IsCaseSensitive = false;

				It should_return_disabled = () =>
					sut.IsCaseSensitive.ShouldBeFalse();

				It should_disable_case_sensitivity_on_the_parser = () =>
					sut.Parser.IsCaseSensitive.ShouldBeFalse();
			}
		}

		sealed class Parse
		{
			abstract class ParseTestContext : FluentCommandLineParserBuilderTestContext
			{
				protected static string[] args;
				protected static ICommandLineParserResult result;

				Because of = () =>
					result = sut.Parse(args);
			}

			class when_invoked_with_example : ParseTestContext
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

			class when_required_option_is_not_provided : ParseTestContext
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

			class when_default_is_specified_on_an_option_that_is_not_specified : ParseTestContext
			{
				static string expectedDefaultValue;

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


			class ParseEnum
			{
				abstract class ParseEnumTestContext : ParseTestContext
				{
					protected static TestEnum expectedTestEnum;

					Establish context = () =>
					{
						expectedTestEnum = TestEnum.Value1;

						sut.Setup(x => x.Enum)
							.As('e', "enum");
					};
				}

				class when_enum_is_specified_as_valid_string : ParseEnumTestContext
				{
					Establish context = () =>
						args = new[] { "-e", expectedTestEnum.ToString() };

					It should_assign_the_expected_enum_value_to_the_args = () =>
						sut.Object.Enum.ShouldEqual(expectedTestEnum);
				}

                class when_enum_is_specified_as_valid_int32 : ParseEnumTestContext
				{
					Establish context = () =>
						args = new[] { "-e", ((int)expectedTestEnum).ToString(CultureInfo.InvariantCulture) };

					It should_assign_the_expected_enum_value_to_the_args = () =>
						sut.Object.Enum.ShouldEqual(expectedTestEnum);
				}

				class when_enum_is_specified_as_invalid_string : ParseEnumTestContext
				{
					Establish context = () =>
						args = new[] { "-e", "not-a-valid-enum" };

				    It should_return_an_error_as_part_of_the_result = () =>
				        result.HasErrors.ShouldBeTrue();

                    It should_return_an_error_for_the_enum_option = () =>
                        result.Errors.Single().Option.ShortName.ShouldEqual("e");
				}

                class when_enum_is_specified_as_invalid_int32 : ParseEnumTestContext
				{
					Establish context = () =>
						args = new[] { "-e", "123456" };

				    It should_return_an_error_as_part_of_the_result = () =>
				        result.HasErrors.ShouldBeTrue();

                    It should_return_an_error_for_the_enum_option = () =>
                        result.Errors.Single().Option.ShortName.ShouldEqual("e");
				}
			}
		}
	}
}