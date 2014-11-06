#region License
// when_specified_args_contain_no_keys.cs
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

using System.Globalization;
using System.Linq;
using Machine.Specifications;

namespace Fclp.Tests
{
	namespace CommandLineParserEngine
	{
		public class when_specified_args_contain_no_keys : CommandLineParserEngineTestContext
		{
			Establish context = () =>
											{
												args = new[]
															{
																@"abcdefghijklmnopqrstuvwxyz0123456789!£$%^&*()_+- @~#\/|¬",
																@"abcdefghijklmnopqrstuvwxyz0123456789!£$%^&*()_+- @~#\/|¬",
																@"abcdefghijklmnopqrstuvwxyz0123456789!£$%^&*()_+- @~#\/|¬",
																@"abcdefghijklmnopqrstuvwxyz0123456789!£$%^&*()_+- @~#\/|¬"
															};
											};
		
			Because of = () => 
				RunParserWith(args);

			Behaves_like<NoResultsBehaviour> there_are_no_keys_found;
		}

		public class when_specified_args_contains_keys_but_no_values : CommandLineParserEngineTestContext
		{
			static string key;
			Establish context = () =>
									{
										key = "s";
										string arg = string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", '-', key, '=');
										args = new[] { arg };
									};
			Because of = () => RunParserWith(args);

			It should_contain_only_one_result = () => results.Count().ShouldEqual(1);
			It should_return_a_result_with_the_correct_key = () => results.Single().Key.ShouldEqual(key);
			It should_return_a_result_with_no = () => results.Single().Value.ShouldBeNull();
		}
	}

}