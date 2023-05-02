using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Lab_2.Core;

namespace Lab_2.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand SingleIterativeCommand { get; set; }
        public RelayCommand SingleGaussianCommand { get; set; }
        public SingleIterativeViewModel SingleIterativeVM { get; set; }
        public SingleGaussianViewModel SingleGaussianVM { get; set; }

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
            CurrentView = SingleIterativeVM;

            SingleIterativeCommand = new RelayCommand(o =>
            {
                CurrentView = SingleIterativeVM;
            });
            SingleGaussianCommand = new RelayCommand(o =>
            {
                CurrentView = SingleGaussianVM;
            });
        }
    }
}
