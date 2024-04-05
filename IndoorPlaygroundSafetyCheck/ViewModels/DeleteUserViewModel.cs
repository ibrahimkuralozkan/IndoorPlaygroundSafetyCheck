using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using IndoorPlaygroundSafetyCheck.Commands;
using IndoorPlaygroundSafetyCheck.Data;
using IndoorPlaygroundSafetyCheck.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.VisualBasic; // Needed for Interaction.InputBox
using Microsoft.EntityFrameworkCore;


namespace IndoorPlaygroundSafetyCheck.ViewModels
{
    public class DeleteUserViewModel : INotifyPropertyChanged
    {
        private readonly Data.SafetyCheckContext _context = new Data.SafetyCheckContext();
        public ObservableCollection<Employee> Employees { get; } = new ObservableCollection<Employee>();

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set { _selectedEmployee = value; OnPropertyChanged(); }
        }

        public ICommand DeleteUserCommand { get; }

        public DeleteUserViewModel()
        {
            DeleteUserCommand = new Commands.RelayCommand(DeleteUserExecute, CanDeleteUserExecute);
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
            // Input RFID UID from the admin to authorize deletion
            string inputRfidUid = Interaction.InputBox("Use Admin RFID Card please", "Admin RFID Verification", "");

            // Check if RFID UID is valid and if the user has admin privileges
            if (string.IsNullOrWhiteSpace(inputRfidUid) || !IsAdminRfidUid(inputRfidUid))
            {
                MessageBox.Show("Invalid or unauthorized RFID UID.", "Authorization Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Check if a user is selected
            if (SelectedEmployee == null || string.IsNullOrWhiteSpace(SelectedEmployee.RfidUid))
            {
                MessageBox.Show("No employee selected or invalid RFID UID.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Ensure the DbContext is not null
                if (_context == null)
                {
                    MessageBox.Show("Database context is not initialized.", "Initialization Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Call the DeleteEmployee method in the context to execute the stored procedure
                _context.DeleteEmployee(SelectedEmployee.RfidUid);

                // Ensure changes are saved to the database
                _context.SaveChanges();

                // Refresh the employees list to reflect the deletion
                LoadEmployees();

                // Notify the user of successful deletion
                //   MessageBox.Show($"{SelectedEmployee.FirstName} {SelectedEmployee.LastName} has been deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private bool IsAdminRfidUid(string rfidUid)
        {
            // Check if the provided RFID UID belongs to an admin
            var employee = _context.Employees.FirstOrDefault(e => e.RfidUid == rfidUid && e.Position == 1); // Assuming PositionIdent 1 is for admins
            return employee != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}