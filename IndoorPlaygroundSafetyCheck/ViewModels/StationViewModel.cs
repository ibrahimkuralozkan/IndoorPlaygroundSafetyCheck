using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IndoorPlaygroundSafetyCheck.Models;
using IndoorPlaygroundSafetyCheck.Data;

namespace IndoorPlaygroundSafetyCheck.ViewModels
{


    public class StationViewModel : INotifyPropertyChanged
    {
        private Data.SafetyCheckContext _context;
        public ObservableCollection<Station> Stations { get; set; }

        public StationViewModel()
        {
            _context = new Data.SafetyCheckContext();
            Stations = new ObservableCollection<Station>(_context.Stations.ToList());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

            public Station SelectedStation { get; set; }

            public void AddStation(string name)
            {
                var newStation = new Station { Name = name };
                _context.Stations.Add(newStation);
                _context.SaveChanges();
                Stations.Add(newStation);
            }

            public void UpdateStation(Station station, string newName)
            {
                station.Name = newName;
                _context.Stations.Update(station);
                _context.SaveChanges();
                OnPropertyChanged(nameof(Stations));
            }

            public void DeleteStation(Station station)
            {
                _context.Stations.Remove(station);
                _context.SaveChanges();
                Stations.Remove(station);
            }
        }

    }
