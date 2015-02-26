#region License
// FluentCommandLineParserTestContext.cs
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
using Machine.Specifications;
using Moq;

namespace Fclp.Tests.FluentCommandLineParser
{
	namespace TestContext
	{
		[Subject(typeof(Fclp.FluentCommandLineParser), "Unit Tests")]
		public abstract class FluentCommandLineParserTestContext : TestContext<Fclp.FluentCommandLineParser>
		{
			private Establish context = () => { sut = new Fclp.FluentCommandLineParser(); };

			protected static void AutoMockAll()
			{
				AutoMockEngineParser();
				AutoMockOptionFactory();
			}

			protected static void AutoMockEngineParser()
			{
				sut.ParserEngine = Mock.Of<ICommandLineParserEngine>();
			}

			protected static void AutoMockOptionFactory()
			{
				var mock = new Mock<ICommandLineOptionFactory>();
				var mockOption = new Mock<ICommandLineOptionResult<TestType>>();

				mock.Setup(x => x.CreateOption<TestType>(Moq.It.IsAny<string>(), Moq.It.IsAny<string>()))
					.Returns(mockOption.Object)
					.Callback<string, string>((s, l) =>
								{
									mockOption.SetupGet(x => x.ShortName).Returns(s);
									mockOption.SetupGet(x => x.LongName).Returns(l);
								});


				sut.OptionFactory = mock.Object;
			}
		}
	}
}