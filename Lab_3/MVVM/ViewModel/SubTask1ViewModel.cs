using Lab_3.BaseValues;
using Lab_3.Core;
using Lab_3.MVVM.Model;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using System;
using System.Linq;

namespace Lab_3.MVVM.ViewModel
{
    public class SubTask1ViewModel : ObservableObject
    {
        private SubTask1Model _subtask1M;
        public RelayCommand DrawGraphsCommand { get; set; }
        public RelayCommand SetLagrangeCommand { get; set; }
        public RelayCommand SetNewtonCommand { get; set; }
        public RelayCommand PointSetACommand { get; set; }
        public RelayCommand PointSetBCommand { get; set; }

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

        private string error1;

        public string Error1
        {
            get { return error1; }
            set
            {
                error1 = value;
                OnPropertyChanged();
            }
        }

        private string error2;

        public string Error2
        {
            get { return error2; }
            set
            {
                error2 = value;
                OnPropertyChanged();
            }
        }



        public bool Interpolator { get; set; }
        public double[] PointSet { get; set; }
        public double[] PointSetS { get; set; }

        public SubTask1ViewModel ()
        {
            _subtask1M = new();
            Interpolator = false;
            PointSet = SubTask1Values.xiA;
            PointSetS = SubTask1Values.xiAS;
            plotModel = new();
            DrawGraphsCommand = new RelayCommand(o =>
            {
                RedrawPlotModel();
            });
            SetLagrangeCommand = new RelayCommand(o =>
            {
                Interpolator = false;
            });
            SetNewtonCommand = new RelayCommand(o =>
            {
                Interpolator = true;
            });
            PointSetACommand = new RelayCommand(o =>
            {
                PointSet = SubTask1Values.xiA;
                PointSetS = SubTask1Values.xiAS;
            });
            PointSetBCommand = new RelayCommand(o =>
            {
                PointSet = SubTask1Values.xiB;
                PointSetS = SubTask1Values.xiBS;
            });
            RedrawPlotModel();
        }

        private void RedrawPlotModel ()
        {
            PlotModel.Series.Clear();
            PlotModel.Annotations.Clear();
            PlotModel.Axes.Clear();
            PlotModel.Legends.Clear();
            Error1 = "";
            Error2 = "";

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
            plotModel.Annotations.Add(zeroLineX);
            plotModel.Annotations.Add(zeroLineY);
            plotModel.Axes.Add(xAxis);
            plotModel.Axes.Add(yAxis);

            plotModel.Series.Add(new FunctionSeries(SubTask1Values.Func, PointSet.Min() - 0.5f, PointSet.Max() + 0.5f, 0.001, "F(x)"));

            var scatterSeries = new ScatterSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 6,
                MarkerStroke = OxyColors.Blue,
                Title = "Original points"
            };
            double[] Y = _subtask1M.CalculateY(SubTask1Values.Func, PointSet);
            for (int i = 0; i < PointSet.Count(); i++)
            {
                scatterSeries.Points.Add(new ScatterPoint(PointSet[i], Y[i]));
            }
            plotModel.Series.Add(scatterSeries);

            if (!Interpolator)
            {
                plotModel.Series.Add(new FunctionSeries(LagrangeFunc, PointSet.Min() - 0.5f, PointSet.Max() + 0.5f, 0.001, "Lagrange interpolation 3d order"));
                plotModel.Series.Add(new FunctionSeries(LagrangeFuncS, PointSet.Min() - 0.5f, PointSet.Max() + 0.5f, 0.001, "Lagrange interpolation 2d order"));

                Error1 = string.Format($"Погрешность в точке X = {SubTask1Values.xStar} c использованием интерполяционного многочлена Лагранжа 3-й степени составляет {Math.Round(Math.Abs(Math.Abs(SubTask1Values.Func(SubTask1Values.xStar) - Math.Abs(LagrangeFunc(SubTask1Values.xStar)))), 5)}");
                Error2 = string.Format($"Погрешность в точке X = {SubTask1Values.xStar} c использованием интерполяционного многочлена Лагранжа 2-й степени составляет {Math.Round(Math.Abs(Math.Abs(SubTask1Values.Func(SubTask1Values.xStar) - Math.Abs(LagrangeFuncS(SubTask1Values.xStar)))), 5)}");
            }
            else
            {
                _subtask1M.CalculateNewtonCoeffs(PointSet, _subtask1M.CalculateY(SubTask1Values.Func, PointSet));
                plotModel.Series.Add(new FunctionSeries(NewtonFunc, PointSet.Min() - 0.5f, PointSet.Max() + 0.5f, 0.001, "Newton interpolation 3d order"));
                Error1 = string.Format($"Погрешность в точке X = {SubTask1Values.xStar} c использованием интерполяционного многочлена Ньютона 3-й степени составляет {Math.Round(Math.Abs(Math.Abs(SubTask1Values.Func(SubTask1Values.xStar) - Math.Abs(NewtonFunc(SubTask1Values.xStar)))), 5)}");

                _subtask1M.CalculateNewtonCoeffs(PointSetS, _subtask1M.CalculateY(SubTask1Values.Func, PointSetS));
                plotModel.Series.Add(new FunctionSeries(NewtonFuncS, PointSet.Min() - 0.5f, PointSet.Max() + 0.5f, 0.001, "Newton interpolation 2d order"));
                Error2 = string.Format($"Погрешность в точке X = {SubTask1Values.xStar} c использованием интерполяционного многочлена Ньютона 2-й степени составляет {Math.Round(Math.Abs(Math.Abs(SubTask1Values.Func(SubTask1Values.xStar) - Math.Abs(NewtonFuncS(SubTask1Values.xStar)))), 5)}");
            }

            plotModel.Legends.Add(new Legend
            {
                LegendPosition = LegendPosition.TopLeft,
                LegendPlacement = LegendPlacement.Inside
            });

            PlotModel.InvalidatePlot(true);
        }

        private double LagrangeFunc (double x)
        {
            return _subtask1M.LagrangeInterpolate(PointSet, _subtask1M.CalculateY(SubTask1Values.Func, PointSet), x);
        }

        private double LagrangeFuncS (double x)
        {
            return _subtask1M.LagrangeInterpolate(PointSetS, _subtask1M.CalculateY(SubTask1Values.Func, PointSetS), x);
        }

        private double NewtonFunc (double x)
        {
            return _subtask1M.NewtonInterpolate(PointSet, _subtask1M.CalculateY(SubTask1Values.Func, PointSet), x);
        }

        private double NewtonFuncS (double x)
        {
            return _subtask1M.NewtonInterpolate(PointSetS, _subtask1M.CalculateY(SubTask1Values.Func, PointSetS), x);
        }
    }
}
