using IndoorPlaygroundSafetyCheck.Data;
using IndoorPlaygroundSafetyCheck.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IndoorPlaygroundSafetyCheck.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private Data.SafetyCheckContext _context;
        public ObservableCollection<Employee> Employees { get; set; }

        public EmployeeViewModel()
        {
            _context = new Data.SafetyCheckContext();
            Employees = new ObservableCollection<Employee>(_context.Employees.ToList());
        }

        // Implement INotifyPropertyChanged members
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}