using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using IndoorPlaygroundSafetyCheck.Data;
using IndoorPlaygroundSafetyCheck.Models;
using Microsoft.EntityFrameworkCore;
using System;

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

        public void SaveQuestionCatalogue(string description)
        {
            if (!string.IsNullOrEmpty(description))
            {
                var newQuestionCatalogue = new QuestionCatalogue
                {
                    Description = description,
                    InsertedBy = LoginViewModel.LoggedInUser.FullName, // Ensure you have access to the current user's FullName
                    UpdatedBy = LoginViewModel.LoggedInUser.FullName,
                    InsertTimeStamp = DateTime.Now, // Assuming these properties exist in your model
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
                _context.QuestionCatalogues.Remove(SelectedQuestionCatalogue);
                _context.SaveChanges();
                QuestionCatalogues.Remove(SelectedQuestionCatalogue);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
