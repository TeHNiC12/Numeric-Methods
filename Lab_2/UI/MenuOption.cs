using Lab_2.SubtaskSolvers;

namespace Lab_2.UI
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
            /*if (Solver is "SolverClass")
            {
                TaskData = defaultValues."CorrespondingDefaultData";
            }*/
            Solve();
        }
        public void SolveCustom(bool InputAssist)
        {
            /*if (Solver is "SolverClass")
            {
                TaskData = InputHandlers."CorrespondingInputMethod";
            }*/
            Solve();
        }
        private void Solve()
        {
            /*if (Solver is "SolverClass")
            {
                (("SolverClass")Solver).Execute(("RightDataStruct")TaskData);
            }*/
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
