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
        private ObservableCollection<Result> results;
        public ResultsTableViewModel(ObservableCollection<Result> _results)
        {
            Results = _results;
        }

        public ObservableCollection<Result> Results
        {
            get
            {
                return results;
            }
            set
            {
                results = value;
            }
        }
    }
}
