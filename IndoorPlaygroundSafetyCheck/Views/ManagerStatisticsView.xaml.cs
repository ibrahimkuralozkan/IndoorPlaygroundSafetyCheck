using System.Windows;
using System.Windows.Controls;
using IndoorPlaygroundSafetyCheck.Data;
using IndoorPlaygroundSafetyCheck.ViewModels;

namespace IndoorPlaygroundSafetyCheck.Views
{
    public partial class ManagerStatisticsView : UserControl
    {
        public ManagerStatisticsView(SafetyCheckContext context)
        {
            InitializeComponent();
            var viewModel = new ManagerStatisticsViewModel(context);
            this.DataContext = viewModel;
        }

        private void LoadData_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ManagerStatisticsViewModel;
            if (viewModel != null)
            {
                viewModel.StartDate = StartDatePicker.SelectedDate ?? DateTime.Today.AddDays(-7); // Use selected date or default
                viewModel.EndDate = EndDatePicker.SelectedDate ?? DateTime.Today; // Use selected date or default
                viewModel.LoadDataCommand.Execute(null);
            }
        }
    }
}
