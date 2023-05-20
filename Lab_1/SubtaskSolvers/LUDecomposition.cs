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
            float detA = Matrix.Determinant(input.A);
            if (detA == 0)
            {
                throw new Exception("Determinant = 0, impossible to solve");
            }
            else
            {
                Console.WriteLine($"Determinant A = {detA}\n");
                Console.WriteLine("LU decomposition:");
                MatExt LUB = LUDecompose(input);
                Console.WriteLine("Matrix L:");
                Matrix.Print(GetL(LUB.A));
                Console.WriteLine("Matrix U:");
                Matrix.Print(GetU(LUB.A));
                Console.WriteLine("Result:");
                float[] result = LUSolve(LUB);
                for (int i = 0; i < result.Length; i++)
                {
                    Console.WriteLine($"X{i + 1} = {result[i]:f}");
                }
            }
        }

        private MatExt LUDecompose (MatExt input)
        {
            int size = input.A.GetLength(0);
            float[,] L = Matrix.CreateIdentity(size);

            for (int k = 0; k < size - 1; k++)
            {
                float[,] M = Matrix.CreateIdentity(size);
                float[,] invM = Matrix.CreateIdentity(size);
                for (int i = k + 1; i < size; i++)
                {
                    float mu = input.A[i, k] / input.A[k, k];
                    M[i, k] = -mu;
                    invM[i, k] = mu;
                }
                input.A = Matrix.Multiply(M, input.A);
                L = Matrix.Multiply(L, invM);
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