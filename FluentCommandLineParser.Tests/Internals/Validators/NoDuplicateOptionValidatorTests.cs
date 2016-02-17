#region License
// NoDuplicateOptionValidatorTests.cs
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
using Fclp.Internals;
using Fclp.Internals.Validators;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Fclp.Tests.Internals.Validators
{
	class NoDuplicateOptionValidatorTests
	{
		[Subject(typeof(NoDuplicateOptionValidator))]
		abstract class NoDuplicateOptionValidatorTestContext : TestContextBase<NoDuplicateOptionValidator>
		{
			protected static Mock<ICommandLineOptionContainer> container;
			protected static List<ICommandLineOption> options;

			Establish context = () =>
			{
				FreezeMock(out container);
                options = new List<ICommandLineOption>();
                container.SetupGet(it => it.Options).Returns(options);

                CreateSut();
			};
		}

		sealed class Validate
		{
			[Subject("Validate")]
			abstract class ValidateTestContext : NoDuplicateOptionValidatorTestContext
			{
				protected static Mock<ICommandLineOption> option;
			    protected static StringComparison ComparisonType;

				Establish context = () =>
				{
					CreateMock(out option);

					option.SetupGet(it => it.HasShortName).Returns(true);
					option.SetupGet(it => it.HasLongName).Returns(true);

					option.SetupGet(it => it.ShortName).Returns(Create<string>());
					option.SetupGet(it => it.LongName).Returns(Create<string>());

                    option.SetupGet(it => it.Command).Returns(() => null);
                    option.SetupGet(it => it.HasCommand).Returns(false);
				};

				Because of = () =>
					error = Catch.Exception(() =>
                        sut.Validate(option.Object, ComparisonType));

				protected static void SetupExistingParserOptions(params ICommandLineOption[] options1)
				{
					options.AddRange(CreateManyAsList(options1));
				}

				protected static ICommandLineOption CreateOptionWith(string shortName = null, string longName = null)
				{
					var existingOption = CreateMock<ICommandLineOption>();

					existingOption.SetupGet(it => it.HasLongName).Returns(longName != null);
					existingOption.SetupGet(it => it.HasShortName).Returns(shortName != null);
					existingOption.SetupGet(it => it.ShortName).Returns(shortName);
					existingOption.SetupGet(it => it.LongName).Returns(longName);

					return existingOption.Object;
				}
			}

			class when_there_have_been_no_options_setup_thus_far : ValidateTestContext
			{
				Establish context = () =>
					container.SetupGet(it => it.Options).Returns(CreateEmptyList<ICommandLineOption>());

				It should_not_throw_an_error = () => error.ShouldBeNull();
			}

			sealed class when_case_sensitive
			{
				abstract class CaseSensitiveTestContext : ValidateTestContext
				{
					Establish context = () => ComparisonType = StringComparison.CurrentCulture;
				}

				class when_an_existing_option_contains_the_same_short_name_but_it_differs_by_case : CaseSensitiveTestContext
				{
					Establish context = () =>
					{
						var existingOption =
							CreateOptionWith(shortName: option.Object.ShortName.ToUpperInvariant());

						SetupExistingParserOptions(existingOption);
					};

					It should_not_throw_an_error = () => error.ShouldBeNull();
				}

				class when_an_existing_option_contains_the_same_short_name : CaseSensitiveTestContext
				{
					Establish context = () =>
					{
						var existingOption =
							CreateOptionWith(shortName: option.Object.ShortName);

						SetupExistingParserOptions(existingOption);
					};

					It should_throw_an_error = () => error.ShouldNotBeNull();
				}

				class when_an_existing_option_contains_the_same_long_name_but_it_differs_by_case : CaseSensitiveTestContext
				{
					Establish context = () =>
					{
						var existingOption =
							CreateOptionWith(longName: option.Object.LongName.ToUpperInvariant());

						SetupExistingParserOptions(existingOption);
					};

					It should_not_throw_an_error = () => error.ShouldBeNull();
				}

				class when_an_existing_option_contains_the_same_long_name : CaseSensitiveTestContext
				{
					Establish context = () =>
					{
						var existingOption =
							CreateOptionWith(longName: option.Object.LongName);

						SetupExistingParserOptions(existingOption);
					};

					It should_throw_an_error = () => error.ShouldNotBeNull();
				}

				class when_an_existing_option_contains_the_same_short_AND_long_name_but_they_differs_by_case : CaseSensitiveTestContext
				{
					Establish context = () =>
					{
						var existingOption =
							CreateOptionWith(shortName: option.Object.ShortName.ToUpperInvariant(), longName: option.Object.LongName.ToUpperInvariant());

						SetupExistingParserOptions(existingOption);
					};

					It should_not_throw_an_error = () => error.ShouldBeNull();
				}

				class when_an_existing_option_contains_the_same_short_AND_long_name : CaseSensitiveTestContext
				{
					Establish context = () =>
					{
						var existingOption =
							CreateOptionWith(shortName: option.Object.ShortName, longName: option.Object.LongName);

						SetupExistingParserOptions(existingOption);
					};

					It should_throw_an_error = () => error.ShouldNotBeNull();
				}
			}

			sealed class when_ignore_case
			{
				abstract class IgnoreCaseTestContext : ValidateTestContext
				{
                    Establish context = () => ComparisonType = StringComparison.CurrentCultureIgnoreCase;
				}

				class when_an_existing_option_contains_the_same_short_name_but_it_differs_by_case : IgnoreCaseTestContext
				{
					Establish context = () =>
					{
						var existingOption =
							CreateOptionWith(shortName: option.Object.ShortName.ToUpperInvariant());

						SetupExistingParserOptions(existingOption);
					};

					It should_throw_an_error_because_case_is_ignored = () => 
                        error.ShouldNotBeNull();
				}

				class when_an_existing_option_contains_the_same_short_name : IgnoreCaseTestContext
				{
					Establish context = () =>
					{
						var existingOption =
							CreateOptionWith(shortName: option.Object.ShortName);

						SetupExistingParserOptions(existingOption);
					};

					It should_throw_an_error = () => error.ShouldNotBeNull();
				}

				class when_an_existing_option_contains_the_same_long_name_but_it_differs_by_case : IgnoreCaseTestContext
				{
					Establish context = () =>
					{
						var existingOption =
							CreateOptionWith(longName: option.Object.LongName.ToUpperInvariant());

						SetupExistingParserOptions(existingOption);
					};

					It should_throw_an_error_because_case_is_ignored = () => error.ShouldNotBeNull();
				}

				class when_an_existing_option_contains_the_same_long_name : IgnoreCaseTestContext
				{
					Establish context = () =>
					{
						var existingOption =
							CreateOptionWith(longName: option.Object.LongName);

						SetupExistingParserOptions(existingOption);
					};

					It should_throw_an_error = () => error.ShouldNotBeNull();
				}

				class when_an_existing_option_contains_the_same_short_AND_long_name_but_they_differs_by_case : IgnoreCaseTestContext
				{
					Establish context = () =>
					{
						var existingOption =
							CreateOptionWith(shortName: option.Object.ShortName.ToUpperInvariant(), longName: option.Object.LongName.ToUpperInvariant());

						SetupExistingParserOptions(existingOption);
					};

					It should_throw_an_error_because_case_is_ignored = () => error.ShouldNotBeNull();
				}

				class when_an_existing_option_contains_the_same_short_AND_long_name : IgnoreCaseTestContext
				{
					Establish context = () =>
					{
						var existingOption =
							CreateOptionWith(shortName: option.Object.ShortName, longName: option.Object.LongName);

						SetupExistingParserOptions(existingOption);
					};

					It should_throw_an_error = () => error.ShouldNotBeNull();
				}
			}
		}
	}
}