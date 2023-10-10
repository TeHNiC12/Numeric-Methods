using Lab_2.Core;
using System;

namespace Lab_2.MVVM.Model
{
    public class MultyNewtonModel
    {
        public double X1L;
        public double X1R;
        public double X2L;
        public double X2R;


        public (double X1, double X2, int step) Solve (double Accuracy)
        {
            double[] xPrev = new double[2];
            double[] xCur = new double[2];
            xCur[0] = (X1L + X1R) / 2;
            xCur[1] = (X2L + X2R) / 2;
            int step;
            for (step = 1; step > 0; step++)
            {
                xPrev[0] = xCur[0];
                xPrev[1] = xCur[1];
                if (CheckJakobian(xPrev))
                {
                    double[] delta = FindDelta(xPrev);
                    xCur[0] = xPrev[0] + delta[0];
                    xCur[1] = xPrev[1] + delta[1];
                    double error = Math.Max(Math.Abs(xCur[0] - xPrev[0]), Math.Abs(xCur[1] - xPrev[1]));
                    if (error <= Accuracy)
                    {
                        break;
                    }
                }
                else
                {
                    throw new Exception($"Jacobian = 0 on step {step} method is unaplicable");
                }
            }
            return (xCur[0], xCur[1], step);
        }

        private double[] FindDelta (double[] X)
        {
            MatExt Condition = new MatExt()
            {
                A = Matrix.CreateEmpty(2, 2),
                B = Matrix.CreateEmpty(2, 1)
            };

            Condition.A[0, 0] = df1X1(X[0], X[1]);
            Condition.A[0, 1] = df1X2(X[0], X[1]);
            Condition.A[1, 0] = df2X1(X[0], X[1]);
            Condition.A[1, 1] = df2X2(X[0], X[1]);

            Condition.B[0, 0] = -f1X1X2(X[0], X[1]);
            Condition.B[1, 0] = -f2X1X2(X[0], X[1]);

            return Gaussian.Execute(Condition);
        }
        private bool CheckJakobian (double[] X)
        {
            double[,] Jakobian =
            {
                {
                    df1X1(X[0], X[1]),
                    df1X2(X[0], X[1])
                },
                {
                    df2X1(X[0], X[1]),
                    df2X2(X[0], X[1])
                }
            };

            if (Matrix.Determinant(Jakobian) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Func<double, double, double> f1X1X2 = (x1, x2) => 3 * x1 - Math.Cos(x2);
        private Func<double, double, double> df1X1 = (x1, x2) => 3;
        private Func<double, double, double> df1X2 = (x1, x2) => Math.Sin(x2);

        public Func<double, double, double> f2X1X2 = (x1, x2) => 3 * x2 - Math.Pow(Math.E, x1);
        private Func<double, double, double> df2X1 = (x1, x2) => -Math.Pow(Math.E, x1);
        private Func<double, double, double> df2X2 = (x1, x2) => 3;

        public Func<double, double> Func1 = x => Math.Acos(3 * x);
        public Func<double, double> Func2 = x => Math.Pow(Math.E, x) / 3;
    }
}
