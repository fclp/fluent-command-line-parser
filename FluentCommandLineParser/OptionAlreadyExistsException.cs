using System;
using System.Runtime.Serialization;

namespace Fclp
{
    /// <summary>
    /// Represents an error that has occurred because a matching Option already exists in the parser.
    /// </summary>
    [Serializable]
    public class OptionAlreadyExistsException : Exception
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="OptionAlreadyExistsException"/> class.
        /// </summary>
        public OptionAlreadyExistsException() { }

        /// <summary>
        /// Initialises a new instance of the <see cref="OptionAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="optionName"></param>
        public OptionAlreadyExistsException(string optionName) : base(optionName) { }
        
        /// <summary>
        /// Initialises a new instance of the <see cref="OptionAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public OptionAlreadyExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        
        /// <summary>
        /// Initialises a new instance of the <see cref="OptionAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="optionName"></param>
        /// <param name="innerException"></param>
        public OptionAlreadyExistsException(string optionName, Exception innerException)
            : base(optionName, innerException) { }

    }
}
