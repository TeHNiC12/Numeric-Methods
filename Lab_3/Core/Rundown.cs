using System;
using System.Windows;

namespace Lab_3.Core
{
    public class Rundown
    {
        public double[] Execute (MatExt input)
        {
            if (Check(input.A))
            {
                MatExt PQ = FindPQ(input);
                return Solve(PQ);
            }
            else
            {
                throw new Exception("Unable to solve");
            }
        }
        private MatExt FindPQ (MatExt input)
        {
            int size = input.A.GetLength(0);
            MatExt PQ = new()
            {
                A = CreateEmpty(size, 1),
                B = CreateEmpty(size, 1)
            };
            for (int i = 0; i < size; i++)
            {
                if (i == 0)
                {
                    PQ.A[i, 0] = -input.A[i, 2] / input.A[i, 1];
                    PQ.B[i, 0] = input.B[i, 0] / input.A[i, 1];
                }
                else
                {
                    PQ.A[i, 0] = -input.A[i, 2] / (input.A[i, 1] + input.A[i, 0] * PQ.A[i - 1, 0]);
                    PQ.B[i, 0] = (input.B[i, 0] - input.A[i, 0] * PQ.B[i - 1, 0]) / (input.A[i, 1] + input.A[i, 0] * PQ.A[i - 1, 0]);
                }
            }
            return PQ;
        }
        private double[] Solve (MatExt PQ)
        {
            int size = PQ.A.GetLength(0);
            double[] X = new double[size];
            for (int i = size - 1; i >= 0; i--)
            {
                if (i == size - 1)
                {
                    X[i] = PQ.B[i, 0];
                }
                else
                {
                    X[i] = PQ.A[i, 0] * X[i + 1] + PQ.B[i, 0];
                }
            }
            return X;
        }
        private bool Check (double[,] input)
        {
            int strict = 0;

            for (int i = 1; i < input.GetLength(0) - 2; i++)
            {
                if (input[i, 0] == 0)
                {
                    MessageBox.Show($"Coefficient a{i + 1} = 0, solution won't be accurate");
                    return false;
                }
                if (input[i, 0] == 0)
                {
                    MessageBox.Show($"Coefficient c{i + 1} = 0, solution won't be accurate");
                    return false;
                }
            }
            for (int i = 0; i < input.GetLength(1) - 1; i++)
            {
                double ac = Math.Abs(input[i, 0]) + Math.Abs(input[i, 2]);
                if (Math.Abs(input[i, 1]) > ac)
                {
                    strict++;
                }
                if (Math.Abs(input[i, 1]) < ac)
                {
                    MessageBox.Show($"|b{i + 1}| < |a{i + 1}| + |c{i + 1}|, solution won't be accurate");
                    return false;
                }
            }
            if (strict == 0)
            {
                MessageBox.Show("Inequasion |bi| < |ai| + |ci| isn't satisfied at least once, solution won't be accurate");
                return false;
            }
            else
            {
                return true;
            }
        }

        public static double[,] CreateEmpty (int rows, int columns)
        {
            double[,] A = new double[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    A[i, j] = 0.0f;
                }
            }
            return A;
        }
    }
}
