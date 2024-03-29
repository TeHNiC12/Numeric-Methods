﻿using Lab_3.Core;

namespace Lab_3.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public RelayCommand SubTask1Command { get; set; }
        public RelayCommand SubTask2Command { get; set; }
        public RelayCommand SubTask3Command { get; set; }
        public RelayCommand SubTask4Command { get; set; }
        public RelayCommand SubTask5Command { get; set; }

        public SubTask1ViewModel SubTask1VM { get; set; }
        public SubTask2ViewModel SubTask2VM { get; set; }
        public SubTask3ViewModel SubTask3VM { get; set; }
        public SubTask4ViewModel SubTask4VM { get; set; }
        public SubTask5ViewModel SubTask5VM { get; set; }

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
            SubTask1VM = new();
            SubTask2VM = new();
            SubTask3VM = new();
            SubTask4VM = new();
            SubTask5VM = new();

            CurrentView = SubTask1VM;

            SubTask1Command = new RelayCommand(o =>
            {
                CurrentView = SubTask1VM;
            });
            SubTask2Command = new RelayCommand(o =>
            {
                CurrentView = SubTask2VM;
            });
            SubTask3Command = new RelayCommand(o =>
            {
                CurrentView = SubTask3VM;
            });
            SubTask4Command = new RelayCommand(o =>
            {
                CurrentView = SubTask4VM;
            });
            SubTask5Command = new RelayCommand(o =>
            {
                CurrentView = SubTask5VM;
            });
        }
    }
}
