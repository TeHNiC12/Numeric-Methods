using System;

namespace Lab_3.BaseValues
{
    public static class SubTask1Values
    {
        public static double[] xiA = { -2, -1, 0, 1 };
        public static double[] xiAS = { -2, -1, 0 };
        public static double[] xiB = { -2, -1, 0.2, 1 };
        public static double[] xiBS = { -2, -1, 0.2 };
        public static double xStar = -0.5;
        public static Func<double, double> Func = x => Math.Pow(Math.E, x) + x;
    }
}
