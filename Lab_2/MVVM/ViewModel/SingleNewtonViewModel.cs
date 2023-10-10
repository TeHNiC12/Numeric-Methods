using Lab_2.Core;
using Lab_2.MVVM.Model;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Windows;

namespace Lab_2.MVVM.ViewModel
{
    public class SingleNewtonViewModel : ObservableObject
    {
        private SingleNewtonModel _singleNewtonM;
        public RelayCommand CollectDataCommand { get; set; }

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
        private double A;
        private double B;
        public PlotModel? PlotModel { get; private set; }
        private string _stAB;
        public string StAB
        {
            get { return _stAB; }
            set { _stAB = value; }
        }

        private string _stAccuracy;
        public string StAccuracy
        {
            get { return _stAccuracy; }
            set { _stAccuracy = value; }
        }

        public SingleNewtonViewModel ()
        {
            StAB = string.Empty;
            StAccuracy = string.Empty;
            _singleNewtonM = new();
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
                _singleNewtonM.A = A;
                _singleNewtonM.B = B;
                (double X, int step) res = _singleNewtonM.Solve(Accuracy);
                SetResult(res.X, res.step);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SetResult (double X, int step)
        {
            Result = String.Format("Корень X = {0} был найден на {1} итерации", Math.Round(X, 4), step);
        }
        private void InitializePlotModel ()
        {
            PlotModel = new();
            var zeroLineY = new LineAnnotation
            {
                Type = LineAnnotationType.Horizontal,
                Y = 0,
                Color = OxyColors.Black,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 1
            };
            var zeroLineX = new LineAnnotation
            {
                Type = LineAnnotationType.Vertical,
                X = 0,
                Color = OxyColors.Black,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 1
            };
            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                TitleFontSize = 18,
                Title = "x"
            };
            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                TitleFontSize = 18,
                Title = "f(x)"
            };
            PlotModel.Annotations.Add(zeroLineX);
            PlotModel.Annotations.Add(zeroLineY);
            PlotModel.Axes.Add(xAxis);
            PlotModel.Axes.Add(yAxis);
            PlotModel.Series.Add(new FunctionSeries(_singleNewtonM.Func1, -1, 2, 0.001, "F1"));
            PlotModel.Series.Add(new FunctionSeries(_singleNewtonM.Func2, -1, 2, 0.001, "F2"));
        }
        public bool CollectData ()
        {
            if (!(CollectAccuracy() | CollectAB()))
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
        public bool CollectAB ()
        {
            if (StAB.Contains(";"))
            {
                string[] AB = StAB.Split(';');
                return double.TryParse(AB[0], out A) | double.TryParse(AB[1], out B);
            }
            else
            {
                return false;
            }
        }
    }
}
