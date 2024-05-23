using IndoorPlaygroundSafetyCheck.Data;
using IndoorPlaygroundSafetyCheck.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace IndoorPlaygroundSafetyCheck.ViewModels
{
    public class StationQuestionViewModel : INotifyPropertyChanged
    {
        private readonly Data.SafetyCheckContext _context;

        public ObservableCollection<Station> Stations { get; set; }
        public ObservableCollection<QuestionCatalogue> QuestionCatalogues { get; set; }
        public QuestionCatalogue SelectedQuestionCatalogue { get; set; }
        public ObservableCollection<string> StationNames { get; private set; }
        public ObservableCollection<Inspection> Inspections { get; private set; }
        public ObservableCollection<StationWithQuestions> StationQuestions { get; private set; } = new ObservableCollection<StationWithQuestions>();

        // Action to show message
        public Action<string> ShowMessageAction { get; set; }

        public StationQuestionViewModel()
        {
            _context = new SafetyCheckContext();
            Stations = new ObservableCollection<Station>(_context.Stations.ToList());
            QuestionCatalogues = new ObservableCollection<QuestionCatalogue>(_context.QuestionCatalogues.ToList());
            LoadStationQuestions();
        }

        public void SaveStationQuestion(string questionText, Station selectedStation)
        {
            if (selectedStation == null || string.IsNullOrWhiteSpace(questionText) || SelectedQuestionCatalogue == null)
            {
                ShowMessageAction?.Invoke("Please fill in all fields and select a question catalogue.");
                return;
            }

            var stationQuestion = new StationQuestion
            {
                QuestionText = questionText,
                StationIdentNavigation = selectedStation,
                InsertTimeStamp = DateTime.Now,
                UpdatedBy = LoginViewModel.LoggedInUser.FullName,
                UpdateTimeStamp = DateTime.Now,
                InsertedBy = LoginViewModel.LoggedInUser.FullName,
                QuestionCatalogueIdentNavigation = SelectedQuestionCatalogue
            };

            _context.StationQuestions.Add(stationQuestion);
            _context.SaveChanges();

            // Load the updated list of station questions
            LoadStationQuestions();
            OnPropertyChanged(nameof(StationQuestions));
        }

        private void LoadStationQuestions()
        {
            var allStations = _context.Stations.Include(st => st.StationQuestions).ToList();

            StationQuestions.Clear();

            foreach (var station in allStations)
            {
                var stationWithQuestions = new StationWithQuestions
                {
                    StationName = station.Name,
                    StationQuestions = station.StationQuestions.Select(q => new StationQuestion
                    {
                        QuestionText = q.QuestionText,
                    }).ToList()
                };

                StationQuestions.Add(stationWithQuestions);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadStationNames()
        {
            var stationNamesFromDb = _context.Stations.Select(station => station.Name).ToList();
            StationNames = new ObservableCollection<string>(stationNamesFromDb);
            OnPropertyChanged(nameof(StationNames));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
