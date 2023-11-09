using System;

namespace Lab_4.BaseValues
{
    public class SubTask2Values
    {
        public double a = 0f;
        public double b = 1f;

        public int n = 10;
        public double epsilon = 0.0001f;

        public double alpha = 0f;
        public double betta = 1f;

        public double delta = 1f;
        public double gamma = 1f;

        public double y0 = 0f;
        public double y1 = -0.75f;

        public Func<double, double> TrueF = x => x - 3f + (1f / (x + 1f));

        public Func<double, double, double, double> ddY = (x, y, dy) => -((x - 3) / (Math.Pow(x, 2) - 1)) * dy + (1f / (Math.Pow(x, 2) - 1)) * y;
    }
}
