using System.Linq;

namespace Fclp.Tests.FluentCommandLineParser
{
    namespace TestContext
    {
        public abstract class SettingUpALongOptionTestContext : SettingUpAShortOptionTestContext
        {
            protected const string valid_long_name_that_is_empty = "";
            protected const string invalid_long_name_that_is_whitespace = " ";
            protected const string invalid_long_name_with_spaces = "long name";
            protected const string invalid_long_name_with_colon = "long:name";
            protected const string invalid_long_name_with_equality_sign = "long=name";
            protected const string valid_long_name = "l";

            protected static void SetupOptionWith(string shortName, string longName)
            {
                CatchAnyError(() =>
                                            {
                                                var ret = sut.Setup<TestType>(shortName, longName);
                                                option = sut.Options.SingleOrDefault(x => ReferenceEquals(x, ret));
                                            });
            }
        }
    }
}