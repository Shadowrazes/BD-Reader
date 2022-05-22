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
            public ColumnListItem(/*string _TableName,*/ string _ColumnName)
            {
                //TableName = _TableName + ": ";
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
        private Dictionary<string, string> Keys = new Dictionary<string, string>()
        {
            { "CarId", "Id"},
            { "FullName", "DriverFullName"},
            { "EventName", "Name"},
            { "TeamName", "TeamName"}
        };

        public QueryManagerViewModel(DBViewerViewModel _DBViewer)
        {
            DbViewer = _DBViewer;
            tables = DbViewer.Tables;
            requests = DbViewer.Requests;
            filters = new ObservableCollection<Filter>();
            columnList = new ObservableCollection<ColumnListItem>();

            SelectedTables = new List<Table>();
            JoinedTable = new List<Dictionary<string, object?>>();

            Filters.Add(new Filter("", ColumnList));
        }

        public void UpdateColumnList()
        {
            ColumnList = new ObservableCollection<ColumnListItem>();
            if (JoinedTable.Count != 0)
            {
                foreach (var column in JoinedTable[0])
                {
                    ColumnList.Add(new ColumnListItem(column.Key));
                }
            }
            Filters.Clear();
            Filters.Add(new Filter("", ColumnList));
        }

        private bool TryJoin(string key1, List<Dictionary<string, object?>> table2, string key2)
        {
            try
            {
                JoinedTable = JoinedTable.Join(
                    table2,
                    firstItem => firstItem[key1],
                    secondItem => secondItem[key2],
                    (firstItem, secondItem) =>
                    {
                        Dictionary<string, object?> resultItem = new Dictionary<string, object?>();
                        foreach (var item in firstItem)
                        {
                            resultItem.TryAdd(item.Key, item.Value);
                        }
                        foreach (var item in secondItem)
                        {
                            if (item.Key != key2)
                                resultItem.TryAdd(item.Key, item.Value);
                        }
                        return resultItem;
                    }
                    ).ToList();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void JoinTables()
        {
            if (SelectedTables.Count > 0)
            {
                var check = SelectedTables.Where(tab => tab.Name == "Events");
                if(check.Count() != 0)
                {
                    Table tmp = check.Last();
                    SelectedTables.Remove(check.Last());
                    SelectedTables.Add(tmp);
                }
                JoinedTable = new List<Dictionary<string, object?>>(SelectedTables[0].Rows);
                if (SelectedTables.Count > 1)
                {
                    List<Dictionary<string, object?>> joiningTable;
                    bool success = false;
                    for (int i = 1; i < SelectedTables.Count; i++)
                    {
                        joiningTable = SelectedTables[i].Rows;
                        foreach (var keysPair in Keys)
                        {
                            success = TryJoin(keysPair.Key, joiningTable, keysPair.Value);
                            if (success)
                                break;
                            else
                            {
                                success = TryJoin(keysPair.Value, joiningTable, keysPair.Key);
                                if (success)
                                    break;
                            }
                        }
                        if (!success)
                        {
                            JoinedTable.Clear();
                            UpdateColumnList();
                            return;
                        }
                    }
                }
                UpdateColumnList();
            }
            else
            {
                JoinedTable.Clear();
                ColumnList.Clear();
            }

            foreach(var item in JoinedTable)
            {
                var a = item;
            }
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

        public List<Dictionary<string, object?>> JoinedTable { get; set; }
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
