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
    public class ResultsTableViewModel : ViewModelBase
    {
        private ObservableCollection<Result> table;
        public ResultsTableViewModel(ObservableCollection<Result> _results)
        {
            Table = _results;
            RemovableItems = new List<object>();
        }

        public ObservableCollection<Result> Table
        {
            get
            {
                return table;
            }
            set
            {
                table = value;
            }
        }

        public override ObservableCollection<Result> GetTable()
        {
            return Table;
        }
    }
}
