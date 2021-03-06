﻿using System;
using YAMP;

namespace YAMP.Physics
{
    [Kind(PopularKinds.Function)]
    [Description("The inverse tangent integral is defined in terms of the polylogarithm.")]
    [Link("http://mathworld.wolfram.com/InverseTangentIntegral.html")]
    class InvTangentFunction : ArgumentFunction
    {
        static readonly ScalarValue IMAGONEHALF = new ScalarValue(0, -0.5);

        [Description("Computes the inverse tangent integral with s = 2 for an argument z.")]
        [Example("invtangent(0.5)", "Evaluates the integral at z = 0.5.")]
        public ScalarValue Function(ScalarValue z)
        {
            return Function(new ScalarValue(2), z);
        }

        [Description("Computes the inverse tangent integral with s = 2 for each value of the matrix Z.")]
        [Example("invtangent([0:0.1:1])", "Evaluates the integral at the values 0, 0.1, 0.2, ..., 1.0.")]
        public MatrixValue Function(MatrixValue Z)
        {
            return Function(new ScalarValue(2), Z);
        }

        [Description("Computes the inverse tangent integral with an arbitrary integer s for an argument z.")]
        [Example("invtangent(1, 0.5)", "Evaluates the inverse tangent integral at z = 0.5, with s = 1, which represents arctan(0.5).")]
        public ScalarValue Function(ScalarValue s, ScalarValue z)
        {
            var n = s.GetIntegerOrThrowException("s", Name);
            return GetValue(n, z);
        }

        [Description("Computes the inverse tangent integral with an arbitrary integer s for each value of the matrix Z.")]
        [Example("invtangent(0, [0:0.1:1])", "Evaluates the inverse tangent integral at the values z = 0, 0.1, 0.2, ..., 1.0 at s = 0, which represents the first derivative, z / (1 + z^2).")]
        public MatrixValue Function(ScalarValue s, MatrixValue Z)
        {
            var n = s.GetIntegerOrThrowException("s", Name);
            var M = new MatrixValue(Z.DimensionY, Z.DimensionX);

            for (var j = 1; j <= Z.DimensionY; j++)
                for (var i = 1; i <= Z.DimensionX; i++)
                    M[j, i] = GetValue(n, Z[j, i]);

            return M;
        }

        ScalarValue GetValue(int n, ScalarValue z)
        {
            if (n == 1)
                return z.Arctan();
            else if (n == 0)
                return z / (1.0 + z * z);

            var iz = z * ScalarValue.I;
            return IMAGONEHALF * (PolyLogFunction.Polylog(n, iz) - PolyLogFunction.Polylog(n, -iz));
        }
    }
}
