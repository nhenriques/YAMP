﻿namespace YAMP
{
    [Description("CothFunctionDescription")]
    [Kind(PopularKinds.Trigonometric)]
    [Link("CothFunctionLink")]
    sealed class CothFunction : StandardFunction
    {
        protected override ScalarValue GetValue(ScalarValue value)
        {
            var a = value.Exp();
            var b = (-value).Exp();
            return (a + b) / (a - b);
        }
    }
}
