//using System;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.IO;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Windows;
//using System.Windows.Input;
//using IndoorPlaygroundSafetyCheck.Commands;
//using IndoorPlaygroundSafetyCheck.Data;
//using IndoorPlaygroundSafetyCheck.Models;
//using Microsoft.VisualBasic;
//using QRCoder;

//namespace IndoorPlaygroundSafetyCheck.ViewModels
//{
//    public class AddStationViewModel : INotifyPropertyChanged
//    {
//        private readonly Data.SafetyCheckContext _context = new Data.SafetyCheckContext();
//        private string _stationName;

//        public string StationName
//        {
//            get => _stationName;
//            set
//            {
//                _stationName = value;
//                OnPropertyChanged();
//            }
//        }

//        public ObservableCollection<Station> Stations { get; private set; }

//        public ICommand AddStationCommand { get; private set; }

//        public AddStationViewModel()
//        {
//            Stations = new ObservableCollection<Station>(_context.Stations.ToList());
//            AddStationCommand = new Commands.RelayCommand(AddStationExecute, CanAddStationExecute);
//        }


//        private bool CanAddStationExecute(object parameter)
//        {
//            return !string.IsNullOrWhiteSpace(StationName);
//        }

//        private void AddStationExecute(object parameter)
//        {
//            string inputRfidUid = Interaction.InputBox("Bitte benutzen Sie die Admin-RFID-Karte", "Admin RFID Verification", "");
//            if (!IsAdminRfidUid(inputRfidUid))
//            {
//                MessageBox.Show("Invalid or unauthorized RFID UID.");
//                return;
//            }

//            var newStation = new Station
//            {
//                Name = StationName,
//                InsertTimeStamp = DateTime.Now,
//                UpdatedBy = LoginViewModel.LoggedInUser.FullName,
//                UpdateTimeStamp = DateTime.Now,
//                InsertedBy = LoginViewModel.LoggedInUser.FullName

//            };

//            _context.Stations.Add(newStation);
//            _context.SaveChanges(); // Save to get the Ident

//            // Generate and save QR code


//            Stations.Add(newStation);
//            StationName = string.Empty; // Reset for next input
//            OnPropertyChanged(nameof(StationName));
//        }

//        private bool IsAdminRfidUid(string rfidUid)
//        {
//            return _context.Employees.Any(e => e.RfidUid == rfidUid && e.Position == 1); // Assume 1 is Admin position identifier
//        }


//        public event PropertyChangedEventHandler PropertyChanged;
//        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }
//}





using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using IndoorPlaygroundSafetyCheck.Commands;
using IndoorPlaygroundSafetyCheck.Data;
using IndoorPlaygroundSafetyCheck.Models;
using IndoorPlaygroundSafetyCheck.Views;

namespace IndoorPlaygroundSafetyCheck.ViewModels
{
    public class AddStationViewModel : INotifyPropertyChanged
    {
        private readonly SafetyCheckContext _context = new SafetyCheckContext();
        private string _stationName;

        public string StationName
        {
            get => _stationName;
            set
            {
                _stationName = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Station> Stations { get; private set; }

        public ICommand AddStationCommand { get; private set; }

        public AddStationViewModel()
        {
            Stations = new ObservableCollection<Station>(_context.Stations.ToList());
            AddStationCommand = new RelayCommand(AddStationExecute, CanAddStationExecute);
        }

        private bool CanAddStationExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(StationName);
        }

        private void AddStationExecute(object parameter)
        {
            var inputDialog = new InputDialog();
            if (inputDialog.ShowDialog() == true)
            {
                string inputRfidUid = inputDialog.RfidUid;
                if (!IsAdminRfidUid(inputRfidUid))
                {
                    MessageBox.Show("Invalid or unauthorized RFID UID.");
                    return;
                }

                var newStation = new Station
                {
                    Name = StationName,
                    InsertTimeStamp = DateTime.Now,
                    UpdatedBy = LoginViewModel.LoggedInUser.FullName,
                    UpdateTimeStamp = DateTime.Now,
                    InsertedBy = LoginViewModel.LoggedInUser.FullName
                };

                _context.Stations.Add(newStation);
                _context.SaveChanges(); // Save to get the Ident

                // Generate and save QR code

                Stations.Add(newStation);
                StationName = string.Empty; // Reset for next input
                OnPropertyChanged(nameof(StationName));
            }
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

