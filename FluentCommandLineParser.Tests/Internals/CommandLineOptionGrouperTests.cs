﻿#region License
// CommandLineOptionGrouperTests.cs
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

using Fclp.Internals.Parsing;
using FluentCommandLineParser.Internals.Parsing;
using Machine.Specifications;

namespace Fclp.Tests.Internals
{
	public class CommandLineOptionGrouperTests
	{
		[Subject(typeof(CommandLineOptionGrouper))]
		abstract class CommandLineOptionGrouperTestContext : TestContextBase<CommandLineOptionGrouper>
		{
			Establish context = () =>
				CreateSut();
		}

		abstract class GroupByOptionTestContext : CommandLineOptionGrouperTestContext
		{
            protected static GroupArgumentsByOptionResult actualResult;
			protected static string[] args;

			Because of = () =>
				error = Catch.Exception(() =>
					actualResult = sut.GroupArgumentsByOption(args));
		}

		class when_double_dashes_are_used_to_terminate_option_parsing : GroupByOptionTestContext
		{
			Establish context = () =>
			{
				args = new []
				{
					"-A", "1", "2", "3",
					"-B", "a", "b", "c",
					"-C", "--", "-a", "-1", "-b"
				};
			};

			It should_return_three_sets = () =>
				actualResult.MatchedArgumentsOptionGroups.Length.ShouldEqual(3);

			It should_group_the_A_elements = () =>
                actualResult.MatchedArgumentsOptionGroups[0].ShouldContainOnly("-A", "1", "2", "3"); 

			It should_group_the_B_elements = () =>
                actualResult.MatchedArgumentsOptionGroups[1].ShouldContainOnly("-B", "a", "b", "c"); 

			It should_group_the_C_elements = () =>
                actualResult.MatchedArgumentsOptionGroups[2].ShouldContainOnly("-C", "--", "-a", "-1", "-b"); 
		}

		class when_double_dashes_are_used_immediately_to_terminate_option_parsing : GroupByOptionTestContext
		{
			Establish context = () =>
				args = new []
				{
					"--", "-A", "1", "2", "3",
					"-B", "a", "b", "c",
					"-C", "-a", "-1", "-b"
				};

			It should_return_a_single_set = () =>
                actualResult.MatchedArgumentsOptionGroups.Length.ShouldEqual(1);

			It should_group_the_A_elements = () =>
                actualResult.MatchedArgumentsOptionGroups[0].ShouldContainOnly("--", "-A", "1", "2", "3", "-B", "a", "b", "c", "-C", "-a", "-1", "-b"); 
		}

		class when_there_are_only_arguments_and_no_options : GroupByOptionTestContext
		{
			Establish context = () =>
				args = new[] { "0", "1", "2", "3" };

			It should_return_a_single_set = () =>
                actualResult.MatchedArgumentsOptionGroups.Length.ShouldEqual(1);

			It should_group_all_the_provided_elements = () =>
                actualResult.MatchedArgumentsOptionGroups[0].ShouldContainOnly("0", "1", "2", "3"); 
		}

		class when_there_is_a_double_dash_but_no_options_before_it : GroupByOptionTestContext
		{
			Establish context = () =>
				args = new[] { "--", "-A", "1", "2", "3" };

			It should_return_a_single_set = () =>
                actualResult.MatchedArgumentsOptionGroups.Length.ShouldEqual(1);

			It should_group_all_the_provided_elements = () =>
                actualResult.MatchedArgumentsOptionGroups[0].ShouldContainOnly("--", "-A", "1", "2", "3"); 
		}

		class when_there_is_a_double_dash_at_the_end : GroupByOptionTestContext
		{
			Establish context = () =>
				args = new[]
				{
					"-A", "1", "2", "3",
					"-B", "a", "b", "c",
					"-C", "a1", "b1", "c1", "--"
				};

			It should_return_three_sets = () =>
                actualResult.MatchedArgumentsOptionGroups.Length.ShouldEqual(3);

			It should_group_the_A_elements = () =>
                actualResult.MatchedArgumentsOptionGroups[0].ShouldContainOnly("-A", "1", "2", "3");

			It should_group_the_B_elements = () =>
                actualResult.MatchedArgumentsOptionGroups[1].ShouldContainOnly("-B", "a", "b", "c");

			It should_group_the_C_elements = () =>
                actualResult.MatchedArgumentsOptionGroups[2].ShouldContainOnly("-C", "a1", "b1", "c1", "--"); 
		}

		class when_options_are_repeated : GroupByOptionTestContext
		{
			Establish context = () =>
				args = new[] { "-A", "1", "2", "3", "-A", "4", "5", "6" };

			It should_return_two_sets = () =>
                actualResult.MatchedArgumentsOptionGroups.Length.ShouldEqual(2);

			It should_group_the_first_elements = () =>
                actualResult.MatchedArgumentsOptionGroups[0].ShouldContainOnly("-A", "1", "2", "3"); 

			It should_group_the_second_repeated_elements = () =>
                actualResult.MatchedArgumentsOptionGroups[1].ShouldContainOnly("-A", "4", "5", "6"); 
		}

		class when_the_args_is_empty : GroupByOptionTestContext
		{
			Establish context = () => 
				args = new string[0];

			It should_not_throw_an_error = () =>
				error.ShouldBeNull();

			It should_return_empty_args = () =>
                actualResult.MatchedArgumentsOptionGroups.ShouldBeEmpty();
		}
	}
}