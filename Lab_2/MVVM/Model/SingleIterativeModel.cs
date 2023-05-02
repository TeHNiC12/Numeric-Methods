using System;

namespace Lab_2.MVVM.Model
{
    public class SingleIterativeModel
    {
        private double MainFunc(double d)
        {
            return Math.Pow(4, d) - 5 * d - 2;
        }
        public double Func1(double d)
        {
            return Math.Pow(4, d);
        }
        public double Func2(double d)
        {
            return 5 * d + 2;
        }
        private double PhiX(double d)
        {
            return 0;
        }
    }
}
