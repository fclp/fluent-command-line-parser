using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Fclp.Internals.Extensions;

namespace Fclp.Internals
{
    /// <summary>
    /// Simple default formatter used to display command line options to the user.
    /// </summary>
    public class CommandLineOptionFormatter : ICommandLineOptionFormatter
    {
        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="CommandLineOptionFormatter"/> class.
        /// </summary>
        public CommandLineOptionFormatter()
        {
            this.ValueText = "Value";
            this.DescriptionText = "Description";
            this.NoOptionsText = "No options have been setup";
        }

        #endregion
        /// <summary>
        /// The text format used in this formatter.
        /// </summary>
        public const string TextFormat = "\t/{0}\t\t{1}\n";

        /// <summary>
        /// Gets or sets the text to use as <c>Value</c> header. This should be localised for the end user.
        /// </summary>
        public string ValueText { get; set; }

        /// <summary>
        /// Gets or sets the text to use as the <c>Description</c> header. This should be localised for the end user.
        /// </summary>
        public string DescriptionText { get; set; }

        /// <summary>
        /// Gets or sets the text to use when there are no options. This should be localised for the end user.
        /// </summary>
        public string NoOptionsText { get; set; }

        /// <summary>
        /// Formats the list of <see cref="ICommandLineOption"/> to be displayed to the user.
        /// </summary>
        /// <param name="options">The list of <see cref="ICommandLineOption"/> to format.</param>
        /// <returns>A <see cref="System.String"/> representing the format</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="options"/> is <c>null</c>.</exception>
        public string Format(IEnumerable<ICommandLineOption> options)
        {
            if (options == null) throw new ArgumentNullException("options");

            var list = options.ToList();

            if (!list.Any()) return this.NoOptionsText;

            var sb = new StringBuilder();

            // add headers first
            sb.AppendFormat(CultureInfo.CurrentUICulture, TextFormat, this.ValueText, this.DescriptionText);

            foreach (var cmdOption in list.OrderBy(x => x.ShortName))
                sb.AppendFormat(CultureInfo.CurrentUICulture, TextFormat, FormatValue(cmdOption), cmdOption.Description);

            return sb.ToString();
        }

        /// <summary>
        /// Formats the short and long names into one <see cref="System.String"/>.
        /// </summary>
        static string FormatValue(ICommandLineOption cmdOption)
        {
            return cmdOption.LongName.IsNullOrWhiteSpace()
                       ? cmdOption.ShortName
                       : cmdOption.ShortName + ":" + cmdOption.LongName;
        }
    }
}
