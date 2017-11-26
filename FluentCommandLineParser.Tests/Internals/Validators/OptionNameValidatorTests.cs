#region License
// OptionNameValidatorTests.cs
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
using Fclp.Internals;
using Fclp.Internals.Validators;
using Machine.Specifications;
using Moq;
using Ploeh.AutoFixture;
using It = Machine.Specifications.It;

namespace Fclp.Tests.Internals.Validators
{
	class OptionNameValidatorTests
	{
		[Subject(typeof(OptionNameValidator))]
		abstract class CommandLineOptionNameValidatorTestContext : TestContextBase<OptionNameValidator>
		{
		    static SpecialCharacters specialCharacters = new SpecialCharacters();

		    Establish context = () =>
		    {
		        fixture.Register(() => specialCharacters);
		        CreateSut();
		    };
        }

		sealed class Validate
		{
			abstract class ValidateTestContext : CommandLineOptionNameValidatorTestContext
			{
				protected const string ValidShortName = "s";
				protected const string ValidLongName = "long";

				protected static Mock<ICommandLineOption> option;

				Establish context = () =>
					CreateMock(out option);

				Because of = () =>
					error = Catch.Exception(() =>
						sut.Validate(option.Object, StringComparison.CurrentCultureIgnoreCase));

				protected static void SetupOptionWith(string shortName = ValidShortName, string longName = ValidLongName)
				{
					option.SetupGet(it => it.ShortName).Returns(shortName);
					option.SetupGet(it => it.LongName).Returns(longName);
				}
			}

			class when_the_short_name_is_null : ValidateTestContext
			{
				Establish context = () =>
					SetupOptionWith(shortName: null);

				It should_not_throw_an_error = () => error.ShouldBeNull();
			}

			class when_the_short_name_is_whitespace : ValidateTestContext
			{
				Establish context = () =>
					SetupOptionWith(shortName: " ");

				It should_throw_an_error = () => error.ShouldBeOfType<InvalidOptionNameException>();
			}

			class when_the_short_name_contains_a_colon : ValidateTestContext
			{
				Establish context = () =>
					SetupOptionWith(shortName: ":");

				It should_throw_an_error = () => error.ShouldBeOfType<InvalidOptionNameException>();
			}

			class when_the_short_name_contains_an_equality_sign : ValidateTestContext
			{
				Establish context = () =>
					SetupOptionWith(shortName: "=");

				It should_throw_an_error = () => error.ShouldBeOfType<InvalidOptionNameException>();
			}

			class when_the_short_name_is_empty : ValidateTestContext
			{
				Establish context = () =>
					SetupOptionWith(shortName: string.Empty);

				It should_not_throw_an_error = () => error.ShouldBeNull();
			}

			class when_the_short_name_is_a_control_char : ValidateTestContext
			{
				Establish context = () =>
					SetupOptionWith(shortName: ((char)7).ToString(CultureInfo.InvariantCulture));

				It should_throw_an_error = () => error.ShouldBeOfType<InvalidOptionNameException>();
			}

			class when_the_short_name_is_longer_than_one_char : ValidateTestContext
			{
				Establish context = () =>
					SetupOptionWith(shortName: CreateStringOfLength(2));

				It should_throw_an_error = () => error.ShouldBeOfType<InvalidOptionNameException>();
			}

			class when_the_short_name_is_one_char : ValidateTestContext
			{
				Establish context = () =>
					SetupOptionWith(shortName: CreateStringOfLength(1));

				It should_not_throw_an_error = () => error.ShouldBeNull();
			}

			class when_the_long_name_is_null : ValidateTestContext
			{
				Establish context = () =>
					SetupOptionWith(longName: null);

				It should_not_throw_an_error = () => error.ShouldBeNull();
			}

			class when_the_long_name_is_whitespace : ValidateTestContext
			{
				Establish context = () =>
					SetupOptionWith(longName: " ");

				It should_throw_an_error = () => error.ShouldBeOfType<InvalidOptionNameException>();
			}

			class when_the_long_name_contains_a_colon : ValidateTestContext
			{
				Establish context = () =>
					SetupOptionWith(longName: ValidLongName + ":");

				It should_throw_an_error = () => error.ShouldBeOfType<InvalidOptionNameException>();
			}

			class when_the_long_name_contains_an_equality_sign : ValidateTestContext
			{
				Establish context = () =>
					SetupOptionWith(longName: ValidLongName + "=");

				It should_throw_an_error = () => error.ShouldBeOfType<InvalidOptionNameException>();
			}

			class when_the_long_name_is_empty : ValidateTestContext
			{
				Establish context = () =>
					SetupOptionWith(longName: string.Empty);

				It should_not_throw_an_error = () => error.ShouldBeNull();
			}

			class when_the_long_name_is_longer_than_one_char : ValidateTestContext
			{
				Establish context = () =>
					SetupOptionWith(longName: CreateStringOfLength(2));

				It should_not_throw_an_error = () => error.ShouldBeNull();
			}

			class when_the_long_name_is_one_char : ValidateTestContext
			{
				Establish context = () =>
					SetupOptionWith(longName: CreateStringOfLength(1));

				It should_throw_a_too_long_error = () => error.ShouldBeOfType<InvalidOptionNameException>();
			}

			class when_the_long_name_contains_whitespace: ValidateTestContext
			{
				Establish context = () =>
					SetupOptionWith(longName: ValidLongName + " " + ValidLongName);

				It should_throw_an_error = () => error.ShouldBeOfType<InvalidOptionNameException>();
			}

			class when_the_long_name_is_null_and_the_short_name_is_null : ValidateTestContext
			{
				Establish context = () =>
					SetupOptionWith(shortName: null, longName: null);

				It should_throw_an_error = () => error.ShouldBeOfType<InvalidOptionNameException>();
			}

			class when_the_long_name_is_empty_and_the_short_name_is_empty : ValidateTestContext
			{
				Establish context = () =>
					SetupOptionWith(shortName: string.Empty, longName: string.Empty);

				It should_throw_an_error = () => error.ShouldBeOfType<InvalidOptionNameException>();
			}
		}
	}
}