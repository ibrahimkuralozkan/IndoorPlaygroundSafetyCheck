using Microsoft.Win32; // For OpenFileDialog
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using IndoorPlaygroundSafetyCheck.ViewModels;
using IndoorPlaygroundSafetyCheck.Models;
using System.Windows.Media;
using System;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace IndoorPlaygroundSafetyCheck.Views
{
    public partial class TrainingView : UserControl
    {
        public TrainingView()
        {
            InitializeComponent();
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is Training training && !string.IsNullOrEmpty(training.VideoPath))
            {
                OpenVideo(training.VideoPath);
            }
        }

        private void OpenVideo(string path)
        {
            try
            {
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to open video: {ex.Message}");
            }
        }

        private void UploadCorrectSetupImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*" };
            if (openFileDialog.ShowDialog() == true)
            {
                var viewModel = DataContext as TrainingViewModel;
                viewModel.TempCorrectSetupImagePath = openFileDialog.FileName;
            }
        }

        private void UploadCommonErrorsImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*" };
            if (openFileDialog.ShowDialog() == true)
            {
                var viewModel = DataContext as TrainingViewModel;
                viewModel.TempCommonErrorsImagePath = openFileDialog.FileName;
            }
        }

        private void UploadVideo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "Video files (*.mp4;*.avi;*.mov)|*.mp4;*.avi;*.mov|All files (*.*)|*.*" };
            if (openFileDialog.ShowDialog() == true)
            {
                var viewModel = DataContext as TrainingViewModel;
                viewModel.TempExternalLink = openFileDialog.FileName;
            }
        }

        private void SaveTraining_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as TrainingViewModel;
            viewModel.AddNewTraining(DescriptionTextBox.Text);
        }
        private void DescriptionTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (DescriptionTextBox.Text == "Description")
            {
                DescriptionTextBox.Text = "";
                DescriptionTextBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void DescriptionTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
            {
                DescriptionTextBox.Text = "Description";
                DescriptionTextBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image image && image.Tag is string tag)
            {
                // Determine which popup to open based on the Tag property of the Image
                Popup popup = null;
                if (tag == "CorrectSetup")
                {
                    popup = this.FindName("CorrectSetupPopup") as Popup;
                }
                else if (tag == "CommonErrors")
                {
                    popup = this.FindName("CommonErrorsPopup") as Popup;
                }

                if (popup != null)
                {
                    popup.IsOpen = !popup.IsOpen; // Toggle the visibility of the popup
                }
            }
        }
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image image && image.Tag is string imagePath)
            {
                // Create and configure the popup to display the full-size image
                var popup = new Popup
                {
                    PlacementTarget = image,
                    Placement = PlacementMode.Mouse,
                    AllowsTransparency = true,
                    PopupAnimation = PopupAnimation.Fade,
                    StaysOpen = false, // Close the popup if clicked outside
                    Child = new Image
                    {
                        Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute)),
                        Width = 800, // Set the width to a larger size
                        Height =800, // Adjust the height proportionally
                        Stretch = Stretch.Uniform // Maintain aspect ratio
                    }
                };

                // Open the popup
                popup.IsOpen = true;
            }
        }

    }
}
