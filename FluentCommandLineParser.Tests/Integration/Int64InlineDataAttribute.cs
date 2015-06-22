namespace Fclp.Tests.Integration
{
    public class Int64InlineDataAttribute : SimpleShortOptionsAreParsedCorrectlyAttribute
    {
        public Int64InlineDataAttribute(string args, long expected)
            : base(args, expectedInt64: expected)
        {
        }
    }
}