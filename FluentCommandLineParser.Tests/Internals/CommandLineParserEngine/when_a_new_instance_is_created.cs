using Machine.Specifications;

namespace Fclp.Tests
{
    namespace CommandLineParserEngine
    {
        public class when_a_new_instance_is_created : CommandLineParserEngineTestContext
        {
            It should_construct = () => sut.ShouldNotBeNull();
        }
    }
}
