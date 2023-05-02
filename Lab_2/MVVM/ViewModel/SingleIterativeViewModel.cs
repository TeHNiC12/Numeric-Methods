using Lab_2.Core;
using Lab_2.MVVM.Model;
using OxyPlot;
using OxyPlot.Series;

namespace Lab_2.MVVM.ViewModel
{
    public class SingleIterativeViewModel : ObservableObject
    {
        private SingleIterativeModel _singleIterativeM;
        public SingleIterativeViewModel()
        {
            _singleIterativeM = new();
            this.MyModel = new();
            this.MyModel.Series.Add(new FunctionSeries(_singleIterativeM.Func1, -1, 2, 0.001, "F1"));
            this.MyModel.Series.Add(new FunctionSeries(_singleIterativeM.Func2, -1, 2, 0.001, "F2"));
        }
        public PlotModel MyModel { get; private set; }
    }
}
