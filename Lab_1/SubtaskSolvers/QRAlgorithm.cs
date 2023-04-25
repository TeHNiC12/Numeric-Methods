using System.Drawing;

namespace Lab_1.SubtaskSolvers
{
    public class QRAlgorithm : SubTask<Mat>
    {
        public override void Execute(Mat input)
        {
            Console.WriteLine("Task Conditions:");
            Console.WriteLine("Matrix A:");
            Matrix.Print(input.A);
            (float[,] A, float error) res = Solve(input.A);
            Console.WriteLine($"Accuracy = {res.error}");
            Console.WriteLine($"A:");
            Matrix.Print(res.A);
            Console.WriteLine("Eigenvalues:");
            for (int i = 0; i < res.A.GetLength(0); i++)
            {
                Console.WriteLine($"Lambda{i + 1} = {res.A[i, i]:0.0000}");
            }
        }
        private (float[,] A, float error) Solve(float[,] A)
        {
            float Accuracy = RequestAccuracy();
            bool PrintEach = PrintEachIterration();
            float[,] ACurrent = A;
            MatExt QR;
            float Error = 0;
            for (int k = 1; k > 0; k++)
            {
                QR = QRDecomposition(ACurrent);
                ACurrent = Matrix.Multiply(QR.B, QR.A);
                Error = FindError(ACurrent);
                if (Error <= Accuracy)
                {
                    Console.WriteLine($"Solution found on step {k}\n");
                    break;
                }
                if (PrintEach)
                {
                    Console.WriteLine($"Iteration {k}");
                    Console.WriteLine($"A{k}");
                    Matrix.Print(ACurrent);
                }
            }
            return (ACurrent, Error);
        }
        private float FindError(float[,] A)
        {
            int size = A.GetLength(0);
            float Sum = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < i; j++)
                Sum += (float)Math.Pow(A[i, j], 2);
            }
            return (float)Math.Cbrt(Sum);
        }
        private MatExt QRDecomposition(float[,] A)
        {
            int size = A.GetLength(0);
            MatExt QR = new()
            {
                A = Matrix.CreateIdentity(size),
                B = A
            };
            for (int index = 0; index < size - 1; index++)
            {
                float[,] B = Matrix.CreateEmpty(size, 1);
                for (int i = index;  i < size; i++)
                {
                    B[i, 0] = QR.B[i, index];
                }
                float[,] HH = CreateHouseholder(B, index);
                QR.A = Matrix.Multiply(QR.A, HH);
                QR.B = Matrix.Multiply(HH, QR.B);
            }
            return QR;
        }
        private float[,] CreateHouseholder(float[,] B, int index)
        {
            int size = B.GetLength(0);
            float[,] V = CreateV(B, index);
            float[,] VVt = Matrix.Multiply(V, Matrix.Transpose(V));
            float coef = -2 / Matrix.Multiply(Matrix.Transpose(V), V)[0, 0];
            float[,] H = Matrix.Add(Matrix.CreateIdentity(size), Matrix.Multiply(coef, VVt));
            return H;
        }
        private float[,] CreateV(float[,] B, int index)
        {
            float[,] V = Matrix.Add(B, Matrix.Multiply(Math.Sign(B[index, 0]) * Matrix.NormA2(B), CreateE(B.GetLength(0), index)));
            return V;
        }
        private float[,] CreateE(int Size, int index)
        {
            float[,] E = Matrix.CreateEmpty(Size, 1);
            E[index, 0] = 1;
            return E;
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
