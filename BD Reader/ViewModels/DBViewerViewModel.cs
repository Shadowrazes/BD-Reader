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
using Avalonia.Controls;
using Microsoft.EntityFrameworkCore;

namespace BD_Reader.ViewModels
{
    public class DBViewerViewModel : ViewModelBase
    {
        private ObservableCollection<Table> tables;
        private ObservableCollection<Table> allTables;
        private ObservableCollection<Driver> drivers;
        private ObservableCollection<Car> cars;
        private ObservableCollection<Event> events;
        private ObservableCollection<Result> results;
        private ObservableCollection<Team> teams;
        private bool currentTableIsSubtable;

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
                            string property = properties[i].Remove(properties[i].IndexOf("("));
                            if (entityName == "Team" && property == "Name")
                                result.Add("TeamName");
                            else if (entityName == "Event" && property == "Name")
                                result.Add("EventName");
                            else if (entityName == "Driver" && property == "FullName")
                                result.Add("DriverFullName");
                            else if (entityName == "Car" && property == "Id")
                                result.Add("CarId");
                            else
                                result.Add(property);


                            //if(!(entityName == "Team" && property == "Name"))
                            //    result.Add(property);
                            //else
                            //    result.Add("TeamName");
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
                DataBase = new WRCContext();

                string tableInfo = DataBase.Model.ToDebugString();
                tableInfo = tableInfo.Replace(" ", "");
                string[] splitTableInfo = tableInfo.Split("\r\n");
                List<string> properties = new List<string>(splitTableInfo.Where(str => str.IndexOf("Entity") != -1 ||
                                                            (str.IndexOf("(") != -1 && str.IndexOf("<") == -1) &&
                                                            str.IndexOf("Navigation") == -1 && str.IndexOf("(Car)") == -1));


                DataBase.Drivers.Load<Driver>();
                Drivers = DataBase.Drivers.Local.ToObservableCollection();
                tables.Add(new Table("Drivers", false, new DriversTableViewModel(Drivers), FindProperties("Driver", properties)));

                DataBase.Cars.Load<Car>();
                Cars = DataBase.Cars.Local.ToObservableCollection();
                tables.Add(new Table("Cars", false, new CarsTableViewModel(Cars), FindProperties("Car", properties)));

                DataBase.Events.Load<Event>();
                Events = DataBase.Events.Local.ToObservableCollection();
                tables.Add(new Table("Events", false, new EventsTableViewModel(Events), FindProperties("Event", properties)));

                DataBase.Results.Load<Result>();
                Results = DataBase.Results.Local.ToObservableCollection();
                tables.Add(new Table("Results", false, new ResultsTableViewModel(Results), FindProperties("Result", properties)));

                DataBase.Teams.Load<Team>();
                Teams = DataBase.Teams.Local.ToObservableCollection();
                tables.Add(new Table("Teams", false, new TeamsTableViewModel(Teams), FindProperties("Team", properties)));

                AllTables = new ObservableCollection<Table>(Tables.ToList());

                CurrentTableName = "Drivers";
                CurrentTableIsSubtable = false;
            }
            catch
            {
                
            }
        }

        public bool CurrentTableIsSubtable
        {
            get => !currentTableIsSubtable;
            set => this.RaiseAndSetIfChanged(ref currentTableIsSubtable, value);
        }
        public string CurrentTableName { get; set; }
        public WRCContext DataBase { get; set; }
        public ObservableCollection<Table> Tables
        {
            get => tables;
            set
            {
                this.RaiseAndSetIfChanged(ref tables, value);
            }
        }
        public ObservableCollection<Table> AllTables
        {
            get => allTables;
            set
            {
                this.RaiseAndSetIfChanged(ref allTables, value);
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

        public void AddItem()
        {
            switch (CurrentTableName)
            {
                case "Drivers":
                    {
                        Drivers.Add(new Driver());
                        break;
                    }
                case "Cars":
                    {
                        Cars.Add(new Car());
                        break;
                    }
                case "Results":
                    {
                        Results.Add(new Result());
                        break;
                    }
                case "Events":
                    {
                        Events.Add(new Event());
                        break;
                    }
                case "Teams":
                    {
                        Teams.Add(new Team());
                        break;
                    }
            }
        }

        public void DeleteItems()
        {
            Table currentTable = Tables.Where(table => table.Name == CurrentTableName).ToList()[0];
            List<object>? RemovableItems = currentTable.GetRemovableItems();
            currentTable.SetRemoveInProgress(true);
            if (RemovableItems != null && RemovableItems.Count != 0) {
                switch (CurrentTableName)
                {
                    case "Drivers":
                        {
                            for(int i = 0; i < RemovableItems.Count; i++)
                            {
                                Drivers.Remove(RemovableItems[i] as Driver);
                            }
                            break;
                        }
                    case "Cars":
                        {
                            for(int i = 0; i < RemovableItems.Count; i++)
                            {
                                Cars.Remove(RemovableItems[i] as Car);
                            }
                            break;
                        }
                    case "Results":
                        {
                            for(int i = 0; i < RemovableItems.Count; i++)
                            {
                                Results.Remove(RemovableItems[i] as Result);
                            }
                            break;
                        }
                    case "Events":
                        {
                            for(int i = 0; i < RemovableItems.Count; i++)
                            {
                                Events.Remove(RemovableItems[i] as Event);
                            }
                            break;
                        }
                    case "Teams":
                        {
                            for(int i = 0; i < RemovableItems.Count; i++)
                            {
                                Teams.Remove(RemovableItems[i] as Team);
                            }
                            break;
                        }
                }
            }
            currentTable.SetRemoveInProgress(false);
        }

        public void Save()
        {
            DataBase.SaveChanges();
        }
    }
}
