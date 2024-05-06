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
using System.Windows.Navigation;
using System.Windows.Shapes;


using IndoorPlaygroundSafetyCheck.ViewModels;
using System.Diagnostics;

namespace IndoorPlaygroundSafetyCheck.Views
{
    public partial class QuestionCatalogueView : UserControl
    {
        public QuestionCatalogueView()
        {
            InitializeComponent();
            this.DataContext = new QuestionCatalogueViewModel();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as QuestionCatalogueViewModel;
            vm?.SaveQuestionCatalogue(DescriptionTextBox.Text);
            DescriptionTextBox.Clear();
        }
        private void TextBlockMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                using (Process powerShellProcess = new Process())
                {
                    powerShellProcess.StartInfo.FileName = "powershell";
                    powerShellProcess.StartInfo.Arguments = "-Command \"Start-Process osk.exe\"";
                    powerShellProcess.StartInfo.UseShellExecute = false;
                    powerShellProcess.StartInfo.CreateNoWindow = true;
                    powerShellProcess.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to launch OSK via PowerShell: " + ex.Message);
            }
        }





        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as QuestionCatalogueViewModel;
            vm?.DeleteQuestionCatalogue();
        }
    }

}
