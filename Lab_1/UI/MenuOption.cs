using Lab_1.SubtaskSolvers;

namespace Lab_1.UI
{
    public class MenuOption
    {
        public MenuOption (object solver, string optionText)
        {
            Solver = solver;
            OptionText = optionText;
        }
        public void SolveDefault ()
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
            else if (Solver is Zeidel)
            {
                TaskData = defaultValues.Cond_Iterative_Zeidel;
            }
            else if (Solver is Jakobi)
            {
                TaskData = defaultValues.Cond_Jakobi;
            }
            else if (Solver is QRAlgorithm)
            {
                TaskData = defaultValues.Cond_QR;
            }
            Solve();
        }
        public void SolveCustom (bool InputAssist)
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
            else if (Solver is Zeidel)
            {
                TaskData = InputHandlers.AB(InputAssist);
            }
            else if (Solver is Jakobi)
            {
                TaskData = InputHandlers.A(InputAssist);
            }
            else if (Solver is QRAlgorithm)
            {
                TaskData = InputHandlers.A(InputAssist);
            }
            Solve();
        }
        private void Solve ()
        {
            if (Solver is LUDecomposition)
            {
                ((LUDecomposition) Solver).Execute((MatExt) TaskData);
            }
            else if (Solver is Rundown)
            {
                ((Rundown) Solver).Execute((MatExt) TaskData);
            }
            else if (Solver is Gaussian)
            {
                ((Gaussian) Solver).Execute((MatExt) TaskData);
            }
            else if (Solver is Iterative)
            {
                ((Iterative) Solver).Execute((MatExt) TaskData);
            }
            else if (Solver is Zeidel)
            {
                ((Zeidel) Solver).Execute((MatExt) TaskData);
            }
            else if (Solver is Jakobi)
            {
                ((Jakobi) Solver).Execute((Mat) TaskData);
            }
            else if (Solver is QRAlgorithm)
            {
                ((QRAlgorithm) Solver).Execute((Mat) TaskData);
            }
        }
        public void DisplayOption ()
        {
            Console.WriteLine($"\t{OptionText}");
        }

        private readonly string OptionText;
        private object TaskData;
        private readonly object Solver;
    }
}
