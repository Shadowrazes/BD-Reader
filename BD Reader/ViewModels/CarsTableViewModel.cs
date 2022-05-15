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
    public class CarsTableViewModel : ViewModelBase
    {
        private ObservableCollection<Car> table;
        public CarsTableViewModel(ObservableCollection<Car> _cars)
        {
            Table = _cars;
        }

        public ObservableCollection<Car> Table
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

        public override ObservableCollection<Car> GetTable()
        {
            return Table;
        }
    }
}
