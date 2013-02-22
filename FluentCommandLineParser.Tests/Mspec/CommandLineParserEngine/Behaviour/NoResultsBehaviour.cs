using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace FluentCommandLineParser.Tests
{
    namespace CommandLineParserEngineTests
    {
        [Behaviors]
        public class NoResultsBehaviour
        {
            protected static Exception error;
            protected static IEnumerable<KeyValuePair<string, string>> results;

            It should_not_error = () => error.ShouldBeNull();
            It should_return_no_found_values = () => results.ShouldBeEmpty();
        }
    }
}
