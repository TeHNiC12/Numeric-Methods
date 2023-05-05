using Lab_2.Core;
using System.Windows;

namespace Lab_2.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public RelayCommand SingleIterativeCommand { get; set; }
        public RelayCommand SingleGaussianCommand { get; set; }
        public RelayCommand MultyIterativeCommand { get; set; }
        public RelayCommand MultyGaussianCommand { get; set; }
        public RelayCommand CollectDataCommand { get; set; }
        public SingleIterativeViewModel SingleIterativeVM { get; set; }
        public SingleGaussianViewModel SingleGaussianVM { get; set; }
        public MultyIteratveViewModel MultyIteratveVM { get; set; }
        public MultyGaussianViewModel MultyGaussianVM { get; set; }

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

        private double _a;
        private double _b;
        private double _accuracy;

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel ()
        {
            StAB = string.Empty;
            StAccuracy = string.Empty;
            SingleIterativeVM = new SingleIterativeViewModel();
            SingleGaussianVM = new SingleGaussianViewModel();
            MultyIteratveVM = new MultyIteratveViewModel();
            MultyGaussianVM = new MultyGaussianViewModel();

            CurrentView = SingleIterativeVM;

            SingleIterativeCommand = new RelayCommand(o =>
            {
                CurrentView = SingleIterativeVM;
            });
            SingleGaussianCommand = new RelayCommand(o =>
            {
                CurrentView = SingleGaussianVM;
            });
            MultyIterativeCommand = new RelayCommand(o =>
            {
                CurrentView = MultyIteratveVM;
            });
            MultyGaussianCommand = new RelayCommand(o =>
            {
                CurrentView = MultyGaussianVM;
            });
            CollectDataCommand = new RelayCommand(o =>
            {
                if (CollectData())
                {
                    SingleIterativeVM.A = _a;
                    SingleIterativeVM.B = _b;
                    SingleIterativeVM.Accuracy = _accuracy;
                    SingleIterativeVM.Solve();
                }
            });
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
            return double.TryParse(_stAccuracy, out _accuracy);
        }
        public bool CollectAB ()
        {
            if (StAB.Contains(";"))
            {
                string[] AB = StAB.Split(';');
                return double.TryParse(AB[0], out _a) | double.TryParse(AB[1], out _b);
            }
            else
            {
                return false;
            }
        }
    }
}
