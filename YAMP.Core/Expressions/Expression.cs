namespace YAMP
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the abstract base class for expressions.
    /// </summary>
	public abstract class Expression : Block, IRegisterElement
	{
		#region ctor

        /// <summary>
        /// Creates a new expression.
        /// </summary>
		public Expression()
		{
		}

        /// <summary>
        /// Creates a new expression.
        /// </summary>
        /// <param name="line">The line of beginning of the expression.</param>
        /// <param name="column">The column in the line of the beginning of the expression.</param>
        public Expression(Int32 line, Int32 column)
        {
            StartLine = line;
            StartColumn = column;
        }

        /// <summary>
        /// Creates a new expression.
        /// </summary>
        /// <param name="query">The context of the expression.</param>
        public Expression(QueryContext query)
        {
            Query = query;
        }

        /// <summary>
        /// Creates a new expression.
        /// </summary>
        /// <param name="query">The context of the expression.</param>
        /// <param name="line">The line of beginning of the expression.</param>
        /// <param name="column">The column in the line of the beginning of the expression.</param>
        public Expression(QueryContext query, Int32 line, Int32 column) 
            : this(line, column)
        {
            Query = query;
        }

        /// <summary>
        /// Creates a new expression.
        /// </summary>
        /// <param name="engine">The parse engine used for creating this expresssion.</param>
        public Expression(ParseEngine engine)
            : this(engine.Query, engine.CurrentLine, engine.CurrentColumn)
        {
        }

		#endregion

        #region Properties

        /// <summary>
        /// Gets a dummy expression for doing nothing.
        /// </summary>
        public static Expression Empty
        {
            get { return new EmptyExpression(); }
        }

        /// <summary>
        /// Gets a value indicating if the expression is a whole statement.
        /// </summary>
        public Boolean IsSingleStatement
        {
            get;
            protected set;
        }

		#endregion

		#region Methods

        /// <summary>
        /// Begins interpreting the contents of the expression.
        /// </summary>
        /// <param name="symbols">The external symbols to consider.</param>
        /// <returns>The evaluated value.</returns>
        public abstract Value Interpret(IDictionary<String, Value> symbols);

        /// <summary>
        /// Scans for an expression given the parse engine.
        /// </summary>
        /// <param name="engine">The engine which scans the query.</param>
        /// <returns>The built expression.</returns>
        public abstract Expression Scan(ParseEngine engine);
		
        /// <summary>
        /// Registers this element at some target.
        /// </summary>
        public virtual void RegisterElement(IElementMapping elementMapping)
		{
            elementMapping.AddExpression(this);
		}

        #endregion

        #region String Representations

        /// <summary>
        /// Returns a string representation of the expression.
        /// </summary>
        /// <returns></returns>
        public override String ToString ()
		{
            return String.Format("({0}, {1}) {2}",
                StartLine, StartColumn, GetType().Name.RemoveExpressionConvention());
		}

		#endregion
	}
}
