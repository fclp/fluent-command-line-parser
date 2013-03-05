#region License
// FluentCommandLineParser.cs
// Copyright (c) 2013, Simon Williams
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
using System.Linq;
using Fclp.Internals;
using Fclp.Internals.Errors;
using Fclp.Internals.Extensions;

namespace Fclp
{
	/// <summary>
	/// A command line parser which provides methods and properties 
	/// to easily and fluently parse command line arguments. 
	/// </summary>
	public class FluentCommandLineParser : IFluentCommandLineParser
	{
		List<ICommandLineOption> _options;
		ICommandLineOptionFactory _optionFactory;
		ICommandLineParserEngine _parserEngine;
		ICommandLineOptionFormatter _defaultOptionFormatter;
		IHelpCommandLineOption _helpOption;

		///// <summary>
		///// Gets or sets whether the parser is case-sensitive. E.g. If <c>true</c> then <c>/a</c> will be treated as identical to <c>/A</c>.
		///// </summary>
		//public bool IsCaseSensitive { get; set; }

		/// <summary>
		/// Gets the <see cref="StringComparison"/> to use when matching values.
		/// </summary>
		internal StringComparison StringComparison
		{
			//get { return this.IsCaseSensitive ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase; }
			get { return StringComparison.CurrentCultureIgnoreCase; }
		}

		/// <summary>
		/// Gets the list of Options that have been setup.
		/// </summary>
		internal List<ICommandLineOption> Options
		{
			get { return _options ?? (_options = new List<ICommandLineOption>()); }
		}

		/// <summary>
		/// Gets or sets the default option formatter.
		/// </summary>
		internal ICommandLineOptionFormatter DefaultOptionFormatter
		{
			get { return _defaultOptionFormatter ?? (_defaultOptionFormatter = new CommandLineOptionFormatter()); }
			set { _defaultOptionFormatter = value; }
		}

		/// <summary>
		/// Gets or sets the <see cref="ICommandLineOptionFactory"/> to use for creating <see cref="ICommandLineOptionFluent{T}"/>.
		/// </summary>
		/// <remarks>If this property is set to <c>null</c> then the default <see cref="OptionFactory"/> is returned.</remarks>
		public ICommandLineOptionFactory OptionFactory
		{
			get { return _optionFactory ?? (_optionFactory = new CommandLineOptionFactory()); }
			set { _optionFactory = value; }
		}

		/// <summary>
		/// Gets or sets the <see cref="ICommandLineParserEngine"/> to use for parsing the command line args.
		/// </summary>
		public ICommandLineParserEngine ParserEngine
		{
			get { return _parserEngine ?? (_parserEngine = new CommandLineParserEngine()); }
			set { _parserEngine = value; }
		}

		internal IHelpCommandLineOption HelpOption
		{
			get { return _helpOption ?? (_helpOption = new EmptyHelpCommandLineOption()); }
			set { _helpOption = value; }
		}

		/// <summary>
		/// Setup a new <see cref="ICommandLineOptionFluent{T}"/> using the specified short and long Option name.
		/// </summary>
		/// <param name="shortOption">The short name for the Option. This must not be <c>null</c>, <c>empty</c> or only <c>whitespace</c>.</param>
		/// <param name="longOption">The long name for the Option or <c>null</c> if not required.</param>
		/// <returns></returns>
		/// <exception cref="OptionAlreadyExistsException">
		/// A Option with the same <paramref name="shortOption"/> name or <paramref name="longOption"/> name
		/// already exists in the <see cref="IFluentCommandLineParser"/>.
		/// </exception>
		public ICommandLineOptionFluent<T> Setup<T>(string shortOption, string longOption)
		{
			EnsureIsValidShortName(shortOption);
			EnsureIsValidLongName(longOption);

			foreach (var option in this.Options)
			{
				if (shortOption.Equals(option.ShortName, this.StringComparison))
					throw new OptionAlreadyExistsException(shortOption);

				if (longOption != null && longOption.Equals(option.LongName, this.StringComparison))
					throw new OptionAlreadyExistsException(longOption);
			}

			var argOption = this.OptionFactory.CreateOption<T>(shortOption, longOption);

			if (argOption == null)
				throw new InvalidOperationException("OptionFactory is producing unexpected results.");

			this.Options.Add(argOption);

			return argOption;
		}

		private static void EnsureIsValidShortName(string value)
		{
			var invalidChars = SpecialCharacters.ValueAssignments.Union(new[] { SpecialCharacters.Whitespace });
			if (value.IsNullOrWhiteSpace() || invalidChars.Any(value.Contains))
				throw new ArgumentOutOfRangeException("value");
		}

