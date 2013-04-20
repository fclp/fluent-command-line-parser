#region License
// returns_a_null_option.cs
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
using System.Globalization;
using Fclp.Internals;
using Fclp.Tests.FluentCommandLineParser.TestContext;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Fclp.Tests.FluentCommandLineParser.when_setting_up_a_new_option
{
	namespace and_the_option_factory
	{
		public class returns_a_null_option : SettingUpALongOptionTestContext
		{
			Establish context = () =>
									{
										ICommandLineOptionResult<TestType> nullOption = null;

										var mockOptionFactoryThatReturnsNull = new Mock<ICommandLineOptionFactory>();
										mockOptionFactoryThatReturnsNull
											.Setup(x => x.CreateOption<TestType>(valid_short_name.ToString(CultureInfo.InvariantCulture), valid_long_name))
											.Returns(nullOption);

										sut.OptionFactory = mockOptionFactoryThatReturnsNull.Object;
									};

			Because of = () => SetupOptionWith(valid_short_name, valid_long_name);

			It should_throw_an_error = () => error.ShouldBeOfType(typeof(InvalidOperationException));
			It should_not_have_setup_an_option = () => sut.Options.ShouldBeEmpty();
		}
	}
}
