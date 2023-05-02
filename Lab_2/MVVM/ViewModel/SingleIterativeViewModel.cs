using Lab_2.Core;
using OxyPlot.Series;
using OxyPlot;
using System;

namespace Lab_2.MVVM.ViewModel
{
    class SingleIterativeViewModel : ObservableObject
    {
        public SingleIterativeViewModel() 
        {
            this.MyModel = new PlotModel();
            this.MyModel.Series.Add(new FunctionSeries(Func1, -1, 0, 0.001, "F1"));
            this.MyModel.Series.Add(new FunctionSeries(Func2, -1, 0, 0.001, "F2"));
        }
        public PlotModel MyModel { get; private set; }

        private double Func1(double d)
        {
            return Math.Exp(d);
        }
        private double Func2(double d)
        {
            return -5 * d - 2;
        }
    }
}
