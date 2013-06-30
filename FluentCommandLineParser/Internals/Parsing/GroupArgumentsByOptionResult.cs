using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentCommandLineParser.Internals.Parsing
{
    /// <summary>
    /// result of arguments grouping
    /// </summary>
    public class GroupArgumentsByOptionResult
    {
        /// <summary>
        /// groups of arguments matched by options
        /// </summary>
        public string[][] MatchedArgumentsOptionGroups { get; private set; }

        /// <summary>
        /// arguments that could not be matched to an option
        /// </summary>
        public string[] UnmatchedArgs { get; private set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="GroupArgumentsByOptionResult"/> class.
        /// </summary>
        /// <param name="matchedArgumentsOptionGroups"></param>
        /// <param name="unmatchedArgs"></param>
        public GroupArgumentsByOptionResult(string[][] matchedArgumentsOptionGroups,string[] unmatchedArgs)
        {
            this.MatchedArgumentsOptionGroups=matchedArgumentsOptionGroups;
            this.UnmatchedArgs=unmatchedArgs;
        }

    }
}
