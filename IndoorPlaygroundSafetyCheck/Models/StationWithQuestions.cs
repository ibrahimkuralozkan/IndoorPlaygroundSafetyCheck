using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Windows.Input;

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IndoorPlaygroundSafetyCheck.Models
{
    public class StationWithQuestions : INotifyPropertyChanged
    {
        public int StationIdent { get; set; }
        public string StationName { get; set; }
        public List<StationQuestion> StationQuestions { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
