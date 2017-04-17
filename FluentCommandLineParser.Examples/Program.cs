using System.Collections.Generic;

namespace Fclp.Examples
{
    class CommandProgram
    {
        // Contains all arguments for the add command
        class AddArgs
        {
            public bool Verbose { get; set; }
            public bool IgnoreErrors { get; set; }
            public List<string> Files { get; set; }
            public List<string> Files2 { get; set; }
        }

        // Contains all arguments for the remove command
        class RemoveArgs
        {
            public bool Verbose { get; set; }
            public List<string> Files { get; set; }
        }

        static void Main(string[] args)
        {
            var fclp = new FluentCommandLineParser();

            // use new SetupCommand method to initialise a command
            var addCmd = fclp.SetupCommand<AddArgs>("add")
                             .OnSuccess(Add); // executed when the add command is used

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
                  .WithDescription("Files to be tracked")
                  .UseForOrphanArguments();

            // add the remove command
            var remCmd = fclp.SetupCommand<RemoveArgs>("rem")
                             .OnSuccess(Remove); // executed when the remove command is used

            remCmd.Setup(removeArgs => removeArgs.Verbose)
                     .As('v', "verbose")
                     .SetDefault(false)
                     .WithDescription("Be verbose");

            remCmd.Setup(removeArgs => removeArgs.Files)
                     .As('f', "files")
                     .WithDescription("Files to be untracked")
                     .UseForOrphanArguments();

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
