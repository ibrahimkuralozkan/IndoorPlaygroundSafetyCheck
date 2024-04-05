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
using System.Windows.Navigation;
using System.Windows.Shapes;
using IndoorPlaygroundSafetyCheck.ViewModels;


namespace IndoorPlaygroundSafetyCheck.Views
{
    public partial class DeleteStationView : UserControl
    {
        public DeleteStationView()
        {
            InitializeComponent();
            DataContext = new DeleteStationViewModel();
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Your deletion logic here, or simply invoke the command if applicable
        }

    }
}
