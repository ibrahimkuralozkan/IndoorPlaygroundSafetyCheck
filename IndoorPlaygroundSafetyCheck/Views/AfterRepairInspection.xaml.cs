using System.Windows.Controls;
using IndoorPlaygroundSafetyCheck.ViewModels;

namespace IndoorPlaygroundSafetyCheck.Views
{
    /// <summary>
    /// Interaction logic for AfterRepairInspection.xaml
    /// </summary>
    public partial class AfterRepairInspection : UserControl
    {
        public AfterRepairInspection()
        {
            InitializeComponent();
            DataContext = new AfterRepairInspectionViewModel();
        }
    }
}
