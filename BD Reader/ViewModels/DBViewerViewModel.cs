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
        private ObservableCollection<Driver> drivers;
        private ObservableCollection<Car> cars;
        private ObservableCollection<Event> events;
        private ObservableCollection<Result> results;
        private ObservableCollection<Team> teams;
        private ObservableCollection<Table> requests;

        private ObservableCollection<string> FindProperties(string entityName, List<string> properties)
        {
            ObservableCollection<string> result = new ObservableCollection<string>();
            for (int i = 0; i < properties.Count(); i++)
            {
                if (properties[i].IndexOf("EntityType:" + entityName) != -1)
                {
                    try
                    {
                        i++;
                        while (properties[i].IndexOf("(") != -1 && i < properties.Count())
                        {
                            result.Add(properties[i].Remove(properties[i].IndexOf("(")));
                            i++;
                        }
                        return result;
                    }
                    catch
                    {
                        return result;
                    }
                }
            }
            return result;
        }

        public DBViewerViewModel()
        {
            try
            {
                tables = new ObservableCollection<Table>();
                requests = new ObservableCollection<Table>();
                var DataBase = new WRCContext();

                string tableInfo = DataBase.Model.ToDebugString();
                tableInfo = tableInfo.Replace(" ", "");
                string[] splitTableInfo = tableInfo.Split("\r\n");
                List<string> properties = new List<string>(splitTableInfo.Where(str => str.IndexOf("Entity") != -1 ||
                                                            (str.IndexOf("(") != -1 && str.IndexOf("<") == -1) &&
                                                            str.IndexOf("Navigation") == -1 && str.IndexOf("(Car)") == -1));
               


                drivers = new ObservableCollection<Driver>(DataBase.Drivers);
                tables.Add(new Table("Drivers", false, new DriversTableViewModel(drivers), FindProperties("Driver", properties)));

                cars = new ObservableCollection<Car>(DataBase.Cars);
                tables.Add(new Table("Cars", false, new CarsTableViewModel(cars), FindProperties("Car", properties)));

                events = new ObservableCollection<Event>(DataBase.Events);
                tables.Add(new Table("Events", false, new EventsTableViewModel(events), FindProperties("Event", properties)));

                results = new ObservableCollection<Result>(DataBase.Results);
                tables.Add(new Table("Results", false, new ResultsTableViewModel(results), FindProperties("Result", properties)));

                teams = new ObservableCollection<Team>(DataBase.Teams);
                tables.Add(new Table("Teams", false, new TeamsTableViewModel(teams), FindProperties("Team", properties)));
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
        public ObservableCollection<Table> Requests
        {
            get => requests;
            set
            {
                this.RaiseAndSetIfChanged(ref requests, value);
            }
        }
    }
}
