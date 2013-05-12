#region License
// CommandLineParserEngineMark2.cs
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

using System.Collections.Generic;
using System.Linq;
using Fclp.Internals.Parsing;

namespace Fclp.Internals
{
	/// <summary>
	/// More advanced parser for transforming command line arguments into appropriate <see cref="ParsedOption"/>.
	/// </summary>
	public class CommandLineParserEngineMark2 : ICommandLineParserEngine
	{
		private readonly List<string> _additionalOptionsFound = new List<string>();
		private readonly List<ParsedOption> _parsedOptions = new List<ParsedOption>();

		/// <summary>
		/// Parses the specified <see><cref>T:System.String[]</cref></see> into appropriate <see cref="ParsedOption"/> objects..
		/// </summary>
		/// <param name="args">The <see><cref>T:System.String[]</cref></see> to parse.</param>
		/// <returns>An <see cref="ParserEngineResult"/> representing the results of the parse operation.</returns>
		public ParserEngineResult Parse(string[] args)
		{
			args = args ?? new string[0];

			var grouper = new CommandLineOptionGrouper();

			foreach (var optionGroup in grouper.GroupByOption(args))
			{
				string rawKey = optionGroup.First();
				ParseGroupIntoOption(rawKey, optionGroup.Skip(1));
			}

			return new ParserEngineResult(_parsedOptions, _additionalOptionsFound);
		}

		private void ParseGroupIntoOption(string rawKey, IEnumerable<string> optionGroup)
		{
			if (IsAKey(rawKey))
			{
				var parsedOption = ParsedOptionFactory.Create(rawKey);

				TrimSuffix(parsedOption);

				new OptionArgumentParser().ParseArguments(optionGroup, parsedOption);

				AddParsedOptionToList(parsedOption);
			}
			else
			{
				_additionalOptionsFound.AddRange(optionGroup);
			}
		}

		private void AddParsedOptionToList(ParsedOption parsedOption)
		{
			if (ShortOptionNeedsToBeSplit(parsedOption))
			{
				_parsedOptions.AddRange(CloneAndSplit(parsedOption));
			}
			else
			{
				_parsedOptions.Add(parsedOption);
			}
		}

		private static bool ShortOptionNeedsToBeSplit(ParsedOption parsedOption)
		{
			return PrefixIsShortOption(parsedOption.Prefix) && parsedOption.Key.Length > 1;
		}

		private static IEnumerable<ParsedOption> CloneAndSplit(ParsedOption parsedOption)
		{
			return parsedOption.Key.Select(c => Clone(parsedOption, c)).ToList();
		}

		private static ParsedOption Clone(ParsedOption toClone, char c)
		{
			var clone = toClone.Clone();
			clone.Key = new string(new[] { c });
			return clone;
		}

		private static bool PrefixIsShortOption(string key)
		{
			return SpecialCharacters.ShortOptionPrefix.Contains(key);
		}

		private static void TrimSuffix(ParsedOption parsedOption)
		{
			if (parsedOption.HasSuffix)
			{
				parsedOption.Key = parsedOption.Key.TrimEnd(parsedOption.Suffix.ToCharArray());
			}
		}

		/// <summary>
		/// Gets whether the specified <see cref="System.String"/> is a Option key.
		/// </summary>
		/// <param name="arg">The <see cref="System.String"/> to examine.</param>
		/// <returns><c>true</c> if <paramref name="arg"/> is a Option key; otherwise <c>false</c>.</returns>
		static bool IsAKey(string arg)
		{
			return arg != null && SpecialCharacters.OptionPrefix.Any(arg.StartsWith);
		}

		/// <summary>
		/// Parses the specified <see><cref>T:System.String[]</cref></see> into key value pairs.
		/// </summary>
		/// <param name="args">The <see><cref>T:System.String[]</cref></see> to parse.</param>
		/// <returns>An <see cref="IEnumerable{T}"/> containing the results of the parse operation.</returns>
		IEnumerable<ParsedOption> ICommandLineParserEngine.Parse(string[] args)
		{
			return Parse(args).ParsedOptions;
		}
	}
}