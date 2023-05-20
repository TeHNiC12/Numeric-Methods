using Lab_3.Core;

namespace Lab_3.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public RelayCommand SubTask1LagrangeCommand { get; set; }

        public SubTask1LagrangeViewModel SubTask1LagrangeVM { get; set; }

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
            SubTask1LagrangeVM = new();

            CurrentView = SubTask1LagrangeVM;

            SubTask1LagrangeCommand = new RelayCommand(o =>
            {
                CurrentView = SubTask1LagrangeVM;
            });
        }
    }
}
