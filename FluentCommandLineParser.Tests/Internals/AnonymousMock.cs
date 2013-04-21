#region License
// AnonymousMock.cs
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

using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace Fclp.Tests.Internals
{
	/// <summary>
	/// http://pol84.tumblr.com/post/23032897078/autofixture-and-interfaces
	/// </summary>
	public static class AnonymousMock
	{
		public static Mock<T> CreateMock<T>(
			this ISpecimenBuilder composer) where T : class
		{
			return new Factory<T>(composer).CreateAnonymousMock();
		}

		private class Factory<T> where T : class
		{
			private readonly ISpecimenBuilder composer;
			private Mock<T> mock;
			private MethodInfo setupGet;
			private PropertyInfo property;
			private object returnsGetter;
			private MethodInfo returns;

			internal Factory(ISpecimenBuilder composer)
			{
				this.composer = composer;
			}

			internal Mock<T> CreateAnonymousMock()
			{
				mock = new Mock<T>();
				setupGet = mock.GetType().GetMethod("SetupGet");
				typeof(T).GetProperties()
				         .Where(p => p.CanRead)
				         .ToList()
				         .ForEach(p => { property = p; SetupGet(); });
				return mock;
			}

			private void SetupGet()
			{
				var genericSetupGet = setupGet
					.MakeGenericMethod(property.PropertyType);
				var expression = GetPropertyLambdaExpression();
				returnsGetter = genericSetupGet
					.Invoke(mock, new object[] { expression });
				UseReturnsGetter();
			}

			private void UseReturnsGetter()
			{
				returns = returnsGetter.GetType().GetMethods()
				                       .Single(m => m.Name == "Returns" && m.GetParameters()
				                                                            .Any(p => p.ParameterType == property.PropertyType));
				Returns();
			}

			private void Returns()
			{
				var propertyValue = GetValueFromFixture();
				returns.Invoke(returnsGetter, new object[] { propertyValue });
			}

			private LambdaExpression GetPropertyLambdaExpression()
			{
				var typeParameter = Expression.Parameter(typeof(T));
				var propertyExpression = Expression
					.Property(typeParameter, property);
				return Expression.Lambda(propertyExpression, typeParameter);
			}

			private object GetValueFromFixture()
			{
				var createAnonymous = typeof(SpecimenFactory).GetMethods()
				                                             .Single(m => m.Name == "Create"
				                                                          && m.GetParameters().All(p => p.ParameterType
				                                                                                        == typeof(ISpecimenBuilder)));
				var genericCreateAnonymous = createAnonymous
					.MakeGenericMethod(property.PropertyType);
				return genericCreateAnonymous
					.Invoke(null, new object[] { composer });
			}
		}
	}
}