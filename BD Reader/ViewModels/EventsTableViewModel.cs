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
    public class EventsTableViewModel : ViewModelBase
    {
        private ObservableCollection<Event> table;
        public EventsTableViewModel(ObservableCollection<Event> _events)
        {
            Table = _events;
            RemovableItems = new List<object>();
        }

        public ObservableCollection<Event> Table
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

        public override ObservableCollection<Event> GetTable()
        {
            return Table;
        }
    }
}
