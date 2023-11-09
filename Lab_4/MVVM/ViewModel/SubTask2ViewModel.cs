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
    public class SubTask2ViewModel : ObservableObject
    {
        private SubTask2Model _SubTask2M;
        private SubTask2Values val;

        private PlotModel plotModelS;
        public PlotModel PlotModelS
        {
            get { return plotModelS; }
            set { plotModelS = value; OnPropertyChanged(); }
        }

        private PlotModel plotModelFD;
        public PlotModel PlotModelFD
        {
            get { return plotModelFD; }
            set { plotModelFD = value; OnPropertyChanged(); }
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

        public SubTask2ViewModel ()
        {
            _SubTask2M = new();
            val = new();
            plotModelS = new();
            plotModelFD = new();
            CalculatePoints();
            UpdateErrors();
            UpdatePlotS();
            UpdatePlotFD();
        }

        private void CalculatePoints ()
        {
            ShootingRes = _SubTask2M.ShootingMethod(val.a, val.b, 0.05f, val.alpha, val.betta, val.y0, val.delta, val.gamma, val.y1, val.y0, val.epsilon, val.ddY);

            TrueYS = new double[ShootingRes.Item3.Length];
            for (int i = 0; i < ShootingRes.Item3.Length; i++)
            {
                TrueYS[i] = val.TrueF(ShootingRes.Item3[i]);
            }
        }

        private void UpdateErrors ()
        {
            error1 = string.Format($"Error Shooting method :  {Math.Round(_SubTask2M.Error(ShootingRes.Item2, TrueYS), 8)}");
            /*error2 = string.Format($"Error Runge-Knutta 4th order method :  {Math.Round(_SubTask1M.Error(RungeKnutta4OrderYk, TrueY), 8)}");*/

            rrr1 = string.Format($"Runge Romberg error Shooting method :  {Math.Round(_SubTask2M.Error(ShootingRes.Item2, _SubTask2M.RungeRombergRichardson(ShootingRes.Item2, _SubTask2M.ShootingMethod(val.a, val.b, 0.005f, val.alpha, val.betta, val.y0, val.delta, val.gamma, val.y1, val.y0, val.epsilon, val.ddY).Item2, 10, 4)), 9)}");
            /*rrr2 = string.Format($"Runge Romberg error Runge-Knutta 4th order method :  {Math.Round(_SubTask1M.Error(RungeKnutta4OrderYk, _SubTask1M.RungeRombergRichardson(RungeKnutta4OrderYk, _SubTask1M.RungeKnutta4Order(SubTask1Values.a, SubTask1Values.b, SubTask1Values.h / 10f, SubTask1Values.Y0, SubTask1Values.dY0, SubTask1Values.ddY).Item1, 10, 4)), 9)}");*/
        }

        private void UpdatePlotS ()
        {
            plotModelS.Annotations.Clear();
            plotModelS.Axes.Clear();
            plotModelS.Legends.Clear();
            plotModelS.Series.Clear();

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

            plotModelS.Legends.Add(new Legend
            {
                LegendPosition = LegendPosition.TopLeft,
                LegendPlacement = LegendPlacement.Inside
            });

            plotModelS.Annotations.Add(zeroLineX);
            plotModelS.Annotations.Add(zeroLineY);
            plotModelS.Axes.Add(xAxis);
            plotModelS.Axes.Add(yAxis);

            var lineSeries1 = new LineSeries { Title = "True function" };
            for (int i = 0; i < ShootingRes.Item3.Length; i++)
            {
                lineSeries1.Points.Add(new DataPoint(ShootingRes.Item3[i], TrueYS[i]));
            }

            var lineSeries2 = new LineSeries { Title = "Euler implicit" };
            for (int i = 0; i < ShootingRes.Item3.Length; i++)
            {
                lineSeries2.Points.Add(new DataPoint(ShootingRes.Item3[i], ShootingRes.Item2[i]));
            }


            plotModelS.Series.Add(lineSeries1);
            plotModelS.Series.Add(lineSeries2);

            plotModelS.InvalidatePlot(true);
        }

        private void UpdatePlotFD ()
        {
            plotModelFD.Annotations.Clear();
            plotModelFD.Axes.Clear();
            plotModelFD.Legends.Clear();
            plotModelFD.Series.Clear();

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

            plotModelS.Legends.Add(new Legend
            {
                LegendPosition = LegendPosition.TopLeft,
                LegendPlacement = LegendPlacement.Inside
            });

            plotModelFD.Annotations.Add(zeroLineX);
            plotModelFD.Annotations.Add(zeroLineY);
            plotModelFD.Axes.Add(xAxis);
            plotModelFD.Axes.Add(yAxis);

            /*var lineSeries1 = new LineSeries { Title = "Euler explicit" };
            for (int i = 0; i < Xk.Length; i++)
            {
                lineSeries1.Points.Add(new DataPoint(Xk[i], EulerExplicitYk[i]));
            }

            var lineSeries2 = new LineSeries { Title = "Euler implicit" };
            for (int i = 0; i < Xk.Length; i++)
            {
                lineSeries2.Points.Add(new DataPoint(Xk[i], EulerImplicitYk[i]));
            }


            plotModelS.Series.Add(lineSeries1);
            plotModelS.Series.Add(lineSeries2);*/

            plotModelFD.InvalidatePlot(true);
        }

        private Tuple<double[], double[], double[]> ShootingRes;
        private double[] TrueYS;
    }
}
