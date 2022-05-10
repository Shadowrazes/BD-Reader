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
    public class QueryManagerViewModel : ViewModelBase
    {
        abstract class Handler<T>
        {
            public Handler<T> NextHope { get; set; }
            public abstract void Try(ObservableCollection<T> list);
        }

        public class Filter
        {
            public Filter(string _BoolOper, ObservableCollection<ColumnListItem> _Columns)
            {
                BoolOper = _BoolOper;
                Columns = _Columns;
                Operators = new ObservableCollection<string> { 
                    ">", ">=", "=", "<>", "<", "<="
                };
            }
            public string BoolOper { get; set; }
            public ObservableCollection<ColumnListItem> Columns { get; set; }
            public ObservableCollection<string> Operators { get; set; }
            public string FilterVal { get; set; }
        }

        public class ColumnListItem
        {
            public ColumnListItem(string _TableName, string _ColumnName)
            {
                TableName = _TableName + ": ";
                ColumnName = _ColumnName;
            }
            public string TableName { get; set; }
            public string ColumnName { get; set; }
        }

        private DBViewerViewModel DbViewer;
        private ObservableCollection<Table> tables;
        private ObservableCollection<Table> requests;
        private ObservableCollection<ColumnListItem> columnList;
        private ObservableCollection<Filter> filters;

        public QueryManagerViewModel(DBViewerViewModel _DBViewer)
        {
            DbViewer = _DBViewer;
            tables = DbViewer.Tables;
            requests = DbViewer.Requests;
            filters = new ObservableCollection<Filter>();
            columnList = new ObservableCollection<ColumnListItem>();

            SelectedTables = new List<Table>();

            Filters.Add(new Filter("", ColumnList));
        }

        public void UpdateColumnList()
        {
            ColumnList = new ObservableCollection<ColumnListItem>();
            foreach (Table table in SelectedTables)
            {
                foreach (var column in table.Properties)
                {
                    ColumnList.Add(new ColumnListItem(table.Name, column));
                }
            }
            Filters.Clear();
            Filters.Add(new Filter("", ColumnList));
        }

        public void AddFilterOR()
        {
            Filters.Add(new Filter("OR", ColumnList));
        }

        public void AddFilterAND()
        {
            Filters.Add(new Filter("AND", ColumnList));
        }

        public void AddRequest(string tableName)
        {
            List<List<object>> list = new List<List<object>>();
            for (int j = 0; j < 30; j++)
            {
                List<object> a = new List<object>();
                for (int i = 0; i < 65; i++)
                {
                    a.Add("Ok" + i.ToString());
                }
                list.Add(a);
            }
            tables.Add(new Table(tableName, true, new QueryTableViewModel(list), new ObservableCollection<string>()));
            Requests.Add(tables.Last<Table>());
        }

        public List<Table> SelectedTables { get; set; }

        public ObservableCollection<Filter> Filters
        {
            get => filters;
            set
            {
                this.RaiseAndSetIfChanged(ref filters, value);
            }
        }
        public ObservableCollection<ColumnListItem> ColumnList
        {
            get => columnList;
            set
            {
                this.RaiseAndSetIfChanged(ref columnList, value);
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
        public ObservableCollection<Table> Requests
        {
            get => requests;
            set
            {
                this.RaiseAndSetIfChanged(ref requests, value);
            }
        }
        public DBViewerViewModel DBViewer { get; }
    }
}
