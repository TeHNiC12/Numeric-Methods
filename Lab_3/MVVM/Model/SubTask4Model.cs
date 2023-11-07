using System;

namespace Lab_3.MVVM.Model
{
    public class SubTask4Model
    {
        public double Derivative1FirstOrder (int i, double[] Xi, double[] Yi)
        {
            int M = Xi.Length - 1;
            if (i >= 0 && i + 1 <= M)
            {
                return BaseFraction(i, Xi, Yi);
            }
            else
            {
                throw new Exception("Index i not in range");
            }
        }

        public double Derivative1SecondOrder (int i, double[] Xi, double[] Yi, double x)
        {
            int M = Xi.Length - 1;
            if (i >= 0 && i + 2 <= M)
            {
                return BaseFraction(i, Xi, Yi) + ((BaseFraction(i + 1, Xi, Yi) - BaseFraction(i, Xi, Yi)) / (Xi[i + 2] - Xi[i])) * (2 * x - Xi[i] - Xi[i + 1]);
            }
            else
            {
                throw new Exception("Index i not in range");
            }
        }

        public double Derivative2 (int i, double[] Xi, double[] Yi)
        {
            int M = Xi.Length - 1;
            if (i >= 0 && i + 2 <= M)
            {
                return 2 * ((BaseFraction(i + 1, Xi, Yi) - BaseFraction(i, Xi, Yi)) / (Xi[i + 2] - Xi[i]));
            }
            else
            {
                throw new Exception("Index i not in range");
            }
        }

        private double BaseFraction (int i, double[] Xi, double[] Yi)
        {
            return (Yi[i + 1] - Yi[i]) / (Xi[i + 1] - Xi[i]);
        }
    }
}
