using Machine.Specifications;

namespace Fclp.Tests
{
    namespace CommandLineParserEngine
    {
        public class when_specified_args_are_null : CommandLineParserEngineTestContext
        {
            Establish context = () => args = null;

            Because of = () => RunParserWith(args);

            Behaves_like<NoResultsBehaviour> there_are_no_keys_found;
        }
    }
}