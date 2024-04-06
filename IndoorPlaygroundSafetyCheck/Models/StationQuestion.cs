using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace IndoorPlaygroundSafetyCheck.Models
{
    public partial class StationQuestion : INotifyPropertyChanged
    {

        private bool _yesSelected;
        private bool _noSelected;
        private bool _naSelected;

        public int Ident { get; set; }
        public int QuestionCatalogueIdent { get; set; }
        public int StationIdent { get; set; }
        public DateTime InsertTimeStamp { get; set; }
        public string InsertedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime UpdateTimeStamp { get; set; }
        public string QuestionText { get; set; }
        public DateTime? RepairedTime { get; set; }

        // Define RepairPlan property
        public DateTime? RepairPlan { get; set; }


        public int? ErrorType { get; set; }

        public virtual ICollection<InspectionQuestionResult> InspectionQuestionResults { get; set; } = new List<InspectionQuestionResult>();
        public virtual QuestionCatalogue QuestionCatalogueIdentNavigation { get; set; } = null!;
        public virtual Station StationIdentNavigation { get; set; } = null!;

        [NotMapped] // Indicates EF should ignore the YesSelected property
        public bool YesSelected
        {
            get => _yesSelected;
            set => SetProperty(ref _yesSelected, value, nameof(YesSelected));
        }

        [NotMapped] // Indicates EF should ignore the NoSelected property
        public bool NoSelected
        {
            get => _noSelected;
            set => SetProperty(ref _noSelected, value, nameof(NoSelected));
        }

        [NotMapped] // Indicates EF should ignore the NaSelected property
        public bool NaSelected
        {
            get => _naSelected;
            set => SetProperty(ref _naSelected, value, nameof(NaSelected));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string? propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
            }
        }
        // Inside the StationQuestion class in your models namespace

        private string _notes;

        [NotMapped] // This ensures EF core does not try to map this property to a database column
        public string Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value, nameof(Notes));
        }
      
    }
}
