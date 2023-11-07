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
    public class SubTask3ViewModel : ObservableObject
    {
        private SubTask3Model _SubTask3M;

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

        private PlotModel plotModel;
        public PlotModel PlotModel
        {
            get { return plotModel; }
            set { plotModel = value; OnPropertyChanged(); }
        }

        public SubTask3ViewModel ()
        {
            _SubTask3M = new();
            plotModel = new();
            UpdatePlot();
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
                Title = "Узлы интерполяции"
            };

            for (int i = 0; i < SubTask3Values.Xi.Length; i++)
            {
                scatterSeries.Points.Add(new ScatterPoint(SubTask3Values.Xi[i], SubTask3Values.Yi[i]));
            }

            plotModel.Series.Add(scatterSeries);

            _SubTask3M.CalculateCoeffsA(SubTask3Values.Xi, SubTask3Values.Yi, 1);
            plotModel.Series.Add(new FunctionSeries(_SubTask3M.LeastSquaresAppriximation, SubTask3Values.Xi.Min(), SubTask3Values.Xi.Max(), 0.001, "1st order MNK"));
            error1 = string.Format($"Сумма квадратов ошибок для многочлена первой степени:  {Math.Round(_SubTask3M.CalculateError(SubTask3Values.Xi, SubTask3Values.Yi), 4)}");

            _SubTask3M.CalculateCoeffsA(SubTask3Values.Xi, SubTask3Values.Yi, 2);
            plotModel.Series.Add(new FunctionSeries(_SubTask3M.LeastSquaresAppriximation, SubTask3Values.Xi.Min(), SubTask3Values.Xi.Max(), 0.001, "1st order MNK"));
            error2 = string.Format($"Сумма квадратов ошибок для многочлена второй степени:  {Math.Round(_SubTask3M.CalculateError(SubTask3Values.Xi, SubTask3Values.Yi), 4)}");

            plotModel.InvalidatePlot(true);
        }
    }
}
