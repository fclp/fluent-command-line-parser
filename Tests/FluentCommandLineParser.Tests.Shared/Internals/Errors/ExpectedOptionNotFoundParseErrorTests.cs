#region license
//
// Copyright (c) 2012, Siy Williams (siy.williams@gmail.com)
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, are permitted 
// provided that the following conditions are met:
//
// Redistributions of source code must retain the above copyright notice, this list of conditions
// and the following disclaimer. Redistributions in binary form must reproduce the above copyright
// notice, this list of conditions and the following disclaimer in the documentation and/or other 
// materials provided with the distribution. Neither the name of the organization nor the names 
// of its contributors may be used to endorse or promote products derived from this software without 
// specific prior written permission. 
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR 
// IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY 
// AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR 
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL 
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, 
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER 
// IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
// OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
#endregion

using System;
using NUnit.Framework;
using Moq;

namespace FluentCommandLineParser.Internals.Errors
{
    [TestFixture]
    public class ExpectedOptionNotFoundParseErrorTests
    {
        [Test]
        public void Ensure_Can_Be_Constructed()
        {
            var cmdOption = Mock.Of<ICommandLineOption>();
            var snfError = new ExpectedOptionNotFoundParseError(cmdOption);
            Assert.AreSame(cmdOption, snfError.Option);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ensure_Cannot_Specify_Null_option()
        {
            new ExpectedOptionNotFoundParseError(null);
        }
    }
}
