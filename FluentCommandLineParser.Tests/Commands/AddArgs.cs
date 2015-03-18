using System.Collections.Generic;

namespace Fclp.Tests.Commands
{
    class AddArgs
    {
        public bool Verbose { get; set; }
        public bool IgnoreErrors { get; set; }
        public List<string> Files { get; set; }
    }
}
