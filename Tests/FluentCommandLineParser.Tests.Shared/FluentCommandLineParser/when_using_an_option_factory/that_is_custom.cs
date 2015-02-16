#region License
// that_is_custom.cs
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
using Fclp.Tests.FluentCommandLineParser.TestContext;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Fclp.Tests.FluentCommandLineParser
{
	namespace when_using_an_option_factory
	{
		public class that_is_custom : SettingUpALongOptionTestContext
		{
		    private const string valid_short_name_custom_factory = "s";
		    static ICommandLineOptionFactory customOptionFactory { get { return mockedOptionFactory.Object; } }
			static Mock<ICommandLineOptionFactory> mockedOptionFactory;

			Establish context = () =>
			{
				sut = new Fclp.FluentCommandLineParser();
				mockedOptionFactory = new Mock<ICommandLineOptionFactory>();

				mockedOptionFactory
                    .Setup(x => x.CreateOption<TestType>(valid_short_name_custom_factory, valid_long_name))
					.Verifiable();
			};

			Because of = () =>
			{
				sut.OptionFactory = customOptionFactory;
				SetupOptionWith(valid_short_name, valid_long_name);
			};

			It should_replace_the_old_factory =
				() => sut.OptionFactory.ShouldBeTheSameAs(customOptionFactory);

			It should_be_used_to_create_the_options_objects =
                () => mockedOptionFactory.Verify(x => x.CreateOption<TestType>(valid_short_name_custom_factory, valid_long_name));
		}
	}
}
