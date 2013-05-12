using System;
using System.Collections.Generic;
using System.Linq;
using Fclp.Internals.Extensions;

namespace Fclp.Internals.Parsing
{
	/// <summary>
	/// 
	/// </summary>
	public class OptionArgumentParser
	{
		/// <summary>
		/// Parses the values.
		/// </summary>
		/// <param name="args">The args.</param>
		/// <param name="option">The option.</param>
		public void ParseArguments(IEnumerable<string> args, ParsedOption option)
		{
			if (SpecialCharacters.ValueAssignments.Any(option.Key.Contains))
			{
				TryGetArgumentFromKey(option);
			}

			var allArguments = new List<string>();
			var additionalArguments = new List<string>();

			var otherArguments = CollectArgumentsUntilNextKey(args).ToList();

			if (option.HasValue) allArguments.Add(option.Value);

			if (otherArguments.Any())
			{
				allArguments.AddRange(otherArguments);

				if (otherArguments.Count() > 1)
				{
					additionalArguments.AddRange(otherArguments);
					additionalArguments.RemoveAt(0);
				}
			}

			option.Value = allArguments.FirstOrDefault();
			option.Values = allArguments.ToArray();
			option.AddtionalValues = additionalArguments.ToArray();
		}

		private static void TryGetArgumentFromKey(ParsedOption option)
		{
			var split = option.Key.Split(SpecialCharacters.ValueAssignments, 2, StringSplitOptions.RemoveEmptyEntries);

			option.Key = split[0];
			option.Value = split.Length > 1 
				               ? split[1].WrapInDoubleQuotesIfContainsWhitespace()
				               : null;
		}

		static IEnumerable<string> CollectArgumentsUntilNextKey(IEnumerable<string> args)
		{
			return from argument in args
			       where !IsEndOfOptionsKey(argument)
			       select argument.WrapInDoubleQuotesIfContainsWhitespace();
		}

		/// <summary>
		/// Determines whether the specified string indicates the end of parsed options.
		/// </summary>
		static bool IsEndOfOptionsKey(string arg)
		{
			return string.Equals(arg, SpecialCharacters.EndOfOptionsKey, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}