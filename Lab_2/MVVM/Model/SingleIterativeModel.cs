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
        private bool CheckIntervalCorrectness ()
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
                throw new Exception("q не удоветворяет требованиям: 0 < q < 1");
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
    }
}
