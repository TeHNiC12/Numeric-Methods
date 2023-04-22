namespace Lab_1.SubtaskSolvers
{
    public class Rundown : SubTask<MatExt>
    {
        public override void Execute(MatExt input)
        {
            Console.WriteLine("Task Conditions:");
            Console.WriteLine("Coefficients a b c:");
            Matrix.Print(input.A);
            Console.WriteLine("Matrix D:");
            Matrix.Print(input.B);
            Console.WriteLine("Rundown coefficients:");
            MatExt PQ = FindPQ(input);
            Console.WriteLine("Matrix P:");
            Matrix.Print(PQ.A);
            Console.WriteLine("Matrix Q:");
            Matrix.Print(PQ.B);
            Console.WriteLine("Result:");
            float[] result = Solve(PQ);
            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine($"X{i + 1} = {result[i]:f}");
            }
        }
        private MatExt FindPQ(MatExt input)
        {
            int size = input.A.GetLength(0);
            MatExt PQ = new()
            {
                A = Matrix.CreateEmpty(size, 1),
                B = Matrix.CreateEmpty(size, 1)
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
        private float[] Solve(MatExt PQ)
        {
            int size = PQ.A.GetLength(0);
            float[] X = new float[size];
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
    }
}
