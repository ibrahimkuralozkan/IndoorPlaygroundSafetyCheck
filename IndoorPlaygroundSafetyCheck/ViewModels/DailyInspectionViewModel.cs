using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using IndoorPlaygroundSafetyCheck.Commands;
using IndoorPlaygroundSafetyCheck.Enums;
using IndoorPlaygroundSafetyCheck.Data;
using IndoorPlaygroundSafetyCheck.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows.Ink;
using RelayCommand = IndoorPlaygroundSafetyCheck.Commands.RelayCommand;
using System.Windows.Media.Imaging;
using Microsoft.IdentityModel.Tokens;

namespace IndoorPlaygroundSafetyCheck.ViewModels
{
    public class DailyInspectionViewModel : INotifyPropertyChanged
    {
        public ICommand StartScanCommand { get; private set; }
        public ICommand StartDailyInspectionCommand { get; private set; }
        public ICommand SendCommand { get; private set; }

        private bool _isScanEnabled = false;
        public bool IsScanEnabled
        {
            get => _isScanEnabled;
            set
            {
                _isScanEnabled = value;
                OnPropertyChanged(nameof(IsScanEnabled));
            }
        }

        private readonly SafetyCheckContext _context = new SafetyCheckContext();
        private bool _isInspectionReadyToSend;

        private BitmapImage _currentFrame;
        public BitmapImage CurrentFrame
        {
            get => _currentFrame;
            set
            {
                _currentFrame = value;
                OnPropertyChanged(nameof(CurrentFrame));
            }
        }

        public DailyInspectionViewModel()
        {
            StartDailyInspectionCommand = new RelayCommand(StartDailyInspection);
            SendCommand = new RelayCommand(SendInspection, CanSendInspection);
            LoadInspections();
            LoadStationNames();
            LoadStationQuestions();
        }

