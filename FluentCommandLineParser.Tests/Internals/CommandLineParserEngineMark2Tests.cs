#region License
// CommandLineParserEngineMark2Tests.cs
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

using System.Linq;
using Fclp.Internals;
using Fclp.Internals.Extensions;
using Fclp.Internals.Parsing;
using Machine.Specifications;
using Ploeh.AutoFixture;

namespace Fclp.Tests.Internals
{
	sealed class CommandLineParserEnginerMark2Tests
	{
		[Subject(typeof(CommandLineParserEngineMark2))]
		abstract class CommandLineParserEngineMark2TestContext : TestContextBase<CommandLineParserEngineMark2>
		{
		    static SpecialCharacters specialCharacters = new SpecialCharacters();

		    Establish context = () =>
		    {
		        fixture.Register(() => specialCharacters);
                CreateSut();
		    };
        }

		sealed class Parse
		{
			abstract class ParseTestContext : CommandLineParserEngineMark2TestContext
			{
				protected static string[] args;
				protected static ParserEngineResult result;

				protected static void SetupArgs(string arguments)
				{
					args = arguments.SplitOnWhitespace().ToArray();
				}

				Because of = () =>
                    result = sut.Parse(args, false);
			}

			class when_args_is_null : ParseTestContext
			{
				Establish context = () => args = null;
			
				It should_return_a_result_with_no_parsed_options = () =>
					result.ParsedOptions.ShouldBeEmpty();

				It should_return_a_result_with_no_additional_values = () =>
					result.AdditionalValues.ShouldBeEmpty();
			}

			class when_args_is_empty : ParseTestContext
			{
				Establish context = () => args = CreateEmptyList<string>().ToArray();

				It should_return_a_result_with_no_parsed_options = () =>
					result.ParsedOptions.ShouldBeEmpty();

				It should_return_a_result_with_no_additional_values = () =>
					result.AdditionalValues.ShouldBeEmpty();
			}

			class when_args_contains_negative_argument_seperated_with_a_colon : ParseTestContext
			{
				Establish context = () => SetupArgs("--int:-1");

				It should_return_a_single_option = () =>
					result.ParsedOptions.Count().ShouldEqual(1);

				It should_set_the_parsed_option_value_to_the_negative_number = () =>
					result.ParsedOptions.First().Value.ShouldEqual("-1");
			}

			class when_args_contains_negative_argument_seperated_with_a_equals : ParseTestContext
			{
				Establish context = () => SetupArgs("--int=-123");

				It should_return_a_single_option = () =>
					result.ParsedOptions.Count().ShouldEqual(1);

				It should_set_the_parsed_option_value_to_the_negative_number = () =>
					result.ParsedOptions.First().Value.ShouldEqual("-123");
			}

			class when_args_contains_negative_arguments_seperated_with_double_dash : ParseTestContext
			{
				Establish context = () => SetupArgs("--int -- -4321");

				It should_return_a_single_option = () =>
					result.ParsedOptions.Count().ShouldEqual(1);

				It should_set_the_parsed_option_value_to_the_negative_number = () =>
					result.ParsedOptions.First().Value.ShouldEqual("-4321");
			}

			class when_args_contains_a_single_switch : ParseTestContext
			{
				Establish context = () => SetupArgs("-b");

				It should_return_a_single_option = () =>
					result.ParsedOptions.Count().ShouldEqual(1);

				It should_set_the_parsed_key_to_the_correct_value = () =>
					result.ParsedOptions.First().Key.ShouldEqual("b");

				It should_set_the_parsed_raw_key_to_the_correct_value = () =>
					result.ParsedOptions.First().RawKey.ShouldEqual("-b");
			}

			class when_args_contains_only_the_double_dash_option_prefix : ParseTestContext
			{
				Establish context = () => SetupArgs("--");

				It should_return_no_parsed_options = () =>
					result.ParsedOptions.ShouldBeEmpty();

				It should_return_no_additional = () =>
					result.AdditionalValues.ShouldBeEmpty();
			}

			class when_args_contains_only_the_single_dash_option_prefix : ParseTestContext
			{
				Establish context = () => SetupArgs("-");

				It should_return_no_parsed_options = () =>
					result.ParsedOptions.ShouldBeEmpty();

				It should_return_it_as_an_additional = () =>
					result.AdditionalValues.ShouldContainOnly("-");
			}

			class when_args_contains_only_the_slash_option_prefix : ParseTestContext
			{
				Establish context = () => SetupArgs("/");

				It should_return_no_parsed_options = () =>
					result.ParsedOptions.ShouldBeEmpty();

				It should_return_it_as_an_additional = () =>
					result.AdditionalValues.ShouldContainOnly("/");
			}

			class when_args_contains_only_arguments_and_no_options : ParseTestContext
			{
				Establish context = () => SetupArgs("arg1 arg2 arg3");

				It should_return_no_parsed_options = () =>
					result.ParsedOptions.ShouldBeEmpty();

				It should_return_it_as_an_additional = () =>
					result.AdditionalValues.ShouldContainOnly("arg1", "arg2", "arg3");				
			}

			class when_args_starts_with_a_double_dash : ParseTestContext
			{
				Establish context = () => SetupArgs("-- --int 1 2 3 -a -b ");

				It should_return_no_parsed_options = () =>
					result.ParsedOptions.ShouldBeEmpty();

				It should_return_all_but_the_double_dash_as_an_additional = () =>
					result.AdditionalValues.ShouldContainOnly("--int", "1", "2", "3", "-a", "-b");			
			}
		} 
	}
	
}