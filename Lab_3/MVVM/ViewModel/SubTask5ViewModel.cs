using Lab_3.BaseValues;
using Lab_3.Core;
using Lab_3.MVVM.Model;
using System;

namespace Lab_3.MVVM.ViewModel
{
    public class SubTask5ViewModel : ObservableObject
    {
        private SubTask5Model _SubTask5M;

        private string f1;
        public string F1
        {
            get { return f1; }
            set { f1 = value; OnPropertyChanged(); }
        }

        private string f2;
        public string F2
        {
            get { return f2; }
            set { f2 = value; OnPropertyChanged(); }
        }

        private string f3;
        public string F3
        {
            get { return f3; }
            set { f3 = value; OnPropertyChanged(); }
        }

        private string f4;
        public string F4
        {
            get { return f4; }
            set { f4 = value; OnPropertyChanged(); }
        }

        private string f5;
        public string F5
        {
            get { return f5; }
            set { f5 = value; OnPropertyChanged(); }
        }

        private string f6;
        public string F6
        {
            get { return f6; }
            set { f6 = value; OnPropertyChanged(); }
        }

        private string error1;
        public string Error1
        {
            get { return error1; }
            set { error1 = value; OnPropertyChanged(); }
        }

        private string error2;
        public string Error2
        {
            get { return error2; }
            set { error2 = value; OnPropertyChanged(); }
        }

        private string error3;
        public string Error3
        {
            get { return error3; }
            set { error3 = value; OnPropertyChanged(); }
        }

        private string error4;
        public string Error4
        {
            get { return error4; }
            set { error4 = value; OnPropertyChanged(); }
        }

        private string error5;
        public string Error5
        {
            get { return error5; }
            set { error5 = value; OnPropertyChanged(); }
        }

        private string error6;
        public string Error6
        {
            get { return error6; }
            set { error6 = value; OnPropertyChanged(); }
        }

        private string rect1;
        public string Rect1
        {
            get { return rect1; }
            set { rect1 = value; OnPropertyChanged(); }
        }

        private string rect2;
        public string Rect2
        {
            get { return rect2; }
            set { rect2 = value; OnPropertyChanged(); }
        }

        private string rect3;
        public string Rect3
        {
            get { return rect3; }
            set { rect3 = value; OnPropertyChanged(); }
        }




        public SubTask5ViewModel ()
        {
            _SubTask5M = new();
            _SubTask5M.RectangleIntegral(SubTask5Values.X0, SubTask5Values.Xk, SubTask5Values.h2, SubTask5Values.Y);
            UpdateTable();
        }

        private void UpdateTable ()
        {
            double f1Res = _SubTask5M.RectangleIntegral(SubTask5Values.X0, SubTask5Values.Xk, SubTask5Values.h1, SubTask5Values.Y);
            double f2Res = _SubTask5M.RectangleIntegral(SubTask5Values.X0, SubTask5Values.Xk, SubTask5Values.h2, SubTask5Values.Y);
            double rect1Res = _SubTask5M.RungeRombergRichardson(f1Res, f2Res, SubTask5Values.h2 / SubTask5Values.h1, 2f);

            f1 = string.Format($"{Math.Round(f1Res, 8)}");
            f2 = string.Format($"{Math.Round(f2Res, 8)}");
            error1 = string.Format($"{Math.Round(Math.Abs(rect1Res - f1Res), 8)}");
            error2 = string.Format($"{Math.Round(Math.Abs(rect1Res - f2Res), 8)}");
            rect1 = string.Format($"{Math.Round(rect1Res, 8)}");

            double f3Res = _SubTask5M.TrapezoidIntegral(SubTask5Values.X0, SubTask5Values.Xk, SubTask5Values.h1, SubTask5Values.Y);
            double f4Res = _SubTask5M.TrapezoidIntegral(SubTask5Values.X0, SubTask5Values.Xk, SubTask5Values.h2, SubTask5Values.Y);
            double rect2Res = _SubTask5M.RungeRombergRichardson(f3Res, f4Res, SubTask5Values.h2 / SubTask5Values.h1, 2f);

            f3 = string.Format($"{Math.Round(f3Res, 8)}");
            f4 = string.Format($"{Math.Round(f4Res, 8)}");
            error3 = string.Format($"{Math.Round(Math.Abs(rect2Res - f3Res), 8)}");
            error4 = string.Format($"{Math.Round(Math.Abs(rect2Res - f4Res), 8)}");
            rect2 = string.Format($"{Math.Round(rect2Res, 8)}");

            double f5Res = _SubTask5M.SimpsonIntegral(SubTask5Values.X0, SubTask5Values.Xk, SubTask5Values.h1, SubTask5Values.Y);
            double f6Res = _SubTask5M.SimpsonIntegral(SubTask5Values.X0, SubTask5Values.Xk, SubTask5Values.h2, SubTask5Values.Y);
            double rect3Res = _SubTask5M.RungeRombergRichardson(f5Res, f6Res, SubTask5Values.h2 / SubTask5Values.h1, 4f);

            f5 = string.Format($"{Math.Round(f5Res, 8)}");
            f6 = string.Format($"{Math.Round(f6Res, 8)}");
            error5 = string.Format($"{Math.Round(Math.Abs(rect3Res - f5Res), 8)}");
            error6 = string.Format($"{Math.Round(Math.Abs(rect3Res - f6Res), 8)}");
            rect3 = string.Format($"{Math.Round(rect3Res, 8)}");
        }
    }
}
