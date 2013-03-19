#region License
// with_a_formatter_that_is_custom.cs
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

using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Fclp.Tests.FluentCommandLineParser
{
	namespace when_creating_show_usage_text
	{
		class with_a_formatter_that_is_custom : TestContext<Fclp.FluentCommandLineParser>
		{
			static Mock<ICommandLineOptionFormatter> mockOptionFormatter;
			static ICommandLineOptionFormatter customOptionFormatter { get { return mockOptionFormatter.Object; } }
			static string result;
			static string expectedResult;

			Establish context = () =>
									{
										sut = new Fclp.FluentCommandLineParser();

										expectedResult = "my expected results";

										mockOptionFormatter = new Mock<ICommandLineOptionFormatter>();
										mockOptionFormatter
											.Setup(x => x.Format(sut.Options))
											.Returns(expectedResult)
											.Verifiable();
									};

			Because of = () => CatchAnyError(() => result = sut.CreateShowUsageText(customOptionFormatter));

			It should_not_throw_an_error = () => error.ShouldBeNull();
			It should_call_it_with_all_the_options = () => mockOptionFormatter.Verify(x => x.Format(sut.Options));
			It should_return_the_results_obtained_from_it = () => expectedResult.ShouldEqual(result);
		}
	}
}