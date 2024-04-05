using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using IndoorPlaygroundSafetyCheck.Commands;
using IndoorPlaygroundSafetyCheck.Data;
using IndoorPlaygroundSafetyCheck.Models;
using Microsoft.EntityFrameworkCore;

namespace IndoorPlaygroundSafetyCheck.ViewModels
{
    public class ManagerStatisticsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<InspectionQuestionResult> _inspectionQuestionResults;
        private int? _selectedErrorType; // Changed to int? to handle error type as integer
        public int? SelectedErrorType // Changed this to an integer to match your ErrorType definition
        {
            get => _selectedErrorType;
            set
            {
                _selectedErrorType = value;
                OnPropertyChanged();
                LoadData(); // Reload data when selection changes
            }
        }

        public List<int> ErrorTypes { get; } = new List<int> { 0, 1, 2 }; // Assuming ErrorTypes are integers

        //
        public ObservableCollection<InspectionQuestionResult> InspectionQuestionResults
        {
            get => _inspectionQuestionResults;
            set
            {
                _inspectionQuestionResults = value;
                OnPropertyChanged();
            }
        }

        // Date range properties
        public DateTime StartDate { get; set; } = DateTime.Today.AddDays(-7); // Default to last 7 days
        public DateTime EndDate { get; set; } = DateTime.Today;

        public ICommand LoadDataCommand { get; }

        private readonly SafetyCheckContext _context;

        public ManagerStatisticsViewModel(SafetyCheckContext context)
        {
            _context = context;
            // Use a lambda expression to adapt the method group to Action<object>
            LoadDataCommand = new RelayCommand(obj => LoadData());
            LoadData(); // Initial load
        }



        private void LoadData()
        {
            var query = _context.InspectionQuestionResults
                .Include(iqr => iqr.InspectionIdentNavigation)
                .Include(iqr => iqr.StationQuestionIdentNavigation)
                .Where(iqr =>
                    iqr.InspectionIdentNavigation.CheckStart >= StartDate &&
                    iqr.InspectionIdentNavigation.CheckDone <= EndDate &&
                    (!SelectedErrorType.HasValue || iqr.ErrorType == SelectedErrorType)) // Filter by selected error type
                .ToList();

            InspectionQuestionResults = new ObservableCollection<InspectionQuestionResult>(query);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
