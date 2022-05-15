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
        private ObservableCollection<Driver> table;
        public DriversTableViewModel(ObservableCollection<Driver> _drivers)
        {
            Table = _drivers;
        }

        public ObservableCollection<Driver> Table
        {
            get
            {
                return table;
            }
            set
            {
                table = value;
            }
        }

        public override ObservableCollection<Driver> GetTable()
        {
            return Table;
        }
    }
}
