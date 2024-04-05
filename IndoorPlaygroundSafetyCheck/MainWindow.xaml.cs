using IndoorPlaygroundSafetyCheck.Data;
using IndoorPlaygroundSafetyCheck.Models;
using IndoorPlaygroundSafetyCheck.Views;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace IndoorPlaygroundSafetyCheck
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;

        // Add this property inside the MainWindow class
        public static Employee LoggedInUser { get; set; }


        public MainWindow(Employee loggedInUser)
        {
            InitializeComponent();
            DisplayStatisticsView();
            var dailyInspection = new DailyInspection(this);

            if (loggedInUser != null)
            {
                // Setting FullName
                textBlockFullName.Text = $"{loggedInUser.FirstName} {loggedInUser.LastName}";
                DataContext = new MainWindowViewModel();

                // Adjust visibility of buttons based on role
                AdjustButtonVisibility(loggedInUser.Position);
            }
            this.DataContext = new MainWindowViewModel();
        }
        private void DailyInspection_RequestNavigateToStatistics(object sender, EventArgs e)
        {
            DisplayStatisticsView();
        }

      
        private void DisplayStatisticsView()
        {
            MainContentArea.Content = new Statistics();
        }
        private void AdjustButtonVisibility(int positionId)
        {
            // Assuming positionId: 1 for Mitarbeiter, 4 for Manager
            bool isManager = positionId == 1;

            // Visibility for Manager specific buttons
            var managerButtonsVisibility = isManager ? Visibility.Visible : Visibility.Collapsed;

            // Set the visibility of manager buttons
            btn_AddStation.Visibility = managerButtonsVisibility;
            btn_InspectionQuestion.Visibility = managerButtonsVisibility;
            btn_DeleteStation.Visibility = managerButtonsVisibility;
            btn_AddUser.Visibility = managerButtonsVisibility;
            btn_DeleteUser.Visibility = managerButtonsVisibility;
            //btn_Statistics.Visibility = managerButtonsVisibility;


            // Visibility for Mitarbeiter specific button (Daily Inspection)
            btn_DailyInspection.Visibility = Visibility.Visible; // Always visible
            btn_AfterInspection.Visibility = Visibility.Visible; // Always visible
            btn_Training.Visibility = Visibility.Visible; // Always visible
        }
        private void DailyInspection_Click(object sender, RoutedEventArgs e)
        {
            var dailyInspection = new DailyInspection(this);
            dailyInspection.RequestClose += (s, args) => MainContentArea.Content = null;
            MainContentArea.Content = dailyInspection;
        }




        private void AfterRepairInspection_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the AfterRepairInspection UserControl
            AfterRepairInspection afterRepairInspection = new AfterRepairInspection();

            // Set the MainContentArea's Content to the AfterRepairInspection UserControl
            MainContentArea.Content = afterRepairInspection;
        }

        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            AddStation addStation = new AddStation();
            MainContentArea.Content = addStation;


        }

        private void DeleteStation_Click(object sender, RoutedEventArgs e)
        {
           DeleteStationView deleteStationView = new DeleteStationView();
            MainContentArea.Content = deleteStationView;    


        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUser addUserView = new AddUser();
            MainContentArea.Content = addUserView;


        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            DeleteUser deleteUserView = new DeleteUser();
            MainContentArea.Content = deleteUserView;
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            // Example of directly creating a context.
            // Consider using dependency injection for real applications.
            var context = new SafetyCheckContext(new DbContextOptions<SafetyCheckContext>());

            ManagerStatisticsView statisticsView = new ManagerStatisticsView(context);
            MainContentArea.Content = statisticsView;
        }


        private void Training_Click(object sender, RoutedEventArgs e)
        {
            TrainingView trainingView = new TrainingView();
            MainContentArea.Content = trainingView;

        }
        private void InspectionQuestions_Click(object sender, RoutedEventArgs e)
        {
           QuestionCatalogueView questionCatalogueView = new QuestionCatalogueView();   
            MainContentArea.Content = questionCatalogueView;    

        }

        
        public static class SessionManager
        {
            // Example property to hold the current user's ID
            public static int CurrentUserId { get; set; }

            // Other session-related properties and methods
        }

        private void Questions_Click(object sender, RoutedEventArgs e)
        {
            StationQuestionView stationQuestionView = new StationQuestionView();
            MainContentArea.Content = stationQuestionView;
        }
    }
}