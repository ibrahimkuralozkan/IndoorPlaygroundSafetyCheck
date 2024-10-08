﻿// path: IndoorPlaygroundSafetyCheck/Views/DeleteStationView.xaml.cs

using System.Windows;
using System.Windows.Controls;
using IndoorPlaygroundSafetyCheck.ViewModels;

namespace IndoorPlaygroundSafetyCheck.Views
{
    public partial class DeleteStationView : UserControl
    {
        public DeleteStationView()
        {
            InitializeComponent();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as DeleteStationViewModel;
            vm?.DeleteStationCommand.Execute(null);
        }
    }
}
