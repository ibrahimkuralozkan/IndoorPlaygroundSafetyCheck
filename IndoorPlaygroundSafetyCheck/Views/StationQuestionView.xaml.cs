using IndoorPlaygroundSafetyCheck.Models;
using IndoorPlaygroundSafetyCheck.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IndoorPlaygroundSafetyCheck.Views
{
    public partial class StationQuestionView : UserControl
    {



        public StationQuestionView()
        {
            InitializeComponent();
            this.DataContext = new StationQuestionViewModel();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as StationQuestionViewModel;
            if (viewModel != null)
            {
                viewModel.SaveStationQuestion(QuestionTextInput.Text, StationDropDown.SelectedItem as Station);
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Enter Question Text")
            {
                textBox.Text = "";
                textBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Enter Question Text";
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
    }
}
