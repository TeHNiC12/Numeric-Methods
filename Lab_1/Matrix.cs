namespace Lab_1
{
    public static class Matrix
    {
        public static void Print(float[,] A)
        {
            int rows = A.GetLength(0);
            int columns = A.GetLength(1);
            string separatorH = string.Concat(Enumerable.Repeat("+------------", columns));
            separatorH = string.Concat(separatorH, "+\n");
            for (int i = 0; i < rows; i++)
            {
                Console.Write(separatorH);
                Console.Write("|");
                for (int j = 0; j < columns; j++)
                {
                    float current = A[i, j];
                    Console.Write(" ");
                    Console.Write($"{current,10:0.00}");
                    Console.Write(" ");
                    if (j < columns - 1)
                    {
                        Console.Write(":");
                    }
                }
                Console.Write("|\n");
            }
            Console.WriteLine(separatorH);
        }
        public static float[,] CreateEmpty(int rows, int columns)
        {
            float[,] A = new float[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    A[i, j] = 0.0f;
                }
            }
            return A;
        }
        public static float[,] CreateIdentity(int size)
        {
            float[,] A = new float[size, size];
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
        public static float[,] Add(float[,] A, float[,] B)
        {
            if ((A.GetLength(0) == B.GetLength(0)) & (A.GetLength(1) == B.GetLength(1)))
            {
                int rows = A.GetLength(0);
                int columns = A.GetLength(1);
                float[,] C = new float[rows, columns];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        C[i, j] = A[i, j] + B[i, j];
                    }
                }
                return C;
            }
            else
            {
                throw new Exception("Can't add matrices: size does not match");
            }
        }
        public static float[,] Multiply(float[,] A, float[,] B)
        {
            if (A.GetLength(1) == B.GetLength(0))
            {
                int rows = A.GetLength(0);
                int columns = B.GetLength(1);
                int n = A.GetLength(1);
                float[,] C = new float[rows, columns];
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
        public static float[,] Multiply(float c, float[,] A)
        {
            int rows = A.GetLength(0);
            int columns = A.GetLength(1);
            float[,] B = new float[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    B[i, j] = A[i, j] * c;
                }
            }
            return B;
        }
        public static float[,] Transpose(float[,] A)
        {
            int rows = A.GetLength(0);
            int columns = A.GetLength(1);
            float[,] A_T = new float[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    A_T[i, j] = A[j, i];
                }
            }
            return A_T;
        }
        private static float[,] CreateMinorMatrix(float[,] A, int row, int column)
        {
            int size = A.GetLength(0);
            float[,] M = CreateEmpty(size - 1, size - 1);
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
        public static float Minor(float[,] A, int row, int column)
        {
            return Determinant(CreateMinorMatrix(A, row, column));
        }
        public static float Determinant(float[,] A)
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
                    float det = 0;
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
        public static float[,] Invert(float[,] A)
        {
            int rows = A.GetLength(0);
            int columns = A.GetLength(1);
            if (rows == columns)
            {
                float detA = Determinant(A);
                if (detA != 0)
                {
                    float[,] A_D = new float[rows, columns];
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            if ((i + j) % 2 == 0)
                            {
                                A_D[i, j] = Minor(A, i, j);
                            }
                            else
                            {
                                A_D[i, j] = -Minor(A, i, j);
                            }
                        }
                    }
                    return Multiply(1 / detA, Transpose(A_D));
                }
                else
                {
                    throw new Exception("Determinant equals zero");
                }
            }
            else
            {
                throw new Exception("Matrix isn't square");
            }
        }
        public static void SwichString(MatExt input, int st1, int st2)
        {
            int A_columns = input.A.GetLength(1);
            int B_columns = input.B.GetLength(1);
            float temp;
            for (int i = 0; i < A_columns; i++)
            {
                temp = input.A[st1, i];
                input.A[st1, i] = input.A[st2, i];
                input.A[st2, i] = temp;
            }
            for (int i = 0; i < B_columns; i++)
            {
                temp = input.B[st1, i];
                input.B[st1, i] = input.B[st2, i];
                input.B[st2, i] = temp;
            }
        }
    }
}