using System.Collections.Generic;
using System.Linq;
using Fclp.Internals;
using Fclp.Tests.FluentCommandLineParser.TestContext;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Fclp.Tests.FluentCommandLineParser
{
    namespace when_executing_parse_operation
    {
        class with_options_that_are_not_specified_in_the_args : FluentCommandLineParserTestContext
        {
            static ICommandLineParserResult result;
            static List<ICommandLineOption> expectedUnMatched = new List<ICommandLineOption>();
            static string[] args = null;
            static Mock<ICommandLineOption> _notRequiredButHasDefaultValue = new Mock<ICommandLineOption>();
            static Mock<ICommandLineOption> _notRequiredAndHasNoDefaultValue = new Mock<ICommandLineOption>();
            static Mock<ICommandLineOption> _required = new Mock<ICommandLineOption>();

            Establish context = () =>
                {
                    // create item that won't be matched an has no default value
                    _notRequiredAndHasNoDefaultValue.SetupGet(x => x.HasDefault).Returns(false);
                    _notRequiredAndHasNoDefaultValue.SetupGet(x => x.ShortName).Returns("notRequiredAndHasNoDefaultValue");
                    _notRequiredAndHasNoDefaultValue.Setup(x => x.BindDefault()).Verifiable();
                    sut.Options.Add(_notRequiredAndHasNoDefaultValue.Object);
                    
                    // create item that won't be matched but is required
                    _required.SetupGet(x => x.IsRequired).Returns(true);
                    _required.SetupGet(x => x.ShortName).Returns("required");
                    sut.Options.Add(_required.Object);
                    
                    // create item that isn't required but has a default value
                    _notRequiredButHasDefaultValue.SetupGet(x => x.HasDefault).Returns(true);
                    _notRequiredButHasDefaultValue.SetupGet(x => x.ShortName).Returns("notRequiredButHasDefaultValue");
                    _notRequiredButHasDefaultValue.Setup(x => x.BindDefault()).Verifiable();
                    sut.Options.Add(_notRequiredButHasDefaultValue.Object);

                    // these will be unmatched
                    expectedUnMatched.Add(_notRequiredAndHasNoDefaultValue.Object);
                    expectedUnMatched.Add(_notRequiredButHasDefaultValue.Object);
                    expectedUnMatched.Add(_required.Object);
                };

            Because of = () => CatchAnyError(() => result = sut.Parse(args));

            It should_list_them_all_in_the_results = () => result.UnMatchedOptions.ShouldContainOnly(expectedUnMatched);
            
            It should_bind_the_default_value_if_setup = () => _notRequiredButHasDefaultValue.Verify(x => x.BindDefault(), Times.Once());
            
            It should_not_bind_the_default_value_if_not_setup = () => _notRequiredAndHasNoDefaultValue.Verify(x => x.BindDefault(), Times.Never());
            
            It should_not_throw_an_error = () => error.ShouldBeNull();
            
            It should_list_them_as_errors_if_they_are_required = () => result.Errors.Single().Option.ShouldEqual(_required.Object);
        }
    }
}
