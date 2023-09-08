namespace Lab_1.SubtaskSolvers
{
    public class Rundown : SubTask<MatExt>
    {
        public override void Execute (MatExt input)
        {
            Console.WriteLine("Task Conditions:");
            Console.WriteLine("Coefficients a b c:");
            Matrix.Print(input.A);
            Console.WriteLine("Matrix D:");
            Matrix.Print(input.B);
            if (Check(input.A))
            {
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
        }
        private MatExt FindPQ (MatExt input)
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
        private float[] Solve (MatExt PQ)
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
        private bool Check (float[,] input)
        {
            int strict = 0;

            for (int i = 1; i < input.GetLength(0) - 2; i++)
            {
                if (input[i, 0] == 0)
                {
                    Console.WriteLine($"Coefficient a{i + 1} = 0, solution won't be accurate");
                    return false;
                }
                if (input[i, 0] == 0)
                {
                    Console.WriteLine($"Coefficient c{i + 1} = 0, solution won't be accurate");
                    return false;
                }
            }
            for (int i = 0; i < input.GetLength(1) - 1; i++)
            {
                float ac = Math.Abs(input[i, 0]) + Math.Abs(input[i, 2]);
                if (Math.Abs(input[i, 1]) > ac)
                {
                    strict++;
                }
                if (Math.Abs(input[i, 1]) < ac)
                {
                    Console.WriteLine($"|b{i + 1}| < |a{i + 1}| + |c{i + 1}|, solution won't be accurate");
                    return false;
                }
            }
            if (strict == 0)
            {
                Console.WriteLine("Inequasion |bi| < |ai| + |ci| isn't satisfied at least once, solution won't be accurate");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
