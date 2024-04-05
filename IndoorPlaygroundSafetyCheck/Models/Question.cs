using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndoorPlaygroundSafetyCheck.Models
{
    public class Question : INotifyPropertyChanged
    {
        private string _answer;

        public int Ident { get; set; } // Unique identifier for each question
        public string Text { get; set; }

        public string Answer
        {
            get => _answer;
            set
            {
                if (_answer != value)
                {
                    _answer = value;
                    OnPropertyChanged(nameof(Answer));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
