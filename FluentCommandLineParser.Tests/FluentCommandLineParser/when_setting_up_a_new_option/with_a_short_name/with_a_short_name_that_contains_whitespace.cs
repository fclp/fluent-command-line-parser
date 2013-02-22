using Fclp.Tests.FluentCommandLineParser.Behaviour;
using Fclp.Tests.FluentCommandLineParser.TestContext;
using Machine.Specifications;

namespace Fclp.Tests.FluentCommandLineParser
{
    namespace when_setting_up_a_new_option
    {
        public class with_a_short_name_that_contains_whitespace : SettingUpAShortOptionTestContext
        {
            Establish context = AutoMockAll;

            Because of = () => SetupOptionWith(invalid_short_name_with_spaces);

            Behaves_like<InvalidOptionSetupBehaviour> a_failed_setup_option;
        }
    }
}