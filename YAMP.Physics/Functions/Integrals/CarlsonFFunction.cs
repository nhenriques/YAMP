﻿using System;
using YAMP;
using YAMP.Numerics;

namespace YAMP.Physics
{
    [Description("In mathematics, the Carlson symmetric forms of elliptic integrals are a small canonical set of elliptic integrals to which all others may be reduced. They are a modern alternative to the Legendre forms. The Legendre forms may be expressed in terms of the Carlson forms and vice versa. This function computes the elliptic integral named R_F(x, y, z).")]
    [Kind(PopularKinds.Function)]
    class CarlsonFFunction : ArgumentFunction
    {
        [Description("Computes the value of the funtion R_F(x, y, z) by solving the elliptic integral for the real arguments x, y, z.")]
        [Example("carlsonf(1, 2.5, 1.5)", "Evaluates R_F at x = 1, y = 2.5 and z = 1.5.")]
        public ScalarValue Function(ScalarValue x, ScalarValue y, ScalarValue z)
        {
            return new ScalarValue(CarlsonF(x.Re, y.Re, z.Re));
        }

        #region Algorithm

        public static double CarlsonF(double x, double y, double z)
        {
            if (x < 0.0)
                throw new YAMPArgumentRangeException("x", 0.0);

            if (y < 0.0)
                throw new YAMPArgumentRangeException("y", 0.0);

            if (z < 0.0)
                throw new YAMPArgumentRangeException("z", 0.0);

            // if more than one is zero, the result diverges
            if (((x == 0.0) && ((y == 0.0) || (z == 0.0))) || ((y == 0.0) && (z == 0.0))) 
                return double.PositiveInfinity;

            for (int n = 0; n < 250; n++)
            {
                // find out how close we are to the expansion point
                double m = (x + y + z) / 3.0;
                double dx = (x - m) / m;
                double dy = (y - m) / m;
                double dz = (z - m) / m;
                double e = Math.Max(Math.Abs(dx), Math.Max(Math.Abs(dy), Math.Abs(dz)));

                if (e < 0.01)
                {
                    double E2 = dx * dy + dx * dz + dy * dz;
                    double E3 = dx * dy * dz;
                    double F = 1.0 - E2 / 10.0 - E3 / 14.0 + E2 * E2 / 24.0 + 3.0 * E2 * E3 / 44.0 - 5.0 / 208.0 * E2 * E2 * E2 + 3.0 / 104.0 * E3 * E3 - E2 * E2 * E3 / 16.0;
                    return F / Math.Sqrt(m);
                }

                // if we are not close enough, use the duplication theorem (DLMF 19.26.18) to move us closer
                double lambda = Math.Sqrt(x * y) + Math.Sqrt(x * z) + Math.Sqrt(y * z);

                x = (x + lambda) / 4.0;
                y = (y + lambda) / 4.0;
                z = (z + lambda) / 4.0;
            }

            throw new YAMPNotConvergedException("CarlsonF");
        }

        #endregion
    }
}
