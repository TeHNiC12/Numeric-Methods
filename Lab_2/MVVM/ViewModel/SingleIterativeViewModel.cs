using Lab_2.Core;
using Lab_2.MVVM.Model;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Windows;

namespace Lab_2.MVVM.ViewModel
{
    public class SingleIterativeViewModel : ObservableObject
    {
        private SingleIterativeModel _singleIterativeM;

        private string _result;
        public string Result
        {
            get { return _result; }
            set 
            { 
                _result = value;
                OnPropertyChanged();
            }
        }

        public double Accuracy { get; set; }
        public double A { get; set; }
        public double B { get; set; }
        public PlotModel? PlotModel { get; private set; }

        public SingleIterativeViewModel()
        {
            _singleIterativeM = new();
            InitializePlotModel();
        }

        public void Solve()
        {
            try
            {
                _singleIterativeM.A = A;
                _singleIterativeM.B = B;
                (double X, int step) res = _singleIterativeM.Solve(Accuracy);
                SetResult(res.X, res.step);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SetResult(double X, int step)
        {
            Result = String.Format("Корень X = {0} был найден на {1} итерации", Math.Round(X, 4), step);
        }
        private void InitializePlotModel()
        {
            PlotModel = new();
            PlotModel.Series.Add(new FunctionSeries(_singleIterativeM.Func1, -1, 2, 0.001, "F1"));
            PlotModel.Series.Add(new FunctionSeries(_singleIterativeM.Func2, -1, 2, 0.001, "F2"));
        }
    }
}
