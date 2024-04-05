using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using IndoorPlaygroundSafetyCheck.ViewModels;
using IndoorPlaygroundSafetyCheck.Data; 

namespace IndoorPlaygroundSafetyCheck.Views
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            var context = new SafetyCheckContext();
            var viewModel = new LoginViewModel(context);

            viewModel.OnSuccessfulLogin = () =>
            {
                // Pass LoggedInUser to the MainWindow
                var mainWindow = new MainWindow(LoginViewModel.LoggedInUser);
                mainWindow.Show();
                this.Close();
            };

            DataContext = viewModel;
        }
        private void PasswordBoxRfid_Loaded(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            if (passwordBox != null)
            {
                passwordBox.Focus();
            }
        }


        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                // Determine if RFID or username/password should be used for login
                if (!string.IsNullOrWhiteSpace(passwordBoxRfid.Password))
                {
                    viewModel.Rfid = passwordBoxRfid.Password;
                }
                else
                {
                   // viewModel.Password = passwordBoxPassword.Password;
                }
                viewModel.LoginCommand.Execute(null);
            }
        }

    }
}
