#region License
// FluentCommandLineParserT.cs
// Copyright (c) 2014, Simon Williams
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification, are permitted provide
// d that the following conditions are met:
// 
// Redistributions of source code must retain the above copyright notice, this list of conditions and the
// following disclaimer.
// 
// Redistributions in binary form must reproduce the above copyright notice, this list of conditions and
// the following disclaimer in the documentation and/or other materials provided with the distribution.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED 
// WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
// PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
// TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
// POSSIBILITY OF SUCH DAMAGE.
#endregion

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Fclp.Internals;

namespace Fclp
{
    /// <summary>
    /// A command line parser which provides methods and properties 
    /// to easily and fluently parse command line arguments into
    /// a predefined arguments object.
    /// </summary>
	/// <typeparam name="TBuildType">The object type containing the argument properties to populate from parsed command line arguments.</typeparam>
	public class FluentCommandLineParser<TBuildType> : IFluentCommandLineParser<TBuildType> where TBuildType : new()
	{
		/// <summary>
		/// Gets the <see cref="IFluentCommandLineParser"/>.
		/// </summary>
		public IFluentCommandLineParser Parser { get; private set; }

		/// <summary>
		/// Gets the constructed object.
		/// </summary>
		public TBuildType Object { get; private set; }

		/// <summary>
		/// Initialises a new instance of the <see cref="FluentCommandLineParser{TBuildType}"/> class.
		/// </summary>
		public FluentCommandLineParser()
		{
			Object = new TBuildType();
			Parser = new FluentCommandLineParser();
		}

		/// <summary>
		/// Sets up an Option for a write-able property on the type being built.
		/// </summary>
		public ICommandLineOptionBuilderFluent<TProperty> Setup<TProperty>(Expression<Func<TBuildType, TProperty>> propertyPicker)
		{
			return new CommandLineOptionBuilderFluent<TBuildType, TProperty>(Parser, Object, propertyPicker);
		}

		/// <summary>
		/// Parses the specified <see><cref>T:System.String[]</cref></see> using the setup Options.
		/// </summary>
		/// <param name="args">The <see><cref>T:System.String[]</cref></see> to parse.</param>
		/// <returns>An <see cref="ICommandLineParserResult"/> representing the results of the parse operation.</returns>
		public ICommandLineParserResult Parse(string[] args)
		{
			return Parser.Parse(args);
		}

		/// <summary>
		/// Setup the help args.
		/// </summary>
		/// <param name="helpArgs">The help arguments to register.</param>
		public IHelpCommandLineOptionFluent SetupHelp(params string[] helpArgs)
		{
			return Parser.SetupHelp(helpArgs);
		}

		/// <summary>
		/// Gets or sets whether values that differ by case are considered different. 
		/// </summary>
		public bool IsCaseSensitive
		{
			get { return Parser.IsCaseSensitive; }
			set { Parser.IsCaseSensitive = value; }
		}

        /// <summary>
        /// Gets or sets the option used for when help is detected in the command line args.
        /// </summary>
        public IHelpCommandLineOption HelpOption
        {
            get { return Parser.HelpOption; }
            set { Parser.HelpOption = value; }
        }

        /// <summary>
        /// Returns the Options that have been setup for this parser.
        /// </summary>
        public IEnumerable<ICommandLineOption> Options
        {
            get { return Parser.Options; }
        }
	}
}