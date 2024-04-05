using System;
using System.Windows.Input;
using IndoorPlaygroundSafetyCheck.Commands;
using IndoorPlaygroundSafetyCheck.Data;
using IndoorPlaygroundSafetyCheck.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace IndoorPlaygroundSafetyCheck.ViewModels
{
    public class AddUserViewModel : INotifyPropertyChanged
    {
        private readonly Data.SafetyCheckContext _context = new Data.SafetyCheckContext();
        public AddUserViewModel()
        {
            AddUserCommand = new Commands.RelayCommand(AddUserExecute, CanAddUserExecute);
            Employees = new ObservableCollection<Employee>();
            LoadEmployees();
        }

        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }
        private void LoadEmployees()
        {
            Employees.Clear();
            using (var context = new Data.SafetyCheckContext())
            {
                var employees = context.Employees.ToList();
                foreach (var employee in employees)
                {
                    Employees.Add(employee);
                }
            }
        }

        private string _insertedBy;
        public string InsertedBy
        {
            get => _insertedBy;
            set
            {
                _insertedBy = value; OnPropertyChanged(nameof(InsertedBy));
            }
        }

        private string _updatedBy;
        public string UpdatedBy
        {
            get => _updatedBy;
            set
            {
                _updatedBy = value; OnPropertyChanged(nameof(UpdatedBy));
            }
        }
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(); }
        }

        private string? _lastName;
        public string? LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(); }
        }

        private int _position;
        public int Position
        {
            get => _position;
            set { _position = value; OnPropertyChanged(); }
        }

        private string _rfidUid;
        public string RfidUid
        {
            get => _rfidUid;
            set { _rfidUid = value; OnPropertyChanged(); }
        }

        public ICommand AddUserCommand { get; private set; }

       
        private bool CanAddUserExecute(object parameter)
        {
            // Basic validation (extend as needed)
            return !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(RfidUid);
        }

        private void AddUserExecute(object parameter)
        {
            string inputRfidUid = Interaction.InputBox("Use Admin RFID Card please", "Admin RFID Verification", "");

            if (string.IsNullOrWhiteSpace(inputRfidUid) || !IsAdminRfidUid(inputRfidUid))
            {
                MessageBox.Show("Invalid or unauthorized RFID UID.");
                return;
            }

            try
            {
                // Assuming AddEmployee is a method that adds an employee and returns the ID
                // For displaying names, we don't need to change how the employee is added
                _context.AddEmployee(FirstName, LastName ?? string.Empty, Position, RfidUid);
                _context.SaveChanges(); // Ensure changes are saved if AddEmployee does not

                LoadEmployees();
                MessageBox.Show($"User added successfully. New Employee: {FirstName} {LastName}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    

    private bool IsAdminRfidUid(string rfidUid)
        {
            // Assuming Employee has a RfidUid property and a PositionIdent property
            var employee = _context.Employees.FirstOrDefault(e => e.RfidUid == rfidUid && e.Position == 1);
            return employee != null;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
