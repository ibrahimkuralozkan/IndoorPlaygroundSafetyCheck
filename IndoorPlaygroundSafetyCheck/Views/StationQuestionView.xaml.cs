using IndoorPlaygroundSafetyCheck.Models;
using IndoorPlaygroundSafetyCheck.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace IndoorPlaygroundSafetyCheck.Views
{
    public partial class StationQuestionView : UserControl
    {
        public StationQuestionView()
        {
            InitializeComponent();
            var viewModel = (StationQuestionViewModel)DataContext;
            viewModel.ShowMessageAction = ShowMessage;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (StationQuestionViewModel)DataContext;
            var questionText = QuestionTextInput.Text;
            var selectedStation = (Station)StationDropDown.SelectedItem;

            viewModel.SaveStationQuestion(questionText, selectedStation);

            // Clear input fields after saving
            QuestionTextInput.Text = string.Empty;
            StationDropDown.SelectedItem = null;
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
 