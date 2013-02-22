using Machine.Specifications;
using Fclp.Tests.CommandLineParserEngine;
using System.Linq;
using System.Collections.Generic;

namespace Fclp.Tests.Internals.CommandLineParserEngine
{
    class when_arg_contains_key_and_value_assignment_but_no_value : CommandLineParserEngineTestContext
    {
        static KeyValuePair<string, string> key = new KeyValuePair<string, string>("key", null);
        static KeyValuePair<string, string> key2 = new KeyValuePair<string, string>("key2", "key2value");

        Establish context = () => args = new[] { "/key=", "/key2", "key2value" };
        Because of = () => RunParserWith(args);
        It should_return_key_with_null_value = () => results.ShouldContain(key);
        It should_return_key2_with_expected_value = () => results.ShouldContain(key2);
    }
}
