using System;

namespace Lab_3.MVVM.Model
{
    public class SubTask1Model
    {

        public double[] CalculateY (Func<double, double> func, double[] X)
        {
            double[] Y = new double[X.Length];
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = func(X[i]);
            }
            return Y;
        }

        public double LagrangeInterpolate (double[] Xi, double[] Fi, double x)
        {
            if (Xi.Length != Fi.Length)
            {
                throw new Exception("Количество значений функции F(Xi) не соотвествует количеству точек Xi");
            }

            int n = Xi.Length;
            double lagrangeVal = 0;

            for (int i = 0; i < n; i++)
            {
                double coef = 1;
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        coef *= (x - Xi[j]) / (Xi[i] - Xi[j]);
                    }
                }
                lagrangeVal += Fi[i] * coef;
            }
            return lagrangeVal;
        }

        public double NewtonInterpolate (double[] Xi, double[] Fi, double x)
        {
            if (Xi.Length != Fi.Length)
            {
                throw new Exception("Количество значений функции F(Xi) не соотвествует количеству точек Xi");
            }
            int n = Xi.Length;
            double Px = Fi[0];
            for (int k = 1; k < n; k++)
            {
                double P = 1;
                for (int i = 0; i < k; i++)
                {
                    P *= x - Xi[i];
                }
                Px += NewtonCoeffs[k] * P;
            }
            return Px;
        }

        public void CalculateNewtonCoeffs (double[] Xi, double[] Fi)
        {
            int n = Xi.Length;
            NewtonCoeffs = new double[n];

            for (int k = 1; k < n; k++)
            {
                double coef = 0;
                for (int i = 0; i <= k; i++)
                {
                    double P = 1;
                    for (int j = 0; j <= k; j++)
                    {
                        if (j != i)
                        {
                            P *= Xi[i] - Xi[j];
                        }
                    }
                    coef += Fi[i] / P;
                }
                NewtonCoeffs[k] = coef;
            }
        }

        double[] NewtonCoeffs;
    }
}
