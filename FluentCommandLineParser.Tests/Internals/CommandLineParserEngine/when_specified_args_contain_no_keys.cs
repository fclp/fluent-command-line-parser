using System.Globalization;
using System.Linq;
using Machine.Specifications;

namespace Fclp.Tests
{
    namespace CommandLineParserEngine
    {
        public class when_specified_args_contain_no_keys : CommandLineParserEngineTestContext
        {
            Establish context = () =>
                                            {
                                                args = new[]
                                                           {
                                                               @"abcdefghijklmnopqrstuvwxyz0123456789!£$%^&*()_+- @~#\/|¬",
                                                               @"abcdefghijklmnopqrstuvwxyz0123456789!£$%^&*()_+- @~#\/|¬",
                                                               @"abcdefghijklmnopqrstuvwxyz0123456789!£$%^&*()_+- @~#\/|¬",
                                                               @"abcdefghijklmnopqrstuvwxyz0123456789!£$%^&*()_+- @~#\/|¬"
                                                           };
                                            };
        
            Because of = () => RunParserWith(args);

            Behaves_like<NoResultsBehaviour> there_are_no_keys_found;
        }

        public class when_specified_args_contains_keys_but_no_values : CommandLineParserEngineTestContext
        {
            static string key;
            Establish context = () =>
                                    {
                                        key = "s";
                                        string arg = string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", '-', key, '=');
                                        args = new[] { arg };
                                    };
            Because of = () => RunParserWith(args);

            It should_contain_only_one_result = () => results.Count().ShouldEqual(1);
            It should_return_a_result_with_the_correct_key = () => results.Single().Key.ShouldEqual(key);
            It should_return_a_result_with_no = () => results.Single().Value.ShouldBeNull();
        }
    }

}