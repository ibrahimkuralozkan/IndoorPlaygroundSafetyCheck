// path: IndoorPlaygroundSafetyCheck/ViewModels/DeleteUserViewModel.cs

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using IndoorPlaygroundSafetyCheck.Commands;
using IndoorPlaygroundSafetyCheck.Data;
using IndoorPlaygroundSafetyCheck.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;

namespace IndoorPlaygroundSafetyCheck.ViewModels
{
    public class DeleteUserViewModel : INotifyPropertyChanged
    {
        private readonly SafetyCheckContext _context = new SafetyCheckContext();
        public ObservableCollection<Employee> Employees { get; } = new ObservableCollection<Employee>();

        private Employee _selectedEmployee;
        private string _warningMessage;

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged();
            }
        }

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

        public ICommand DeleteUserCommand { get; }

        public DeleteUserViewModel()
        {
            DeleteUserCommand = new RelayCommand(DeleteUserExecute, CanDeleteUserExecute);
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            Employees.Clear();
            var employees = _context.Employees.ToList();
            foreach (var employee in employees)
            {
                Employees.Add(employee);
            }
        }

        private bool CanDeleteUserExecute(object parameter) => SelectedEmployee != null;

        private void DeleteUserExecute(object parameter)
        {
            string inputRfidUid = Interaction.InputBox("Use Admin RFID Card please", "Admin RFID Verification", "");

            if (string.IsNullOrWhiteSpace(inputRfidUid) || !IsAdminRfidUid(inputRfidUid))
            {
                WarningMessage = "Invalid or unauthorized RFID UID.";
                return;
            }

            if (SelectedEmployee == null || string.IsNullOrWhiteSpace(SelectedEmployee.RfidUid))
            {
                WarningMessage = "No employee selected or invalid RFID UID.";
                return;
            }

            try
            {
                _context.Employees.Remove(SelectedEmployee);
                _context.SaveChanges();
                Employees.Remove(SelectedEmployee);
                WarningMessage = string.Empty; // Clear previous warnings if successful
            }
            catch (DbUpdateException ex) when (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number == 547)
                {
                    WarningMessage = "Selected user has associated records. Deletion is not permitted.";
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                WarningMessage = $"Unexpected error: {ex.Message}";
            }
        }

        private bool IsAdminRfidUid(string rfidUid)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.RfidUid == rfidUid && e.Position == 1);
            return employee != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
