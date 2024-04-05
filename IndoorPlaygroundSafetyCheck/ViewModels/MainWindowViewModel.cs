using IndoorPlaygroundSafetyCheck.Data;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using Microsoft.EntityFrameworkCore;

namespace IndoorPlaygroundSafetyCheck
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _currentDateTime;
        public string CurrentDateTime
        {
            get { return _currentDateTime; }
            set
            {
                _currentDateTime = value;
                OnPropertyChanged();
            }
        }
        public MainWindowViewModel()
        {
            // Set up a timer to update the time every second.
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) =>
            {
                // Using "F" format specifier for full date/time pattern
                CurrentDateTime = DateTime.Now.ToString("F"); // e.g., "Monday, March 09, 2024 4:05:07 PM"
            };
            timer.Start();
            LoadErrorTypeZeroCount();
            LoadErrorTypeOneCount();
            LoadErrorTypeTwoCount();
           
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private int _errorTypeZeroCount;
        public int ErrorTypeZeroCount
        {
            get => _errorTypeZeroCount;
            set
            {
                _errorTypeZeroCount = value;
                OnPropertyChanged();
            }
        }

        private int _errorTypeOneCount;
        public int ErrorTypeOneCount
        {
            get => _errorTypeOneCount;
            set
            {
                _errorTypeOneCount = value;
                OnPropertyChanged();
            }
        }

        private int _errorTypeTwoCount;
        public int ErrorTypeTwoCount
        {
            get => _errorTypeTwoCount;
            set
            {
                _errorTypeTwoCount = value;
                OnPropertyChanged();
            }
        }

        private async void LoadErrorTypeZeroCount()
        {
            // Assuming you have async method to fetch data. If not, adjust accordingly
            using (var context = new SafetyCheckContext())
            {
                ErrorTypeZeroCount = await context.InspectionQuestionResults
                                    .Where(iqr => iqr.ErrorType == 0 && iqr.InsertTimeStamp.Date == DateTime.Today)
                                    .CountAsync();
            }
        }
        private async void LoadErrorTypeOneCount()
        {
            // Assuming you have async method to fetch data. If not, adjust accordingly
            using (var context = new SafetyCheckContext())
            {
                ErrorTypeOneCount = await context.InspectionQuestionResults
                                    .Where(iqr => iqr.ErrorType == 1 && iqr.InsertTimeStamp.Date == DateTime.Today)
                                    .CountAsync();
            }
        }
        private async void LoadErrorTypeTwoCount()
        {
            // Assuming you have async method to fetch data. If not, adjust accordingly
            using (var context = new SafetyCheckContext())
            {
                ErrorTypeTwoCount = await context.InspectionQuestionResults
                                    .Where(iqr => iqr.ErrorType == 2 && iqr.InsertTimeStamp.Date == DateTime.Today)
                                    .CountAsync();
            }
        }
    }
}
