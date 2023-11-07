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
    public class SubTask4ViewModel : ObservableObject
    {
        private SubTask4Model _SubTask4M;

        private PlotModel plotModel;
        public PlotModel PlotModel
        {
            get { return plotModel; }
            set { plotModel = value; OnPropertyChanged(); }
        }

        private string output1;
        public string Output1
        {
            get { return output1; }
            set { output1 = value; OnPropertyChanged(); }
        }

        private string output2;
        public string Output2
        {
            get { return output2; }
            set { output2 = value; OnPropertyChanged(); }
        }

        private string output3;
        public string Output3
        {
            get { return output3; }
            set { output3 = value; OnPropertyChanged(); }
        }

        private string output4;
        public string Output4
        {
            get { return output4; }
            set { output4 = value; OnPropertyChanged(); }
        }

        private string output5;
        public string Output5
        {
            get { return output5; }
            set { output5 = value; OnPropertyChanged(); }
        }

        private string output6;
        public string Output6
        {
            get { return output6; }
            set { output6 = value; OnPropertyChanged(); }
        }



        public SubTask4ViewModel ()
        {
            _SubTask4M = new();
            plotModel = new();
            UpdatePlot();
            UpdateResults();
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

            var lineSeries = new LineSeries
            {
                Title = "Таблично заданная функция",
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.Black,
                MarkerFill = OxyColors.Blue,
            };

            for (int i = 0; i < SubTask4Values.Xi.Length; i++)
            {
                lineSeries.Points.Add(new DataPoint(SubTask4Values.Xi[i], SubTask4Values.Yi[i]));
            }

            plotModel.Series.Add(lineSeries);

            plotModel.InvalidatePlot(true);
        }

        private void UpdateResults ()
        {
            output1 = string.Format($"Первая производная в точке X = {Math.Round(SubTask4Values.XStar, 1)} с первым порядком точности на отрезке [{Math.Round(SubTask4Values.Xi[1], 1)}, {Math.Round(SubTask4Values.Xi[1 + 1], 1)}] равняется: {Math.Round(_SubTask4M.Derivative1FirstOrder(1, SubTask4Values.Xi, SubTask4Values.Yi), 4)}");
            output2 = string.Format($"Первая производная в точке X = {Math.Round(SubTask4Values.XStar, 1)} со вторым порядком точности на отрезке [{Math.Round(SubTask4Values.Xi[1], 1)}, {Math.Round(SubTask4Values.Xi[1 + 1], 1)}] равняется: {Math.Round(_SubTask4M.Derivative1SecondOrder(1, SubTask4Values.Xi, SubTask4Values.Yi, SubTask4Values.XStar), 4)}");
            output3 = string.Format($"Вторая производная в точке X = {Math.Round(SubTask4Values.XStar, 1)} на отрезке [{Math.Round(SubTask4Values.Xi[1], 1)}, {Math.Round(SubTask4Values.Xi[1 + 1], 1)}] равняется: {Math.Round(_SubTask4M.Derivative2(1, SubTask4Values.Xi, SubTask4Values.Yi), 4)}");

            output4 = string.Format($"Первая производная в точке X = {Math.Round(SubTask4Values.XStar, 1)} с первым порядком точности на отрезке [{Math.Round(SubTask4Values.Xi[2], 1)}, {Math.Round(SubTask4Values.Xi[2 + 1], 1)}] равняется: {Math.Round(_SubTask4M.Derivative1FirstOrder(2, SubTask4Values.Xi, SubTask4Values.Yi), 4)}");
            output5 = string.Format($"Первая производная в точке X = {Math.Round(SubTask4Values.XStar, 1)} со вторым порядком точности на отрезке [{Math.Round(SubTask4Values.Xi[2], 1)}, {Math.Round(SubTask4Values.Xi[2 + 1], 1)}] равняется: {Math.Round(_SubTask4M.Derivative1SecondOrder(2, SubTask4Values.Xi, SubTask4Values.Yi, SubTask4Values.XStar), 4)}");
            output6 = string.Format($"Вторая производная в точке X = {Math.Round(SubTask4Values.XStar, 1)} на отрезке [{Math.Round(SubTask4Values.Xi[2], 1)}, {Math.Round(SubTask4Values.Xi[2 + 1], 1)}] равняется: {Math.Round(_SubTask4M.Derivative2(2, SubTask4Values.Xi, SubTask4Values.Yi), 4)}");
        }
    }
}
