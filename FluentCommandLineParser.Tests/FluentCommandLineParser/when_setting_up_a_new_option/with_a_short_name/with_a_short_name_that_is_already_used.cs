﻿#region License
// with_a_short_name_that_is_already_used.cs
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

using Fclp.Internals;
using Fclp.Tests.FluentCommandLineParser.TestContext;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Fclp.Tests.FluentCommandLineParser
{
    namespace when_setting_up_a_new_option
    {
        public class with_a_short_name_that_is_already_used : SettingUpAShortOptionTestContext
        {
            private const string existingShortName = "s";
            private static ICommandLineOption existingOption;

            Establish context = () =>
                                    {
                                        AutoMockAll();

                                        var option = new Mock<ICommandLineOption>();
                                        option.SetupGet(x => x.ShortName).Returns(existingShortName);
                                        existingOption = option.Object;
                                    };

            Because of = () =>
                             {
                                 sut.Options.Add(existingOption);
                                 SetupOptionWith(existingShortName);
                             };

            It should_throw_an_error = () => error.ShouldBeOfType(typeof(OptionAlreadyExistsException));
            It should_not_have_setup_an_option = () => sut.Options.ShouldContainOnly(existingOption);
        }
    }
}