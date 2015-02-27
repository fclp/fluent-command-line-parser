using System.Collections.Generic;

namespace Fclp.Tests
{
    public class CommandTests
    {
        // Contains all arguments for the add command
        class AddArgs
        {
            public bool Verbose;
            public bool IgnoreErrors;
            public IEnumerable<string> Files;
        }

        // Contains all arguments for the remove command
        class RemoveArgs
        {
            public bool Verbose;
            public IEnumerable<string> Files;
        }

        // entry point into console app
        static void Main(string[] args)
        {
            var fclp = new Fclp.FluentCommandLineParser();

            // use new SetupCommand method to initialise a command
            var addCmd = fclp.SetupCommand<AddArgs>("add")
                             .Callback(Add); // executed when the add command is used

            // the standard fclp framework, except against the created command rather than the fclp itself
            addCmd.Setup(addArgs => addArgs.Verbose)
                  .As('v', "verbose")
                  .SetDefault(false)
                  .WithDescription("Be verbose");

            addCmd.Setup(addArgs => addArgs.IgnoreErrors)
                  .As("ignore-errors")
                  .SetDefault(false)
                  .WithDescription("If some files could not be added, do not abort");

            addCmd.Setup(addArgs => addArgs.Files)
                  .As('f', "files")
                  .WithDescription("Files to be tracked");

            // add the remove command
            var remCmd = fclp.SetupCommand<RemoveArgs>("rem")
                             .Callback(Remove); // executed when the remove command is used

            remCmd.Setup(removeArgs => removeArgs.Verbose)
                     .As('v', "verbose")
                     .SetDefault(false)
                     .WithDescription("Be verbose");

            remCmd.Setup(removeArgs => removeArgs.Files)
                     .As('f', "files")
                     .WithDescription("Files to be untracked");

            fclp.Parse(args);
        }

        static void Add(AddArgs args)
        {
            // add was executed
        }

        static void Remove(RemoveArgs args)
        {
            // remove was executed
        }
    }
}
