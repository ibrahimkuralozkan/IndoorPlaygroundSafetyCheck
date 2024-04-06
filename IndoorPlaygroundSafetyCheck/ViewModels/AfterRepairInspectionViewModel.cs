using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using IndoorPlaygroundSafetyCheck.Commands;
using IndoorPlaygroundSafetyCheck.Models;
using IndoorPlaygroundSafetyCheck.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows.Ink;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace IndoorPlaygroundSafetyCheck.ViewModels
{
    public class AfterRepairInspectionViewModel : INotifyPropertyChanged
    {
        // Example property structure
        private ObservableCollection<StationWithIssues> _stationsWithIssues;
        public ObservableCollection<StationWithIssues> StationsWithIssues
        {
            get => _stationsWithIssues;
            set
            {
                _stationsWithIssues = value;
                OnPropertyChanged();
            }
        }

        private StationWithIssues _selectedStationWithIssues;
        public StationWithIssues SelectedStationWithIssues
        {
            get => _selectedStationWithIssues;
            set
            {
                _selectedStationWithIssues = value;
                OnPropertyChanged();
                // Any additional logic when a station is selected
            }
        }

        private StrokeCollection _signatureStrokes;
        public StrokeCollection SignatureStrokes
        {
            get => _signatureStrokes;
            set
            {
                _signatureStrokes = value;
                OnPropertyChanged();
            }
        }

        public ICommand SendAfterRepairInspectionCommand { get; private set; }

        private readonly SafetyCheckContext _context = new SafetyCheckContext();

        public AfterRepairInspectionViewModel()
        {
            SendAfterRepairInspectionCommand = new RelayCommand(SendAfterRepairInspection, CanSendAfterRepairInspection);
            LoadStationsWithIssues();
        }

        private void LoadStationsWithIssues()
        {
            // Implementation to load stations with previous errors
        }

        private bool CanSendAfterRepairInspection(object parameter)
        {
            // Implementation to determine if the after repair inspection can be sent
            // For example: Check if a station is selected and a signature is provided
            return SelectedStationWithIssues != null && SignatureStrokes != null && SignatureStrokes.Count > 0;
        }

        private void SendAfterRepairInspection(object parameter)
        {
            // Implementation to send the after repair inspection results
            // Similar to SendInspection method in DailyInspectionViewModel
        }

        // Implement the INotifyPropertyChanged event handler
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
