using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IndoorPlaygroundSafetyCheck.ViewModels
{
        public class AfterRepairInspectionViewModel : INotifyPropertyChanged
        {
            // Example of a property that notifies the view of changes
            private string _exampleProperty;
            public string ExampleProperty
            {
                get => _exampleProperty;
                set
                {
                    _exampleProperty = value;
                    OnPropertyChanged();
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            // Add additional properties and methods relevant to the AfterRepairInspection view
        }
    }

