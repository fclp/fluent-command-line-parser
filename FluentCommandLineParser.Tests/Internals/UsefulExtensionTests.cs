using Fclp.Internals.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fclp.Tests.Internals
{
	/// <summary>
	/// option value that has double quote at the end
	/// </summary>
	[TestFixture]
	public class when_there_is_qoute_in_the_end
	{
		[Test]
		public void parser() // not sure that it should be here
		{
			var args = new[] { "--param", "something \"4\"" };

			var sut = new Fclp.FluentCommandLineParser<Config>();
			sut.Setup(_ => _.Param).As('p', "param");
			var res = sut.Parse(args);

			Assert.AreEqual("something \"4\"", sut.Object.Param);
		}

		[Test]
		public void RemoveAnyWrappingDoubleQuotes()
		{
			var str = "something \"4\"";
			str = str.WrapInDoubleQuotes();
			str = str.RemoveAnyWrappingDoubleQuotes();
			Assert.AreEqual("something \"4\"", str);
		}
	}

	public class Config
	{
		public string Param { get; set; }
	}
}
