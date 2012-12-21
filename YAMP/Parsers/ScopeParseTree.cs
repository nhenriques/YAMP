﻿/*
	Copyright (c) 2012, Florian Rappl.
	All rights reserved.

	Redistribution and use in source and binary forms, with or without
	modification, are permitted provided that the following conditions are met:
		* Redistributions of source code must retain the above copyright
		  notice, this list of conditions and the following disclaimer.
		* Redistributions in binary form must reproduce the above copyright
		  notice, this list of conditions and the following disclaimer in the
		  documentation and/or other materials provided with the distribution.
		* Neither the name of the YAMP team nor the names of its contributors
		  may be used to endorse or promote products derived from this
		  software without specific prior written permission.

	THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
	ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
	WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
	DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
	DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
	(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
	LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
	ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
	(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
	SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System;
using System.Collections.Generic;

namespace YAMP
{
	/// <summary>
	/// The scope parse tree that allows to create new local variables and more.
	/// </summary>
	class ScopeParseTree : ParseTree
	{
		#region ctor

		public ScopeParseTree(QueryContext query, string input, int line) : base(new QueryContext(query), input, true)
		{
			Query.Statements.Init(input, line);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the starting statement line of this scope.
		/// </summary>
		public int StartLine
		{
			get
			{
				return Query.Statements.StartLine;
			}
		}

		/// <summary>
		/// Gets the statements included in this scope.
		/// </summary>
		public IEnumerable<ParseTree> Statements
		{
			get
			{
				return Query.Statements.Statements;
			}
		}

		#endregion

		#region Methods

		internal override Value Interpret(Dictionary<string, Value> symbols)
		{
			return Query.Statements.Run(symbols);
		}

		public override string ToString()
		{
			return Query.Statements.ToString();
		}

		#endregion
	}
}