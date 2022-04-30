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
        private ObservableCollection<Event> events;
        public EventsTableViewModel(ObservableCollection<Event> _events)
        {
            Events = _events;
        }

        public ObservableCollection<Event> Events
        {
            get
            {
                return events;
            }
            set
            {
                events = value;
            }
        }
    }
}
