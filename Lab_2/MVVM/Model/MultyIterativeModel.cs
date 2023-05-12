using System;

namespace Lab_2.MVVM.Model
{
    public class MultyIterativeModel
    {
        public double X1L;
        public double X1R;
        public double X2L;
        public double X2R;

        public (double X1, double X2, int step) Solve (double Accuracy)
        {
            double[] xPrev = new double[2];
            double[] xCur = new double[2];
            xCur[0] = (X1L + X1R) / 2;
            xCur[1] = (X2L + X2R) / 2;
            double q = CalculateQ(xCur);
            int step;
            for (step = 1; step > 0; step++)
            {
                xPrev[0] = xCur[0];
                xPrev[1] = xCur[1];
                xCur[0] = Phi1(xPrev[0], xPrev[1]);
                xCur[1] = Phi2(xPrev[0], xPrev[1]);
                double error = q / (1 - q) * Math.Max(Math.Abs(xCur[0] - xPrev[0]), Math.Abs(xCur[1] - xPrev[1]));
                if (error <= Accuracy)
                {
                    break;
                }
            }
            return (xCur[0], xCur[1], step);
        }

        private double CalculateQ (double[] X)
        {
            double q = Math.Round(
                Math.Max
                (
                    Math.Abs(DPhi1X1(X[0], X[1])) + Math.Abs(DPhi1X2(X[0], X[1])),
                    Math.Abs(DPhi2X1(X[0], X[1])) + Math.Abs(DPhi2X2(X[0], X[1]))
                    ), 2, MidpointRounding.AwayFromZero);
            if (q > 0 & q < 1)
            {
                return q;
            }
            else
            {
                throw new Exception("q не удоветворяет требованиям: 0 < q < 1");
            }
        }

        public Func<double, double, double> f1X1X2 = (x1, x2) => 3 * x1 - Math.Cos(x2);
        public Func<double, double, double> f2X1X2 = (x1, x2) => 3 * x2 - Math.Pow(Math.E, x1);

        public Func<double, double, double> Phi1 = (x1, x2) => Math.Cos(x2) / 3;
        public Func<double, double, double> DPhi1X1 = (x1, x2) => 0;
        public Func<double, double, double> DPhi1X2 = (x1, x2) => -Math.Sin(x2) / 3;

        public Func<double, double, double> Phi2 = (x1, x2) => Math.Pow(Math.E, x1) / 3;
        public Func<double, double, double> DPhi2X1 = (x1, x2) => Math.Pow(Math.E, x1) / 3;
        public Func<double, double, double> DPhi2X2 = (x1, x2) => 0;

        public Func<double, double> Func1 = x => Math.Acos(3 * x);
        public Func<double, double> Func2 = x => Math.Pow(Math.E, x) / 3;
    }
}
