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
    public class MainWindowViewModel : ViewModelBase
    {
        private string DbPath = @"Assets/WRC.db";
        private ObservableCollection<Table> tables;
        private SqliteConnection connection;
        private SqliteCommand command;
        private SqliteDataReader reader;

        public MainWindowViewModel()
        {
            try
            {
                string directoryPath = Directory.GetCurrentDirectory();
                directoryPath = directoryPath.Remove(directoryPath.LastIndexOf("bin"));
                DbPath = directoryPath + DbPath;
                tables = new ObservableCollection<Table>();

                connection = new SqliteConnection($"Data Source={DbPath}");
                connection.Open();
                command = connection.CreateCommand();
                LoadDrivers();
                LoadCars();
                LoadTeams();
                LoadResults();
                LoadEvents();
            }
            catch
            {

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

        void LoadEvents()
        {
            ObservableCollection<Event> data = new ObservableCollection<Event>();
            command.CommandText = "SELECT * FROM Events";
            try
            {
                reader = command.ExecuteReader();
                if (reader.HasRows != false)
                {
                    while (reader.Read() != false)
                    {
                        data.Add(new Event(reader[0] as string, reader[1] as string, reader[2] as string));
                    }
                    tables.Add(new Table("Events", new EventsTableViewModel(data)));
                }
                reader.Close();
            }
            catch
            {

            }
        }

        void LoadDrivers()
        {
            ObservableCollection<Driver> data = new ObservableCollection<Driver>();
            command.CommandText = "SELECT * FROM Drivers";
            try
            {
                reader = command.ExecuteReader();
                if (reader.HasRows != false)
                {
                    while (reader.Read() != false)
                    {
                        uint age;
                        try
                        {
                            age = Convert.ToUInt16(reader[3]);
                        }
                        catch
                        {
                            age = 0;
                        }

                        data.Add(new Driver(reader[0] as string, Convert.ToUInt16(reader[1]), reader[2] as string, age,
                            Convert.ToUInt16(reader[4]), Convert.ToUInt16(reader[5]), Convert.ToDouble(reader[6])));
                    }
                    tables.Add(new Table("Drivers", new DriversTableViewModel(data)));
                }
                reader.Close();
            }
            catch
            {
               
            }
        }

        void LoadCars()
        {
            ObservableCollection<Car> data = new ObservableCollection<Car>();
            command.CommandText = "SELECT * FROM Cars";
            try
            {
                reader = command.ExecuteReader();
                if (reader.HasRows != false)
                {
                    while (reader.Read() != false)
                    {
                        data.Add(new Car(Convert.ToUInt16(reader[0]), Convert.ToUInt16(reader[1]),
                            reader[2] as string, reader[3] as string, reader[4] as string));
                    }
                    tables.Add(new Table("Cars", new CarsTableViewModel(data)));
                }
                reader.Close();
            }
            catch
            {

            }
        }

        void LoadResults()
        {
            ObservableCollection<Result> data = new ObservableCollection<Result>();
            command.CommandText = "SELECT * FROM Results";
            try
            {
                reader = command.ExecuteReader();
                if (reader.HasRows != false)
                {
                    while (reader.Read() != false)
                    {
                        data.Add(new Result(reader[0] as string, reader[1] as string,
                            reader[2] as string, Convert.ToUInt16(reader[3]), reader[4] as string));
                    }
                    tables.Add(new Table("Results", new ResultsTableViewModel(data)));
                }
                reader.Close();
            }
            catch
            {

            }
        }

        void LoadTeams()
        {
            ObservableCollection<Team> data = new ObservableCollection<Team>();
            command.CommandText = "SELECT * FROM Teams";
            try
            {
                reader = command.ExecuteReader();
                if (reader.HasRows != false)
                {
                    while (reader.Read() != false)
                    {
                        data.Add(new Team(reader[0] as string, Convert.ToUInt16(reader[1]), Convert.ToUInt16(reader[2]),
                            Convert.ToUInt16(reader[3]), Convert.ToUInt16(reader[4])));
                    }
                    tables.Add(new Table("Teams", new TeamsTableViewModel(data)));
                }
                reader.Close();
            }
            catch
            {

            }
        }
    }
}
