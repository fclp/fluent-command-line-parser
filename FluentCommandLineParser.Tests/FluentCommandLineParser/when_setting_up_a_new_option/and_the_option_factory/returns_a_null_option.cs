using System;
using Fclp.Internals;
using Fclp.Tests.FluentCommandLineParser.TestContext;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Fclp.Tests.FluentCommandLineParser.when_setting_up_a_new_option
{
    namespace and_the_option_factory
    {
        public class returns_a_null_option : SettingUpALongOptionTestContext
        {
            Establish context = () =>
                                    {
                                        ICommandLineOptionResult<TestType> nullOption = null;

                                        var mockOptionFactoryThatReturnsNull = new Mock<ICommandLineOptionFactory>();
                                        mockOptionFactoryThatReturnsNull
                                            .Setup(x => x.CreateOption<TestType>(valid_short_name, valid_long_name))
                                            .Returns(nullOption);

                                        sut.OptionFactory = mockOptionFactoryThatReturnsNull.Object;
                                    };

            Because of = () => SetupOptionWith(valid_short_name, valid_long_name);

            It should_throw_an_error = () => error.ShouldBeOfType(typeof(InvalidOperationException));
            It should_not_have_setup_an_option = () => sut.Options.ShouldBeEmpty();
        }
    }
}
