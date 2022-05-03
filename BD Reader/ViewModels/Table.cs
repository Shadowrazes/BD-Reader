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
    public class Table
    {
        private string name;
        private ViewModelBase tableView;
        public Table(string _name, ViewModelBase _tableView, ObservableCollection<string> _Properties)
        {
            name = _name;
            tableView = _tableView;
            Properties = _Properties;
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

        public ViewModelBase TableView
        {
            get
            {
                return tableView;
            }
            set
            {
                tableView = value;
            }
        }

        public ObservableCollection<string> Properties { get; set; }
    }
}
