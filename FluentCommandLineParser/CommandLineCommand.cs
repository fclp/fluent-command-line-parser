using System;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Fclp.Internals;

namespace Fclp
{
    class CommandLineCommand<TBuildType> : ICommandLineCommandFluent<TBuildType> where TBuildType : new()
    {
        public CommandLineCommand(IFluentCommandLineParser parser)
        {
            Parser = parser;
            Object = new TBuildType();
        }

        /// <summary>
        /// Gets the <see cref="IFluentCommandLineParser"/>.
        /// </summary>
        public IFluentCommandLineParser Parser { get; private set; }

        /// <summary>
        /// Gets the constructed object.
        /// </summary>
        public TBuildType Object { get; private set; }

        /// <summary>
        /// The callback to execute with the results of this command if used.
        /// </summary>
        public Action<TBuildType> ReturnCallback { get; set; }

        /// <summary>
        /// Gets or sets the command name
        /// </summary>
        public string Name { get; set; }

        public ICommandLineCommandFluent<TBuildType> Callback(Action<TBuildType> callback)
        {
            ReturnCallback = callback;
            return this;
        }

        public ICommandLineOptionBuilderFluent<TProperty> Setup<TProperty>(Expression<Func<TBuildType, TProperty>> propertyPicker)
        {
            return new CommandLineOptionBuilderFluent<TBuildType, TProperty>(Parser, Object, propertyPicker);
        }
    }
}