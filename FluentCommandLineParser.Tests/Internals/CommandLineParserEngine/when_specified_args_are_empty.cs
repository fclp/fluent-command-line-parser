using Machine.Specifications;

namespace Fclp.Tests
{
    namespace CommandLineParserEngine
    {
        public class when_specified_args_are_empty : CommandLineParserEngineTestContext
        {
            Establish context = () => args = new string[0];

            Because of = () => RunParserWith(args);

            Behaves_like<NoResultsBehaviour> there_are_no_keys_found;
        }
    }
}