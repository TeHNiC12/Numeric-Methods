namespace Lab_1.UI
{
    public static class InputHandlers
    {
        public static MatExt AB(bool InputAssist)
        {
            Console.Write("Input amount of variables: ");
            string sizeInput = Console.ReadLine();
            int size;
            while (!int.TryParse(sizeInput, out size))
            {
                Console.Write("Please try again: ");
                sizeInput = Console.ReadLine();
            }
            MatExt tCond = new()
            {
                A = Matrix.CreateEmpty(size, size),
                B = Matrix.CreateEmpty(size, 1)
            };
            InputMatrix(tCond.A, "A", InputAssist);
            InputMatrix(tCond.B, "B", InputAssist);
            return tCond;
        }
        public static MatExt ABC_D(bool InputAssist)
        {
            Console.Write("Input amount of variables: ");
            string sizeInput = Console.ReadLine();
            int size;
            while (!int.TryParse(sizeInput, out size))
            {
                Console.Write("Please try again: ");
                sizeInput = Console.ReadLine();
            }
            MatExt tCond = new()
            {
                A = Matrix.CreateEmpty(size, 3),
                B = Matrix.CreateEmpty(size, 1)
            };
            Console.WriteLine($"\nFill non zero coefficients");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!((i == 0 & j == 0) | (i == size - 1 & j == 2)))
                    {
                        string name;
                        if (j == 0)
                        {
                            name = "a";
                        }
                        else if (j == 1)
                        {
                            name = "b";
                        }
                        else
                        {
                            name = "c";
                        }
                        if (InputAssist)
                        {
                            Matrix.Print(tCond.A);
                        }
                        Console.Write($"Please input coefficient {name}{i + 1}: ");
                        string coefInput = Console.ReadLine();
                        float coef;
                        while (!float.TryParse(coefInput, out coef))
                        {
                            Console.Write("Please try again: ");
                            coefInput = Console.ReadLine();
                        }
                        tCond.A[i, j] = coef;
                    }
                }
            }
            Console.WriteLine($"\nMatrix of coefficients:");
            Matrix.Print(tCond.A);
            InputMatrix(tCond.B, "D", InputAssist);
            return tCond;
        }
        private static void InputMatrix(float[,] matrix, string name, bool inputAssist)
        {
            Console.WriteLine($"\nFill matrix {name} values");
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (inputAssist)
                    {
                        Matrix.Print(matrix);
                    }
                    Console.Write($"Please input {name}[{i + 1},{j + 1}]: ");
                    string coefInput = Console.ReadLine();
                    float coef;
                    while (!float.TryParse(coefInput, out coef))
                    {
                        Console.Write("Please try again: ");
                        coefInput = Console.ReadLine();
                    }
                    matrix[i, j] = coef;
                }
            }
            Console.WriteLine($"\nMatrix {name}:");
            Matrix.Print(matrix);
        }
    }
}
