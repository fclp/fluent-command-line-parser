using System;
using System.Collections.Generic;
using Fclp.Internals.Parsers;

namespace Fclp.Internals
{
    /// <summary>
    /// 
    /// </summary>
    public class CommandLineOptionParserFactory : ICommandLineOptionParserFactory
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="CommandLineOptionParserFactory"/> class.
        /// </summary>
        public CommandLineOptionParserFactory()
        {
            this.Parsers = new Dictionary<Type, object>();
            this.AddOrReplace(new BoolCommandLineOptionParser());
            this.AddOrReplace(new Int32CommandLineOptionParser());
            this.AddOrReplace(new StringCommandLineOptionParser());
            this.AddOrReplace(new DateTimeCommandLineOptionParser());
            this.AddOrReplace(new DoubleCommandLineOptionParser());
        }

        internal Dictionary<Type, object> Parsers { get; set; }

        /// <summary>
        /// Adds the specified <see cref="ICommandLineOptionParser{T}"/> to this factories list of supported parsers.
        /// If an existing parser has already been registered for the type then it will be replaced.
        /// </summary>
        /// <typeparam name="T">The type which the <see cref="ICommandLineOptionParser{T}"/> will be returned for.</typeparam>
        /// <param name="parser">The parser to return for the specified type.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="parser"/> is <c>null</c>.</exception>
        public void AddOrReplace<T>(ICommandLineOptionParser<T> parser)
        {
            if (parser == null) throw new ArgumentNullException("parser");

            var parserType = typeof (T); 

            // remove existing
            this.Parsers.Remove(parserType);

            this.Parsers.Add(parserType, parser);
        }

        /// <summary>
        /// Creates a <see cref="ICommandLineOptionParser{T}"/> to handle the specified type.
        /// </summary>
        /// <typeparam name="T">The type of parser to create.</typeparam>
        /// <returns>A <see cref="ICommandLineOptionParser{T}"/> suitable for the specified type.</returns>
        /// <exception cref="UnsupportedTypeException">If the specified type is not supported by this factory.</exception>
        public ICommandLineOptionParser<T> CreateParser<T>()
        {
            var type = typeof(T);

            if (!this.Parsers.ContainsKey(type)) throw new UnsupportedTypeException();

            return (ICommandLineOptionParser<T>)this.Parsers[type];
        }
    }
}