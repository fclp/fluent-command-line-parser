using System.Collections.Generic;
using Fclp.Internals.Extensions;

namespace Fclp.Internals.Parsing
{
	/// <summary>
	/// 
	/// </summary>
	public class CommandLineOptionGrouper
	{
		/// <summary>
		/// Groups the by option.
		/// </summary>
		/// <param name="args">The args.</param>
		/// <returns></returns>
		public string[][] GroupByOption(string[] args)
		{
			if (args.IsNullOrEmpty()) return new string[0][];

			var optionParser = new Something(args);

			var options = new List<string[]>();

			if (optionParser.ArgsContainsOptions() == false)
			{
				options.Add(optionParser.CreateGroupForCurrent());
			}
			else
			{
				while (optionParser.MoveToNextOption())
				{
					options.Add(optionParser.CreateGroupForCurrent());
				}
			}

			return options.ToArray();
		}

	}
}