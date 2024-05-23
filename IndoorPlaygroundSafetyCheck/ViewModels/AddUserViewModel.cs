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
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace IndoorPlaygroundSafetyCheck.ViewModels
{
    public class AddUserViewModel : INotifyPropertyChanged
    {
        private readonly SafetyCheckContext _context = new SafetyCheckContext();

        public AddUserViewModel()
        {
            AddUserCommand = new RelayCommand(AddUserExecute, CanAddUserExecute);
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
                OnPropertyChanged();
            }
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

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        private string? _lastName;
        public string? LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        private string _position;
        public string Position
        {
            get => _position;
            set
            {
                _position = value;
                OnPropertyChanged();
            }
        }

        private string _rfidUid;
        public string RfidUid
        {
            get => _rfidUid;
            set
            {
                _rfidUid = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddUserCommand { get; }

        private bool CanAddUserExecute(object parameter)
        {
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
                _context.AddEmployee(FirstName, LastName ?? string.Empty, ConvertPosition(Position), RfidUid);
                _context.SaveChanges();

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
            var employee = _context.Employees.FirstOrDefault(e => e.RfidUid == rfidUid && e.Position == 1);
            return employee != null;
        }

        private int ConvertPosition(string position)
        {
            return position == "Manager" ? 1 : 2;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
