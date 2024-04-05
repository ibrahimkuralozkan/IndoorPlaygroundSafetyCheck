using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IndoorPlaygroundSafetyCheck.Data;

namespace IndoorPlaygroundSafetyCheck.ViewModels
{
    public class StatisticsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<InspectionQuestionResultViewModel> Results { get; } = new ObservableCollection<InspectionQuestionResultViewModel>();
        public ObservableCollection<string> StationsErrorTypeZero { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> StationsErrorTypeOne { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> StationsErrorTypeTwo { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> UncheckedStations { get; } = new ObservableCollection<string>();
        

        private int _errorTypeZeroCount;
        private int _errorTypeOneCount;
        private int _errorTypeTwoCount;
        private int _uncheckedStationsCount;

        public int ErrorTypeZeroCount
        {
            get => _errorTypeZeroCount;
            set => SetProperty(ref _errorTypeZeroCount, value);
        }

        public int ErrorTypeOneCount
        {
            get => _errorTypeOneCount;
            set => SetProperty(ref _errorTypeOneCount, value);
        }

        public int ErrorTypeTwoCount
        {
            get => _errorTypeTwoCount;
            set => SetProperty(ref _errorTypeTwoCount, value);
        }

        public int UncheckedStationsCount
        {
            get => _uncheckedStationsCount;
            set => SetProperty(ref _uncheckedStationsCount, value);
        }

        public StatisticsViewModel()
        {
            LoadStationCountsAsync();
            LoadResultsAsync(); // Ensure this method is called to load the data
        }

        private async Task LoadStationCountsAsync()
        {
            using var context = new SafetyCheckContext();
            var today = System.DateTime.Today;

            // Fetch stations and check for the highest error type today
            var stationsCheckedToday = await context.InspectionQuestionResults
                .Where(result => result.InsertTimeStamp.Date == today)
                .GroupBy(result => result.StationIdent)
                .Select(group => new
                {
                    StationIdent = group.Key,
                    HasNullErrorTypes = group.All(result => result.ErrorType == null),
                    MaxErrorType = group.Max(result => result.ErrorType)
                })
                .ToListAsync();

            var allStations = await context.Stations.ToDictionaryAsync(s => s.Ident, s => s.Name);

            StationsErrorTypeZero.Clear();
            StationsErrorTypeOne.Clear();
            StationsErrorTypeTwo.Clear();
            UncheckedStations.Clear();

            foreach (var station in stationsCheckedToday)
            {
                var stationName = allStations[station.StationIdent];
                if (!station.HasNullErrorTypes)
                {
                    switch (station.MaxErrorType)
                    {
                        case 0: StationsErrorTypeZero.Add(stationName); break;
                        case 1: StationsErrorTypeOne.Add(stationName); break;
                        case 2: StationsErrorTypeTwo.Add(stationName); break;
                    }
                }
            }

            // Stations with all null ErrorType are considered unchecked
            var uncheckedStationIdents = allStations.Keys.Except(stationsCheckedToday.Select(s => s.StationIdent)).ToList();
            uncheckedStationIdents.AddRange(stationsCheckedToday.Where(s => s.HasNullErrorTypes).Select(s => s.StationIdent));
            foreach (var ident in uncheckedStationIdents.Distinct())
            {
                UncheckedStations.Add(allStations[ident]);
            }

            ErrorTypeZeroCount = StationsErrorTypeZero.Count;
            ErrorTypeOneCount = StationsErrorTypeOne.Count;
            ErrorTypeTwoCount = StationsErrorTypeTwo.Count;
            UncheckedStationsCount = UncheckedStations.Count;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        private async Task LoadResultsAsync()
        {
            using var context = new SafetyCheckContext();
            var results = await (from ir in context.InspectionQuestionResults
                                 join s in context.Stations on ir.StationIdent equals s.Ident
                                 select new InspectionQuestionResultViewModel
                                 {
                                     Ident = ir.Ident,
                                     InspectionIdent = ir.InspectionIdent,
                                     StationQuestionIdent = ir.StationQuestionIdent,
                                     QuestionText = ir.QuestionText,
                                     ErrorType = ir.ErrorType,
                                     Notes = ir.Notes,
                                     InsertTimeStamp = ir.InsertTimeStamp,
                                     InsertedBy = ir.InsertedBy,
                                     UpdatedBy = ir.UpdatedBy,
                                     UpdateTimeStamp = ir.UpdateTimeStamp,
                                     StationIdent = ir.StationIdent,
                                     StationName = s.Name // Fetching the station name
                                 }).ToListAsync();

            Results.Clear();
            results.ForEach(Results.Add);
        }


    }

}
