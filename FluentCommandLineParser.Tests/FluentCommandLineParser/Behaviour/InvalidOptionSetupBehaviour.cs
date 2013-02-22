using System;
using Machine.Specifications;

namespace Fclp.Tests.FluentCommandLineParser
{
    namespace Behaviour
    {
        [Behaviors]
        public class InvalidOptionSetupBehaviour
        {
            protected static Fclp.FluentCommandLineParser sut;
            protected static Exception error;

            It should_throw_an_error = () => error.ShouldNotBeNull();
            It should_throw_an_ArgumentOutOfRangeException = () => error.ShouldBeOfType(typeof(ArgumentOutOfRangeException));
            It should_not_have_setup_an_option = () => sut.Options.ShouldBeEmpty();
        }
    }
}
