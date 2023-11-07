using Lab_3.Core;
using System;

namespace Lab_3.MVVM.Model
{
    public class SubTask2Model
    {
        public double SplineInterpolation (double x, double[] Xi, double[] Fi)
        {
            int i = 1;
            for (int k = 1; k <= Xi.Length - 1; k++)
            {
                if (x >= Xi[k - 1] && x <= Xi[k])
                {
                    i = k;
                    break;
                }
            }
            return GetAi(i, Fi) + GetBi(i, Xi, Fi) * (x - Xi[i - 1]) + GetCi(i) * Math.Pow(x - Xi[i - 1], 2) + GetDi(i, Xi) * Math.Pow(x - Xi[i - 1], 3);
        }

        private double hi (int i, double[] Xi)
        {
            return Xi[i] - Xi[i - 1];
        }

        private double GetAi (int i, double[] Fi)
        {
            return Fi[i - 1];
        }

        private double GetBi (int i, double[] Xi, double[] Fi)
        {
            int n = Xi.Length - 1;

            if (i == n)
            {
                return (Fi[i] - Fi[i - 1]) / hi(i, Xi) - ((2f / 3f) * hi(i, Xi) * GetCi(i));
            }
            else
            {
                return ((Fi[i] - Fi[i - 1]) / hi(i, Xi)) - ((1f / 3f) * hi(i, Xi) * (GetCi(i + 1) + 2 * GetCi(i)));
            }
        }

        private double GetCi (int i)
        {
            if (i == 1)
            {
                return 0;
            }
            else
            {
                return coefC[i - 2];
            }
        }

        private double GetDi (int i, double[] Xi)
        {
            int n = Xi.Length - 1;

            if (i == n)
            {
                return -GetCi(i) / (3 * hi(i, Xi));
            }
            else
            {
                return (GetCi(i + 1) - GetCi(i)) / (3 * hi(i, Xi));
            }
        }

        public void FindC (double[] Xi, double[] Fi)
        {
            int n = Xi.Length - 1;
            MatExt MatC = new()
            {
                A = new double[n - 1, 3],
                B = new double[n - 1, 1]
            };

            MatC.A[0, 0] = 0;
            MatC.A[0, 1] = 2 * (hi(1, Xi) + hi(2, Xi));
            MatC.A[0, 2] = hi(2, Xi);
            MatC.B[0, 0] = 3 * ((Fi[2] - Fi[1]) / hi(2, Xi) - (Fi[1] - Fi[0]) / hi(1, Xi));

            for (int i = 3; i <= n - 1; i++)
            {
                MatC.A[i - 2, 0] = hi(i - 1, Xi);
                MatC.A[i - 2, 1] = 2 * (hi(i - 1, Xi) + hi(i, Xi));
                MatC.A[i - 2, 2] = hi(i, Xi);
                MatC.B[i - 2, 0] = 3 * ((Fi[i] - Fi[i - 1]) / hi(i, Xi) - (Fi[i - 1] - Fi[i - 2]) / hi(i - 1, Xi));
            }

            MatC.A[n - 2, 0] = hi(n - 1, Xi);
            MatC.A[n - 2, 1] = 2 * (hi(n - 1, Xi) + hi(n, Xi));
            MatC.A[n - 2, 2] = 0;
            MatC.B[n - 2, 0] = 3 * ((Fi[n] - Fi[n - 1]) / hi(n, Xi) - (Fi[n - 1] - Fi[n - 2]) / hi(n - 1, Xi));

            coefC = rundown.Execute(MatC);
        }

        private Rundown rundown = new Rundown();
        private double[] coefC;
    }
}
