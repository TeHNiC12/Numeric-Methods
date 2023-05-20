using System;

namespace Lab_3.MVVM.Model
{
    public class SubTask1Model
    {
        private double[] xiA = { -2, -1, 0, 1 };
        private double[] xiB = { -2, -1, 0.2, 1 };
        private double xStar = -0.5;
        public Func<double, double> Func = x => Math.Pow(Math.E, x) + x;

        private double[] FindY (double[] X)
        {
            double[] Y = new double[X.Length];
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = Func(X[i]);
            }
            return Y;
        }
    }
}
