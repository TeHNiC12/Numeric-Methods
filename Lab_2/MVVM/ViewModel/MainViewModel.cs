using Lab_2.Core;

namespace Lab_2.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public RelayCommand SingleIterativeCommand { get; set; }
        public RelayCommand SingleGaussianCommand { get; set; }
        public RelayCommand MultyIterativeCommand { get; set; }
        public RelayCommand MultyGaussianCommand { get; set; }
        public SingleIterativeViewModel SingleIterativeVM { get; set; }
        public SingleGaussianViewModel SingleGaussianVM { get; set; }
        public MultyIteratveViewModel MultyIteratveVM { get; set; }
        public MultyGaussianViewModel MultyGaussianVM { get; set; }

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

        public MainViewModel()
        {
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
        }
    }
}
