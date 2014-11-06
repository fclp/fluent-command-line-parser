#region License
// TestContextBase.cs
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
using System.Collections.Generic;
using System.Linq;
using Fclp.Internals.Extensions;
using Machine.Specifications;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Fclp.Tests.Internals
{
	public abstract class TestContextBase<TSut> where TSut : class
	{
		protected static TSut sut;
		protected static IFixture fixture;
		protected static Exception error;

		Establish context = () =>
		{
			InitialiseFixture();
		};

		protected static IFixture InitialiseFixture()
		{
			fixture = new Fixture().Customize(new AutoMoqCustomization());
			return fixture;
		}

		protected static TSut CreateSut()
		{
			sut = fixture.Create<TSut>();
			return sut;
		}

		protected Mock<TType> InitialiseMock<TType>(out Mock<TType> mockObj) where TType : class
		{
			mockObj = fixture.Freeze<Mock<TType>>();
			return mockObj;
		}

		protected static string[] ParseArguments(string args)
		{
			args = ReplaceWithDoubleQuotes(args);
			return args.SplitOnWhitespace().ToArray();
		}

		protected static string ReplaceWithDoubleQuotes(string args)
		{
			if (args == null) return null;
			return args.Replace('\'', '"');
		}

		protected static void FreezeMock<TType>(out Mock<TType> obj) where TType : class
		{
			obj = fixture.Freeze<Mock<TType>>();
		}

		protected static void Create<TType>(out TType obj)
		{
			obj = Create<TType>();
		}

		protected static TType Create<TType>()
		{
			return fixture.Create<TType>();
		}

		protected static void CreateMock<TType>(out Mock<TType> obj) where TType : class
		{
			obj = CreateMock<TType>();
		}

		protected static Mock<TType> CreateMock<TType>() where TType : class
		{
			return fixture.CreateMock<TType>();
		}

		protected static List<TType> CreateEmptyList<TType>()
		{
			return new List<TType>();
		}

		protected static void CreateEmptyList<TType>(out List<TType> list)
		{
			list = CreateEmptyList<TType>();
		}

		protected static List<TType> CreateManyAsList<TType>(params TType[] additionalItemsToAdd)
		{
			var list = fixture.CreateMany<TType>(5).ToList();
			list.AddRange(additionalItemsToAdd);
			return list;
		}

		protected static string CreateStringOfLength(int length)
		{
			var str = Create<string>();
			return new string(str.Take(length).ToArray());
		}
	}
}