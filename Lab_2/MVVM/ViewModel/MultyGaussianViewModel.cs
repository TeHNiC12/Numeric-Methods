using Lab_2.Core;
using OxyPlot;
using OxyPlot.Series;
using System;

namespace Lab_2.MVVM.ViewModel
{
    public class MultyGaussianViewModel : ObservableObject
    {
        public MultyGaussianViewModel ()
        {
            this.MyModel = new PlotModel();
            this.MyModel.Series.Add(new FunctionSeries(Func1, -1, 0, 0.001, "F1"));
            this.MyModel.Series.Add(new FunctionSeries(Func2, -1, 0, 0.001, "F2"));
        }
        public PlotModel MyModel { get; private set; }

        private double Func1 (double d)
        {
            return Math.Exp(d);
        }
        private double Func2 (double d)
        {
            return -10 * d - 2;
        }
    }
}
