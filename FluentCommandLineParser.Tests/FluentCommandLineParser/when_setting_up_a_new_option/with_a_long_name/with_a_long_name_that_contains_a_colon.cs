﻿#region License
// with_a_long_name_that_contains_a_colon.cs
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

using Fclp.Tests.FluentCommandLineParser.Behaviour.Behaviour;
using Fclp.Tests.FluentCommandLineParser.TestContext.TestContext;
using Machine.Specifications;

namespace Fclp.Tests.FluentCommandLineParser.when_setting_up_a_new_option.with_a_long_name
{
	namespace when_setting_up_a_new_option
	{
		public class with_a_long_name_that_contains_a_colon : SettingUpALongOptionTestContext
		{
			Establish context = AutoMockAll;

			Because of = () => SetupOptionWith(valid_short_name, invalid_long_name_with_colon);

		    // ReSharper disable once ArrangeTypeMemberModifiers
#pragma warning disable 169
			Behaves_like<InvalidOptionSetupBehaviour> a_failed_setup_option;
#pragma warning restore 169
		}
	}
}