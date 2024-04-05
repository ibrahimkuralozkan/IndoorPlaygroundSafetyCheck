using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using IndoorPlaygroundSafetyCheck.Data;
using IndoorPlaygroundSafetyCheck.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows.Input;
using IndoorPlaygroundSafetyCheck.Commands;
using System.Diagnostics;

namespace IndoorPlaygroundSafetyCheck.ViewModels
{
    public class TrainingViewModel : INotifyPropertyChanged
    {
      
        private ObservableCollection<Training> _trainings = new ObservableCollection<Training>();
        private ObservableCollection<Station> _stations = new ObservableCollection<Station>();
        private Station _selectedStation;
        private readonly SafetyCheckContext _context = new SafetyCheckContext();
        public ICommand OpenVideoCommand { get; }
        public TrainingViewModel()
        {
            LoadStationsAsync();
            LoadTrainingsAsync(); // Load existing trainings
            OpenVideoCommand = new RelayCommand(OpenVideo);
        }
        private void OpenVideo(object parameter)
        {
            var path = parameter as string;
            if (!string.IsNullOrWhiteSpace(path))
            {
                try
                {
                    Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening video: {ex.Message}");
                }
            }
        }

        public ObservableCollection<Training> Trainings { get => _trainings; set => SetProperty(ref _trainings, value); }
        public ObservableCollection<Station> Stations { get => _stations; set => SetProperty(ref _stations, value); }
        public Station SelectedStation { get => _selectedStation; set => SetProperty(ref _selectedStation, value); }

        public string TempCorrectSetupImagePath { get; set; }
        public string TempCommonErrorsImagePath { get; set; }
        public string TempExternalLink { get; set; }

        private async void LoadStationsAsync()
        {
            using var context = new SafetyCheckContext();
            var stations = await context.Stations.ToListAsync();
            foreach (var station in stations)
            {
                Stations.Add(station);
            }
        }

        private async void LoadTrainingsAsync()
        {
            using var context = new SafetyCheckContext();
            var trainings = await context.Training.Include(t => t.StationIdentNavigation).ToListAsync();
            foreach (var training in trainings)
            {
                Trainings.Add(training);
            }
        }

        public void AddNewTraining(string description)
        {
            if (SelectedStation == null)
            {
                MessageBox.Show("Please select a station.");
                return;
            }

            var newTraining = new Training
            {
                Description = description,
                CorrectSetupImagePath = TempCorrectSetupImagePath,
                CommonErrorsImagePath = TempCommonErrorsImagePath,
                VideoPath = TempExternalLink,
                StationIdent = SelectedStation.Ident
            };

            _context.Training.Add(newTraining);
            _context.SaveChanges();

            Trainings.Add(newTraining); // Add the new training to the observable collection
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
