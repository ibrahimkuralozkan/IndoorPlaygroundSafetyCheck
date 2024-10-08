﻿using System;
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
using Microsoft.EntityFrameworkCore;

namespace IndoorPlaygroundSafetyCheck.ViewModels
{
    public class DeleteStationViewModel : INotifyPropertyChanged
    {
        private readonly SafetyCheckContext _context = new SafetyCheckContext();
        private Station _selectedStation;
        private string _warningMessage;

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

        public string WarningMessage
        {
            get => _warningMessage;
            set
            {
                if (_warningMessage != value)
                {
                    _warningMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand DeleteStationCommand { get; private set; }

        public DeleteStationViewModel()
        {
            Stations = new ObservableCollection<Station>(_context.Stations.ToList());
            DeleteStationCommand = new RelayCommand(DeleteStationExecute, CanDeleteStationExecute);
        }

        private bool CanDeleteStationExecute(object parameter)
        {
            return SelectedStation != null;
        }

        private void DeleteStationExecute(object parameter)
        {
            var inputDialog = new InputDialog
            {
                Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive)
            };

            if (inputDialog.ShowDialog() == true)
            {
                string inputRfidUid = inputDialog.RfidUid;
                if (!IsAdminRfidUid(inputRfidUid))
                {
                    WarningMessage = "Invalid or unauthorized RFID UID.";
                    ShowWarningMessage(WarningMessage);
                    return;
                }

                try
                {
                    _context.Stations.Remove(SelectedStation);
                    _context.SaveChanges();
                    Stations.Remove(SelectedStation);
                    WarningMessage = string.Empty; // Clear previous warning messages if successful
                }
                catch (DbUpdateException ex) when (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlEx)
                {
                    // SQL Server's FK constraint violation error code is 547
                    if (sqlEx.Number == 547)
                    {
                        WarningMessage = "Das ausgewählte Element hat einen Eintrag in der Historie. Aufgrund dessen ist das Löschen nicht gestattet.";
                        ShowWarningMessage(WarningMessage);
                    }
                    else
                    {
                        throw; // Optionally re-throw other exceptions
                    }
                }
                catch (Exception ex)
                {
                    // Log or handle any unexpected exceptions
                    WarningMessage = $"Unexpected error: {ex.Message}";
                    ShowWarningMessage(WarningMessage);
                }
            }
        }

        private bool IsAdminRfidUid(string rfidUid)
        {
            return _context.Employees.Any(e => e.RfidUid == rfidUid && e.Position == 1); // Assuming 1 is the Admin position identifier
        }

        private void ShowWarningMessage(string message)
        {
            var warningDialog = new InputDialog
            {
                Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive),
                Title = "Warning",
                Message = message,
                IsWarning = true
            };
            warningDialog.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
