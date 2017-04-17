namespace Fclp.Tests.Integration.Lists
{
    public class Int64ListInlineDataAttribute : ArgumentInlineDataAttribute
    {
        public Int64ListInlineDataAttribute(string args, params long[] listItems)
            : base(args, listItems)
        {
        }
    }
}
