using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_2.MVVM.Model
{
    public class SingleIterativeModel
    {
        public double A;
        public double B;
        private double Q;

        public (double X, int step) Solve (double Accuracy)
        {
            if (CheckIntervalCorrectness())
            {
                CalculateQ();
                double xCur = (A + B) / 2;
                double xPrev;
                int step;
                for (step = 1; step > 0; step++)
                {
                    xPrev = xCur;
                    xCur = Phi(xPrev);
                    if (((Q / (1 - Q)) * Math.Abs(xCur - xPrev)) <= Accuracy)
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

        private bool ContainsX ()
        {
            bool contains = true;
            contains = contains && F(A) * F(B) < 0;

            int sign1 = Math.Sign(FirstDerivativeF(A));
            int sign2 = Math.Sign(SecondDerivativeF(A));
            for (double x = A + 0.01f; x <= B; x += 0.01f)
            {
                int nsign1 = Math.Sign(FirstDerivativeF(x));
                int nsign2 = Math.Sign(SecondDerivativeF(x));
                contains = contains && sign1 == nsign1;
                contains = contains && sign2 == nsign2;
                sign1 = nsign1;
                sign2 = nsign2;
            }
            return contains;
        }

        private bool CheckIntervalCorrectness ()
        {
            if (ContainsX())
            {
                if (Cond(A) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new Exception("Интервал не содержит корня");
            }
        }
        private void CalculateQ ()
        {
            List<double> phiRange = new();
            for (double i = A; i < B; i += 0.01f)
            {
                phiRange.Add(Math.Abs(DerivativePhi(i)));
            }
            double q = Math.Round(phiRange.Max(), 2, MidpointRounding.AwayFromZero);
            if (q > 0 & q < 1)
            {
                Q = q;
            }
            else
            {
                throw new Exception("q не удоветворяет требованиям: 0 < q < 1 => метод не сходится");
            }
        }

        public Func<double, double> Func1 = x => Math.Pow(4, x);
        public Func<double, double> Func2 = x => 5 * x + 2;

        /*private Func<double, double> Phi1 = x => (Math.Pow(4, x) - 2) / 5;
        private Func<double, double> DerivativePhi1 = x => Math.Log(x) * Math.Pow(4, x) / 5;

        private Func<double, double> Phi2 = x => Math.Log2(5 * x + 2) / 2;
        private Func<double, double> DerivativePhi2 = x => 5 / (2 * Math.Log(2) * (5 * x + 2));*/

        private Func<double, double> Cond = x => 5 * x + 2;
        private Func<double, double> Phi = x => Math.Log2(5 * x + 2) / 2;
        private Func<double, double> DerivativePhi = x => 5 / (2 * Math.Log(2) * (5 * x + 2));

        private Func<double, double> F = x => Math.Pow(4, x) - 5 * x - 2;
        private Func<double, double> FirstDerivativeF = x => Math.Pow(4, x) * Math.Log(4) - 5;
        private Func<double, double> SecondDerivativeF = x => Math.Pow(4, x) * Math.Pow(Math.Log(4), 2);

    }
}
