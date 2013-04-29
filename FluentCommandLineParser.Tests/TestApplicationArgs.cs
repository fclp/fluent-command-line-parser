#region License
// TestApplicationArgs.cs
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

using System;
using System.Collections.Generic;

namespace Fclp.Tests
{
	/// <summary>
	/// 
	/// </summary>
	public class TestApplicationArgs
	{
		public int RecordId { get; set; }
		public bool Silent { get; set; }
		public string NewValue { get; set; }
	}

	public class ListTestApplicationArgs
	{
		public List<int> Integers { get; set; }
		public List<string> Strings { get; set; }
		public List<bool> Booleans { get; set; }
		public List<double> Doubles { get; set; }
		public List<DateTime> DateTimes { get; set; } 
	}

	public class EnumerableApplicationArgs
	{
		public List<int> Integers { get; set; } 
		public List<string> Strings { get; set; } 
		public List<bool> Booleans { get; set; } 
		public List<double> Doubles { get; set; } 
		public List<DateTime> DateTimes { get; set; } 
	}
}