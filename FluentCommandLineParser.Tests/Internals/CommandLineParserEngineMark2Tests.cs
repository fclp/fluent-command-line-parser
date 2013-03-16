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

using Fclp.Internals;
using Fclp.Tests.TestContext;
using Machine.Specifications;

namespace Fclp.Tests.Internals
{
    abstract class CommandLineParserEngineMark2TestContext : TestContextBase<CommandLineParserEngineMark2>
    {
        Establish context = () => CreatSut();
    }

    sealed class Parse
    {
        abstract class ParseTestContext : CommandLineParserEngineMark2TestContext
        {
            protected static string[] args;
            protected static ParserEngineResult result;

            protected static void SetupArgs(string arguments)
            {
                args = TestHelpers.ParseArguments(arguments);
            }

            Because of = () =>
                result = sut.Parse(args);
        }

        class when_args_is_null : ParseTestContext
        {
            Establish context = () => args = null;
            
            It should_return_a_result_with_no_parsed_options = () =>
                result.ParsedOptions.ShouldBeEmpty();

            It should_return_a_result_with_no_additional_values = () =>
                result.AdditionalValues.ShouldBeEmpty();
        }

        class when_ : ParseTestContext
        {
            Establish context = () => SetupArgs("");
        }
    }
    
}