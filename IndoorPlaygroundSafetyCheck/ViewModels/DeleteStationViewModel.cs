    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using IndoorPlaygroundSafetyCheck.Commands;
    using IndoorPlaygroundSafetyCheck.Data;
    using IndoorPlaygroundSafetyCheck.Models;
    using Microsoft.VisualBasic;
    using QRCoder;

    namespace IndoorPlaygroundSafetyCheck.ViewModels
    {
        public class DeleteStationViewModel : INotifyPropertyChanged
        {
            private readonly SafetyCheckContext _context = new SafetyCheckContext();
            private Station _selectedStation;

            public Station SelectedStation
            {
                get => _selectedStation;
                set
                {
                    _selectedStation = value;
                    OnPropertyChanged();
                  
                }
            }

            public ObservableCollection<Station> Stations { get; private set; }

            public ICommand DeleteStationCommand { get; private set; }

       

        public DeleteStationViewModel()
        {
            Stations = new ObservableCollection<Station>(_context.Stations.ToList());
            DeleteStationCommand = new IndoorPlaygroundSafetyCheck.Commands.RelayCommand(DeleteStationExecute, CanDeleteStationExecute);
        }

        private bool CanDeleteStationExecute(object parameter)
            {
                return SelectedStation != null;
            }

            private void DeleteStationExecute(object parameter)
            {
                string inputRfidUid = Interaction.InputBox("Bitte benutzen Sie die Admin-RFID-Karte", "Admin RFID Verification", "");
                if (!IsAdminRfidUid(inputRfidUid))
                {
                    MessageBox.Show("Invalid or unauthorized RFID UID.");
                    return;
                }

                _context.Stations.Remove(SelectedStation);
                
            _context.SaveChanges();

                Stations.Remove(SelectedStation);
            }

            private bool IsAdminRfidUid(string rfidUid)
            {
                return _context.Employees.Any(e => e.RfidUid == rfidUid && e.Position == 1); // Assume 1 is Admin position identifier
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
