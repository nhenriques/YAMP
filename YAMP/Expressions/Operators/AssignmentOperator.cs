using System;
using System.Collections;

namespace YAMP
{
	class AssignmentOperator : BinaryOperator
	{
		public AssignmentOperator () : this(string.Empty)
		{
		}

        public AssignmentOperator(string prefix) : base(prefix + "=", -1)
        {
        }

        public override Value Handle(AbstractExpression left, AbstractExpression right, Hashtable symbols)
        {
            var bottom = right.Interpret(symbols);
			return Assign(left, bottom, symbols);
        }
		
		public override Value Perform (Value left, Value right)
		{
			return right;
		}
		
		Value Assign(AbstractExpression left, Value value, Hashtable symbols)
		{
			if (left is SymbolExpression)
                return Assign(left as SymbolExpression, value);
			else if(left is BracketExpression)
			{
				var bracket = left as BracketExpression;
				
				if(bracket.Tree.Operator == null)
					return Assign(bracket.Tree.Expressions[0], value, symbols);
				else if(bracket.Tree.Operator is IndexOperator)
				{
					var ix = bracket.Tree.Operator as IndexOperator;
					return ix.Handle(bracket.Tree.Expressions[0], value, symbols);
				}
				else
					throw new AssignmentException("The left side of an assignment must be a symbol or index on a symbol");
			}
			
			return value;
		}

        Value Assign(SymbolExpression left, Value value)
		{
            if(left.IsSymbol)
                Tokens.Instance.AssignVariable(left.SymbolName, value);
			else
				throw new AssignmentException("The left side must be a symbol - found a function.");
			
			return value;
		}
	}
}

