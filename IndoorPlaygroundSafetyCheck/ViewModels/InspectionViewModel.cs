using IndoorPlaygroundSafetyCheck.Data;
using IndoorPlaygroundSafetyCheck.Models;
using IndoorPlaygroundSafetyCheck.Enums;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;


namespace IndoorPlaygroundSafetyCheck.ViewModels
{
    public class InspectionViewModel : INotifyPropertyChanged
    {
        private Data.SafetyCheckContext _context = new Data.SafetyCheckContext(); // Initialize your database context here

        public ObservableCollection<Inspection> Inspections { get; set; } = new ObservableCollection<Inspection>();

        private InspectionType _selectedInspectionType = InspectionType.DailyInspection;
        public InspectionType SelectedInspectionType
        {
            get => _selectedInspectionType;
            set
            {
                if (_selectedInspectionType != value)
                {
                    _selectedInspectionType = value;
                    OnPropertyChanged();
                    FilterInspectionsByType();
                }
            }
        }

        public InspectionViewModel()
        {
            LoadInspections();
        }

        private void LoadInspections()
        {
            // This method should load inspections from the database.
            // For demonstration, let's pretend we're loading them directly.
            // In real application, replace the next line with actual data loading from _context.
            Inspections = new ObservableCollection<Inspection>(_context.Inspections.ToList());
        }

        private void FilterInspectionsByType()
        {
            // This method assumes you want to filter inspections based on the selected type.
            // Replace this with actual filtering logic using _context if fetching from database,
            // or filter the ObservableCollection if all data is already loaded.
            var filteredInspections = _context.Inspections
                .Where(i => i.InspectionType == _selectedInspectionType)
                .ToList();

            Inspections.Clear();
            foreach (var inspection in filteredInspections)
            {
                Inspections.Add(inspection);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Methods for AddInspection, UpdateInspection, and DeleteInspection remain unchanged.
        // Ensure to implement them according to your actual data access strategy.
    }
}
