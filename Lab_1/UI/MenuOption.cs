﻿using Lab_1.SubtaskSolvers;

namespace Lab_1.UI
{
    public class MenuOption
    {
        public MenuOption(object solver, string optionText)
        {
            Solver = solver;
            OptionText = optionText;
        }
        public void SolveDefault()
        {
            DefaultValues defaultValues = new();
            if (Solver is LUDecomposition)
            {
                TaskData = defaultValues.Cond_LU_Gaussian;
            }
            else if (Solver is Rundown)
            {
                TaskData = defaultValues.Cond_Rundown;
            }
            else if (Solver is Gaussian)
            {
                TaskData = defaultValues.Cond_LU_Gaussian;
            }
            else if (Solver is Iterative)
            {
                TaskData = defaultValues.Cond_Iterative_Zeidel;
            }
            Solve();
        }
        public void SolveCustom(bool InputAssist)
        {
            if (Solver is LUDecomposition)
            {
                TaskData = InputHandlers.AB(InputAssist);
            }
            else if (Solver is Rundown)
            {
                TaskData = InputHandlers.ABC_D(InputAssist);
            }
            else if (Solver is Gaussian)
            {
                TaskData = InputHandlers.AB(InputAssist);
            }
            else if (Solver is Iterative)
            {
                TaskData = InputHandlers.AB(InputAssist);
            }
            Solve();
        }
        private void Solve()
        {
            if (Solver is LUDecomposition)
            {
                ((LUDecomposition)Solver).Execute((MatExt)TaskData);
            }
            else if (Solver is Rundown)
            {
                ((Rundown)Solver).Execute((MatExt)TaskData);
            }
            else if (Solver is Gaussian)
            {
                ((Gaussian)Solver).Execute((MatExt)TaskData);
            }
            else if (Solver is Iterative)
            {
                ((Iterative)Solver).Execute((MatExt)TaskData);
            }
        }
        public void DisplayOption()
        {
            Console.WriteLine($"\t{OptionText}");
        }

        private readonly string OptionText;
        private object TaskData;
        private readonly object Solver;
    }
}