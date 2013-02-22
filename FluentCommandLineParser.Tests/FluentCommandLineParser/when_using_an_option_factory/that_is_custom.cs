using Fclp.Internals;
using Fclp.Tests.FluentCommandLineParser.TestContext;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Fclp.Tests.FluentCommandLineParser
{
    namespace when_using_an_option_factory
    {
        public class that_is_custom : SettingUpALongOptionTestContext
        {
            static ICommandLineOptionFactory customOptionFactory { get { return mockedOptionFactory.Object; } }
            static Mock<ICommandLineOptionFactory> mockedOptionFactory;

            Establish context = () =>
            {
                sut = new Fclp.FluentCommandLineParser();
                mockedOptionFactory = new Mock<ICommandLineOptionFactory>();

                mockedOptionFactory
                    .Setup(x => x.CreateOption<TestType>(valid_short_name, valid_long_name))
                    .Verifiable();
            };

            Because of = () =>
            {
                sut.OptionFactory = customOptionFactory;
                SetupOptionWith(valid_short_name, valid_long_name);
            };

            It should_replace_the_old_factory =
                () => sut.OptionFactory.ShouldBeTheSameAs(customOptionFactory);

            It should_be_used_to_create_the_options_objects =
                () => mockedOptionFactory.Verify(x => x.CreateOption<TestType>(valid_short_name, valid_long_name));
        }
    }
}