        private void SendInspection(object parameter)
        {
            if (SelectedInspection != null && LoginViewModel.LoggedInUser != null &&
                SignatureStrokes != null && SignatureStrokes.Count > 0)
            {
                // Check if any station has a pending repair
                bool hasPendingRepair = false;
                foreach (var stationWithQuestions in StationQuestions)
                {
                    foreach (var question in stationWithQuestions.StationQuestions)
                    {
                        if ((question.YesSelected || question.NaSelected) &&
                            question.ErrorType == 2 &&    // Ensure ErrorType is used correctly
                            question.RepairPlan != null && // Check if RepairPlan has a value
                            question.RepairedTime == null)
                        {
                            hasPendingRepair = true;
                            break;
                        }
                    }
                    if (hasPendingRepair)
                        break;
                }
                // If any station has a pending repair, show the message and return
                if (hasPendingRepair)
                {
                    MessageBox.Show("You cannot select Ready to Use or Caution as there is a pending repair for one or more stations.", "Error");
                    return;
                }

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    SignatureStrokes.Save(memoryStream);
                    SelectedInspection.Signature = memoryStream.ToArray();
                }

                // Set CheckDone to the current DateTime
                SelectedInspection.CheckDone = DateTime.Now;
                SelectedInspection.UpdatedBy = $"{LoginViewModel.LoggedInUser.FirstName} {LoginViewModel.LoggedInUser.LastName}";
                SelectedInspection.IsSent = (int)SendStatus.Sended;

                // Update the SelectedInspection entity in the database context
                _context.Inspections.Update(SelectedInspection);

                foreach (var stationWithQuestions in StationQuestions)
                {
                    var station = _context.Stations.FirstOrDefault(s => s.Name == stationWithQuestions.StationName);

                    if (station == null)
                        continue;

                    foreach (var question in stationWithQuestions.StationQuestions)
                    {
                        int? errorType = question.YesSelected ? 0 :
                                         question.NaSelected ? 1 :
                                         question.NoSelected ? 2 : (int?)null;

                        var inspectionQuestionResult = new InspectionQuestionResult
                        {
                            InspectionIdent = SelectedInspection.Ident,
                            StationQuestionIdent = question.Ident,
                            QuestionText = question.QuestionText,
                            ErrorType = errorType,
                            Notes = question.Notes,
                            InsertTimeStamp = DateTime.Now, // Correction made here
                            InsertedBy = LoginViewModel.LoggedInUser.FullName,
                            UpdatedBy = LoginViewModel.LoggedInUser.FullName,
                            UpdateTimeStamp = DateTime.Now,
                            StationIdent = station.Ident
                        };


                        _context.InspectionQuestionResults.Add(inspectionQuestionResult);
                    }
                }

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException dbEx)
                {
                    var errorMsg = dbEx.InnerException?.Message ?? dbEx.Message;
                    MessageBox.Show($"Error saving inspection results: {errorMsg}", "Database Error");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error");
                }
            }
            else
            {
                MessageBox.Show("Inspection or User data is not available, or signature is missing.", "Error");
            }
        }

        public StrokeCollection SignatureStrokes
        {
            get => _signatureStrokes;
            set
            {
                if (_signatureStrokes != value)
                {
                    _signatureStrokes = value;
                    OnPropertyChanged(nameof(SignatureStrokes));
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private StrokeCollection _signatureStrokes;

        public ObservableCollection<string> StationNames { get; private set; }
        public ObservableCollection<Inspection> Inspections { get; private set; }
        public ObservableCollection<StationWithQuestions> StationQuestions { get; private set; } = new ObservableCollection<StationWithQuestions>();

        private Inspection _selectedInspection;
        public Inspection SelectedInspection
        {
            get => _selectedInspection;
            set
            {
                if (_selectedInspection != value)
                {
                    _selectedInspection = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsInspectionReadyToSend
        {
            get => _isInspectionReadyToSend;
            set
            {
                if (_isInspectionReadyToSend != value)
                {
                    _isInspectionReadyToSend = value;
                    OnPropertyChanged(nameof(IsInspectionReadyToSend));
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        private bool CanSendInspection(object parameter)
        {
            return SelectedInspection != null && IsInspectionReadyToSend;
        }

        private void StartDailyInspection(object parameter)
        {
            if (LoginViewModel.LoggedInUser != null)
            {
                var newInspection = new Inspection
                {
                    CheckStart = DateTime.Now,
                    Type = 1,
                    InsertedBy = LoginViewModel.LoggedInUser.FullName,
                    UpdatedBy = LoginViewModel.LoggedInUser.FullName,
                    EmployeeIdent = LoginViewModel.LoggedInUser.Ident,
                    RfidUid = LoginViewModel.LoggedInUser.RfidUid
                };

                _context.Inspections.Add(newInspection);
                _context.SaveChanges();

                LoadInspections();
                SelectedInspection = newInspection;

                IsInspectionReadyToSend = true;
                IsScanEnabled = true;
                MessageBox.Show($"Daily Inspection with ID \"{newInspection.Ident}\" started.", "Inspection Started");
                IsScanEnabled = true;
            }
            else
            {
                MessageBox.Show("No user logged in.");
            }
        }

        private void LoadInspections()
        {
            var inspectionsFromDb = _context.Inspections.ToList();
            Inspections = new ObservableCollection<Inspection>(inspectionsFromDb);
            OnPropertyChanged(nameof(Inspections));
        }

        private void LoadStationNames()
        {
            var stationNamesFromDb = _context.Stations.Select(station => station.Name).ToList();
            StationNames = new ObservableCollection<string>(stationNamesFromDb);
            OnPropertyChanged(nameof(StationNames));
        }

        private void LoadStationQuestions()
        {
            StationQuestions.Clear();

            var allStations = _context.Stations.Include(st => st.StationQuestions).ToList();

            foreach (var station in allStations)
            {
                var stationWithQuestions = new StationWithQuestions
                {
                    StationName = station.Name,
                    StationQuestions = station.StationQuestions.ToList()
                };

                StationQuestions.Add(stationWithQuestions);
            }

            OnPropertyChanged(nameof(StationQuestions));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
