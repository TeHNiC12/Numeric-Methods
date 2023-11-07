using Lab_3.Core;
using System;

namespace Lab_3.MVVM.Model
{
    public class SubTask3Model
    {
        public double LeastSquaresAppriximation (double x)
        {
            int n = coefsA.Length - 1;
            double Fx = 0;

            for (int i = 0; i <= n; i++)
            {
                Fx += coefsA[i] * Math.Pow(x, i);
            }
            return Fx;
        }

        public void CalculateCoeffsA (double[] Xi, double[] Yi, int n)
        {
            int N = Xi.Length - 1;
            MatExt MNK = new()
            {
                A = new double[n + 1, n + 1],
                B = new double[n + 1, 1]
            };

            for (int k = 0; k <= n; k++)
            {
                for (int i = 0; i <= n; i++)
                {
                    double coef = 0;
                    for (int j = 0; j <= N; j++)
                    {
                        coef += Math.Pow(Xi[j], k + i);
                    }
                    MNK.A[k, i] = coef;
                }
                double b = 0;
                for (int j = 0; j <= N; j++)
                {
                    b += Yi[j] * Math.Pow(Xi[j], k);
                }
                MNK.B[k, 0] = b;
            }

            coefsA = LU.Execute(MNK);
        }

        public double CalculateError (double[] Xi, double[] Yi)
        {
            int N = Xi.Length - 1;
            double error = 0;

            for (int j = 0; j <= N; j++)
            {
                error += Math.Pow(LeastSquaresAppriximation(Xi[j]) - Yi[j], 2);
            }
            return error;
        }

        private LUDecomposition LU = new();
        private double[] coefsA;
    }
}
