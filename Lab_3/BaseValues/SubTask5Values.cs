using System;

namespace Lab_3.BaseValues
{
    public static class SubTask5Values
    {
        public static double X0 = -2f;
        public static double Xk = 2f;
        public static double h1 = 1f;
        public static double h2 = 0.5f;

        public static Func<double, double> Y = x => 1f / (256f - Math.Pow(x, 4));
    }
}
