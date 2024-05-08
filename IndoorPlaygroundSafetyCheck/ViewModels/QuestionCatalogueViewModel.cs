using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using IndoorPlaygroundSafetyCheck.Data;
using IndoorPlaygroundSafetyCheck.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
using System.Windows.Shapes;

namespace IndoorPlaygroundSafetyCheck.ViewModels
{
    public class QuestionCatalogueViewModel : INotifyPropertyChanged
    {
        private readonly SafetyCheckContext _context;
        public ObservableCollection<QuestionCatalogue> QuestionCatalogues { get; set; }

        private QuestionCatalogue _selectedQuestionCatalogue;
        public QuestionCatalogue SelectedQuestionCatalogue
        {
            get => _selectedQuestionCatalogue;
            set
            {
                if (_selectedQuestionCatalogue != value)
                {
                    _selectedQuestionCatalogue = value;
                    OnPropertyChanged();
                }
            }
        }

      
       

        public QuestionCatalogueViewModel()
        {
            _context = new SafetyCheckContext();
            QuestionCatalogues = new ObservableCollection<QuestionCatalogue>(_context.QuestionCatalogues.ToList());
        }
        // path: IndoorPlaygroundSafetyCheck/ViewModels/QuestionCatalogueViewModel.cs

        private string _warningMessage;
        public string WarningMessage
        {
            get => _warningMessage;
            set
            {
                if (_warningMessage != value)
                {
                    _warningMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public void SaveQuestionCatalogue(string description)
        {
            if (!string.IsNullOrEmpty(description))
            {
                var newQuestionCatalogue = new QuestionCatalogue
                {
                    Description = description,
                    InsertedBy = LoginViewModel.LoggedInUser.FullName,
                    UpdatedBy = LoginViewModel.LoggedInUser.FullName,
                    InsertTimeStamp = DateTime.Now,
                    UpdateTimeStamp = DateTime.Now
                };

                _context.QuestionCatalogues.Add(newQuestionCatalogue);
                _context.SaveChanges();
                QuestionCatalogues.Add(newQuestionCatalogue);
            }
        }

        public void DeleteQuestionCatalogue()
        {
            if (SelectedQuestionCatalogue != null)
            {
                try
                {
                    _context.QuestionCatalogues.Remove(SelectedQuestionCatalogue);
                    _context.SaveChanges();
                    QuestionCatalogues.Remove(SelectedQuestionCatalogue);
                    WarningMessage = string.Empty; // Clear previous messages if deletion is successful
                }
                catch (DbUpdateException ex) when (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlEx)
                {
                    // SQL Server's FK constraint violation error code is 547
                    if (sqlEx.Number == 547)
                    {
                        WarningMessage = "Das ausgewählte Element hat einen Eintrag in der Historie. Aufgrund dessen ist das Löschen nicht gestattet.";
                    }
                    else
                    {
                        // Optionally log or re-throw other exceptions
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    // Log or handle any unexpected exceptions
                    WarningMessage = $"Unexpected error: {ex.Message}";
                }
            }
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
