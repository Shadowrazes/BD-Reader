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
            public Filter(string _BoolOper, ObservableCollection<string> _Columns)
            {
                BoolOper = _BoolOper;
                Columns = _Columns;
            }
            public string BoolOper { get; set; }
            public ObservableCollection<string> Columns { get; set; }
            public ObservableCollection<string> Operators { get; set; }
            public string FilterVal { get; set; }
        }

        private DBViewerViewModel DbViewer;
        private ObservableCollection<Table> tables;
        public QueryManagerViewModel(DBViewerViewModel _DBViewer)
        {
            DbViewer = _DBViewer;
            Filters = new ObservableCollection<Filter>();
            //DbViewer.Tables
            tables = DbViewer.Tables;
            ObservableCollection<string> columns = new ObservableCollection<string>();
            for (int i = 0; i < 10; i++)
            {
                columns.Add("ASssa");
            }
            for (int i = 0; i < 10; i++)
            {
                Filters.Add(new Filter("AND", columns));
            }
        }
        public ObservableCollection<Filter> Filters { get; set; }
        public ObservableCollection<Table> Tables
        {
            get => tables;
            set
            {
                this.RaiseAndSetIfChanged(ref tables, value);
            }
        }
        public DBViewerViewModel DBViewer { get; }
    }
}
