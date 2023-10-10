using System;

namespace Lab_2.MVVM.Model
{
    public class SingleNewtonModel
    {
        public double A;
        public double B;

        public (double X, int step) Solve (double Accuracy)
        {
            if (CheckIntervalCorrectness())
            {
                double xCur;
                if (Func(A) * Derivative2(A) > 0)
                {
                    xCur = A;
                }
                else if (Func(B) * Derivative2(B) > 0)
                {
                    xCur = B;
                }
                else
                {
                    throw new Exception("Impossible to select X0");
                }
                double xPrev;
                int step;

                for (step = 1; step > 0; step++)
                {
                    xPrev = xCur;
                    xCur = xPrev - Func(xPrev) / Derivative1(xPrev);
                    if (Math.Abs(xCur - xPrev) <= Accuracy)
                    {
                        break;
                    }
                }
                return (xCur, step);
            }
            else
            {
                throw new Exception("Интервал не верен, метод не сходится");
            }
        }

        private bool CheckIntervalCorrectness ()
        {
            bool correctness = true;
            correctness = correctness && Func(A) * Func(B) < 0;

            int sign1 = Math.Sign(Derivative1(A));
            int sign2 = Math.Sign(Derivative2(A));
            for (double x = A; x <= B; x += 0.01f)
            {
                double derVal = Derivative1(x);
                int nsign1 = Math.Sign(derVal);
                int nsign2 = Math.Sign(Derivative2(x));
                correctness = correctness && derVal != 0;
                correctness = correctness && sign1 == nsign1;
                correctness = correctness && sign2 == nsign2;
                sign1 = nsign1;
                sign2 = nsign2;
            }
            return correctness;
        }

        private Func<double, double> Func = x => Math.Pow(4, x) - 5 * x - 2;
        private Func<double, double> Derivative1 = x => Math.Log(4) * Math.Pow(4, x) - 5;
        private Func<double, double> Derivative2 = x => Math.Pow(Math.Log(4), 2) * Math.Pow(4, x);

        public Func<double, double> Func1 = x => Math.Pow(4, x);
        public Func<double, double> Func2 = x => 5 * x + 2;
    }
}
