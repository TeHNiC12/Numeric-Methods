using System;

namespace Lab_3.MVVM.Model
{
    public class SubTask5Model
    {
        public double RectangleIntegral (double X0, double Xk, double h, Func<double, double> Y)
        {
            double[] Xi = GetRange(X0, Xk, h);
            int N = Xi.Length - 1;
            double integral = 0;

            for (int i = 1; i <= N; i++)
            {
                integral += h * Y((Xi[i - 1] + Xi[i]) / 2f);
            }
            return integral;
        }

        public double TrapezoidIntegral (double X0, double Xk, double h, Func<double, double> Y)
        {
            double[] Xi = GetRange(X0, Xk, h);
            int N = Xi.Length - 1;
            double sum = 0;

            for (int i = 1; i <= N; i++)
            {
                sum += (Y(Xi[i]) + Y(Xi[i - 1])) * h;
            }
            return 0.5f * sum;
        }

        public double SimpsonIntegral (double X0, double Xk, double h, Func<double, double> Y)
        {
            double[] Xi = GetRange(X0, Xk, h);
            int N = Xi.Length - 1;
            double sum = Y(Xi[0]) + Y(Xi[N]);

            if (N % 2 != 0)
            {
                throw new Exception("Количество промежутков интегрирования не четное");
            }
            else
            {
                for (int i = 1; i < N; i++)
                {
                    if (i % 2 == 0)
                    {
                        sum += 2 * Y(Xi[i]);
                    }
                    else
                    {
                        sum += 4 * Y(Xi[i]);
                    }
                }
                return (h / 3f) * sum;
            }
        }

        public double RungeRombergRichardson (double Fh, double Fkh, double k, double precisionOrder)
        {
            return Fh + (Fh - Fkh) / (Math.Pow(k, precisionOrder) - 1);
        }

        private double[] GetRange (double X0, double Xk, double h)
        {
            int N = (int) ((Xk - X0) / h + 1);

            double[] Xi = new double[N];

            Xi[0] = X0;
            for (int i = 1; i < N - 1; i++)
            {
                Xi[i] = Xi[i - 1] + h;
            }
            Xi[N - 1] = Xk;
            return Xi;
        }
    }
}
