﻿using System;
using YAMP;

namespace YAMP.Numerics
{
    public class CGSolver : IterativeSolver
    {
        public CGSolver(MatrixValue A) : base(A)
        {
        }

        public override MatrixValue Solve(MatrixValue b)
        {
            MatrixValue x = X0;

            if (x == null)
                x = new MatrixValue(b.DimensionY, b.DimensionX);
            else if (x.DimensionX != b.DimensionX)
                throw new DimensionException(x.DimensionX, b.DimensionX);
            else if (x.DimensionY != b.DimensionY)
                throw new DimensionException(x.DimensionY, b.DimensionY);

            var r = b - A * x;
            var p = r.Clone();
            var l = Math.Max(A.Length, MaxIterations);
            var rsold = Dot(r, r);

            for(var i = 1; i < l; i++)
            {
                var Ap = A * p;
                var alpha = rsold / Dot(p, Ap);
                x = x + alpha * p;
                r = r - alpha * Ap;
                var rsnew = Dot(r, r);

                if (rsnew.Abs().Value < Tolerance)
                    break;

                p = r + rsnew / rsold * p;
                rsold = rsnew;
            }

            return x;
        }

        private ScalarValue Dot(MatrixValue l, MatrixValue r)
        {
            return l.Adjungate().Multiply(r) as ScalarValue;
        }
    }
}