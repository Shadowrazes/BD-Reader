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
    public class TeamsTableViewModel : ViewModelBase
    {
        private ObservableCollection<Team> table;
        public TeamsTableViewModel(ObservableCollection<Team> _teams)
        {
            Table = _teams;
            RemovableItems = new List<object>();
        }

        public ObservableCollection<Team> Table
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

        public override ObservableCollection<Team> GetTable()
        {
            return Table;
        }
    }
}
