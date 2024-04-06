using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using IndoorPlaygroundSafetyCheck.ViewModels;
using IndoorPlaygroundSafetyCheck.Models;

namespace IndoorPlaygroundSafetyCheck.Views
{
    /// <summary>
    /// Interaction logic for DailyInspection.xaml
    /// </summary>
    public partial class DailyInspection : UserControl
    {
        private MainWindow _mainWindow;
        public event EventHandler RequestClose;

        public DailyInspection(MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = new DailyInspectionViewModel();
            _mainWindow = mainWindow;
        }

        private void DisplayStatisticsView()
        {
            _mainWindow.MainContentArea.Content = new Statistics();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayStatisticsView();
        }

        private void ClearSignature_Click(object sender, RoutedEventArgs e)
        {
            signatureCanvas.Strokes.Clear(); // Clears all strokes from the InkCanvas
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as DailyInspectionViewModel;
            if (viewModel != null && signatureCanvas.Strokes.Count > 0)
            {
                viewModel.SignatureStrokes = new StrokeCollection(signatureCanvas.Strokes);
                if (viewModel.SendCommand.CanExecute(null))
                {
                    viewModel.SendCommand.Execute(null);
                }
            }
            else
            {
                MessageBox.Show("Please make sure to sign before sending.", "Signature Required");
            }
        }

        public class StationWithQuestions : INotifyPropertyChanged
        {
            public string StationName { get; set; }
            public List<StationQuestion> StationQuestions { get; set; } // Assuming you have a StationQuestion model

            private bool _isScanStarted;
            public bool IsScanStarted
            {
                get => _isScanStarted;
                set
                {
                    _isScanStarted = value;
                    OnPropertyChanged(nameof(IsScanStarted));
                }
            }

            // Implement INotifyPropertyChanged interface
            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
