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
            double q = CalculateQ();
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

        private double CalculateQ ()
        {
            double Q = -1f;
            for (double x1 = X1L; x1 <= X1R; x1 += 0.01f)
            {
                for (double x2 = X2L; x2 <= X2R; x2 += 0.01f)
                {
                    double q = Math.Round(
                        Math.Max
                            (
                                Math.Abs(DPhi1X1(x1, x2)) + Math.Abs(DPhi1X2(x1, x2)),
                                Math.Abs(DPhi2X1(x1, x2)) + Math.Abs(DPhi2X2(x1, x2))
                            ), 2, MidpointRounding.AwayFromZero);
                    Q = Math.Max(Q, q);
                }
            }

            if (Q > 0 & Q < 1)
            {
                return Q;
            }
            else
            {
                throw new Exception("q не удоветворяет требованиям: 0 < q < 1 метод не сходится");
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
