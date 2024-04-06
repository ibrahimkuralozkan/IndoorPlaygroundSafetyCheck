using IndoorPlaygroundSafetyCheck.ViewModels;
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

namespace IndoorPlaygroundSafetyCheck.Views
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    using IndoorPlaygroundSafetyCheck.ViewModels;

    public partial class AddUser : UserControl
    {
        public AddUser()
        {
            InitializeComponent();
            DataContext = new AddUserViewModel(); // Set the DataContext to your ViewModel
        }

        private void StackPanel_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }

}
