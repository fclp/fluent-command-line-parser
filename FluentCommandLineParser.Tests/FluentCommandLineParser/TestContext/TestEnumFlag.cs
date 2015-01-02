using System;

namespace Fclp.Tests.FluentCommandLineParser
{
    [Flags]
    public enum TestEnumFlag
    {
        Value0 = 0,
        Value1 = 1,
        Value2 = 2,
        Value4 = 4,
        Value8 = 8,
        Value16 = 16,
        Value32 = 32,
        Value64 = 64
    }
}