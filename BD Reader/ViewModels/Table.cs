﻿using System.Collections.Generic;
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
        public Table(string _name, bool _IsSubTable, ViewModelBase _tableView, ObservableCollection<string> _Properties)
        {
            name = _name;
            IsSubTable = _IsSubTable;
            tableView = _tableView;
            Properties = _Properties;
            Rows = new List<Dictionary<string, object?>>();
            dynamic table = TableView.GetTable();
            if (table != null)
            {
                Key = table[0].Key();
                for (int j = 0; j < table.Count; j++)
                {
                    Dictionary<string, object?> tmp = new Dictionary<string, object?>();
                    foreach (string prop in Properties)
                    {
                        tmp.Add(prop, table[j][prop]);
                    }
                    Rows.Add(tmp);
                }
            }
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

        public string Key { get; set; }

        public bool IsSubTable { get; }

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

        public List<Dictionary<string, object?>> Rows { get; }

        public ObservableCollection<string> Properties { get; set; }
    }
}
