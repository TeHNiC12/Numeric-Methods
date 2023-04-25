namespace Lab_1.SubtaskSolvers
{
    public class Jakobi : SubTask<Mat>
    {
        public override void Execute(Mat input)
        {
            Console.WriteLine("Task Conditions:");
            Console.WriteLine("Matrix A:");
            Matrix.Print(input.A);
            if (Matrix.CheckSymmetry(input.A))
            {
                (float[,] A, float[,] U) result = Solve(input.A);
                int size = result.A.GetLength(0);
                Console.WriteLine("Eigenvalues:");
                for (int i = 0; i < size; i++)
                {
                    Console.WriteLine($"Lambda{i + 1} = {result.A[i, i]:0.0000}");
                }
                for (int j = 0; j < size; j++)
                {
                    Console.WriteLine($"x{j + 1}");
                    float[,] X = Matrix.CreateEmpty(size, 1);
                    for (int i = 0; i < size; i++)
                    {
                        X[i, 0] = result.U[i, j];
                    }
                    Matrix.Print(X);
                }
            }
            else
            {
                throw new Exception("Matrix A is not symmetrical");
            }
        }
        private (float[,] A, float[,] U) Solve(float[,] A)
        {
            int size = A.GetLength(0);
            float[,] TotalU = Matrix.CreateIdentity(size);
            float Accuracy = RequestAccuracy();
            bool PrintEach = PrintEachIterration();
            MatExt ACurAPrev = new();
            ACurAPrev.A = A;
            for (int k = 1; k > 0; k++)
            {
                ACurAPrev.B = ACurAPrev.A;
                (int i, int j) coef = FindUpperTriangleMax(ACurAPrev.B);
                float Phi = CalculatePhi(ACurAPrev.B, coef.i, coef.j);
                float[,] U = GenerateU(size, coef.i, coef.j, Phi);
                TotalU = Matrix.Multiply(TotalU, U);
                ACurAPrev.A = Matrix.Multiply(Matrix.Multiply(Matrix.Transpose(U), ACurAPrev.B), U);
                float Error = FindError(ACurAPrev.A);
                if (Error <= Accuracy)
                {
                    Console.WriteLine($"Solution found on step {k}\n");
                    break;
                }
                if (PrintEach)
                {
                    Console.WriteLine($"U({k - 1})");
                    Matrix.Print(U);
                    Console.WriteLine($"A({k})");
                    Matrix.Print(ACurAPrev.A);
                }
            }
            return (ACurAPrev.A, TotalU);
        }
        private float FindError(float[,] A)
        {
            int size = A.GetLength(0);
            float Sum = 0;
            for (int i = 0; i < size - 1; i++)
            {
                for (int j = size - 1; j > i; j--)
                {
                    Sum += (float)Math.Pow(A[i, j], 2);
                }
            }
            return (float)Math.Cbrt(Sum);
        }
        private float[,] GenerateU(int size, int i, int j, float Phi)
        {
            float[,] U = Matrix.CreateIdentity(size);
            U[i, i] = (float)Math.Cos(Phi);
            U[i, j] = (float)-Math.Sin(Phi);
            U[j, i] = (float)Math.Sin(Phi);
            U[j, j] = (float)Math.Cos(Phi);
            return U;
        }
        private float CalculatePhi(float[,] A, int i, int j)
        {
            float Phi = 0;
            if (A[i, i] == A[j, j])
            {
                Phi = (float)(Math.PI / 4);
            }
            else
            {
                Phi = (float)(0.5 * Math.Atan(2 * A[i, j] / (A[i, i] - A[j, j])));
            }
            return Phi;
        }
        private (int i, int j) FindUpperTriangleMax(float[,] A)
        {
            int size = A.GetLength(0);
            float MaxValueAbs = -1;
            float MaxValue;
            int iMax = 0;
            int jMax = 0;
            for (int i = 0; i < size - 1; i++)
            {
                for (int j = size - 1; j > i; j--)
                {
                    float ValueAbs = Math.Abs(A[i, j]);
                    if (ValueAbs > MaxValueAbs)
                    {
                        MaxValueAbs = ValueAbs;
                        MaxValue = A[i, j];
                        iMax = i;
                        jMax = j;
                    }
                }
            }
            return (iMax, jMax);
        }
        private float RequestAccuracy()
        {
            Console.Write("Please input accuracy: ");
            string accuracy = Console.ReadLine();
            float Accuracy;
            while (!float.TryParse(accuracy, out Accuracy))
            {
                Console.Write("Please try again: ");
                accuracy = Console.ReadLine();
            }
            Console.Write("\n");
            return Accuracy;
        }
        private bool PrintEachIterration()
        {
            Console.Write("Show each iteration? (Y/N) ");
            string res = Console.ReadLine();
            while (true)
            {
                if (res == "Y")
                {
                    Console.WriteLine("");
                    return true;
                }
                else if (res == "N")
                {
                    Console.WriteLine("");
                    return false;
                }
                else
                {
                    Console.Write("Please try again: ");
                    res = Console.ReadLine();
                }
            }
        }
    }
}
