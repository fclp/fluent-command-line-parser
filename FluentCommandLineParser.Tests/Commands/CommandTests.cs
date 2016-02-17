using System;
using Fclp.Tests.Extensions;
using NUnit.Framework;

namespace Fclp.Tests.Commands
{
    [TestFixture]
    public class CommandTests
    {
        static Fclp.FluentCommandLineParser SetupParser(Action<AddArgs> addCallback, Action<RemoveArgs> removeCallback)
        {
            var fclp = new Fclp.FluentCommandLineParser();

            // use new SetupCommand method to initialise a command
            var addCmd = fclp.SetupCommand<AddArgs>("add")
                             .OnSuccess(addCallback); // executed when the add command is used

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
                             .OnSuccess(removeCallback); // executed when the remove command is used

            remCmd.Setup(removeArgs => removeArgs.Verbose)
                     .As('v', "verbose")
                     .SetDefault(false)
                     .WithDescription("Be verbose");

            remCmd.Setup(removeArgs => removeArgs.Files)
                     .As('f', "files")
                     .WithDescription("Files to be untracked")
                     .UseForOrphanArguments();

            return fclp;
        }

        [Test]
        public void Add_Command_Usage()
        {
            const string args = "add --files a.txt b.txt c.txt --verbose --ignore-errors";

            AddArgs addArgs = null;
            RemoveArgs removeArgs = null;

            var fclp = SetupParser(x => addArgs = x, x => removeArgs = x);

            var result = fclp.Parse(args.AsCmdArgs());

            Assert.IsFalse(result.HasErrors);
            Assert.IsNull(removeArgs);
            Assert.IsNotNull(addArgs);
            Assert.IsTrue(addArgs.Verbose);
            Assert.IsTrue(addArgs.IgnoreErrors);
            Assert.That(addArgs.Files, Is.EquivalentTo(new[] { "a.txt", "b.txt", "c.txt" }));
        }

        [Test]
        public void Add_Command_With_Orphan()
        {
            const string args = "add a.txt b.txt c.txt --verbose --ignore-errors";

            AddArgs addArgs = null;
            RemoveArgs removeArgs = null;

            var fclp = SetupParser(x => addArgs = x, x => removeArgs = x);

            var result = fclp.Parse(args.AsCmdArgs());

            Assert.IsFalse(result.HasErrors);
            Assert.IsNull(removeArgs);
            Assert.IsNotNull(addArgs);
            Assert.IsTrue(addArgs.Verbose);
            Assert.IsTrue(addArgs.IgnoreErrors);
            Assert.IsNotNull(addArgs.Files);
            Assert.That(addArgs.Files, Is.EquivalentTo(new[] { "a.txt", "b.txt", "c.txt" }));
        }

        [Test]
        public void Add_Command_With_Only_Orphan()
        {
            const string args = "add a.txt b.txt c.txt";

            AddArgs addArgs = null;
            RemoveArgs removeArgs = null;

            var fclp = SetupParser(x => addArgs = x, x => removeArgs = x);

            var result = fclp.Parse(args.AsCmdArgs());

            Assert.IsFalse(result.HasErrors);
            Assert.IsNull(removeArgs);
            Assert.IsNotNull(addArgs);
            Assert.IsFalse(addArgs.Verbose);
            Assert.IsFalse(addArgs.IgnoreErrors);
            Assert.IsNotNull(addArgs.Files);
            Assert.That(addArgs.Files, Is.EquivalentTo(new[] { "a.txt", "b.txt", "c.txt" }));
        }

        [Test]
        public void Remove_Command_Usage()
        {
            const string args = "rem --files a.txt b.txt c.txt --verbose";

            AddArgs addArgs = null;
            RemoveArgs removeArgs = null;

            var fclp = SetupParser(x => addArgs = x, x => removeArgs = x);

            var result = fclp.Parse(args.AsCmdArgs());

            Assert.IsFalse(result.HasErrors);
            Assert.IsNull(addArgs);
            Assert.IsNotNull(removeArgs);
            Assert.IsTrue(removeArgs.Verbose);
            Assert.IsNotNull(removeArgs.Files);
            Assert.That(removeArgs.Files, Is.EquivalentTo(new[] { "a.txt", "b.txt", "c.txt" }));
        }
    }
}
