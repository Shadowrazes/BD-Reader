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
    public class QueryTableViewModel : MainWindowViewModel
    {
        private List<List<string>> queryList;
        public QueryTableViewModel(List<List<string>> _queryList)
        {
            queryList = _queryList;
        }

        public List<List<string>> QueryList
        {
            get
            {
                return queryList;
            }
        }
    }
}
