using IndoorPlaygroundSafetyCheck.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Linq;
using IndoorPlaygroundSafetyCheck.Data;
using System.Windows;
using IndoorPlaygroundSafetyCheck.Models;

namespace IndoorPlaygroundSafetyCheck.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly Data.SafetyCheckContext _context;
        private string _rfid;
        public string Rfid
        {
            get => _rfid;
            set
            {
                if (_rfid == value) return;
                _rfid = value;
                OnPropertyChanged();
                if (CanLogin(null))
                {
                    Login(null);
                }
            }
        }

        public static Employee LoggedInUser { get; private set; }
        public Action OnSuccessfulLogin { get; set; }

        private void Login(object parameter)
        {
            Employee employee = _context.Employees.FirstOrDefault(e => e.RfidUid == Rfid);

            if (employee != null)
            {
                LoggedInUser = employee;
                OnSuccessfulLogin?.Invoke();
            }
            else
            {
                MessageBox.Show("Invalid login attempt.");
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel(Data.SafetyCheckContext context)
        {
            _context = context;
            LoginCommand = new Commands.RelayCommand(Login, CanLogin);
        }

        private bool CanLogin(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Rfid);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
