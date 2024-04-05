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
    /// Interaction logic for AddStation.xaml
    /// </summary>
    public partial class AddStation : UserControl
    {
        public AddStation()
        {
            InitializeComponent();
            DataContext = new AddStationViewModel();
        }

     

       
    }
}
