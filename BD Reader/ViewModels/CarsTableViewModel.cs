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
        private ObservableCollection<Car> cars;
        public CarsTableViewModel(ObservableCollection<Car> _cars)
        {
            Cars = _cars;
        }

        public ObservableCollection<Car> Cars
        {
            get
            {
                return cars;
            }
            set
            {
                cars = value;
            }
        }
    }
}
