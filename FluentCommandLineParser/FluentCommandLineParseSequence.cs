using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fclp
{
    /// <summary>
    /// option/callback execute sequence
    /// </summary>
    public enum FluentCommandLineParseSequence
    {
        /// <summary>
        /// same as the setup sequence, early setup higher priority
        /// </summary>
        SameAsSetup = 0,

        /// <summary>
        /// same as the cmd input options sequence
        /// </summary>
        SameAsOptions
    }
}
