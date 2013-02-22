using System.Linq;
using Fclp.Internals;
using Machine.Specifications;

namespace Fclp.Tests.FluentCommandLineParser
{
    namespace TestContext
    {
        [Subject(Subjects.setup_new_option)]
        public abstract class SettingUpAShortOptionTestContext : FluentCommandLineParserTestContext
        {
            protected const string invalid_short_name_that_is_empty = "";
            protected const string invalid_short_name_that_is_whitespace = " ";
            protected const string invalid_short_name_with_spaces = "short name";
            protected const string invalid_short_name_with_colon = "short:name";
            protected const string invalid_short_name_with_equality_sign = "short=name";
            protected const string valid_short_name = "s";

            protected static ICommandLineOption option;

            protected static void SetupOptionWith(string shortName)
            {
                CatchAnyError(() =>
                {
                    var ret = sut.Setup<TestType>(shortName);
                    option = sut.Options.SingleOrDefault(x => ReferenceEquals(x, ret));
                });
            }
        }
    }
}