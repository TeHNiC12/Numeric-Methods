using System;
using System.Linq;

namespace Lab_4.MVVM.Model
{
    public class SubTask1Model
    {
        public double[] EulerExplicit (double a, double b, double h, double y0, double dy0, Func<double, double, double, double> ddY)
        {
            double[] Xk = Dscretize(a, b, h);
            double[] Yk = new double[Xk.Length];
            double[] dYk = new double[Xk.Length];

            Yk[0] = y0;
            dYk[0] = dy0;

            for (int k = 1; k < Xk.Length; k++)
            {
                dYk[k] = dYk[k - 1] + h * ddY(Xk[k - 1], Yk[k - 1], dYk[k - 1]);
                Yk[k] = Yk[k - 1] + h * dYk[k - 1];
            }
            return Yk;
        }

        public double[] EulerImplicit (double a, double b, double h, double y0, double dy0, Func<double, double, double, double> ddY)
        {
            double[] Xk = Dscretize(a, b, h);
            double[] Yk = new double[Xk.Length];
            double[] dYk = new double[Xk.Length];

            Yk[0] = y0;
            dYk[0] = dy0;

            for (int k = 0; k < Xk.Length - 1; k++)
            {
                (double resY, double resDy) res = SecantSolve(Yk[k], dYk[k], Xk[k + 1], h, 1f / (Xk.Length * 10f), ddY);
                Yk[k + 1] = res.resY;
                dYk[k + 1] = res.resDy;
            }
            return Yk;
        }

        public (double[], double[]) RungeKnutta4Order (double a, double b, double h, double y0, double dy0, Func<double, double, double, double> ddY)
        {
            double[] Xk = Dscretize(a, b, h);
            double[] Yk = new double[Xk.Length];
            double[] dYk = new double[Xk.Length];

            Yk[0] = y0;
            dYk[0] = dy0;

            for (int k = 1; k < Xk.Length; k++)
            {
                double k1 = h * dYk[k - 1];
                double l1 = h * ddY(Xk[k - 1], Yk[k - 1], dYk[k - 1]);

                double k2 = h * (dYk[k - 1] + l1 / 2f);
                double l2 = h * ddY(Xk[k - 1] + h / 2f, Yk[k - 1] + k1 / 2f, dYk[k - 1] + l1 / 2f);

                double k3 = h * (dYk[k - 1] + l2 / 2f);
                double l3 = h * ddY(Xk[k - 1] + h / 2f, Yk[k - 1] + k2 / 2f, dYk[k - 1] + l2 / 2f);

                double k4 = h * (dYk[k - 1] + l3);
                double l4 = h * ddY(Xk[k - 1] + h, Yk[k - 1] + k3, dYk[k - 1] + l3);

                Yk[k] = Yk[k - 1] + (k1 + 2 * k2 + 2 * k3 + k4) / 6f;
                dYk[k] = dYk[k - 1] + (l1 + 2 * l2 + 2 * l3 + l4) / 6f;
            }
            return (Yk, dYk);
        }

        public double[] RungeKnutta2Order (double a, double b, double h, double y0, double dy0, Func<double, double, double, double> ddY)
        {
            double[] Xk = Dscretize(a, b, h);
            double[] Yk = new double[Xk.Length];
            double[] dYk = new double[Xk.Length];

            Yk[0] = y0;
            dYk[0] = dy0;

            for (int k = 1; k < Xk.Length; k++)
            {
                double k1 = h * dYk[k - 1];
                double l1 = h * ddY(Xk[k - 1], Yk[k - 1], dYk[k - 1]);

                double k2 = h * (dYk[k - 1] + l1 / 2f);
                double l2 = h * ddY(Xk[k - 1] + h / 2f, Yk[k - 1] + k1 / 2f, dYk[k - 1] + l1 / 2f);

                Yk[k] = Yk[k - 1] + (k1 + k2) / 2f;
                dYk[k] = dYk[k - 1] + (l1 + l2) / 2f;
            }
            return Yk;
        }

        public double[] Adams4Order (double a, double b, double h, double y0, double dy0, Func<double, double, double, double> ddY)
        {
            (double[], double[]) YdY = RungeKnutta4Order(a, b, h, y0, dy0, ddY);
            double[] Xk = Dscretize(a, b, h);
            double[] Yk = YdY.Item1;
            double[] dYk = YdY.Item2;

            for (int k = 3; k < Xk.Length - 1; k++)
            {
                dYk[k + 1] = dYk[k] + h * (55f * ddY(Xk[k], Yk[k], dYk[k]) - 59f * ddY(Xk[k - 1], Yk[k - 1], dYk[k - 1]) + 37f * ddY(Xk[k - 2], Yk[k - 2], dYk[k - 2]) - 9f * ddY(Xk[k - 3], Yk[k - 3], dYk[k - 3])) / 24f;
                Yk[k + 1] = Yk[k] + h * (55f * dYk[k] - 59f * dYk[k - 1] + 37f * dYk[k - 2] - 9f * dYk[k - 3]) / 24f;
            }
            return Yk;
        }

        public double[] Adams2Order (double a, double b, double h, double y0, double dy0, Func<double, double, double, double> ddY)
        {
            (double[], double[]) YdY = RungeKnutta4Order(a, b, h, y0, dy0, ddY);
            double[] Xk = Dscretize(a, b, h);
            double[] Yk = YdY.Item1;
            double[] dYk = YdY.Item2;

            for (int k = 1; k < Xk.Length - 1; k++)
            {
                dYk[k + 1] = dYk[k] + h * (3f * ddY(Xk[k], Yk[k], dYk[k]) - ddY(Xk[k - 1], Yk[k - 1], dYk[k - 1])) / 2f;
                Yk[k + 1] = Yk[k] + h * (3f * dYk[k] - dYk[k - 1]) / 2f;
            }
            return Yk;
        }

        public double[] Dscretize (double a, double b, double h)
        {
            int N = (int) ((b - a) / h + 2);

            double[] Xk = new double[N];

            Xk[0] = a;
            for (int i = 1; i < N; i++)
            {
                Xk[i] = Xk[i - 1] + h;
            }
            Xk[N - 1] = b;
            return Xk;
        }

        public double Error (double[] Y, double[] TrueY)
        {
            double[] error = new double[Y.Length];
            for (int i = 0; i < Y.Length; i++)
            {
                error[i] = Math.Abs(TrueY[i] - Y[i]);
            }
            return error.Max();
        }

        public double[] RungeRombergRichardson (double[] Fh, double[] Fkh, int k, double p)
        {
            double[] rrr = new double[Fh.Length];
            for (int i = 0; i < Fh.Length; i++)
            {
                rrr[i] = Fh[i] + (Fh[i] - Fkh[i * k]) / (Math.Pow(k, p) - 1f);
            }
            return rrr;
        }

        public (double resY, double resDy) SecantSolve (double y, double dy, double x, double h, double eps, Func<double, double, double, double> ddY)
        {
            double tempY = y;
            double tempDy = dy;

            double resY = 0f;
            double resDy = 0f;

            for (int i = 0; i < (int) (1f / eps); i++)
            {
                resY = y + h * tempY;
                resDy = dy + h * ddY(x, tempY, tempDy);
                tempY = resY;
                tempDy = resDy;
            }

            return (resY, resDy);
        }
    }
}
