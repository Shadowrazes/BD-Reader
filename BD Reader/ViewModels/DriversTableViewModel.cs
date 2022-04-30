using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ReactiveUI;
using System.Reactive;
using BD_Reader.Models;
using Microsoft.Data.Sqlite;
using System.IO;
using System;

namespace BD_Reader.ViewModels
{
    public class DriversTableViewModel : ViewModelBase
    {
        private ObservableCollection<Driver> drivers;
        public DriversTableViewModel(ObservableCollection<Driver> _drivers)
        {
            Drivers = _drivers;
        }

        public ObservableCollection<Driver> Drivers
        {
            get
            {
                return drivers;
            }
            set
            {
                drivers = value;
            }
        }
    }
}