		private static void EnsureIsValidLongName(string value)
		{
			if (string.IsNullOrEmpty(value)) return;

			if (value.Trim() == string.Empty)
				throw new ArgumentOutOfRangeException("value");

			var invalidChars = SpecialCharacters.ValueAssignments.Union(new[] { SpecialCharacters.Whitespace });
			if (invalidChars.Any(value.Contains))
				throw new ArgumentOutOfRangeException("value");
		}

		/// <summary>
		/// Setup a new <see cref="ICommandLineOptionFluent{T}"/> using the specified short Option name.
		/// </summary>
		/// <param name="shortOption">The short name for the Option. This must not be <c>null</c>, <c>empty</c> or only <c>whitespace</c>.</param>
		/// <returns></returns>
		/// <exception cref="OptionAlreadyExistsException">
		/// A Option with the same <paramref name="shortOption"/> name 
		/// already exists in the <see cref="IFluentCommandLineParser"/>.
		/// </exception>
		public ICommandLineOptionFluent<T> Setup<T>(string shortOption)
		{
			return this.Setup<T>(shortOption, null);
		}

		/// <summary>
		/// Parses the specified <see><cref>T:System.String[]</cref></see> using the setup Options.
		/// </summary>
		/// <param name="args">The <see><cref>T:System.String[]</cref></see> to parse.</param>
		/// <returns>An <see cref="ICommandLineParserResult"/> representing the results of the parse operation.</returns>
		public ICommandLineParserResult Parse(string[] args)
		{
			var result = new CommandLineParserResult();

			if (this.HelpOption.ShouldShowHelp(args))
			{
				result.HelpCalled = true;
				this.HelpOption.ShowHelp(this.Options);
				return result;
			}

			var notFoundKey = default(KeyValuePair<string, string>);
			List<KeyValuePair<string, string>> keyValuePairs = null;

			keyValuePairs = this.ParserEngine.Parse(args).ToList();

			foreach (var setupOption in this.Options)
			{
				/*
				 * Step 1. match the setup Option to one provided in the args by either long or short names
				 * Step 2. if the key has been matched then bind the value
				 * Step 3. if the key is not matched and it is required, then add a new error
				 * Step 4. the key is not matched and optional, bind the default value if available
				 */

				// Step 1
				ICommandLineOption option = setupOption;
				var match = keyValuePairs.FirstOrDefault(pair =>
					pair.Key.Equals(option.ShortName, this.StringComparison) // tries to match the short name
					|| pair.Key.Equals(option.LongName, this.StringComparison)); // or else the long name

				if (!notFoundKey.Equals(match)) // Step 2
				{
					setupOption.Bind(match.Value);
					keyValuePairs.Remove(match);
				}
				else
				{
					if (setupOption.IsRequired) // Step 3
						result.Errors.Add(new ExpectedOptionNotFoundParseError(setupOption));
					else if (setupOption.HasDefault)
						setupOption.BindDefault(); // Step 4

					result.UnMatchedOptions.Add(option);
				}
			}

			keyValuePairs.ForEach(result.AdditionalOptionsFound.Add);

			return result;
		}

		/// <summary>
		/// Constructs the show usage text from the existing setup Options using a <see cref="CommandLineOptionFormatter"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> describing all the Options.</returns>
		public string CreateShowUsageText()
		{
			return this.CreateShowUsageText(this.DefaultOptionFormatter);
		}

		/// <summary>
		/// Constructs the show usage text from the existing setup Options using the specified <see cref="ICommandLineOptionFormatter"/>.
		/// </summary>
		/// <param name="formatter">The <see cref="ICommandLineOptionFormatter"/> to use to format the usage text. This must not be <c>null</c>.</param>
		/// <returns>A <see cref="System.String"/> describing all the Options.</returns>
		/// <exception cref="ArgumentNullException">If <paramref name="formatter"/> is <c>null</c>.</exception>
		public string CreateShowUsageText(ICommandLineOptionFormatter formatter)
		{
			if (formatter == null) throw new ArgumentNullException("formatter");
			return formatter.Format(this.Options);
		}

		/// <summary>
		/// Setup the help args.
		/// </summary>
		/// <param name="helpArgs">The help arguments to register.</param>
		public IHelpCommandLineOptionFluent SetupHelp(params string[] helpArgs)
		{
			var helpOption = this.OptionFactory.CreateHelpOption(helpArgs);
			this.HelpOption = helpOption;
			return helpOption;
		}
	}
}
