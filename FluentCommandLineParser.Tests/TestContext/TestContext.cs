using System;
using System.Linq;
using Machine.Specifications;
using System.Collections.Generic;
using System.Globalization;

namespace Fclp.Tests
{
    public abstract class TestContext<T>
    {
        protected static T sut;
        protected static Exception error;

        protected static void CatchAnyError(Action test)
        {
            error = Catch.Exception(test);
        }

        protected static string[] CreateArgsFromKvp(IEnumerable<KeyValuePair<string,string>> kvps)
        {
            return kvps.Select(kvp => string.Format(CultureInfo.InvariantCulture, "/{0}:{1}", kvp.Key, kvp.Value)).ToArray();
        }
    }
}
