using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        public ICommand LoadDataCommand { get; private set; }

        public void LoadData_Click(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is ManagerStatisticsViewModel viewModel))
            {
                MessageBox.Show("ViewModel is not set correctly.");
                return;
            }

            viewModel.StartDate = StartDatePicker.SelectedDate ?? DateTime.Today.AddDays(-7);
            viewModel.EndDate = EndDatePicker.SelectedDate ?? DateTime.Today;

            viewModel.LoadData(); // Directly call the LoadData method
        }


    }
}
