#region License
// ListTests.cs
// Copyright (c) 2013, Simon Williams
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification, are permitted provide
// d that the following conditions are met:
// 
// Redistributions of source code must retain the above copyright notice, this list of conditions and the
// following disclaimer.
// 
// Redistributions in binary form must reproduce the above copyright notice, this list of conditions and
// the following disclaimer in the documentation and/or other materials provided with the distribution.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED 
// WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
// PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
// TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
// POSSIBILITY OF SUCH DAMAGE.
#endregion

using System.Collections.Generic;
using Fclp.Tests.FluentCommandLineParser;
using Fclp.Tests.Internals;
using Machine.Specifications;
using Xunit;
using Xunit.Extensions;

namespace Fclp.Tests.Integration
{
    public class FlagTests : TestContextBase<Fclp.FluentCommandLineParser>
    {
        [Theory]
        [EnumFlagListInlineData("--flag Value0 Value1", TestEnumFlag.Value0, TestEnumFlag.Value1)]
        [EnumFlagListInlineData("-f Value0 Value1", TestEnumFlag.Value0, TestEnumFlag.Value1)]
        [EnumFlagListInlineData("/flag Value0 Value1", TestEnumFlag.Value0, TestEnumFlag.Value1)]
        [EnumFlagListInlineData("/flag:Value0 Value1", TestEnumFlag.Value0, TestEnumFlag.Value1)]
        [EnumFlagListInlineData("/flag=Value0 Value1", TestEnumFlag.Value0, TestEnumFlag.Value1)]
        [EnumFlagListInlineData("--flag:Value0 Value1", TestEnumFlag.Value0, TestEnumFlag.Value1)]
        [EnumFlagListInlineData("--flag=Value0 Value1", TestEnumFlag.Value0, TestEnumFlag.Value1)]
        [EnumFlagListInlineData("--flag 0 1", TestEnumFlag.Value0, TestEnumFlag.Value1)]
        [EnumFlagListInlineData("-f 0 1", TestEnumFlag.Value0, TestEnumFlag.Value1)]
        [EnumFlagListInlineData("/flag 0 1", TestEnumFlag.Value0, TestEnumFlag.Value1)]
        [EnumFlagListInlineData("/flag:0 1", TestEnumFlag.Value0, TestEnumFlag.Value1)]
        [EnumFlagListInlineData("/flag=0 1", TestEnumFlag.Value0, TestEnumFlag.Value1)]
        [EnumFlagListInlineData("--flag:0 1", TestEnumFlag.Value0, TestEnumFlag.Value1)]
        [EnumFlagListInlineData("--flag=0 1", TestEnumFlag.Value0, TestEnumFlag.Value1)]

        [EnumFlagListInlineData("--flag Value0 Value1 Value16", TestEnumFlag.Value0, TestEnumFlag.Value1, TestEnumFlag.Value16)]
        [EnumFlagListInlineData("-f Value0 Value1 Value16", TestEnumFlag.Value0, TestEnumFlag.Value1, TestEnumFlag.Value16)]
        [EnumFlagListInlineData("/flag Value0 Value1 Value16", TestEnumFlag.Value0, TestEnumFlag.Value1, TestEnumFlag.Value16)]
        [EnumFlagListInlineData("/flag:Value0 Value1 Value16", TestEnumFlag.Value0, TestEnumFlag.Value1, TestEnumFlag.Value16)]
        [EnumFlagListInlineData("/flag=Value0 Value1 Value16", TestEnumFlag.Value0, TestEnumFlag.Value1, TestEnumFlag.Value16)]
        [EnumFlagListInlineData("--flag:Value0 Value1 Value16", TestEnumFlag.Value0, TestEnumFlag.Value1, TestEnumFlag.Value16)]
        [EnumFlagListInlineData("--flag=Value0 Value1 Value16", TestEnumFlag.Value0, TestEnumFlag.Value1, TestEnumFlag.Value16)]
        [EnumFlagListInlineData("--flag 0 1 16", TestEnumFlag.Value0, TestEnumFlag.Value1, TestEnumFlag.Value16)]
        [EnumFlagListInlineData("-f 0 1 16", TestEnumFlag.Value0, TestEnumFlag.Value1, TestEnumFlag.Value16)]
        [EnumFlagListInlineData("/flag 0 1 16", TestEnumFlag.Value0, TestEnumFlag.Value1, TestEnumFlag.Value16)]
        [EnumFlagListInlineData("/flag:0 1 16", TestEnumFlag.Value0, TestEnumFlag.Value1, TestEnumFlag.Value16)]
        [EnumFlagListInlineData("/flag=0 1 16", TestEnumFlag.Value0, TestEnumFlag.Value1, TestEnumFlag.Value16)]
        [EnumFlagListInlineData("--flag:0 1 16", TestEnumFlag.Value0, TestEnumFlag.Value1, TestEnumFlag.Value16)]
        [EnumFlagListInlineData("--flag=0 1 16", TestEnumFlag.Value0, TestEnumFlag.Value1, TestEnumFlag.Value16)]
        private void should_contain_list_with_expected_items<T>(string arguments, IEnumerable<TestEnumFlag> expectedItems)
        {
            sut = new Fclp.FluentCommandLineParser();

            var actualEnum = TestEnumFlag.Value0;

            sut.Setup<TestEnumFlag>('f', "flag").Callback(items => actualEnum = items).Required();

            var args = ParseArguments(arguments);

            var results = sut.Parse(args);

            results.HasErrors.ShouldBeFalse();
            foreach (var expectedItem in expectedItems)
            {
                actualEnum.HasFlag(expectedItem).ShouldBeTrue();
            }

            actualEnum.HasFlag(TestEnumFlag.Value2).ShouldBeFalse();
            actualEnum.HasFlag(TestEnumFlag.Value4).ShouldBeFalse();
            actualEnum.HasFlag(TestEnumFlag.Value8).ShouldBeFalse();

        }
    }
}
