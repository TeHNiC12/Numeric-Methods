namespace Lab_1.SubtaskSolvers
{
    public class Gaussian : SubTask<MatExt>
    {
        public override void Execute (MatExt input)
        {
            Console.WriteLine("Task Conditions:");
            Console.WriteLine("Matrix A:");
            Matrix.Print(input.A);
            Console.WriteLine("Matrix B:");
            Matrix.Print(input.B);
            float detA = Matrix.Determinant(input.A);
            if (detA == 0)
            {
                throw new Exception("Determinant = 0, impossible to solve");
            }
            else
            {
                Console.WriteLine("Gaussian transformation:");
                GaussianTransform(input);
                Console.WriteLine("Matrix A:");
                Matrix.Print(input.A);
                Console.WriteLine("Matrix B:");
                Matrix.Print(input.B);
                Console.WriteLine("Result:");
                float[] result = GaussianSolve(input);
                for (int i = 0; i < result.Length; i++)
                {
                    Console.WriteLine($"X{i + 1} = {result[i]:f}");
                }
            }
        }
        private void GaussianTransform (MatExt input)
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
                    input.A[j, i] = 0;
                    for (int z = i + 1; z < columns_A; z++)
                    {
                        input.A[j, z] = input.A[j, z] + k * input.A[i, z];
                    }
                    input.B[j, 0] = input.B[j, 0] + k * input.B[i, 0];
                }
            }
        }
        private float[] GaussianSolve (MatExt input)
        {
            int size = input.A.GetLength(0);
            float[] X = new float[size];
            for (int i = size - 1; i >= 0; i--)
            {
                float sum = 0f;
                for (int j = i + 1; j < size; j++)
                {
                    sum += input.A[i, j] * X[j];
                }
                X[i] = 1 / input.A[i, i] * (input.B[i, 0] - sum);
            }
            return X;
        }
    }
}
