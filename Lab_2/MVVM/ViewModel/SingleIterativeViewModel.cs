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
    public class SingleIterativeViewModel : ObservableObject
    {
        private SingleIterativeModel _singleIterativeM;
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

        public SingleIterativeViewModel ()
        {
            StAB = string.Empty;
            StAccuracy = string.Empty;
            _singleIterativeM = new();
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
            PlotModel.Series.Add(new FunctionSeries(_singleIterativeM.Func1, -1, 2, 0.001, "F1"));
            PlotModel.Series.Add(new FunctionSeries(_singleIterativeM.Func2, -1, 2, 0.001, "F2"));
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
