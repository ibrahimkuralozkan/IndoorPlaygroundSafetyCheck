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
// Ensure System.Windows is added for MessageBox (used here for simplicity)

namespace IndoorPlaygroundSafetyCheck.ViewModels
{
    public class ManagerStatisticsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<InspectionQuestionResult> InspectionQuestionResults { get; set; }
        public List<int> ErrorTypes { get; } = new List<int> { 0, 1, 2 };

        private int? _selectedErrorType;
        private DateTime _startDate = DateTime.Today.AddDays(-7);
        private DateTime _endDate = DateTime.Today;
        private InspectionQuestionResult _selectedInspectionQuestionResult;
        private readonly SafetyCheckContext _context;

        public int? SelectedErrorType
        {
            get => _selectedErrorType;
            set
            {
                _selectedErrorType = value;
                OnPropertyChanged();
                LoadData();
            }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
                LoadData();
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged();
                LoadData();
            }
        }

        public InspectionQuestionResult SelectedInspectionQuestionResult
        {
            get => _selectedInspectionQuestionResult;
            set
            {
                _selectedInspectionQuestionResult = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; private set; }
        public ICommand UpdateRepairPlanCommand { get; private set; }

        public ManagerStatisticsViewModel(SafetyCheckContext context)
        {
            _context = context;
            SaveCommand = new RelayCommand(param => SaveRepairPlan());
            UpdateRepairPlanCommand = new RelayCommand(param => UpdateRepairPlan());
            LoadData();
        }

        private void SaveRepairPlan()
        {
            if (SelectedInspectionQuestionResult != null)
            {
                _context.Update(SelectedInspectionQuestionResult);
                _context.SaveChanges();
                // MessageBox.Show("Repairment Plan updated successfully.");
                // Consider using an event or a message service for MVVM compliance.
            }
        }

        private void UpdateRepairPlan()
        {
            // Save changes to the database context
            _context.SaveChanges();
        }

        public void LoadData()
        {
            var query = _context.InspectionQuestionResults
                .Include(iqr => iqr.InspectionIdentNavigation)
                .Include(iqr => iqr.StationQuestionIdentNavigation)
                .Where(iqr =>
                    iqr.InspectionIdentNavigation.CheckStart >= StartDate &&
                    iqr.InspectionIdentNavigation.CheckDone <= EndDate &&
                    (!SelectedErrorType.HasValue || iqr.ErrorType == SelectedErrorType))
                .ToList();

            InspectionQuestionResults = new ObservableCollection<InspectionQuestionResult>(query);
            OnPropertyChanged(nameof(InspectionQuestionResults));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
