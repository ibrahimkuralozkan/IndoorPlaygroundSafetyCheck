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


namespace IndoorPlaygroundSafetyCheck.Views
{
    public partial class StationView : UserControl
    {
        private StationViewModel _viewModel;

        public StationView()
        {
            InitializeComponent();
            _viewModel = new StationViewModel();
            DataContext = _viewModel;
        }
        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddStation(stationNameTextBox.Text);
        }

        private void UpdateStation_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedStation != null)
                _viewModel.UpdateStation(_viewModel.SelectedStation, stationNameTextBox.Text);
        }

        private void DeleteStation_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedStation != null)
                _viewModel.DeleteStation(_viewModel.SelectedStation);
        }
        private void StationNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (stationNameTextBox.Text == "Station Name")
            {
                stationNameTextBox.Text = "";
                stationNameTextBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void StationNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(stationNameTextBox.Text))
            {
                stationNameTextBox.Text = "Station Name";
                stationNameTextBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

    }
}

