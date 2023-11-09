using System;
using System.Linq;

namespace Lab_4.MVVM.Model
{
    public class SubTask2Model
    {

        public Tuple<double[], double[], double[]> ShootingMethod (double a, double b, double h, double alpha, double betta, double ya, double delta, double gamma, double yn, double etta1, double eps, Func<double, double, double, double> ddY)
        {
            double phi = eps * 100f;
            double etta2 = etta1 + eps * 100f;

            while (Math.Abs(phi) > eps)
            {
                Tuple<double[], double[], double[]> res1 = RungeKnutta4Order(a + eps, b, etta1, GetZ(etta1, ya, alpha, betta), ddY, h);
                Tuple<double[], double[], double[]> res2 = RungeKnutta4Order(a + eps, b, etta2, GetZ(etta2, ya, alpha, betta), ddY, h);


                Tuple<double, double> etaNext = GetEtaNext(etta1, etta2, delta, gamma, yn, res1, res2);
                etta1 = etta2;
                phi = etaNext.Item1;
                etta2 = etaNext.Item2;
            }

            return RungeKnutta4Order(a + eps, b, etta2, GetZ(etta2, ya, alpha, betta), ddY, h);
        }

        private double GetZ (double etta, double ya, double alpha, double betta)
        {
            return (ya - alpha * etta) / betta;
        }

        private Tuple<double, double> GetEtaNext (double etta1, double etta2, double delta, double gamma, double yn, Tuple<double[], double[], double[]> res1, Tuple<double[], double[], double[]> res2)
        {
            double yb_1 = res1.Item1[res1.Item1.Length - 1];
            double zb_1 = res1.Item2[res1.Item2.Length - 1];
            double phi_1 = delta * yb_1 + gamma * zb_1 - yn;

            double yb = res2.Item1[res2.Item1.Length - 1];
            double zb = res2.Item2[res2.Item2.Length - 1];
            double phi_2 = delta * yb + gamma * zb - yn;

            return new Tuple<double, double>(phi_2, etta2 - (etta2 - etta1) / (phi_2 - phi_1) * phi_2);
        }

        private Tuple<double[], double[], double[]> RungeKnutta4Order (double a, double b, double y0, double dy0, Func<double, double, double, double> ddY, double h)
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
            return new Tuple<double[], double[], double[]>(dYk, Yk, Xk);
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
    }
}
