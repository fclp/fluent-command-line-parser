#region License
// with_options_that_are_not_specified_in_the_args.cs
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
using System.Linq;
using Fclp.Internals;
using Fclp.Tests.FluentCommandLineParser.TestContext;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Fclp.Tests.FluentCommandLineParser
{
	namespace when_executing_parse_operation
	{
		class with_options_that_are_not_specified_in_the_args : FluentCommandLineParserTestContext
		{
			static ICommandLineParserResult result;
			static List<ICommandLineOption> expectedUnMatched = new List<ICommandLineOption>();
			static string[] args = null;
			static Mock<ICommandLineOption> _notRequiredButHasDefaultValue = new Mock<ICommandLineOption>();
			static Mock<ICommandLineOption> _notRequiredAndHasNoDefaultValue = new Mock<ICommandLineOption>();
			static Mock<ICommandLineOption> _required = new Mock<ICommandLineOption>();

			Establish context = () =>
				{
					// create item that won't be matched an has no default value
					_notRequiredAndHasNoDefaultValue.SetupGet(x => x.HasDefault).Returns(false);
					_notRequiredAndHasNoDefaultValue.SetupGet(x => x.ShortName).Returns("notRequiredAndHasNoDefaultValue");
					_notRequiredAndHasNoDefaultValue.Setup(x => x.BindDefault()).Verifiable();
					sut.Options.Add(_notRequiredAndHasNoDefaultValue.Object);
					
					// create item that won't be matched but is required
					_required.SetupGet(x => x.IsRequired).Returns(true);
					_required.SetupGet(x => x.ShortName).Returns("required");
					sut.Options.Add(_required.Object);
					
					// create item that isn't required but has a default value
					_notRequiredButHasDefaultValue.SetupGet(x => x.HasDefault).Returns(true);
					_notRequiredButHasDefaultValue.SetupGet(x => x.ShortName).Returns("notRequiredButHasDefaultValue");
					_notRequiredButHasDefaultValue.Setup(x => x.BindDefault()).Verifiable();
					sut.Options.Add(_notRequiredButHasDefaultValue.Object);

					// these will be unmatched
					expectedUnMatched.Add(_notRequiredAndHasNoDefaultValue.Object);
					expectedUnMatched.Add(_notRequiredButHasDefaultValue.Object);
					expectedUnMatched.Add(_required.Object);
				};

			Because of = () => CatchAnyError(() => result = sut.Parse(args));

			It should_list_them_all_in_the_results = () => result.UnMatchedOptions.ShouldContainOnly(expectedUnMatched);
			
			It should_bind_the_default_value_if_setup = () => _notRequiredButHasDefaultValue.Verify(x => x.BindDefault(), Times.Once());
			
			It should_not_bind_the_default_value_if_not_setup = () => _notRequiredAndHasNoDefaultValue.Verify(x => x.BindDefault(), Times.Never());
			
			It should_not_throw_an_error = () => error.ShouldBeNull();
			
			It should_list_them_as_errors_if_they_are_required = () => result.Errors.Single().Option.ShouldEqual(_required.Object);
		}
	}
}
