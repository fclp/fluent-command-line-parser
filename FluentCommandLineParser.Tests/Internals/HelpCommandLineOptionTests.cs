#region License
// HelpCommandLineOptionTests.cs
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
using Fclp.Internals.Parsing;
using Machine.Specifications;

namespace Fclp.Tests.Internals
{
	sealed class HelpCommandLineOptionTests
	{
		abstract class HelpCommandLineOptionTestContext : TestContextBase<HelpCommandLineOption> 
		{
			Establish context = () => CreateSut();    
		}


		sealed class ShouldShowHelp
		{
			abstract class ShouldShowHelpTestContext : HelpCommandLineOptionTestContext
			{
				protected static bool actualResult;
				protected static IEnumerable<ParsedOption> parsedOptions;
				protected static StringComparison comparisonType;

				Establish context = () =>
					comparisonType = StringComparison.CurrentCulture;

				Because of = () => actualResult = sut.ShouldShowHelp(parsedOptions, comparisonType);
			}

			class when_the_args_are_empty_and_the_option_is_setup_to_handle_empty_args_like_help_args : ShouldShowHelpTestContext
			{
				Establish context = () =>
				{
					parsedOptions = null;
					sut.UseForEmptyArgs();
				};

				It should_return_true = () => actualResult.ShouldBeTrue();
			}
			class when_the_args_are_empty_and_the_option_is_not_setup_to_handle_empty_args_like_help_args : ShouldShowHelpTestContext
			{
				Establish context = () => parsedOptions = null;

				It should_return_false = () => actualResult.ShouldBeFalse();
			}
		}
	}
}