﻿namespace Lab_1.SubtaskSolvers
{
    public class Iterative : SubTask<MatExt>
    {
        public override void Execute(MatExt input)
        {
            Console.WriteLine("Task Conditions:");
            Console.WriteLine("Matrix A:");
            Matrix.Print(input.A);
            Console.WriteLine("Matrix B:");
            Matrix.Print(input.B);
            MatExt AlphaBeta = Matrix.AlphaBetaTransform(input);
            Console.WriteLine("Matrix Alpha:");
            Matrix.Print(AlphaBeta.A);
            Console.WriteLine("Matrix Beta:");
            Matrix.Print(AlphaBeta.B);
            float norm = Matrix.NormAc(AlphaBeta.A);
            Console.WriteLine($"||Alpha||c = {norm}");
            float[,] X;
            if (norm < 1)
            {
                Console.WriteLine("Necessary condition met\n");
                X = Solve(AlphaBeta, true);
            }
            else
            {
                Console.WriteLine("Necessary condition isn't met");
                X = Solve(AlphaBeta, false);
            }
            for (int i = 0; i < X.GetLength(0); i++)
            {
                Console.WriteLine($"X{i + 1} = {X[i, 0]:0.0000}");
            }
        }
        private float FindErrorNormal(MatExt XCurXPrev, float[,] Alpha)
        {
            float Error = Matrix.NormAc(Alpha) * Matrix.NormAc(Matrix.Subtract(XCurXPrev.A, XCurXPrev.B)) / (1 - Matrix.NormAc(Alpha));
            return Error;
        }
        private float FindErrorCondNotMet(MatExt XCurXPrev)
        {
            float Error = Matrix.NormAc(Matrix.Subtract(XCurXPrev.A, XCurXPrev.B));
            return Error;
        }
        private float[,] Solve(MatExt AlphaBeta, bool ConditionMet)
        {
            float Accuracy = RequestAccuracy();
            bool PrintEach = PrintEachIterration();
            MatExt XCurXPrev = new();
            XCurXPrev.A = AlphaBeta.B;
            for (int k = 1; k > 0; k++)
            {
                XCurXPrev.B = XCurXPrev.A;
                XCurXPrev.A = Matrix.Add(AlphaBeta.B, Matrix.Multiply(AlphaBeta.A, XCurXPrev.B));
                float Error;
                if (ConditionMet)
                {
                    Error = FindErrorNormal(XCurXPrev, AlphaBeta.A);
                }
                else
                {
                    Error = FindErrorCondNotMet(XCurXPrev);
                }
                if (Error <= Accuracy)
                {
                    Console.WriteLine($"Solution found on step {k}");
                    break;
                }
                if (PrintEach)
                {
                    Console.WriteLine($"X({k})");
                    Matrix.Print(XCurXPrev.A);
                }
            }
            return XCurXPrev.A;
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