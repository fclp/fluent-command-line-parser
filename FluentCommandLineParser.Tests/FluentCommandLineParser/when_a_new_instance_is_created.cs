#region License
// when_a_new_instance_is_created.cs
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
using Fclp.Internals.Parsing;
using Fclp.Tests.FluentCommandLineParser.TestContext;
using Machine.Specifications;

namespace Fclp.Tests
{
	namespace FluentCommandLineParser
	{
		public class when_a_new_instance_is_created : FluentCommandLineParserTestContext
		{
			It should_create_a_default_parser_engine = () => sut.ParserEngine.ShouldBeOfType(typeof(CommandLineParserEngineMark2));
			It should_create_a_default_option_factory = () => sut.OptionFactory.ShouldBeOfType(typeof(CommandLineOptionFactory));
			It should_set_the_string_comparison_to_current_culture = () => sut.StringComparison.ShouldEqual(System.StringComparison.CurrentCulture);
			It should_have_setup_no_options_internally = () => sut.Options.ShouldBeEmpty();
			It should_have_a_default_option_formatter = () => sut.OptionFormatter.ShouldBeOfType(typeof(CommandLineOptionFormatter));
		}
	}
}
