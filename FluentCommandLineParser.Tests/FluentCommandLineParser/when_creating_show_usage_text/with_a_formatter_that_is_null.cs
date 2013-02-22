using System;
using Machine.Specifications;

namespace Fclp.Tests.FluentCommandLineParser
{
    namespace when_creating_show_usage_text
    {
        class with_a_formatter_that_is_null : TestContext<Fclp.FluentCommandLineParser>
        {
            Establish context = () => sut = new Fclp.FluentCommandLineParser();
            
            Because of = () => CatchAnyError(() => sut.CreateShowUsageText(null));
            
            It should_throw_an_error = () => error.ShouldBeOfType(typeof(ArgumentNullException));
        }
    }
}
