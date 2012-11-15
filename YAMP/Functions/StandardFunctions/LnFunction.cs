using System;

namespace YAMP
{
	[Description("This is the natural logarithm, i.e. used to the basis of euler's number.")]
	[Kind(PopularKinds.Function)]
	class LnFunction : StandardFunction
    {
        protected override ScalarValue GetValue(ScalarValue value)
        {
            return value.Ln();
        }
    }
}

