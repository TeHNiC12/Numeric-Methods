using Lab_2.Core;
using Lab_2.MVVM.Model;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Windows;

namespace Lab_2.MVVM.ViewModel
{
    public class MultyNewtonViewModel : ObservableObject
    {
        private MultyNewtonModel _multyNewtonM;
        public RelayCommand CollectDataCommand { get; set; }
        private PlotModel plotModel;
        public PlotModel PlotModel
        {
            get { return plotModel; }
            set
            {
                plotModel = value;
                OnPropertyChanged();
            }
        }

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

        private double Accuracy;
        private double X1L;
        private double X1R;
        private double X2L;
        private double X2R;


        private string stBounds;
        public string StBounds
        {
            get { return stBounds; }
            set { stBounds = value; }
        }
        private string _stAccuracy;
        public string StAccuracy
        {
            get { return _stAccuracy; }
            set { _stAccuracy = value; }
        }

        public MultyNewtonViewModel ()
        {
            StBounds = string.Empty;
            StAccuracy = string.Empty;
            _multyNewtonM = new();
            InitializePlotModel();
            CollectDataCommand = new RelayCommand(o =>
            {
                if (CollectData())
                {
                    Solve();
                }
            });
        }

        public void Solve ()
        {
            try
            {
                _multyNewtonM.X1L = X1L;
                _multyNewtonM.X1R = X1R;
                _multyNewtonM.X2L = X2L;
                _multyNewtonM.X2R = X2R;

                (double X1, double X2, int step) res = _multyNewtonM.Solve(Accuracy);
                SetResult(res.X1, res.X2, res.step);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SetResult (double X1, double X2, int step)
        {
            Result = String.Format("Корни X1 = {0}  X2 = {1} был найден на {2} итерации", Math.Round(X1, 4), Math.Round(X2, 4), step);
        }
        private void InitializePlotModel ()
        {
            PlotModel PM = new();
            /*ScatterSeries series1 = GetPoints(_multyNewtonM.f1X1X2, -0.4, 0.4, -2, 2, 0.001);
            PM.Series.Add(series1);
            ScatterSeries series2 = GetPoints(_multyNewtonM.f2X1X2, -2, 2, -2, 2, 0.001);
            PM.Series.Add(series2);*/
            PM.Axes.Add(new OxyPlot.Axes.LinearAxis { Position = OxyPlot.Axes.AxisPosition.Bottom, ExtraGridlines = new double[] { 0 }, ExtraGridlineThickness = 1, ExtraGridlineColor = OxyColors.Black, });
            PM.Axes.Add(new OxyPlot.Axes.LinearAxis { Position = OxyPlot.Axes.AxisPosition.Left, ExtraGridlines = new double[] { 0 }, ExtraGridlineThickness = 1, ExtraGridlineColor = OxyColors.Black, });
            PM.Series.Add(new FunctionSeries(_multyNewtonM.Func1, -0.4, 0.4, 0.001, "f1(x1, x2)"));
            PM.Series.Add(new FunctionSeries(_multyNewtonM.Func2, -1, 1, 0.001, "f2(x1, x2)"));
            PlotModel = PM;
        }
        public bool CollectData ()
        {
            if (!(CollectAccuracy() | CollectBounds()))
            {
                MessageBox.Show("Пожалуйста проверьте введенные данные");
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool CollectAccuracy ()
        {
            return double.TryParse(_stAccuracy, out Accuracy);
        }
        public bool CollectBounds ()
        {
            if (StBounds.Contains(";"))
            {
                string[] bounds = StBounds.Split(';');
                return double.TryParse(bounds[0], out X1L) | double.TryParse(bounds[1], out X1R) | double.TryParse(bounds[1], out X2L) | double.TryParse(bounds[1], out X2R);
            }
            else
            {
                return false;
            }
        }

        private ScatterSeries GetPoints (Func<double, double, double> func, double rangeX1L, double rangeX1R, double rangeX2L, double rangeX2R, double step)
        {
            var scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle };
            for (double x1 = rangeX1L; x1 < rangeX1R; x1 += step)
            {
                for (double x2 = rangeX2L; x2 < rangeX2R; x2 += step)
                {
                    double res = Math.Round(func(x1, x2), 3);
                    if (res == 0)
                    {
                        scatterSeries.Points.Add(new ScatterPoint(x1, x2, 1.3));
                    }
                }
            }
            return scatterSeries;
        }
    }
}
