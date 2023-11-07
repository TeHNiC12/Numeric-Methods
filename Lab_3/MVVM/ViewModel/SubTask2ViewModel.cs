using Lab_3.BaseValues;
using Lab_3.Core;
using Lab_3.MVVM.Model;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using System;

namespace Lab_3.MVVM.ViewModel
{
    public class SubTask2ViewModel : ObservableObject
    {
        private SubTask2Model _subtask2M;

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

        private string error;
        public string Error
        {
            get { return error; }
            set
            {
                error = value;
                OnPropertyChanged();
            }
        }

        public SubTask2ViewModel ()
        {
            _subtask2M = new();
            PlotModel = new();
            UpdatePlot();
            Error = string.Format($"Значение сплайна в точке X = {SubTask2Values.XStar} :    {Math.Round(SplineInterpolate(SubTask2Values.XStar), 4)}");
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

            var scatterSeries = new ScatterSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 6,
                MarkerStroke = OxyColors.Blue,
                Title = "Interpolation points"
            };

            for (int i = 0; i < SubTask2Values.Xi.Length; i++)
            {
                scatterSeries.Points.Add(new ScatterPoint(SubTask2Values.Xi[i], SubTask2Values.Fi[i]));
            }

            plotModel.Series.Add(scatterSeries);

            _subtask2M.FindC(SubTask2Values.Xi, SubTask2Values.Fi);
            plotModel.Series.Add(new FunctionSeries(SplineInterpolate, SubTask2Values.Xi[0], SubTask2Values.Xi[4], 0.001, "3-d order spline"));

            plotModel.InvalidatePlot(true);
        }

        private double SplineInterpolate (double x)
        {
            return _subtask2M.SplineInterpolation(x, SubTask2Values.Xi, SubTask2Values.Fi);
        }
    }
}
