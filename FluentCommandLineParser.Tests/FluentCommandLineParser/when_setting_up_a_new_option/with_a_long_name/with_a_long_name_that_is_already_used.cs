using Fclp.Internals;
using Fclp.Tests.FluentCommandLineParser.TestContext;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Fclp.Tests.FluentCommandLineParser
{
    namespace when_setting_up_a_new_option
    {
        public class with_a_long_name_that_is_already_used : SettingUpALongOptionTestContext
        {
            private const string existingLongName = "longName";
            private static ICommandLineOption existingOption;

            Establish context = () =>
                                    {
                                        AutoMockAll();

                                        var option = new Mock<ICommandLineOption>();
                                        option.SetupGet(x => x.ShortName).Returns("randomshortname");
                                        option.SetupGet(x => x.LongName).Returns(existingLongName);
                                        existingOption = option.Object;
                                    };

            Because of = () =>
                             {
                                 sut.Options.Add(existingOption);
                                 SetupOptionWith(valid_short_name, existingLongName);
                             };

            It should_throw_an_error = () => error.ShouldBeOfType(typeof(OptionAlreadyExistsException));
            It should_not_have_setup_an_option = () => sut.Options.ShouldContainOnly(existingOption);
        }
    }
}
