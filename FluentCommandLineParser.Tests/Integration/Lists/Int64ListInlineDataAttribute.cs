namespace Fclp.Tests.Integration
{
    public class Int64ListInlineDataAttribute : ArgumentInlineDataAttribute
    {
        public Int64ListInlineDataAttribute(string args, params long[] listItems)
            : base(args, listItems)
        {
        }
    }
}
