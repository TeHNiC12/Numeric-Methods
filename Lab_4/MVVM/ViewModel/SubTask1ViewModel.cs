using Lab_4.BaseValues;
using Lab_4.Core;
using Lab_4.MVVM.Model;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using System;

namespace Lab_4.MVVM.ViewModel
{
    public class SubTask1ViewModel : ObservableObject
    {
        private SubTask1Model _SubTask1M;

        private PlotModel plotModel;
        public PlotModel PlotModel
        {
            get { return plotModel; }
            set { plotModel = value; OnPropertyChanged(); }
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

        private string rrr1;
        public string RRR1
        {
            get { return rrr1; }
            set { rrr1 = value; OnPropertyChanged(); }
        }

        private string rrr2;
        public string RRR2
        {
            get { return rrr2; }
            set { rrr2 = value; OnPropertyChanged(); }
        }

        private string rrr3;
        public string RRR3
        {
            get { return rrr3; }
            set { rrr3 = value; OnPropertyChanged(); }
        }

        private string rrr4;
        public string RRR4
        {
            get { return rrr4; }
            set { rrr4 = value; OnPropertyChanged(); }
        }

        private string rrr5;
        public string RRR5
        {
            get { return rrr5; }
            set { rrr5 = value; OnPropertyChanged(); }
        }

        private string rrr6;
        public string RRR6
        {
            get { return rrr6; }
            set { rrr6 = value; OnPropertyChanged(); }
        }


        public SubTask1ViewModel ()
        {
            _SubTask1M = new();
            plotModel = new();
            CalculatePoints();
            UpdateErrors();
            UpdatePlot();
        }

        private void CalculatePoints ()
        {
            Xk = _SubTask1M.Dscretize(SubTask1Values.a, SubTask1Values.b, SubTask1Values.h);
            TrueY = new double[Xk.Length];
            for (int i = 0; i < Xk.Length; i++)
            {
                TrueY[i] = SubTask1Values.TrueF(Xk[i]);
            }

            EulerExplicitYk = _SubTask1M.EulerExplicit(SubTask1Values.a, SubTask1Values.b, SubTask1Values.h, SubTask1Values.Y0, SubTask1Values.dY0, SubTask1Values.ddY);
            EulerImplicitYk = _SubTask1M.EulerImplicit(SubTask1Values.a, SubTask1Values.b, SubTask1Values.h, SubTask1Values.Y0, SubTask1Values.dY0, SubTask1Values.ddY);
            RungeKnutta4OrderYk = _SubTask1M.RungeKnutta4Order(SubTask1Values.a, SubTask1Values.b, SubTask1Values.h, SubTask1Values.Y0, SubTask1Values.dY0, SubTask1Values.ddY).Item1;
            RungeKnutta2OrderYk = _SubTask1M.RungeKnutta2Order(SubTask1Values.a, SubTask1Values.b, SubTask1Values.h, SubTask1Values.Y0, SubTask1Values.dY0, SubTask1Values.ddY);
            Adams4OrderYk = _SubTask1M.Adams4Order(SubTask1Values.a, SubTask1Values.b, SubTask1Values.h, SubTask1Values.Y0, SubTask1Values.dY0, SubTask1Values.ddY);
            Adams2OrderYk = _SubTask1M.Adams2Order(SubTask1Values.a, SubTask1Values.b, SubTask1Values.h, SubTask1Values.Y0, SubTask1Values.dY0, SubTask1Values.ddY);
        }

        private void UpdateErrors ()
        {
            error1 = string.Format($"Error explicit Euler method :  {Math.Round(_SubTask1M.Error(EulerExplicitYk, TrueY), 8)}");
            error6 = string.Format($"Error implicit Euler method :  {Math.Round(_SubTask1M.Error(EulerImplicitYk, TrueY), 8)}");
            error2 = string.Format($"Error Runge-Knutta 4th order method :  {Math.Round(_SubTask1M.Error(RungeKnutta4OrderYk, TrueY), 8)}");
            error3 = string.Format($"Error Runge-Knutta 2nd order method :  {Math.Round(_SubTask1M.Error(RungeKnutta2OrderYk, TrueY), 8)}");
            error4 = string.Format($"Error Adams 4th order method :  {Math.Round(_SubTask1M.Error(Adams4OrderYk, TrueY), 8)}");
            error5 = string.Format($"Error Adams 2nd order method :  {Math.Round(_SubTask1M.Error(Adams2OrderYk, TrueY), 8)}");

            rrr1 = string.Format($"Runge Romberg error explicit Euler method :  {Math.Round(_SubTask1M.Error(EulerExplicitYk, _SubTask1M.RungeRombergRichardson(EulerExplicitYk, _SubTask1M.EulerExplicit(SubTask1Values.a, SubTask1Values.b, SubTask1Values.h / 10f, SubTask1Values.Y0, SubTask1Values.dY0, SubTask1Values.ddY), 10, 1)), 9)}");
            rrr6 = string.Format($"Runge Romberg error implicit Euler method :  {Math.Round(_SubTask1M.Error(EulerImplicitYk, _SubTask1M.RungeRombergRichardson(EulerImplicitYk, _SubTask1M.EulerImplicit(SubTask1Values.a, SubTask1Values.b, SubTask1Values.h / 10f, SubTask1Values.Y0, SubTask1Values.dY0, SubTask1Values.ddY), 10, 1)), 9)}");
            rrr2 = string.Format($"Runge Romberg error Runge-Knutta 4th order method :  {Math.Round(_SubTask1M.Error(RungeKnutta4OrderYk, _SubTask1M.RungeRombergRichardson(RungeKnutta4OrderYk, _SubTask1M.RungeKnutta4Order(SubTask1Values.a, SubTask1Values.b, SubTask1Values.h / 10f, SubTask1Values.Y0, SubTask1Values.dY0, SubTask1Values.ddY).Item1, 10, 4)), 9)}");
            rrr3 = string.Format($"Runge Romberg error Runge-Knutta 2nd order method :  {Math.Round(_SubTask1M.Error(RungeKnutta2OrderYk, _SubTask1M.RungeRombergRichardson(RungeKnutta2OrderYk, _SubTask1M.RungeKnutta2Order(SubTask1Values.a, SubTask1Values.b, SubTask1Values.h / 10f, SubTask1Values.Y0, SubTask1Values.dY0, SubTask1Values.ddY), 10, 2)), 9)}");
            rrr4 = string.Format($"Runge Romberg error Adams 4th order method :  {Math.Round(_SubTask1M.Error(Adams4OrderYk, _SubTask1M.RungeRombergRichardson(Adams4OrderYk, _SubTask1M.Adams4Order(SubTask1Values.a, SubTask1Values.b, SubTask1Values.h / 10f, SubTask1Values.Y0, SubTask1Values.dY0, SubTask1Values.ddY), 10, 4)), 9)}");
            rrr5 = string.Format($"Runge Romberg error Adams 2nd order method :  {Math.Round(_SubTask1M.Error(Adams2OrderYk, _SubTask1M.RungeRombergRichardson(Adams2OrderYk, _SubTask1M.Adams2Order(SubTask1Values.a, SubTask1Values.b, SubTask1Values.h / 10f, SubTask1Values.Y0, SubTask1Values.dY0, SubTask1Values.ddY), 10, 2)), 9)}");
        }

        private void UpdatePlot ()
        {
            plotModel.Annotations.Clear();
            plotModel.Axes.Clear();
            plotModel.Legends.Clear();
            plotModel.Series.Clear();

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
                Title = "X"
            };
            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                TitleFontSize = 18,
                Title = "Y"
            };

