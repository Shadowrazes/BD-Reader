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
    public class DBViewerViewModel : ViewModelBase
    {
        public class Table
        {
            private string name;
            private ViewModelBase tableList;
            public Table(string _name, ViewModelBase _tableList)
            {
                name = _name;
                tableList = _tableList;
            }

            public string Name
            {
                get
                {
                    return name;
                }
                set
                {
                    name = value;
                }
            }

            public ViewModelBase TableList
            {
                get
                {
                    return tableList;
                }
                set
                {
                    tableList = value;
                }
            }
        }

        private ObservableCollection<Table> tables;
        private List<List<string>> queryTable = new List<List<string>>();
        private ObservableCollection<Driver> drivers;
        ObservableCollection<Car> cars;
        ObservableCollection<Event> events;
        ObservableCollection<Result> results;
        ObservableCollection<Team> teams;

        public DBViewerViewModel()
        {
            try
            {
                tables = new ObservableCollection<Table>();
                var DataBase = new WRCContext();

                foreach (var car in DataBase.Cars.Where(i => i.Id == 1))
                {
                    var a = 0;
                }

                drivers = new ObservableCollection<Driver>(DataBase.Drivers);
                tables.Add(new Table("Drivers", new DriversTableViewModel(drivers)));

                cars = new ObservableCollection<Car>(DataBase.Cars);
                tables.Add(new Table("Cars", new CarsTableViewModel(cars)));

                events = new ObservableCollection<Event>(DataBase.Events);
                tables.Add(new Table("Events", new EventsTableViewModel(events)));

                results = new ObservableCollection<Result>(DataBase.Results);
                tables.Add(new Table("Results", new ResultsTableViewModel(results)));

                teams = new ObservableCollection<Team>(DataBase.Teams);
                tables.Add(new Table("Teams", new TeamsTableViewModel(teams)));
            }
            catch
            {
                var a = 0;
            }
        }

        public ObservableCollection<Table> Tables
        {
            get
            {
                return tables;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref tables, value);
            }
        }
    }
}
