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
    /// The class used as a parser for matrix expressions [ ... ]
    /// </summary>
	class MatrixParseTree : ParseTree
	{
		static Dictionary<string, Operator> operators = new Dictionary<string, Operator>();

		static internal void Register(Operator op)
		{
			operators.Add(op.Op, op);
		}

		public MatrixParseTree(QueryContext context, string input, int offset) : base(context, input, offset)
		{
		}

		protected override Value DefaultValue()
		{
			return new MatrixValue();
		}

		protected override Operator FindOperator(string input)
		{
			var op = Elements.FindOperator(operators, Query, input);

			if (op != null)
				return op;

			op = Elements.FindAvailableOperator(Query, input);

			if (op != null)
				return op;

            var containsSpace = false;
            var containsNewline = false;

            while (Skips.Count != 0)
            {
                var c = Skips.Pop();

				if (c == Tokens.Newline)
					containsNewline = true;
				else if (c == Tokens.Whitespace)
					containsSpace = true;
            }

            if (containsNewline)
                return new MatrixRowOperator();
			else if(containsSpace)
				return new MatrixColumnOperator();

			throw new ParseException(input);
		}
	}
}
