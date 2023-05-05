namespace Lab_1.SubtaskSolvers
{
    public class LUDecomposition : SubTask<MatExt>
    {
        public override void Execute (MatExt input)
        {
            Console.WriteLine("Task Conditions:");
            Console.WriteLine("Matrix A:");
            Matrix.Print(input.A);
            Console.WriteLine("Matrix B:");
            Matrix.Print(input.B);
            Console.WriteLine("A inverted:");
            Matrix.Print(Matrix.Invert(input.A));
            Console.WriteLine($"Determinant A = {Matrix.Determinant(input.A)}\n");
            Console.WriteLine("LU decomposition:");
            LUDecompose(input);
            Console.WriteLine("Matrix L:");
            Matrix.Print(GetL(input.A));
            Console.WriteLine("Matrix U:");
            Matrix.Print(GetU(input.A));
            Console.WriteLine("Result:");
            float[] result = LUSolve(input);
            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine($"X{i + 1} = {result[i]:f}");
            }
        }
        private void LUDecompose (MatExt input)
        {
            int size = input.A.GetLength(0);
            int columns_A = input.A.GetLength(1);
            for (int i = 0; i < size - 1; i++)
            {
                int exchRow = i + 1;
                while (input.A[i, i] == 0)
                {
                    Matrix.SwichString(input, i, exchRow);
                    exchRow++;
                }
                for (int j = i + 1; j < size; j++)
                {
                    float k = -input.A[j, i] / input.A[i, i];
                    input.A[j, i] = -k;
                    for (int z = i + 1; z < columns_A; z++)
                    {
                        input.A[j, z] = input.A[j, z] + k * input.A[i, z];
                    }
                }
            }
        }
        private float[] LUSolve (MatExt input)
        {
            int size = input.A.GetLength(0);
            float[] Z = new float[size];
            Z[0] = input.B[0, 0];
            for (int i = 1; i < size; i++)
            {
                float sum = 0f;
                for (int j = 0; j < i; j++)
                {
                    sum += GetL(input.A, i, j) * Z[j];
                }
                Z[i] = input.B[i, 0] - sum;
            }
            float[] X = new float[size];
            for (int i = size - 1; i >= 0; i--)
            {
                float sum = 0f;
                for (int j = i + 1; j < size; j++)
                {
                    sum += GetU(input.A, i, j) * X[j];
                }
                X[i] = 1 / GetU(input.A, i, i) * (Z[i] - sum);
            }
            return X;
        }
        private float[,] GetL (float[,] LU)
        {
            int size = LU.GetLength(0);
            float[,] L = Matrix.CreateEmpty(size, size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i < j)
                    {
                        L[i, j] = 0;
                    }
                    else if (i > j)
                    {
                        L[i, j] = LU[i, j];
                    }
                    else
                    {
                        L[i, j] = 1;
                    }
                }
            }
            return L;
        }
        private float GetL (float[,] LU, int i, int j)
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
        private float[,] GetU (float[,] LU)
        {
            int size = LU.GetLength(0);
            float[,] U = Matrix.CreateEmpty(size, size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i > j)
                    {
                        U[i, j] = 0;
                    }
                    else
                    {
                        U[i, j] = LU[i, j];
                    }
                }
            }
            return U;
        }
        private float GetU (float[,] LU, int i, int j)
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
    }
}