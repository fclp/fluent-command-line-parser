#region License
// with_options_that_have_not_been_setup.cs
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
using Fclp.Tests.FluentCommandLineParser.TestContext;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Fclp.Tests.FluentCommandLineParser
{
    namespace when_executing_parse_operation
    {
        class with_options_that_have_not_been_setup : FluentCommandLineParserTestContext
        {
            static string[] args;
            static ICommandLineParserResult result;
            static IEnumerable<KeyValuePair<string, string>> additionalOptions;

            Establish context = () =>
            {
                additionalOptions = new Dictionary<string, string>
                {
                    {"arg1", "1"},
                    {"arg2", "2"},
                    {"arg3", "3"},
                };

                args = CreateArgsFromKvp(additionalOptions);

                var mockEngine = new Mock<Fclp.Internals.ICommandLineParserEngine>();
                mockEngine.Setup(x => x.Parse(args)).Returns(additionalOptions);
                sut.ParserEngine = mockEngine.Object;
            };

            Because of = () => CatchAnyError(() => result = sut.Parse(args));

            It should_not_throw_any_errors = () => error.ShouldBeNull();
            It should_return_a_result = () => result.ShouldNotBeNull();
            It should_return_them_as_additional = () => result.AdditionalOptionsFound.ShouldContainOnly(additionalOptions);
        }
    }
}
