using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Fclp.Internals.Extensions;

namespace Fclp.Internals
{
    /// <summary>
    /// 
    /// </summary>
	public class CommandLineOptionFormatterMark2 : ICommandLineOptionFormatter
	{
        /// <summary>
        /// 
        /// </summary>
		public CommandLineOptionFormatterMark2()
		{
			Header = "Options:";
			ValueText = "Value";
			DescriptionText = "Description";
			NoOptionsText = "No options have been setup";
			MinOptionPadding = 2;
			LeftPadding = 2;
		}

        /// <summary>
        /// 
        /// </summary>
		public const string TextFormat = "\t{0}\t\t{1}\n";

		private bool ShowHeader
		{
			get { return Header != null; }
		}

        /// <summary>
        /// 
        /// </summary>
		public string Header { get; set; }
        /// <summary>
        /// 
        /// </summary>
		public string ValueText { get; set; }
        /// <summary>
        /// 
        /// </summary>
		public string DescriptionText { get; set; }
        /// <summary>
        /// 
        /// </summary>
		public string NoOptionsText { get; set; }
        /// <summary>
        /// 
        /// </summary>
		public int MinOptionPadding { get; set; }
        /// <summary>
        /// 
        /// </summary>
		public int LeftPadding { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
		public string Format(System.Collections.Generic.IEnumerable<ICommandLineOption> options)
		{
			if (options == null) throw new ArgumentNullException("options");

			var list = options.ToList();

			if (!list.Any()) return this.NoOptionsText;

			var sb = new StringBuilder();
			sb.AppendLine();

			// add headers first
			if (ShowHeader)
			{
				sb.AppendLine(Header);
				sb.AppendLine();
			}

			var ordered = (from option in list
						   orderby option.ShortName.IsNullOrWhiteSpace() == false descending, option.ShortName
						   select option).ToList();

			var minPadding = FindLongestLongOption(list) + MinOptionPadding;

			foreach (var cmdOption in ordered)
				AppendWithPadding(sb, FormatValue(cmdOption), cmdOption.Description, minPadding);

			return sb.ToString();
		}

		static int FindLongestLongOption(System.Collections.Generic.IEnumerable<ICommandLineOption> options)
		{
			return options.Select(option => FormatValue(option).Length).OrderByDescending(len => len).First();
		}

		void AppendWithPadding(StringBuilder sb, string optionText, string descriptionText, int minOptionPadding)
		{
			var requiredPadding = minOptionPadding - optionText.Length;

			var leftPadding = new string(' ', LeftPadding);
			var optionPadding = new string(' ', requiredPadding);

			sb.AppendFormat(CultureInfo.CurrentUICulture, "{0}{1}{2}{3}\n", leftPadding, optionText, optionPadding, descriptionText);
		}
        ///
		static string FormatValue(ICommandLineOption cmdOption)
		{
            if (cmdOption.ShortName.IsNullOrWhiteSpace())
			{
				return FormatLong(cmdOption.LongName);
			}

            if (cmdOption.LongName.IsNullOrWhiteSpace())
			{
				return FormatShort(cmdOption.ShortName);
			}

			return FormatShort(cmdOption.ShortName) + ", " + FormatLong(cmdOption.LongName);
		}

		static string FormatShort(string shortOption)
		{
			return "-" + shortOption; 
		}

		static string FormatLong(string longOption)
		{
			return "--" + longOption; 
		}
	}
}
