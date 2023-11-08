using System;

namespace Lab_4.BaseValues
{
    public static class SubTask1Values
    {
        public static double a = 1f;
        public static double b = 2f;
        public static double h = 0.1f;

        public static double Y0 = 2 + Math.E;
        public static double dY0 = 1 + Math.E;

        public static Func<double, double> TrueF = x => x + 1 + Math.Pow(Math.E, x);

        public static Func<double, double, double, double> ddY = (x, y, dy) => ((x + 1f) / x) * dy - (1f / x) * y;
    }
}
