// path: IndoorPlaygroundSafetyCheck/Views/DeleteUser.xaml.cs

using System.Windows;
using System.Windows.Controls;
using IndoorPlaygroundSafetyCheck.ViewModels;

namespace IndoorPlaygroundSafetyCheck.Views
{
    public partial class DeleteUser : UserControl
    {
        public DeleteUser()
        {
            InitializeComponent();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as DeleteUserViewModel;
            vm?.DeleteUserCommand.Execute(null);

            if (!string.IsNullOrEmpty(vm?.WarningMessage))
            {
                MessageBox.Show(vm.WarningMessage, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
