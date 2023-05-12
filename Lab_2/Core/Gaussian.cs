namespace Lab_2.Core
{
    public static class Gaussian
    {
        public static double[] Execute (MatExt input)
        {
            GaussianTransform(input);
            return GaussianSolve(input);
        }
        private static void GaussianTransform (MatExt input)
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
                    double k = -input.A[j, i] / input.A[i, i];
                    input.A[j, i] = 0;
                    for (int z = i + 1; z < columns_A; z++)
                    {
                        input.A[j, z] = input.A[j, z] + k * input.A[i, z];
                    }
                    input.B[j, 0] = input.B[j, 0] + k * input.B[i, 0];
                }
            }
        }
        private static double[] GaussianSolve (MatExt input)
        {
            int size = input.A.GetLength(0);
            double[] X = new double[size];
            for (int i = size - 1; i >= 0; i--)
            {
                double sum = 0f;
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
