using System;

namespace Lab_3.Core
{
    public class LUDecomposition
    {
        public double[] Execute (MatExt input)
        {
            double detA = Determinant(input.A);
            if (detA == 0)
            {
                throw new Exception("Determinant = 0, impossible to solve");
            }
            else
            {
                MatExt LUB = LUDecompose(input);
                return LUSolve(LUB);
            }
        }

        private MatExt LUDecompose (MatExt input)
        {
            int size = input.A.GetLength(0);
            double[,] L = CreateIdentity(size);

            for (int k = 0; k < size - 1; k++)
            {
                double[,] M = CreateIdentity(size);
                double[,] invM = CreateIdentity(size);
                for (int i = k + 1; i < size; i++)
                {
                    double mu = input.A[i, k] / input.A[k, k];
                    M[i, k] = -mu;
                    invM[i, k] = mu;
                }
                input.A = Multiply(M, input.A);
                L = Multiply(L, invM);
            }

            for (int i = 1; i < size; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    input.A[i, j] = L[i, j];
                }
            }

            return input;
        }

        private double[] LUSolve (MatExt input)
        {
            int size = input.A.GetLength(0);
            double[] Z = new double[size];
            Z[0] = input.B[0, 0];
            for (int i = 1; i < size; i++)
            {
                double sum = 0f;
                for (int j = 0; j < i; j++)
                {
                    sum += GetL(input.A, i, j) * Z[j];
                }
                Z[i] = input.B[i, 0] - sum;
            }
            double[] X = new double[size];
            for (int i = size - 1; i >= 0; i--)
            {
                double sum = 0f;
                for (int j = i + 1; j < size; j++)
                {
                    sum += GetU(input.A, i, j) * X[j];
                }
                X[i] = 1 / GetU(input.A, i, i) * (Z[i] - sum);
            }
            return X;
        }

        private double GetL (double[,] LU, int i, int j)
        {
            if (i < j)
            {
                return 0;
            }
            else if (i > j)
            {
                return LU[i, j];
            }
            else
            {
                return 1;
            }
        }

        private double GetU (double[,] LU, int i, int j)
        {
            if (i > j)
            {
                return 0;
            }
            else
            {
                return LU[i, j];
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

        public static double[,] CreateIdentity (int size)
        {
            double[,] A = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == j)
                    {
                        A[i, j] = 1.0f;
                    }
                    else
                    {
                        A[i, j] = 0.0f;
                    }
                }
            }
            return A;
        }

        public static double[,] Multiply (double[,] A, double[,] B)
        {
            if (A.GetLength(1) == B.GetLength(0))
            {
                int rows = A.GetLength(0);
                int columns = B.GetLength(1);
                int n = A.GetLength(1);
                double[,] C = new double[rows, columns];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        for (int k = 0; k < n; k++)
                        {
                            C[i, j] += A[i, k] * B[k, j];
                        }
                    }
                }
                return C;
            }
            else
            {
                throw new Exception("Can't multiply matrices: A columns amount doesn't match B rows count");
            }
        }

        public static double Determinant (double[,] A)
        {
            if (A.GetLength(0) == A.GetLength(1))
            {
                int size = A.GetLength(0);
                if (size == 1)
                {
                    return A[0, 0];
                }
                else if (size == 2)
                {
                    return ((A[0, 0] * A[1, 1]) - (A[0, 1] * A[1, 0]));
                }
                else
                {
                    double det = 0;
                    for (int i = 0; i < size; i++)
                    {
                        if (i % 2 == 0)
                        {
                            det += A[0, i] * Minor(A, 0, i);
                        }
                        else
                        {
                            det -= A[0, i] * Minor(A, 0, i);
                        }
                    }
                    return det;
                }
            }
            else
            {
                throw new Exception("Matrix isn't sqare");
            }
        }

        public static double Minor (double[,] A, int row, int column)
        {
            return Determinant(CreateMinorMatrix(A, row, column));
        }

        private static double[,] CreateMinorMatrix (double[,] A, int row, int column)
        {
            int size = A.GetLength(0);
            double[,] M = CreateEmpty(size - 1, size - 1);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i != row & j != column)
                    {
                        if (i < row)
                        {
                            if (j < column)
                            {
                                M[i, j] = A[i, j];
                            }
                            else
                            {
                                M[i, j - 1] = A[i, j];
                            }
                        }
                        else
                        {
                            if (j < column)
                            {
                                M[i - 1, j] = A[i, j];
                            }
                            else
                            {
                                M[i - 1, j - 1] = A[i, j];
                            }
                        }
                    }
                }
            }
            return M;
        }
    }
}