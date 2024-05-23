using System.Windows;

namespace IndoorPlaygroundSafetyCheck.Views
{
    public partial class InputDialog : Window
    {
        public string RfidUid { get; private set; }
        public string Message { get; set; }
        public bool IsWarning { get; set; }

        public InputDialog()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWarning)
            {
                RfidUid = passwordBox.Password;
                DialogResult = true;
            }
            else
            {
                DialogResult = false;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
