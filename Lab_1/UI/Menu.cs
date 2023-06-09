﻿using Lab_1.SubtaskSolvers;

namespace Lab_1.UI
{
    public class Menu
    {
        public Menu ()
        {
            Options.Add(new MenuOption(new LUDecomposition(), "1 LU Decomposition"));
            Options.Add(new MenuOption(new Gaussian(), "2 Gaussian method"));
            Options.Add(new MenuOption(new Rundown(), "3 Rundown method"));
            Options.Add(new MenuOption(new Iterative(), "4 Iterative method"));
            Options.Add(new MenuOption(new Zeidel(), "5 Zeidel method"));
            Options.Add(new MenuOption(new Jakobi(), "6 Jakobi method"));
            Options.Add(new MenuOption(new QRAlgorithm(), "7 QR algorithm"));
        }
        public void Run ()
        {
            while (true)
            {
                DisplayOptions();
                if (!RunSelector())
                {
                    break;
                }
            }
        }
        private void DisplayOptions ()
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("\t0 Exit");
            foreach (var option in Options)
                option.DisplayOption();
        }
        private bool RunSelector ()
        {
            Console.Write("Input: ");
            string selectedOption = Console.ReadLine();
            Console.Write("\n");
            try
            {
                int SelectedOption = int.Parse(selectedOption);
                if (SelectedOption == 0)
                {
                    Console.WriteLine("Exiting");
                    return false;
                }
                else
                {
                    Console.Write(Devider);
                    Options[SelectedOption - 1].DisplayOption();
                    Console.Write("\n");
                    try
                    {
                        OnSelection(SelectedOption - 1);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Encountered error: {ex.Message}");
                        //Console.WriteLine(ex.ToString());
                    }
                    Console.WriteLine(Devider);
                    return true;
                }
            }
            catch
            {
                Console.WriteLine("Please input a valid option");
                return true;
            }
        }
        private void OnSelection (int SelectedOption)
        {
            if (YesNoSelector("Would you like to use standard values?"))
            {
                Options[SelectedOption].SolveDefault();
            }
            else
            {
                if (YesNoSelector("Would you like to enable input assist?"))
                {
                    Options[SelectedOption].SolveCustom(true);
                }
                else
                {
                    Options[SelectedOption].SolveCustom(false);
                }
            }
        }
        private bool YesNoSelector (string Question)
        {
            Console.Write($"{Question} (Y/N) ");
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

        private readonly List<MenuOption> Options = new();
        private readonly string Devider = "-------------------------------------------------------------------\n";
    }
}
