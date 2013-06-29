#region License
// FluentCommandLineParserMSpecTests.cs
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
	class FluentCommandLineParserMSpecTests
	{
		[Subject(typeof(Fclp.FluentCommandLineParser))]
		abstract class FluentCommandLineParserTestContext : TestContextBase<Fclp.FluentCommandLineParser>
		{
			Establish context = () => CreateSut();
		}

		sealed class Constructor
		{
			class when_initialised : FluentCommandLineParserTestContext
			{
				It should_enable_case_sensitive = () =>
					sut.IsCaseSensitive.ShouldBeTrue();

				It should_have_an_error_formatter = () => 
					sut.ErrorFormatter.ShouldNotBeNull();

				It should_have_a_help_option = () => 
					sut.HelpOption.ShouldNotBeNull();

				It should_have_a_option_factory = () => 
					sut.OptionFactory.ShouldNotBeNull();

				It should_have_a_option_formatter = () => 
					sut.OptionFormatter.ShouldNotBeNull();

				It should_have_a_option_validator = () => 
					sut.OptionValidator.ShouldNotBeNull();

				It should_not_have_any_options_setup = () => 
					sut.Options.ShouldBeEmpty();

				It should_have_a_parser_engine = () => 
					sut.ParserEngine.ShouldNotBeNull();
			}
		}

		sealed class IsCaseSensitive
		{
			abstract class IsCaseSensitiveTestContext : FluentCommandLineParserTestContext { }

			class when_enabled : IsCaseSensitiveTestContext
			{
				Because of = () => sut.IsCaseSensitive = true;

				It should_return_enabled = () =>
					sut.IsCaseSensitive.ShouldBeTrue();

				It should_set_the_comparison_type_to_case_sensitive = () =>
					sut.StringComparison.ShouldEqual(Fclp.FluentCommandLineParser.CaseSensitiveComparison);
			}

			class when_disabled : IsCaseSensitiveTestContext
			{
				Because of = () => sut.IsCaseSensitive = false;

				It should_return_disabled = () =>
					sut.IsCaseSensitive.ShouldBeFalse();

				It should_set_the_comparison_type_to_ignore_case = () =>
					sut.StringComparison.ShouldEqual(Fclp.FluentCommandLineParser.IgnoreCaseComparison);
			}
		}
	}
}
