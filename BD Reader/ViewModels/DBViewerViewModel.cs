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

                //cars = new ObservableCollection<Car>(DataBase.Cars);
                //tables.Add(new Table("Cars", new CarsTableViewModel(cars)));

                events = new ObservableCollection<Event>(DataBase.Events);
                tables.Add(new Table("Events", new EventsTableViewModel(events)));

                //results = new ObservableCollection<Result>(DataBase.Results);
                //tables.Add(new Table("Results", new ResultsTableViewModel(results)));

                teams = new ObservableCollection<Team>(DataBase.Teams);
                tables.Add(new Table("Teams", new TeamsTableViewModel(teams)));

                //var res = from result in results join evenet in events on result.EventName equals evenet.Name select new { resulte }; 
            }
            catch
            {
                var a = 0;
            }
        }

        public ObservableCollection<Table> Tables
        {
            get => tables;
            set
            {
                this.RaiseAndSetIfChanged(ref tables, value);
            }
        }
        public ObservableCollection<Driver> Drivers
        {
            get => drivers;
            set
            {
                this.RaiseAndSetIfChanged(ref drivers, value);
            }
        }
        public ObservableCollection<Car> Cars
        {
            get => cars;
            set
            {
                this.RaiseAndSetIfChanged(ref cars, value);
            }
        }
        public ObservableCollection<Event> Events
        {
            get => events;
            set
            {
                this.RaiseAndSetIfChanged(ref events, value);
            }
        }
        public ObservableCollection<Result> Results
        {
            get => results;
            set
            {
                this.RaiseAndSetIfChanged(ref results, value);
            }
        }
        public ObservableCollection<Team> Teams
        {
            get => teams;
            set
            {
                this.RaiseAndSetIfChanged(ref teams, value);
            }
        }
    }
}
