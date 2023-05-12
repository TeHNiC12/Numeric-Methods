using System;

namespace Lab_2.MVVM.Model
{
    public class SingleNewtonModel
    {
        public double A;
        public double B;

        public (double X, int step) Solve (double Accuracy)
        {
            double xCur = (A + B) / 2;
            double xPrev;
            int step;
            if (CheckIntervalCorrectness(xCur))
            {
                for (step = 1; step > 0; step++)
                {
                    xPrev = xCur;
                    xCur = xPrev - Func(xPrev) / Derivative1(xPrev);
                    if (Math.Abs(xCur - xPrev) < Accuracy)
                    {
                        break;
                    }
                }
                return (xCur, step);
            }
            else
            {
                throw new Exception("Interval not correct");
            }
        }

        private bool CheckIntervalCorrectness (double X0)
        {
            bool sign1 = Math.Sign(Derivative1(A)) == Math.Sign(Derivative1(B));
            bool sign2 = Math.Sign(Derivative2(A)) == Math.Sign(Derivative2(B));
            bool check1 = Math.Sign(Func(A) * Func(B)) == -1;
            bool check2 = Math.Sign(Func(X0) * Derivative2(X0)) == 1;
            return sign1 & sign2 & check1 & check2;
        }

        private Func<double, double> Func = x => Math.Pow(4, x) - 5 * x - 2;
        private Func<double, double> Derivative1 = x => Math.Log(4) * Math.Pow(4, x) - 5;
        private Func<double, double> Derivative2 = x => Math.Pow(Math.Log(4), 2) * Math.Pow(4, x);

        public Func<double, double> Func1 = x => Math.Pow(4, x);
        public Func<double, double> Func2 = x => 5 * x + 2;
    }
}
