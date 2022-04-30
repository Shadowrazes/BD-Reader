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
        private ObservableCollection<Team> teams;
        public TeamsTableViewModel(ObservableCollection<Team> _teams)
        {
            Teams = _teams;
        }

        public ObservableCollection<Team> Teams
        {
            get
            {
                return teams;
            }
            set
            {
                teams = value;
            }
        }
    }
}