            plotModel.Legends.Add(new Legend
            {
                LegendPosition = LegendPosition.TopLeft,
                LegendPlacement = LegendPlacement.Inside
            });

            plotModel.Annotations.Add(zeroLineX);
            plotModel.Annotations.Add(zeroLineY);
            plotModel.Axes.Add(xAxis);
            plotModel.Axes.Add(yAxis);

            var lineSeries1 = new LineSeries { Title = "Euler explicit" };
            for (int i = 0; i < Xk.Length; i++)
            {
                lineSeries1.Points.Add(new DataPoint(Xk[i], EulerExplicitYk[i]));
            }

            var lineSeries6 = new LineSeries { Title = "Euler implicit" };
            for (int i = 0; i < Xk.Length; i++)
            {
                lineSeries6.Points.Add(new DataPoint(Xk[i], EulerImplicitYk[i]));
            }

            var lineSeries2 = new LineSeries { Title = "Runge-Knutta 4th order" };
            for (int i = 0; i < Xk.Length; i++)
            {
                lineSeries2.Points.Add(new DataPoint(Xk[i], RungeKnutta4OrderYk[i]));
            }

            var lineSeries3 = new LineSeries { Title = "Runge-Knutta 2nd order" };
            for (int i = 0; i < Xk.Length; i++)
            {
                lineSeries3.Points.Add(new DataPoint(Xk[i], RungeKnutta2OrderYk[i]));
            }

            var lineSeries4 = new LineSeries { Title = "Adams 4th order" };
            for (int i = 0; i < Xk.Length; i++)
            {
                lineSeries4.Points.Add(new DataPoint(Xk[i], Adams4OrderYk[i]));
            }

            var lineSeries5 = new LineSeries { Title = "Adams 2nd order" };
            for (int i = 0; i < Xk.Length; i++)
            {
                lineSeries5.Points.Add(new DataPoint(Xk[i], Adams2OrderYk[i]));
            }


            plotModel.Series.Add(lineSeries1);
            plotModel.Series.Add(lineSeries2);
            plotModel.Series.Add(lineSeries3);
            plotModel.Series.Add(lineSeries4);
            plotModel.Series.Add(lineSeries5);
            plotModel.Series.Add(lineSeries6);

            plotModel.Series.Add(new FunctionSeries(SubTask1Values.TrueF, SubTask1Values.a, SubTask1Values.b, 0.001, "True F(x)"));

            plotModel.InvalidatePlot(true);
        }

        private double[] Xk;
        private double[] TrueY;
        private double[] EulerExplicitYk;
        private double[] EulerImplicitYk;
        private double[] RungeKnutta4OrderYk;
        private double[] RungeKnutta2OrderYk;
        private double[] Adams4OrderYk;
        private double[] Adams2OrderYk;
    }
}
