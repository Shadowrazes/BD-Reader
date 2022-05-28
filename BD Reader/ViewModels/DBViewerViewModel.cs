// DBViewerViewModel
// Реализация логики окна просмотра таблиц

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
        private ObservableCollection<Table> m_tables;       // Таблицы БД
        private ObservableCollection<Table> m_allTables;    // Таблицы БД + таблицы запросов
        private ObservableCollection<Driver> m_drivers;     // Таблица пилотов
        private ObservableCollection<Car> m_cars;           // Таблица автомобилей
        private ObservableCollection<Event> m_events;       // Таблица событий
        private ObservableCollection<Result> m_results;     // Таблица результатов
        private ObservableCollection<Team> m_teams;         // Таблица команд
        private bool m_currentTableIsSubtable;              // Является ли отображаемая таблица таблицей запроса

        // Находим названия колонок каждой таблицы БД
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
                m_tables = new ObservableCollection<Table>();
                DataBase = new WRCContext();

                string tableInfo = DataBase.Model.ToDebugString();
                tableInfo = tableInfo.Replace(" ", "");
                string[] splitTableInfo = tableInfo.Split("\r\n");
                List<string> properties = new List<string>(splitTableInfo.Where(str => str.IndexOf("Entity") != -1 ||
                                                            (str.IndexOf("(") != -1 && str.IndexOf("<") == -1) &&
                                                            str.IndexOf("Navigation") == -1 && str.IndexOf("(Car)") == -1));


                // Загружаем таблицы БД и создаем объекты Table
                DataBase.Drivers.Load<Driver>();
                Drivers = DataBase.Drivers.Local.ToObservableCollection();
                m_tables.Add(new Table("Drivers", false, new DriversTableViewModel(Drivers), FindProperties("Driver", properties)));

                DataBase.Cars.Load<Car>();
                Cars = DataBase.Cars.Local.ToObservableCollection();
                m_tables.Add(new Table("Cars", false, new CarsTableViewModel(Cars), FindProperties("Car", properties)));

                DataBase.Events.Load<Event>();
                Events = DataBase.Events.Local.ToObservableCollection();
                m_tables.Add(new Table("Events", false, new EventsTableViewModel(Events), FindProperties("Event", properties)));

                DataBase.Results.Load<Result>();
                Results = DataBase.Results.Local.ToObservableCollection();
                m_tables.Add(new Table("Results", false, new ResultsTableViewModel(Results), FindProperties("Result", properties)));

                DataBase.Teams.Load<Team>();
                Teams = DataBase.Teams.Local.ToObservableCollection();
                m_tables.Add(new Table("Teams", false, new TeamsTableViewModel(Teams), FindProperties("Team", properties)));

                AllTables = new ObservableCollection<Table>(Tables.ToList());

                // Имя начальной таблицы
                CurrentTableName = "Drivers";

                // Эта таблицы не запросная
                CurrentTableIsSubtable = false;
            }
            catch
            {
                
            }
        }

        public bool CurrentTableIsSubtable
        {
            get => !m_currentTableIsSubtable;
            set => this.RaiseAndSetIfChanged(ref m_currentTableIsSubtable, value);
        }
        public string CurrentTableName { get; set; }
        public WRCContext DataBase { get; set; }
        public ObservableCollection<Table> Tables
        {
            get => m_tables;
            set
            {
                this.RaiseAndSetIfChanged(ref m_tables, value);
            }
        }
        public ObservableCollection<Table> AllTables
        {
            get => m_allTables;
            set
            {
                this.RaiseAndSetIfChanged(ref m_allTables, value);
            }
        }
        public ObservableCollection<Driver> Drivers
        {
            get => m_drivers;
            set
            {
                this.RaiseAndSetIfChanged(ref m_drivers, value);
            }
        }
        public ObservableCollection<Car> Cars
        {
            get => m_cars;
            set
            {
                this.RaiseAndSetIfChanged(ref m_cars, value);
            }
        }
        public ObservableCollection<Event> Events
        {
            get => m_events;
            set
            {
                this.RaiseAndSetIfChanged(ref m_events, value);
            }
        }
        public ObservableCollection<Result> Results
        {
            get => m_results;
            set
            {
                this.RaiseAndSetIfChanged(ref m_results, value);
            }
        }
        public ObservableCollection<Team> Teams
        {
            get => m_teams;
            set
            {
                this.RaiseAndSetIfChanged(ref m_teams, value);
            }
        }

        // В зависимости от отображаемой таблицы выбираем массив, в который добавится новая запись
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

        // В зависимости от отображаемой таблицы выбираем массив, из которого удалятся выбранные записи
        public void DeleteItems()
        {
            // Выбираем нужную таблицы
            Table currentTable = Tables.Where(table => table.Name == CurrentTableName).ToList()[0];

            // Получаем удаляемые элементы
            List<object>? RemovableItems = currentTable.GetRemovableItems();

            // Помечаем, что идет удаление, чтобы событие DataGrid'a на изменение выбранной строчки не срабатывало
            currentTable.SetRemoveInProgress(true);

            // Если список удаляемых элементов не пуст
            if (RemovableItems != null && RemovableItems.Count != 0) {

                // Выбираем таблицу по имени и удаляем элементы
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

        // Сохраняем изменения в БД
        public void Save()
        {
            DataBase.SaveChanges();
        }
    }
}
