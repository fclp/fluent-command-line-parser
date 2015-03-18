#region License
// with_options_that_are_specified_in_the_args.cs
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

using System.Collections.Generic;
using Fclp.Internals;
using Fclp.Internals.Parsing;
using Fclp.Tests.FluentCommandLineParser.TestContext;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Fclp.Tests.FluentCommandLineParser
{
	namespace when_executing_parse_operation
	{
		class with_options_that_are_specified_in_the_args : FluentCommandLineParserTestContext
		{
			static ICommandLineParserResult result;
			static string[] args = null;

			static Mock<ICommandLineOption> _blankOption = new Mock<ICommandLineOption>();
			static string _blankOptionName = "blankOption";
			static string _blankOptionValue = "blank Option Value";
			private static ParsedOption _parsedBlankOption;

			static Mock<ICommandLineOption> _optionThatHasCallback = new Mock<ICommandLineOption>();
			static string _optionThatHasCallbackName = "optionThatHasCallback";
			static string _optionThatHasCallbackValue = "Callback Value";
			private static ParsedOption _parsedOptionThatHasCallback;

			static Mock<ICommandLineOption> _optionThatIsRequired = new Mock<ICommandLineOption>();
			static string _optionThatIsRequiredName = "optionThatIsRequired";
			static string _optionThatIsRequiredValue = "Is required value";
			private static ParsedOption _parsedOptionThatIsRequired;

			Establish context = () =>
			{
				// create item that has a callback - the bind value should be executed
				_parsedOptionThatHasCallback = new ParsedOption { Value = _optionThatHasCallbackValue, Key = _optionThatHasCallbackName };
				_optionThatHasCallback.SetupGet(x => x.ShortName).Returns(_optionThatHasCallbackName);
				_optionThatHasCallback.Setup(x => x.BindDefault()).Verifiable();
				_optionThatHasCallback.Setup(x => x.Bind(_parsedOptionThatHasCallback)).Verifiable();
				sut.Options.Add(_optionThatHasCallback.Object);

				// create option that has a callback and is required - the bind value should be executed like normal
				_parsedOptionThatIsRequired = new ParsedOption { Value = _optionThatIsRequiredValue, Key = _optionThatIsRequiredName };
				_optionThatIsRequired.SetupGet(x => x.IsRequired).Returns(true);
				_optionThatIsRequired.SetupGet(x => x.ShortName).Returns(_optionThatIsRequiredName);
				_optionThatIsRequired.Setup(x => x.Bind(_parsedOptionThatIsRequired)).Verifiable();
				sut.Options.Add(_optionThatIsRequired.Object);

				// create blank option
				_parsedBlankOption = new ParsedOption { Value = _blankOptionValue, Key = _blankOptionName };
				_blankOption.SetupGet(x => x.ShortName).Returns(_blankOptionName);
				_blankOption.Setup(x => x.Bind(_parsedBlankOption)).Verifiable();
				sut.Options.Add(_blankOption.Object);

				var parsedOptions = new List<ParsedOption>
				{
					_parsedOptionThatHasCallback,
					_parsedOptionThatIsRequired,
					_parsedBlankOption
				};

				var parserEngineResult = new ParserEngineResult(parsedOptions, CreateEmptyList<string>(), Create<string>());

				args = CreateArgsFromKvp(parsedOptions);

				var parserEngineMock = new Mock<ICommandLineParserEngine>();
                parserEngineMock.Setup(x => x.Parse(args, false)).Returns(parserEngineResult);
				sut.ParserEngine = parserEngineMock.Object;
			};

			Because of = () => CatchAnyError(() => result = sut.Parse(args));

			It should_not_error = () => error.ShouldBeNull();

			It should_return_results_with_no_errors = () => result.Errors.ShouldBeEmpty();

			It should_return_no_unmatched_options = () => result.UnMatchedOptions.ShouldBeEmpty();

			It should_have_called_bind_on_the_option_has_callback_setup = () => _optionThatHasCallback.Verify(x => x.Bind(_parsedOptionThatHasCallback), Times.Once());

			It should_have_called_bind_on_the_option_that_does_not_have_callback_setup = () => _blankOption.Verify(x => x.Bind(_parsedBlankOption), Times.Once());

			It should_have_called_bind_on_the_option_that_is_required = () => _optionThatIsRequired.Verify(x => x.Bind(_parsedOptionThatIsRequired), Times.Once());
		}
	}
}
